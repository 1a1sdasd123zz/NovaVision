using System.Windows.Forms;

namespace NovaVision.VisionForm.MainForm
{
    partial class Frm_Error
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Error));
            this.txt_Error = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_Error
            // 
            this.txt_Error.BackColor = System.Drawing.Color.SkyBlue;
            this.txt_Error.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Error.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Error.Location = new System.Drawing.Point(0, 0);
            this.txt_Error.Margin = new System.Windows.Forms.Padding(4);
            this.txt_Error.Multiline = true;
            this.txt_Error.Name = "txt_Error";
            this.txt_Error.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_Error.Size = new System.Drawing.Size(1000, 392);
            this.txt_Error.TabIndex = 1;
            // 
            // Frm_Error
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 392);
            this.Controls.Add(this.txt_Error);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Frm_Error";
            this.TabText = "错误栏";
            this.Text = "错误栏";
            this.DockStateChanged += new System.EventHandler(this.Frm_Error_DockStateChanged);
            this.Load += new System.EventHandler(this.Frm_Error_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public TextBox txt_Error;
    }
}