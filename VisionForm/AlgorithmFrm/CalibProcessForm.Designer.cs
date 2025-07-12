namespace NovaVision.VisionForm.AlgorithmFrm
{
    partial class CalibProcessForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalibProcessForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Apply = new System.Windows.Forms.Button();
            this.btn_SaveFile = new System.Windows.Forms.Button();
            this.btnClearData = new System.Windows.Forms.Button();
            this.btnLoadExcel = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_OpenFile = new System.Windows.Forms.Button();
            this.btn_OpenDisplayShow = new System.Windows.Forms.Button();
            this.btn_SoftOnce = new System.Windows.Forms.Button();
            this.cmb_CameraName = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_GenerateNPoint = new System.Windows.Forms.Button();
            this.btn_OpenProcess = new System.Windows.Forms.Button();
            this.btn_OpenNpointTool = new System.Windows.Forms.Button();
            this.btn_RunProcess = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_GenerateCheckboard = new System.Windows.Forms.Button();
            this.btn_OpenCheckboardTool = new System.Windows.Forms.Button();
            this.cb_IsUseCheckboard = new System.Windows.Forms.CheckBox();
            this.gb_ToolShowName = new System.Windows.Forms.GroupBox();
            this.cogRecordDisplay1 = new Cognex.VisionPro.CogRecordDisplay();
            this.cogCalibNPointToNPointEditV21 = new Cognex.VisionPro.CalibFix.CogCalibNPointToNPointEditV2();
            this.cogCalibCheckerboardEditV21 = new Cognex.VisionPro.CalibFix.CogCalibCheckerboardEditV2();
            this.cogToolBlockEditV21 = new Cognex.VisionPro.ToolBlock.CogToolBlockEditV2();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gb_ToolShowName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cogCalibNPointToNPointEditV21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cogCalibCheckerboardEditV21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cogToolBlockEditV21)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gb_ToolShowName);
            this.splitContainer1.Size = new System.Drawing.Size(1513, 1021);
            this.splitContainer1.SplitterDistance = 589;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(589, 1021);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.SkyBlue;
            this.groupBox4.Controls.Add(this.panel2);
            this.groupBox4.Controls.Add(this.btn_SaveFile);
            this.groupBox4.Controls.Add(this.dataGridView);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 584);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(583, 434);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "物理坐标Excel表加载";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_Apply);
            this.panel2.Controls.Add(this.btnClearData);
            this.panel2.Controls.Add(this.btnLoadExcel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 373);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(577, 58);
            this.panel2.TabIndex = 6;
            // 
            // btn_Apply
            // 
            this.btn_Apply.BackColor = System.Drawing.Color.Turquoise;
            this.btn_Apply.Location = new System.Drawing.Point(420, 10);
            this.btn_Apply.Name = "btn_Apply";
            this.btn_Apply.Size = new System.Drawing.Size(120, 40);
            this.btn_Apply.TabIndex = 5;
            this.btn_Apply.Text = "应用";
            this.btn_Apply.UseVisualStyleBackColor = false;
            this.btn_Apply.Click += new System.EventHandler(this.btn_Apply_Click);
            // 
            // btn_SaveFile
            // 
            this.btn_SaveFile.BackColor = System.Drawing.Color.Cyan;
            this.btn_SaveFile.Location = new System.Drawing.Point(1062, 365);
            this.btn_SaveFile.Name = "btn_SaveFile";
            this.btn_SaveFile.Size = new System.Drawing.Size(140, 38);
            this.btn_SaveFile.TabIndex = 4;
            this.btn_SaveFile.Text = "保存配置文件";
            this.btn_SaveFile.UseVisualStyleBackColor = false;
            // 
            // btnClearData
            // 
            this.btnClearData.BackColor = System.Drawing.Color.Turquoise;
            this.btnClearData.Location = new System.Drawing.Point(220, 10);
            this.btnClearData.Name = "btnClearData";
            this.btnClearData.Size = new System.Drawing.Size(120, 40);
            this.btnClearData.TabIndex = 2;
            this.btnClearData.Text = "清除表格";
            this.btnClearData.UseVisualStyleBackColor = false;
            this.btnClearData.Click += new System.EventHandler(this.btnClearData_Click);
            // 
            // btnLoadExcel
            // 
            this.btnLoadExcel.BackColor = System.Drawing.Color.Turquoise;
            this.btnLoadExcel.Location = new System.Drawing.Point(20, 10);
            this.btnLoadExcel.Name = "btnLoadExcel";
            this.btnLoadExcel.Size = new System.Drawing.Size(120, 40);
            this.btnLoadExcel.TabIndex = 1;
            this.btnLoadExcel.Text = "加载表格";
            this.btnLoadExcel.UseVisualStyleBackColor = false;
            this.btnLoadExcel.Click += new System.EventHandler(this.btnLoadExcel_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.Color.LightCyan;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView.Location = new System.Drawing.Point(3, 24);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 62;
            this.dataGridView.RowTemplate.Height = 30;
            this.dataGridView.Size = new System.Drawing.Size(577, 337);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValueChanged);
            this.dataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView_DataBindingComplete);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.SkyBlue;
            this.groupBox1.Controls.Add(this.btn_OpenFile);
            this.groupBox1.Controls.Add(this.btn_OpenDisplayShow);
            this.groupBox1.Controls.Add(this.btn_SoftOnce);
            this.groupBox1.Controls.Add(this.cmb_CameraName);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(583, 96);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "相机选择";
            // 
            // btn_OpenFile
            // 
            this.btn_OpenFile.AutoSize = true;
            this.btn_OpenFile.BackColor = System.Drawing.Color.Turquoise;
            this.btn_OpenFile.Location = new System.Drawing.Point(301, 54);
            this.btn_OpenFile.Name = "btn_OpenFile";
            this.btn_OpenFile.Size = new System.Drawing.Size(126, 30);
            this.btn_OpenFile.TabIndex = 3;
            this.btn_OpenFile.Text = "选择本地图像";
            this.btn_OpenFile.UseVisualStyleBackColor = false;
            // 
            // btn_OpenDisplayShow
            // 
            this.btn_OpenDisplayShow.AutoSize = true;
            this.btn_OpenDisplayShow.BackColor = System.Drawing.Color.Turquoise;
            this.btn_OpenDisplayShow.Location = new System.Drawing.Point(444, 18);
            this.btn_OpenDisplayShow.Name = "btn_OpenDisplayShow";
            this.btn_OpenDisplayShow.Size = new System.Drawing.Size(126, 30);
            this.btn_OpenDisplayShow.TabIndex = 2;
            this.btn_OpenDisplayShow.Text = "打开图像显示";
            this.btn_OpenDisplayShow.UseVisualStyleBackColor = false;
            this.btn_OpenDisplayShow.Click += new System.EventHandler(this.btn_OpenDisplayShow_Click);
            // 
            // btn_SoftOnce
            // 
            this.btn_SoftOnce.AutoSize = true;
            this.btn_SoftOnce.BackColor = System.Drawing.Color.Turquoise;
            this.btn_SoftOnce.Location = new System.Drawing.Point(301, 18);
            this.btn_SoftOnce.Name = "btn_SoftOnce";
            this.btn_SoftOnce.Size = new System.Drawing.Size(124, 30);
            this.btn_SoftOnce.TabIndex = 1;
            this.btn_SoftOnce.Text = "采集图像";
            this.btn_SoftOnce.UseVisualStyleBackColor = false;
            this.btn_SoftOnce.Click += new System.EventHandler(this.btn_SoftOnce_Click);
            // 
            // cmb_CameraName
            // 
            this.cmb_CameraName.FormattingEnabled = true;
            this.cmb_CameraName.Location = new System.Drawing.Point(9, 44);
            this.cmb_CameraName.Name = "cmb_CameraName";
            this.cmb_CameraName.Size = new System.Drawing.Size(267, 26);
            this.cmb_CameraName.TabIndex = 0;
            this.cmb_CameraName.SelectedValueChanged += new System.EventHandler(this.cmb_CameraName_SelectedValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.SkyBlue;
            this.groupBox3.Controls.Add(this.tableLayoutPanel3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 207);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(583, 371);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "九点标定栏";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.dgv, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 24);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72.10031F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.89969F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(577, 344);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.BackgroundColor = System.Drawing.Color.MintCream;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(3, 3);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersWidth = 62;
            this.dgv.RowTemplate.Height = 30;
            this.dgv.Size = new System.Drawing.Size(571, 242);
            this.dgv.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SkyBlue;
            this.panel1.Controls.Add(this.btn_GenerateNPoint);
            this.panel1.Controls.Add(this.btn_OpenProcess);
            this.panel1.Controls.Add(this.btn_OpenNpointTool);
            this.panel1.Controls.Add(this.btn_RunProcess);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 251);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(571, 90);
            this.panel1.TabIndex = 1;
            // 
            // btn_GenerateNPoint
            // 
            this.btn_GenerateNPoint.AutoSize = true;
            this.btn_GenerateNPoint.BackColor = System.Drawing.Color.Turquoise;
            this.btn_GenerateNPoint.Location = new System.Drawing.Point(38, 8);
            this.btn_GenerateNPoint.Name = "btn_GenerateNPoint";
            this.btn_GenerateNPoint.Size = new System.Drawing.Size(126, 30);
            this.btn_GenerateNPoint.TabIndex = 4;
            this.btn_GenerateNPoint.Text = "生成标定文件";
            this.btn_GenerateNPoint.UseVisualStyleBackColor = false;
            // 
            // btn_OpenProcess
            // 
            this.btn_OpenProcess.AutoSize = true;
            this.btn_OpenProcess.BackColor = System.Drawing.Color.Turquoise;
            this.btn_OpenProcess.Location = new System.Drawing.Point(339, 44);
            this.btn_OpenProcess.Name = "btn_OpenProcess";
            this.btn_OpenProcess.Size = new System.Drawing.Size(162, 30);
            this.btn_OpenProcess.TabIndex = 3;
            this.btn_OpenProcess.Text = "打开九点标定流程";
            this.btn_OpenProcess.UseVisualStyleBackColor = false;
            this.btn_OpenProcess.Click += new System.EventHandler(this.btn_OpenProcess_Click);
            // 
            // btn_OpenNpointTool
            // 
            this.btn_OpenNpointTool.AutoSize = true;
            this.btn_OpenNpointTool.BackColor = System.Drawing.Color.Turquoise;
            this.btn_OpenNpointTool.Location = new System.Drawing.Point(38, 44);
            this.btn_OpenNpointTool.Name = "btn_OpenNpointTool";
            this.btn_OpenNpointTool.Size = new System.Drawing.Size(126, 30);
            this.btn_OpenNpointTool.TabIndex = 2;
            this.btn_OpenNpointTool.Text = "打开标定工具";
            this.btn_OpenNpointTool.UseVisualStyleBackColor = false;
            this.btn_OpenNpointTool.Click += new System.EventHandler(this.btn_OpenNpointTool_Click);
            // 
            // btn_RunProcess
            // 
            this.btn_RunProcess.AutoSize = true;
            this.btn_RunProcess.BackColor = System.Drawing.Color.Turquoise;
            this.btn_RunProcess.Location = new System.Drawing.Point(339, 8);
            this.btn_RunProcess.Name = "btn_RunProcess";
            this.btn_RunProcess.Size = new System.Drawing.Size(162, 30);
            this.btn_RunProcess.TabIndex = 1;
            this.btn_RunProcess.Text = "运行九点标定流程";
            this.btn_RunProcess.UseVisualStyleBackColor = false;
            this.btn_RunProcess.Click += new System.EventHandler(this.btn_RunProcess_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.SkyBlue;
            this.groupBox2.Controls.Add(this.btn_GenerateCheckboard);
            this.groupBox2.Controls.Add(this.btn_OpenCheckboardTool);
            this.groupBox2.Controls.Add(this.cb_IsUseCheckboard);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 105);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(583, 96);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "棋盘格标定栏";
            // 
            // btn_GenerateCheckboard
            // 
            this.btn_GenerateCheckboard.AutoSize = true;
            this.btn_GenerateCheckboard.BackColor = System.Drawing.Color.Turquoise;
            this.btn_GenerateCheckboard.Location = new System.Drawing.Point(211, 34);
            this.btn_GenerateCheckboard.Name = "btn_GenerateCheckboard";
            this.btn_GenerateCheckboard.Size = new System.Drawing.Size(126, 33);
            this.btn_GenerateCheckboard.TabIndex = 2;
            this.btn_GenerateCheckboard.Text = "生成标定文件";
            this.btn_GenerateCheckboard.UseVisualStyleBackColor = false;
            // 
            // btn_OpenCheckboardTool
            // 
            this.btn_OpenCheckboardTool.AutoSize = true;
            this.btn_OpenCheckboardTool.BackColor = System.Drawing.Color.Turquoise;
            this.btn_OpenCheckboardTool.Location = new System.Drawing.Point(371, 34);
            this.btn_OpenCheckboardTool.Name = "btn_OpenCheckboardTool";
            this.btn_OpenCheckboardTool.Size = new System.Drawing.Size(126, 33);
            this.btn_OpenCheckboardTool.TabIndex = 1;
            this.btn_OpenCheckboardTool.Text = "打开标定工具";
            this.btn_OpenCheckboardTool.UseVisualStyleBackColor = false;
            this.btn_OpenCheckboardTool.Click += new System.EventHandler(this.btn_OpenCheckboardTool_Click);
            // 
            // cb_IsUseCheckboard
            // 
            this.cb_IsUseCheckboard.AutoSize = true;
            this.cb_IsUseCheckboard.Location = new System.Drawing.Point(10, 40);
            this.cb_IsUseCheckboard.Name = "cb_IsUseCheckboard";
            this.cb_IsUseCheckboard.Size = new System.Drawing.Size(160, 22);
            this.cb_IsUseCheckboard.TabIndex = 0;
            this.cb_IsUseCheckboard.Text = "使用棋盘格标定";
            this.cb_IsUseCheckboard.UseVisualStyleBackColor = true;
            this.cb_IsUseCheckboard.CheckedChanged += new System.EventHandler(this.cb_IsUseCheckboard_CheckedChanged);
            // 
            // gb_ToolShowName
            // 
            this.gb_ToolShowName.Controls.Add(this.cogRecordDisplay1);
            this.gb_ToolShowName.Controls.Add(this.cogCalibNPointToNPointEditV21);
            this.gb_ToolShowName.Controls.Add(this.cogCalibCheckerboardEditV21);
            this.gb_ToolShowName.Controls.Add(this.cogToolBlockEditV21);
            this.gb_ToolShowName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_ToolShowName.Location = new System.Drawing.Point(0, 0);
            this.gb_ToolShowName.Name = "gb_ToolShowName";
            this.gb_ToolShowName.Size = new System.Drawing.Size(920, 1021);
            this.gb_ToolShowName.TabIndex = 0;
            this.gb_ToolShowName.TabStop = false;
            this.gb_ToolShowName.Text = "工具";
            // 
            // cogRecordDisplay1
            // 
            this.cogRecordDisplay1.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.cogRecordDisplay1.ColorMapLowerRoiLimit = 0D;
            this.cogRecordDisplay1.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.cogRecordDisplay1.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.cogRecordDisplay1.ColorMapUpperRoiLimit = 1D;
            this.cogRecordDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cogRecordDisplay1.DoubleTapZoomCycleLength = 2;
            this.cogRecordDisplay1.DoubleTapZoomSensitivity = 2.5D;
            this.cogRecordDisplay1.Location = new System.Drawing.Point(3, 24);
            this.cogRecordDisplay1.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.cogRecordDisplay1.MouseWheelSensitivity = 1D;
            this.cogRecordDisplay1.Name = "cogRecordDisplay1";
            this.cogRecordDisplay1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("cogRecordDisplay1.OcxState")));
            this.cogRecordDisplay1.Size = new System.Drawing.Size(914, 994);
            this.cogRecordDisplay1.TabIndex = 3;
            this.cogRecordDisplay1.Click += new System.EventHandler(this.cogRecordDisplay1_Click);
            // 
            // cogCalibNPointToNPointEditV21
            // 
            this.cogCalibNPointToNPointEditV21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cogCalibNPointToNPointEditV21.Location = new System.Drawing.Point(3, 24);
            this.cogCalibNPointToNPointEditV21.MinimumSize = new System.Drawing.Size(489, 0);
            this.cogCalibNPointToNPointEditV21.Name = "cogCalibNPointToNPointEditV21";
            this.cogCalibNPointToNPointEditV21.Size = new System.Drawing.Size(914, 994);
            this.cogCalibNPointToNPointEditV21.SuspendElectricRuns = false;
            this.cogCalibNPointToNPointEditV21.TabIndex = 2;
            // 
            // cogCalibCheckerboardEditV21
            // 
            this.cogCalibCheckerboardEditV21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cogCalibCheckerboardEditV21.Location = new System.Drawing.Point(3, 24);
            this.cogCalibCheckerboardEditV21.MinimumSize = new System.Drawing.Size(489, 0);
            this.cogCalibCheckerboardEditV21.Name = "cogCalibCheckerboardEditV21";
            this.cogCalibCheckerboardEditV21.Size = new System.Drawing.Size(914, 994);
            this.cogCalibCheckerboardEditV21.SuspendElectricRuns = false;
            this.cogCalibCheckerboardEditV21.TabIndex = 1;
            // 
            // cogToolBlockEditV21
            // 
            this.cogToolBlockEditV21.AllowDrop = true;
            this.cogToolBlockEditV21.ContextMenuCustomizer = null;
            this.cogToolBlockEditV21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cogToolBlockEditV21.Location = new System.Drawing.Point(3, 24);
            this.cogToolBlockEditV21.MinimumSize = new System.Drawing.Size(489, 0);
            this.cogToolBlockEditV21.Name = "cogToolBlockEditV21";
            this.cogToolBlockEditV21.ShowNodeToolTips = true;
            this.cogToolBlockEditV21.Size = new System.Drawing.Size(914, 994);
            this.cogToolBlockEditV21.SuspendElectricRuns = false;
            this.cogToolBlockEditV21.TabIndex = 0;
            // 
            // CalibProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1513, 1021);
            this.Controls.Add(this.splitContainer1);
            this.Name = "CalibProcessForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CalibProcessForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CalibProcessForm_FormClosing);
            this.Load += new System.EventHandler(this.CalibProcessForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gb_ToolShowName.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cogCalibNPointToNPointEditV21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cogCalibCheckerboardEditV21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cogToolBlockEditV21)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_SoftOnce;
        private System.Windows.Forms.ComboBox cmb_CameraName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_GenerateCheckboard;
        private System.Windows.Forms.Button btn_OpenCheckboardTool;
        private System.Windows.Forms.CheckBox cb_IsUseCheckboard;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btn_RunProcess;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_OpenProcess;
        private System.Windows.Forms.Button btn_OpenNpointTool;
        private System.Windows.Forms.GroupBox gb_ToolShowName;
        private Cognex.VisionPro.CalibFix.CogCalibNPointToNPointEditV2 cogCalibNPointToNPointEditV21;
        private Cognex.VisionPro.CalibFix.CogCalibCheckerboardEditV2 cogCalibCheckerboardEditV21;
        private Cognex.VisionPro.ToolBlock.CogToolBlockEditV2 cogToolBlockEditV21;
        private Cognex.VisionPro.CogRecordDisplay cogRecordDisplay1;
        private System.Windows.Forms.Button btn_OpenDisplayShow;
        private System.Windows.Forms.Button btn_OpenFile;
        private System.Windows.Forms.Button btn_GenerateNPoint;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button btn_SaveFile;
		private System.Windows.Forms.Button btnClearData;
		private System.Windows.Forms.Button btnLoadExcel;
		private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button btn_Apply;
        private System.Windows.Forms.Panel panel2;
    }
}