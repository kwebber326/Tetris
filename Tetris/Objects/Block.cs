using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris.Objects.TetrisEventArgs;
using Tetris.Objects.Utilities;

namespace Tetris.Objects
{
    public class Block : PictureBox
    {
        private bool _breakAnimation;
        private Image _originalImage;
        private int _xIndex;
        private int _yIndex;
   
        private bool _isBreaking;
        private Timer _breakTimer;
        private int _currentBlockBreakAnimationDelayTick;
        private int _currentBlockBreakAnimationCount;
        private bool _isActive;

        public event EventHandler<BlockEventArgs> Destroyed;
        public event EventHandler AnimationExecuted;

        public Block(Color color, int xIndex, int yIndex)
        {
            _xIndex = xIndex;
            _yIndex = yIndex;
            this.Color = color;
            if (!BlockUtilities.ImageMapping.TryGetValue(color, out Image img))
            {
                throw new ArgumentException("The provided color is not a valid type");
            }
            else
            {
                _originalImage = img;
                this.Size = new Size(BlockUtilities.BLOCK_SIZE, BlockUtilities.BLOCK_SIZE);
                this.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Image = img;
            }

            _breakTimer = new Timer();
            _breakTimer.Interval = BlockUtilities.BLOCK_BREAK_ANIMATION_INTERVAL_MILLISECONDS;
            _breakTimer.Tick += _breakTimer_Tick;
            _isActive = true;
        }

        private void _breakTimer_Tick(object sender, EventArgs e)
        {
            if (_currentBlockBreakAnimationDelayTick++ >= BlockUtilities.BLOCK_BREAK_ANIMATION_DELAY)
            {
                _currentBlockBreakAnimationDelayTick = 0;
                this.BreakAnimation = !this.BreakAnimation;//this executes the change in visuals for the animation
                OnAnimationExecuted();
                if (++_currentBlockBreakAnimationCount >= BlockUtilities.BLOCK_BREAK_ANIMATIONS)
                {
                    this.IsBreaking = false;
                    _breakTimer.Stop();
                    OnDestroy();
                }
            }
        }

        public void Break()
        {
            this.IsBreaking = true;
        }

        public void Deactivate()
        {
            _isActive = false;
        }

        public Color Color
        {
            get; private set;
        }

        public bool IsBreaking
        {
            get
            {
                return _isBreaking;
            }
            set
            {
                _isBreaking = value;
                if (!_breakTimer.Enabled && _isBreaking)
                {
                    _breakTimer.Start();
                }
            }
        }

        public int XIndex
        {
            get
            {
                return _xIndex;
            }
        }

        public int YIndex
        {
            get
            {
                return _yIndex;
            }
        }

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
        }

        private bool BreakAnimation
        {
            get
            {
                return _breakAnimation;
            }
            set
            {
                _breakAnimation = value;
                if (_breakAnimation)
                {
                    this.Image = null;
                    this.BackColor = Color.Silver;
                }
                else
                {
                    this.Image = _originalImage;
                    this.BackColor = Color.Transparent;
                }
            }
        }

       

        protected void OnDestroy()
        {
            this.Destroyed?.Invoke(this, new BlockEventArgs() { Block = this });
        }

        protected void OnAnimationExecuted()
        {
            this.AnimationExecuted?.Invoke(this, EventArgs.Empty);
        }
    }
}
