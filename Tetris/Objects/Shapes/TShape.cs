using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Objects.Enums;

namespace Tetris.Objects
{
    public class TShape : Shape
    {
        private Direction _direction;

        public static TShape Generate(Direction direction, int currentXOffset, int currentYOffset)
        {
            Block[,] blockMatrix = null;
            Color tShapeColor = Color.Purple;
            switch (direction)
            {
                case Direction.DOWN:
                    blockMatrix = new Block[3, 2];
                    blockMatrix[0, 0] = new Block(tShapeColor, 0, 0);
                    blockMatrix[1, 0] = new Block(tShapeColor, 1, 0);
                    blockMatrix[2, 0] = new Block(tShapeColor, 2, 0);
                    blockMatrix[1, 1] = new Block(tShapeColor, 1, 1);

                    if (currentXOffset > 0)
                        currentXOffset--;
                    if (currentYOffset + 2 < TetrisGrid.HEIGHT)
                        currentYOffset++;
                    break;
                case Direction.UP:
                    blockMatrix = new Block[3, 2];
                    blockMatrix[0, 1] = new Block(tShapeColor, 0, 1);
                    blockMatrix[1, 1] = new Block(tShapeColor, 1, 1);
                    blockMatrix[2, 1] = new Block(tShapeColor, 2, 1);
                    blockMatrix[1, 0] = new Block(tShapeColor, 1, 0);

                    break;
                case Direction.LEFT:
                    blockMatrix = new Block[2, 3];
                    blockMatrix[1, 0] = new Block(tShapeColor, 1, 0);
                    blockMatrix[1, 1] = new Block(tShapeColor, 1, 1);
                    blockMatrix[1, 2] = new Block(tShapeColor, 1, 2);
                    blockMatrix[0, 1] = new Block(tShapeColor, 0, 1);

                    break;
                case Direction.RIGHT:
                    blockMatrix = new Block[2, 3];
                    blockMatrix[0, 0] = new Block(tShapeColor, 0, 0);
                    blockMatrix[0, 1] = new Block(tShapeColor, 0, 1);
                    blockMatrix[0, 2] = new Block(tShapeColor, 0, 2);
                    blockMatrix[1, 1] = new Block(tShapeColor, 1, 1);

                    currentXOffset++;
                    break;
            }
            var shape = new TShape(blockMatrix, direction);
            shape.CurrentXOffset = currentXOffset;
            shape.CurrentYOffset = currentYOffset;
            return shape;
        }

        public Block TBlock { get; private set; }

        private TShape(Block[,] blocks, Direction direction) : base(blocks, Color.Purple)
        {
            _direction = direction;
            SetTBlock();
        }

        private void SetTBlock()
        {
            switch (_direction)
            {
                case Direction.DOWN:
                    this.TBlock = this.BlockMatrix[1, 1];
                    break;
                case Direction.UP:
                    this.TBlock = this.BlockMatrix[1, 0];
                    break;
                case Direction.LEFT:
                    this.TBlock = this.BlockMatrix[0, 1];
                    break;
                case Direction.RIGHT:
                    this.TBlock = this.BlockMatrix[1, 1];
                    break;
            }
        }

        public Direction Direction
        {
            get
            {
                return _direction;
            }
        }

        public TShape RotateClockwise()
        {
            switch (_direction)
            {
                case Direction.DOWN:
                    return TShape.Generate(Direction.LEFT, this.CurrentXOffset, this.CurrentYOffset);
                case Direction.LEFT:
                    return TShape.Generate(Direction.UP, this.CurrentXOffset, this.CurrentYOffset);
                case Direction.UP:
                    return TShape.Generate(Direction.RIGHT, this.CurrentXOffset, this.CurrentYOffset);
                case Direction.RIGHT:
                    return TShape.Generate(Direction.DOWN, this.CurrentXOffset, this.CurrentYOffset);
            }
            return null;
        }
    }
}
