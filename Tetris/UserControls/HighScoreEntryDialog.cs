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
    public partial class HighScoreEntryDialog : Form
    {
        private int _score;
        private DateTime _date;
        public HighScoreEntryDialog(int score, DateTime date)
        {
            _score = score;
            _date = date;
            InitializeComponent();
        }

        private void TxtInitials_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInitials.Text))
            {
                if (txtInitials.Text.Length > 3)
                {
                    txtInitials.Text = txtInitials.Text.Substring(0, 3).ToUpper();
                }
                else
                {
                    txtInitials.Text = txtInitials.Text.ToUpper();
                }
            }
            
        }

        private void BtnDone_Click(object sender, EventArgs e)
        {
            ExitDialog();
        }

        private void ExitDialog()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public int Score
        {
            get
            {
                return _score;
            }
        }

        public DateTime Date
        {
            get
            {
                return _date;
            }
        }

        public string Initials
        {
            get
            {
                return txtInitials.Text;
            }
        }

        private void HighScoreEntryDialog_Load(object sender, EventArgs e)
        {

        }

        private void HighScoreEntryDialog_KeyUp(object sender, KeyEventArgs e)
        {
            if  (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(txtInitials.Text))
            {
                ExitDialog();
            }
        }
    }
}
