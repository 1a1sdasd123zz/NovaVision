using System.Windows.Forms;
using NovaVision.UserControlLibrary;

namespace NovaVision.VisionForm.CarameFrm
{
    partial class FrmCameraLinear2DSetting
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
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cbCameras = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CCD_groupBox = new System.Windows.Forms.GroupBox();
            this.btnSaveParams = new System.Windows.Forms.Button();
            this.btnAddCam = new System.Windows.Forms.Button();
            this.CCDList = new System.Windows.Forms.ListBox();
            this.DelCameraSettings = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.nUD_LinePeriod = new System.Windows.Forms.NumericUpDown();
            this.nUD_TapNum = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.nUD_offsetX = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nUDTimeout = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nUD_AcqLineRate = new System.Windows.Forms.NumericUpDown();
            this.label22 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.nUD_timerDuration = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.nUD_scanHeight = new System.Windows.Forms.NumericUpDown();
            this.nUD_scanWidth = new System.Windows.Forms.NumericUpDown();
            this.nUD_gain = new System.Windows.Forms.NumericUpDown();
            this.nUD_exposure = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.StatusLabelInfo = new System.Windows.Forms.ToolStripLabel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnFreeze = new System.Windows.Forms.Button();
            this.btnGrab = new System.Windows.Forms.Button();
            this.btnSnap = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblIp = new System.Windows.Forms.Label();
            this.lblModel = new System.Windows.Forms.Label();
            this.lblVendor = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbRotaryDirection = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cbTriggerSelector = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.imageDisplay1 = new UserControlLibrary.ImageDisplay();
            ((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.splitContainer3).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.CCD_groupBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.nUD_LinePeriod).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nUD_TapNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nUD_offsetX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nUDTimeout).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nUD_AcqLineRate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nUD_timerDuration).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nUD_scanHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nUD_scanWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nUD_gain).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nUD_exposure).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.splitContainer2).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            base.SuspendLayout();
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(763, 617);
            this.splitContainer1.SplitterDistance = 262;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer3.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer3.Panel1.Controls.Add(this.CCD_groupBox);
            this.splitContainer3.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer3.Size = new System.Drawing.Size(262, 592);
            this.splitContainer3.SplitterDistance = 281;
            this.splitContainer3.TabIndex = 6;
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.cbCameras);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(251, 93);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "连接控制";
            this.button1.Location = new System.Drawing.Point(6, 59);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "查询设备";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(button1_Click);
            this.btnConnect.Location = new System.Drawing.Point(166, 59);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(68, 23);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(btnConnect_Click);
            this.cbCameras.FormattingEnabled = true;
            this.cbCameras.Location = new System.Drawing.Point(86, 27);
            this.cbCameras.Name = "cbCameras";
            this.cbCameras.Size = new System.Drawing.Size(154, 20);
            this.cbCameras.TabIndex = 1;
            this.cbCameras.SelectedIndexChanged += new System.EventHandler(cbCameras_SelectedIndexChanged);
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "相机序列号(含ModelName)：";
            this.CCD_groupBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CCD_groupBox.Controls.Add(this.btnSaveParams);
            this.CCD_groupBox.Controls.Add(this.btnAddCam);
            this.CCD_groupBox.Controls.Add(this.CCDList);
            this.CCD_groupBox.Controls.Add(this.DelCameraSettings);
            this.CCD_groupBox.Location = new System.Drawing.Point(3, 102);
            this.CCD_groupBox.Name = "CCD_groupBox";
            this.CCD_groupBox.Size = new System.Drawing.Size(251, 180);
            this.CCD_groupBox.TabIndex = 5;
            this.CCD_groupBox.TabStop = false;
            this.CCD_groupBox.Text = "已配置相机列表";
            this.btnSaveParams.Location = new System.Drawing.Point(8, 147);
            this.btnSaveParams.Name = "btnSaveParams";
            this.btnSaveParams.Size = new System.Drawing.Size(87, 23);
            this.btnSaveParams.TabIndex = 8;
            this.btnSaveParams.Text = "保存参数";
            this.btnSaveParams.UseVisualStyleBackColor = true;
            this.btnSaveParams.Click += new System.EventHandler(btnSaveParams_Click);
            this.btnAddCam.Location = new System.Drawing.Point(8, 147);
            this.btnAddCam.Name = "btnAddCam";
            this.btnAddCam.Size = new System.Drawing.Size(87, 23);
            this.btnAddCam.TabIndex = 7;
            this.btnAddCam.Text = "添加相机";
            this.btnAddCam.UseVisualStyleBackColor = true;
            this.btnAddCam.Click += new System.EventHandler(btnAddCam_Click);
            this.CCDList.Dock = System.Windows.Forms.DockStyle.Top;
            this.CCDList.FormattingEnabled = true;
            this.CCDList.ItemHeight = 12;
            this.CCDList.Location = new System.Drawing.Point(3, 17);
            this.CCDList.Name = "CCDList";
            this.CCDList.Size = new System.Drawing.Size(245, 124);
            this.CCDList.TabIndex = 9;
            this.DelCameraSettings.Location = new System.Drawing.Point(159, 147);
            this.DelCameraSettings.Name = "DelCameraSettings";
            this.DelCameraSettings.Size = new System.Drawing.Size(75, 23);
            this.DelCameraSettings.TabIndex = 8;
            this.DelCameraSettings.Text = "移除配置";
            this.DelCameraSettings.UseVisualStyleBackColor = true;
            this.DelCameraSettings.Click += new System.EventHandler(DelCameraSettings_Click);
            this.groupBox3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.nUD_LinePeriod);
            this.groupBox3.Controls.Add(this.nUD_TapNum);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.nUD_offsetX);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.nUDTimeout);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.nUD_AcqLineRate);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.nUD_timerDuration);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.nUD_scanHeight);
            this.groupBox3.Controls.Add(this.nUD_scanWidth);
            this.groupBox3.Controls.Add(this.nUD_gain);
            this.groupBox3.Controls.Add(this.nUD_exposure);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(255, 307);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "取像设置";
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(204, 246);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(17, 12);
            this.label24.TabIndex = 33;
            this.label24.Text = "us";
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(3, 246);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(113, 12);
            this.label21.TabIndex = 32;
            this.label21.Text = "线周期(IKap专用)：";
            this.nUD_LinePeriod.Location = new System.Drawing.Point(121, 241);
            this.nUD_LinePeriod.Maximum = new decimal(new int[4] { 64000, 0, 0, 0 });
            this.nUD_LinePeriod.Minimum = new decimal(new int[4] { 30, 0, 0, 0 });
            this.nUD_LinePeriod.Name = "nUD_LinePeriod";
            this.nUD_LinePeriod.Size = new System.Drawing.Size(76, 21);
            this.nUD_LinePeriod.TabIndex = 31;
            this.nUD_LinePeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUD_LinePeriod.Value = new decimal(new int[4] { 125, 0, 0, 0 });
            this.nUD_LinePeriod.ValueChanged += new System.EventHandler(UpdateCamSettings);
            this.nUD_TapNum.Location = new System.Drawing.Point(121, 214);
            this.nUD_TapNum.Maximum = new decimal(new int[4] { 5, 0, 0, 0 });
            this.nUD_TapNum.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
            this.nUD_TapNum.Name = "nUD_TapNum";
            this.nUD_TapNum.Size = new System.Drawing.Size(75, 21);
            this.nUD_TapNum.TabIndex = 30;
            this.nUD_TapNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUD_TapNum.Value = new decimal(new int[4] { 1, 0, 0, 0 });
            this.nUD_TapNum.ValueChanged += new System.EventHandler(UpdateCamSettings);
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 219);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(113, 12);
            this.label14.TabIndex = 29;
            this.label14.Text = "TapNum(IKap专用)：";
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(203, 138);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 28;
            this.label10.Text = "pixels";
            this.nUD_offsetX.Location = new System.Drawing.Point(121, 129);
            this.nUD_offsetX.Maximum = new decimal(new int[4] { 29145, 0, 0, 0 });
            this.nUD_offsetX.Name = "nUD_offsetX";
            this.nUD_offsetX.Size = new System.Drawing.Size(75, 21);
            this.nUD_offsetX.TabIndex = 27;
            this.nUD_offsetX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUD_offsetX.ValueChanged += new System.EventHandler(UpdateCamSettings);
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(2, 138);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(119, 12);
            this.label13.TabIndex = 26;
            this.label13.Text = "水平偏移(OffsetX)：";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 274);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 25;
            this.label2.Text = "ms";
            this.nUDTimeout.Location = new System.Drawing.Point(122, 268);
            this.nUDTimeout.Maximum = new decimal(new int[4] { 100000, 0, 0, 0 });
            this.nUDTimeout.Name = "nUDTimeout";
            this.nUDTimeout.Size = new System.Drawing.Size(75, 21);
            this.nUDTimeout.TabIndex = 24;
            this.nUDTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUDTimeout.ValueChanged += new System.EventHandler(nUDTimeout_ValueChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(75, 272);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 23;
            this.label5.Text = "超时：";
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(203, 196);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "Hz";
            this.nUD_AcqLineRate.Location = new System.Drawing.Point(122, 187);
            this.nUD_AcqLineRate.Maximum = new decimal(new int[4] { 45000, 0, 0, 0 });
            this.nUD_AcqLineRate.Minimum = new decimal(new int[4] { 100, 0, 0, 0 });
            this.nUD_AcqLineRate.Name = "nUD_AcqLineRate";
            this.nUD_AcqLineRate.Size = new System.Drawing.Size(75, 21);
            this.nUD_AcqLineRate.TabIndex = 21;
            this.nUD_AcqLineRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUD_AcqLineRate.Value = new decimal(new int[4] { 100, 0, 0, 0 });
            this.nUD_AcqLineRate.ValueChanged += new System.EventHandler(UpdateCamSettings);
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(51, 196);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(65, 12);
            this.label22.TabIndex = 20;
            this.label22.Text = "扫描行频：";
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(203, 167);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "us";
            this.nUD_timerDuration.Location = new System.Drawing.Point(122, 158);
            this.nUD_timerDuration.Maximum = new decimal(new int[4] { 100000, 0, 0, 0 });
            this.nUD_timerDuration.Name = "nUD_timerDuration";
            this.nUD_timerDuration.Size = new System.Drawing.Size(75, 21);
            this.nUD_timerDuration.TabIndex = 18;
            this.nUD_timerDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUD_timerDuration.ValueChanged += new System.EventHandler(UpdateCamSettings);
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 167);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "TimerDuration：";
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(203, 109);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(35, 12);
            this.label19.TabIndex = 14;
            this.label19.Text = "lines";
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(203, 80);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 12);
            this.label18.TabIndex = 13;
            this.label18.Text = "pixels";
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(202, 22);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(17, 12);
            this.label17.TabIndex = 12;
            this.label17.Text = "us";
            this.nUD_scanHeight.Location = new System.Drawing.Point(122, 100);
            this.nUD_scanHeight.Maximum = new decimal(new int[4] { 29145, 0, 0, 0 });
            this.nUD_scanHeight.Minimum = new decimal(new int[4] { 2, 0, 0, 0 });
            this.nUD_scanHeight.Name = "nUD_scanHeight";
            this.nUD_scanHeight.Size = new System.Drawing.Size(75, 21);
            this.nUD_scanHeight.TabIndex = 11;
            this.nUD_scanHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUD_scanHeight.Value = new decimal(new int[4] { 2, 0, 0, 0 });
            this.nUD_scanHeight.ValueChanged += new System.EventHandler(UpdateCamSettings);
            this.nUD_scanWidth.Location = new System.Drawing.Point(122, 71);
            this.nUD_scanWidth.Maximum = new decimal(new int[4] { 8192, 0, 0, 0 });
            this.nUD_scanWidth.Minimum = new decimal(new int[4] { 128, 0, 0, 0 });
            this.nUD_scanWidth.Name = "nUD_scanWidth";
            this.nUD_scanWidth.Size = new System.Drawing.Size(75, 21);
            this.nUD_scanWidth.TabIndex = 10;
            this.nUD_scanWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUD_scanWidth.Value = new decimal(new int[4] { 128, 0, 0, 0 });
            this.nUD_scanWidth.ValueChanged += new System.EventHandler(UpdateCamSettings);
            this.nUD_gain.Location = new System.Drawing.Point(122, 42);
            this.nUD_gain.Maximum = new decimal(new int[4] { 10, 0, 0, 0 });
            this.nUD_gain.Name = "nUD_gain";
            this.nUD_gain.Size = new System.Drawing.Size(75, 21);
            this.nUD_gain.TabIndex = 9;
            this.nUD_gain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUD_gain.Value = new decimal(new int[4] { 1, 0, 0, 0 });
            this.nUD_gain.ValueChanged += new System.EventHandler(UpdateCamSettings);
            this.nUD_exposure.Location = new System.Drawing.Point(122, 13);
            this.nUD_exposure.Maximum = new decimal(new int[4] { 300000, 0, 0, 0 });
            this.nUD_exposure.Minimum = new decimal(new int[4] { 4, 0, 0, 0 });
            this.nUD_exposure.Name = "nUD_exposure";
            this.nUD_exposure.Size = new System.Drawing.Size(75, 21);
            this.nUD_exposure.TabIndex = 8;
            this.nUD_exposure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUD_exposure.Value = new decimal(new int[4] { 4, 0, 0, 0 });
            this.nUD_exposure.ValueChanged += new System.EventHandler(UpdateCamSettings);
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 109);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(113, 12);
            this.label16.TabIndex = 3;
            this.label16.Text = "扫描高度(Height)：";
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 80);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(107, 12);
            this.label15.TabIndex = 2;
            this.label15.Text = "扫描宽度(Width)：";
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(75, 51);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 1;
            this.label12.Text = "增益：";
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(75, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "曝光：";
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.toolStripLabel1, this.StatusLabelInfo });
            this.toolStrip1.Location = new System.Drawing.Point(0, 592);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(262, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel1.Text = "状态：";
            this.StatusLabelInfo.Name = "StatusLabelInfo";
            this.StatusLabelInfo.Size = new System.Drawing.Size(31, 22);
            this.StatusLabelInfo.Text = "N/A";
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitContainer2.Panel1.Controls.Add(this.btnFreeze);
            this.splitContainer2.Panel1.Controls.Add(this.btnGrab);
            this.splitContainer2.Panel1.Controls.Add(this.btnSnap);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox4);
            this.splitContainer2.Panel2.Controls.Add(this.imageDisplay1);
            this.splitContainer2.Size = new System.Drawing.Size(497, 617);
            this.splitContainer2.SplitterDistance = 138;
            this.splitContainer2.TabIndex = 0;
            this.btnFreeze.Enabled = false;
            this.btnFreeze.Location = new System.Drawing.Point(329, 106);
            this.btnFreeze.Name = "btnFreeze";
            this.btnFreeze.Size = new System.Drawing.Size(75, 23);
            this.btnFreeze.TabIndex = 3;
            this.btnFreeze.Text = "停止实时";
            this.btnFreeze.UseVisualStyleBackColor = true;
            this.btnFreeze.Click += new System.EventHandler(btnFreeze_Click);
            this.btnGrab.Location = new System.Drawing.Point(194, 106);
            this.btnGrab.Name = "btnGrab";
            this.btnGrab.Size = new System.Drawing.Size(75, 23);
            this.btnGrab.TabIndex = 2;
            this.btnGrab.Text = "开启实时";
            this.btnGrab.UseVisualStyleBackColor = true;
            this.btnGrab.Click += new System.EventHandler(btnGrab_Click);
            this.btnSnap.Location = new System.Drawing.Point(59, 106);
            this.btnSnap.Name = "btnSnap";
            this.btnSnap.Size = new System.Drawing.Size(75, 23);
            this.btnSnap.TabIndex = 1;
            this.btnSnap.Text = "软触发一次";
            this.btnSnap.UseVisualStyleBackColor = true;
            this.btnSnap.Click += new System.EventHandler(btnSnap_Click);
            this.groupBox2.Controls.Add(this.lblIp);
            this.groupBox2.Controls.Add(this.lblModel);
            this.groupBox2.Controls.Add(this.lblVendor);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(253, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(228, 90);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "传感器信息";
            this.lblIp.AutoSize = true;
            this.lblIp.Location = new System.Drawing.Point(93, 73);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(23, 12);
            this.lblIp.TabIndex = 7;
            this.lblIp.Text = "N/A";
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(93, 48);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(23, 12);
            this.lblModel.TabIndex = 5;
            this.lblModel.Text = "N/A";
            this.lblVendor.AutoSize = true;
            this.lblVendor.Location = new System.Drawing.Point(93, 23);
            this.lblVendor.Name = "lblVendor";
            this.lblVendor.Size = new System.Drawing.Size(23, 12);
            this.lblVendor.TabIndex = 4;
            this.lblVendor.Text = "N/A";
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "传感器IP:";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "型号：";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "传感器品牌：";
            this.groupBox4.Controls.Add(this.cbRotaryDirection);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.cbTriggerSelector);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Location = new System.Drawing.Point(3, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(244, 91);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "采集控制";
            this.cbRotaryDirection.FormattingEnabled = true;
            this.cbRotaryDirection.Items.AddRange(new object[3] { "Not Enabled", "Clockwise", "CounterClockwise" });
            this.cbRotaryDirection.Location = new System.Drawing.Point(113, 52);
            this.cbRotaryDirection.Name = "cbRotaryDirection";
            this.cbRotaryDirection.Size = new System.Drawing.Size(123, 20);
            this.cbRotaryDirection.TabIndex = 7;
            this.cbRotaryDirection.SelectedIndexChanged += new System.EventHandler(cbRotaryDirection_SelectedIndexChanged);
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(40, 55);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(77, 12);
            this.label23.TabIndex = 5;
            this.label23.Text = "编码器方向：";
            this.cbTriggerSelector.FormattingEnabled = true;
            this.cbTriggerSelector.Items.AddRange(new object[5] { "Time-Line1", "Time-Software", "RotaryEncoder", "Test-Software", "RotaryEncoder_Hardware" });
            this.cbTriggerSelector.Location = new System.Drawing.Point(113, 20);
            this.cbTriggerSelector.Name = "cbTriggerSelector";
            this.cbTriggerSelector.Size = new System.Drawing.Size(121, 20);
            this.cbTriggerSelector.TabIndex = 2;
            this.cbTriggerSelector.SelectedIndexChanged += new System.EventHandler(cbTriggerSelector_SelectedIndexChanged);
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(4, 23);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(113, 12);
            this.label20.TabIndex = 0;
            this.label20.Text = "Trigger Selector：";
            this.imageDisplay1.BackColor = System.Drawing.Color.FromArgb(0, 140, 206);
            this.imageDisplay1.CogImage = null;
            this.imageDisplay1.DisplayName = "图_1";
            this.imageDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageDisplay1.Location = new System.Drawing.Point(0, 0);
            this.imageDisplay1.Name = "imageDisplay1";
            this.imageDisplay1.Record = null;
            this.imageDisplay1.Size = new System.Drawing.Size(497, 475);
            this.imageDisplay1.TabIndex = 0;
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(763, 617);
            base.Controls.Add(this.splitContainer1);
            base.Name = "FrmCameraLinear2DSetting";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "2D线扫相机参数配置";
            base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmCameraLinear2DSetting_FormClosing);
            base.Load += new System.EventHandler(CameraLinear2DSetting_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.splitContainer3).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.CCD_groupBox.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.nUD_LinePeriod).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nUD_TapNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nUD_offsetX).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nUDTimeout).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nUD_AcqLineRate).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nUD_timerDuration).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nUD_scanHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nUD_scanWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nUD_gain).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nUD_exposure).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.splitContainer2).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            base.ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;

        private GroupBox groupBox1;

        private Label label1;

        private ComboBox cbCameras;

        private GroupBox groupBox2;

        private Button btnConnect;

        private Label label3;

        private Label lblIp;

        private Label lblModel;

        private Label lblVendor;

        private Label label6;

        private Label label4;

        private GroupBox groupBox3;

        private Label label19;

        private Label label18;

        private Label label17;

        private NumericUpDown nUD_scanHeight;

        private NumericUpDown nUD_scanWidth;

        private NumericUpDown nUD_gain;

        private NumericUpDown nUD_exposure;

        private Label label16;

        private Label label15;

        private Label label12;

        private Label label11;

        private SplitContainer splitContainer2;

        private GroupBox groupBox4;

        private ComboBox cbRotaryDirection;

        private Label label23;

        private ComboBox cbTriggerSelector;

        private Label label20;

        private ToolStrip toolStrip1;

        private ToolStripLabel toolStripLabel1;

        private ToolStripLabel StatusLabelInfo;

        private ImageDisplay imageDisplay1;

        private Button btnFreeze;

        private Button btnGrab;

        private Button btnSnap;

        private Label label9;

        private NumericUpDown nUD_timerDuration;

        private Label label8;

        private Button button1;

        private Label label7;

        private NumericUpDown nUD_AcqLineRate;

        private Label label22;

        private Button btnAddCam;

        private Button DelCameraSettings;

        private Button btnSaveParams;

        private Label label2;

        private NumericUpDown nUDTimeout;

        private Label label5;

        private Label label10;

        private NumericUpDown nUD_offsetX;

        private Label label13;

        private GroupBox CCD_groupBox;

        private ListBox CCDList;

        private SplitContainer splitContainer3;

        private NumericUpDown nUD_TapNum;

        private Label label14;

        private Label label24;

        private Label label21;

        private NumericUpDown nUD_LinePeriod;
    }
}