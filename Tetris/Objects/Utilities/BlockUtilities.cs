using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Objects.Utilities
{
    public static class BlockUtilities
    {
        private static Dictionary<Color, Image> _imageDict;

        public static Dictionary<Color, Image> ImageMapping
        {
            get
            {
                if (_imageDict == null)
                {
                    _imageDict = new Dictionary<Color, Image>()
                    {
                        { Color.LightBlue, Properties.Resources.block_light_blue },
                        { Color.Blue, Properties.Resources.block_blue },
                        { Color.Red, Properties.Resources.block_red },
                        { Color.Orange, Properties.Resources.block_orange },
                        { Color.Green, Properties.Resources.block_green },
                        { Color.Purple, Properties.Resources.block_purple },
                        { Color.Yellow, Properties.Resources.block_yellow }
                    };
                }
                return _imageDict;
            }
        }

        public const int BLOCK_BREAK_ANIMATIONS = 8;
        public const int BLOCK_BREAK_ANIMATION_DELAY = 2;
        public const int BLOCK_BREAK_ANIMATION_INTERVAL_MILLISECONDS = 10;
        public const int BLOCK_SIZE = 42;
    }
}
