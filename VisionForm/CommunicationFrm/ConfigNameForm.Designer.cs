using System.Drawing;
using System.Windows.Forms;

namespace NovaVision.VisionForm.CommunicationFrm
{
    partial class ConfigNameForm
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
            this.txt_Name = new TextBox();
            this.btn_Yes = new Button();
            this.label1 = new Label();
            this.btn_No = new Button();
            base.SuspendLayout();
            this.txt_Name.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.txt_Name.Location = new Point(80, 34);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new Size(151, 23);
            this.txt_Name.TabIndex = 0;
            this.btn_Yes.Location = new Point(29, 97);
            this.btn_Yes.Name = "btn_Yes";
            this.btn_Yes.Size = new Size(75, 23);
            this.btn_Yes.TabIndex = 1;
            this.btn_Yes.Text = "确定";
            this.btn_Yes.UseVisualStyleBackColor = true;
            this.btn_Yes.Click += this.btn_Yes_Click;
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.label1.Location = new Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "配置名称：";
            this.btn_No.Location = new Point(142, 97);
            this.btn_No.Name = "btn_No";
            this.btn_No.Size = new Size(75, 23);
            this.btn_No.TabIndex = 3;
            this.btn_No.Text = "取消";
            this.btn_No.UseVisualStyleBackColor = true;
            this.btn_No.Click += this.btn_No_Click;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(247, 138);
            base.ControlBox = false;
            base.Controls.Add(this.btn_No);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btn_Yes);
            base.Controls.Add(this.txt_Name);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            this.Name = "ConfigNameForm";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "配置通讯点位名称";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion

        private TextBox txt_Name;

        private Button btn_Yes;

        private Label label1;

        private Button btn_No;
    }
}