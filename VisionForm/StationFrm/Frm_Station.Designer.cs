using System.Windows.Forms;

namespace NovaVision.VisionForm.StationFrm
{
    partial class Frm_Station
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
            this.CMS_Station = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btn_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Run = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_LoadRun = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_LoadImagesRun = new System.Windows.Forms.ToolStripMenuItem();
            this.CMS_Inspect = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmb_JobChangeCommSerial = new System.Windows.Forms.ComboBox();
            this.cmb_JobChangeCommTable = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.treeView_Station = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Load = new System.Windows.Forms.Button();
            this.btn_SaveParam = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ProcedurePlayBack = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cb_IsEnableDp = new System.Windows.Forms.CheckBox();
            this.label11 = new AntdUI.Label();
            this.numSingleCol = new System.Windows.Forms.NumericUpDown();
            this.label10 = new AntdUI.Label();
            this.numFly2Row = new System.Windows.Forms.NumericUpDown();
            this.label7 = new AntdUI.Label();
            this.numFly1Row = new System.Windows.Forms.NumericUpDown();
            this.label5 = new AntdUI.Label();
            this.numFlyNum = new System.Windows.Forms.NumericUpDown();
            this.label6 = new AntdUI.Label();
            this.btn_Save = new AntdUI.Button();
            this.numCol = new System.Windows.Forms.NumericUpDown();
            this.label4 = new AntdUI.Label();
            this.numRow = new System.Windows.Forms.NumericUpDown();
            this.label3 = new AntdUI.Label();
            this.cb_IsReverse1 = new AntdUI.Checkbox();
            this.cb_IsReverse2 = new AntdUI.Checkbox();
            this.CMS_Station.SuspendLayout();
            this.CMS_Inspect.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSingleCol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFly2Row)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFly1Row)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFlyNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRow)).BeginInit();
            this.SuspendLayout();
            // 
            // CMS_Station
            // 
            this.CMS_Station.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.CMS_Station.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_Add,
            this.btn_Rename,
            this.btn_Delete,
            this.btn_Run,
            this.tsm_LoadRun,
            this.tsm_LoadImagesRun});
            this.CMS_Station.Name = "contextMenuStrip1";
            this.CMS_Station.Size = new System.Drawing.Size(189, 184);
            // 
            // btn_Add
            // 
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(188, 30);
            this.btn_Add.Text = "添加节点";
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_Rename
            // 
            this.btn_Rename.Name = "btn_Rename";
            this.btn_Rename.Size = new System.Drawing.Size(188, 30);
            this.btn_Rename.Text = "重命名";
            this.btn_Rename.Click += new System.EventHandler(this.btn_Rename_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(188, 30);
            this.btn_Delete.Text = "删除节点";
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Run
            // 
            this.btn_Run.Name = "btn_Run";
            this.btn_Run.Size = new System.Drawing.Size(188, 30);
            this.btn_Run.Text = "运行此流程";
            this.btn_Run.Click += new System.EventHandler(this.btn_Run_Click);
            // 
            // tsm_LoadRun
            // 
            this.tsm_LoadRun.Name = "tsm_LoadRun";
            this.tsm_LoadRun.Size = new System.Drawing.Size(188, 30);
            this.tsm_LoadRun.Text = "加载图片运行";
            this.tsm_LoadRun.Click += new System.EventHandler(this.tsm_LoadRun_Click);
            // 
            // tsm_LoadImagesRun
            // 
            this.tsm_LoadImagesRun.Name = "tsm_LoadImagesRun";
            this.tsm_LoadImagesRun.Size = new System.Drawing.Size(188, 30);
            this.tsm_LoadImagesRun.Text = "加载文件夹";
            this.tsm_LoadImagesRun.Click += new System.EventHandler(this.tsm_LoadImagesRun_Click);
            // 
            // CMS_Inspect
            // 
            this.CMS_Inspect.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.CMS_Inspect.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpen});
            this.CMS_Inspect.Name = "contextMenuStrip2";
            this.CMS_Inspect.Size = new System.Drawing.Size(153, 34);
            // 
            // btnOpen
            // 
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(152, 30);
            this.btnOpen.Text = "打开配置";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1095, 894);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmb_JobChangeCommSerial);
            this.groupBox1.Controls.Add(this.cmb_JobChangeCommTable);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(1086, 75);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "切换作业通信配置";
            // 
            // cmb_JobChangeCommSerial
            // 
            this.cmb_JobChangeCommSerial.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_JobChangeCommSerial.FormattingEnabled = true;
            this.cmb_JobChangeCommSerial.Location = new System.Drawing.Point(754, 27);
            this.cmb_JobChangeCommSerial.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmb_JobChangeCommSerial.Name = "cmb_JobChangeCommSerial";
            this.cmb_JobChangeCommSerial.Size = new System.Drawing.Size(271, 26);
            this.cmb_JobChangeCommSerial.TabIndex = 3;
            this.cmb_JobChangeCommSerial.SelectedIndexChanged += new System.EventHandler(this.cmb_JobChangeCommSerial_SelectedIndexChanged);
            // 
            // cmb_JobChangeCommTable
            // 
            this.cmb_JobChangeCommTable.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_JobChangeCommTable.FormattingEnabled = true;
            this.cmb_JobChangeCommTable.Location = new System.Drawing.Point(213, 27);
            this.cmb_JobChangeCommTable.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmb_JobChangeCommTable.Name = "cmb_JobChangeCommTable";
            this.cmb_JobChangeCommTable.Size = new System.Drawing.Size(253, 26);
            this.cmb_JobChangeCommTable.TabIndex = 2;
            this.cmb_JobChangeCommTable.SelectedIndexChanged += new System.EventHandler(this.cmb_JobChangeCommTable_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(562, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "切换作业通信序列号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(39, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "切换作业通信表名：";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 87);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1089, 804);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage1.Size = new System.Drawing.Size(1081, 772);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "工位配置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 330F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.propertyGrid, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.treeView_Station, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1073, 766);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // propertyGrid
            // 
            this.propertyGrid.BackColor = System.Drawing.SystemColors.Control;
            this.propertyGrid.CommandsBackColor = System.Drawing.SystemColors.Control;
            this.propertyGrid.ContextMenuStrip = this.CMS_Inspect;
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.propertyGrid.Location = new System.Drawing.Point(334, 3);
            this.propertyGrid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid.Size = new System.Drawing.Size(735, 706);
            this.propertyGrid.TabIndex = 6;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            this.propertyGrid.PropertySortChanged += new System.EventHandler(this.propertyGrid_PropertySortChanged);
            // 
            // treeView_Station
            // 
            this.treeView_Station.ContextMenuStrip = this.CMS_Station;
            this.treeView_Station.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_Station.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView_Station.LabelEdit = true;
            this.treeView_Station.Location = new System.Drawing.Point(3, 3);
            this.treeView_Station.Name = "treeView_Station";
            this.treeView_Station.Size = new System.Drawing.Size(324, 706);
            this.treeView_Station.TabIndex = 0;
            this.treeView_Station.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_Station_AfterLabelEdit);
            this.treeView_Station.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_Station_NodeMouseDoubleClick);
            this.treeView_Station.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_Station_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_Load);
            this.panel1.Controls.Add(this.btn_SaveParam);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(330, 712);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(743, 54);
            this.panel1.TabIndex = 4;
            // 
            // btn_Load
            // 
            this.btn_Load.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_Load.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Load.Location = new System.Drawing.Point(0, 0);
            this.btn_Load.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_Load.Name = "btn_Load";
            this.btn_Load.Size = new System.Drawing.Size(134, 54);
            this.btn_Load.TabIndex = 1;
            this.btn_Load.Text = "加载配置";
            this.btn_Load.UseVisualStyleBackColor = true;
            this.btn_Load.Visible = false;
            // 
            // btn_SaveParam
            // 
            this.btn_SaveParam.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_SaveParam.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SaveParam.Location = new System.Drawing.Point(609, 0);
            this.btn_SaveParam.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_SaveParam.Name = "btn_SaveParam";
            this.btn_SaveParam.Size = new System.Drawing.Size(134, 54);
            this.btn_SaveParam.TabIndex = 2;
            this.btn_SaveParam.Text = "保存配置";
            this.btn_SaveParam.UseVisualStyleBackColor = true;
            this.btn_SaveParam.Click += new System.EventHandler(this.btn_SaveParam_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ProcedurePlayBack);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 712);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(330, 54);
            this.panel2.TabIndex = 5;
            // 
            // ProcedurePlayBack
            // 
            this.ProcedurePlayBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcedurePlayBack.Location = new System.Drawing.Point(0, 0);
            this.ProcedurePlayBack.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.ProcedurePlayBack.Name = "ProcedurePlayBack";
            this.ProcedurePlayBack.Size = new System.Drawing.Size(330, 54);
            this.ProcedurePlayBack.TabIndex = 0;
            this.ProcedurePlayBack.Text = "流程回放设置";
            this.ProcedurePlayBack.UseVisualStyleBackColor = true;
            this.ProcedurePlayBack.Click += new System.EventHandler(this.ProcedurePlayBack_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cb_IsReverse2);
            this.tabPage3.Controls.Add(this.cb_IsReverse1);
            this.tabPage3.Controls.Add(this.cb_IsEnableDp);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.numSingleCol);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.numFly2Row);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.numFly1Row);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.numFlyNum);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.btn_Save);
            this.tabPage3.Controls.Add(this.numCol);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.numRow);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Location = new System.Drawing.Point(4, 28);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1081, 772);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "飞拍配置";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cb_IsEnableDp
            // 
            this.cb_IsEnableDp.AutoSize = true;
            this.cb_IsEnableDp.Location = new System.Drawing.Point(209, 351);
            this.cb_IsEnableDp.Name = "cb_IsEnableDp";
            this.cb_IsEnableDp.Size = new System.Drawing.Size(178, 22);
            this.cb_IsEnableDp.TabIndex = 36;
            this.cb_IsEnableDp.Text = "是否加载深度学习";
            this.cb_IsEnableDp.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(541, 67);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(313, 37);
            this.label11.TabIndex = 35;
            this.label11.Text = "行、列对应激光打标的坐标";
            // 
            // numSingleCol
            // 
            this.numSingleCol.Location = new System.Drawing.Point(209, 193);
            this.numSingleCol.Name = "numSingleCol";
            this.numSingleCol.Size = new System.Drawing.Size(120, 28);
            this.numSingleCol.TabIndex = 34;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(79, 191);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(124, 32);
            this.label10.TabIndex = 33;
            this.label10.Text = "一次飞拍行数";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numFly2Row
            // 
            this.numFly2Row.Location = new System.Drawing.Point(209, 282);
            this.numFly2Row.Name = "numFly2Row";
            this.numFly2Row.Size = new System.Drawing.Size(120, 28);
            this.numFly2Row.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(102, 280);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 32);
            this.label7.TabIndex = 27;
            this.label7.Text = "飞拍2列数";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numFly1Row
            // 
            this.numFly1Row.Location = new System.Drawing.Point(209, 244);
            this.numFly1Row.Name = "numFly1Row";
            this.numFly1Row.Size = new System.Drawing.Size(120, 28);
            this.numFly1Row.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(102, 242);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 32);
            this.label5.TabIndex = 25;
            this.label5.Text = "飞拍1列数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numFlyNum
            // 
            this.numFlyNum.Location = new System.Drawing.Point(209, 153);
            this.numFlyNum.Name = "numFlyNum";
            this.numFlyNum.Size = new System.Drawing.Size(120, 28);
            this.numFlyNum.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(118, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 32);
            this.label6.TabIndex = 23;
            this.label6.Text = "飞拍次数";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(173, 432);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(156, 43);
            this.btn_Save.TabIndex = 22;
            this.btn_Save.Text = "保存";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // numCol
            // 
            this.numCol.Location = new System.Drawing.Point(209, 110);
            this.numCol.Name = "numCol";
            this.numCol.Size = new System.Drawing.Size(120, 28);
            this.numCol.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(118, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 32);
            this.label4.TabIndex = 20;
            this.label4.Text = "产品列数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numRow
            // 
            this.numRow.Location = new System.Drawing.Point(209, 67);
            this.numRow.Name = "numRow";
            this.numRow.Size = new System.Drawing.Size(120, 28);
            this.numRow.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(118, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 32);
            this.label3.TabIndex = 18;
            this.label3.Text = "产品行数";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_IsReverse1
            // 
            this.cb_IsReverse1.Location = new System.Drawing.Point(379, 250);
            this.cb_IsReverse1.Name = "cb_IsReverse1";
            this.cb_IsReverse1.Size = new System.Drawing.Size(225, 23);
            this.cb_IsReverse1.TabIndex = 37;
            this.cb_IsReverse1.Text = "飞拍1行数倒叙";
            // 
            // cb_IsReverse2
            // 
            this.cb_IsReverse2.Location = new System.Drawing.Point(379, 289);
            this.cb_IsReverse2.Name = "cb_IsReverse2";
            this.cb_IsReverse2.Size = new System.Drawing.Size(225, 23);
            this.cb_IsReverse2.TabIndex = 38;
            this.cb_IsReverse2.Text = "飞拍2行数倒叙";
            // 
            // Frm_Station
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 894);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Frm_Station";
            this.Text = "工位配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Station_FormClosing);
            this.CMS_Station.ResumeLayout(false);
            this.CMS_Inspect.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSingleCol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFly2Row)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFly1Row)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFlyNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ContextMenuStrip CMS_Station;

        private ContextMenuStrip CMS_Inspect;

        private ToolStripMenuItem btnOpen;

        private ToolStripMenuItem btn_Add;

        private ToolStripMenuItem btn_Rename;

        private ToolStripMenuItem btn_Delete;

        private TableLayoutPanel tableLayoutPanel1;

        private GroupBox groupBox1;

        private Label label2;

        private Label label1;

        private ComboBox cmb_JobChangeCommSerial;

        private ComboBox cmb_JobChangeCommTable;

        private TabControl tabControl1;

        private TabPage tabPage1;

        private TableLayoutPanel tableLayoutPanel2;

        private PropertyGrid propertyGrid;

        private TreeView treeView_Station;

        private Panel panel1;

        private Button btn_Load;

        private Button btn_SaveParam;

        private Panel panel2;

        private Button ProcedurePlayBack;
        private ToolStripMenuItem btn_Run;
        private ToolStripMenuItem tsm_LoadRun;
        private TabPage tabPage3;
        private AntdUI.Label label11;
        private NumericUpDown numSingleCol;
        private AntdUI.Label label10;
        private NumericUpDown numFly2Row;
        private AntdUI.Label label7;
        private NumericUpDown numFly1Row;
        private AntdUI.Label label5;
        private NumericUpDown numFlyNum;
        private AntdUI.Label label6;
        private AntdUI.Button btn_Save;
        private NumericUpDown numCol;
        private AntdUI.Label label4;
        private NumericUpDown numRow;
        private AntdUI.Label label3;
        private CheckBox cb_IsEnableDp;
        private ToolStripMenuItem tsm_LoadImagesRun;
        private AntdUI.Checkbox cb_IsReverse2;
        private AntdUI.Checkbox cb_IsReverse1;
    }
}