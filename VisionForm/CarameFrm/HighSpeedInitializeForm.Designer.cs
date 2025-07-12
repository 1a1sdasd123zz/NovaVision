using System.Windows.Forms;

namespace NovaVision.VisionForm.CarameFrm
{
    partial class HighSpeedInitializeForm
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
            this._labelDescription = new System.Windows.Forms.Label();
            this._textBoxPort = new System.Windows.Forms.TextBox();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._buttonOk = new System.Windows.Forms.Button();
            this._labelPort = new System.Windows.Forms.Label();
            this._textBoxIpFourthSegment = new System.Windows.Forms.TextBox();
            this._textBoxIpThirdSegment = new System.Windows.Forms.TextBox();
            this._textBoxIpSecondSegment = new System.Windows.Forms.TextBox();
            this._textBoxIpFirstSegment = new System.Windows.Forms.TextBox();
            this._labelIpAddress = new System.Windows.Forms.Label();
            this._textBoxProfileCnt = new System.Windows.Forms.TextBox();
            this._labelProfileCnt = new System.Windows.Forms.Label();
            this._textBoxHighSpeedPortNo = new System.Windows.Forms.TextBox();
            this._labelHighSpeedPortNo = new System.Windows.Forms.Label();
            base.SuspendLayout();
            this._labelDescription.AutoSize = true;
            this._labelDescription.Location = new System.Drawing.Point(12, 12);
            this._labelDescription.Name = "_labelDescription";
            this._labelDescription.Size = new System.Drawing.Size(365, 13);
            this._labelDescription.TabIndex = 0;
            this._labelDescription.Text = "[Valid range] The IP address is a byte value and the port is a ushort value.";
            this._textBoxPort.Location = new System.Drawing.Point(81, 70);
            this._textBoxPort.Name = "_textBoxPort";
            this._textBoxPort.Size = new System.Drawing.Size(194, 21);
            this._textBoxPort.TabIndex = 7;
            this._textBoxPort.Text = "24691";
            this._buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(330, 168);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 25);
            this._buttonCancel.TabIndex = 13;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            this._buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this._buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOk.Location = new System.Drawing.Point(231, 168);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(75, 25);
            this._buttonOk.TabIndex = 12;
            this._buttonOk.Text = "OK";
            this._buttonOk.UseVisualStyleBackColor = true;
            this._labelPort.AutoSize = true;
            this._labelPort.Location = new System.Drawing.Point(12, 74);
            this._labelPort.Name = "_labelPort";
            this._labelPort.Size = new System.Drawing.Size(27, 13);
            this._labelPort.TabIndex = 6;
            this._labelPort.Text = "Port";
            this._textBoxIpFourthSegment.Location = new System.Drawing.Point(231, 38);
            this._textBoxIpFourthSegment.Name = "_textBoxIpFourthSegment";
            this._textBoxIpFourthSegment.Size = new System.Drawing.Size(44, 21);
            this._textBoxIpFourthSegment.TabIndex = 5;
            this._textBoxIpFourthSegment.Text = "1";
            this._textBoxIpThirdSegment.Location = new System.Drawing.Point(181, 38);
            this._textBoxIpThirdSegment.Name = "_textBoxIpThirdSegment";
            this._textBoxIpThirdSegment.Size = new System.Drawing.Size(44, 21);
            this._textBoxIpThirdSegment.TabIndex = 4;
            this._textBoxIpThirdSegment.Text = "0";
            this._textBoxIpSecondSegment.Location = new System.Drawing.Point(131, 38);
            this._textBoxIpSecondSegment.Name = "_textBoxIpSecondSegment";
            this._textBoxIpSecondSegment.Size = new System.Drawing.Size(44, 21);
            this._textBoxIpSecondSegment.TabIndex = 3;
            this._textBoxIpSecondSegment.Text = "168";
            this._textBoxIpFirstSegment.Location = new System.Drawing.Point(81, 38);
            this._textBoxIpFirstSegment.Name = "_textBoxIpFirstSegment";
            this._textBoxIpFirstSegment.Size = new System.Drawing.Size(44, 21);
            this._textBoxIpFirstSegment.TabIndex = 2;
            this._textBoxIpFirstSegment.Text = "192";
            this._labelIpAddress.AutoSize = true;
            this._labelIpAddress.Location = new System.Drawing.Point(12, 42);
            this._labelIpAddress.Name = "_labelIpAddress";
            this._labelIpAddress.Size = new System.Drawing.Size(58, 13);
            this._labelIpAddress.TabIndex = 1;
            this._labelIpAddress.Text = "IP address";
            this._textBoxProfileCnt.Location = new System.Drawing.Point(118, 134);
            this._textBoxProfileCnt.Name = "_textBoxProfileCnt";
            this._textBoxProfileCnt.Size = new System.Drawing.Size(64, 21);
            this._textBoxProfileCnt.TabIndex = 11;
            this._textBoxProfileCnt.Text = "100";
            this._labelProfileCnt.AutoSize = true;
            this._labelProfileCnt.Location = new System.Drawing.Point(12, 138);
            this._labelProfileCnt.Name = "_labelProfileCnt";
            this._labelProfileCnt.Size = new System.Drawing.Size(95, 13);
            this._labelProfileCnt.TabIndex = 10;
            this._labelProfileCnt.Text = "Number of profiles";
            this._textBoxHighSpeedPortNo.Location = new System.Drawing.Point(118, 102);
            this._textBoxHighSpeedPortNo.Name = "_textBoxHighSpeedPortNo";
            this._textBoxHighSpeedPortNo.Size = new System.Drawing.Size(157, 21);
            this._textBoxHighSpeedPortNo.TabIndex = 9;
            this._textBoxHighSpeedPortNo.Text = "24692";
            this._labelHighSpeedPortNo.AutoSize = true;
            this._labelHighSpeedPortNo.Location = new System.Drawing.Point(12, 106);
            this._labelHighSpeedPortNo.Name = "_labelHighSpeedPortNo";
            this._labelHighSpeedPortNo.Size = new System.Drawing.Size(90, 13);
            this._labelHighSpeedPortNo.TabIndex = 8;
            this._labelHighSpeedPortNo.Text = "Port (high speed)";
            base.AcceptButton = this._buttonOk;
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.CancelButton = this._buttonCancel;
            base.ClientSize = new System.Drawing.Size(417, 204);
            base.Controls.Add(this._textBoxProfileCnt);
            base.Controls.Add(this._labelProfileCnt);
            base.Controls.Add(this._textBoxHighSpeedPortNo);
            base.Controls.Add(this._labelHighSpeedPortNo);
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
            base.Name = "HighSpeedInitializeForm";
            base.ShowIcon = false;
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "HighSpeedInitialize";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion


        private Label _labelDescription;

        private TextBox _textBoxPort;

        public Button _buttonCancel;

        public Button _buttonOk;

        private Label _labelPort;

        private TextBox _textBoxIpFourthSegment;

        private TextBox _textBoxIpThirdSegment;

        private TextBox _textBoxIpSecondSegment;

        private TextBox _textBoxIpFirstSegment;

        private Label _labelIpAddress;

        private TextBox _textBoxProfileCnt;

        private Label _labelProfileCnt;

        private TextBox _textBoxHighSpeedPortNo;

        private Label _labelHighSpeedPortNo;
    }
}