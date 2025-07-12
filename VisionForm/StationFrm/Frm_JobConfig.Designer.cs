using System.Windows.Forms;

namespace NovaVision.VisionForm.StationFrm
{
    partial class Frm_JobConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_JobConfig));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsBtn_NewLine = new System.Windows.Forms.ToolStripButton();
            this.tsBtn_DeleteLine = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_JobNames = new System.Windows.Forms.ComboBox();
            this.txt_JobName = new AntdUI.Label();
            this.btn_ChangeJob = new AntdUI.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_CancelLoad = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Controls.Add(this.dgv, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1045, 876);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(5, 125);
            this.dgv.Margin = new System.Windows.Forms.Padding(5);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidth = 51;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(1035, 686);
            this.dgv.TabIndex = 7;
            this.dgv.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValueChanged);
            this.dgv.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgv_CurrentCellDirtyStateChanged);
            // 
            // toolStrip
            // 
            this.toolStrip.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtn_NewLine,
            this.tsBtn_DeleteLine});
            this.toolStrip.Location = new System.Drawing.Point(0, 60);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.toolStrip.Size = new System.Drawing.Size(1045, 29);
            this.toolStrip.TabIndex = 5;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tsBtn_NewLine
            // 
            this.tsBtn_NewLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtn_NewLine.Image = ((System.Drawing.Image)(resources.GetObject("tsBtn_NewLine.Image")));
            this.tsBtn_NewLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtn_NewLine.Name = "tsBtn_NewLine";
            this.tsBtn_NewLine.Size = new System.Drawing.Size(34, 24);
            this.tsBtn_NewLine.Text = "添加型号";
            this.tsBtn_NewLine.Click += new System.EventHandler(this.tsBtn_NewLine_Click);
            // 
            // tsBtn_DeleteLine
            // 
            this.tsBtn_DeleteLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtn_DeleteLine.Image = ((System.Drawing.Image)(resources.GetObject("tsBtn_DeleteLine.Image")));
            this.tsBtn_DeleteLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtn_DeleteLine.Name = "tsBtn_DeleteLine";
            this.tsBtn_DeleteLine.Size = new System.Drawing.Size(34, 24);
            this.tsBtn_DeleteLine.Text = "移除型号";
            this.tsBtn_DeleteLine.Click += new System.EventHandler(this.tsBtn_DeleteLine_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 367F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmb_JobNames, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.txt_JobName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_ChangeJob, 4, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1039, 54);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前型号：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmb_JobNames
            // 
            this.cmb_JobNames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmb_JobNames.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_JobNames.FormattingEnabled = true;
            this.cmb_JobNames.Location = new System.Drawing.Point(476, 3);
            this.cmb_JobNames.Name = "cmb_JobNames";
            this.cmb_JobNames.Size = new System.Drawing.Size(194, 32);
            this.cmb_JobNames.TabIndex = 1;
            // 
            // txt_JobName
            // 
            this.txt_JobName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_JobName.Location = new System.Drawing.Point(144, 4);
            this.txt_JobName.Margin = new System.Windows.Forms.Padding(4);
            this.txt_JobName.Name = "txt_JobName";
            this.txt_JobName.Size = new System.Drawing.Size(125, 46);
            this.txt_JobName.TabIndex = 3;
            this.txt_JobName.Text = "label2";
            // 
            // btn_ChangeJob
            // 
            this.btn_ChangeJob.Location = new System.Drawing.Point(677, 4);
            this.btn_ChangeJob.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ChangeJob.Name = "btn_ChangeJob";
            this.btn_ChangeJob.Size = new System.Drawing.Size(192, 46);
            this.btn_ChangeJob.TabIndex = 2;
            this.btn_ChangeJob.Text = "型号切换";
            this.btn_ChangeJob.Click += new System.EventHandler(this.btn_LoadCurrentJob_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.progressBar1, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btn_CancelLoad, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btn_Save, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 819);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1039, 54);
            this.tableLayoutPanel3.TabIndex = 8;
            // 
            // btn_CancelLoad
            // 
            this.btn_CancelLoad.AutoSize = true;
            this.btn_CancelLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_CancelLoad.Location = new System.Drawing.Point(243, 3);
            this.btn_CancelLoad.Name = "btn_CancelLoad";
            this.btn_CancelLoad.Size = new System.Drawing.Size(114, 48);
            this.btn_CancelLoad.TabIndex = 4;
            this.btn_CancelLoad.Text = "刷新";
            this.btn_CancelLoad.UseVisualStyleBackColor = true;
            this.btn_CancelLoad.Visible = false;
            this.btn_CancelLoad.Click += new System.EventHandler(this.btn_CancelLoad_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.AutoSize = true;
            this.btn_Save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Save.Location = new System.Drawing.Point(3, 3);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(234, 48);
            this.btn_Save.TabIndex = 3;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_LoadJobs_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(363, 3);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(673, 48);
            this.progressBar1.TabIndex = 4;
            // 
            // Frm_JobConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 876);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Frm_JobConfig";
            this.Text = "Frm_JobConfig";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;

        private ToolStrip toolStrip;

        private TableLayoutPanel tableLayoutPanel2;

        private Label label1;

        private ComboBox cmb_JobNames;

        private DataGridView dgv;

        private TableLayoutPanel tableLayoutPanel3;

        private Button btn_CancelLoad;

        private Button btn_Save;
        private ToolStripButton tsBtn_NewLine;
        private ToolStripButton tsBtn_DeleteLine;
        private AntdUI.Button btn_ChangeJob;
        private AntdUI.Label txt_JobName;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}