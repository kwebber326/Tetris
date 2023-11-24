namespace Tetris
{
    partial class TetrisBoard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblScore = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblPaused = new System.Windows.Forms.Label();
            this.pbGameOver = new System.Windows.Forms.PictureBox();
            this.lblLines = new System.Windows.Forms.Label();
            this.matchAnimationControl1 = new Tetris.UserControls.MatchAnimationControl();
            this._tetrisBoard = new Tetris.UserControls.TetrisBoardMatrix();
            this._blockQueue = new Tetris.UserControls.TetrisBlockQueue();
            ((System.ComponentModel.ISupportInitialize)(this.pbGameOver)).BeginInit();
            this.SuspendLayout();
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.Location = new System.Drawing.Point(31, 62);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(111, 32);
            this.lblScore.TabIndex = 5;
            this.lblScore.Text = "Score: ";
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevel.Location = new System.Drawing.Point(31, 21);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(98, 32);
            this.lblLevel.TabIndex = 6;
            this.lblLevel.Text = "Level:";
            // 
            // lblPaused
            // 
            this.lblPaused.AutoSize = true;
            this.lblPaused.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPaused.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaused.ForeColor = System.Drawing.Color.Red;
            this.lblPaused.Location = new System.Drawing.Point(932, 21);
            this.lblPaused.Name = "lblPaused";
            this.lblPaused.Size = new System.Drawing.Size(125, 37);
            this.lblPaused.TabIndex = 7;
            this.lblPaused.Text = "Paused";
            this.lblPaused.Visible = false;
            // 
            // pbGameOver
            // 
            this.pbGameOver.Image = global::Tetris.Properties.Resources.game_over;
            this.pbGameOver.Location = new System.Drawing.Point(597, 186);
            this.pbGameOver.Name = "pbGameOver";
            this.pbGameOver.Size = new System.Drawing.Size(600, 600);
            this.pbGameOver.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbGameOver.TabIndex = 4;
            this.pbGameOver.TabStop = false;
            this.pbGameOver.Visible = false;
            // 
            // lblLines
            // 
            this.lblLines.AutoSize = true;
            this.lblLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLines.Location = new System.Drawing.Point(31, 105);
            this.lblLines.Name = "lblLines";
            this.lblLines.Size = new System.Drawing.Size(98, 32);
            this.lblLines.TabIndex = 9;
            this.lblLines.Text = "Lines:";
            // 
            // matchAnimationControl1
            // 
            this.matchAnimationControl1.Location = new System.Drawing.Point(12, 166);
            this.matchAnimationControl1.Name = "matchAnimationControl1";
            this.matchAnimationControl1.Size = new System.Drawing.Size(614, 601);
            this.matchAnimationControl1.TabIndex = 8;
            // 
            // _tetrisBoard
            // 
            this._tetrisBoard.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this._tetrisBoard.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._tetrisBoard.Location = new System.Drawing.Point(632, 12);
            this._tetrisBoard.Name = "_tetrisBoard";
            this._tetrisBoard.Size = new System.Drawing.Size(680, 1390);
            this._tetrisBoard.TabIndex = 3;
            // 
            // _blockQueue
            // 
            this._blockQueue.Location = new System.Drawing.Point(1355, 12);
            this._blockQueue.Name = "_blockQueue";
            this._blockQueue.Size = new System.Drawing.Size(475, 901);
            this._blockQueue.TabIndex = 2;
            // 
            // TetrisBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1842, 1474);
            this.Controls.Add(this.lblLines);
            this.Controls.Add(this.matchAnimationControl1);
            this.Controls.Add(this.lblPaused);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.pbGameOver);
            this.Controls.Add(this._tetrisBoard);
            this.Controls.Add(this._blockQueue);
            this.KeyPreview = true;
            this.Name = "TetrisBoard";
            this.Text = "Tetris";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TetrisBoard_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TetrisBoard_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TetrisBoard_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pbGameOver)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private UserControls.TetrisBlockQueue _blockQueue;
        private UserControls.TetrisBoardMatrix _tetrisBoard;
        private System.Windows.Forms.PictureBox pbGameOver;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblPaused;
        private UserControls.MatchAnimationControl matchAnimationControl1;
        private System.Windows.Forms.Label lblLines;
    }
}