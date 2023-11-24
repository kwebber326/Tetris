using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris.Objects;
using Tetris.Objects.Utilities;
using Tetris.UserControls;

namespace Tetris
{
    public partial class TetrisBoard : Form
    {
        public TetrisBoard()
        {
            InitializeComponent();
        }

        private bool _isGameOver = false;
        private bool _isGameOverAnimationDone = false;
        private Timer _updateTimer = new Timer();
        private Timer _gameOverAnimationTimer = new Timer();
        private bool _isKeyDown;
        private Keys _selectedKeys;

        private int _score;
        private int _level = 1;
        private bool _isPaused;
        private int _lines;

        private void TetrisBoard_Load(object sender, EventArgs e)
        {
            _blockQueue.BlockDropped += _blockQueue_BlockDropped;
            _blockQueue.DropNextBlock();
            _tetrisBoard.Grid.ShapeLanded += Grid_ShapeLanded;
            _tetrisBoard.Grid.GameOver += Grid_GameOver;
            _tetrisBoard.Grid.MatchAnimationEnd += Grid_MatchAnimationEnd;
            _tetrisBoard.Grid.MatchFound += Grid_MatchFound;
            _tetrisBoard.GameUpdated += _tetrisBoard_GameUpdated;

            _updateTimer.Interval = 100;
            _updateTimer.Tick += _updateTimer_Tick;
            _updateTimer.Start();

            _gameOverAnimationTimer.Interval = 2000;
            _gameOverAnimationTimer.Tick += _gameOverAnimationTimer_Tick;

            SetGameStatusText();
        }

        private void Grid_MatchFound(object sender, Objects.TetrisEventArgs.MatchEventArgs e)
        {
            int lines = e.RowsDestroyed;
            int baseScore = 0;
            switch (lines)
            {
                case 1:
                    baseScore = TetrisGameLogic.BASE_SINGLE_MATCH;
                    break;
                case 2:
                    baseScore = TetrisGameLogic.BASE_DOUBLE_MATCH;
                    break;
                case 3:
                    baseScore = TetrisGameLogic.BASE_TRIPLE_MATCH;
                    break;
                case 4:
                    baseScore = TetrisGameLogic.BASE_TETRIS_MATCH;
                    break;

            }



            int scoreAdded = ((_level) * baseScore) + (_level * TetrisGameLogic.BASE_COMBO_SCORE * e.ComboCount) + TetrisGameLogic.GetTSpinScore(lines, e.TSpinMultiplier, _level);

            if (lines == 0 && scoreAdded > 0)
            {
                _score += scoreAdded;
                _tetrisBoard.IncrementGameLogicScore(scoreAdded);
                SetGameStatusText();
            }

            matchAnimationControl1.RunAnimation(lines, scoreAdded, e.ComboCount, e.TSpinMultiplier);
        }

        private void _gameOverAnimationTimer_Tick(object sender, EventArgs e)
        {
            _isGameOverAnimationDone = true;
            _gameOverAnimationTimer.Stop();
        }

        private void _tetrisBoard_GameUpdated(object sender, Objects.TetrisEventArgs.GameUpdateEventArgs e)
        {
            _score = e.GameLogic.Score;
            if (_level != e.GameLogic.Level)
                _tetrisBoard.UpdateGravitySpeed();

            _level = e.GameLogic.Level;
            _lines = e.GameLogic.MatchCount;
            SetGameStatusText();
        }

        private void SetGameStatusText()
        {
            lblScore.Text = $"Score: {_score}";
            lblLevel.Text = $"Level: {_level}";
            lblLines.Text = $"Lines: {_lines}";
        }

        private void Grid_MatchedRows(object sender, Objects.TetrisEventArgs.MatchEventArgs e)
        {

        }

        public void Pause()
        {
            _isPaused = true;
            lblPaused.Visible = _isPaused;
            _tetrisBoard.SetPausedState(_isPaused);
        }

        public void UnPause()
        {
            _isPaused = false;
            lblPaused.Visible = _isPaused;
            _tetrisBoard.SetPausedState(_isPaused);
        }

        private void Grid_MatchAnimationEnd(object sender, EventArgs e)
        {
            _blockQueue.DropNextBlock();
        }

        private void _updateTimer_Tick(object sender, EventArgs e)
        {
            if (_isKeyDown && !_isPaused)
            {
                switch (_selectedKeys)
                {
                    case Keys.Left:
                        _tetrisBoard.Grid.MoveShape(Objects.Enums.Direction.LEFT);
                        break;
                    case Keys.Right:
                        _tetrisBoard.Grid.MoveShape(Objects.Enums.Direction.RIGHT);
                        break;
                    case Keys.Down:
                        _tetrisBoard.Grid.MoveShape(Objects.Enums.Direction.DOWN);
                        break;

                }
            }
        }

        private void Grid_GameOver(object sender, EventArgs e)
        {
            _isGameOver = true;
            pbGameOver.Visible = _isGameOver;
            _updateTimer.Stop();
            _gameOverAnimationTimer.Start();
        }

        private void Grid_ShapeLanded(object sender, Objects.TetrisEventArgs.ShapeEventArgs e)
        {
            _blockQueue.DropNextBlock();
        }

        private void _blockQueue_BlockDropped(object sender, Objects.TetrisEventArgs.ShapeEventArgs e)
        {
            _tetrisBoard.Grid.InsertShape(e.Shape);
        }

        protected override bool IsInputKey(Keys keyData)
        {
            return base.IsInputKey(keyData);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (_isGameOverAnimationDone)
            {
                var highScores = FileIOUtilities.ReadScoreList();
                int minScore = highScores.Any() ? highScores.Select(s => s.Score).Min() : 0;
                if (_score >= minScore)
                {
                    var date = DateTime.Now;
                    HighScoreEntryDialog highScoreEntryDialog = new HighScoreEntryDialog(_score, date);
                    if (highScoreEntryDialog.ShowDialog() == DialogResult.OK)
                    {
                        HighScore score = new HighScore()
                        {
                            Score = _score,
                            Date = date,
                            Initials = highScoreEntryDialog.Initials
                        };
                        FileIOUtilities.WriteScore(score);
                        this.DialogResult = DialogResult.OK;
                    }
                }
                this.Close();
            }

            switch (keyData)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    KeyEventArgs args = new KeyEventArgs(keyData);
                    base.OnKeyDown(args);
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void TetrisBoard_KeyDown(object sender, KeyEventArgs e)
        {
            _isKeyDown = true;
            _selectedKeys = e.KeyCode;
        }

        private void TetrisBoard_KeyUp(object sender, KeyEventArgs e)
        {
            _isKeyDown = false;
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (!_isPaused)
                        _tetrisBoard.Grid.RotateShape();
                    break;
                case Keys.Escape:
                case Keys.Enter:
                    if (!_isGameOver)
                    {
                        if (_isPaused)
                        {
                            UnPause();
                        }
                        else
                        {
                            Pause();
                        }
                    }
                    else if (_isPaused)
                    {
                        UnPause();
                    }
                    break;
            }
        }
    }
}
