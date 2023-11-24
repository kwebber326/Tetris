using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris.Objects;
using Tetris.Objects.Utilities;
using Tetris.Objects.TetrisEventArgs;

namespace Tetris.UserControls
{
    public partial class TetrisBlockQueue : UserControl
    {
        private Queue<Shape> _shapeQueue = new Queue<Shape>();
        private const int SHAPES_PER_QUEUE = 3;
        private const int HORIZONTAL_OFFSET = 64;
        private const int VERTICAL_OFFSET = 32;

        public event EventHandler<ShapeEventArgs> BlockDropped;
        public TetrisBlockQueue()
        {
            InitializeComponent();
        }

        private void TetrisBlockQueue_Load(object sender, EventArgs e)
        {
            SetBlockQueue();
            SetNextBlock();
        }

        private void SetNextBlock()
        {
            if (_shapeQueue.Any())
            {
                var shape = _shapeQueue.Peek();
                int x = HORIZONTAL_OFFSET;
                int y = VERTICAL_OFFSET;
                Point p = new Point(x, y);
                var clone = shape.Clone();
                Shape.SetShapeLocation(clone, p);
            }
        }

        private void AddNewBlockToQueue()
        {
            var shape = ShapeFactory.GenerateRandomShape();
            _shapeQueue.Enqueue(shape);
            var lastShape = _shapeQueue.LastOrDefault();
            if (lastShape != null)
            {
                int x = HORIZONTAL_OFFSET;
                int y = _shapeQueue.Count * (BlockUtilities.BLOCK_SIZE * VERTICAL_OFFSET);
                Point p = new Point(x, y);
                Shape.SetShapeLocation(shape, p);
                foreach (var block in shape.Blocks)
                {
                    pnlBlockQueue.Controls.Add(block);
                }
            }
        }

        public void SetBlockQueue()
        {
            if (_shapeQueue.Any())
                _shapeQueue.Clear();

            int x = HORIZONTAL_OFFSET, y = VERTICAL_OFFSET;
            for (int i = 0; i < SHAPES_PER_QUEUE; i++)
            {
                var shape = ShapeFactory.GenerateRandomShape();
                _shapeQueue.Enqueue(shape);
                Point p = new Point(x, y);
                Shape.SetShapeLocation(shape, p);
                y += (shape.BlockMatrix.GetLength(1) * BlockUtilities.BLOCK_SIZE + VERTICAL_OFFSET);
                foreach (var block in shape.Blocks)
                {
                    pnlBlockQueue.Controls.Add(block);
                }
            }
        }

        public void ResetBlockQueue()
        {
            int x = HORIZONTAL_OFFSET, y = VERTICAL_OFFSET;
            foreach (var shape in _shapeQueue)
            {
                Point p = new Point(x, y);
                Shape.SetShapeLocation(shape, p);
                y += (shape.BlockMatrix.GetLength(1) * BlockUtilities.BLOCK_SIZE + VERTICAL_OFFSET);
            }
        }

        public void DropNextBlock()
        {
            var shape = _shapeQueue.Dequeue();
            foreach (var block in shape.Blocks)
            {
                pnlBlockQueue.Controls.Remove(block);
            }
            SetNextBlock();
            AddNewBlockToQueue();
            ResetBlockQueue();
            OnBlockDropped(shape);  
        }

        protected void OnBlockDropped(Shape shape)
        {
            ShapeEventArgs shapeEventArgs = new ShapeEventArgs
            {
                Shape = shape
            };
            BlockDropped?.Invoke(this, shapeEventArgs);
        }
    }
}
