using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris.Objects.Enums;
using Tetris.Objects.TetrisEventArgs;
using Tetris.Objects.Utilities;

namespace Tetris.Objects
{
    public class TetrisGrid
    {
        public const int WIDTH = 10;
        public const int HEIGHT = 20;


        private Shape _activeShape;

        private int _rowsToFall;
        private int _matchStreak;
        private int _tSpinMultiplier;
        private List<int> _matchingRows = new List<int>();

        public event EventHandler<ShapeEventArgs> ShapeLanded;
        public event EventHandler GameOver;
        public event EventHandler MatchAnimationStart;
        public event EventHandler MatchAnimationEnd;
        public event EventHandler<MatchEventArgs> MatchedRows;
        public event EventHandler<MatchEventArgs> MatchFound;
        public event EventHandler ForcedGravity;

        private List<Block> _breakingBlocks = new List<Block>();

        public TetrisGridNode[,] BlockMatrix { get; private set; }

        public TetrisGrid()
        {
            InitializeMatrix();
        }



        #region public methods
        public void InsertShape(Shape shape)
        {
            if (shape == null)
                return;

            _activeShape = shape;

            int shapeCenterX = shape.BlockMatrix.GetLength(0) / 2;
            int gridCenterX = WIDTH / 2;

            int xLen = shape.BlockMatrix.GetLength(0);
            int yLen = shape.BlockMatrix.GetLength(1);
            int xOffset = gridCenterX - shapeCenterX;
            int yOffset = 0;
            shape.CurrentXOffset = xOffset;
            shape.CurrentYOffset = yOffset;
            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    var block = shape.BlockMatrix[i, j];
                    if (block != null)
                    {
                        int x = gridCenterX + (i - shapeCenterX);
                        int y = block.YIndex;
                        if (this.BlockMatrix[x, y].Block != null)
                        {
                            OnGameOver();
                            return;
                        }
                        this.BlockMatrix[x, y].Block = block;
                    }
                }
            }
        }

        public void ExecuteBlockGravity(bool forcedDown)
        {
            if (_activeShape != null)
            {
                //check index beneath
                //if it has a block or is larger than the height of the grid, stop
                if (_activeShape.IndexBelow >= HEIGHT || CollidesWithBlocksBelow())
                {
                    //Stop gravity and deactivate blocks
                    foreach (var block in _activeShape.Blocks)
                    {
                        block.Deactivate();
                    }
                    //evaluate for matches
                    List<int> rows = _activeShape.Blocks.Select(b => b.YIndex + _activeShape.CurrentYOffset).Distinct().ToList();
                    _matchingRows = GetMatchingRows(rows);
                  
                    _tSpinMultiplier = GetTSpinMultiplier();

                    //if matches, fire event for matching animation start
                    if (_matchingRows.Any())
                    {
                        _matchStreak++;
                        //execute block DestroyAnimation
                        OnMatchAnimationStart();
                        DestroyRows(_matchingRows);
                        OnMatchFound();
                        _activeShape = null;
                    }
                    else if (_tSpinMultiplier > 0)
                    {
                        _matchStreak = 0;
                        OnMatchFound();
                        OnShapeLanded();
                    }
                    else
                    {
                        _matchStreak = 0;
                        //if no matches fire event for gravity ending
                        OnShapeLanded();
                    }
                }
                else
                {
                    ExecuteBlockFall();
                    if (forcedDown)
                    {
                        if (_activeShape != null)
                        {
                            _activeShape.LastMove = MovementType.MOVE_DOWN;
                        }
                        OnForcedGravity();
                    }
                    else
                    {
                        _activeShape.LastMove = MovementType.FALL;
                    }
                }
            }
        }

        public void MoveShape(Direction direction)
        {
            if (_activeShape == null)
                return;

            switch (direction)
            {
                case Direction.LEFT:
                    //check if on left edge of map or bumping into block on the right
                    if (!(_activeShape.IndexLeft < 0 || CollidesWithBlocksHorizontally(direction)))
                    {
                        MoveLeft();
                    }
                    break;
                case Direction.RIGHT:
                    //check if on left edge of map or bumping into block on the right
                    if (!(_activeShape.IndexRight >= WIDTH || CollidesWithBlocksHorizontally(direction)))
                    {
                        MoveRight();
                    }
                    break;
                case Direction.DOWN:
                    ExecuteBlockGravity(true);
                    break;
            }
        }

        public void RotateShape()
        {
            if (_activeShape == null)
                return;

            var newShape = Shape.RotateClockwise(_activeShape);
            if (!CollidesInArea(newShape))
            {
                ClearActiveShapeFromGrid();
                ApplyNewShapeToGrid(newShape);
            }
        }

        #endregion

        #region private helper methods

        private int GetTSpinMultiplier()
        {
            if (_activeShape != null && _activeShape is TShape)
            {
                TShape shape = (TShape)_activeShape;
                if (shape.LastMove != MovementType.ROTATION)
                    return 0;

                int frontLeftCornerX = -1, frontLeftCornerY = -1, frontRightCornerX = -1, frontRightCornerY = -1;
                int backLeftCornerX = -1, backLeftCornerY = -1, backRightCornerX = -1, backRightCornerY = -1;

                bool isFrontLeftCornerAWall = false, isFrontRightCornerAWall = false;
                bool isBackLeftCornerAWall = false, isBackRightCornerAWall = false;

                switch (shape.Direction)
                {
                    case Direction.DOWN:
                        frontLeftCornerX = shape.CurrentXOffset + shape.TBlock.XIndex + 1;
                        frontLeftCornerY = shape.CurrentYOffset + shape.TBlock.YIndex;
                        frontRightCornerX = shape.CurrentXOffset + shape.TBlock.XIndex - 1;
                        frontRightCornerY = shape.CurrentYOffset + shape.TBlock.YIndex;
                        backLeftCornerX = shape.CurrentXOffset + shape.TBlock.XIndex + 1;
                        backLeftCornerY = shape.CurrentYOffset + shape.TBlock.YIndex - 2;
                        backRightCornerX = shape.CurrentXOffset + shape.TBlock.XIndex - 1;
                        backRightCornerY = shape.CurrentYOffset + shape.TBlock.YIndex - 2;
                        isFrontLeftCornerAWall = frontLeftCornerX < 0 || frontLeftCornerX >= WIDTH;
                        isFrontRightCornerAWall = frontRightCornerX < 0 || frontRightCornerX >= WIDTH;
                        isBackLeftCornerAWall = backLeftCornerY < 0;
                        isBackRightCornerAWall = backRightCornerY < 0;
                        break;
                    case Direction.UP:
                        frontLeftCornerX = shape.CurrentXOffset + shape.TBlock.XIndex - 1;
                        frontLeftCornerY = shape.CurrentYOffset + shape.TBlock.YIndex;
                        frontRightCornerX = shape.CurrentXOffset + shape.TBlock.XIndex + 1;
                        frontRightCornerY = shape.CurrentYOffset + shape.TBlock.YIndex;
                        backLeftCornerX = shape.CurrentXOffset + shape.TBlock.XIndex - 1;
                        backLeftCornerY = shape.CurrentYOffset + shape.TBlock.YIndex + 2;
                        backRightCornerX = shape.CurrentXOffset + shape.TBlock.XIndex + 1;
                        backRightCornerY = shape.CurrentYOffset + shape.TBlock.YIndex + 2;
                        isFrontLeftCornerAWall = frontLeftCornerX < 0 || frontLeftCornerX >= WIDTH;
                        isFrontRightCornerAWall = frontRightCornerX < 0 || frontRightCornerX >= WIDTH;
                        isBackLeftCornerAWall = backLeftCornerY >= HEIGHT;
                        isBackRightCornerAWall = backRightCornerY >= HEIGHT;
                        break;
                    case Direction.LEFT:
                        frontLeftCornerX = shape.CurrentXOffset + shape.TBlock.XIndex;
                        frontLeftCornerY = shape.CurrentYOffset + shape.TBlock.YIndex + 1;
                        frontRightCornerX = shape.CurrentXOffset + shape.TBlock.XIndex;
                        frontRightCornerY = shape.CurrentYOffset + shape.TBlock.YIndex - 1;
                        backLeftCornerX = shape.CurrentXOffset + shape.TBlock.XIndex + 2;
                        backLeftCornerY = shape.CurrentYOffset + shape.TBlock.YIndex + 1;
                        backRightCornerX = shape.CurrentXOffset + shape.TBlock.XIndex + 2;
                        backRightCornerY = shape.CurrentYOffset + shape.TBlock.YIndex - 1;
                        isFrontLeftCornerAWall = frontLeftCornerY >= HEIGHT;
                        isFrontRightCornerAWall = frontRightCornerY < 0;
                        isBackLeftCornerAWall = backLeftCornerX < 0 || backLeftCornerX >= WIDTH;
                        isBackRightCornerAWall = backRightCornerX < 0 || backLeftCornerX >= WIDTH;
                        break;
                    case Direction.RIGHT:
                        frontLeftCornerX = shape.CurrentXOffset + shape.TBlock.XIndex;
                        frontLeftCornerY = shape.CurrentYOffset + shape.TBlock.YIndex - 1;
                        frontRightCornerX = shape.CurrentXOffset + shape.TBlock.XIndex;
                        frontRightCornerY = shape.CurrentYOffset + shape.TBlock.YIndex + 1;
                        backLeftCornerX = shape.CurrentXOffset + shape.TBlock.XIndex - 2;
                        backLeftCornerY = shape.CurrentYOffset + shape.TBlock.YIndex - 1;
                        backRightCornerX = shape.CurrentXOffset + shape.TBlock.XIndex - 2;
                        backRightCornerY = shape.CurrentYOffset + shape.TBlock.YIndex + 1;
                        isFrontLeftCornerAWall = frontLeftCornerY >= HEIGHT;
                        isFrontRightCornerAWall = frontRightCornerY < 0;
                        isBackLeftCornerAWall = backLeftCornerX < 0 || backLeftCornerX >= WIDTH;
                        isBackRightCornerAWall = backRightCornerX < 0 || backLeftCornerX >= WIDTH;
                        break;
                }



                Block frontCornerLeft = isFrontLeftCornerAWall ? null : this.BlockMatrix[frontLeftCornerX, frontLeftCornerY].Block;
                Block frontCornerRight = isFrontRightCornerAWall ? null : this.BlockMatrix[frontRightCornerX, frontRightCornerY].Block;
                Block backCornerLeft = isBackLeftCornerAWall ? null : this.BlockMatrix[backLeftCornerX, backLeftCornerY].Block;
                Block backCornerRight = isBackRightCornerAWall ? null : this.BlockMatrix[backRightCornerX, backRightCornerY].Block;

                //TSpin: both fron corners of the 3x3 T occupied and at least 1 back corner occupied
                bool isTSpin = (frontCornerLeft != null || isFrontLeftCornerAWall)
                    && (frontCornerRight != null || isFrontRightCornerAWall)
                    && ((backCornerLeft != null || isBackLeftCornerAWall) || (backCornerRight != null || isBackRightCornerAWall));

                if (isTSpin)
                    return 2;

                //Mini TSpin: both back corners of 3x3 T occupied but only one of the front corners occupied
                bool isMiniTSpin = ((frontCornerLeft != null || isFrontLeftCornerAWall) && !(frontCornerRight != null || isFrontRightCornerAWall))
                                || !((frontCornerLeft != null || isFrontLeftCornerAWall) && (frontCornerRight != null || isFrontRightCornerAWall))
                                 && ((backCornerLeft != null || isBackLeftCornerAWall) && (backCornerRight != null || isBackRightCornerAWall));

                if (isMiniTSpin)
                    return 1;
            }
            return 0;
        }

        private void MoveLeft()
        {
            _activeShape.LastMove = MovementType.SHIFT;
            //clear existing nodes that have the active shapes blocks
            ClearActiveShapeFromGrid();
            //populate new nodes by new xOffset
            ResetShapeHorizontalLocationOnGrid(Direction.LEFT);
            //set new xOffset
            _activeShape.CurrentXOffset--;
        }

        private void MoveRight()
        {
            _activeShape.LastMove = MovementType.SHIFT;
            //clear existing nodes that have the active shapes blocks
            ClearActiveShapeFromGrid();
            //populate new nodes by new xOffset
            ResetShapeHorizontalLocationOnGrid(Direction.RIGHT);
            //set new xOffset
            _activeShape.CurrentXOffset++;
        }

        private void ExecuteBlockFall()
        {
            //clear existing nodes that have the active shapes blocks
            ClearActiveShapeFromGrid();
            //populate new nodes by new yOffset
            ResetShapeVerticalLocationOnGrid();
            //set new YOffset
            _activeShape.CurrentYOffset++;
        }

        private void ApplyNewShapeToGrid(Shape newShape)
        {
            bool transferBlock = true;
            foreach (var block in newShape.Blocks)
            {
                int x = newShape.CurrentXOffset + block.XIndex;
                int y = newShape.CurrentYOffset + block.YIndex;
                if (this.BlockMatrix[x, y].Block != null)
                {
                    transferBlock = false;
                    break;
                }
            }
            if (transferBlock)
            {
                _activeShape = newShape;
                _activeShape.LastMove = MovementType.ROTATION;
            }

            int xLen = _activeShape.BlockMatrix.GetLength(0);
            int yLen = _activeShape.BlockMatrix.GetLength(1);

            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    var block = _activeShape.BlockMatrix[i, j];
                    if (block != null)
                    {
                        int x = _activeShape.CurrentXOffset + i;
                        int y = _activeShape.CurrentYOffset + j;
                        this.BlockMatrix[x, y].Block = block;
                    }
                }
            }
        }

        private void ResetShapeVerticalLocationOnGrid()
        {
            int xLen = _activeShape.BlockMatrix.GetLength(0);
            int yLen = _activeShape.BlockMatrix.GetLength(1);
            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    var block = _activeShape.BlockMatrix[i, j];
                    if (block != null)
                    {
                        int x = _activeShape.CurrentXOffset + i;
                        int y = j + _activeShape.CurrentYOffset + 1;
                        this.BlockMatrix[x, y].Block = block;
                    }
                }
            }
        }

        private void ResetShapeHorizontalLocationOnGrid(Direction direction)
        {
            int xLen = _activeShape.BlockMatrix.GetLength(0);
            int yLen = _activeShape.BlockMatrix.GetLength(1);
            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    var block = _activeShape.BlockMatrix[i, j];
                    if (block != null)
                    {
                        int x = direction == Direction.LEFT ? _activeShape.CurrentXOffset + i - 1 : _activeShape.CurrentXOffset + i + 1;
                        int y = j + _activeShape.CurrentYOffset;
                        this.BlockMatrix[x, y].Block = block;
                    }
                }
            }
        }

        private void ClearActiveShapeFromGrid()
        {
            int xLen = _activeShape.BlockMatrix.GetLength(0);
            int yLen = _activeShape.BlockMatrix.GetLength(1);
            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    var block = _activeShape.BlockMatrix[i, j];
                    if (block != null)
                    {
                        int x = _activeShape.CurrentXOffset + i;
                        int y = j + _activeShape.CurrentYOffset;
                        this.BlockMatrix[x, y].Block = null;
                    }
                }
            }
        }

        private bool CollidesWithBlocksBelow()
        {
            if (_activeShape != null)
            {
                foreach (var block in _activeShape.Blocks)
                {
                    int x = _activeShape.CurrentXOffset + block.XIndex;
                    int y = _activeShape.CurrentYOffset + block.YIndex + 1;
                    if (this.BlockMatrix[x, y].Block != null && !_activeShape.Blocks.Contains(this.BlockMatrix[x, y].Block))
                        return true;
                }
            }
            return false;
        }

        private bool CollidesWithBlocksHorizontally(Direction direction)
        {
            if (_activeShape != null)
            {
                foreach (var block in _activeShape.Blocks)
                {
                    int x = direction == Direction.LEFT ? _activeShape.CurrentXOffset + block.XIndex - 1 : _activeShape.CurrentXOffset + block.XIndex + 1;
                    int y = _activeShape.CurrentYOffset + block.YIndex;
                    if (this.BlockMatrix[x, y].Block != null && !_activeShape.Blocks.Contains(this.BlockMatrix[x, y].Block))
                        return true;
                }
            }
            return false;
        }

        private bool CollidesInArea(Shape shape)
        {
            if (_activeShape != null && shape != null)
            {
                foreach (var block in shape.Blocks)
                {
                    int x = shape.CurrentXOffset + block.XIndex;
                    int y = shape.CurrentYOffset + block.YIndex;

                    if (y < 0 || y >= HEIGHT)
                        return true;

                    if (x < 0)
                    {
                        shape.CurrentXOffset += Math.Abs(x);
                        x = shape.CurrentXOffset + block.XIndex;
                    }
                    else if (x >= WIDTH)
                    {
                        shape.CurrentXOffset -= (x - WIDTH + 1);
                        x = shape.CurrentXOffset + block.XIndex;
                    }

                    if (this.BlockMatrix[x, y].Block != null && !_activeShape.Blocks.Contains(this.BlockMatrix[x, y].Block))
                        return true;
                }
            }
            return false;
        }

        private void InitializeMatrix()
        {
            this.BlockMatrix = new TetrisGridNode[WIDTH, HEIGHT];
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    this.BlockMatrix[i, j] = new TetrisGridNode(i, j);
                }
            }
        }

        private List<int> GetMatchingRows(List<int> rowsToCheck)
        {
            List<int> matchingRows = new List<int>();
            foreach (int row in rowsToCheck)
            {
                int xLen = this.BlockMatrix.GetLength(0);
                bool matchingRow = true;
                for (int i = 0; i < xLen; i++)
                {
                    if (this.BlockMatrix[i, row].Block == null)
                    {
                        matchingRow = false;
                    }
                }
                if (matchingRow)
                {
                    matchingRows.Add(row);
                }
            }
            return matchingRows;
        }

        private void DestroyRows(List<int> matchingRows)
        {
            foreach (int row in matchingRows)
            {
                int xLen = this.BlockMatrix.GetLength(0);
                for (int i = 0; i < xLen; i++)
                {
                    var block = this.BlockMatrix[i, row].Block;
                    block.Break();
                    _breakingBlocks.Add(block);
                    block.Destroyed += Block_Destroyed;
                }
            }
            _rowsToFall = matchingRows.Count;
        }

        #endregion

        #region event handlers

        private void Block_Destroyed(object sender, BlockEventArgs e)
        {
            if (e.Block != null)
            {
                e.Block.Destroyed -= Block_Destroyed;
                _breakingBlocks.Remove(e.Block);
                if (!_breakingBlocks.Any())
                    DropStructure();
            }
        }

        protected virtual void OnShapeLanded()
        {
            ShapeEventArgs e = new ShapeEventArgs()
            {
                Shape = _activeShape
            };
            this.ShapeLanded?.Invoke(this, e);
        }

        protected virtual void OnGameOver()
        {
            this.GameOver?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnMatchAnimationStart()
        {
            this.MatchAnimationStart?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnMatchAnimationEnd()
        {
            this.MatchAnimationEnd?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnRowsMatched()
        {
            MatchEventArgs e = CreateMatchEventArgs();
            this.MatchedRows?.Invoke(this, e);
        }

        protected virtual void OnMatchFound()
        {
            MatchEventArgs e = CreateMatchEventArgs();
            this.MatchFound?.Invoke(this, e);
        }

        private MatchEventArgs CreateMatchEventArgs()
        {
            return new MatchEventArgs()
            {
                RowsDestroyed = _rowsToFall,
                ComboCount = _matchStreak - 1 < 0 ? 0 : _matchStreak - 1,
                TSpinMultiplier = _tSpinMultiplier

            };
        }

        protected virtual void OnForcedGravity()
        {
            this.ForcedGravity?.Invoke(this, EventArgs.Empty);
        }

        private void DropStructure()
        {
            int xLen = this.BlockMatrix.GetLength(0);
            int currentRow = _matchingRows.Max();
            bool done = false;
            int rowsToDrop = 0;
            List<Point> newPoints = new List<Point>();
            List<Block> storedBlocks = new List<Block>();

            //clear out previous structure and store modified indices/block in memory
            while (!done)
            {
                if (_matchingRows.Contains(currentRow))
                {
                    rowsToDrop++;
                    _matchingRows.Remove(currentRow);
                }
                else
                {
                    bool foundBlock = false;
                    for (int i = 0; i < xLen; i++)
                    {//calculate new points to fall for the current row based on
                        var block = this.BlockMatrix[i, currentRow].Block;
                        if (block != null)
                        {
                            foundBlock = true;
                            newPoints.Add(new Point(i, currentRow + rowsToDrop));
                            storedBlocks.Add(block);
                            this.BlockMatrix[i, currentRow].Block = null;
                        }
                    }
                    if (!_matchingRows.Any() && !foundBlock)
                    {
                        done = true;
                    }
                }
                currentRow--;
                if (currentRow < 0)
                    done = true;
            }
            for (int i = 0; i < newPoints.Count; i++)
            {
                int x = newPoints[i].X;
                int y = newPoints[i].Y;
                var block = storedBlocks[i];
                this.BlockMatrix[x, y].Block = block;
            }
            //fire Match Event
            OnRowsMatched();
            //reset variables
            _rowsToFall = 0;
            _matchingRows.Clear();
            //fire animation end
            OnMatchAnimationEnd();
        }


        #endregion
    }

    public class TetrisGridNode : PictureBox
    {
        private Block _block;

        public TetrisGridNode(int xIndex, int yIndex)
        {
            this.Size = new System.Drawing.Size(BlockUtilities.BLOCK_SIZE + 2, BlockUtilities.BLOCK_SIZE + 2);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.BackColor = Color.Black;
            this.XIndex = xIndex;
            this.YIndex = yIndex;
            int x = this.XIndex * this.Width + (this.XIndex);
            int y = this.YIndex * this.Height + (this.YIndex);
            this.Location = new Point(x, y);
        }

        public int XIndex
        {
            get; private set;
        }

        public int YIndex
        {
            get; private set;
        }

        public Block Block
        {
            get
            {
                return _block;
            }
            set
            {
                if (value == null && _block != null)
                {
                    _block.Destroyed -= _block_Destroyed;
                    _block.AnimationExecuted -= _block_AnimationExecuted;
                }
                _block = value;
                if (_block == null)
                {
                    this.Image = null;
                }
                else
                {
                    this.Image = _block.Image;
                    _block.Destroyed += _block_Destroyed;
                    _block.AnimationExecuted += _block_AnimationExecuted;
                }
            }
        }

        private void _block_AnimationExecuted(object sender, EventArgs e)
        {
            this.Image = _block.Image;
            this.BackColor = _block.BackColor;
        }

        private void _block_Destroyed(object sender, TetrisEventArgs.BlockEventArgs e)
        {
            this.Block = null;
            this.BackColor = Color.Black;
        }
    }
}
