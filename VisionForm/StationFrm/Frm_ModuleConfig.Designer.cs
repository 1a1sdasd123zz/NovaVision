using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NovaVision.VisionForm.StationFrm
{
    partial class Frm_ModuleConfig
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
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.dgv_InOut = new DataGridView();
            this.tableLayoutPanel2 = new TableLayoutPanel();
            this.btn_Close = new Button();
            this.btn_Update = new Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((ISupportInitialize)this.dgv_InOut).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            base.SuspendLayout();
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
            this.tableLayoutPanel1.Controls.Add(this.dgv_InOut, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            this.tableLayoutPanel1.Size = new Size(917, 648);
            this.tableLayoutPanel1.TabIndex = 0;
            this.dgv_InOut.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_InOut.Dock = DockStyle.Fill;
            this.dgv_InOut.Location = new Point(1, 1);
            this.dgv_InOut.Margin = new Padding(1);
            this.dgv_InOut.Name = "dgv_InOut";
            this.dgv_InOut.RowTemplate.Height = 23;
            this.dgv_InOut.Size = new Size(915, 616);
            this.dgv_InOut.TabIndex = 1;
            this.dgv_InOut.CellContentClick += this.dgv_InOut_CellContentClick;
            this.dgv_InOut.CellMouseClick += this.dgv_InOut_CellMouseClick;
            this.dgv_InOut.Scroll += this.dgv_InOut_Scroll;
            this.dgv_InOut.SizeChanged += this.dgv_InOut_SizeChanged;
            this.dgv_InOut.Click += this.dgv_InOut_Click;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100f));
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100f));
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.btn_Close, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_Update, 0, 0);
            this.tableLayoutPanel2.Dock = DockStyle.Fill;
            this.tableLayoutPanel2.Location = new Point(1, 619);
            this.tableLayoutPanel2.Margin = new Padding(1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel2.Size = new Size(915, 28);
            this.tableLayoutPanel2.TabIndex = 2;
            this.btn_Close.Dock = DockStyle.Fill;
            this.btn_Close.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.btn_Close.Location = new Point(100, 0);
            this.btn_Close.Margin = new Padding(0);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new Size(100, 28);
            this.btn_Close.TabIndex = 6;
            this.btn_Close.Text = "关闭";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += this.btn_Close_Click;
            this.btn_Update.Dock = DockStyle.Fill;
            this.btn_Update.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.btn_Update.Location = new Point(0, 0);
            this.btn_Update.Margin = new Padding(0);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new Size(100, 28);
            this.btn_Update.TabIndex = 5;
            this.btn_Update.Text = "更新";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += this.btn_Update_Click;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(917, 648);
            base.ControlBox = false;
            base.Controls.Add(this.tableLayoutPanel1);
            base.Name = "Frm_ModuleConfig";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "参数配置界面";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((ISupportInitialize)this.dgv_InOut).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;

        private DataGridView dgv_InOut;

        private TableLayoutPanel tableLayoutPanel2;

        private Button btn_Close;

        private Button btn_Update;

        private class TableParam
        {
            public List<string> ColName;

            public List<string> HeaderTexts;
        }
    }
}