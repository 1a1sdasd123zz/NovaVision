using System.Windows.Forms;

namespace NovaVision.VisionForm.StationFrm
{
    partial class Frm_ProcedurePlayBack
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chk_IsSaveResultData = new System.Windows.Forms.CheckBox();
            this.chk_IsSaveResultImage = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.btnExcuteAllProucedure = new System.Windows.Forms.Button();
            this.btnChooseResultImagePath = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.txtResultImagePath = new System.Windows.Forms.TextBox();
            this.txtImageFilesPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsBtn_DeleteLine = new System.Windows.Forms.ToolStripButton();
            this.tsBtn_Save = new System.Windows.Forms.ToolStripButton();
            this.tsBtn_NewLine = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgv, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(740, 774);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chk_IsSaveResultData);
            this.panel1.Controls.Add(this.chk_IsSaveResultImage);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtCode);
            this.panel1.Controls.Add(this.btnExcuteAllProucedure);
            this.panel1.Controls.Add(this.btnChooseResultImagePath);
            this.panel1.Controls.Add(this.btnOpenFile);
            this.panel1.Controls.Add(this.txtResultImagePath);
            this.panel1.Controls.Add(this.txtImageFilesPath);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(732, 172);
            this.panel1.TabIndex = 0;
            // 
            // chk_IsSaveResultData
            // 
            this.chk_IsSaveResultData.AutoSize = true;
            this.chk_IsSaveResultData.Location = new System.Drawing.Point(627, 70);
            this.chk_IsSaveResultData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chk_IsSaveResultData.Name = "chk_IsSaveResultData";
            this.chk_IsSaveResultData.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_IsSaveResultData.Size = new System.Drawing.Size(88, 22);
            this.chk_IsSaveResultData.TabIndex = 7;
            this.chk_IsSaveResultData.Text = "存数据";
            this.chk_IsSaveResultData.UseVisualStyleBackColor = true;
            // 
            // chk_IsSaveResultImage
            // 
            this.chk_IsSaveResultImage.AutoSize = true;
            this.chk_IsSaveResultImage.Location = new System.Drawing.Point(609, 24);
            this.chk_IsSaveResultImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chk_IsSaveResultImage.Name = "chk_IsSaveResultImage";
            this.chk_IsSaveResultImage.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_IsSaveResultImage.Size = new System.Drawing.Size(106, 22);
            this.chk_IsSaveResultImage.TabIndex = 7;
            this.chk_IsSaveResultImage.Text = "存结果图";
            this.chk_IsSaveResultImage.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 124);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "模组码";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(99, 118);
            this.txtCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(428, 28);
            this.txtCode.TabIndex = 5;
            // 
            // btnExcuteAllProucedure
            // 
            this.btnExcuteAllProucedure.Location = new System.Drawing.Point(556, 111);
            this.btnExcuteAllProucedure.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExcuteAllProucedure.Name = "btnExcuteAllProucedure";
            this.btnExcuteAllProucedure.Size = new System.Drawing.Size(160, 40);
            this.btnExcuteAllProucedure.TabIndex = 3;
            this.btnExcuteAllProucedure.Text = "执行所有流程";
            this.btnExcuteAllProucedure.UseVisualStyleBackColor = true;
            // 
            // btnChooseResultImagePath
            // 
            this.btnChooseResultImagePath.Location = new System.Drawing.Point(538, 20);
            this.btnChooseResultImagePath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnChooseResultImagePath.Name = "btnChooseResultImagePath";
            this.btnChooseResultImagePath.Size = new System.Drawing.Size(51, 34);
            this.btnChooseResultImagePath.TabIndex = 2;
            this.btnChooseResultImagePath.Text = "...";
            this.btnChooseResultImagePath.UseVisualStyleBackColor = true;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(538, 68);
            this.btnOpenFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(51, 34);
            this.btnOpenFile.TabIndex = 2;
            this.btnOpenFile.Text = "...";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            // 
            // txtResultImagePath
            // 
            this.txtResultImagePath.Location = new System.Drawing.Point(129, 20);
            this.txtResultImagePath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtResultImagePath.Name = "txtResultImagePath";
            this.txtResultImagePath.Size = new System.Drawing.Size(398, 28);
            this.txtResultImagePath.TabIndex = 1;
            // 
            // txtImageFilesPath
            // 
            this.txtImageFilesPath.Location = new System.Drawing.Point(99, 70);
            this.txtImageFilesPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtImageFilesPath.Name = "txtImageFilesPath";
            this.txtImageFilesPath.Size = new System.Drawing.Size(428, 28);
            this.txtImageFilesPath.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 24);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "结果图文件夹";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 75);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择文件";
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(4, 229);
            this.dgv.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersWidth = 62;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(732, 511);
            this.dgv.TabIndex = 1;
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtn_NewLine,
            this.tsBtn_DeleteLine,
            this.tsBtn_Save});
            this.toolStrip.Location = new System.Drawing.Point(0, 180);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip.Size = new System.Drawing.Size(740, 45);
            this.toolStrip.TabIndex = 4;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tsBtn_DeleteLine
            // 
            this.tsBtn_DeleteLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtn_DeleteLine.Image = global::NovaVision.Properties.Resources.Substract;
            this.tsBtn_DeleteLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtn_DeleteLine.Name = "tsBtn_DeleteLine";
            this.tsBtn_DeleteLine.Size = new System.Drawing.Size(34, 40);
            this.tsBtn_DeleteLine.Text = "tsBtn_DeleteLine_In";
            this.tsBtn_DeleteLine.ToolTipText = "删除一行";
            // 
            // tsBtn_Save
            // 
            this.tsBtn_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtn_Save.Image = global::NovaVision.Properties.Resources.Save;
            this.tsBtn_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtn_Save.Name = "tsBtn_Save";
            this.tsBtn_Save.Size = new System.Drawing.Size(34, 40);
            this.tsBtn_Save.Text = "toolStripButton1";
            this.tsBtn_Save.ToolTipText = "保存";
            // 
            // tsBtn_NewLine
            // 
            this.tsBtn_NewLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtn_NewLine.Image = global::NovaVision.Properties.Resources.Plus;
            this.tsBtn_NewLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtn_NewLine.Name = "tsBtn_NewLine";
            this.tsBtn_NewLine.Size = new System.Drawing.Size(34, 40);
            this.tsBtn_NewLine.Text = "tsBbtn_NewLine_In";
            this.tsBtn_NewLine.ToolTipText = "新增一行";
            // 
            // Frm_ProcedurePlayBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 774);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Frm_ProcedurePlayBack";
            this.Text = "流程回放配置界面";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;

        private Panel panel1;

        private DataGridView dgv;

        private Button btnOpenFile;

        private TextBox txtImageFilesPath;

        private Button btnExcuteAllProucedure;

        private ToolStrip toolStrip;

        public ToolStripButton tsBtn_NewLine;

        public ToolStripButton tsBtn_DeleteLine;

        private ToolStripButton tsBtn_Save;

        private Label label2;

        private TextBox txtCode;

        private Label label1;

        private CheckBox chk_IsSaveResultData;

        private CheckBox chk_IsSaveResultImage;

        private Button btnChooseResultImagePath;

        private TextBox txtResultImagePath;

        private Label label3;
    }
}