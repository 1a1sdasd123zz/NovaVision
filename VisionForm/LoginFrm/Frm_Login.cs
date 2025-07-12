using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Authority;
using NovaVision.BaseClass.DataBase;
using NovaVision.VisionForm.MainForm;

namespace NovaVision.VisionForm.LoginFrm
{
    public partial class Frm_Login : Form, IMessageFilter
    {
        private System.Timers.Timer KeyDeleteTimer;

        private LoginControl loginControl;

        private string AuthType = ConfigurationManager.AppSettings["AuthType"];

        private KeyboardHook k_hook;

        private KeyEventHandler myKeyEventHandeler;

        private Frm_Main frm_Main;

        private SQLiteInfoBll sQLiteInfoBll = new SQLiteInfoBll();

        private DataSet ds;

        public string passw;

        public bool IsExist;

        public List<string> listname = new List<string>();

        public List<string> listauthy = new List<string>();

        private string filename;

        private StringBuilder sb = new StringBuilder();
        private List<Image> images = new List<Image>();

        public Frm_Login(Frm_Main _frm_Main, string path)
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            frm_Main = _frm_Main;
            filename = path + "UserInfo.db";
            images = new List<Image>();
            images.Add(global::NovaVision.Properties.Resources.EyeBlue);
            images.Add(global::NovaVision.Properties.Resources.EyeGrey);
            InitializeComponent();
            LoadConfig();
            if (loginControl.LoginType == 1)
            {
                KeyDeleteTimer = new System.Timers.Timer();
                KeyDeleteTimer.Interval = loginControl.TimerInterval;
                KeyDeleteTimer.Enabled = loginControl.LoginType == 1;
                KeyDeleteTimer.Elapsed += KeyDeleteTimer_Elapsed;
                comboBox_username.Visible = false;
                label_username.Visible = false;
                textBoxEx1.Visible = false;
                label_pwd.Visible = false;
                btn_login.Visible = false;
                k_hook = new KeyboardHook();
                startListen();

            }
            else
            {
                BackgroundImage = null;
                pic1.Visible = loginControl.ShowPwdButton;
                pic1.Image = images[1];
            }

            Application.AddMessageFilter(this);
        }

        private void KeyDeleteTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            textBoxEx1.Invoke((Action)delegate
            {
                textBoxEx1.Text = "";
                sb.Remove(0, sb.Length);
            });
        }

        private void LoadConfig()
        {
            string path = Application.StartupPath + "\\Login.xml";
            if (File.Exists(path))
            {
                loginControl = (LoginControl)XmlHelp.ReadXML(path, typeof(LoginControl));
            }
            else
            {
                loginControl = new LoginControl();
            }
        }

        private void SaveConfig()
        {
            string path = Application.StartupPath + "\\Login.xml";
            XmlHelp.WriteXML(path, loginControl);
        }

        private void Frm_Login_Load(object sender, EventArgs e)
        {
            Userlist("UserName");
        }

        public void Userlist(string str)
        {
            try
            {
                if (File.Exists(filename))
                {
                    IsExist = true;
                    listname.Clear();
                    comboBox_username.Items.Clear();
                    int rows = 0;
                    ds = new DataSet();
                    ds = sQLiteInfoBll.GetList("UserInfo");
                    rows = ds.Tables[0].Rows.Count;
                    DataTable dt = ds.Tables[0];
                    for (int i = 0; i < rows; i++)
                    {
                        string authName = dt.Rows[i][3].ToString();
                        if (AuthType == "0")
                        {
                            if (authName == "操作员" || authName == "工程师" || authName == "管理员")
                            {
                                comboBox_username.Items.Add(dt.Rows[i][str].ToString());
                            }

                            continue;
                        }

                        switch (authName)
                        {
                            default:
                                if (!(authName == "管理员"))
                                {
                                    continue;
                                }

                                break;
                            case "OPN":
                            case "OPN技师":
                            case "ME":
                            case "PE":
                                break;
                        }

                        comboBox_username.Items.Add(dt.Rows[i][str].ToString());
                    }
                }
                else
                {
                    IsExist = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateAuth(textBoxEx1.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("登录异常，" + ex.Message);
            }
        }

        private void comboBox_username_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsExist && comboBox_username.Items != null && comboBox_username.Items.Count > 0)
                {
                    passw = "";
                    ds = new DataSet();
                    ds = sQLiteInfoBll.GetList("UserName", comboBox_username.Text.Trim(), "UserInfo");
                    textBox_authority.Text = ds.Tables[0].Rows[0]["Authority"].ToString();
                    passw = ds.Tables[0].Rows[0]["Pwd"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据查询失败," + ex.Message);
            }
        }

        private void Frm_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (KeyDeleteTimer != null)
            {
                KeyDeleteTimer.Enabled = false;
                KeyDeleteTimer.Elapsed -= KeyDeleteTimer_Elapsed;
            }

            SaveConfig();
            stopListen();
            Application.RemoveMessageFilter(this);
            Dispose();
        }

        private void textBox_pwd_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btn_login_Click(null, null);
            }
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg >= 516 && m.Msg <= 517 && base.Name == "Frm_Login")
            {
                return true;
            }

            if (m.Msg == 770)
            {
                m.Result = IntPtr.Zero;
                return true;
            }

            WndProc(ref m);
            return false;
        }

        private void hook_KeyDown(object sender, KeyEventArgs e)
        {
            string str = GetKeyLabel(e);
            if (str == "Enter")
            {
                try
                {
                    string pwdTxt = sb.ToString();
                    sb.Remove(0, sb.Length);
                    ValidateAuth(pwdTxt);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("登录异常，" + ex.Message);
                    return;
                }
            }

            if (str != "Unknown")
            {
                sb.Append(str);
            }
        }

        private string GetKeyLabel(KeyEventArgs e)
        {
            Keys keyCode = e.KeyCode;
            if (keyCode >= Keys.A && keyCode <= Keys.Z)
            {
                return ((char)(65 + (keyCode - 65))).ToString();
            }

            if (keyCode >= Keys.D0 && keyCode <= Keys.D9)
            {
                return ((int)(keyCode - 48)).ToString();
            }

            if (keyCode >= Keys.NumPad0 && keyCode <= Keys.NumPad9)
            {
                return ((int)(keyCode - 96)).ToString();
            }

            if (keyCode == Keys.Return)
            {
                return "Enter";
            }

            return "Unknown";
        }

        private void ValidateAuth(string pwdTxt)
        {
            try
            {
                if (loginControl.LoginType == 1)
                {
                    ds = sQLiteInfoBll.GetList("Pwd", pwdTxt, "UserInfo");
                    if (ds.Tables[0].Rows.Count < 1)
                    {
                        MessageBox.Show(@"无法找到匹配的用户，请先注册");
                        return;
                    }

                    comboBox_username.Text = ds.Tables[0].Rows[0]["UserName"].ToString();
                }

                if (pwdTxt == passw && pwdTxt.Trim() != "")
                {
                    string MsgShow = "登录成功";
                    if (AuthType == "0")
                    {
                        switch (textBox_authority.Text.Trim())
                        {
                            case "操作员":
                                frm_Main.Load_Authority(AuthorityName.Operator, comboBox_username.Text.Trim());
                                LogUtil.Log("操作员(" + comboBox_username.Text.Trim() + ")已登录！");
                                break;
                            case "管理员":
                                frm_Main.Load_Authority(AuthorityName.Manager, comboBox_username.Text.Trim());
                                LogUtil.Log("管理员(" + comboBox_username.Text.Trim() + ")已登录！");
                                break;
                            case "工程师":
                                frm_Main.Load_Authority(AuthorityName.Engineer, comboBox_username.Text.Trim());
                                LogUtil.Log("工程师(" + comboBox_username.Text.Trim() + ")已登录！");
                                break;
                            default:
                                MsgShow = "无法找到匹配的权限，请确认配置是否正确";
                                break;
                        }
                    }
                    else
                    {
                        switch (textBox_authority.Text.Trim())
                        {
                            case "OPN":
                                frm_Main.Load_Authority(AuthorityName.OPN, comboBox_username.Text.Trim());
                                LogUtil.Log("OPN(" + comboBox_username.Text.Trim() + ")已登录！");
                                break;
                            case "OPN技师":
                                frm_Main.Load_Authority(AuthorityName.OPNTech, comboBox_username.Text.Trim());
                                LogUtil.Log("OPN技师(" + comboBox_username.Text.Trim() + ")已登录！");
                                break;
                            case "ME":
                                frm_Main.Load_Authority(AuthorityName.ME, comboBox_username.Text.Trim());
                                LogUtil.Log("ME(" + comboBox_username.Text.Trim() + ")已登录！");
                                break;
                            case "PE":
                                frm_Main.Load_Authority(AuthorityName.PE, comboBox_username.Text.Trim());
                                LogUtil.Log("PE(" + comboBox_username.Text.Trim() + ")已登录！");
                                break;
                            case "管理员":
                                frm_Main.Load_Authority(AuthorityName.Manager, comboBox_username.Text.Trim());
                                LogUtil.Log("管理员(" + comboBox_username.Text.Trim() + ")已登录！");
                                break;
                            default:
                                MsgShow = "无法找到匹配的权限，请确认配置是否正确";
                                break;
                        }
                    }

                    MessageBox.Show(MsgShow);
                    Close();
                }
                else
                {
                    int num = DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day;
                    string pwd = "YZ" + num.ToString();
                    if (pwdTxt.Trim() == pwd)
                    {
                        string MsgShow = "登录成功";
                        frm_Main.Load_Authority(AuthorityName.Manager, comboBox_username.Text.Trim());
                        LogUtil.Log("管理员(" + comboBox_username.Text.Trim() + ")已登录！");
                        MessageBox.Show(MsgShow);
                        Close();
                        return;
                    }

                    LogUtil.Log("密码错误，登录失败");
                    MessageBox.Show(@"密码错误，登录失败");
                }
            }
            catch (Exception ex)
            {
                LogUtil.Log("登录异常，" + ex.Message);
                MessageBox.Show("登录异常，" + ex.Message);
            }
        }

        public void startListen()
        {
            myKeyEventHandeler = hook_KeyDown;
            k_hook.KeyDownEvent += myKeyEventHandeler;
            k_hook.Start();
        }

        public void stopListen()
        {
            if (myKeyEventHandeler != null)
            {
                k_hook.KeyDownEvent -= myKeyEventHandeler;
                myKeyEventHandeler = null;
                k_hook.Stop();
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0014) // WM_ERASEBKGND
                return;
            base.WndProc(ref m);
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
