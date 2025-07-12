using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace NovaVision.UserControlLibrary
{
    partial class ConfigCtrl
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
            this.btn_Save = new System.Windows.Forms.Button();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStrip_Config = new System.Windows.Forms.ToolStrip();
            this.tsBtn_Record = new System.Windows.Forms.ToolStripButton();
            this.tsBtn_Add = new System.Windows.Forms.ToolStripButton();
            this.tsBtn_Delete = new System.Windows.Forms.ToolStripButton();
            this.tsBtn_Up = new System.Windows.Forms.ToolStripButton();
            this.tsBtn_Down = new System.Windows.Forms.ToolStripButton();
            this.listBox_Names = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.contextMenuStrip.SuspendLayout();
            this.ToolStrip_Config.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Save
            // 
            this.btn_Save.AutoSize = true;
            this.btn_Save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Save.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Save.Location = new System.Drawing.Point(2, 472);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(266, 46);
            this.btn_Save.TabIndex = 7;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Rename});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(135, 34);
            // 
            // Rename
            // 
            this.Rename.Name = "Rename";
            this.Rename.Size = new System.Drawing.Size(134, 30);
            this.Rename.Text = "重命名";
            this.Rename.Click += new System.EventHandler(this.ToolStripMenuItemRename_Click);
            // 
            // ToolStrip_Config
            // 
            this.ToolStrip_Config.AutoSize = false;
            this.ToolStrip_Config.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStrip_Config.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStrip_Config.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ToolStrip_Config.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtn_Record,
            this.tsBtn_Add,
            this.tsBtn_Delete,
            this.tsBtn_Up,
            this.tsBtn_Down});
            this.ToolStrip_Config.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip_Config.Name = "ToolStrip_Config";
            this.ToolStrip_Config.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.ToolStrip_Config.Size = new System.Drawing.Size(270, 47);
            this.ToolStrip_Config.TabIndex = 6;
            this.ToolStrip_Config.Text = "toolStrip1";
            // 
            // tsBtn_Record
            // 
            this.tsBtn_Record.AutoSize = false;
            this.tsBtn_Record.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtn_Record.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsBtn_Record.Image = global::NovaVision.Properties.Resources.Record;
            this.tsBtn_Record.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtn_Record.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.tsBtn_Record.Name = "tsBtn_Record";
            this.tsBtn_Record.Size = new System.Drawing.Size(30, 30);
            this.tsBtn_Record.Text = "回放";
            this.tsBtn_Record.Click += new System.EventHandler(this.tsBtn_Record_Click);
            // 
            // tsBtn_Add
            // 
            this.tsBtn_Add.AutoSize = false;
            this.tsBtn_Add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtn_Add.Image = global::NovaVision.Properties.Resources.Plus;
            this.tsBtn_Add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtn_Add.Name = "tsBtn_Add";
            this.tsBtn_Add.Size = new System.Drawing.Size(30, 30);
            this.tsBtn_Add.Text = "添加";
            this.tsBtn_Add.Click += new System.EventHandler(this.tsBtn_Add_Click);
            // 
            // tsBtn_Delete
            // 
            this.tsBtn_Delete.AutoSize = false;
            this.tsBtn_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtn_Delete.Image = global::NovaVision.Properties.Resources.Substract;
            this.tsBtn_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtn_Delete.Name = "tsBtn_Delete";
            this.tsBtn_Delete.Size = new System.Drawing.Size(30, 30);
            this.tsBtn_Delete.Text = "删除";
            this.tsBtn_Delete.Click += new System.EventHandler(this.tsBtn_Delete_Click);
            // 
            // tsBtn_Up
            // 
            this.tsBtn_Up.AutoSize = false;
            this.tsBtn_Up.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtn_Up.Image = global::NovaVision.Properties.Resources.Up;
            this.tsBtn_Up.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtn_Up.Name = "tsBtn_Up";
            this.tsBtn_Up.Size = new System.Drawing.Size(30, 30);
            this.tsBtn_Up.Text = "上移";
            this.tsBtn_Up.Click += new System.EventHandler(this.tsBtn_Up_Click);
            // 
            // tsBtn_Down
            // 
            this.tsBtn_Down.AutoSize = false;
            this.tsBtn_Down.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtn_Down.Image = global::NovaVision.Properties.Resources.Down;
            this.tsBtn_Down.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtn_Down.Name = "tsBtn_Down";
            this.tsBtn_Down.Size = new System.Drawing.Size(30, 30);
            this.tsBtn_Down.Text = "下移";
            this.tsBtn_Down.Click += new System.EventHandler(this.tsBtn_Down_Click);
            // 
            // listBox_Names
            // 
            this.listBox_Names.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox_Names.ContextMenuStrip = this.contextMenuStrip;
            this.listBox_Names.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_Names.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox_Names.FormattingEnabled = true;
            this.listBox_Names.HorizontalScrollbar = true;
            this.listBox_Names.ItemHeight = 21;
            this.listBox_Names.Location = new System.Drawing.Point(2, 49);
            this.listBox_Names.Margin = new System.Windows.Forms.Padding(2);
            this.listBox_Names.Name = "listBox_Names";
            this.listBox_Names.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox_Names.Size = new System.Drawing.Size(266, 419);
            this.listBox_Names.TabIndex = 5;
            this.listBox_Names.SelectedIndexChanged += new System.EventHandler(this.listBox_Names_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.ToolStrip_Config, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.listBox_Names, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_Save, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(270, 520);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // ConfigCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ConfigCtrl";
            this.Size = new System.Drawing.Size(270, 520);
            this.Resize += new System.EventHandler(this.ConfigCtrl_Resize);
            this.contextMenuStrip.ResumeLayout(false);
            this.ToolStrip_Config.ResumeLayout(false);
            this.ToolStrip_Config.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button btn_Save;

        private ToolStrip ToolStrip_Config;

        public ToolStripButton tsBtn_Record;

        private ContextMenuStrip contextMenuStrip;

        private ToolStripMenuItem Rename;

        public ToolStripButton tsBtn_Add;

        public ToolStripButton tsBtn_Delete;

        public ToolStripButton tsBtn_Up;

        public ToolStripButton tsBtn_Down;
        private ListBox listBox_Names;
        private TableLayoutPanel tableLayoutPanel1;

        public ListBox ListBoxNames => listBox_Names;

        public Button Btn_Save => btn_Save;

        public event EventHandler BtnRecordClick;

        public event EventHandler BtnAddClick;

        public event EventHandler BtnDeleteClick;

        public event EventHandler BtnMoveUpClick;

        public event EventHandler BtnMoveDownClick;

        public event EventHandler BtnSaveClick;

        public event EventHandler SelectIndexChanged;

        public event EventHandler ToolStripMenuItem_Click;


        [Browsable(true)]
        [Description("控件宽度")]
        [Category("自定义")]
        [DefaultValue("")]
        public int ControlWidth
        {
            get
            {
                return ToolStrip_Config.Width;
            }
            set
            {
                base.Width = value;
                ToolStrip_Config.Width = value;
                listBox_Names.Width = value;
                btn_Save.Width = value;
            }
        }

        [Browsable(true)]
        [Description("ToolStrip高度")]
        [Category("自定义")]
        [DefaultValue("")]
        public int ToolStripHeight
        {
            get
            {
                return ToolStrip_Config.Height;
            }
            set
            {
                ToolStrip_Config.Height = value;
                ChangePosition(ToolStrip_Config, listBox_Names);
                ChangePosition(listBox_Names, btn_Save);
            }
        }

        [Browsable(true)]
        [Description("ToolStrip每个控件的宽度")]
        [Category("自定义")]
        [DefaultValue("")]
        public int ToolStripWidth
        {
            get
            {
                return ToolStrip_Config.Items[1].Width;
            }
            set
            {
                for (int i = 0; i < ToolStrip_Config.Items.Count; i++)
                {
                    if (ToolStrip_Config.Items[i].Name != "tsBtn_Record")
                    {
                        ToolStrip_Config.Items[i].Width = value;
                    }
                    if (ToolStrip_Config.Items[i].Name == "tsBtn_Record" && ToolStrip_Config.Items[i].Visible)
                    {
                        ToolStrip_Config.Items[i].Width = value;
                    }
                }
            }
        }

        [Browsable(true)]
        [Description("ListBox高度")]
        [Category("自定义")]
        [DefaultValue("")]
        public int ListBoxHeight
        {
            get
            {
                return listBox_Names.Height;
            }
            set
            {
                listBox_Names.Height = value;
                ChangePosition(listBox_Names, btn_Save);
            }
        }

        [Browsable(true)]
        [Description("Butto高度")]
        [Category("自定义")]
        [DefaultValue("")]
        public int ButtonHeight
        {
            get
            {
                return btn_Save.Height;
            }
            set
            {
                btn_Save.Height = value;
            }
        }

        [Browsable(true)]
        [Description("是否显示回放按钮")]
        [Category("自定义")]
        [DefaultValue("")]
        public bool IsShownRecord
        {
            get
            {
                return ToolStrip_Config.Items["tsBtn_Record"].Visible;
            }
            set
            {
                if (value)
                {
                    ToolStrip_Config.Items["tsBtn_Record"].Width = ToolStrip_Config.Items["tsBtn_Add"].Width;
                }
                else
                {
                    ToolStrip_Config.Items["tsBtn_Record"].Width = 0;
                }
                ToolStrip_Config.Items["tsBtn_Record"].Visible = value;
            }
        }
    }
}