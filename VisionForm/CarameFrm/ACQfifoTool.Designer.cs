using Cognex.VisionPro;

namespace NovaVision.VisionForm.CarameFrm
{
    partial class ACQfifoTool
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
            this.cogAcqFifoCtlV21 = new Cognex.VisionPro.CogAcqFifoCtlV2();
            base.SuspendLayout();
            this.cogAcqFifoCtlV21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cogAcqFifoCtlV21.Location = new System.Drawing.Point(0, 0);
            this.cogAcqFifoCtlV21.Name = "cogAcqFifoCtlV21";
            this.cogAcqFifoCtlV21.Size = new System.Drawing.Size(800, 450);
            this.cogAcqFifoCtlV21.Subject = null;
            this.cogAcqFifoCtlV21.TabIndex = 1;
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(800, 450);
            base.Controls.Add(this.cogAcqFifoCtlV21);
            base.Name = "ACQfifoTool";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "相机设置";
            base.ResumeLayout(false);
        }

        #endregion

        public CogAcqFifoCtlV2 cogAcqFifoCtlV21;
    }
}