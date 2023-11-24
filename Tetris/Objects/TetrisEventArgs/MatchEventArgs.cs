using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Objects.TetrisEventArgs
{
    public class MatchEventArgs : EventArgs
    {
        public int RowsDestroyed { get; set; }

        public int ComboCount { get; set; }

        public int TSpinMultiplier { get; set; }
    }
}
