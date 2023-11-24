using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Objects.TetrisEventArgs
{
    public class GameUpdateEventArgs : EventArgs
    {
        public TetrisGameLogic GameLogic { get; set; }
    }
}
