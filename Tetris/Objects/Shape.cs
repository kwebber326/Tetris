using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Objects.Enums;
using Tetris.Objects.Utilities;

namespace Tetris.Objects
{
    public class Shape
    {
        private Color _color;
        
        public Shape(Block[,] blocks, Color color)
        {
            _color = color;
            this.BlockMatrix = blocks;
            Initialize();
        }

        private void Initialize()
        {
            this.Blocks = new List<Block>();
            for (int i = 0; i < this.BlockMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.BlockMatrix.GetLength(1); j++)
                {
                    if (this.BlockMatrix[i, j] != null)
                    {
                        this.Blocks.Add(this.BlockMatrix[i, j]);
                    }
                }
            }
            this.LastMove = MovementType.FALL;
        }

        public virtual Block[,] BlockMatrix { get; private set; }

        public Color Color
        {
            get
            {
                return _color;
            }
        }

        public int CurrentXOffset
        {
            get;set;
        }

        public int CurrentYOffset
        {
            get;set;
        }

        public int IndexBelow
        {
            get
            {
                return CurrentYOffset + this.BlockMatrix.GetLength(1);
            }
        }

        public int IndexAbove
        {
            get
            {
                return CurrentYOffset - 1;
            }
        }

        public int IndexLeft
        {
            get
            {
                return CurrentXOffset - 1;
            }
        }

        public int IndexRight
        {
            get
            {
                return CurrentXOffset + this.BlockMatrix.GetLength(0);
            }
        }

        public MovementType LastMove
        {
            get;set;
        } 

        public virtual List<Block> Blocks { get; private set; }

        private static Block GetCenterBlockToRotateAround(Shape shapeToRotate)
        {
            var blockMatrix = shapeToRotate.BlockMatrix;

            int maxXLength = blockMatrix.GetLength(0);
            int maxYLength = blockMatrix.GetLength(1);

            int centerX = maxXLength % 2 == 0 || maxXLength == 1 ? maxXLength / 2 : maxXLength / 2 + 1;
            int centerY = maxYLength % 2 == 0 || maxYLength == 1 ? maxYLength / 2 : maxYLength / 2 + 1;
            int itemsAvailable = maxYLength * maxXLength;
            int itemsChecked = 0;

            int upIndex = centerY;
            int downIndex = centerY;
            int leftIndex = centerX;
            int rightIndex = centerX;

            HashSet<Point> pointsChecked = new HashSet<Point>();

            while (itemsChecked < itemsAvailable)
            {
                if (upIndex >= 0 && blockMatrix[centerX, upIndex] != null)
                {
                    return blockMatrix[centerX, upIndex];
                }
                else if (upIndex >= 0)
                {
                    Point p = new Point(centerX, upIndex);
                    pointsChecked.Add(p);
                    itemsChecked = pointsChecked.Count;
                    if (upIndex > 0)
                        upIndex--;
                }

                if (downIndex < maxYLength && blockMatrix[centerX, downIndex] != null)
                {
                    return blockMatrix[centerX, downIndex];
                }
                else if (downIndex < maxXLength)
                {
                    Point p = new Point(centerX, downIndex);
                    pointsChecked.Add(p);
                    itemsChecked = pointsChecked.Count;
                    downIndex++;
                }

                if (leftIndex >= 0 && blockMatrix[leftIndex, centerY] != null)
                {
                    return blockMatrix[leftIndex, centerY];
                }
                else if (leftIndex >= 0)
                {
                    Point p = new Point(leftIndex, centerY);
                    pointsChecked.Add(p);
                    itemsChecked = pointsChecked.Count;
                    if (leftIndex > 0)
                        leftIndex--;
                }

                if (rightIndex < maxXLength && blockMatrix[rightIndex, centerY] != null)
                {
                    return blockMatrix[rightIndex, centerY];
                }
                else if (rightIndex < maxXLength)
                {
                    Point p = new Point(rightIndex, centerY);
                    pointsChecked.Add(p);
                    itemsChecked = pointsChecked.Count;
                    rightIndex++;
                }

                //check diagonals
                if (rightIndex < maxXLength && upIndex >= 0 && blockMatrix[rightIndex, upIndex] != null)
                {
                    return blockMatrix[rightIndex, upIndex];
                }
                else if (rightIndex < maxXLength && upIndex >= 0)
                {
                    Point p = new Point(rightIndex, upIndex);
                    pointsChecked.Add(p);
                    itemsChecked = pointsChecked.Count;
                }

                if (leftIndex >= 0 && upIndex >= 0 && blockMatrix[leftIndex, upIndex] != null)
                {
                    return blockMatrix[leftIndex, upIndex];
                }
                else if (leftIndex >= 0 && upIndex >= 0)
                {
                    Point p = new Point(leftIndex, upIndex);
                    pointsChecked.Add(p);
                    itemsChecked = pointsChecked.Count;
                }

                if (rightIndex < maxXLength && downIndex < maxYLength && blockMatrix[rightIndex, downIndex] != null)
                {
                    return blockMatrix[rightIndex, downIndex];
                }
                else if (rightIndex < maxXLength && downIndex < maxYLength)
                {
                    Point p = new Point(rightIndex, downIndex);
                    pointsChecked.Add(p);
                    itemsChecked = pointsChecked.Count;
                }

                if (leftIndex >= 0 && downIndex < maxYLength && blockMatrix[leftIndex, downIndex] != null)
                {
                    return blockMatrix[leftIndex, downIndex];
                }
                else if (leftIndex >= 0 && downIndex < maxYLength)
                {
                    Point p = new Point(leftIndex, downIndex);
                    pointsChecked.Add(p);
                    itemsChecked = pointsChecked.Count;
                }
            }

            return null;
        }

        public static Shape RotateClockwise(Shape shapeToRotate)
        {
            if (shapeToRotate is TShape)
            {
                var tShape = (TShape)shapeToRotate;
                return tShape.RotateClockwise();
            }

            Block centerBlock = GetCenterBlockToRotateAround(shapeToRotate);
            if (centerBlock != null)
            {
                List<Point> newIndices = new List<Point>() { new Point(centerBlock.XIndex, centerBlock.YIndex) };
                var oldMatrix = shapeToRotate.BlockMatrix;
                int oldXLength = oldMatrix.GetLength(0);
                int oldYLength = oldMatrix.GetLength(1);
                for (int i = 0; i < oldXLength; i++)
                {
                    for (int j = 0; j < oldYLength; j++)
                    {
                        var block = oldMatrix[i, j];
                        if (block != null && block != centerBlock)
                        {

                            //get x and y differences from center
                            int xDiff = block.XIndex - centerBlock.XIndex;
                            int yDiff = block.YIndex - centerBlock.YIndex;
                            int newX = centerBlock.XIndex;
                            int newY = centerBlock.YIndex;
                            //horizontal diff only 
                            if (xDiff != 0 && yDiff == 0)
                            {
                                newY = centerBlock.YIndex + xDiff;
                            }//vertical diff only
                            else if (xDiff == 0 && yDiff != 0)
                            {
                                newX = centerBlock.XIndex - yDiff;
                            }
                            //diagonal down left
                            else if (xDiff < 0 && yDiff > 0)
                            {
                                newX = centerBlock.XIndex - yDiff;
                                newY = centerBlock.YIndex + xDiff;
                            }
                            //diagonal down right
                            else if (xDiff > 0 && yDiff > 0)
                            {
                                newY = centerBlock.YIndex + xDiff;
                                newX = centerBlock.XIndex - yDiff;
                            }
                            //diagonal up right
                            else if (xDiff > 0 && yDiff < 0)
                            {
                                newX = centerBlock.XIndex - yDiff;
                                newY = centerBlock.YIndex + xDiff;
                            }
                            //diagonal up left
                            else if (xDiff < 0 && yDiff < 0)
                            {
                                newX = centerBlock.XIndex - yDiff;
                                newY = centerBlock.YIndex + xDiff;
                            }
                            newIndices.Add(new Point(newX, newY));
                        }
                    }
                }
                //readjust the matrix indices to center after a rotation
                int minX = newIndices.Select(i => i.X).Min();
                int minY = newIndices.Select(i => i.Y).Min();

                List<Point> newMatrixIndices = newIndices.Select(p => new Point(p.X - minX, p.Y - minY)).ToList();

                int newXLength = newMatrixIndices.Select(i => i.X).Max() + 1;
                int newYLength = newMatrixIndices.Select(i => i.Y).Max() + 1;
                //initialize the new matrix
                Block[,] newMatrix = new Block[newXLength, newYLength];
                foreach (var index in newMatrixIndices)
                {
                    newMatrix[index.X, index.Y] = new Block(shapeToRotate.Color, index.X, index.Y);
                }

                //create the customBlock to return
                Shape customBlock = new Shape(newMatrix, shapeToRotate.Color);

                customBlock.CurrentXOffset = shapeToRotate.CurrentXOffset;
                customBlock.CurrentYOffset = shapeToRotate.CurrentYOffset;
                return customBlock;
            }

            return shapeToRotate;
        }

        public Shape Clone()
        {
            int xLen = this.BlockMatrix.GetLength(0);
            int yLen = this.BlockMatrix.GetLength(1);
            var blockMatrix = new Block[xLen, yLen];
            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    if (this.BlockMatrix[i, j] != null)
                    {
                        var block = this.BlockMatrix[i, j];
                        blockMatrix[i, j] = new Block(block.Color, block.XIndex, block.YIndex);
                    }
                }
            }
            var shape = new Shape(blockMatrix, this.Color);
            return shape;
        }

        public static void SetShapeLocation(Shape shape, Point startLocation)
        {
            if (shape == null)
                return;

            var blockMatrix = shape.BlockMatrix;
            int xLen = blockMatrix.GetLength(0);
            int yLen = blockMatrix.GetLength(1);
            int startX = startLocation.X;
            int startY = startLocation.Y;
            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    if (blockMatrix[i, j] != null)
                    {
                        int x = startX + (i * BlockUtilities.BLOCK_SIZE) + i, y = startY + (j * BlockUtilities.BLOCK_SIZE) + j;
                        blockMatrix[i, j].Location = new Point(x, y);
                    }
                }
            }
        }
    }
}
