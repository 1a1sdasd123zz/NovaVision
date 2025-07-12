using System.Windows.Forms;

namespace NovaVision.VisionForm.MainForm
{
    partial class Frm_Splash
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
            this.lbl_Splash = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_Splash
            // 
            this.lbl_Splash.AutoSize = true;
            this.lbl_Splash.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Splash.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Splash.ForeColor = System.Drawing.Color.Lime;
            this.lbl_Splash.Location = new System.Drawing.Point(44, 501);
            this.lbl_Splash.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Splash.Name = "lbl_Splash";
            this.lbl_Splash.Size = new System.Drawing.Size(193, 28);
            this.lbl_Splash.TabIndex = 0;
            this.lbl_Splash.Text = "视觉系统启动中……";
            // 
            // Frm_Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::NovaVision.Properties.Resources.启动界面;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(795, 568);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_Splash);
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Frm_Splash";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_Splash";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Label lbl_Splash;
    }
}