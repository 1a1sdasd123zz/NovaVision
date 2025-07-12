using System.Windows.Forms;
using NovaVision.UserControlLibrary;

namespace NovaVision.VisionForm.LoginFrm
{
    partial class Frm_Login
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
            this.comboBox_username = new System.Windows.Forms.ComboBox();
            this.label_pwd = new System.Windows.Forms.Label();
            this.btn_login = new System.Windows.Forms.Button();
            this.label_username = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbl_auth = new System.Windows.Forms.ToolStripStatusLabel();
            this.textBox_authority = new System.Windows.Forms.ToolStripStatusLabel();
            this.textBoxEx1 = new TextBoxEx(this.components);
            this.textBox_pwd = new TextBoxEx(this.components);
            this.pic1 = new System.Windows.Forms.PictureBox();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox_username
            // 
            this.comboBox_username.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_username.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_username.FormattingEnabled = true;
            this.comboBox_username.Location = new System.Drawing.Point(216, 99);
            this.comboBox_username.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_username.Name = "comboBox_username";
            this.comboBox_username.Size = new System.Drawing.Size(208, 32);
            this.comboBox_username.TabIndex = 16;
            this.comboBox_username.SelectedIndexChanged += new System.EventHandler(this.comboBox_username_SelectedIndexChanged);
            // 
            // label_pwd
            // 
            this.label_pwd.BackColor = System.Drawing.Color.Transparent;
            this.label_pwd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_pwd.ForeColor = System.Drawing.Color.White;
            this.label_pwd.Location = new System.Drawing.Point(111, 184);
            this.label_pwd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_pwd.Name = "label_pwd";
            this.label_pwd.Size = new System.Drawing.Size(114, 22);
            this.label_pwd.TabIndex = 13;
            this.label_pwd.Text = "密码：";
            this.label_pwd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_login
            // 
            this.btn_login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(140)))), ((int)(((byte)(206)))));
            this.btn_login.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_login.FlatAppearance.BorderSize = 0;
            this.btn_login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_login.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_login.ForeColor = System.Drawing.Color.White;
            this.btn_login.Location = new System.Drawing.Point(126, 302);
            this.btn_login.Margin = new System.Windows.Forms.Padding(0);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(303, 45);
            this.btn_login.TabIndex = 10;
            this.btn_login.Text = "登  录";
            this.btn_login.UseVisualStyleBackColor = false;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // label_username
            // 
            this.label_username.BackColor = System.Drawing.Color.Transparent;
            this.label_username.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_username.ForeColor = System.Drawing.Color.White;
            this.label_username.Location = new System.Drawing.Point(111, 100);
            this.label_username.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_username.Name = "label_username";
            this.label_username.Size = new System.Drawing.Size(114, 22);
            this.label_username.TabIndex = 12;
            this.label_username.Text = "用户名：";
            this.label_username.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(140)))), ((int)(((byte)(206)))));
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_auth,
            this.textBox_authority});
            this.statusStrip1.Location = new System.Drawing.Point(0, 398);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(564, 33);
            this.statusStrip1.TabIndex = 21;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.Visible = false;
            // 
            // lbl_auth
            // 
            this.lbl_auth.BackColor = System.Drawing.Color.Transparent;
            this.lbl_auth.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lbl_auth.ForeColor = System.Drawing.Color.White;
            this.lbl_auth.Name = "lbl_auth";
            this.lbl_auth.Size = new System.Drawing.Size(64, 26);
            this.lbl_auth.Text = "权限：";
            // 
            // textBox_authority
            // 
            this.textBox_authority.BackColor = System.Drawing.Color.Transparent;
            this.textBox_authority.ForeColor = System.Drawing.Color.White;
            this.textBox_authority.Name = "textBox_authority";
            this.textBox_authority.Size = new System.Drawing.Size(46, 26);
            this.textBox_authority.Text = "N/A";
            // 
            // textBoxEx1
            // 
            this.textBoxEx1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxEx1.Location = new System.Drawing.Point(216, 183);
            this.textBoxEx1.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxEx1.Name = "textBoxEx1";
            this.textBoxEx1.PasswordChar = '*';
            this.textBoxEx1.Size = new System.Drawing.Size(208, 31);
            this.textBoxEx1.TabIndex = 19;
            this.textBoxEx1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_pwd_KeyUp);
            // 
            // textBox_pwd
            // 
            this.textBox_pwd.Location = new System.Drawing.Point(30, 0);
            this.textBox_pwd.Name = "textBox_pwd";
            this.textBox_pwd.Size = new System.Drawing.Size(100, 28);
            this.textBox_pwd.TabIndex = 0;
            // 
            // pic1
            // 
            this.pic1.BackColor = System.Drawing.Color.White;
            this.pic1.Image = global::NovaVision.Properties.Resources.EyeGrey;
            this.pic1.Location = new System.Drawing.Point(399, 194);
            this.pic1.Margin = new System.Windows.Forms.Padding(4);
            this.pic1.Name = "pic1";
            this.pic1.Size = new System.Drawing.Size(22, 12);
            this.pic1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic1.TabIndex = 27;
            this.pic1.TabStop = false;
            this.pic1.Tag = "";
            this.pic1.Click += new System.EventHandler(this.pic1_Click);
            // 
            // Frm_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(97)))));
            this.ClientSize = new System.Drawing.Size(564, 430);
            this.Controls.Add(this.pic1);
            this.Controls.Add(this.textBoxEx1);
            this.Controls.Add(this.comboBox_username);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.label_username);
            this.Controls.Add(this.label_pwd);
            this.Controls.Add(this.statusStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Login";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录系统";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Login_FormClosing);
            this.Load += new System.EventHandler(this.Frm_Login_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBoxEx textBox_pwd;

        private TextBoxEx textBoxEx1;

        private Label label_username;

        private Button btn_login;

        private Label label_pwd;

        private ComboBox comboBox_username;

        private StatusStrip statusStrip1;

        private ToolStripStatusLabel lbl_auth;

        private ToolStripStatusLabel textBox_authority;
        private PictureBox pic1;
    }
}