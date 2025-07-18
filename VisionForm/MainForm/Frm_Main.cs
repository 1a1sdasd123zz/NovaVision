using System;
using System.Drawing;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Authority;
using NovaVision.BaseClass.DataBase;
using NovaVision.BaseClass.VisionConfig;
using NovaVision.Hardware;
using NovaVision.VisionForm.AlgorithmFrm;
using NovaVision.VisionForm.CarameFrm;
using NovaVision.VisionForm.CommunicationFrm;
using NovaVision.VisionForm.LightFrm;
using NovaVision.VisionForm.LoginFrm;
using NovaVision.VisionForm.StationFrm;
using NovaVision.WorkFlow;
using WeifenLuo.WinFormsUI.Docking;
using Color = System.Drawing.Color;


namespace NovaVision.VisionForm.MainForm
{
    public partial class Frm_Main : Form, IMessageFilter
    {
        // 添加所有键盘相关消息常量
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;


        public JobCollection mJobs;
        public JobData mJobData;
        public MainWorkFlow mMainWorkFlow;

        public string CurrentAuthority;
        public AuthorityInfo MauthorityInfo;

        public string memoryConsume = "0MB";
        private static int iOperCount;//退出登录计时
        bool IsOnLine;

        System.Timers.Timer timerDelete = new();

        private SQLiteInfoBll mSQLiteInfoBll = new();

        #region 界面控件
        public Frm_Error frm_Error = Frm_Error.GetInstance(null, null);
        public Frm_Log frm_Log = Frm_Log.GetInstance(null, null);
        public Frm_2D frm_2D;
        public Frm_3D frm_3D;
        #endregion

        #region[定时监控操作]
        private DateTime _lastOperationTime = DateTime.Now;
        private const int MouseMoveThreshold = 5; // 鼠标移动阈值，单位：像素
        private const int InactivityTimeout = 3 * 60 * 1000; // 三分钟的毫秒数
        private System.Timers.Timer _inactivityTimer;
        private Point _lastMousePosition;
        private const int CheckInterval = 5000; // 定时器检查间隔，单位：毫秒
        #endregion

        private string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();


        public Frm_Main()
        {
            InitializeComponent();
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            if (!HslCommunication.Authorization.SetAuthorizationCode("cc792fa4-0c45-4748-a1d9-a18db8c5c3ab"))
            {
                MessageBox.Show(@"通信组件授权失败！请联系厂商！");
                string logmsg = "PLC 通讯组件授权失败";
                MessageBox.Show(logmsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Frm_Splash frm_Splash = new();
            frm_Splash.Show();
            frm_Splash.lbl_Splash.Text = "系统启动中……";
            frm_Splash.lbl_Splash.Refresh();
            // 强制创建控件句柄
            var unused = frm_Error.Handle;
            unused = frm_Log.Handle;

            LogUtil.path = AppDomain.CurrentDomain.BaseDirectory + "Project\\Log";
            LogUtil.TextBoxInfo = frm_Log.txt_Info;
            LogUtil.TextBoxError = frm_Error.txt_Error;
            frm_Splash.lbl_Splash.Text = "作业加载中……";
            mJobs = JobCollection.Instance;
            frm_Splash.lbl_Splash.Text = "作业加载完成";
            frm_Splash.lbl_Splash.Refresh();

            mJobs.JobChangedEvent += Jobs_JobChangedEvent;
            if (mJobs.Jobs.Count > 0)
            {
                if (mJobs.CurrentID > 0)
                {
                    mJobData = mJobs.Jobs[mJobs.CurrentName];
                    frm_Splash.lbl_Splash.Text = "硬件加载中……";
                    frm_Splash.lbl_Splash.Refresh();
                    mJobData.InitHardWare();
                    frm_Splash.lbl_Splash.Text = "硬件加载完成";
                    frm_Splash.lbl_Splash.Refresh();
                    mJobData.RegisterEvents();
                    toolStrip_JobName.Text = $"型号：{JobCollection.Instance.GetCurrentExplain()}";
                }
                else
                {
                    mJobData = new JobData();
                    toolStrip_JobName.Text = "型号：无";
                }
            }
            else
            {
                mJobData = new JobData();
                toolStrip_JobName.Text = "型号：无";
            }
            CurrentAuthority = "空";

            frm_Splash.lbl_Splash.Text = "初始化流程……";
            mMainWorkFlow = new MainWorkFlow();
            mMainWorkFlow.InitWorkFlow(mJobData);
            frm_Splash.lbl_Splash.Text = "流程初始化完成";

            if (mJobData.mProductInfo.IsEnableDp)
            {
                frm_Splash.lbl_Splash.Text = "加载深度学习模型……";
                var __ = DnnSingleton.Instance;
                frm_Splash.lbl_Splash.Text = "深度学习加载完成……";
            }


            Text = "Vision System(Ver" + AssemblyVersion + ")";
            frm_Splash.lbl_Splash.Text = "初始化界面……";
            // 显示主窗体后标记控件就绪
            this.Shown += (_, args) =>
            {
                LogUtil.SetControlsReady();
            };
            Show_Frm();
            frm_Splash.lbl_Splash.Text = "界面初始化完成";
            Load_Authority(AuthorityName.Empty, CurrentAuthority);

            timerDelete.Enabled = true;
            timerDelete.Interval = 1000.0;
            timerDelete.Elapsed += TimerState_Elapsed;
            timerDelete.Start();


            MultiLanguage.LoadLanguage(this, ConfigurationManager.AppSettings["Language"]);
            string IsOnLine = ConfigurationManager.AppSettings["IsOnLine"];
            if (IsOnLine.ToLower() == "true")
            {
                this.IsOnLine = false;//取反
                系统在线ToolStripMenuItem_Click(null, null);
            }
            else
            {
                this.IsOnLine = true;//取反
                系统在线ToolStripMenuItem_Click(null, null);
            }

            SetupActivityMonitoring();
            frm_Splash.Close();
            this.WindowState = FormWindowState.Maximized;
        }


        #region[定时监控有无操作电脑]
        private void SetupActivityMonitoring()
        {
            // 启用消息过滤监听整个应用程序的输入
            Application.AddMessageFilter(this);
            _lastMousePosition = Cursor.Position;

            _inactivityTimer = new System.Timers.Timer(CheckInterval); // 每 5 秒检查一次
            _inactivityTimer.Elapsed += InactivityTimer_Elapsed;
            _inactivityTimer.Start();
        }

        public bool PreFilterMessage(ref Message m)
        {
            const int WM_MOUSEMOVE = 0x0200;

            if (m.Msg == WM_MOUSEMOVE)
            {
                var currentPos = Cursor.Position;
                var deltaX = Math.Abs(currentPos.X - _lastMousePosition.X);
                var deltaY = Math.Abs(currentPos.Y - _lastMousePosition.Y);

                if (deltaX > MouseMoveThreshold || deltaY > MouseMoveThreshold)
                {
                    _lastOperationTime = DateTime.Now;
                    _lastMousePosition = currentPos;
                }
            }

            // 捕获所有键盘消息（包括 ALT 组合键）
            if (m.Msg == WM_KEYDOWN ||
                m.Msg == WM_KEYUP ||
                m.Msg == WM_SYSKEYDOWN ||
                m.Msg == WM_SYSKEYUP)
            {
                _lastOperationTime = DateTime.Now;
            }

            return false; // 允许消息继续传递
        }

        private void InactivityTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                TimeSpan elapsed = DateTime.Now - _lastOperationTime;
                if (elapsed.TotalMilliseconds > InactivityTimeout)
                {
                    if (CurrentAuthority != "空")
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke((Action)delegate
                            {
                                this.toolStrip_Logout_ButtonClick(null, null);
                            });
                        }
                        else
                        {
                            this.toolStrip_Logout_ButtonClick(null, null);
                        }
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        #endregion

        private void TimerState_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                timerDelete.Enabled = false;
                iOperCount++;
                if (iOperCount == 180)
                {
                    iOperCount = 0;
                    if (CurrentAuthority != "空")
                    {
                        Invoke((Action)delegate
                        {
                            this.toolStrip_Logout_ButtonClick(null, null);
                        });
                    }
                }
                UpdateMemoryConsume();
                timerDelete.Enabled = true;
            }
            catch
            {
                // ignored
            }
        }

        public void MTCPClient_Connected(bool isConnected)
        {
            Invoke((Action)delegate
            {
                if (!isConnected)
                {
                    toolStrip_CommState.Text = "通讯：Disconnect";
                    toolStrip_CommState.ForeColor = Color.Red;
                }
                else
                {
                    toolStrip_CommState.Text = "通讯：Connect";
                    toolStrip_CommState.ForeColor = Color.Green;
                }
            });
        }

        private void Jobs_JobChangedEvent(int id, string name)
        {
            if (mMainWorkFlow == null)
            {

                mJobData = mJobs.Jobs[mJobs.CurrentName];
                mMainWorkFlow = new MainWorkFlow();
                mMainWorkFlow.InitWorkFlow(mJobData);
            }
            else
            {
                mJobData = mJobs.Jobs[name];
                mMainWorkFlow.InitWorkFlow(mJobData);
            }
            LogUtil.Log($"当前切换型号 {mJobs.CurrentID},名称 {mJobs.CurrentName}");
            Invoke((Action)delegate
            {
                if (IsOnLine)
                {
                    toolStrip_State.Text = "系统：OnLine";
                    toolStrip_State.ForeColor = Color.Green;
                }
                else
                {
                    toolStrip_State.Text = "系统：OffLine";
                    toolStrip_State.ForeColor = Color.Red;
                }
                toolStrip_JobName.Text = $"型号：{JobCollection.Instance.GetCurrentExplain()}";
                mJobData.mUIControl.ResumeLayout();
                Show_Frm();
            });
            ClearMemory();
        }

        public void Load_Authority(AuthorityName authorityName, string name)
        {
            try
            {
                switch (authorityName)
                {
                    case AuthorityName.Empty:
                        CurrentAuthority = "空";
                        toolStrip_Auth.Text = "权限";
                        toolStrip_User.Text = name;
                        UpdataAuthority(CurrentAuthority);
                        break;
                    case AuthorityName.OPN:
                        CurrentAuthority = "OPN";
                        toolStrip_Auth.Text = "OPN";
                        toolStrip_User.Text = name;
                        UpdataAuthority(CurrentAuthority);
                        break;
                    case AuthorityName.OPNTech:
                        CurrentAuthority = "OPN技师";
                        toolStrip_Auth.Text = "OPN技师";
                        toolStrip_User.Text = name;
                        UpdataAuthority(CurrentAuthority);
                        break;
                    case AuthorityName.ME:
                        CurrentAuthority = "ME";
                        toolStrip_Auth.Text = "ME";
                        toolStrip_User.Text = name;
                        UpdataAuthority(CurrentAuthority);
                        break;
                    case AuthorityName.PE:
                        CurrentAuthority = "PE";
                        toolStrip_Auth.Text = "PE";
                        toolStrip_User.Text = name;
                        UpdataAuthority(CurrentAuthority);
                        break;
                    case AuthorityName.Manager:
                        CurrentAuthority = "管理员";
                        toolStrip_Auth.Text = "管理员";
                        toolStrip_User.Text = name;
                        UpdataAuthority(CurrentAuthority);
                        break;
                    case AuthorityName.Engineer:
                        CurrentAuthority = "工程师";
                        toolStrip_Auth.Text = "工程师";
                        toolStrip_User.Text = name;
                        UpdataAuthority(CurrentAuthority);
                        break;
                    case AuthorityName.Operator:
                        CurrentAuthority = "操作员";
                        toolStrip_Auth.Text = "操作员";
                        toolStrip_User.Text = name;
                        UpdataAuthority(CurrentAuthority);
                        break;
                    default:
                        CurrentAuthority = "空";
                        toolStrip_Auth.Text = "权限";
                        toolStrip_User.Text = name;
                        UpdataAuthority(CurrentAuthority);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("登录失败，" + ex.Message);
            }
        }

        public void UpdataAuthority(string currentauthority)
        {
            try
            {
                MauthorityInfo = AuthorityInfo.ReadXML(mJobData.AuthorityFilePath);
                系统配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].SystemSetModule;
                产品型号配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].JobConfig;
                文件参数配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].StationSet;
                //参数配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].SystemPar;
                权限配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].AuthoritySet;
                //检测项参数配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].InspectParamsSet;
                系统在线ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].SystemState;
                用户管理ToolStripMenuItem.Enabled = UserAuth(currentauthority);
                //图片回放ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].PicPlayBack;
                通讯模块ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].CommModule;
                //通讯类型ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].CommType;
                //通讯配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].CommSet;
                硬件模块ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].CameraModule;
                //cameraToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].CameraSet;
                算法模块ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].AlgorithmModule;
                //AlgCognex.Enabled = MauthorityInfo.Dicauth[currentauthority].AlgorithmVpp;
                //算法输入配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].AlgorithmParam;
                //MES模块ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].MesModule;
                //mes数据配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].MesData;
                //mes通讯配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].MesParam;
                //toolStrip_Manual.Visible = MauthorityInfo.Dicauth[currentauthority].MesData_Manual;
                //数据管理ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].DataModule;
                //数据库ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].DataBaseSet;
                视图ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].ViewModule;
                视图适应ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].ViewAdaptation;
                tsmi_2D.Enabled = MauthorityInfo.Dicauth[currentauthority].Display2D;
                //tsmi_3D.Enabled = MauthorityInfo.Dicauth[currentauthority].Display3D;
                Refresh();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public bool UserAuth(string currentauthority)
        {
            bool rtnBool = MauthorityInfo.Dicauth[currentauthority].UserManagement;
            //if (currentauthority == "空")
            //{
            //    string path = mJobData.mSystemConfigData.DataBasePath;
            //    string filename = path + "UserInfo.db";
            //    if (!File.Exists(filename))
            //    {
            //        rtnBool = true;
            //    }
            //    else
            //    {
            //        DataSet ds = mSQLiteInfoBll.GetList("UserInfo");
            //        List<VisionForm.LoginFrm.UserInfo> list = DataBaseHelper.DataTableToList<VisionForm.LoginFrm.UserInfo>(ds.Tables[0]);
            //        if (list == null || list.Count < 1)
            //        {
            //            rtnBool = true;
            //        }
            //    }
            //}
            //if (rtnBool)
            //{
            //    系统配置ToolStripMenuItem.Enabled = true;
            //}
            return rtnBool;
        }

        private void UpdateMemoryConsume()
        {
            memoryConsume = GetMemory();
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    tssl_MemoryConsume.Text = memoryConsume;
                    tssl_MemoryConsume.ForeColor = Color.Red;
                });
            }
            catch (Exception)
            {
            }
        }

        private static string GetMemory()
        {
            var process = Process.GetCurrentProcess();
            return $"{process.WorkingSet64 / 1024.0 / 1024.0:F2} MB";
        }

        [DllImport("kernel32.dll")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        private int CheckJobIsExist()
        {
            if (mJobData.ID == -1)
            {
                MessageBox.Show(@"当前作业号为空，请设置一个型号再进行相关操作！", "警告", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
                return 1;
            }
            if (IsOnLine)
            {
                MessageBox.Show(@"系统在线不能进行相关操作，请将系统置为离线状态！", "警告", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
                return 2;
            }
            return 0;
        }

        #region[窗体适应]


        public void Show_Frm()
        {
            try
            {
                dockPanel_Main.DockLeftPortion = mJobData.mUIControl.mAppConfig.DS_DockLeftPortion;
                dockPanel_Main.DockRightPortion = mJobData.mUIControl.mAppConfig.DS_DockRighttPortion;
                dockPanel_Main.DockBottomPortion = mJobData.mUIControl.mAppConfig.DS_DockBottomPortion;
                Show_Frm_2D();
                Show_Frm_3D();
                ////Show_Frm_Statistics();
                ////Show_Frm_Data();
                Show_Frm_Log();
                Show_Frm_Error();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Show_Frm_2D()
        {
            try
            {
                frm_2D = Frm_2D.GetInstance(this, mJobData.mUIControl);
                if (mJobData.mUIControl.mAppConfig.DS_Frm_2D_Dsiplay)
                {
                    frm_2D.Show(dockPanel_Main, mJobData.mUIControl.mAppConfig.DS_Frm_2D);
                    frm_2D.HideOnClose = true;
                    tsmi_2D.Checked = true;
                }
                else
                {
                    frm_2D.Visible = false;
                }
            }
            catch (Exception)
            {
            }
        }

        public void Show_Frm_3D()
        {
            try
            {
                frm_3D = Frm_3D.GetInstance(this, mJobData.mUIControl);
                if (mJobData.mUIControl.mAppConfig.DS_Frm_3D_Dsiplay && mJobData.mUIControl.mAppConfig.DS_Frm_2D_Dsiplay)
                {
                    frm_3D.Show(frm_2D.Pane, DockAlignment.Left, mJobData.mUIControl.mAppConfig.DS_3D_Size);
                    frm_3D.HideOnClose = true;
                    tsmi_3D.Checked = true;
                }
                else if (mJobData.mUIControl.mAppConfig.DS_Frm_3D_Dsiplay && !mJobData.mUIControl.mAppConfig.DS_Frm_2D_Dsiplay)
                {
                    frm_3D.Show(dockPanel_Main, mJobData.mUIControl.mAppConfig.DS_Frm_3D);
                    frm_3D.HideOnClose = true;
                    tsmi_3D.Checked = true;
                }
                else
                {
                    frm_3D.Visible = false;
                }
            }
            catch (Exception)
            {
            }
        }

        public void Show_Frm_Data()
        {
            //frm_Data = Frm_Data.GetInstance(this, mJobData.mUIControl.mAppConfig);
            //MultiLanguage.LoadLanguage(frm_Data, MultiLanguage.GetDefaultLanguage());
            //frm_Data.Show(dockPanel_Main, mJobData.mUIControl.mAppConfig.DS_Frm_Data);
            //frm_Data.HideOnClose = true;
            //tsmi_Data.Checked = true;
        }

        public void Show_Frm_Statistics()
        {
            //frm_Statistics = Frm_Statistics.GetInstance(this, mJobData.mUIControl.mAppConfig);
            //frm_Statistics.Show(dockPanel_Main, mJobData.mUIControl.mAppConfig.DS_Frm_Statistics);
            //frm_Statistics.HideOnClose = true;
            //tsmi_Statistics.Checked = true;
        }

        public void Show_Frm_Error()
        {
            frm_Error = Frm_Error.GetInstance(this, mJobData.mUIControl.mAppConfig);
            frm_Error.Show(dockPanel_Main, mJobData.mUIControl.mAppConfig.DS_Frm_Error);
            frm_Error.HideOnClose = true;
            tsmi_Error.Checked = true;
        }
        public void Show_Frm_Log()
        {
            frm_Log = Frm_Log.GetInstance(this, mJobData.mUIControl.mAppConfig);
            frm_Log.Show(dockPanel_Main, mJobData.mUIControl.mAppConfig.DS_Frm_Log);
            frm_Log.HideOnClose = true;
            tsmi_Log.Checked = true;
        }
        private void tsmi_2D_Click(object sender, EventArgs e)
        {
            if (CheckJobIsExist() == 0)
            {
                Show_Frm_2D();
            }
        }
        private void tsmi_3D_Click(object sender, EventArgs e)
        {
            if (CheckJobIsExist() == 0)
            {
                Show_Frm_3D();
            }
        }
        private void tsmi_Log_Click(object sender, EventArgs e)
        {
            Show_Frm_Log();
        }
        private void tsmi_Error_Click(object sender, EventArgs e)
        {
            Show_Frm_Error();
        }
        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(@"是否确认关闭软件", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    e.Cancel = false;
                    mJobData.UnRegisterEvents();
                    List<string> keys = CameraOperator.camera2DCollection.ListKeys;
                    for (int j = 0; j < keys.Count; j++)
                    {
                        if (CameraOperator.camera2DCollection[keys[j]] != null)
                        {
                            CameraOperator.camera2DCollection[keys[j]].CloseCamera();
                        }
                    }


                    timerDelete.Enabled = false;
                    timerDelete.Elapsed -= TimerState_Elapsed;

                    LogUtil.Log("软件关闭");
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch { }
        }


        private void toolStrip_Logout_ButtonClick(object sender, EventArgs e)
        {
            CurrentAuthority = "空";
            Load_Authority(AuthorityName.Empty, CurrentAuthority);
        }

        private void 登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Login frm_Login = new(this, mJobData.mSystemConfigData.DataBasePath);
            MultiLanguage.LoadLanguage(frm_Login, MultiLanguage.GetDefaultLanguage());
            frm_Login.ShowDialog();
        }

        private void 产品型号配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_JobConfig frmJobConfig = new Frm_JobConfig(mJobs,IsOnLine);
            MultiLanguage.LoadLanguage(frmJobConfig, MultiLanguage.GetDefaultLanguage());
            frmJobConfig.ShowDialog();
        }

        private void 工位配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CheckJobIsExist() == 0)
            {
                Frm_Station frm_Station = new Frm_Station(this.mJobData, mMainWorkFlow);
                MultiLanguage.LoadLanguage(frm_Station, MultiLanguage.GetDefaultLanguage());
                frm_Station.ShowDialog();
            }
        }

        private void 文件参数配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_FileParam frm_Param = new Frm_FileParam(mJobData.mSystemConfigData);
            MultiLanguage.LoadLanguage(frm_Param, MultiLanguage.GetDefaultLanguage());
            frm_Param.ShowDialog();
        }
        private void 权限配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Authority frm_Authority = new Frm_Authority(MauthorityInfo, CurrentAuthority, mJobData.AuthorityFilePath);
            MultiLanguage.LoadLanguage(frm_Authority, MultiLanguage.GetDefaultLanguage());
            frm_Authority.ShowDialog();
        }

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_UserManage frm_UserManage = new Frm_UserManage(mJobData.mSystemConfigData.DataBasePath, MauthorityInfo, CurrentAuthority);
            MultiLanguage.LoadLanguage(frm_UserManage, MultiLanguage.GetDefaultLanguage());
            if (frm_UserManage.ShowDialog() == DialogResult.OK)
            {
                UpdataAuthority(CurrentAuthority);
            }
        }

        private void 系统在线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsOnLine = !IsOnLine;
            if (IsOnLine)
            {
                toolStrip_State.Text = "系统：OnLine";
                toolStrip_State.ForeColor = Color.Green;
                系统在线ToolStripMenuItem.Text = "系统离线";
            }
            else
            {
                toolStrip_State.Text = "系统：OffOnLine";
                toolStrip_State.ForeColor = Color.Red;
                系统在线ToolStripMenuItem.Text = "系统在线";
            }
            mMainWorkFlow.OnLineStateChange(IsOnLine);
        }

        private void tcpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckJobIsExist() == 0)
            {
                TcpConfigForm _tcpConfigForm = new TcpConfigForm(mJobData.mCommBaseInfo, mJobData.CommDeviceInfoPath);
                MultiLanguage.LoadLanguage(_tcpConfigForm, MultiLanguage.GetDefaultLanguage());
                _tcpConfigForm.ShowDialog();
            }
        }

        private void 通讯配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckJobIsExist() == 0)
            {
                CommInOutConfigForm frm_commInOut = new CommInOutConfigForm(mJobData);
                MultiLanguage.LoadLanguage(frm_commInOut, MultiLanguage.GetDefaultLanguage());
                frm_commInOut.ShowDialog();
            }
        }


        private void Camera2DSettings_TSMItem_Click(object sender, EventArgs e)
        {
            if (CheckJobIsExist() == 0)
            {
                FrmCamera2DSetting frm_2DSetting = new FrmCamera2DSetting(mJobData.mCameraInfo, mJobData.CameraDeviceInfoPath);
                MultiLanguage.LoadLanguage(frm_2DSetting, MultiLanguage.GetDefaultLanguage());
                frm_2DSetting.ShowDialog();
            }
        }

        private void Camera2DLinearSettings_TSMItem_Click(object sender, EventArgs e)
        {
            if (CheckJobIsExist() == 0)
            {
                FrmCameraLinear2DSetting frmCameraLinear2DSetting = new FrmCameraLinear2DSetting(mJobData.mCameraInfo, mJobData.CameraDeviceInfoPath);
                MultiLanguage.LoadLanguage(frmCameraLinear2DSetting, MultiLanguage.GetDefaultLanguage());
                frmCameraLinear2DSetting.ShowDialog();
            }
        }
        private void Camera3DSettings_TSMItem_Click(object sender, EventArgs e)
        {
            if (CheckJobIsExist() == 0)
            {
                FrmCamera3DSetting frmCamera3DSetting = new FrmCamera3DSetting(mJobData.mCameraInfo, mJobData.CameraDeviceInfoPath);
                MultiLanguage.LoadLanguage(frmCamera3DSetting, MultiLanguage.GetDefaultLanguage());
                frmCamera3DSetting.ShowDialog();
            }
        }


        private void GigEHikrobot2DLineScan_TsmItem_Click(object sender, EventArgs e)
        {
            FrmCameraHikLinear2DSetting frmCameraHikLinear2DSetting = new FrmCameraHikLinear2DSetting(null);
            MultiLanguage.LoadLanguage(frmCameraHikLinear2DSetting, MultiLanguage.GetDefaultLanguage());
            frmCameraHikLinear2DSetting.ShowDialog();
        }

        private void GigEDahua2DLineScan_TsmItem_Click(object sender, EventArgs e)
        {
            FrmCameraDahuaLinear2DSetting frmCameraDahuaLinear2DSetting = new FrmCameraDahuaLinear2DSetting(null);
            MultiLanguage.LoadLanguage(frmCameraDahuaLinear2DSetting, MultiLanguage.GetDefaultLanguage());
            frmCameraDahuaLinear2DSetting.ShowDialog();
        }

        private void CameraDeploy_TSMItem_Click(object sender, EventArgs e)
        {
            FrmCameraDeploySetting frmCameraDeploy = new FrmCameraDeploySetting();
            MultiLanguage.LoadLanguage(frmCameraDeploy, MultiLanguage.GetDefaultLanguage());
            frmCameraDeploy.ShowDialog();
        }

        private void 康视达光源ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTest frm_test = new FrmTest();
            MultiLanguage.LoadLanguage(frm_test, MultiLanguage.GetDefaultLanguage());
            frm_test.ShowDialog();
        }

        private void 配置界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Light frm_light = new Frm_Light(mJobData);
            MultiLanguage.LoadLanguage(frm_light, MultiLanguage.GetDefaultLanguage());
            frm_light.ShowDialog();
        }
        private void 硬件部署配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHardwareDeployment frmHardwareDeployment = new FrmHardwareDeployment();
            MultiLanguage.LoadLanguage(frmHardwareDeployment, MultiLanguage.GetDefaultLanguage());
            frmHardwareDeployment.ShowDialog();
        }

        private void 算法配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlgConfigForm algForm = AlgConfigForm.CreateInstance(mJobData, this.IsOnLine);
            MultiLanguage.LoadLanguage(algForm, MultiLanguage.GetDefaultLanguage());
            algForm.Show();
        }

        private void 定位参数配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //iOperCount = 0;
            //定位参数设置 frm = new 定位参数设置(mJobData, mWorkFlow);
            //MultiLanguage.LoadLanguage(frm, MultiLanguage.GetDefaultLanguage());
            //frm.ShowDialog();
        }

        private void 标定配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CalibConfigForm calib_frm = new CalibConfigForm(mJobData);
            calib_frm.ShowDialog();
        }
        private void 视图适应ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mJobData.mUIControl.ResumeLayout();
            Show_Frm();
            Refresh();
        }

        private void Window_2DChange_Click(object sender, EventArgs e)
        {
            if (CheckJobIsExist() == 0)
            {
                Frm_DisplaySet frm_DisplaySet = new Frm_DisplaySet(mJobData.mUIControl, "2D");
                MultiLanguage.LoadLanguage(frm_DisplaySet, MultiLanguage.GetDefaultLanguage());
                frm_DisplaySet.ShowDialog();
            }
        }

        private void Window_3DChange_Click(object sender, EventArgs e)
        {
            if (CheckJobIsExist() == 0)
            {
                Frm_DisplaySet frm_DisplaySet = new Frm_DisplaySet(mJobData.mUIControl, "3D");
                MultiLanguage.LoadLanguage(frm_DisplaySet, MultiLanguage.GetDefaultLanguage());
                frm_DisplaySet.ShowDialog();
            }
        }

        private void Frm_Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue > 0)
            {
                iOperCount = 0;
            }
        }

        private void chineseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurationManager.AppSettings["Language"] = Language.Chinese.ToString();
            MultiLanguage.LoadLanguage(this, MultiLanguage.GetDefaultLanguage());
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurationManager.AppSettings["Language"] = Language.English.ToString();
            MultiLanguage.LoadLanguage(this, MultiLanguage.GetDefaultLanguage());
        }


        private void Frm_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            if (MainWorkFlow.iStart)
            {
                MainWorkFlow.iStart = false;
                toolStripMenuItem1.Text = "开始";
            }
            else
            {
                MainWorkFlow.iStart = true;
                toolStripMenuItem1.Text = "停止";
            }
        }
    }
}
