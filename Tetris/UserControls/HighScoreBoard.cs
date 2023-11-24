using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris.Objects.Utilities;
using Tetris.Objects;

namespace Tetris.UserControls
{
    public partial class HighScoreBoard : UserControl
    {
        private const int HORIZONTAL_OFFSET = 2;
        private const int VERTICAL_OFFSET = 2;
        public HighScoreBoard()
        {
            InitializeComponent();
        }

        private void HighScoreBoard_Load(object sender, EventArgs e)
        {
            InitializeHighScores();
        }

        public void ReloadHighScores()
        {
            pnlScores.Controls.Clear();
            InitializeHighScores();
        }

        private void InitializeHighScores()
        {
            var scores = FileIOUtilities.ReadScoreList();
            int scoreLength = scores.Count;
            int xPos = HORIZONTAL_OFFSET;
            int yPos = VERTICAL_OFFSET;
            for (int i = 0; i < FileIOUtilities.HIGH_SCORE_COUNT; i++)
            {

                HighScoreRow row;
                if (i >= scoreLength)
                {
                    row = new HighScoreRow()
                    {
                        Rank = i + 1,
                        Initial1 = 'A',
                        Initial2 = 'L',
                        Initial3 = 'P',
                        Date = new DateTime(1984, 6, 6),
                        Score = 0
                    };
                }
                else if (!string.IsNullOrEmpty(scores[i].Initials))
                {
                    string initial = scores[i].Initials.ToUpper();
                    row = new HighScoreRow()
                    {
                        Rank = i + 1,
                        Initial1 = initial[0],
                        Initial2 = initial.Length >= 2 ? initial[1] : ' ',
                        Initial3 = initial.Length >= 3 ? initial[2] : ' ',
                        Date = scores[i].Date,
                        Score = scores[i].Score
                    };
                }
                else
                {
                    string initial = scores[i].Initials;
                    row = new HighScoreRow()
                    {
                        Rank = i + 1,
                        Initial1 = ' ',
                        Initial2 = ' ',
                        Initial3 = ' ',
                        Date = scores[i].Date,
                        Score = scores[i].Score
                    };
                }
                if (row != null)
                {
                    row.Location = new Point(xPos, yPos);
                    pnlScores.Controls.Add(row);
                    yPos = (row.Bottom + VERTICAL_OFFSET);
                }

            }
        }
    }
}
