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
    public partial class HighScoreRow : UserControl
    {
        public HighScoreRow()
        {
            InitializeComponent();
        }

        private void HighScoreRow_Load(object sender, EventArgs e)
        {

        }

        public int Rank
        {
            get
            {
                return int.TryParse(lblRank.Text, out int result) ? result : -1;
            }
            set
            {
                lblRank.Text = value.ToString();
            }
        }

        public char Initial1
        {
            get
            {
                return !string.IsNullOrEmpty(lblInitial1.Text) ? lblInitial1.Text[0] : ' ';
            }
            set
            {
                lblInitial1.Text = value.ToString();
            }
        }
        public char Initial2
        {
            get
            {
                return !string.IsNullOrEmpty(lblInitial2.Text) ? lblInitial2.Text[0] : ' ';
            }
            set
            {
                lblInitial2.Text = value.ToString();
            }
        }

        public char Initial3
        {
            get
            {
                return !string.IsNullOrEmpty(lblInitial3.Text) ? lblInitial3.Text[0] : ' ';
            }
            set
            {
                lblInitial3.Text = value.ToString();
            }
        }

        public DateTime Date
        {
            get
            {
                return DateTime.TryParse(lblDate.Text, out DateTime result) ? result : new DateTime(1984, 6, 6);//default to the year tetris was invented
            }
            set
            {
                lblDate.Text = value.ToShortDateString();
            }
        }

        public int Score
        {
            get
            {
                return int.TryParse(lblScore.Text, out int result) ? result : 0;
            }
            set
            {
                lblScore.Text = value.ToString();
            }
        }
    }
}
