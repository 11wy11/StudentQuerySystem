namespace Whu_Navigation
{
    partial class AdmitBookmarkName
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
            this.tbBookmarkName = new System.Windows.Forms.TextBox();
            this.tbAdmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbBookmarkName
            // 
            this.tbBookmarkName.Location = new System.Drawing.Point(12, 27);
            this.tbBookmarkName.Name = "tbBookmarkName";
            this.tbBookmarkName.Size = new System.Drawing.Size(71, 21);
            this.tbBookmarkName.TabIndex = 0;
            // 
            // tbAdmit
            // 
            this.tbAdmit.Location = new System.Drawing.Point(97, 27);
            this.tbAdmit.Name = "tbAdmit";
            this.tbAdmit.Size = new System.Drawing.Size(75, 23);
            this.tbAdmit.TabIndex = 1;
            this.tbAdmit.Text = "确定";
            this.tbAdmit.UseVisualStyleBackColor = true;
            this.tbAdmit.Click += new System.EventHandler(this.tbAdmit_Click);
            // 
            // AdmitBookmarkName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(220, 63);
            this.Controls.Add(this.tbAdmit);
            this.Controls.Add(this.tbBookmarkName);
            this.Name = "AdmitBookmarkName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "书签名称设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbBookmarkName;
        private System.Windows.Forms.Button tbAdmit;
    }
}