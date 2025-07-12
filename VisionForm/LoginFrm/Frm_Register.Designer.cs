using System.Windows.Forms;

namespace NovaVision.VisionForm.LoginFrm
{
    partial class Frm_Register
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
            this.label1 = new System.Windows.Forms.Label();
            this.btn_create = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_authority = new System.Windows.Forms.ComboBox();
            this.textBox_pwd = new System.Windows.Forms.TextBox();
            this.pic1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)this.pic1).BeginInit();
            base.SuspendLayout();
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(76, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 15);
            this.label1.TabIndex = 20;
            this.label1.Text = "用户名";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_create.BackColor = System.Drawing.Color.FromArgb(0, 140, 206);
            this.btn_create.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_create.FlatAppearance.BorderSize = 0;
            this.btn_create.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_create.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            this.btn_create.ForeColor = System.Drawing.Color.White;
            this.btn_create.Location = new System.Drawing.Point(98, 184);
            this.btn_create.Margin = new System.Windows.Forms.Padding(0);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(181, 25);
            this.btn_create.TabIndex = 19;
            this.btn_create.Text = "创  建";
            this.btn_create.UseVisualStyleBackColor = false;
            this.btn_create.Click += new System.EventHandler(btn_create_Click);
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(76, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 15);
            this.label2.TabIndex = 21;
            this.label2.Text = "密码";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.textBox_username.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            this.textBox_username.Location = new System.Drawing.Point(158, 39);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(121, 23);
            this.textBox_username.TabIndex = 25;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(76, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 15);
            this.label3.TabIndex = 22;
            this.label3.Text = "权限";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.comboBox_authority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_authority.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            this.comboBox_authority.FormattingEnabled = true;
            this.comboBox_authority.Items.AddRange(new object[5] { "OPN", "OPN技师", "ME", "PE", "管理员" });
            this.comboBox_authority.Location = new System.Drawing.Point(158, 140);
            this.comboBox_authority.Name = "comboBox_authority";
            this.comboBox_authority.Size = new System.Drawing.Size(121, 25);
            this.comboBox_authority.TabIndex = 24;
            this.textBox_pwd.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            this.textBox_pwd.Location = new System.Drawing.Point(158, 89);
            this.textBox_pwd.Name = "textBox_pwd";
            this.textBox_pwd.PasswordChar = '*';
            this.textBox_pwd.Size = new System.Drawing.Size(121, 23);
            this.textBox_pwd.TabIndex = 23;
            this.pic1.BackColor = System.Drawing.Color.White;
            this.pic1.Image = NovaVision.Properties.Resources.EyeGrey;
            this.pic1.Location = new System.Drawing.Point(261, 96);
            this.pic1.Name = "pic1";
            this.pic1.Size = new System.Drawing.Size(15, 8);
            this.pic1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic1.TabIndex = 26;
            this.pic1.TabStop = false;
            this.pic1.Tag = "";
            this.pic1.Click += new System.EventHandler(pic1_Click);
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(0, 64, 97);
            base.ClientSize = new System.Drawing.Size(362, 235);
            base.Controls.Add(this.pic1);
            base.Controls.Add(this.btn_create);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.textBox_username);
            base.Controls.Add(this.comboBox_authority);
            base.Controls.Add(this.textBox_pwd);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.label3);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "Frm_Register";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户注册";
            base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Frm_Register_FormClosing);
            ((System.ComponentModel.ISupportInitialize)this.pic1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion

        private Label label1;

        private Button btn_create;

        private Label label2;

        private TextBox textBox_username;

        private Label label3;

        private ComboBox comboBox_authority;

        private TextBox textBox_pwd;

        private PictureBox pic1;
    }
}