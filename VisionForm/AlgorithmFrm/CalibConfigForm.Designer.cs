namespace NovaVision.VisionForm.AlgorithmFrm
{
    partial class CalibConfigForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_SaveFile = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtn_AddLine = new System.Windows.Forms.ToolStripButton();
            this.tsBtn_DeleteLine = new System.Windows.Forms.ToolStripButton();
            this.tsBtn_UpLine = new System.Windows.Forms.ToolStripButton();
            this.tsBtn_DownLine = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 53);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1281, 833);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.groupBox2.Controls.Add(this.dgv);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1275, 743);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "工位标定文件配置";
            // 
            // dgv
            // 
            this.dgv.BackgroundColor = System.Drawing.Color.LightCyan;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(3, 24);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidth = 62;
            this.dgv.RowTemplate.Height = 30;
            this.dgv.Size = new System.Drawing.Size(1269, 716);
            this.dgv.TabIndex = 2;
            this.dgv.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgv_CellBeginEdit);
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_SaveFile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 752);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1275, 78);
            this.panel1.TabIndex = 2;
            // 
            // btn_SaveFile
            // 
            this.btn_SaveFile.BackColor = System.Drawing.Color.LightCyan;
            this.btn_SaveFile.Location = new System.Drawing.Point(477, 19);
            this.btn_SaveFile.Name = "btn_SaveFile";
            this.btn_SaveFile.Size = new System.Drawing.Size(132, 41);
            this.btn_SaveFile.TabIndex = 0;
            this.btn_SaveFile.Text = "保存配置";
            this.btn_SaveFile.UseVisualStyleBackColor = false;
            this.btn_SaveFile.Click += new System.EventHandler(this.btn_SaveFile_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.LightCyan;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtn_AddLine,
            this.tsBtn_DeleteLine,
            this.tsBtn_UpLine,
            this.tsBtn_DownLine});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1281, 33);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsBtn_AddLine
            // 
            this.tsBtn_AddLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtn_AddLine.Image = global::NovaVision.Properties.Resources.Plus;
            this.tsBtn_AddLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtn_AddLine.Name = "tsBtn_AddLine";
            this.tsBtn_AddLine.Size = new System.Drawing.Size(34, 28);
            this.tsBtn_AddLine.Text = "toolStripButton1";
            this.tsBtn_AddLine.ToolTipText = "添加";
            this.tsBtn_AddLine.Click += new System.EventHandler(this.tsBtn_Add_Click);
            // 
            // tsBtn_DeleteLine
            // 
            this.tsBtn_DeleteLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtn_DeleteLine.Image = global::NovaVision.Properties.Resources.Substract;
            this.tsBtn_DeleteLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtn_DeleteLine.Name = "tsBtn_DeleteLine";
            this.tsBtn_DeleteLine.Size = new System.Drawing.Size(34, 28);
            this.tsBtn_DeleteLine.Text = "toolStripButton2";
            this.tsBtn_DeleteLine.ToolTipText = "删除";
            this.tsBtn_DeleteLine.Click += new System.EventHandler(this.tsBtn_Delete_Click);
            // 
            // tsBtn_UpLine
            // 
            this.tsBtn_UpLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtn_UpLine.Image = global::NovaVision.Properties.Resources.Up;
            this.tsBtn_UpLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtn_UpLine.Name = "tsBtn_UpLine";
            this.tsBtn_UpLine.Size = new System.Drawing.Size(34, 28);
            this.tsBtn_UpLine.Text = "toolStripButton3";
            this.tsBtn_UpLine.ToolTipText = "上移";
            this.tsBtn_UpLine.Click += new System.EventHandler(this.tsBtn_Up_Click);
            // 
            // tsBtn_DownLine
            // 
            this.tsBtn_DownLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtn_DownLine.Image = global::NovaVision.Properties.Resources.Down;
            this.tsBtn_DownLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtn_DownLine.Name = "tsBtn_DownLine";
            this.tsBtn_DownLine.Size = new System.Drawing.Size(34, 28);
            this.tsBtn_DownLine.Text = "toolStripButton4";
            this.tsBtn_DownLine.ToolTipText = "下移";
            this.tsBtn_DownLine.Click += new System.EventHandler(this.tsBtn_Down_Click);
            // 
            // CalibConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(1281, 886);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CalibConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "九点标定配置界面";
            this.Load += new System.EventHandler(this.九点标定_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsBtn_AddLine;
        private System.Windows.Forms.ToolStripButton tsBtn_DeleteLine;
        private System.Windows.Forms.ToolStripButton tsBtn_UpLine;
        private System.Windows.Forms.ToolStripButton tsBtn_DownLine;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_SaveFile;
    }
}