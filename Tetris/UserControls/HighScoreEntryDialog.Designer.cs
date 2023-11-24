namespace Tetris.UserControls
{
    partial class HighScoreEntryDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtInitials = new System.Windows.Forms.TextBox();
            this.btnDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(460, 47);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter Your Initials:";
            // 
            // txtInitials
            // 
            this.txtInitials.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.txtInitials.Font = new System.Drawing.Font("Consolas", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInitials.ForeColor = System.Drawing.SystemColors.Info;
            this.txtInitials.Location = new System.Drawing.Point(477, 17);
            this.txtInitials.Name = "txtInitials";
            this.txtInitials.Size = new System.Drawing.Size(181, 54);
            this.txtInitials.TabIndex = 1;
            this.txtInitials.TextChanged += new System.EventHandler(this.TxtInitials_TextChanged);
            // 
            // btnDone
            // 
            this.btnDone.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDone.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDone.Location = new System.Drawing.Point(373, 336);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(162, 76);
            this.btnDone.TabIndex = 2;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.BtnDone_Click);
            // 
            // HighScoreEntryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(934, 431);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.txtInitials);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.KeyPreview = true;
            this.Name = "HighScoreEntryDialog";
            this.Load += new System.EventHandler(this.HighScoreEntryDialog_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HighScoreEntryDialog_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInitials;
        private System.Windows.Forms.Button btnDone;
    }
}
