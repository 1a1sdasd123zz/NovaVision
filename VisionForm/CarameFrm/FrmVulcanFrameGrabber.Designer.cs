using System.Windows.Forms;
using NovaVision.UserControlLibrary;

namespace NovaVision.VisionForm.CarameFrm
{
    partial class FrmVulcanFrameGrabber
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
            this.cbCameraVendor = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnOpenClose = new System.Windows.Forms.Button();
            this.checkConfigFile = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tbConfigfile = new System.Windows.Forms.TextBox();
            this.cbConfigfile = new System.Windows.Forms.ComboBox();
            this.gbLoc = new System.Windows.Forms.GroupBox();
            this.cbSerial = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tpBoardParam = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.gbBoard = new System.Windows.Forms.GroupBox();
            this.nudTapNum = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudBufferFrameCount = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cbMultiplier = new System.Windows.Forms.ComboBox();
            this.nudDrop = new System.Windows.Forms.NumericUpDown();
            this.nudFrameCount = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.tbPortName = new System.Windows.Forms.TextBox();
            this.nudTimeout = new System.Windows.Forms.NumericUpDown();
            this.cbWorkMode = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tpCameraParam = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.gbCamera = new System.Windows.Forms.GroupBox();
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
            ((System.ComponentModel.ISupportInitialize)this.nudTapNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nudBufferFrameCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nudDrop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nudFrameCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nudTimeout).BeginInit();
            this.tpCameraParam.SuspendLayout();
            this.gbCamera.SuspendLayout();
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
            this.imageDisplay1.BackColor = System.Drawing.Color.CornflowerBlue;
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
            this.gbConfigFile.Controls.Add(this.cbCameraVendor);
            this.gbConfigFile.Controls.Add(this.label13);
            this.gbConfigFile.Controls.Add(this.btnOpenClose);
            this.gbConfigFile.Controls.Add(this.checkConfigFile);
            this.gbConfigFile.Controls.Add(this.btnBrowse);
            this.gbConfigFile.Controls.Add(this.tbConfigfile);
            this.gbConfigFile.Controls.Add(this.cbConfigfile);
            this.gbConfigFile.Location = new System.Drawing.Point(10, 109);
            this.gbConfigFile.Name = "gbConfigFile";
            this.gbConfigFile.Size = new System.Drawing.Size(296, 230);
            this.gbConfigFile.TabIndex = 1;
            this.gbConfigFile.TabStop = false;
            this.cbCameraVendor.FormattingEnabled = true;
            this.cbCameraVendor.Items.AddRange(new object[3] { "Dalsa_CL", "Hikrobot_CL", "Itek_CL" });
            this.cbCameraVendor.Location = new System.Drawing.Point(129, 104);
            this.cbCameraVendor.Name = "cbCameraVendor";
            this.cbCameraVendor.Size = new System.Drawing.Size(144, 20);
            this.cbCameraVendor.TabIndex = 44;
            this.cbCameraVendor.SelectedIndexChanged += new System.EventHandler(cbCameraVendor_SelectedIndexChanged);
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(21, 107);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 12);
            this.label13.TabIndex = 43;
            this.label13.Text = "CameraVendor:";
            this.btnOpenClose.Location = new System.Drawing.Point(26, 130);
            this.btnOpenClose.Name = "btnOpenClose";
            this.btnOpenClose.Size = new System.Drawing.Size(75, 23);
            this.btnOpenClose.TabIndex = 0;
            this.btnOpenClose.Text = "Open Card";
            this.btnOpenClose.UseVisualStyleBackColor = true;
            this.btnOpenClose.Click += new System.EventHandler(btnOK_Click);
            this.checkConfigFile.AutoSize = true;
            this.checkConfigFile.Location = new System.Drawing.Point(26, 10);
            this.checkConfigFile.Name = "checkConfigFile";
            this.checkConfigFile.Size = new System.Drawing.Size(132, 16);
            this.checkConfigFile.TabIndex = 3;
            this.checkConfigFile.Text = "Configuration File";
            this.checkConfigFile.UseVisualStyleBackColor = true;
            this.checkConfigFile.CheckedChanged += new System.EventHandler(checkConfigFile_CheckedChanged);
            this.btnBrowse.Location = new System.Drawing.Point(188, 130);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(btnBrowse_Click);
            this.tbConfigfile.Location = new System.Drawing.Point(9, 72);
            this.tbConfigfile.Name = "tbConfigfile";
            this.tbConfigfile.Size = new System.Drawing.Size(264, 21);
            this.tbConfigfile.TabIndex = 1;
            this.cbConfigfile.FormattingEnabled = true;
            this.cbConfigfile.Location = new System.Drawing.Point(9, 32);
            this.cbConfigfile.Name = "cbConfigfile";
            this.cbConfigfile.Size = new System.Drawing.Size(264, 20);
            this.cbConfigfile.TabIndex = 0;
            this.cbConfigfile.SelectedIndexChanged += new System.EventHandler(cbConfigfile_SelectedIndexChanged);
            this.gbLoc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.gbLoc.BackColor = System.Drawing.SystemColors.Control;
            this.gbLoc.Controls.Add(this.cbSerial);
            this.gbLoc.Controls.Add(this.label1);
            this.gbLoc.Location = new System.Drawing.Point(10, 7);
            this.gbLoc.Name = "gbLoc";
            this.gbLoc.Size = new System.Drawing.Size(296, 96);
            this.gbLoc.TabIndex = 0;
            this.gbLoc.TabStop = false;
            this.gbLoc.Text = "Location";
            this.cbSerial.FormattingEnabled = true;
            this.cbSerial.Location = new System.Drawing.Point(9, 49);
            this.cbSerial.Name = "cbSerial";
            this.cbSerial.Size = new System.Drawing.Size(264, 20);
            this.cbSerial.TabIndex = 1;
            this.cbSerial.SelectedIndexChanged += new System.EventHandler(cbSerial_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Serial:";
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
            this.gbBoard.Controls.Add(this.nudTapNum);
            this.gbBoard.Controls.Add(this.label3);
            this.gbBoard.Controls.Add(this.nudBufferFrameCount);
            this.gbBoard.Controls.Add(this.label2);
            this.gbBoard.Controls.Add(this.cbMultiplier);
            this.gbBoard.Controls.Add(this.nudDrop);
            this.gbBoard.Controls.Add(this.nudFrameCount);
            this.gbBoard.Controls.Add(this.label14);
            this.gbBoard.Controls.Add(this.tbPortName);
            this.gbBoard.Controls.Add(this.nudTimeout);
            this.gbBoard.Controls.Add(this.cbWorkMode);
            this.gbBoard.Controls.Add(this.label12);
            this.gbBoard.Controls.Add(this.label11);
            this.gbBoard.Controls.Add(this.label10);
            this.gbBoard.Controls.Add(this.label9);
            this.gbBoard.Controls.Add(this.label8);
            this.gbBoard.Location = new System.Drawing.Point(16, 6);
            this.gbBoard.Name = "gbBoard";
            this.gbBoard.Size = new System.Drawing.Size(294, 409);
            this.gbBoard.TabIndex = 22;
            this.gbBoard.TabStop = false;
            this.nudTapNum.Location = new System.Drawing.Point(167, 245);
            this.nudTapNum.Maximum = new decimal(new int[4] { 3, 0, 0, 0 });
            this.nudTapNum.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
            this.nudTapNum.Name = "nudTapNum";
            this.nudTapNum.Size = new System.Drawing.Size(114, 21);
            this.nudTapNum.TabIndex = 49;
            this.nudTapNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudTapNum.Value = new decimal(new int[4] { 1, 0, 0, 0 });
            this.nudTapNum.ValueChanged += new System.EventHandler(nud_ValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 247);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 48;
            this.label3.Text = "TapNum:";
            this.nudBufferFrameCount.Location = new System.Drawing.Point(167, 180);
            this.nudBufferFrameCount.Maximum = new decimal(new int[4] { 1000, 0, 0, 0 });
            this.nudBufferFrameCount.Name = "nudBufferFrameCount";
            this.nudBufferFrameCount.Size = new System.Drawing.Size(114, 21);
            this.nudBufferFrameCount.TabIndex = 47;
            this.nudBufferFrameCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudBufferFrameCount.ValueChanged += new System.EventHandler(nud_ValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 12);
            this.label2.TabIndex = 46;
            this.label2.Text = "BufferFrameCount:";
            this.cbMultiplier.FormattingEnabled = true;
            this.cbMultiplier.Items.AddRange(new object[6] { "1", "2", "4", "8", "16", "32" });
            this.cbMultiplier.Location = new System.Drawing.Point(205, 54);
            this.cbMultiplier.Name = "cbMultiplier";
            this.cbMultiplier.Size = new System.Drawing.Size(76, 20);
            this.cbMultiplier.TabIndex = 45;
            this.cbMultiplier.SelectedIndexChanged += new System.EventHandler(cbMultiplier_SelectedIndexChanged);
            this.nudDrop.Location = new System.Drawing.Point(205, 24);
            this.nudDrop.Maximum = new decimal(new int[4] { 255, 0, 0, 0 });
            this.nudDrop.Name = "nudDrop";
            this.nudDrop.Size = new System.Drawing.Size(76, 21);
            this.nudDrop.TabIndex = 43;
            this.nudDrop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudDrop.ValueChanged += new System.EventHandler(nud_ValueChanged);
            this.nudFrameCount.Location = new System.Drawing.Point(167, 214);
            this.nudFrameCount.Maximum = new decimal(new int[4] { 1000, 0, 0, 0 });
            this.nudFrameCount.Name = "nudFrameCount";
            this.nudFrameCount.Size = new System.Drawing.Size(114, 21);
            this.nudFrameCount.TabIndex = 42;
            this.nudFrameCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudFrameCount.ValueChanged += new System.EventHandler(nud_ValueChanged);
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 217);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 12);
            this.label14.TabIndex = 41;
            this.label14.Text = "FrameCount:";
            this.tbPortName.Location = new System.Drawing.Point(108, 150);
            this.tbPortName.Name = "tbPortName";
            this.tbPortName.Size = new System.Drawing.Size(173, 21);
            this.tbPortName.TabIndex = 40;
            this.tbPortName.Text = "COM13";
            this.tbPortName.TextChanged += new System.EventHandler(tbPortName_TextChanged);
            this.nudTimeout.Location = new System.Drawing.Point(167, 117);
            this.nudTimeout.Maximum = new decimal(new int[4] { 100000000, 0, 0, 0 });
            this.nudTimeout.Name = "nudTimeout";
            this.nudTimeout.Size = new System.Drawing.Size(114, 21);
            this.nudTimeout.TabIndex = 39;
            this.nudTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudTimeout.ValueChanged += new System.EventHandler(nud_ValueChanged);
            this.cbWorkMode.FormattingEnabled = true;
            this.cbWorkMode.Items.AddRange(new object[6] { "FreeRun", "Time_Software", "Time_Hardware", "ShaftEncoder_Software", "ShaftEncoder_Hardware", "ShaftEncoder_Burst" });
            this.cbWorkMode.Location = new System.Drawing.Point(108, 83);
            this.cbWorkMode.Name = "cbWorkMode";
            this.cbWorkMode.Size = new System.Drawing.Size(173, 20);
            this.cbWorkMode.TabIndex = 38;
            this.cbWorkMode.SelectedIndexChanged += new System.EventHandler(cbWorkMode_SelectedIndexChanged);
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 121);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 12);
            this.label12.TabIndex = 30;
            this.label12.Text = "Timeout(ms):";
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 153);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 29;
            this.label11.Text = "PortName:";
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 85);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 28;
            this.label10.Text = "WorkMode:";
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(185, 12);
            this.label9.TabIndex = 27;
            this.label9.Text = "Shaft Encoder Edge Multiplier:";
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(149, 12);
            this.label8.TabIndex = 26;
            this.label8.Text = "Shaft Encoder Edge Drop:";
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
            this.label22.Location = new System.Drawing.Point(4, 80);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(89, 12);
            this.label22.TabIndex = 23;
            this.label22.Text = "ScanDirection:";
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(4, 51);
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
            base.Name = "FrmVulcanFrameGrabber";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IKap采集卡配置界面";
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
            ((System.ComponentModel.ISupportInitialize)this.nudTapNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nudBufferFrameCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nudDrop).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nudFrameCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nudTimeout).EndInit();
            this.tpCameraParam.ResumeLayout(false);
            this.gbCamera.ResumeLayout(false);
            this.gbCamera.PerformLayout();
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

        private ComboBox cbCameraVendor;

        private Label label13;

        private Button btnOpenClose;

        private CheckBox checkConfigFile;

        private Button btnBrowse;

        private TextBox tbConfigfile;

        private ComboBox cbConfigfile;

        private GroupBox gbLoc;

        private ComboBox cbSerial;

        private Label label1;

        private TabPage tpBoardParam;

        private Button button3;

        private GroupBox gbBoard;

        private NumericUpDown nudDrop;

        private NumericUpDown nudFrameCount;

        private Label label14;

        private TextBox tbPortName;

        private NumericUpDown nudTimeout;

        private ComboBox cbWorkMode;

        private Label label12;

        private Label label11;

        private Label label10;

        private Label label9;

        private Label label8;

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

        private ComboBox cbMultiplier;

        private NumericUpDown nudBufferFrameCount;

        private Label label2;

        private NumericUpDown nudTapNum;

        private Label label3;
    }
}