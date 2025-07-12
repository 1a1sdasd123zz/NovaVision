using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using NovaVision.BaseClass;
using NovaVision.BaseClass.DataBase;

namespace NovaVision.VisionForm.LoginFrm
{
    public partial class Frm_Register : Form
    {
        private SQLiteInfoBll sQLiteInfoBll = new SQLiteInfoBll();

        private string AuthType = ConfigurationManager.AppSettings["AuthType"];

        private List<Image> images = new List<Image>();

        public Frm_Register()
        {
            InitializeComponent();
            ClearComponent();
            LoadAuthList();
            images = new List<Image>();
            images.Add(global::NovaVision.Properties.Resources.EyeBlue);
            images.Add(global::NovaVision.Properties.Resources.EyeGrey);
            pic1.Image = images[1];
        }

        private void LoadAuthList()
        {
            if (AuthType == "0")
            {
                comboBox_authority.Items.Clear();
                List<string> objList = new List<string> { "操作员", "工程师", "管理员" };
                ComboBox.ObjectCollection items = comboBox_authority.Items;
                object[] items2 = objList.ToArray();
                items.AddRange(items2);
            }
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            try
            {
                if (sQLiteInfoBll.Exists("UserName", textBox_username.Text.Trim(), "UserInfo"))
                {
                    MessageBox.Show(@"该用户已存在.");
                }
                else if (textBox_username.Text.Trim() != "")
                {
                    if (textBox_pwd.Text.Trim() != "")
                    {
                        if (comboBox_authority.SelectedItem != null)
                        {
                            BaseClass.Authority.UserInfo userInfo = new();
                            userInfo.UserName = textBox_username.Text.Trim();
                            userInfo.Pwd = textBox_pwd.Text.Trim();
                            userInfo.Authority = comboBox_authority.Text.Trim();
                            userInfo.CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            sQLiteInfoBll.Add(userInfo, "UserInfo");
                            LogUtil.Log(userInfo.UserName + " _用户创建成功");
                            MessageBox.Show(userInfo.UserName + " _用户创建成功");
                            ClearComponent();
                        }
                        else
                        {
                            MessageBox.Show(@"请选择权限");
                        }
                    }
                    else
                    {
                        MessageBox.Show(@"请输入密码");
                    }
                }
                else
                {
                    MessageBox.Show(@"请输入用户名");
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogError(textBox_username.Text.Trim() + " _创建失败_" + ex.Message);
                MessageBox.Show(textBox_username.Text.Trim() + " _创建失败_" + ex.Message);
            }
        }

        private void ClearComponent()
        {
            textBox_username.Text = "";
            textBox_pwd.Text = "";
            comboBox_authority.Text = "";
            comboBox_authority.SelectedIndex = -1;
        }

        private void Frm_Register_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.DialogResult = DialogResult.OK;
        }

        private void pic1_Click(object sender, EventArgs e)
        {
            if (pic1.Image.Tag == null)
            {
                textBox_pwd.PasswordChar = '\0';
                pic1.Image = images[0];
                pic1.Image.Tag = new
                {
                    V = "blue"
                };
            }
            else
            {
                textBox_pwd.PasswordChar = '*';
                pic1.Image = images[1];
                pic1.Image.Tag = null;
            }
        }
    }
}
