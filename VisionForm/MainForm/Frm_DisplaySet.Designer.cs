using NovaVision.UserControlLibrary;

namespace NovaVision.VisionForm.MainForm
{
    partial class Frm_DisplaySet
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
            this.imageDisplayControl1 = new ImageDisplayControl();
            this.SuspendLayout();
            // 
            // imageDisplayControl1
            // 
            this.imageDisplayControl1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.imageDisplayControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageDisplayControl1.Location = new System.Drawing.Point(0, 0);
            this.imageDisplayControl1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.imageDisplayControl1.Name = "imageDisplayControl1";
            this.imageDisplayControl1.Size = new System.Drawing.Size(586, 356);
            this.imageDisplayControl1.TabIndex = 0;
            this.imageDisplayControl1.BtnSaveClick += new System.EventHandler(this.imageDisplayControl1_BtnSaveClick);
            this.imageDisplayControl1.Load += new System.EventHandler(this.imageDisplayControl1_Load);
            // 
            // Frm_DisplaySet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 356);
            this.Controls.Add(this.imageDisplayControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Frm_DisplaySet";
            this.Text = "显示窗口设置";
            this.ResumeLayout(false);

        }

        #endregion

        private ImageDisplayControl imageDisplayControl1;
    }
}