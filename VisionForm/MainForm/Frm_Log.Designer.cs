using System.Windows.Forms;

namespace NovaVision.VisionForm.MainForm
{
    partial class Frm_Log
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Log));
            this.txt_Info = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_Info
            // 
            this.txt_Info.BackColor = System.Drawing.Color.SkyBlue;
            this.txt_Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Info.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Info.Location = new System.Drawing.Point(0, 0);
            this.txt_Info.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_Info.Multiline = true;
            this.txt_Info.Name = "txt_Info";
            this.txt_Info.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_Info.Size = new System.Drawing.Size(1150, 392);
            this.txt_Info.TabIndex = 0;
            // 
            // Frm_Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 392);
            this.Controls.Add(this.txt_Info);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_Log";
            this.TabText = "日志栏";
            this.Text = "日志栏";
            this.DockStateChanged += new System.EventHandler(this.Frm_Log_DockStateChanged);
            this.Load += new System.EventHandler(this.Frm_Log_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public TextBox txt_Info;
    }
}