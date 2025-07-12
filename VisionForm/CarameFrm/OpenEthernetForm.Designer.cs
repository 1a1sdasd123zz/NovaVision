using System.Windows.Forms;

namespace NovaVision.VisionForm.CarameFrm
{
    partial class OpenEthernetForm
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
            this._labelIpAddress = new System.Windows.Forms.Label();
            this._textBoxIpFirstSegment = new System.Windows.Forms.TextBox();
            this._textBoxIpSecondSegment = new System.Windows.Forms.TextBox();
            this._textBoxIpThirdSegment = new System.Windows.Forms.TextBox();
            this._textBoxIpFourthSegment = new System.Windows.Forms.TextBox();
            this._labelPort = new System.Windows.Forms.Label();
            this._buttonOk = new System.Windows.Forms.Button();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._textBoxPort = new System.Windows.Forms.TextBox();
            this._labelDescription = new System.Windows.Forms.Label();
            base.SuspendLayout();
            this._labelIpAddress.AutoSize = true;
            this._labelIpAddress.Location = new System.Drawing.Point(12, 40);
            this._labelIpAddress.Name = "_labelIpAddress";
            this._labelIpAddress.Size = new System.Drawing.Size(58, 13);
            this._labelIpAddress.TabIndex = 1;
            this._labelIpAddress.Text = "IP address";
            this._textBoxIpFirstSegment.Location = new System.Drawing.Point(81, 37);
            this._textBoxIpFirstSegment.Name = "_textBoxIpFirstSegment";
            this._textBoxIpFirstSegment.Size = new System.Drawing.Size(44, 21);
            this._textBoxIpFirstSegment.TabIndex = 2;
            this._textBoxIpFirstSegment.Text = "192";
            this._textBoxIpSecondSegment.Location = new System.Drawing.Point(131, 37);
            this._textBoxIpSecondSegment.Name = "_textBoxIpSecondSegment";
            this._textBoxIpSecondSegment.Size = new System.Drawing.Size(44, 21);
            this._textBoxIpSecondSegment.TabIndex = 3;
            this._textBoxIpSecondSegment.Text = "168";
            this._textBoxIpThirdSegment.Location = new System.Drawing.Point(181, 37);
            this._textBoxIpThirdSegment.Name = "_textBoxIpThirdSegment";
            this._textBoxIpThirdSegment.Size = new System.Drawing.Size(44, 21);
            this._textBoxIpThirdSegment.TabIndex = 4;
            this._textBoxIpThirdSegment.Text = "0";
            this._textBoxIpFourthSegment.Location = new System.Drawing.Point(231, 37);
            this._textBoxIpFourthSegment.Name = "_textBoxIpFourthSegment";
            this._textBoxIpFourthSegment.Size = new System.Drawing.Size(44, 21);
            this._textBoxIpFourthSegment.TabIndex = 5;
            this._textBoxIpFourthSegment.Text = "1";
            this._labelPort.AutoSize = true;
            this._labelPort.Location = new System.Drawing.Point(12, 74);
            this._labelPort.Name = "_labelPort";
            this._labelPort.Size = new System.Drawing.Size(27, 13);
            this._labelPort.TabIndex = 6;
            this._labelPort.Text = "Port";
            this._buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this._buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOk.Location = new System.Drawing.Point(231, 104);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(75, 25);
            this._buttonOk.TabIndex = 8;
            this._buttonOk.Text = "OK";
            this._buttonOk.UseVisualStyleBackColor = true;
            this._buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(327, 104);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 25);
            this._buttonCancel.TabIndex = 9;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            this._textBoxPort.Location = new System.Drawing.Point(81, 70);
            this._textBoxPort.Name = "_textBoxPort";
            this._textBoxPort.Size = new System.Drawing.Size(194, 21);
            this._textBoxPort.TabIndex = 7;
            this._textBoxPort.Text = "24691";
            this._labelDescription.AutoSize = true;
            this._labelDescription.Location = new System.Drawing.Point(12, 10);
            this._labelDescription.Name = "_labelDescription";
            this._labelDescription.Size = new System.Drawing.Size(365, 13);
            this._labelDescription.TabIndex = 0;
            this._labelDescription.Text = "[Valid range] The IP address is a byte value and the port is a ushort value.";
            base.AcceptButton = this._buttonOk;
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.CancelButton = this._buttonCancel;
            base.ClientSize = new System.Drawing.Size(414, 142);
            base.Controls.Add(this._labelDescription);
            base.Controls.Add(this._textBoxPort);
            base.Controls.Add(this._buttonCancel);
            base.Controls.Add(this._buttonOk);
            base.Controls.Add(this._labelPort);
            base.Controls.Add(this._textBoxIpFourthSegment);
            base.Controls.Add(this._textBoxIpThirdSegment);
            base.Controls.Add(this._textBoxIpSecondSegment);
            base.Controls.Add(this._textBoxIpFirstSegment);
            base.Controls.Add(this._labelIpAddress);
            this.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "OpenEthernetForm";
            base.ShowIcon = false;
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Open Ethernet";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion

        private Label _labelIpAddress;

        private TextBox _textBoxIpFirstSegment;

        private TextBox _textBoxIpSecondSegment;

        private TextBox _textBoxIpThirdSegment;

        private TextBox _textBoxIpFourthSegment;

        private Label _labelPort;

        private TextBox _textBoxPort;

        private Label _labelDescription;

        public Button _buttonOk;

        public Button _buttonCancel;
    }
}