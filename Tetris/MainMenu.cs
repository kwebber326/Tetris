using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            TetrisBoard game = new TetrisBoard();
            if (game.ShowDialog() == DialogResult.OK)
            {
                highScoreBoard1.ReloadHighScores();
            };

        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
