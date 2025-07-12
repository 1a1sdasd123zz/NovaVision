using System.Windows.Forms;
using NovaVision.UserControlLibrary;

namespace NovaVision.VisionForm.CarameFrm
{
    partial class FrmHikFrameGrabber
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.imageDisplay1 = new UserControlLibrary.ImageDisplay();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpMain = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnBoard = new System.Windows.Forms.Button();
            this.btnCamera = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.Button();
            this.listDevice = new System.Windows.Forms.ListBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tpConfig = new System.Windows.Forms.TabPage();
            this.button4 = new System.Windows.Forms.Button();
            this.gbControl = new System.Windows.Forms.GroupBox();
            this.btnStopGrab = new System.Windows.Forms.Button();
            this.btnStartGrab = new System.Windows.Forms.Button();
            this.btnSnap = new System.Windows.Forms.Button();
            this.gbConfigFile = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnOpenClose = new System.Windows.Forms.Button();
            this.gbLoc = new System.Windows.Forms.GroupBox();
            this.cbCameraSerial = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tpBoardParam = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.gbBoard = new System.Windows.Forms.GroupBox();
            this.nudTimeout = new System.Windows.Forms.NumericUpDown();
            this.cbWorkMode = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tpCameraParam = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.gbCamera = new System.Windows.Forms.GroupBox();
            this.nudScanHeight = new System.Windows.Forms.NumericUpDown();
            this.nudScanWidth = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbScanDirection = new System.Windows.Forms.ComboBox();
            this.nudGain = new System.Windows.Forms.NumericUpDown();
            this.nudExposure = new System.Windows.Forms.NumericUpDown();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslElapsedTime = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpMain.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tpConfig.SuspendLayout();
            this.gbControl.SuspendLayout();
            this.gbConfigFile.SuspendLayout();
            this.gbLoc.SuspendLayout();
            this.tpBoardParam.SuspendLayout();
            this.gbBoard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.nudTimeout).BeginInit();
            this.tpCameraParam.SuspendLayout();
            this.gbCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.nudScanHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nudScanWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nudGain).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nudExposure).BeginInit();
            this.statusStrip1.SuspendLayout();
            base.SuspendLayout();
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.imageDisplay1);
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(987, 591);
            this.splitContainer1.SplitterDistance = 652;
            this.splitContainer1.TabIndex = 0;
            this.imageDisplay1.BackColor = System.Drawing.Color.FromArgb(0, 140, 206);
            this.imageDisplay1.CogImage = null;
            this.imageDisplay1.DisplayName = "Vulcan";
            this.imageDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageDisplay1.Location = new System.Drawing.Point(0, 0);
            this.imageDisplay1.Margin = new System.Windows.Forms.Padding(4);
            this.imageDisplay1.Name = "imageDisplay1";
            this.imageDisplay1.Record = null;
            this.imageDisplay1.Size = new System.Drawing.Size(652, 591);
            this.imageDisplay1.TabIndex = 1;
            this.tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.tabControl1.Controls.Add(this.tpMain);
            this.tabControl1.Controls.Add(this.tpConfig);
            this.tabControl1.Controls.Add(this.tpBoardParam);
            this.tabControl1.Controls.Add(this.tpCameraParam);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(322, 563);
            this.tabControl1.TabIndex = 3;
            this.tpMain.Controls.Add(this.groupBox3);
            this.tpMain.Location = new System.Drawing.Point(4, 22);
            this.tpMain.Name = "tpMain";
            this.tpMain.Size = new System.Drawing.Size(314, 537);
            this.tpMain.TabIndex = 2;
            this.tpMain.Text = "MainPage";
            this.tpMain.UseVisualStyleBackColor = true;
            this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Controls.Add(this.btnBoard);
            this.groupBox3.Controls.Add(this.btnCamera);
            this.groupBox3.Controls.Add(this.btnConfig);
            this.groupBox3.Controls.Add(this.listDevice);
            this.groupBox3.Controls.Add(this.btnRemove);
            this.groupBox3.Controls.Add(this.btnAdd);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(309, 534);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "已添加设备";
            this.btnSave.Location = new System.Drawing.Point(88, 470);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(124, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save Settings";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(btnSave_Click);
            this.btnBoard.Location = new System.Drawing.Point(88, 393);
            this.btnBoard.Name = "btnBoard";
            this.btnBoard.Size = new System.Drawing.Size(124, 23);
            this.btnBoard.TabIndex = 5;
            this.btnBoard.Text = "To BoardParams";
            this.btnBoard.UseVisualStyleBackColor = true;
            this.btnBoard.Click += new System.EventHandler(btnBoard_Click);
            this.btnCamera.Location = new System.Drawing.Point(88, 431);
            this.btnCamera.Name = "btnCamera";
            this.btnCamera.Size = new System.Drawing.Size(124, 23);
            this.btnCamera.TabIndex = 4;
            this.btnCamera.Text = "To CameraParams";
            this.btnCamera.UseVisualStyleBackColor = true;
            this.btnCamera.Click += new System.EventHandler(btnCamera_Click);
            this.btnConfig.Location = new System.Drawing.Point(88, 355);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(124, 23);
            this.btnConfig.TabIndex = 3;
            this.btnConfig.Text = "To Configuration";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(btnConfig_Click);
            this.listDevice.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.listDevice.FormattingEnabled = true;
            this.listDevice.ItemHeight = 12;
            this.listDevice.Location = new System.Drawing.Point(14, 23);
            this.listDevice.Name = "listDevice";
            this.listDevice.Size = new System.Drawing.Size(283, 268);
            this.listDevice.TabIndex = 0;
            this.btnRemove.Location = new System.Drawing.Point(159, 314);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(94, 23);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "Remove Device";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(btnRemove_Click);
            this.btnAdd.Location = new System.Drawing.Point(44, 314);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add Device";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(btnAdd_Click);
            this.tpConfig.AllowDrop = true;
            this.tpConfig.Controls.Add(this.button4);
            this.tpConfig.Controls.Add(this.gbControl);
            this.tpConfig.Controls.Add(this.gbConfigFile);
            this.tpConfig.Controls.Add(this.gbLoc);
            this.tpConfig.Location = new System.Drawing.Point(4, 22);
            this.tpConfig.Name = "tpConfig";
            this.tpConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpConfig.Size = new System.Drawing.Size(314, 537);
            this.tpConfig.TabIndex = 0;
            this.tpConfig.Text = "Configuration";
            this.tpConfig.UseVisualStyleBackColor = true;
            this.button4.Location = new System.Drawing.Point(116, 451);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(81, 40);
            this.button4.TabIndex = 26;
            this.button4.Text = "返回主界面";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(btnBackToMenu_Click);
            this.gbControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.gbControl.BackColor = System.Drawing.SystemColors.Control;
            this.gbControl.Controls.Add(this.btnStopGrab);
            this.gbControl.Controls.Add(this.btnStartGrab);
            this.gbControl.Controls.Add(this.btnSnap);
            this.gbControl.Location = new System.Drawing.Point(10, 345);
            this.gbControl.Name = "gbControl";
            this.gbControl.Size = new System.Drawing.Size(296, 80);
            this.gbControl.TabIndex = 2;
            this.gbControl.TabStop = false;
            this.gbControl.Text = "Control";
            this.btnStopGrab.Location = new System.Drawing.Point(203, 36);
            this.btnStopGrab.Name = "btnStopGrab";
            this.btnStopGrab.Size = new System.Drawing.Size(75, 23);
            this.btnStopGrab.TabIndex = 2;
            this.btnStopGrab.Text = "Stop Grab";
            this.btnStopGrab.UseVisualStyleBackColor = true;
            this.btnStopGrab.Click += new System.EventHandler(btnStopGrab_Click);
            this.btnStartGrab.Location = new System.Drawing.Point(106, 36);
            this.btnStartGrab.Name = "btnStartGrab";
            this.btnStartGrab.Size = new System.Drawing.Size(75, 23);
            this.btnStartGrab.TabIndex = 1;
            this.btnStartGrab.Text = "Start Grab";
            this.btnStartGrab.UseVisualStyleBackColor = true;
            this.btnStartGrab.Click += new System.EventHandler(btnStartGrab_Click);
            this.btnSnap.Location = new System.Drawing.Point(11, 36);
            this.btnSnap.Name = "btnSnap";
            this.btnSnap.Size = new System.Drawing.Size(75, 23);
            this.btnSnap.TabIndex = 0;
            this.btnSnap.Text = "Snap";
            this.btnSnap.UseVisualStyleBackColor = true;
            this.btnSnap.Click += new System.EventHandler(btnSnap_Click);
            this.gbConfigFile.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.gbConfigFile.BackColor = System.Drawing.SystemColors.Control;
            this.gbConfigFile.Controls.Add(this.button5);
            this.gbConfigFile.Controls.Add(this.button1);
            this.gbConfigFile.Controls.Add(this.radioButton1);
            this.gbConfigFile.Controls.Add(this.listBox1);
            this.gbConfigFile.Controls.Add(this.btnOpenClose);
            this.gbConfigFile.Location = new System.Drawing.Point(10, 109);
            this.gbConfigFile.Name = "gbConfigFile";
            this.gbConfigFile.Size = new System.Drawing.Size(296, 230);
            this.gbConfigFile.TabIndex = 1;
            this.gbConfigFile.TabStop = false;
            this.button5.Location = new System.Drawing.Point(190, 167);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(88, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "枚举卡";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(button5_Click);
            this.button1.Location = new System.Drawing.Point(190, 196);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "保存XML";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(button1_Click);
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(11, 32);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(125, 16);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "保存采集卡参数XML";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(radioButton1_CheckedChanged);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(11, 71);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(170, 148);
            this.listBox1.TabIndex = 1;
            this.listBox1.Visible = false;
            this.btnOpenClose.Location = new System.Drawing.Point(187, 29);
            this.btnOpenClose.Name = "btnOpenClose";
            this.btnOpenClose.Size = new System.Drawing.Size(91, 23);
            this.btnOpenClose.TabIndex = 0;
            this.btnOpenClose.Text = "打开相机";
            this.btnOpenClose.UseVisualStyleBackColor = true;
            this.btnOpenClose.Click += new System.EventHandler(btnOK_Click);
            this.gbLoc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.gbLoc.BackColor = System.Drawing.SystemColors.Control;
            this.gbLoc.Controls.Add(this.cbCameraSerial);
            this.gbLoc.Controls.Add(this.label4);
            this.gbLoc.Location = new System.Drawing.Point(10, 7);
            this.gbLoc.Name = "gbLoc";
            this.gbLoc.Size = new System.Drawing.Size(296, 96);
            this.gbLoc.TabIndex = 0;
            this.gbLoc.TabStop = false;
            this.gbLoc.Text = "Location";
            this.cbCameraSerial.FormattingEnabled = true;
            this.cbCameraSerial.Location = new System.Drawing.Point(11, 54);
            this.cbCameraSerial.Name = "cbCameraSerial";
            this.cbCameraSerial.Size = new System.Drawing.Size(264, 20);
            this.cbCameraSerial.TabIndex = 2;
            this.cbCameraSerial.SelectedIndexChanged += new System.EventHandler(cbCameraSerial_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "DeviceSN:";
            this.tpBoardParam.Controls.Add(this.button3);
            this.tpBoardParam.Controls.Add(this.gbBoard);
            this.tpBoardParam.Location = new System.Drawing.Point(4, 22);
            this.tpBoardParam.Name = "tpBoardParam";
            this.tpBoardParam.Padding = new System.Windows.Forms.Padding(3);
            this.tpBoardParam.Size = new System.Drawing.Size(314, 537);
            this.tpBoardParam.TabIndex = 1;
            this.tpBoardParam.Text = "BoardParams";
            this.tpBoardParam.UseVisualStyleBackColor = true;
            this.button3.Location = new System.Drawing.Point(124, 457);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(81, 40);
            this.button3.TabIndex = 25;
            this.button3.Text = "返回主界面";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(btnBackToMenu_Click);
            this.gbBoard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.gbBoard.Controls.Add(this.nudTimeout);
            this.gbBoard.Controls.Add(this.cbWorkMode);
            this.gbBoard.Controls.Add(this.label12);
            this.gbBoard.Controls.Add(this.label10);
            this.gbBoard.Location = new System.Drawing.Point(14, 6);
            this.gbBoard.Name = "gbBoard";
            this.gbBoard.Size = new System.Drawing.Size(294, 409);
            this.gbBoard.TabIndex = 22;
            this.gbBoard.TabStop = false;
            this.nudTimeout.Location = new System.Drawing.Point(167, 62);
            this.nudTimeout.Maximum = new decimal(new int[4] { 100000000, 0, 0, 0 });
            this.nudTimeout.Name = "nudTimeout";
            this.nudTimeout.Size = new System.Drawing.Size(114, 21);
            this.nudTimeout.TabIndex = 39;
            this.nudTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudTimeout.ValueChanged += new System.EventHandler(nud_ValueChanged);
            this.cbWorkMode.FormattingEnabled = true;
            this.cbWorkMode.Items.AddRange(new object[5] { "FreeRun", "Time_Software", "Time_Hardware", "ShaftEncoder_Software", "ShaftEncoder_Hardware" });
            this.cbWorkMode.Location = new System.Drawing.Point(108, 29);
            this.cbWorkMode.Name = "cbWorkMode";
            this.cbWorkMode.Size = new System.Drawing.Size(173, 20);
            this.cbWorkMode.TabIndex = 38;
            this.cbWorkMode.SelectedIndexChanged += new System.EventHandler(cbWorkMode_SelectedIndexChanged);
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 64);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 12);
            this.label12.TabIndex = 30;
            this.label12.Text = "Timeout(ms):";
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 28;
            this.label10.Text = "WorkMode:";
            this.tpCameraParam.Controls.Add(this.button2);
            this.tpCameraParam.Controls.Add(this.gbCamera);
            this.tpCameraParam.Location = new System.Drawing.Point(4, 22);
            this.tpCameraParam.Name = "tpCameraParam";
            this.tpCameraParam.Size = new System.Drawing.Size(314, 537);
            this.tpCameraParam.TabIndex = 3;
            this.tpCameraParam.Text = "CameraParams";
            this.tpCameraParam.UseVisualStyleBackColor = true;
            this.button2.Location = new System.Drawing.Point(124, 434);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(81, 40);
            this.button2.TabIndex = 26;
            this.button2.Text = "返回主界面";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(btnBackToMenu_Click);
            this.gbCamera.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.gbCamera.Controls.Add(this.nudScanHeight);
            this.gbCamera.Controls.Add(this.nudScanWidth);
            this.gbCamera.Controls.Add(this.label3);
            this.gbCamera.Controls.Add(this.label2);
            this.gbCamera.Controls.Add(this.cbScanDirection);
            this.gbCamera.Controls.Add(this.nudGain);
            this.gbCamera.Controls.Add(this.nudExposure);
            this.gbCamera.Controls.Add(this.label22);
            this.gbCamera.Controls.Add(this.label23);
            this.gbCamera.Controls.Add(this.label24);
            this.gbCamera.Location = new System.Drawing.Point(14, 14);
            this.gbCamera.Name = "gbCamera";
            this.gbCamera.Size = new System.Drawing.Size(287, 354);
            this.gbCamera.TabIndex = 23;
            this.gbCamera.TabStop = false;
            this.nudScanHeight.Location = new System.Drawing.Point(165, 141);
            this.nudScanHeight.Maximum = new decimal(new int[4] { 100000, 0, 0, 0 });
            this.nudScanHeight.Name = "nudScanHeight";
            this.nudScanHeight.Size = new System.Drawing.Size(116, 21);
            this.nudScanHeight.TabIndex = 37;
            this.nudScanHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudScanHeight.Value = new decimal(new int[4] { 10000, 0, 0, 0 });
            this.nudScanHeight.ValueChanged += new System.EventHandler(nud_ValueChanged);
            this.nudScanWidth.Location = new System.Drawing.Point(165, 109);
            this.nudScanWidth.Maximum = new decimal(new int[4] { 100000, 0, 0, 0 });
            this.nudScanWidth.Name = "nudScanWidth";
            this.nudScanWidth.Size = new System.Drawing.Size(116, 21);
            this.nudScanWidth.TabIndex = 36;
            this.nudScanWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudScanWidth.Value = new decimal(new int[4] { 4096, 0, 0, 0 });
            this.nudScanWidth.ValueChanged += new System.EventHandler(nud_ValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 35;
            this.label3.Text = "ScanHeight:";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 34;
            this.label2.Text = "ScanWidth:";
            this.cbScanDirection.FormattingEnabled = true;
            this.cbScanDirection.Items.AddRange(new object[2] { "0-TopToBottom", "1-BottomToTop" });
            this.cbScanDirection.Location = new System.Drawing.Point(165, 80);
            this.cbScanDirection.Name = "cbScanDirection";
            this.cbScanDirection.Size = new System.Drawing.Size(116, 20);
            this.cbScanDirection.TabIndex = 33;
            this.cbScanDirection.SelectedIndexChanged += new System.EventHandler(cbScanDirection_SelectedIndexChanged);
            this.nudGain.DecimalPlaces = 1;
            this.nudGain.Location = new System.Drawing.Point(165, 48);
            this.nudGain.Name = "nudGain";
            this.nudGain.Size = new System.Drawing.Size(116, 21);
            this.nudGain.TabIndex = 32;
            this.nudGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudGain.Value = new decimal(new int[4] { 1, 0, 0, 0 });
            this.nudGain.ValueChanged += new System.EventHandler(nud_ValueChanged);
            this.nudExposure.Location = new System.Drawing.Point(165, 19);
            this.nudExposure.Maximum = new decimal(new int[4] { 10000, 0, 0, 0 });
            this.nudExposure.Name = "nudExposure";
            this.nudExposure.Size = new System.Drawing.Size(116, 21);
            this.nudExposure.TabIndex = 31;
            this.nudExposure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudExposure.Value = new decimal(new int[4] { 50, 0, 0, 0 });
            this.nudExposure.ValueChanged += new System.EventHandler(nud_ValueChanged);
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 80);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(89, 12);
            this.label22.TabIndex = 23;
            this.label22.Text = "ScanDirection:";
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 50);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(35, 12);
            this.label23.TabIndex = 22;
            this.label23.Text = "Gain:";
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 23);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(107, 12);
            this.label24.TabIndex = 0;
            this.label24.Text = "ExposureTime(us):";
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.tsslStatus, this.toolStripStatusLabel1, this.tsslElapsedTime });
            this.statusStrip1.Location = new System.Drawing.Point(0, 569);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(331, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(31, 17);
            this.tsslStatus.Text = "N/A";
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(35, 17);
            this.toolStripStatusLabel1.Text = "耗时:";
            this.tsslElapsedTime.Name = "tsslElapsedTime";
            this.tsslElapsedTime.Size = new System.Drawing.Size(0, 17);
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(987, 591);
            base.Controls.Add(this.splitContainer1);
            base.Margin = new System.Windows.Forms.Padding(2);
            base.Name = "FrmHikFrameGrabber";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hik采集卡配置界面";
            base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmFrameGrabberSetting_FormClosing);
            base.Load += new System.EventHandler(FrmFrameGrabberSetting_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpMain.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tpConfig.ResumeLayout(false);
            this.gbControl.ResumeLayout(false);
            this.gbConfigFile.ResumeLayout(false);
            this.gbConfigFile.PerformLayout();
            this.gbLoc.ResumeLayout(false);
            this.gbLoc.PerformLayout();
            this.tpBoardParam.ResumeLayout(false);
            this.gbBoard.ResumeLayout(false);
            this.gbBoard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.nudTimeout).EndInit();
            this.tpCameraParam.ResumeLayout(false);
            this.gbCamera.ResumeLayout(false);
            this.gbCamera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.nudScanHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nudScanWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nudGain).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nudExposure).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            base.ResumeLayout(false);
        }

        #endregion

        private FolderBrowserDialog folderBrowserDialog1;

        private SplitContainer splitContainer1;

        private ImageDisplay imageDisplay1;

        private TabControl tabControl1;

        private TabPage tpMain;

        private GroupBox groupBox3;

        private Button btnSave;

        private Button btnBoard;

        private Button btnCamera;

        private Button btnConfig;

        private ListBox listDevice;

        private Button btnRemove;

        private Button btnAdd;

        private TabPage tpConfig;

        private Button button4;

        private GroupBox gbControl;

        private Button btnStopGrab;

        private Button btnStartGrab;

        private Button btnSnap;

        private GroupBox gbConfigFile;

        private Button btnOpenClose;

        private GroupBox gbLoc;

        private TabPage tpBoardParam;

        private Button button3;

        private GroupBox gbBoard;

        private NumericUpDown nudTimeout;

        private ComboBox cbWorkMode;

        private Label label12;

        private Label label10;

        private TabPage tpCameraParam;

        private Button button2;

        private GroupBox gbCamera;

        private ComboBox cbScanDirection;

        private NumericUpDown nudGain;

        private NumericUpDown nudExposure;

        private Label label22;

        private Label label23;

        private Label label24;

        private StatusStrip statusStrip1;

        private ToolStripStatusLabel tsslStatus;

        private ToolStripStatusLabel toolStripStatusLabel1;

        private ToolStripStatusLabel tsslElapsedTime;

        private ComboBox cbCameraSerial;

        private NumericUpDown nudScanHeight;

        private NumericUpDown nudScanWidth;

        private Label label3;

        private Label label2;

        private Label label4;

        private RadioButton radioButton1;

        private ListBox listBox1;

        private Button button1;

        private Button button5;
    }
}