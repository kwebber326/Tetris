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
    public partial class TetrisBoardMatrix : UserControl
    {
        private TetrisGrid _grid = new TetrisGrid();
        private TetrisGameLogic _gameLogic = new TetrisGameLogic();

        private Timer _gravityTimer = new Timer();

        public event EventHandler<GameUpdateEventArgs> GameUpdated;
        public bool _isPaused;

        public TetrisBoardMatrix()
        {
            InitializeComponent();
        }

        private void TetrisBoard_Load(object sender, EventArgs e)
        {
            InitializeGrid();

            _gravityTimer.Interval = _gameLogic.MovementInterval;
            _gravityTimer.Tick += _gravityTimer_Tick;
            _gravityTimer.Start();

            _grid.GameOver += _grid_GameOver;
            _grid.MatchAnimationStart += _grid_MatchAnimationStart;
            _grid.MatchAnimationEnd += _grid_MatchAnimationEnd;
            _grid.MatchedRows += _grid_MatchedRows;
            _grid.ForcedGravity += _grid_ForcedGravity;

            _gameLogic.GameChanged += _gameLogic_GameChanged;
        }

        private void _grid_ForcedGravity(object sender, EventArgs e)
        {
            _gameLogic.IncrementScore(1);
        }

        private void _gameLogic_GameChanged(object sender, EventArgs e)
        {
            var gameLogic = sender as TetrisGameLogic;
            if (gameLogic != null)
            {
                GameUpdated?.Invoke(this, new GameUpdateEventArgs() { GameLogic = gameLogic });
            }
        }

        public void SetPausedState(bool paused)
        {
            _isPaused = paused;
        }

        public void IncrementGameLogicScore(int amount)
        {
            _gameLogic.IncrementScore(amount);
        }

        private void _grid_MatchedRows(object sender, Objects.TetrisEventArgs.MatchEventArgs e)
        {
            _gameLogic.ScoreMatch(e.RowsDestroyed, e.ComboCount, e.TSpinMultiplier);
        }

        private void _grid_MatchAnimationEnd(object sender, EventArgs e)
        {
            _gravityTimer.Start();
        }

        private void _grid_MatchAnimationStart(object sender, EventArgs e)
        {
            _gravityTimer.Stop();
        }

        private void _grid_GameOver(object sender, EventArgs e)
        {
            _gravityTimer.Stop();
        }

        private void _gravityTimer_Tick(object sender, EventArgs e)
        {
            if (!_isPaused)
            {
                _grid.ExecuteBlockGravity(false);
            }
        }

        public void UpdateGravitySpeed()
        {
            _gravityTimer.Interval = _gameLogic.MovementInterval;
            if (_gravityTimer.Enabled)
            {
                _gravityTimer.Stop();
                _gravityTimer.Start();
            }
        }

        private void InitializeGrid()
        {
            var xLen = _grid.BlockMatrix.GetLength(0);
            var yLen = _grid.BlockMatrix.GetLength(1);
            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    this.Controls.Add(_grid.BlockMatrix[i, j]);
                }
            }
        }

        public TetrisGrid Grid
        {
            get
            {
                return _grid;
            }
        }
    }
}
