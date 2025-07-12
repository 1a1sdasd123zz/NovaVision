using System.Windows.Forms;
using NovaVision.UserControlLibrary;

namespace NovaVision.VisionForm.CarameFrm
{
    partial class FrmCamera3DSetting
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.CCD_groupBox = new System.Windows.Forms.GroupBox();
            this.btnSaveParams = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnAddCam = new System.Windows.Forms.Button();
            this.CCDList = new System.Windows.Forms.ListBox();
            this.gBConnectControl = new System.Windows.Forms.GroupBox();
            this.btnSnap = new System.Windows.Forms.Button();
            this.tbIp = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnRecOrSto = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cbVendors = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gBAcqControl = new System.Windows.Forms.GroupBox();
            this.nROIButtom = new System.Windows.Forms.NumericUpDown();
            this.label33 = new System.Windows.Forms.Label();
            this.nROITop = new System.Windows.Forms.NumericUpDown();
            this.label32 = new System.Windows.Forms.Label();
            this.setCCDParamsInfo = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.nUDTimeout = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.cbExposure = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.nUDScanLines = new System.Windows.Forms.NumericUpDown();
            this.label23 = new System.Windows.Forms.Label();
            this.cbAcqLineRate = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.nUDExpo = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.nUDScanLength = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.nUDAcqLineRate = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cbAcqMode = new System.Windows.Forms.ComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.imageDisplay1 = new ImageDisplay();
            this.btn_ZPositive = new System.Windows.Forms.Button();
            this.ZscaleUpDown = new System.Windows.Forms.NumericUpDown();
            this.btn_ClearDisplay = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.btn_ZNegative = new System.Windows.Forms.Button();
            this.YscaleUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.XscaleUpDown = new System.Windows.Forms.NumericUpDown();
            this.gBSensorInfo = new System.Windows.Forms.GroupBox();
            this.lbIP = new System.Windows.Forms.Label();
            this.lbSerialNum = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.lbModel = new System.Windows.Forms.Label();
            this.lbVendor = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gBEncoder = new System.Windows.Forms.GroupBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.nUDSpacing = new System.Windows.Forms.NumericUpDown();
            this.lbTriggerSource = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.nUDEncoderPitch = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.nUDSpeed = new System.Windows.Forms.NumericUpDown();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.CCD_groupBox.SuspendLayout();
            this.gBConnectControl.SuspendLayout();
            this.gBAcqControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nROIButtom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nROITop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDScanLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDExpo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDScanLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDAcqLineRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZscaleUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YscaleUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XscaleUpDown)).BeginInit();
            this.gBSensorInfo.SuspendLayout();
            this.gBEncoder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDSpacing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDEncoderPitch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDSpeed)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1246, 1060);
            this.splitContainer1.SplitterDistance = 228;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 5;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.CCD_groupBox);
            this.splitContainer4.Panel1.Controls.Add(this.gBConnectControl);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.gBAcqControl);
            this.splitContainer4.Size = new System.Drawing.Size(228, 1060);
            this.splitContainer4.SplitterDistance = 312;
            this.splitContainer4.SplitterWidth = 6;
            this.splitContainer4.TabIndex = 9;
            // 
            // CCD_groupBox
            // 
            this.CCD_groupBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CCD_groupBox.Controls.Add(this.btnSaveParams);
            this.CCD_groupBox.Controls.Add(this.button2);
            this.CCD_groupBox.Controls.Add(this.btnAddCam);
            this.CCD_groupBox.Controls.Add(this.CCDList);
            this.CCD_groupBox.Location = new System.Drawing.Point(4, 198);
            this.CCD_groupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CCD_groupBox.Name = "CCD_groupBox";
            this.CCD_groupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CCD_groupBox.Size = new System.Drawing.Size(328, 266);
            this.CCD_groupBox.TabIndex = 3;
            this.CCD_groupBox.TabStop = false;
            this.CCD_groupBox.Text = "已配置相机列表";
            // 
            // btnSaveParams
            // 
            this.btnSaveParams.Location = new System.Drawing.Point(9, 222);
            this.btnSaveParams.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSaveParams.Name = "btnSaveParams";
            this.btnSaveParams.Size = new System.Drawing.Size(110, 34);
            this.btnSaveParams.TabIndex = 8;
            this.btnSaveParams.Text = "保存参数";
            this.btnSaveParams.UseVisualStyleBackColor = true;
            this.btnSaveParams.Click += new System.EventHandler(this.btnSaveParams_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(206, 220);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 34);
            this.button2.TabIndex = 5;
            this.button2.Tag = "将当前设备移除配置列表";
            this.button2.Text = "移除配置";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnAddCam
            // 
            this.btnAddCam.Location = new System.Drawing.Point(6, 220);
            this.btnAddCam.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddCam.Name = "btnAddCam";
            this.btnAddCam.Size = new System.Drawing.Size(112, 34);
            this.btnAddCam.TabIndex = 4;
            this.btnAddCam.Tag = "将当前选择的设备添加进配置列表";
            this.btnAddCam.Text = "添加设备";
            this.btnAddCam.UseVisualStyleBackColor = true;
            this.btnAddCam.Click += new System.EventHandler(this.btnAddCam_Click);
            // 
            // CCDList
            // 
            this.CCDList.Dock = System.Windows.Forms.DockStyle.Top;
            this.CCDList.FormattingEnabled = true;
            this.CCDList.ItemHeight = 18;
            this.CCDList.Location = new System.Drawing.Point(4, 25);
            this.CCDList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CCDList.Name = "CCDList";
            this.CCDList.Size = new System.Drawing.Size(320, 184);
            this.CCDList.TabIndex = 0;
            this.CCDList.SelectedIndexChanged += new System.EventHandler(this.CCDList_SelectedIndexChanged);
            // 
            // gBConnectControl
            // 
            this.gBConnectControl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gBConnectControl.Controls.Add(this.btnSnap);
            this.gBConnectControl.Controls.Add(this.tbIp);
            this.gBConnectControl.Controls.Add(this.label7);
            this.gBConnectControl.Controls.Add(this.btnRecOrSto);
            this.gBConnectControl.Controls.Add(this.btnConnect);
            this.gBConnectControl.Controls.Add(this.cbVendors);
            this.gBConnectControl.Controls.Add(this.label1);
            this.gBConnectControl.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.gBConnectControl.Location = new System.Drawing.Point(3, 0);
            this.gBConnectControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gBConnectControl.Name = "gBConnectControl";
            this.gBConnectControl.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gBConnectControl.Size = new System.Drawing.Size(328, 188);
            this.gBConnectControl.TabIndex = 0;
            this.gBConnectControl.TabStop = false;
            this.gBConnectControl.Text = "连接控制";
            // 
            // btnSnap
            // 
            this.btnSnap.Location = new System.Drawing.Point(232, 82);
            this.btnSnap.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSnap.Name = "btnSnap";
            this.btnSnap.Size = new System.Drawing.Size(87, 34);
            this.btnSnap.TabIndex = 6;
            this.btnSnap.Text = "软触发";
            this.btnSnap.UseVisualStyleBackColor = true;
            this.btnSnap.Click += new System.EventHandler(this.btnSnap_Click);
            // 
            // tbIp
            // 
            this.tbIp.Location = new System.Drawing.Point(138, 144);
            this.tbIp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbIp.Name = "tbIp";
            this.tbIp.Size = new System.Drawing.Size(145, 28);
            this.tbIp.TabIndex = 5;
            this.tbIp.Text = "192.168.1.10";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 148);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 18);
            this.label7.TabIndex = 4;
            this.label7.Text = "相机IP:";
            // 
            // btnRecOrSto
            // 
            this.btnRecOrSto.Location = new System.Drawing.Point(124, 82);
            this.btnRecOrSto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRecOrSto.Name = "btnRecOrSto";
            this.btnRecOrSto.Size = new System.Drawing.Size(98, 34);
            this.btnRecOrSto.TabIndex = 3;
            this.btnRecOrSto.Text = "开始接收";
            this.btnRecOrSto.UseVisualStyleBackColor = true;
            this.btnRecOrSto.Click += new System.EventHandler(this.btnRecOrSto_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(9, 82);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(106, 34);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cbVendors
            // 
            this.cbVendors.FormattingEnabled = true;
            this.cbVendors.Items.AddRange(new object[] {
            "LMI",
            "Keyence",
            "翌视(LVM)"});
            this.cbVendors.Location = new System.Drawing.Point(138, 30);
            this.cbVendors.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbVendors.Name = "cbVendors";
            this.cbVendors.Size = new System.Drawing.Size(145, 26);
            this.cbVendors.TabIndex = 1;
            this.cbVendors.SelectedIndexChanged += new System.EventHandler(this.cbVendors_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "相机品牌：";
            // 
            // gBAcqControl
            // 
            this.gBAcqControl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gBAcqControl.Controls.Add(this.nROIButtom);
            this.gBAcqControl.Controls.Add(this.label33);
            this.gBAcqControl.Controls.Add(this.nROITop);
            this.gBAcqControl.Controls.Add(this.label32);
            this.gBAcqControl.Controls.Add(this.setCCDParamsInfo);
            this.gBAcqControl.Controls.Add(this.label26);
            this.gBAcqControl.Controls.Add(this.nUDTimeout);
            this.gBAcqControl.Controls.Add(this.label11);
            this.gBAcqControl.Controls.Add(this.cbExposure);
            this.gBAcqControl.Controls.Add(this.label25);
            this.gBAcqControl.Controls.Add(this.label24);
            this.gBAcqControl.Controls.Add(this.nUDScanLines);
            this.gBAcqControl.Controls.Add(this.label23);
            this.gBAcqControl.Controls.Add(this.cbAcqLineRate);
            this.gBAcqControl.Controls.Add(this.label22);
            this.gBAcqControl.Controls.Add(this.label21);
            this.gBAcqControl.Controls.Add(this.nUDExpo);
            this.gBAcqControl.Controls.Add(this.label8);
            this.gBAcqControl.Controls.Add(this.label16);
            this.gBAcqControl.Controls.Add(this.nUDScanLength);
            this.gBAcqControl.Controls.Add(this.label15);
            this.gBAcqControl.Controls.Add(this.label14);
            this.gBAcqControl.Controls.Add(this.nUDAcqLineRate);
            this.gBAcqControl.Controls.Add(this.label13);
            this.gBAcqControl.Controls.Add(this.label10);
            this.gBAcqControl.Controls.Add(this.cbAcqMode);
            this.gBAcqControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.gBAcqControl.Location = new System.Drawing.Point(0, 0);
            this.gBAcqControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gBAcqControl.Name = "gBAcqControl";
            this.gBAcqControl.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gBAcqControl.Size = new System.Drawing.Size(333, 742);
            this.gBAcqControl.TabIndex = 2;
            this.gBAcqControl.TabStop = false;
            this.gBAcqControl.Text = "采集模式控制及曝光参数设置";
            // 
            // nROIButtom
            // 
            this.nROIButtom.Location = new System.Drawing.Point(118, 452);
            this.nROIButtom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nROIButtom.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            65536});
            this.nROIButtom.Minimum = new decimal(new int[] {
            100000000,
            0,
            0,
            -2147418112});
            this.nROIButtom.Name = "nROIButtom";
            this.nROIButtom.Size = new System.Drawing.Size(132, 28);
            this.nROIButtom.TabIndex = 30;
            this.nROIButtom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nROIButtom.ValueChanged += new System.EventHandler(this.nROIButtom_ValueChanged);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(9, 458);
            this.label33.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(116, 18);
            this.label33.TabIndex = 29;
            this.label33.Text = "ROI_Buttom：";
            // 
            // nROITop
            // 
            this.nROITop.Location = new System.Drawing.Point(118, 406);
            this.nROITop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nROITop.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            65536});
            this.nROITop.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.nROITop.Name = "nROITop";
            this.nROITop.Size = new System.Drawing.Size(132, 28);
            this.nROITop.TabIndex = 28;
            this.nROITop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nROITop.ValueChanged += new System.EventHandler(this.nROITop_ValueChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(9, 412);
            this.label32.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(89, 18);
            this.label32.TabIndex = 27;
            this.label32.Text = "ROI_Top：";
            // 
            // setCCDParamsInfo
            // 
            this.setCCDParamsInfo.Location = new System.Drawing.Point(9, 492);
            this.setCCDParamsInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.setCCDParamsInfo.Name = "setCCDParamsInfo";
            this.setCCDParamsInfo.Size = new System.Drawing.Size(160, 34);
            this.setCCDParamsInfo.TabIndex = 26;
            this.setCCDParamsInfo.Text = "设置相机参数";
            this.setCCDParamsInfo.UseVisualStyleBackColor = true;
            this.setCCDParamsInfo.Click += new System.EventHandler(this.setCCDParamsInfo_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(274, 374);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(26, 18);
            this.label26.TabIndex = 25;
            this.label26.Text = "ms";
            // 
            // nUDTimeout
            // 
            this.nUDTimeout.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nUDTimeout.Location = new System.Drawing.Point(120, 364);
            this.nUDTimeout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nUDTimeout.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            65536});
            this.nUDTimeout.Name = "nUDTimeout";
            this.nUDTimeout.Size = new System.Drawing.Size(132, 28);
            this.nUDTimeout.TabIndex = 24;
            this.nUDTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUDTimeout.ValueChanged += new System.EventHandler(this.nUDTimeout_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 370);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 18);
            this.label11.TabIndex = 23;
            this.label11.Text = "采集超时：";
            // 
            // cbExposure
            // 
            this.cbExposure.FormattingEnabled = true;
            this.cbExposure.Location = new System.Drawing.Point(122, 270);
            this.cbExposure.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbExposure.Name = "cbExposure";
            this.cbExposure.Size = new System.Drawing.Size(132, 26);
            this.cbExposure.TabIndex = 22;
            this.cbExposure.SelectedIndexChanged += new System.EventHandler(this.cbExposure_SelectedIndexChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(10, 270);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(98, 18);
            this.label25.TabIndex = 21;
            this.label25.Text = "曝光选择：";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(261, 226);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 18);
            this.label24.TabIndex = 20;
            this.label24.Text = "lines";
            // 
            // nUDScanLines
            // 
            this.nUDScanLines.Location = new System.Drawing.Point(122, 220);
            this.nUDScanLines.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nUDScanLines.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nUDScanLines.Name = "nUDScanLines";
            this.nUDScanLines.Size = new System.Drawing.Size(132, 28);
            this.nUDScanLines.TabIndex = 19;
            this.nUDScanLines.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUDScanLines.ValueChanged += new System.EventHandler(this.nUDScanLines_ValueChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(12, 226);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(98, 18);
            this.label23.TabIndex = 18;
            this.label23.Text = "扫描行数：";
            // 
            // cbAcqLineRate
            // 
            this.cbAcqLineRate.FormattingEnabled = true;
            this.cbAcqLineRate.Location = new System.Drawing.Point(122, 81);
            this.cbAcqLineRate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbAcqLineRate.Name = "cbAcqLineRate";
            this.cbAcqLineRate.Size = new System.Drawing.Size(130, 26);
            this.cbAcqLineRate.TabIndex = 17;
            this.cbAcqLineRate.SelectedIndexChanged += new System.EventHandler(this.cbAcqLineRate_SelectedIndexChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(12, 87);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(98, 18);
            this.label22.TabIndex = 16;
            this.label22.Text = "行频选择：";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(274, 276);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(26, 18);
            this.label21.TabIndex = 15;
            this.label21.Text = "us";
            // 
            // nUDExpo
            // 
            this.nUDExpo.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nUDExpo.Location = new System.Drawing.Point(122, 315);
            this.nUDExpo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nUDExpo.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            65536});
            this.nUDExpo.Name = "nUDExpo";
            this.nUDExpo.Size = new System.Drawing.Size(130, 28);
            this.nUDExpo.TabIndex = 14;
            this.nUDExpo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUDExpo.ValueChanged += new System.EventHandler(this.nUDExpo_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 320);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 18);
            this.label8.TabIndex = 0;
            this.label8.Text = "曝光指示：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(274, 183);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(26, 18);
            this.label16.TabIndex = 7;
            this.label16.Text = "mm";
            // 
            // nUDScanLength
            // 
            this.nUDScanLength.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nUDScanLength.Location = new System.Drawing.Point(123, 176);
            this.nUDScanLength.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nUDScanLength.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            65536});
            this.nUDScanLength.Name = "nUDScanLength";
            this.nUDScanLength.Size = new System.Drawing.Size(129, 28);
            this.nUDScanLength.TabIndex = 6;
            this.nUDScanLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUDScanLength.Value = new decimal(new int[] {
            100,
            0,
            0,
            65536});
            this.nUDScanLength.ValueChanged += new System.EventHandler(this.nUDScanLength_ValueChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 180);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(98, 18);
            this.label15.TabIndex = 5;
            this.label15.Text = "扫描长度：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(274, 138);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(26, 18);
            this.label14.TabIndex = 4;
            this.label14.Text = "Hz";
            // 
            // nUDAcqLineRate
            // 
            this.nUDAcqLineRate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nUDAcqLineRate.Location = new System.Drawing.Point(122, 130);
            this.nUDAcqLineRate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nUDAcqLineRate.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            65536});
            this.nUDAcqLineRate.Name = "nUDAcqLineRate";
            this.nUDAcqLineRate.Size = new System.Drawing.Size(132, 28);
            this.nUDAcqLineRate.TabIndex = 3;
            this.nUDAcqLineRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUDAcqLineRate.ValueChanged += new System.EventHandler(this.nUDAcqLineRate_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 135);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(98, 18);
            this.label13.TabIndex = 2;
            this.label13.Text = "采集行频：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 42);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 18);
            this.label10.TabIndex = 1;
            this.label10.Text = "采集模式：";
            // 
            // cbAcqMode
            // 
            this.cbAcqMode.FormattingEnabled = true;
            this.cbAcqMode.Items.AddRange(new object[] {
            "Time_ExternTrigger",
            "Time_Software",
            "Encoder_ExternTrigger",
            "Encoder_Software",
            "Test_Time"});
            this.cbAcqMode.Location = new System.Drawing.Point(122, 34);
            this.cbAcqMode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbAcqMode.Name = "cbAcqMode";
            this.cbAcqMode.Size = new System.Drawing.Size(180, 26);
            this.cbAcqMode.TabIndex = 0;
            this.cbAcqMode.SelectedIndexChanged += new System.EventHandler(this.cbAcqMode_SelectedIndexChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitContainer2.Panel2.Controls.Add(this.gBSensorInfo);
            this.splitContainer2.Panel2.Controls.Add(this.gBEncoder);
            this.splitContainer2.Size = new System.Drawing.Size(1012, 1060);
            this.splitContainer2.SplitterDistance = 851;
            this.splitContainer2.SplitterWidth = 6;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.btn_ZPositive);
            this.splitContainer3.Panel2.Controls.Add(this.ZscaleUpDown);
            this.splitContainer3.Panel2.Controls.Add(this.btn_ClearDisplay);
            this.splitContainer3.Panel2.Controls.Add(this.label28);
            this.splitContainer3.Panel2.Controls.Add(this.btn_ZNegative);
            this.splitContainer3.Panel2.Controls.Add(this.YscaleUpDown);
            this.splitContainer3.Panel2.Controls.Add(this.label3);
            this.splitContainer3.Panel2.Controls.Add(this.label27);
            this.splitContainer3.Panel2.Controls.Add(this.XscaleUpDown);
            this.splitContainer3.Size = new System.Drawing.Size(1012, 851);
            this.splitContainer3.SplitterDistance = 804;
            this.splitContainer3.SplitterWidth = 6;
            this.splitContainer3.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1012, 804);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(1004, 772);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "PointCloud";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.imageDisplay1);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(1004, 772);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "RangeImage";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // imageDisplay1
            // 
            this.imageDisplay1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(140)))), ((int)(((byte)(206)))));
            this.imageDisplay1.CogImage = null;
            this.imageDisplay1.DisplayName = "RangeImage";
            this.imageDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageDisplay1.Location = new System.Drawing.Point(4, 4);
            this.imageDisplay1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.imageDisplay1.Name = "imageDisplay1";
            this.imageDisplay1.Record = null;
            this.imageDisplay1.Size = new System.Drawing.Size(996, 764);
            this.imageDisplay1.TabIndex = 0;
            // 
            // btn_ZPositive
            // 
            this.btn_ZPositive.Location = new System.Drawing.Point(4, 12);
            this.btn_ZPositive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_ZPositive.Name = "btn_ZPositive";
            this.btn_ZPositive.Size = new System.Drawing.Size(112, 34);
            this.btn_ZPositive.TabIndex = 1;
            this.btn_ZPositive.Text = "Z正方向";
            this.btn_ZPositive.UseVisualStyleBackColor = true;
            this.btn_ZPositive.Click += new System.EventHandler(this.btn_ZPositive_Click);
            // 
            // ZscaleUpDown
            // 
            this.ZscaleUpDown.Location = new System.Drawing.Point(672, 15);
            this.ZscaleUpDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ZscaleUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.ZscaleUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ZscaleUpDown.Name = "ZscaleUpDown";
            this.ZscaleUpDown.Size = new System.Drawing.Size(76, 28);
            this.ZscaleUpDown.TabIndex = 5;
            this.ZscaleUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ZscaleUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btn_ClearDisplay
            // 
            this.btn_ClearDisplay.Location = new System.Drawing.Point(772, 12);
            this.btn_ClearDisplay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_ClearDisplay.Name = "btn_ClearDisplay";
            this.btn_ClearDisplay.Size = new System.Drawing.Size(112, 34);
            this.btn_ClearDisplay.TabIndex = 3;
            this.btn_ClearDisplay.Text = "清除显示";
            this.btn_ClearDisplay.UseVisualStyleBackColor = true;
            this.btn_ClearDisplay.Click += new System.EventHandler(this.btn_ClearDisplay_Click);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(594, 21);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(71, 18);
            this.label28.TabIndex = 4;
            this.label28.Text = "Zscale:";
            // 
            // btn_ZNegative
            // 
            this.btn_ZNegative.Location = new System.Drawing.Point(126, 14);
            this.btn_ZNegative.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_ZNegative.Name = "btn_ZNegative";
            this.btn_ZNegative.Size = new System.Drawing.Size(112, 34);
            this.btn_ZNegative.TabIndex = 2;
            this.btn_ZNegative.Text = "Z反方向";
            this.btn_ZNegative.UseVisualStyleBackColor = true;
            this.btn_ZNegative.Click += new System.EventHandler(this.btn_ZNegative_Click);
            // 
            // YscaleUpDown
            // 
            this.YscaleUpDown.Location = new System.Drawing.Point(510, 15);
            this.YscaleUpDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.YscaleUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.YscaleUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.YscaleUpDown.Name = "YscaleUpDown";
            this.YscaleUpDown.Size = new System.Drawing.Size(76, 28);
            this.YscaleUpDown.TabIndex = 5;
            this.YscaleUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.YscaleUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(270, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Xscale:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(432, 21);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(71, 18);
            this.label27.TabIndex = 4;
            this.label27.Text = "Yscale:";
            // 
            // XscaleUpDown
            // 
            this.XscaleUpDown.Location = new System.Drawing.Point(348, 15);
            this.XscaleUpDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.XscaleUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.XscaleUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.XscaleUpDown.Name = "XscaleUpDown";
            this.XscaleUpDown.Size = new System.Drawing.Size(76, 28);
            this.XscaleUpDown.TabIndex = 5;
            this.XscaleUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.XscaleUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // gBSensorInfo
            // 
            this.gBSensorInfo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gBSensorInfo.Controls.Add(this.lbIP);
            this.gBSensorInfo.Controls.Add(this.lbSerialNum);
            this.gBSensorInfo.Controls.Add(this.lbVersion);
            this.gBSensorInfo.Controls.Add(this.lbModel);
            this.gBSensorInfo.Controls.Add(this.lbVendor);
            this.gBSensorInfo.Controls.Add(this.label12);
            this.gBSensorInfo.Controls.Add(this.label6);
            this.gBSensorInfo.Controls.Add(this.label5);
            this.gBSensorInfo.Controls.Add(this.label4);
            this.gBSensorInfo.Controls.Add(this.label2);
            this.gBSensorInfo.Location = new System.Drawing.Point(399, 10);
            this.gBSensorInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gBSensorInfo.Name = "gBSensorInfo";
            this.gBSensorInfo.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gBSensorInfo.Size = new System.Drawing.Size(488, 256);
            this.gBSensorInfo.TabIndex = 1;
            this.gBSensorInfo.TabStop = false;
            this.gBSensorInfo.Text = "传感器信息";
            // 
            // lbIP
            // 
            this.lbIP.AutoSize = true;
            this.lbIP.Location = new System.Drawing.Point(134, 198);
            this.lbIP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbIP.Name = "lbIP";
            this.lbIP.Size = new System.Drawing.Size(35, 18);
            this.lbIP.TabIndex = 11;
            this.lbIP.Text = "N/A";
            // 
            // lbSerialNum
            // 
            this.lbSerialNum.AutoSize = true;
            this.lbSerialNum.Location = new System.Drawing.Point(134, 159);
            this.lbSerialNum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSerialNum.Name = "lbSerialNum";
            this.lbSerialNum.Size = new System.Drawing.Size(35, 18);
            this.lbSerialNum.TabIndex = 10;
            this.lbSerialNum.Text = "N/A";
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.Location = new System.Drawing.Point(134, 120);
            this.lbVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(35, 18);
            this.lbVersion.TabIndex = 9;
            this.lbVersion.Text = "N/A";
            // 
            // lbModel
            // 
            this.lbModel.AutoSize = true;
            this.lbModel.Location = new System.Drawing.Point(134, 81);
            this.lbModel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbModel.Name = "lbModel";
            this.lbModel.Size = new System.Drawing.Size(35, 18);
            this.lbModel.TabIndex = 8;
            this.lbModel.Text = "N/A";
            // 
            // lbVendor
            // 
            this.lbVendor.AutoSize = true;
            this.lbVendor.Location = new System.Drawing.Point(134, 42);
            this.lbVendor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbVendor.Name = "lbVendor";
            this.lbVendor.Size = new System.Drawing.Size(35, 18);
            this.lbVendor.TabIndex = 6;
            this.lbVendor.Text = "N/A";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(54, 198);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 18);
            this.label12.TabIndex = 5;
            this.label12.Text = "相机IP:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(45, 159);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 4;
            this.label6.Text = "序列号：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(63, 120);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 18);
            this.label5.TabIndex = 3;
            this.label5.Text = "版本：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(63, 81);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 18);
            this.label4.TabIndex = 2;
            this.label4.Text = "型号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "传感器品牌：";
            // 
            // gBEncoder
            // 
            this.gBEncoder.Controls.Add(this.label31);
            this.gBEncoder.Controls.Add(this.label30);
            this.gBEncoder.Controls.Add(this.label29);
            this.gBEncoder.Controls.Add(this.nUDSpacing);
            this.gBEncoder.Controls.Add(this.lbTriggerSource);
            this.gBEncoder.Controls.Add(this.label20);
            this.gBEncoder.Controls.Add(this.nUDEncoderPitch);
            this.gBEncoder.Controls.Add(this.label9);
            this.gBEncoder.Controls.Add(this.label19);
            this.gBEncoder.Controls.Add(this.label18);
            this.gBEncoder.Controls.Add(this.label17);
            this.gBEncoder.Controls.Add(this.nUDSpeed);
            this.gBEncoder.Location = new System.Drawing.Point(18, 10);
            this.gBEncoder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gBEncoder.Name = "gBEncoder";
            this.gBEncoder.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gBEncoder.Size = new System.Drawing.Size(376, 256);
            this.gBEncoder.TabIndex = 0;
            this.gBEncoder.TabStop = false;
            this.gBEncoder.Text = "编码器参数设置";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(308, 189);
            this.label31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(26, 18);
            this.label31.TabIndex = 17;
            this.label31.Text = "mm";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(190, 190);
            this.label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(35, 18);
            this.label30.TabIndex = 16;
            this.label30.Text = "N/A";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(9, 189);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(107, 18);
            this.label29.TabIndex = 15;
            this.label29.Text = "X方向点距：";
            // 
            // nUDSpacing
            // 
            this.nUDSpacing.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.nUDSpacing.Location = new System.Drawing.Point(136, 148);
            this.nUDSpacing.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nUDSpacing.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            196608});
            this.nUDSpacing.Name = "nUDSpacing";
            this.nUDSpacing.Size = new System.Drawing.Size(154, 28);
            this.nUDSpacing.TabIndex = 14;
            this.nUDSpacing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUDSpacing.ValueChanged += new System.EventHandler(this.nUDSpacing_ValueChanged);
            // 
            // lbTriggerSource
            // 
            this.lbTriggerSource.AutoSize = true;
            this.lbTriggerSource.Location = new System.Drawing.Point(308, 153);
            this.lbTriggerSource.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTriggerSource.Name = "lbTriggerSource";
            this.lbTriggerSource.Size = new System.Drawing.Size(26, 18);
            this.lbTriggerSource.TabIndex = 6;
            this.lbTriggerSource.Text = "mm";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(302, 68);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(71, 18);
            this.label20.TabIndex = 13;
            this.label20.Text = "mm/tick";
            // 
            // nUDEncoderPitch
            // 
            this.nUDEncoderPitch.DecimalPlaces = 4;
            this.nUDEncoderPitch.Increment = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.nUDEncoderPitch.Location = new System.Drawing.Point(136, 62);
            this.nUDEncoderPitch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nUDEncoderPitch.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            196608});
            this.nUDEncoderPitch.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            393216});
            this.nUDEncoderPitch.Name = "nUDEncoderPitch";
            this.nUDEncoderPitch.Size = new System.Drawing.Size(154, 28);
            this.nUDEncoderPitch.TabIndex = 12;
            this.nUDEncoderPitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUDEncoderPitch.Value = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.nUDEncoderPitch.ValueChanged += new System.EventHandler(this.nUDEncoderPitch_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 150);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 18);
            this.label9.TabIndex = 1;
            this.label9.Text = "Y方向点距：";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(9, 66);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(134, 18);
            this.label19.TabIndex = 11;
            this.label19.Text = "编码器分辨率：";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(306, 105);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(44, 18);
            this.label18.TabIndex = 10;
            this.label18.Text = "mm/s";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(9, 110);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(98, 18);
            this.label17.TabIndex = 8;
            this.label17.Text = "运动速度：";
            // 
            // nUDSpeed
            // 
            this.nUDSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nUDSpeed.Location = new System.Drawing.Point(136, 102);
            this.nUDSpeed.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nUDSpeed.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            131072});
            this.nUDSpeed.Name = "nUDSpeed";
            this.nUDSpeed.Size = new System.Drawing.Size(154, 28);
            this.nUDSpeed.TabIndex = 9;
            this.nUDSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUDSpeed.ThousandsSeparator = true;
            this.nUDSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nUDSpeed.ValueChanged += new System.EventHandler(this.nUDSpeed_ValueChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1038);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1246, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 15);
            // 
            // FrmCamera3DSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1246, 1060);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmCamera3DSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "3D相机参数配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCamera3DSetting_FormClosing);
            this.Load += new System.EventHandler(this.FrmCamera3DSetting_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.CCD_groupBox.ResumeLayout(false);
            this.gBConnectControl.ResumeLayout(false);
            this.gBConnectControl.PerformLayout();
            this.gBAcqControl.ResumeLayout(false);
            this.gBAcqControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nROIButtom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nROITop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDScanLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDExpo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDScanLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDAcqLineRate)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ZscaleUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YscaleUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XscaleUpDown)).EndInit();
            this.gBSensorInfo.ResumeLayout(false);
            this.gBSensorInfo.PerformLayout();
            this.gBEncoder.ResumeLayout(false);
            this.gBEncoder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDSpacing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDEncoderPitch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDSpeed)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SplitContainer splitContainer1;

        private GroupBox gBSensorInfo;

        private Label label6;

        private Label label5;

        private Label label4;

        private Label label2;

        private GroupBox gBConnectControl;

        private Button btnRecOrSto;

        private Button btnConnect;

        private ComboBox cbVendors;

        private Label label1;

        private SplitContainer splitContainer2;

        private Button btn_ClearDisplay;

        private Button btn_ZNegative;

        private Button btn_ZPositive;

        private GroupBox gBEncoder;

        private Label label9;

        private Label label8;

        private StatusStrip statusStrip1;

        private ToolStripStatusLabel toolStripStatusLabel1;

        private Label label12;

        private Label lbIP;

        private Label lbSerialNum;

        private Label lbVersion;

        private Label lbModel;

        private Label lbVendor;

        private Label lbTriggerSource;

        private GroupBox gBAcqControl;

        private ComboBox cbExposure;

        private Label label25;

        private Label label24;

        private NumericUpDown nUDScanLines;

        private Label label23;

        private ComboBox cbAcqLineRate;

        private Label label22;

        private Label label21;

        private NumericUpDown nUDExpo;

        private Label label16;

        private NumericUpDown nUDScanLength;

        private Label label15;

        private Label label14;

        private NumericUpDown nUDAcqLineRate;

        private Label label13;

        private Label label10;

        private ComboBox cbAcqMode;

        private TextBox tbIp;

        private Label label7;

        private NumericUpDown nUDSpacing;

        private Label label20;

        private NumericUpDown nUDEncoderPitch;

        private Label label19;

        private Label label18;

        private Label label17;

        private NumericUpDown nUDSpeed;

        private Button btnSnap;

        private Label label26;

        private NumericUpDown nUDTimeout;

        private Label label11;

        private Button button2;

        private Button btnAddCam;

        private TabControl tabControl1;

        private TabPage tabPage1;

        private TabPage tabPage2;

        private ImageDisplay imageDisplay1;

        private Button btnSaveParams;

        private GroupBox CCD_groupBox;

        private ListBox CCDList;

        private NumericUpDown ZscaleUpDown;

        private Label label28;

        private NumericUpDown YscaleUpDown;

        private Label label27;

        private NumericUpDown XscaleUpDown;

        private Label label3;

        private SplitContainer splitContainer3;

        private SplitContainer splitContainer4;

        private Button setCCDParamsInfo;

        private Label label31;

        private Label label30;

        private Label label29;

        private NumericUpDown nROIButtom;

        private Label label33;

        private NumericUpDown nROITop;

        private Label label32;
    }
}