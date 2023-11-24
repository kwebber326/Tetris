using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Objects.TetrisEventArgs
{
    public class ShapeEventArgs : EventArgs
    {
        public Shape Shape { get; set; }
    }
}

