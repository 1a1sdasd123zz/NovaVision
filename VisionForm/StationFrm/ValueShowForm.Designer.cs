using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NovaVision.VisionForm.StationFrm
{
    partial class ValueShowForm
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
            this.dgv = new DataGridView();
            this.Tip = new ToolStripStatusLabel();
            this.statusStrip1 = new StatusStrip();
            this.btn_Close = new Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((ISupportInitialize)this.dgv).BeginInit();
            this.statusStrip1.SuspendLayout();
            base.SuspendLayout();
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.Controls.Add(this.dgv, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btn_Close, 0, 1);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
            this.tableLayoutPanel1.Size = new Size(247, 318);
            this.tableLayoutPanel1.TabIndex = 0;
            this.dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Dock = DockStyle.Fill;
            this.dgv.Location = new Point(3, 3);
            this.dgv.Name = "dgv";
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new Size(241, 257);
            this.dgv.TabIndex = 5;
            this.Tip.Name = "Tip";
            this.Tip.Size = new Size(0, 17);
            this.statusStrip1.Items.AddRange(new ToolStripItem[]
            {
                this.Tip
            });
            this.statusStrip1.Location = new Point(0, 296);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new Size(247, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            this.btn_Close.Dock = DockStyle.Fill;
            this.btn_Close.Location = new Point(3, 266);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new Size(241, 24);
            this.btn_Close.TabIndex = 7;
            this.btn_Close.Text = "关闭";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += this.btn_Close_Click;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(247, 318);
            base.ControlBox = false;
            base.Controls.Add(this.tableLayoutPanel1);
            base.Name = "ValueEditForm";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "ValueEditForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((ISupportInitialize)this.dgv).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            base.ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;

        private DataGridView dgv;

        private StatusStrip statusStrip1;

        private ToolStripStatusLabel Tip;

        private Button btn_Close;
    }
}