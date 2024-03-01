using System;
using System.Drawing;
using System.Windows.Forms;

namespace SolarBar.UI
{
    partial class UcSolarBar
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

        private void InitializeComponent()
        {
            this.tooltip1 = new CustomToolTip();
            this.barChart1 = new BarChart();
            this.buttonL = new Button();
            this.buttonP = new Button();
            this.contextMenuStrip1 = new ContextMenuStrip();
            this.energyInfo = new System.Windows.Forms.Label();
            this.energyDate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.barChart1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.barChart1.Dock = System.Windows.Forms.DockStyle.None;
            this.barChart1.Location = new System.Drawing.Point(0, 0);
            this.barChart1.Name = "pictureBox1";
            this.barChart1.Size = new System.Drawing.Size(60, 30);
            this.barChart1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
            this.barChart1.TabIndex = 1;
            this.barChart1.TabStop = false;
            this.barChart1.ContextMenuStrip = this.contextMenuStrip1;
            this.barChart1.Click += new System.EventHandler(this.refresh_Click);
            // 
            // energyInfo
            // 
            /*    this.energyInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
                this.energyInfo.AutoSize = true;*/
            this.energyInfo.Dock = System.Windows.Forms.DockStyle.None;
            this.energyInfo.AutoSize = false;
            this.energyInfo.BackColor = System.Drawing.Color.Transparent;
            this.energyInfo.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.energyInfo.Location = new System.Drawing.Point(61, 1);
            this.energyInfo.Name = "energyInfo";
            this.energyInfo.Size = new System.Drawing.Size(260, 16);
            this.energyInfo.TabIndex = 2;
            this.energyInfo.Text = "Czekam na dane...";
            this.energyInfo.Click += new System.EventHandler(this.refresh_Click);
            this.energyInfo.Font = new Font("Arial", 12);
            this.energyInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;

            this.energyInfo.ContextMenuStrip = this.contextMenuStrip1;

            // contextMenuStrip1
            ToolStripMenuItem saveCVSMenuItem = new ToolStripMenuItem("Zapisz aktualne dane...");
            ToolStripMenuItem showEditorMenuItem = new ToolStripMenuItem("Zmień parametry...");

            // Dodanie obsługi zdarzeń
            showEditorMenuItem.Click += showEditorMenuItem_Click;
            saveCVSMenuItem.Click += SaveToCSVButton_Click;

            this.contextMenuStrip1.Items.Add(saveCVSMenuItem);
            this.contextMenuStrip1.Items.Add(showEditorMenuItem);

            // 
            // energyDate
            // 
            this.energyDate.Dock = System.Windows.Forms.DockStyle.None;
            this.energyDate.AutoSize = false;
            this.energyDate.BackColor = System.Drawing.Color.Transparent;
            this.energyDate.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.energyDate.Location = new System.Drawing.Point(111, 20);
            this.energyDate.Name = "energyDate";
            this.energyDate.Size = new System.Drawing.Size(260, 13);
            this.energyDate.TabIndex = 2;
            this.energyDate.Text = "";
            this.energyDate.Font = new Font("Arial", 9);
            this.energyDate.RightToLeft = System.Windows.Forms.RightToLeft.No;

            this.buttonL.Dock = System.Windows.Forms.DockStyle.None;
            this.buttonL.AutoSize = false;
            buttonL.Text = "\u25c0";
            this.buttonL.Name = "buttonL";
            buttonL.Size = new System.Drawing.Size(16, 20);
            buttonL.Location = new System.Drawing.Point(64, 20);
            buttonL.Font = new Font("Arial", 13);
            this.buttonL.BackColor = System.Drawing.Color.Transparent;
            this.buttonL.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.buttonL.FlatStyle = FlatStyle.Flat;
            this.buttonL.FlatAppearance.BorderSize = 0;
            buttonL.Click += new EventHandler(buttonL_Click);

            this.buttonP.Dock = System.Windows.Forms.DockStyle.None;
            this.buttonP.AutoSize = false;
            buttonP.Text = "\u25B6"; 
            this.buttonP.Name = "buttonP";
            buttonP.Size = new System.Drawing.Size(16, 20);
            buttonP.Location = new System.Drawing.Point(81, 20);
            buttonP.Font = new Font("Arial", 13);
            this.buttonP.BackColor = System.Drawing.Color.Transparent;
            this.buttonP.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.buttonP.FlatStyle = FlatStyle.Flat;
            this.buttonP.FlatAppearance.BorderSize = 0;
            buttonP.Click += new EventHandler(buttonP_Click);
     
            // 
            // SolarBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.energyInfo);
            this.Controls.Add(this.energyDate);
            this.Controls.Add(this.barChart1);
            this.Controls.Add(this.buttonL);
            this.Controls.Add(this.buttonP);
            this.Name = "SolarBar";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Size = new System.Drawing.Size(245, 21);
            this.Click += new System.EventHandler(this.refresh_Click);
            ((System.ComponentModel.ISupportInitialize)(this.barChart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private CustomToolTip tooltip1;
        private BarChart barChart1;
        private Label energyInfo;
        private Label energyDate;
        private ContextMenuStrip contextMenuStrip1;
        private Button buttonL;
        private Button buttonP;
    }
}
