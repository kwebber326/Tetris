using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris.UserControls
{
    public partial class MatchAnimationControl : UserControl
    {
        private Timer _animationTimer = new Timer();
        private const int ANIMATION_COUNT = 10;
        private int _currentAnimationCount = 0;
        private int _lines, _score, _combos, _tSpinType;
        private bool _visible;
        private Image _originalImage;
        public MatchAnimationControl()
        {
            InitializeComponent();
        }

        private void MatchAnimationControl_Load(object sender, EventArgs e)
        {
            Reset();
            _animationTimer.Interval = 100;
            _animationTimer.Tick += _animationTimer_Tick;
            _originalImage = pbTetris.Image;
        }

        private void _animationTimer_Tick(object sender, EventArgs e)
        {
            if (++_currentAnimationCount <= ANIMATION_COUNT)
            {
                _visible = !_visible;
                ToggleAnimationState(_visible, _lines, _score, _combos, _tSpinType);
            }
            else
            {
                Reset();
            }
        }

        public void RunAnimation(int lines, int score, int combos, int tSpinMultiplier)
        {
            //reset in case there was another animation running
            Reset();
            //decide which controls should show based on match data
            SetVisible(lines, score, combos, tSpinMultiplier);

            _animationTimer.Start();
        }

        private void SetVisible(int lines, int score, int combos, int tSpinMultiplier)
        {
            _visible = true;
            _lines = lines;
            _score = score;
            _tSpinType = tSpinMultiplier;
            pnlDescription.Visible = true;
            if (lines >= 4)
            {
                pbTetris.Visible = true;
                lblAdjective.Visible = false;
            }
            else
            {
                switch (lines)
                {
                    case 1:
                        lblAdjective.Text = "Single!";
                        break;
                    case 2:
                        lblAdjective.Text = "Double!!";
                        break;
                    case 3:
                        lblAdjective.Text = "Triple!!!";
                        break;
                }
            }
            lblScoreAdded.Text = $"+{score}";
            if (combos > 0)
            {
                lblCombo.Text = $"{combos}xCombo!";
            }

            if (tSpinMultiplier > 0)
            {
                lblTSpin.Text = tSpinMultiplier == 1 ? "Mini-TSpin!" : "TSpin!!";
            }
        }

        public void ToggleAnimationState(bool visible, int lines, int score, int combos, int tSpinMultiplier)
        {
            if (visible)
            {
                SetVisible(lines, score, combos, tSpinMultiplier);
            }
            else
            {
                pnlDescription.Visible = false;
                pbTetris.Visible = false;
            }
        }

        private void Reset()
        {
            _animationTimer.Stop();
            _currentAnimationCount = 0;
            _lines = 0;
            _score = 0;
            _combos = 0;
            pnlDescription.Visible = false;
            lblAdjective.Visible = true;
            lblScoreAdded.Visible = true;
            pbTetris.Visible = false;
            lblAdjective.Text = string.Empty;
            lblScoreAdded.Text = string.Empty;
            lblCombo.Text = string.Empty;
            lblTSpin.Text = string.Empty;
        }
    }
}
