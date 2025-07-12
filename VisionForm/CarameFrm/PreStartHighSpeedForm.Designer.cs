using System.Windows.Forms;

namespace NovaVision.VisionForm.CarameFrm
{
    partial class PreStartHighSpeedForm
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
            this._textBoxSendPos = new System.Windows.Forms.TextBox();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._buttonOk = new System.Windows.Forms.Button();
            this._labelSendPos = new System.Windows.Forms.Label();
            base.SuspendLayout();
            this._textBoxSendPos.Location = new System.Drawing.Point(129, 20);
            this._textBoxSendPos.Name = "_textBoxSendPos";
            this._textBoxSendPos.Size = new System.Drawing.Size(107, 21);
            this._textBoxSendPos.TabIndex = 1;
            this._textBoxSendPos.Text = "2";
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(177, 54);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 25);
            this._buttonCancel.TabIndex = 3;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            this._buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOk.Location = new System.Drawing.Point(80, 54);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(75, 25);
            this._buttonOk.TabIndex = 2;
            this._buttonOk.Text = "OK";
            this._buttonOk.UseVisualStyleBackColor = true;
            this._labelSendPos.AutoSize = true;
            this._labelSendPos.Location = new System.Drawing.Point(21, 23);
            this._labelSendPos.Name = "_labelSendPos";
            this._labelSendPos.Size = new System.Drawing.Size(97, 13);
            this._labelSendPos.TabIndex = 0;
            this._labelSendPos.Text = "Send start position";
            base.AcceptButton = this._buttonOk;
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.CancelButton = this._buttonCancel;
            base.ClientSize = new System.Drawing.Size(282, 90);
            base.Controls.Add(this._textBoxSendPos);
            base.Controls.Add(this._buttonCancel);
            base.Controls.Add(this._buttonOk);
            base.Controls.Add(this._labelSendPos);
            this.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "PreStartHighSpeedForm";
            base.ShowIcon = false;
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "High-speed communication start request";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion

        private TextBox _textBoxSendPos;

        private Button _buttonCancel;

        private Button _buttonOk;

        private Label _labelSendPos;
    }
}