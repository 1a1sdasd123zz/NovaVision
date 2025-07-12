using System.Drawing;
using System.Windows.Forms;
using NovaVision.UserControlLibrary.CommCtrl;

namespace NovaVision.VisionForm.CommunicationFrm
{
    partial class CommInOutConfigForm
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
            this.mCommDataConfig_Ctrl = new CommCtrl();
            base.SuspendLayout();
            this.mCommDataConfig_Ctrl.ConfigCtrlWidth = 200;
            this.mCommDataConfig_Ctrl.Dock = DockStyle.Fill;
            this.mCommDataConfig_Ctrl.Location = new Point(0, 0);
            this.mCommDataConfig_Ctrl.Name = "mCommDataConfig_Ctrl";
            this.mCommDataConfig_Ctrl.Size = new Size(1143, 611);
            this.mCommDataConfig_Ctrl.TabIndex = 3;
            this.mCommDataConfig_Ctrl.BtnAddClick += this.mCommDataConfig_Ctrl_BtnAddClick;
            this.mCommDataConfig_Ctrl.BtnDeleteClick += this.mCommDataConfig_Ctrl_BtnDeleteClick;
            this.mCommDataConfig_Ctrl.BtnMoveUpClick += this.mCommDataConfig_Ctrl_BtnMoveUpClick;
            this.mCommDataConfig_Ctrl.BtnMoveDownClick += this.mCommDataConfig_Ctrl_BtnMoveDownClick;
            this.mCommDataConfig_Ctrl.BtnSaveClick += this.mCommDataConfig_Ctrl_BtnSaveClick;
            this.mCommDataConfig_Ctrl.SelectIndexChanged += this.mCommDataConfig_Ctrl_SelectIndexChanged;
            this.mCommDataConfig_Ctrl.ToolStripMenuItem_Click += this.mCommDataConfig_Ctrl_ToolStripMenuItem_Click;
            this.mCommDataConfig_Ctrl.BtnNewLineIn_Click += this.mCommDataConfig_Ctrl_BtnNewLineIn_Click;
            this.mCommDataConfig_Ctrl.BtnDeleteLineIn_Click += this.mCommDataConfig_Ctrl_BtnDeleteLineIn_Click;
            this.mCommDataConfig_Ctrl.BtnNewLineOut_Click += this.mCommDataConfig_Ctrl_BtnNewLineOut_Click;
            this.mCommDataConfig_Ctrl.BtnDeleteLineOut_Click += this.mCommDataConfig_Ctrl_BtnDeleteLineOut_Click;
            this.mCommDataConfig_Ctrl.BtnUpLineIn_Click += this.mCommDataConfig_Ctrl_BtnUpLineIn_Click;
            this.mCommDataConfig_Ctrl.BtnDownLineIn_Click += this.mCommDataConfig_Ctrl_BtnDownLineIn_Click;
            this.mCommDataConfig_Ctrl.BtnUpLineOut_Click += this.mCommDataConfig_Ctrl_BtnUpLineOut_Click;
            this.mCommDataConfig_Ctrl.BtnDownLineOut_Click += this.mCommDataConfig_Ctrl_BtnDownLineOut_Click;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(1143, 611);
            base.Controls.Add(this.mCommDataConfig_Ctrl);
            base.Name = "CommInOutConfigForm";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "CommInOutConfig";
            base.FormClosing += this.CommInOutConfigForm_FormClosing;
            base.ResumeLayout(false);
        }

        #endregion

        private CommCtrl mCommDataConfig_Ctrl;
    }
}