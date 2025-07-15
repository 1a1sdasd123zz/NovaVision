using System.Windows.Forms;
using Cognex.VisionPro.ToolBlock;
using NovaVision.UserControlLibrary;

namespace NovaVision.VisionForm.AlgorithmFrm
{
    partial class AlgConfigForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cogToolBlockEditV21 = new Cognex.VisionPro.ToolBlock.CogToolBlockEditV2();
            this.configCtrl_Alg = new NovaVision.UserControlLibrary.ConfigCtrl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.保存原始vppToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存不带图像与结果vppToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cogToolBlockEditV21)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.configCtrl_Alg, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1787, 917);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cogToolBlockEditV21, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(405, 5);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1377, 907);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.txt_Name, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1371, 54);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // txt_Name
            // 
            this.txt_Name.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txt_Name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Name.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Name.Location = new System.Drawing.Point(203, 3);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(1165, 35);
            this.txt_Name.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(5, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 54);
            this.label1.TabIndex = 7;
            this.label1.Text = "输入算法名：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cogToolBlockEditV21
            // 
            this.cogToolBlockEditV21.AllowDrop = true;
            this.cogToolBlockEditV21.ContextMenuCustomizer = null;
            this.cogToolBlockEditV21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cogToolBlockEditV21.Location = new System.Drawing.Point(5, 65);
            this.cogToolBlockEditV21.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cogToolBlockEditV21.MinimumSize = new System.Drawing.Size(979, 0);
            this.cogToolBlockEditV21.Name = "cogToolBlockEditV21";
            this.cogToolBlockEditV21.ShowNodeToolTips = true;
            this.cogToolBlockEditV21.Size = new System.Drawing.Size(1367, 837);
            this.cogToolBlockEditV21.SuspendElectricRuns = false;
            this.cogToolBlockEditV21.TabIndex = 2;
            // 
            // configCtrl_Alg
            // 
            this.configCtrl_Alg.ButtonHeight = 60;
            this.configCtrl_Alg.ControlWidth = 384;
            this.configCtrl_Alg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configCtrl_Alg.IsShownRecord = true;
            this.configCtrl_Alg.ListBoxHeight = 737;
            this.configCtrl_Alg.Location = new System.Drawing.Point(8, 8);
            this.configCtrl_Alg.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.configCtrl_Alg.Name = "configCtrl_Alg";
            this.configCtrl_Alg.Size = new System.Drawing.Size(384, 901);
            this.configCtrl_Alg.TabIndex = 6;
            this.configCtrl_Alg.ToolStripHeight = 83;
            this.configCtrl_Alg.ToolStripWidth = 30;
            this.configCtrl_Alg.BtnRecordClick += new System.EventHandler(this.btn_Review_Click);
            this.configCtrl_Alg.BtnAddClick += new System.EventHandler(this.btn_Add_Click);
            this.configCtrl_Alg.BtnDeleteClick += new System.EventHandler(this.btn_Delete_Click);
            this.configCtrl_Alg.BtnMoveUpClick += new System.EventHandler(this.tsBtn_Up_Click);
            this.configCtrl_Alg.BtnMoveDownClick += new System.EventHandler(this.tsBtn_Down_Click);
            this.configCtrl_Alg.BtnSaveClick += new System.EventHandler(this.btn_Save_Click);
            this.configCtrl_Alg.SelectIndexChanged += new System.EventHandler(this.listBox_Name_SelectedIndexChanged);
            this.configCtrl_Alg.ToolStripMenuItem_Click += new System.EventHandler(this.configCtrl_Alg_ToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存原始vppToolStripMenuItem,
            this.保存不带图像与结果vppToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(276, 64);
            // 
            // 保存原始vppToolStripMenuItem
            // 
            this.保存原始vppToolStripMenuItem.Name = "保存原始vppToolStripMenuItem";
            this.保存原始vppToolStripMenuItem.Size = new System.Drawing.Size(275, 30);
            this.保存原始vppToolStripMenuItem.Text = "保存原始vpp";
            this.保存原始vppToolStripMenuItem.Click += new System.EventHandler(this.保存原始vppToolStripMenuItem_Click);
            // 
            // 保存不带图像与结果vppToolStripMenuItem
            // 
            this.保存不带图像与结果vppToolStripMenuItem.Name = "保存不带图像与结果vppToolStripMenuItem";
            this.保存不带图像与结果vppToolStripMenuItem.Size = new System.Drawing.Size(275, 30);
            this.保存不带图像与结果vppToolStripMenuItem.Text = "保存不带图像与结果vpp";
            this.保存不带图像与结果vppToolStripMenuItem.Click += new System.EventHandler(this.保存不带图像与结果vppToolStripMenuItem_Click);
            // 
            // AlgConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1787, 917);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "AlgConfigForm";
            this.Text = "算法配置界面";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlgConfigForm_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cogToolBlockEditV21)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;

        private TableLayoutPanel tableLayoutPanel2;

        private TableLayoutPanel tableLayoutPanel3;

        private TextBox txt_Name;

        private Label label1;

        private CogToolBlockEditV2 cogToolBlockEditV21;

        private ConfigCtrl configCtrl_Alg;

        private ContextMenuStrip contextMenuStrip1;

        private ToolStripMenuItem 保存原始vppToolStripMenuItem;

        private ToolStripMenuItem 保存不带图像与结果vppToolStripMenuItem;
    }
}