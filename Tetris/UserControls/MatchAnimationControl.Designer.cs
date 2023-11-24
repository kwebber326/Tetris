namespace Tetris.UserControls
{
    partial class MatchAnimationControl
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
            this.lblScoreAdded = new System.Windows.Forms.Label();
            this.lblAdjective = new System.Windows.Forms.Label();
            this.pnlDescription = new System.Windows.Forms.Panel();
            this.lblCombo = new System.Windows.Forms.Label();
            this.pbTetris = new System.Windows.Forms.PictureBox();
            this.lblTSpin = new System.Windows.Forms.Label();
            this.pnlDescription.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTetris)).BeginInit();
            this.SuspendLayout();
            // 
            // lblScoreAdded
            // 
            this.lblScoreAdded.AutoSize = true;
            this.lblScoreAdded.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScoreAdded.ForeColor = System.Drawing.Color.Red;
            this.lblScoreAdded.Location = new System.Drawing.Point(16, 12);
            this.lblScoreAdded.Name = "lblScoreAdded";
            this.lblScoreAdded.Size = new System.Drawing.Size(99, 32);
            this.lblScoreAdded.TabIndex = 0;
            this.lblScoreAdded.Text = "label1";
            // 
            // lblAdjective
            // 
            this.lblAdjective.AutoSize = true;
            this.lblAdjective.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdjective.ForeColor = System.Drawing.Color.Red;
            this.lblAdjective.Location = new System.Drawing.Point(16, 54);
            this.lblAdjective.Name = "lblAdjective";
            this.lblAdjective.Size = new System.Drawing.Size(99, 32);
            this.lblAdjective.TabIndex = 1;
            this.lblAdjective.Text = "label1";
            // 
            // pnlDescription
            // 
            this.pnlDescription.Controls.Add(this.lblTSpin);
            this.pnlDescription.Controls.Add(this.lblCombo);
            this.pnlDescription.Controls.Add(this.lblScoreAdded);
            this.pnlDescription.Controls.Add(this.lblAdjective);
            this.pnlDescription.Location = new System.Drawing.Point(19, 15);
            this.pnlDescription.Name = "pnlDescription";
            this.pnlDescription.Size = new System.Drawing.Size(354, 100);
            this.pnlDescription.TabIndex = 2;
            // 
            // lblCombo
            // 
            this.lblCombo.AutoSize = true;
            this.lblCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCombo.ForeColor = System.Drawing.Color.Red;
            this.lblCombo.Location = new System.Drawing.Point(196, 12);
            this.lblCombo.Name = "lblCombo";
            this.lblCombo.Size = new System.Drawing.Size(99, 32);
            this.lblCombo.TabIndex = 4;
            this.lblCombo.Text = "label1";
            // 
            // pbTetris
            // 
            this.pbTetris.Image = global::Tetris.Properties.Resources.TETRIS_3DS_LOGO;
            this.pbTetris.Location = new System.Drawing.Point(19, 121);
            this.pbTetris.Name = "pbTetris";
            this.pbTetris.Size = new System.Drawing.Size(400, 279);
            this.pbTetris.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbTetris.TabIndex = 3;
            this.pbTetris.TabStop = false;
            // 
            // lblTSpin
            // 
            this.lblTSpin.AutoSize = true;
            this.lblTSpin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTSpin.ForeColor = System.Drawing.Color.Red;
            this.lblTSpin.Location = new System.Drawing.Point(196, 54);
            this.lblTSpin.Name = "lblTSpin";
            this.lblTSpin.Size = new System.Drawing.Size(99, 32);
            this.lblTSpin.TabIndex = 5;
            this.lblTSpin.Text = "label1";
            // 
            // MatchAnimationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbTetris);
            this.Controls.Add(this.pnlDescription);
            this.Name = "MatchAnimationControl";
            this.Size = new System.Drawing.Size(579, 444);
            this.Load += new System.EventHandler(this.MatchAnimationControl_Load);
            this.pnlDescription.ResumeLayout(false);
            this.pnlDescription.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTetris)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblScoreAdded;
        private System.Windows.Forms.Label lblAdjective;
        private System.Windows.Forms.Panel pnlDescription;
        private System.Windows.Forms.PictureBox pbTetris;
        private System.Windows.Forms.Label lblCombo;
        private System.Windows.Forms.Label lblTSpin;
    }
}
