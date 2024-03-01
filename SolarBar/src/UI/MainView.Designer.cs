namespace SolarBar.UI
{
    partial class MainView
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region

        private void InitializeComponent()
        {
            this.ucSolarBar1 = new UcSolarBar();
            this.SuspendLayout();
            // 
            // ucSolarBar1
            // 
            this.ucSolarBar1.BackColor = System.Drawing.Color.Black;
            this.ucSolarBar1.Location = new System.Drawing.Point(3, 3);
            this.ucSolarBar1.Name = "ucSolarBar1";
            this.ucSolarBar1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ucSolarBar1.Size = new System.Drawing.Size(239, 20);
            this.ucSolarBar1.TabIndex = 0;
            this.ucSolarBar1.Load += new System.EventHandler(this.ucSolarBar1_Load);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.ucSolarBar1);
            this.Name = "MainView";
            this.Size = new System.Drawing.Size(246, 26);
            this.ResumeLayout(false);

        }

        #endregion

        private UcSolarBar ucSolarBar1;
    }
}
