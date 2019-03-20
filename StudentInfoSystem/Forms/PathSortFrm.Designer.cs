namespace Whu_Navigation.Forms
{
    partial class PathSortFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PathSortFrm));
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.PathOpenMxd = new System.Windows.Forms.ToolStripMenuItem();
            this.AddNetPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.PathSolution = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearNet = new System.Windows.Forms.ToolStripMenuItem();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // axMapControl1
            // 
            this.axMapControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axMapControl1.Location = new System.Drawing.Point(12, 72);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(778, 452);
            this.axMapControl1.TabIndex = 0;
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Location = new System.Drawing.Point(12, 38);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(778, 28);
            this.axToolbarControl1.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PathOpenMxd,
            this.AddNetPoint,
            this.PathSolution,
            this.ClearNet});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(810, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // PathOpenMxd
            // 
            this.PathOpenMxd.Name = "PathOpenMxd";
            this.PathOpenMxd.Size = new System.Drawing.Size(68, 21);
            this.PathOpenMxd.Text = "打开地图";
            this.PathOpenMxd.Click += new System.EventHandler(this.PathOpenMxd_Click);
            // 
            // AddNetPoint
            // 
            this.AddNetPoint.Image = ((System.Drawing.Image)(resources.GetObject("AddNetPoint.Image")));
            this.AddNetPoint.Name = "AddNetPoint";
            this.AddNetPoint.Size = new System.Drawing.Size(84, 21);
            this.AddNetPoint.Text = "添加地点";
            this.AddNetPoint.Click += new System.EventHandler(this.AddNetPoint_Click);
            // 
            // PathSolution
            // 
            this.PathSolution.Image = ((System.Drawing.Image)(resources.GetObject("PathSolution.Image")));
            this.PathSolution.Name = "PathSolution";
            this.PathSolution.Size = new System.Drawing.Size(84, 21);
            this.PathSolution.Text = "路径方案";
            this.PathSolution.Click += new System.EventHandler(this.PathSolution_Click);
            // 
            // ClearNet
            // 
            this.ClearNet.Image = ((System.Drawing.Image)(resources.GetObject("ClearNet.Image")));
            this.ClearNet.Name = "ClearNet";
            this.ClearNet.Size = new System.Drawing.Size(84, 21);
            this.ClearNet.Text = "清除路径";
            this.ClearNet.Click += new System.EventHandler(this.ClearNet_Click);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(375, 280);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 3;
            // 
            // PathSortFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 536);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.axMapControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PathSortFrm";
            this.Text = "最短路径查询";
            this.Load += new System.EventHandler(this.PathSortFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem AddNetPoint;
        private System.Windows.Forms.ToolStripMenuItem PathSolution;
        private System.Windows.Forms.ToolStripMenuItem ClearNet;
        private System.Windows.Forms.ToolStripMenuItem PathOpenMxd;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
    }
}