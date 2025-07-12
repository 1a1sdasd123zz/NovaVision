using System.Windows.Forms;

namespace NovaVision.VisionForm.StationFrm
{
    partial class FrmCameraConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCameraConfig));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip_In = new System.Windows.Forms.ToolStrip();
            this.tsBtn_NewLine = new System.Windows.Forms.ToolStripButton();
            this.tsBtn_DeleteLine = new System.Windows.Forms.ToolStripButton();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.lbl_CamType = new System.Windows.Forms.Label();
            this.cmb_SN = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip_In.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip_In, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgv, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1144, 720);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // toolStrip_In
            // 
            this.toolStrip_In.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip_In.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip_In.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtn_NewLine,
            this.tsBtn_DeleteLine});
            this.toolStrip_In.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_In.Name = "toolStrip_In";
            this.toolStrip_In.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.toolStrip_In.Size = new System.Drawing.Size(1144, 29);
            this.toolStrip_In.TabIndex = 4;
            this.toolStrip_In.Text = "toolStrip1";
            // 
            // tsBtn_NewLine
            // 
            this.tsBtn_NewLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtn_NewLine.Image = ((System.Drawing.Image)(resources.GetObject("tsBtn_NewLine.Image")));
            this.tsBtn_NewLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtn_NewLine.Name = "tsBtn_NewLine";
            this.tsBtn_NewLine.Size = new System.Drawing.Size(34, 24);
            this.tsBtn_NewLine.Text = "tsBbtn_NewLine_In";
            this.tsBtn_NewLine.ToolTipText = "新增一行";
            this.tsBtn_NewLine.Click += new System.EventHandler(this.tsBtn_NewLine_Click);
            // 
            // tsBtn_DeleteLine
            // 
            this.tsBtn_DeleteLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtn_DeleteLine.Image = ((System.Drawing.Image)(resources.GetObject("tsBtn_DeleteLine.Image")));
            this.tsBtn_DeleteLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtn_DeleteLine.Name = "tsBtn_DeleteLine";
            this.tsBtn_DeleteLine.Size = new System.Drawing.Size(34, 24);
            this.tsBtn_DeleteLine.Text = "tsBtn_DeleteLine_In";
            this.tsBtn_DeleteLine.ToolTipText = "删除一行";
            this.tsBtn_DeleteLine.Click += new System.EventHandler(this.tsBtn_DeleteLine_Click);
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(5, 107);
            this.dgv.Margin = new System.Windows.Forms.Padding(5);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersWidth = 51;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(1134, 608);
            this.dgv.TabIndex = 5;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 264F));
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.txt_Name, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbl_CamType, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.cmb_SN, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 51);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1144, 51);
            this.tableLayoutPanel3.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 41);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入配置名：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_Name
            // 
            this.txt_Name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Name.Location = new System.Drawing.Point(185, 5);
            this.txt_Name.Margin = new System.Windows.Forms.Padding(5);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(290, 35);
            this.txt_Name.TabIndex = 1;
            // 
            // lbl_CamType
            // 
            this.lbl_CamType.AutoSize = true;
            this.lbl_CamType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_CamType.Location = new System.Drawing.Point(885, 0);
            this.lbl_CamType.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_CamType.Name = "lbl_CamType";
            this.lbl_CamType.Size = new System.Drawing.Size(254, 51);
            this.lbl_CamType.TabIndex = 2;
            this.lbl_CamType.Text = "相机类型";
            this.lbl_CamType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmb_SN
            // 
            this.cmb_SN.FormattingEnabled = true;
            this.cmb_SN.Location = new System.Drawing.Point(485, 5);
            this.cmb_SN.Margin = new System.Windows.Forms.Padding(5);
            this.cmb_SN.Name = "cmb_SN";
            this.cmb_SN.Size = new System.Drawing.Size(384, 32);
            this.cmb_SN.TabIndex = 3;
            this.cmb_SN.SelectedIndexChanged += new System.EventHandler(this.cmb_SN_SelectedIndexChanged);
            // 
            // FrmCameraConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 720);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmCameraConfig";
            this.Text = "相机配置名";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_CameraConfig_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip_In.ResumeLayout(false);
            this.toolStrip_In.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;

        private ToolStrip toolStrip_In;

        public ToolStripButton tsBtn_NewLine;

        public ToolStripButton tsBtn_DeleteLine;

        private DataGridView dgv;

        private TableLayoutPanel tableLayoutPanel3;

        private Label label1;

        private TextBox txt_Name;

        private Label lbl_CamType;

        private ComboBox cmb_SN;
    }
}