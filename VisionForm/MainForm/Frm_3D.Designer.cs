namespace NovaVision.VisionForm.MainForm
{
    partial class Frm_3D
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
            this.components = new System.ComponentModel.Container();
            base.SuspendLayout();
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(567, 477);
            base.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            base.Name = "Frm_3D";
            base.TabText = "3D显示窗口";
            this.Text = "3D显示窗口";
            base.DockStateChanged += new System.EventHandler(Frm_3D_DockStateChanged);
            base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Frm_3D_FormClosing);
            base.ResumeLayout(false);
        }

        #endregion
    }
}