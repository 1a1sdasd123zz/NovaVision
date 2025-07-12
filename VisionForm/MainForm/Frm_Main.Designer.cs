using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace NovaVision.VisionForm.MainForm
{
    partial class Frm_Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.登录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.产品型号配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工位配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文件参数配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.权限配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统在线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.通讯模块ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.通讯类型TcpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModbustoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tcpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.通讯配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.硬件模块ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.相机配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Camera2DSettings_TSMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Camera2DLinearSettings_TSMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Camera3DSettings_TSMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CameraDeploy_TSMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.线扫配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GigEHikrobot2DLineScan_TsmItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GigEDahua2DLineScan_TsmItem = new System.Windows.Forms.ToolStripMenuItem();
            this.光源配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.配置界面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试界面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.康视达光源ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.硬件部署配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.算法模块ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.算法配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.标定配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视图适应ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_2D = new System.Windows.Forms.ToolStripMenuItem();
            this.Window_2DChange = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_3D = new System.Windows.Forms.ToolStripMenuItem();
            this.Window_3DChange = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Log = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Error = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip_Auth = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_User = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_Logout = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStrip_JobName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_State = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_CommState = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_MemoryConsume = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.chineseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dockPanel_Main = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vS2015BlueTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.SkyBlue;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.登录ToolStripMenuItem,
            this.系统配置ToolStripMenuItem,
            this.通讯模块ToolStripMenuItem,
            this.硬件模块ToolStripMenuItem,
            this.算法模块ToolStripMenuItem,
            this.视图ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1717, 36);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 登录ToolStripMenuItem
            // 
            this.登录ToolStripMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("登录ToolStripMenuItem.BackgroundImage")));
            this.登录ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.登录ToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.登录ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("登录ToolStripMenuItem.Image")));
            this.登录ToolStripMenuItem.Name = "登录ToolStripMenuItem";
            this.登录ToolStripMenuItem.Size = new System.Drawing.Size(86, 30);
            this.登录ToolStripMenuItem.Text = "登录";
            this.登录ToolStripMenuItem.Click += new System.EventHandler(this.登录ToolStripMenuItem_Click);
            // 
            // 系统配置ToolStripMenuItem
            // 
            this.系统配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.产品型号配置ToolStripMenuItem,
            this.工位配置ToolStripMenuItem,
            this.文件参数配置ToolStripMenuItem,
            this.权限配置ToolStripMenuItem,
            this.用户管理ToolStripMenuItem,
            this.系统在线ToolStripMenuItem});
            this.系统配置ToolStripMenuItem.Enabled = false;
            this.系统配置ToolStripMenuItem.Image = global::NovaVision.Properties.Resources.系统配置;
            this.系统配置ToolStripMenuItem.Name = "系统配置ToolStripMenuItem";
            this.系统配置ToolStripMenuItem.Size = new System.Drawing.Size(122, 30);
            this.系统配置ToolStripMenuItem.Text = "系统配置";
            // 
            // 产品型号配置ToolStripMenuItem
            // 
            this.产品型号配置ToolStripMenuItem.Name = "产品型号配置ToolStripMenuItem";
            this.产品型号配置ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.产品型号配置ToolStripMenuItem.Text = "产品型号配置";
            this.产品型号配置ToolStripMenuItem.Click += new System.EventHandler(this.产品型号配置ToolStripMenuItem_Click);
            // 
            // 工位配置ToolStripMenuItem
            // 
            this.工位配置ToolStripMenuItem.Name = "工位配置ToolStripMenuItem";
            this.工位配置ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.工位配置ToolStripMenuItem.Text = "工位配置";
            this.工位配置ToolStripMenuItem.Click += new System.EventHandler(this.工位配置ToolStripMenuItem_Click);
            // 
            // 文件参数配置ToolStripMenuItem
            // 
            this.文件参数配置ToolStripMenuItem.Name = "文件参数配置ToolStripMenuItem";
            this.文件参数配置ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.文件参数配置ToolStripMenuItem.Text = "文件参数配置";
            this.文件参数配置ToolStripMenuItem.Click += new System.EventHandler(this.文件参数配置ToolStripMenuItem_Click);
            // 
            // 权限配置ToolStripMenuItem
            // 
            this.权限配置ToolStripMenuItem.Name = "权限配置ToolStripMenuItem";
            this.权限配置ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.权限配置ToolStripMenuItem.Text = "权限配置";
            this.权限配置ToolStripMenuItem.Click += new System.EventHandler(this.权限配置ToolStripMenuItem_Click);
            // 
            // 用户管理ToolStripMenuItem
            // 
            this.用户管理ToolStripMenuItem.Name = "用户管理ToolStripMenuItem";
            this.用户管理ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.用户管理ToolStripMenuItem.Text = "用户管理";
            this.用户管理ToolStripMenuItem.Click += new System.EventHandler(this.用户管理ToolStripMenuItem_Click);
            // 
            // 系统在线ToolStripMenuItem
            // 
            this.系统在线ToolStripMenuItem.Name = "系统在线ToolStripMenuItem";
            this.系统在线ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.系统在线ToolStripMenuItem.Text = "系统在线";
            this.系统在线ToolStripMenuItem.Click += new System.EventHandler(this.系统在线ToolStripMenuItem_Click);
            // 
            // 通讯模块ToolStripMenuItem
            // 
            this.通讯模块ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.通讯类型TcpToolStripMenuItem,
            this.通讯配置ToolStripMenuItem});
            this.通讯模块ToolStripMenuItem.Enabled = false;
            this.通讯模块ToolStripMenuItem.Image = global::NovaVision.Properties.Resources.通讯;
            this.通讯模块ToolStripMenuItem.Name = "通讯模块ToolStripMenuItem";
            this.通讯模块ToolStripMenuItem.Size = new System.Drawing.Size(122, 30);
            this.通讯模块ToolStripMenuItem.Text = "通讯模块";
            // 
            // 通讯类型TcpToolStripMenuItem
            // 
            this.通讯类型TcpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModbustoolStripMenuItem,
            this.tcpToolStripMenuItem});
            this.通讯类型TcpToolStripMenuItem.Name = "通讯类型TcpToolStripMenuItem";
            this.通讯类型TcpToolStripMenuItem.Size = new System.Drawing.Size(182, 34);
            this.通讯类型TcpToolStripMenuItem.Text = "通讯类型";
            // 
            // ModbustoolStripMenuItem
            // 
            this.ModbustoolStripMenuItem.Name = "ModbustoolStripMenuItem";
            this.ModbustoolStripMenuItem.Size = new System.Drawing.Size(213, 34);
            this.ModbustoolStripMenuItem.Text = "ModbusTcp";
            // 
            // tcpToolStripMenuItem
            // 
            this.tcpToolStripMenuItem.Name = "tcpToolStripMenuItem";
            this.tcpToolStripMenuItem.Size = new System.Drawing.Size(213, 34);
            this.tcpToolStripMenuItem.Text = "Tcp";
            this.tcpToolStripMenuItem.Click += new System.EventHandler(this.tcpToolStripMenuItem_Click);
            // 
            // 通讯配置ToolStripMenuItem
            // 
            this.通讯配置ToolStripMenuItem.Name = "通讯配置ToolStripMenuItem";
            this.通讯配置ToolStripMenuItem.Size = new System.Drawing.Size(182, 34);
            this.通讯配置ToolStripMenuItem.Text = "通讯配置";
            this.通讯配置ToolStripMenuItem.Click += new System.EventHandler(this.通讯配置ToolStripMenuItem_Click);
            // 
            // 硬件模块ToolStripMenuItem
            // 
            this.硬件模块ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.相机配置ToolStripMenuItem,
            this.线扫配置ToolStripMenuItem,
            this.光源配置ToolStripMenuItem,
            this.硬件部署配置ToolStripMenuItem});
            this.硬件模块ToolStripMenuItem.Enabled = false;
            this.硬件模块ToolStripMenuItem.Image = global::NovaVision.Properties.Resources.相机模块;
            this.硬件模块ToolStripMenuItem.Name = "硬件模块ToolStripMenuItem";
            this.硬件模块ToolStripMenuItem.Size = new System.Drawing.Size(122, 30);
            this.硬件模块ToolStripMenuItem.Text = "硬件模块";
            // 
            // 相机配置ToolStripMenuItem
            // 
            this.相机配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Camera2DSettings_TSMItem,
            this.Camera2DLinearSettings_TSMItem,
            this.Camera3DSettings_TSMItem,
            this.CameraDeploy_TSMItem});
            this.相机配置ToolStripMenuItem.Name = "相机配置ToolStripMenuItem";
            this.相机配置ToolStripMenuItem.Size = new System.Drawing.Size(254, 34);
            this.相机配置ToolStripMenuItem.Text = "相机配置";
            // 
            // Camera2DSettings_TSMItem
            // 
            this.Camera2DSettings_TSMItem.Name = "Camera2DSettings_TSMItem";
            this.Camera2DSettings_TSMItem.Size = new System.Drawing.Size(279, 34);
            this.Camera2DSettings_TSMItem.Text = "2D面阵相机配置";
            this.Camera2DSettings_TSMItem.Click += new System.EventHandler(this.Camera2DSettings_TSMItem_Click);
            // 
            // Camera2DLinearSettings_TSMItem
            // 
            this.Camera2DLinearSettings_TSMItem.Name = "Camera2DLinearSettings_TSMItem";
            this.Camera2DLinearSettings_TSMItem.Size = new System.Drawing.Size(279, 34);
            this.Camera2DLinearSettings_TSMItem.Text = "2D线扫相机参数配置";
            this.Camera2DLinearSettings_TSMItem.Click += new System.EventHandler(this.Camera2DLinearSettings_TSMItem_Click);
            // 
            // Camera3DSettings_TSMItem
            // 
            this.Camera3DSettings_TSMItem.Name = "Camera3DSettings_TSMItem";
            this.Camera3DSettings_TSMItem.Size = new System.Drawing.Size(279, 34);
            this.Camera3DSettings_TSMItem.Text = "3D相机参数配置";
            this.Camera3DSettings_TSMItem.Click += new System.EventHandler(this.Camera3DSettings_TSMItem_Click);
            // 
            // CameraDeploy_TSMItem
            // 
            this.CameraDeploy_TSMItem.Name = "CameraDeploy_TSMItem";
            this.CameraDeploy_TSMItem.Size = new System.Drawing.Size(279, 34);
            this.CameraDeploy_TSMItem.Text = "相机配置";
            this.CameraDeploy_TSMItem.Click += new System.EventHandler(this.CameraDeploy_TSMItem_Click);
            // 
            // 线扫配置ToolStripMenuItem
            // 
            this.线扫配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GigEHikrobot2DLineScan_TsmItem,
            this.GigEDahua2DLineScan_TsmItem});
            this.线扫配置ToolStripMenuItem.Name = "线扫配置ToolStripMenuItem";
            this.线扫配置ToolStripMenuItem.Size = new System.Drawing.Size(254, 34);
            this.线扫配置ToolStripMenuItem.Text = "Gige_2D线扫";
            // 
            // GigEHikrobot2DLineScan_TsmItem
            // 
            this.GigEHikrobot2DLineScan_TsmItem.Name = "GigEHikrobot2DLineScan_TsmItem";
            this.GigEHikrobot2DLineScan_TsmItem.Size = new System.Drawing.Size(187, 34);
            this.GigEHikrobot2DLineScan_TsmItem.Text = "Hikrobot";
            this.GigEHikrobot2DLineScan_TsmItem.Click += new System.EventHandler(this.GigEHikrobot2DLineScan_TsmItem_Click);
            // 
            // GigEDahua2DLineScan_TsmItem
            // 
            this.GigEDahua2DLineScan_TsmItem.Name = "GigEDahua2DLineScan_TsmItem";
            this.GigEDahua2DLineScan_TsmItem.Size = new System.Drawing.Size(187, 34);
            this.GigEDahua2DLineScan_TsmItem.Text = "Dahua";
            this.GigEDahua2DLineScan_TsmItem.Click += new System.EventHandler(this.GigEDahua2DLineScan_TsmItem_Click);
            // 
            // 光源配置ToolStripMenuItem
            // 
            this.光源配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.配置界面ToolStripMenuItem,
            this.测试界面ToolStripMenuItem});
            this.光源配置ToolStripMenuItem.Name = "光源配置ToolStripMenuItem";
            this.光源配置ToolStripMenuItem.Size = new System.Drawing.Size(254, 34);
            this.光源配置ToolStripMenuItem.Text = "光源配置";
            // 
            // 配置界面ToolStripMenuItem
            // 
            this.配置界面ToolStripMenuItem.Name = "配置界面ToolStripMenuItem";
            this.配置界面ToolStripMenuItem.Size = new System.Drawing.Size(182, 34);
            this.配置界面ToolStripMenuItem.Text = "配置界面";
            this.配置界面ToolStripMenuItem.Click += new System.EventHandler(this.配置界面ToolStripMenuItem_Click);
            // 
            // 测试界面ToolStripMenuItem
            // 
            this.测试界面ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.康视达光源ToolStripMenuItem});
            this.测试界面ToolStripMenuItem.Name = "测试界面ToolStripMenuItem";
            this.测试界面ToolStripMenuItem.Size = new System.Drawing.Size(182, 34);
            this.测试界面ToolStripMenuItem.Text = "测试界面";
            // 
            // 康视达光源ToolStripMenuItem
            // 
            this.康视达光源ToolStripMenuItem.Name = "康视达光源ToolStripMenuItem";
            this.康视达光源ToolStripMenuItem.Size = new System.Drawing.Size(200, 34);
            this.康视达光源ToolStripMenuItem.Text = "康视达光源";
            this.康视达光源ToolStripMenuItem.Click += new System.EventHandler(this.康视达光源ToolStripMenuItem_Click);
            // 
            // 硬件部署配置ToolStripMenuItem
            // 
            this.硬件部署配置ToolStripMenuItem.Name = "硬件部署配置ToolStripMenuItem";
            this.硬件部署配置ToolStripMenuItem.Size = new System.Drawing.Size(254, 34);
            this.硬件部署配置ToolStripMenuItem.Text = "相机硬件部署配置";
            this.硬件部署配置ToolStripMenuItem.Click += new System.EventHandler(this.硬件部署配置ToolStripMenuItem_Click);
            // 
            // 算法模块ToolStripMenuItem
            // 
            this.算法模块ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.算法配置ToolStripMenuItem,
            this.标定配置ToolStripMenuItem});
            this.算法模块ToolStripMenuItem.Enabled = false;
            this.算法模块ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("算法模块ToolStripMenuItem.Image")));
            this.算法模块ToolStripMenuItem.Name = "算法模块ToolStripMenuItem";
            this.算法模块ToolStripMenuItem.Size = new System.Drawing.Size(122, 30);
            this.算法模块ToolStripMenuItem.Text = "算法模块";
            // 
            // 算法配置ToolStripMenuItem
            // 
            this.算法配置ToolStripMenuItem.Name = "算法配置ToolStripMenuItem";
            this.算法配置ToolStripMenuItem.Size = new System.Drawing.Size(182, 34);
            this.算法配置ToolStripMenuItem.Text = "算法配置";
            this.算法配置ToolStripMenuItem.Click += new System.EventHandler(this.算法配置ToolStripMenuItem_Click);
            // 
            // 标定配置ToolStripMenuItem
            // 
            this.标定配置ToolStripMenuItem.Name = "标定配置ToolStripMenuItem";
            this.标定配置ToolStripMenuItem.Size = new System.Drawing.Size(182, 34);
            this.标定配置ToolStripMenuItem.Text = "标定配置";
            this.标定配置ToolStripMenuItem.Click += new System.EventHandler(this.标定配置ToolStripMenuItem_Click);
            // 
            // 视图ToolStripMenuItem
            // 
            this.视图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.视图适应ToolStripMenuItem,
            this.tsmi_2D,
            this.tsmi_3D,
            this.tsmi_Log,
            this.tsmi_Error});
            this.视图ToolStripMenuItem.Enabled = false;
            this.视图ToolStripMenuItem.Image = global::NovaVision.Properties.Resources.视图恢复;
            this.视图ToolStripMenuItem.Name = "视图ToolStripMenuItem";
            this.视图ToolStripMenuItem.Size = new System.Drawing.Size(122, 30);
            this.视图ToolStripMenuItem.Text = "视图编辑";
            // 
            // 视图适应ToolStripMenuItem
            // 
            this.视图适应ToolStripMenuItem.Name = "视图适应ToolStripMenuItem";
            this.视图适应ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.视图适应ToolStripMenuItem.Text = "视图适应";
            this.视图适应ToolStripMenuItem.Click += new System.EventHandler(this.视图适应ToolStripMenuItem_Click);
            // 
            // tsmi_2D
            // 
            this.tsmi_2D.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Window_2DChange});
            this.tsmi_2D.Name = "tsmi_2D";
            this.tsmi_2D.Size = new System.Drawing.Size(270, 34);
            this.tsmi_2D.Text = "2D视图";
            this.tsmi_2D.Click += new System.EventHandler(this.tsmi_2D_Click);
            // 
            // Window_2DChange
            // 
            this.Window_2DChange.Name = "Window_2DChange";
            this.Window_2DChange.Size = new System.Drawing.Size(182, 34);
            this.Window_2DChange.Text = "视图编辑";
            this.Window_2DChange.Click += new System.EventHandler(this.Window_2DChange_Click);
            // 
            // tsmi_3D
            // 
            this.tsmi_3D.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Window_3DChange});
            this.tsmi_3D.Name = "tsmi_3D";
            this.tsmi_3D.Size = new System.Drawing.Size(270, 34);
            this.tsmi_3D.Text = "3D视图";
            this.tsmi_3D.Click += new System.EventHandler(this.tsmi_3D_Click);
            // 
            // Window_3DChange
            // 
            this.Window_3DChange.Name = "Window_3DChange";
            this.Window_3DChange.Size = new System.Drawing.Size(182, 34);
            this.Window_3DChange.Text = "视觉编辑";
            this.Window_3DChange.Click += new System.EventHandler(this.Window_3DChange_Click);
            // 
            // tsmi_Log
            // 
            this.tsmi_Log.Name = "tsmi_Log";
            this.tsmi_Log.Size = new System.Drawing.Size(270, 34);
            this.tsmi_Log.Text = "日志栏";
            this.tsmi_Log.Click += new System.EventHandler(this.tsmi_Log_Click);
            // 
            // tsmi_Error
            // 
            this.tsmi_Error.Name = "tsmi_Error";
            this.tsmi_Error.Size = new System.Drawing.Size(270, 34);
            this.tsmi_Error.Text = "错误栏";
            this.tsmi_Error.Click += new System.EventHandler(this.tsmi_Error_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.SkyBlue;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStrip_Auth,
            this.toolStrip_User,
            this.toolStrip_Logout,
            this.toolStrip_JobName,
            this.toolStrip_State,
            this.toolStrip_CommState,
            this.toolStripStatusLabel1,
            this.tssl_MemoryConsume,
            this.toolStripSplitButton1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 749);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1717, 35);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStrip_Auth
            // 
            this.toolStrip_Auth.Margin = new System.Windows.Forms.Padding(0, 3, 10, 2);
            this.toolStrip_Auth.Name = "toolStrip_Auth";
            this.toolStrip_Auth.Size = new System.Drawing.Size(46, 30);
            this.toolStrip_Auth.Text = "权限";
            this.toolStrip_Auth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStrip_User
            // 
            this.toolStrip_User.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStrip_User.Margin = new System.Windows.Forms.Padding(0, 3, 10, 2);
            this.toolStrip_User.Name = "toolStrip_User";
            this.toolStrip_User.Size = new System.Drawing.Size(50, 30);
            this.toolStrip_User.Text = "用户";
            // 
            // toolStrip_Logout
            // 
            this.toolStrip_Logout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStrip_Logout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_Logout.Margin = new System.Windows.Forms.Padding(0, 2, 20, 0);
            this.toolStrip_Logout.Name = "toolStrip_Logout";
            this.toolStrip_Logout.Size = new System.Drawing.Size(67, 33);
            this.toolStrip_Logout.Text = "注销";
            this.toolStrip_Logout.ButtonClick += new System.EventHandler(this.toolStrip_Logout_ButtonClick);
            // 
            // toolStrip_JobName
            // 
            this.toolStrip_JobName.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.toolStrip_JobName.Name = "toolStrip_JobName";
            this.toolStrip_JobName.Size = new System.Drawing.Size(50, 28);
            this.toolStrip_JobName.Text = "型号";
            // 
            // toolStrip_State
            // 
            this.toolStrip_State.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip_State.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStrip_State.ForeColor = System.Drawing.Color.Red;
            this.toolStrip_State.Name = "toolStrip_State";
            this.toolStrip_State.Size = new System.Drawing.Size(130, 28);
            this.toolStrip_State.Text = "系统：OffLine";
            // 
            // toolStrip_CommState
            // 
            this.toolStrip_CommState.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStrip_CommState.ForeColor = System.Drawing.Color.Red;
            this.toolStrip_CommState.Name = "toolStrip_CommState";
            this.toolStrip_CommState.Size = new System.Drawing.Size(163, 28);
            this.toolStrip_CommState.Text = "通讯：Disconnect";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Navy;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(136, 28);
            this.toolStripStatusLabel1.Text = "当前内存消耗：";
            // 
            // tssl_MemoryConsume
            // 
            this.tssl_MemoryConsume.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tssl_MemoryConsume.Name = "tssl_MemoryConsume";
            this.tssl_MemoryConsume.Size = new System.Drawing.Size(54, 28);
            this.tssl_MemoryConsume.Text = "0MB";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chineseToolStripMenuItem,
            this.englishToolStripMenuItem});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(103, 32);
            this.toolStripSplitButton1.Text = "语言切换";
            // 
            // chineseToolStripMenuItem
            // 
            this.chineseToolStripMenuItem.Name = "chineseToolStripMenuItem";
            this.chineseToolStripMenuItem.Size = new System.Drawing.Size(172, 34);
            this.chineseToolStripMenuItem.Text = "中文";
            this.chineseToolStripMenuItem.Click += new System.EventHandler(this.chineseToolStripMenuItem_Click);
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(172, 34);
            this.englishToolStripMenuItem.Text = "English";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dockPanel_Main, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 36);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 717F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1717, 713);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // dockPanel_Main
            // 
            this.dockPanel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel_Main.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.dockPanel_Main.Location = new System.Drawing.Point(3, 3);
            this.dockPanel_Main.Name = "dockPanel_Main";
            this.dockPanel_Main.Padding = new System.Windows.Forms.Padding(6);
            this.dockPanel_Main.ShowAutoHideContentOnHover = false;
            this.dockPanel_Main.Size = new System.Drawing.Size(1711, 707);
            this.dockPanel_Main.TabIndex = 0;
            this.dockPanel_Main.Theme = this.vS2015BlueTheme1;
            // 
            // Frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 33F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(1717, 784);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Frm_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Vision System";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Frm_Main_FormClosed);
            this.Load += new System.EventHandler(this.Frm_Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 登录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 产品型号配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 文件参数配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统在线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 通讯模块ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 硬件模块ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 相机配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Camera2DSettings_TSMItem;
        private System.Windows.Forms.ToolStripMenuItem 算法模块ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 算法配置ToolStripMenuItem;
        private ToolStripMenuItem 用户管理ToolStripMenuItem;
        private ToolStripMenuItem 视图ToolStripMenuItem;
        private ToolStripMenuItem 权限配置ToolStripMenuItem;
        public ToolStripMenuItem tsmi_2D;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStrip_Auth;
        private ToolStripStatusLabel toolStrip_User;
        private ToolStripSplitButton toolStrip_Logout;
        private ToolStripStatusLabel toolStrip_JobName;
        private ToolStripStatusLabel toolStrip_State;
        private ToolStripStatusLabel toolStrip_CommState;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel tssl_MemoryConsume;
        private ToolStripSplitButton toolStripSplitButton1;
        private ToolStripMenuItem chineseToolStripMenuItem;
        private ToolStripMenuItem englishToolStripMenuItem;
        private ToolStripMenuItem 视图适应ToolStripMenuItem;
        private ToolStripMenuItem Window_2DChange;
        private ToolStripMenuItem 通讯类型TcpToolStripMenuItem;
        private ToolStripMenuItem 光源配置ToolStripMenuItem;
        private ToolStripMenuItem 配置界面ToolStripMenuItem;
        private ToolStripMenuItem 测试界面ToolStripMenuItem;
        private ToolStripMenuItem 康视达光源ToolStripMenuItem;
        private ToolStripMenuItem 通讯配置ToolStripMenuItem;
        private ToolStripMenuItem 工位配置ToolStripMenuItem;
        private ToolStripMenuItem ModbustoolStripMenuItem;
        private ToolStripMenuItem 标定配置ToolStripMenuItem;
        public ToolStripMenuItem tsmi_3D;
        private ToolStripMenuItem Camera2DLinearSettings_TSMItem;
        private ToolStripMenuItem Camera3DSettings_TSMItem;
        private ToolStripMenuItem CameraDeploy_TSMItem;
        private ToolStripMenuItem 线扫配置ToolStripMenuItem;
        private ToolStripMenuItem GigEHikrobot2DLineScan_TsmItem;
        private ToolStripMenuItem GigEDahua2DLineScan_TsmItem;
        private ToolStripMenuItem 硬件部署配置ToolStripMenuItem;
        private ToolStripMenuItem Window_3DChange;
        public ToolStripMenuItem tsmi_Log;
        public ToolStripMenuItem tsmi_Error;
        private ToolStripMenuItem tcpToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel1;
        private DockPanel dockPanel_Main;
        private VS2015BlueTheme vS2015BlueTheme1;
    }
}

