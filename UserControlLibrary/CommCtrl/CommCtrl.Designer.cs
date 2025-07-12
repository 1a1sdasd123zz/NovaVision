using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NovaVision.UserControlLibrary.CommCtrl
{
    partial class CommCtrl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any global::StarVision.Properties.Resources being used.
        /// </summary>
        /// <param name="disposing">true if managed global::StarVision.Properties.Resources should be disposed; otherwise, false.</param>
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
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.configCtrl = new ConfigCtrl();
            this.tabControl1 = new TabControl();
            this.tabPage_Input = new TabPage();
            this.tableLayoutPanel2 = new TableLayoutPanel();
            this.dgv_In = new DataGridView();
            this.panel1 = new Panel();
            this.label2 = new Label();
            this.label1 = new Label();
            this.txt_End_Byte_In = new TextBox();
            this.txt_Start_Byte_In = new TextBox();
            this.lbl_Start_Input = new Label();
            this.toolStrip_In = new ToolStrip();
            this.tsBtn_NewLine_In = new ToolStripButton();
            this.tsBtn_DeleteLine_In = new ToolStripButton();
            this.tsBtn_Up_In = new ToolStripButton();
            this.tsBtn_Down_In = new ToolStripButton();
            this.tabPage_Output = new TabPage();
            this.tableLayoutPanel3 = new TableLayoutPanel();
            this.dgv_Out = new DataGridView();
            this.panel2 = new Panel();
            this.label5 = new Label();
            this.label6 = new Label();
            this.txt_End_Byte_Out = new TextBox();
            this.txt_Start_Byte_Out = new TextBox();
            this.label7 = new Label();
            this.toolStrip_Out = new ToolStrip();
            this.tsBtn_NewLine_out = new ToolStripButton();
            this.tsBtn_DeleteLine_out = new ToolStripButton();
            this.tsBtn_Up_Out = new ToolStripButton();
            this.tsBtn_Down_Out = new ToolStripButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage_Input.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((ISupportInitialize)this.dgv_In).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip_In.SuspendLayout();
            this.tabPage_Output.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((ISupportInitialize)this.dgv_Out).BeginInit();
            this.panel2.SuspendLayout();
            this.toolStrip_Out.SuspendLayout();
            base.SuspendLayout();
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 206f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.Controls.Add(this.configCtrl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 1, 0);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.Size = new Size(800, 500);
            this.tableLayoutPanel1.TabIndex = 0;
            this.configCtrl.ButtonHeight = 30;
            this.configCtrl.ControlWidth = 200;
            this.configCtrl.Dock = DockStyle.Fill;
            this.configCtrl.IsShownRecord = false;
            this.configCtrl.ListBoxHeight = 422;
            this.configCtrl.Location = new Point(0, 3);
            this.configCtrl.Margin = new Padding(0, 3, 3, 3);
            this.configCtrl.Name = "configCtrl";
            this.configCtrl.Size = new Size(203, 494);
            this.configCtrl.TabIndex = 1;
            this.configCtrl.ToolStripHeight = 30;
            this.configCtrl.ToolStripWidth = 30;
            this.configCtrl.BtnRecordClick += this.tsBtn_Record_Click;
            this.configCtrl.BtnAddClick += this.tsBtn_Add_Click;
            this.configCtrl.BtnDeleteClick += this.tsBtn_Delete_Click;
            this.configCtrl.BtnMoveUpClick += this.tsBtn_Up_Click;
            this.configCtrl.BtnMoveDownClick += this.tsBtn_Down_Click;
            this.configCtrl.BtnSaveClick += this.btn_Save_Click;
            this.configCtrl.SelectIndexChanged += this.listBox_Names_SelectedIndexChanged;
            this.configCtrl.ToolStripMenuItem_Click += this.ConfigCtrl_ToolStripMenuItem_Click;
            this.tabControl1.Controls.Add(this.tabPage_Input);
            this.tabControl1.Controls.Add(this.tabPage_Output);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new Point(209, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(588, 494);
            this.tabControl1.TabIndex = 2;
            this.tabPage_Input.Controls.Add(this.tableLayoutPanel2);
            this.tabPage_Input.Location = new Point(4, 22);
            this.tabPage_Input.Name = "tabPage_Input";
            this.tabPage_Input.Padding = new Padding(3);
            this.tabPage_Input.Size = new Size(580, 468);
            this.tabPage_Input.TabIndex = 0;
            this.tabPage_Input.Text = "Input";
            this.tabPage_Input.UseVisualStyleBackColor = true;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel2.Controls.Add(this.dgv_In, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.toolStrip_In, 0, 1);
            this.tableLayoutPanel2.Dock = DockStyle.Fill;
            this.tableLayoutPanel2.Location = new Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel2.Size = new Size(574, 462);
            this.tableLayoutPanel2.TabIndex = 1;
            this.dgv_In.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_In.Dock = DockStyle.Fill;
            this.dgv_In.Location = new Point(3, 63);
            this.dgv_In.Name = "dgv_In";
            this.dgv_In.RowTemplate.Height = 23;
            this.dgv_In.Size = new Size(568, 396);
            this.dgv_In.TabIndex = 5;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txt_End_Byte_In);
            this.panel1.Controls.Add(this.txt_Start_Byte_In);
            this.panel1.Controls.Add(this.lbl_Start_Input);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Margin = new Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(574, 30);
            this.panel1.TabIndex = 4;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(214, 9);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "字节";
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 134);
            this.label1.Location = new Point(137, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(12, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "-";
            this.txt_End_Byte_In.Location = new Point(152, 4);
            this.txt_End_Byte_In.Name = "txt_End_Byte_In";
            this.txt_End_Byte_In.Size = new Size(56, 21);
            this.txt_End_Byte_In.TabIndex = 3;
            this.txt_Start_Byte_In.Location = new Point(72, 4);
            this.txt_Start_Byte_In.Name = "txt_Start_Byte_In";
            this.txt_Start_Byte_In.Size = new Size(59, 21);
            this.txt_Start_Byte_In.TabIndex = 1;
            this.lbl_Start_Input.AutoSize = true;
            this.lbl_Start_Input.Location = new Point(5, 9);
            this.lbl_Start_Input.Name = "lbl_Start_Input";
            this.lbl_Start_Input.Size = new Size(65, 12);
            this.lbl_Start_Input.TabIndex = 0;
            this.lbl_Start_Input.Text = "地址分配：";
            this.toolStrip_In.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.toolStrip_In.Items.AddRange(new ToolStripItem[]
            {
                this.tsBtn_NewLine_In,
                this.tsBtn_DeleteLine_In,
                this.tsBtn_Up_In,
                this.tsBtn_Down_In
            });
            this.toolStrip_In.Location = new Point(0, 30);
            this.toolStrip_In.Name = "toolStrip_In";
            this.toolStrip_In.Size = new Size(574, 25);
            this.toolStrip_In.TabIndex = 3;
            this.toolStrip_In.Text = "toolStrip1";
            this.tsBtn_NewLine_In.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsBtn_NewLine_In.Image = global::NovaVision.Properties.Resources.Plus;
            this.tsBtn_NewLine_In.ImageTransparentColor = Color.Magenta;
            this.tsBtn_NewLine_In.Name = "tsBtn_NewLine_In";
            this.tsBtn_NewLine_In.Size = new Size(23, 22);
            this.tsBtn_NewLine_In.Text = "tsBbtn_NewLine_In";
            this.tsBtn_NewLine_In.ToolTipText = "新增一行";
            this.tsBtn_NewLine_In.Click += this.tsBtn_NewLine_In_Click;
            this.tsBtn_DeleteLine_In.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsBtn_DeleteLine_In.Image = global::NovaVision.Properties.Resources.Substract;
            this.tsBtn_DeleteLine_In.ImageTransparentColor = Color.Magenta;
            this.tsBtn_DeleteLine_In.Name = "tsBtn_DeleteLine_In";
            this.tsBtn_DeleteLine_In.Size = new Size(23, 22);
            this.tsBtn_DeleteLine_In.Text = "tsBtn_DeleteLine_In";
            this.tsBtn_DeleteLine_In.ToolTipText = "删除一行";
            this.tsBtn_DeleteLine_In.Click += this.tsBtn_DeleteLine_In_Click;
            this.tsBtn_Up_In.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsBtn_Up_In.Image = global::NovaVision.Properties.Resources.Up;
            this.tsBtn_Up_In.ImageTransparentColor = Color.Magenta;
            this.tsBtn_Up_In.Name = "tsBtn_Up_In";
            this.tsBtn_Up_In.Size = new Size(23, 22);
            this.tsBtn_Up_In.Text = "tsBtn_Up_In";
            this.tsBtn_Up_In.ToolTipText = "向上移动一行";
            this.tsBtn_Up_In.Click += this.tsBtn_Up_In_Click;
            this.tsBtn_Down_In.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsBtn_Down_In.Image = global::NovaVision.Properties.Resources.Down;
            this.tsBtn_Down_In.ImageTransparentColor = Color.Magenta;
            this.tsBtn_Down_In.Name = "tsBtn_Down_In";
            this.tsBtn_Down_In.Size = new Size(23, 22);
            this.tsBtn_Down_In.Text = "tsBtn_Down_In";
            this.tsBtn_Down_In.ToolTipText = "向下移动一行";
            this.tsBtn_Down_In.Click += this.tsBtn_Down_In_Click;
            this.tabPage_Output.Controls.Add(this.tableLayoutPanel3);
            this.tabPage_Output.Location = new Point(4, 22);
            this.tabPage_Output.Name = "tabPage_Output";
            this.tabPage_Output.Padding = new Padding(3);
            this.tabPage_Output.Size = new Size(580, 468);
            this.tabPage_Output.TabIndex = 1;
            this.tabPage_Output.Text = "Output";
            this.tabPage_Output.UseVisualStyleBackColor = true;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel3.Controls.Add(this.dgv_Out, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.toolStrip_Out, 0, 1);
            this.tableLayoutPanel3.Dock = DockStyle.Fill;
            this.tableLayoutPanel3.Location = new Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel3.Size = new Size(574, 462);
            this.tableLayoutPanel3.TabIndex = 1;
            this.dgv_Out.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Out.Dock = DockStyle.Fill;
            this.dgv_Out.Location = new Point(3, 63);
            this.dgv_Out.Name = "dgv_Out";
            this.dgv_Out.RowTemplate.Height = 23;
            this.dgv_Out.Size = new Size(568, 396);
            this.dgv_Out.TabIndex = 5;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txt_End_Byte_Out);
            this.panel2.Controls.Add(this.txt_Start_Byte_Out);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Margin = new Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(574, 30);
            this.panel2.TabIndex = 4;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(212, 9);
            this.label5.Name = "label5";
            this.label5.Size = new Size(29, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "字节";
            this.label6.AutoSize = true;
            this.label6.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 134);
            this.label6.Location = new Point(135, 9);
            this.label6.Name = "label6";
            this.label6.Size = new Size(12, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "-";
            this.txt_End_Byte_Out.Location = new Point(150, 4);
            this.txt_End_Byte_Out.Name = "txt_End_Byte_Out";
            this.txt_End_Byte_Out.Size = new Size(56, 21);
            this.txt_End_Byte_Out.TabIndex = 3;
            this.txt_Start_Byte_Out.Location = new Point(70, 4);
            this.txt_Start_Byte_Out.Name = "txt_Start_Byte_Out";
            this.txt_Start_Byte_Out.Size = new Size(59, 21);
            this.txt_Start_Byte_Out.TabIndex = 1;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(3, 9);
            this.label7.Name = "label7";
            this.label7.Size = new Size(65, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "地址分配：";
            this.toolStrip_Out.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.toolStrip_Out.Items.AddRange(new ToolStripItem[]
            {
                this.tsBtn_NewLine_out,
                this.tsBtn_DeleteLine_out,
                this.tsBtn_Up_Out,
                this.tsBtn_Down_Out
            });
            this.toolStrip_Out.Location = new Point(0, 30);
            this.toolStrip_Out.Name = "toolStrip_Out";
            this.toolStrip_Out.Size = new Size(574, 25);
            this.toolStrip_Out.TabIndex = 3;
            this.toolStrip_Out.Text = "toolStrip2";
            this.tsBtn_NewLine_out.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsBtn_NewLine_out.Image = global::NovaVision.Properties.Resources.Plus;
            this.tsBtn_NewLine_out.ImageTransparentColor = Color.Magenta;
            this.tsBtn_NewLine_out.Name = "tsBtn_NewLine_out";
            this.tsBtn_NewLine_out.Size = new Size(23, 22);
            this.tsBtn_NewLine_out.Text = "tsBtn_NewLine_Out";
            this.tsBtn_NewLine_out.ToolTipText = "新增一行";
            this.tsBtn_NewLine_out.Click += this.tsBtn_NewLine_Out_Click;
            this.tsBtn_DeleteLine_out.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsBtn_DeleteLine_out.Image = global::NovaVision.Properties.Resources.Substract;
            this.tsBtn_DeleteLine_out.ImageTransparentColor = Color.Magenta;
            this.tsBtn_DeleteLine_out.Name = "tsBtn_DeleteLine_out";
            this.tsBtn_DeleteLine_out.Size = new Size(23, 22);
            this.tsBtn_DeleteLine_out.Text = "tsBtn_DeleteLine_Out";
            this.tsBtn_DeleteLine_out.ToolTipText = "删除一行";
            this.tsBtn_DeleteLine_out.Click += this.tsBtn_DeleteLine_Out_Click;
            this.tsBtn_Up_Out.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsBtn_Up_Out.Image = global::NovaVision.Properties.Resources.Up;
            this.tsBtn_Up_Out.ImageTransparentColor = Color.Magenta;
            this.tsBtn_Up_Out.Name = "tsBtn_Up_Out";
            this.tsBtn_Up_Out.Size = new Size(23, 22);
            this.tsBtn_Up_Out.Text = "tsBtn_Up_Out";
            this.tsBtn_Up_Out.ToolTipText = "向上移动一行";
            this.tsBtn_Up_Out.Click += this.tsBtn_Up_Out_Click;
            this.tsBtn_Down_Out.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsBtn_Down_Out.Image = global::NovaVision.Properties.Resources.Down;
            this.tsBtn_Down_Out.ImageTransparentColor = Color.Magenta;
            this.tsBtn_Down_Out.Name = "tsBtn_Down_Out";
            this.tsBtn_Down_Out.Size = new Size(23, 22);
            this.tsBtn_Down_Out.Text = "tsBtn_Down_Out";
            this.tsBtn_Down_Out.ToolTipText = "向下移动一行";
            this.tsBtn_Down_Out.Click += this.tsBtn_Down_Out_Click;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.tableLayoutPanel1);
            base.Name = "CommCtrl";
            base.Size = new Size(800, 500);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_Input.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((ISupportInitialize)this.dgv_In).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip_In.ResumeLayout(false);
            this.toolStrip_In.PerformLayout();
            this.tabPage_Output.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((ISupportInitialize)this.dgv_Out).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip_Out.ResumeLayout(false);
            this.toolStrip_Out.PerformLayout();
            base.ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;

        private ConfigCtrl configCtrl;

        private TabControl tabControl1;

        private TabPage tabPage_Input;

        private TabPage tabPage_Output;

        private TableLayoutPanel tableLayoutPanel3;

        private DataGridView dgv_Out;

        private Panel panel2;

        private Label label5;

        private Label label6;

        private TextBox txt_End_Byte_Out;

        private TextBox txt_Start_Byte_Out;

        private Label label7;

        private ToolStrip toolStrip_Out;

        public ToolStripButton tsBtn_NewLine_out;

        public ToolStripButton tsBtn_DeleteLine_out;

        private TableLayoutPanel tableLayoutPanel2;

        private DataGridView dgv_In;

        private Panel panel1;

        private Label label2;

        private Label label1;

        private TextBox txt_End_Byte_In;

        private TextBox txt_Start_Byte_In;

        private Label lbl_Start_Input;

        private ToolStrip toolStrip_In;

        public ToolStripButton tsBtn_NewLine_In;

        public ToolStripButton tsBtn_DeleteLine_In;

        public ToolStripButton tsBtn_Up_In;

        public ToolStripButton tsBtn_Down_In;

        public ToolStripButton tsBtn_Up_Out;

        public ToolStripButton tsBtn_Down_Out;

        public ConfigCtrl ConfigCtrl => configCtrl;

        public ListBox ListBoxNames => configCtrl.ListBoxNames;

        public Button BtnSave => configCtrl.Btn_Save;

        public TextBox Txt_StartByteIn => txt_Start_Byte_In;

        public TextBox Txt_EndByteIn => txt_End_Byte_In;

        public TextBox Txt_StartByteOut => txt_Start_Byte_Out;

        public TextBox Txt_EndByteOut => txt_End_Byte_Out;

        public ToolStripButton BtnNewLineIn => tsBtn_NewLine_In;

        public ToolStripButton BtnDeleteLineIn => tsBtn_DeleteLine_In;

        public ToolStripButton BtnNewLineOut => tsBtn_NewLine_out;

        public ToolStripButton BtnDeleteLineOut => tsBtn_DeleteLine_out;

        public ToolStripButton BtnLineUpIn => tsBtn_Up_In;

        public ToolStripButton BtnLineUpOut => tsBtn_Up_Out;

        public ToolStripButton BtnLineDownIn => tsBtn_Down_In;

        public ToolStripButton BtnLineDownOut => tsBtn_Down_Out;

        public DataGridView DgvIn => dgv_In;

        public DataGridView DgvOut => dgv_Out;

        public TextBox Txt_Rename { get; }

        [Browsable(true)]
        [Description("ConfigCtr控件宽度")]
        [Category("自定义")]
        [DefaultValue("")]
        public int ConfigCtrlWidth
        {
            get
            {
                return ConfigCtrl.ControlWidth;
            }
            set
            {
                ConfigCtrl.ControlWidth = value;
            }
        }

        public event EventHandler BtnRecordClick;

        public event EventHandler BtnAddClick;

        public event EventHandler BtnDeleteClick;

        public event EventHandler BtnMoveUpClick;

        public event EventHandler BtnMoveDownClick;

        public event EventHandler BtnSaveClick;

        public event EventHandler SelectIndexChanged;

        public event EventHandler ToolStripMenuItem_Click;

        public event EventHandler BtnNewLineIn_Click;

        public event EventHandler BtnDeleteLineIn_Click;

        public event EventHandler BtnNewLineOut_Click;

        public event EventHandler BtnDeleteLineOut_Click;

        public event EventHandler BtnUpLineIn_Click;

        public event EventHandler BtnDownLineIn_Click;

        public event EventHandler BtnUpLineOut_Click;

        public event EventHandler BtnDownLineOut_Click;
    }
}