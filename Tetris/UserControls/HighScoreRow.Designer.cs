namespace Tetris.UserControls
{
    partial class HighScoreRow
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblRank = new System.Windows.Forms.Label();
            this.lblInitial1 = new System.Windows.Forms.Label();
            this.lblInitial2 = new System.Windows.Forms.Label();
            this.lblInitial3 = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblRank
            // 
            this.lblRank.AutoSize = true;
            this.lblRank.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRank.Font = new System.Drawing.Font("Consolas", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRank.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblRank.Location = new System.Drawing.Point(3, 0);
            this.lblRank.Name = "lblRank";
            this.lblRank.Size = new System.Drawing.Size(44, 49);
            this.lblRank.TabIndex = 0;
            this.lblRank.Text = "1";
            // 
            // lblInitial1
            // 
            this.lblInitial1.AutoSize = true;
            this.lblInitial1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInitial1.Font = new System.Drawing.Font("Consolas", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInitial1.Location = new System.Drawing.Point(84, 1);
            this.lblInitial1.Name = "lblInitial1";
            this.lblInitial1.Size = new System.Drawing.Size(44, 49);
            this.lblInitial1.TabIndex = 1;
            this.lblInitial1.Text = "1";
            // 
            // lblInitial2
            // 
            this.lblInitial2.AutoSize = true;
            this.lblInitial2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInitial2.Font = new System.Drawing.Font("Consolas", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInitial2.Location = new System.Drawing.Point(134, 1);
            this.lblInitial2.Name = "lblInitial2";
            this.lblInitial2.Size = new System.Drawing.Size(44, 49);
            this.lblInitial2.TabIndex = 2;
            this.lblInitial2.Text = "1";
            // 
            // lblInitial3
            // 
            this.lblInitial3.AutoSize = true;
            this.lblInitial3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInitial3.Font = new System.Drawing.Font("Consolas", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInitial3.Location = new System.Drawing.Point(184, 1);
            this.lblInitial3.Name = "lblInitial3";
            this.lblInitial3.Size = new System.Drawing.Size(44, 49);
            this.lblInitial3.TabIndex = 3;
            this.lblInitial3.Text = "1";
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblScore.Font = new System.Drawing.Font("Consolas", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.Location = new System.Drawing.Point(265, 0);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(44, 49);
            this.lblScore.TabIndex = 4;
            this.lblScore.Text = "0";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDate.Font = new System.Drawing.Font("Consolas", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(425, 1);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(242, 49);
            this.lblDate.TabIndex = 5;
            this.lblDate.Text = "06/06/1984";
            // 
            // HighScoreRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblInitial3);
            this.Controls.Add(this.lblInitial2);
            this.Controls.Add(this.lblInitial1);
            this.Controls.Add(this.lblRank);
            this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Name = "HighScoreRow";
            this.Size = new System.Drawing.Size(721, 64);
            this.Load += new System.EventHandler(this.HighScoreRow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRank;
        private System.Windows.Forms.Label lblInitial1;
        private System.Windows.Forms.Label lblInitial2;
        private System.Windows.Forms.Label lblInitial3;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblDate;
    }
}
