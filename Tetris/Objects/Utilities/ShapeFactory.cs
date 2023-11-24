using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Objects.Enums;

namespace Tetris.Objects.Utilities
{
    public static class ShapeFactory
    {
        private static int _seed;
        private static int Seed
        {
            get
            {
                if (_seed == 0)
                {
                    Random random = new Random();
                    _seed = random.Next(int.MinValue, int.MaxValue);
                }
                return _seed;
            }
        }

        private static Random _random;
        private static Random Random
        {
            get
            {
                if (_random == null)
                {
                    _random = new Random(Seed);
                }
                return _random;
            }
        }

        public static Shape GenerateShape(TetrisShapeType type)
        {
            Shape shape = null;
            Color color;
            switch (type)
            {
                case TetrisShapeType.LEFT_L_BLOCK:
                    Block[,] blockMatrix = new Block[3, 2];
                    color = Color.Blue;
                    blockMatrix[0, 0] = new Block(color, 0, 0);
                    blockMatrix[0, 1] = new Block(color, 0, 1);
                    blockMatrix[1, 1] = new Block(color, 1, 1);
                    blockMatrix[2, 1] = new Block(color, 2, 1);
                    shape = new Shape(blockMatrix, color);
                    break;
                case TetrisShapeType.LEFT_S_BLOCK:
                    blockMatrix = new Block[3, 2];
                    color = Color.Red;
                    blockMatrix[0, 0] = new Block(color, 0, 0);
                    blockMatrix[1, 0] = new Block(color, 1, 0);
                    blockMatrix[1, 1] = new Block(color, 1, 1);
                    blockMatrix[2, 1] = new Block(color, 2, 1);
                    shape = new Shape(blockMatrix, color);
                    break;
                case TetrisShapeType.RIGHT_L_BLOCK:
                    blockMatrix = new Block[3, 2];
                    color = Color.Orange;
                    blockMatrix[0, 1] = new Block(color, 0, 1);
                    blockMatrix[1, 1] = new Block(color, 1, 1);
                    blockMatrix[2, 1] = new Block(color, 2, 1);
                    blockMatrix[2, 0] = new Block(color, 2, 0);
                    shape = new Shape(blockMatrix, color);
                    break;
                case TetrisShapeType.RIGHT_S_BLOCK:
                    blockMatrix = new Block[3, 2];
                    color = Color.Green;
                    blockMatrix[0, 1] = new Block(color, 0, 1);
                    blockMatrix[1, 1] = new Block(color, 1, 1);
                    blockMatrix[1, 0] = new Block(color, 1, 0);
                    blockMatrix[2, 0] = new Block(color, 2, 0);
                    shape = new Shape(blockMatrix, color);
                    break;
                case TetrisShapeType.SQUARE_BLOCK:
                    blockMatrix = new Block[2, 2];
                    color = Color.Yellow;
                    blockMatrix[0, 0] = new Block(color, 0, 0);
                    blockMatrix[0, 1] = new Block(color, 0, 1);
                    blockMatrix[1, 0] = new Block(color, 1, 0);
                    blockMatrix[1, 1] = new Block(color, 1, 1);
                    shape = new Shape(blockMatrix, color);
                    break;
                case TetrisShapeType.STICK_BLOCK:
                    blockMatrix = new Block[4, 1];
                    color = Color.LightBlue;
                    blockMatrix[0, 0] = new Block(color, 0, 0);
                    blockMatrix[1, 0] = new Block(color, 1, 0);
                    blockMatrix[2, 0] = new Block(color, 2, 0);
                    blockMatrix[3, 0] = new Block(color, 3, 0);
                    shape = new Shape(blockMatrix, color);
                    break;
                case TetrisShapeType.T_BLOCK:
                    shape = TShape.Generate(Direction.UP, 0, 0);
                    break;
            }
            return shape;
        }

        public static Shape GenerateRandomShape()
        {
            int randomShape = Random.Next(1, 8);
            TetrisShapeType tetrisShapeType = (TetrisShapeType)randomShape;
            return GenerateShape(tetrisShapeType);
        }
    }
}
