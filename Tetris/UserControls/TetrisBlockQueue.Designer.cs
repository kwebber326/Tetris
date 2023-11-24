namespace Tetris.UserControls
{
    partial class TetrisBlockQueue
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
            this.pnlBlockQueue = new System.Windows.Forms.Panel();
            this.lblNext = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pnlBlockQueue
            // 
            this.pnlBlockQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBlockQueue.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnlBlockQueue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlBlockQueue.Location = new System.Drawing.Point(3, 51);
            this.pnlBlockQueue.Name = "pnlBlockQueue";
            this.pnlBlockQueue.Size = new System.Drawing.Size(450, 695);
            this.pnlBlockQueue.TabIndex = 0;
            // 
            // lblNext
            // 
            this.lblNext.AutoSize = true;
            this.lblNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNext.Location = new System.Drawing.Point(132, 4);
            this.lblNext.Name = "lblNext";
            this.lblNext.Size = new System.Drawing.Size(73, 29);
            this.lblNext.TabIndex = 1;
            this.lblNext.Text = "Next:";
            // 
            // TetrisBlockQueue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblNext);
            this.Controls.Add(this.pnlBlockQueue);
            this.Name = "TetrisBlockQueue";
            this.Size = new System.Drawing.Size(456, 963);
            this.Load += new System.EventHandler(this.TetrisBlockQueue_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlBlockQueue;
        private System.Windows.Forms.Label lblNext;
    }
}
