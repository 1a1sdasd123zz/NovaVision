using System.Windows.Forms;

namespace NovaVision.VisionForm.CommunicationFrm
{
    partial class ValueEditForm
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
            this.toolStrip_In = new System.Windows.Forms.ToolStrip();
            this.tsBtn_NewLine_In = new System.Windows.Forms.ToolStripButton();
            this.tsBtn_DeleteLine_In = new System.Windows.Forms.ToolStripButton();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Tip = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip_In.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip_In, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgv, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(370, 477);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // toolStrip_In
            // 
            this.toolStrip_In.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip_In.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip_In.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtn_NewLine_In,
            this.tsBtn_DeleteLine_In});
            this.toolStrip_In.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_In.Name = "toolStrip_In";
            this.toolStrip_In.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip_In.Size = new System.Drawing.Size(370, 33);
            this.toolStrip_In.TabIndex = 4;
            this.toolStrip_In.Text = "toolStrip1";
            // 
            // tsBtn_NewLine_In
            // 
            this.tsBtn_NewLine_In.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtn_NewLine_In.Image = global::NovaVision.Properties.Resources.Plus;
            this.tsBtn_NewLine_In.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtn_NewLine_In.Name = "tsBtn_NewLine_In";
            this.tsBtn_NewLine_In.Size = new System.Drawing.Size(34, 28);
            this.tsBtn_NewLine_In.Text = "tsBbtn_NewLine_In";
            this.tsBtn_NewLine_In.ToolTipText = "新增一行";
            this.tsBtn_NewLine_In.Click += new System.EventHandler(this.tsBtn_NewLine_In_Click);
            // 
            // tsBtn_DeleteLine_In
            // 
            this.tsBtn_DeleteLine_In.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtn_DeleteLine_In.Image = global::NovaVision.Properties.Resources.Substract;
            this.tsBtn_DeleteLine_In.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtn_DeleteLine_In.Name = "tsBtn_DeleteLine_In";
            this.tsBtn_DeleteLine_In.Size = new System.Drawing.Size(34, 28);
            this.tsBtn_DeleteLine_In.Text = "tsBtn_DeleteLine_In";
            this.tsBtn_DeleteLine_In.ToolTipText = "删除一行";
            this.tsBtn_DeleteLine_In.Click += new System.EventHandler(this.tsBtn_DeleteLine_In_Click);
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(4, 42);
            this.dgv.Margin = new System.Windows.Forms.Padding(4);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersWidth = 62;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(362, 348);
            this.dgv.TabIndex = 5;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tip});
            this.statusStrip1.Location = new System.Drawing.Point(0, 455);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(370, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Tip
            // 
            this.Tip.Name = "Tip";
            this.Tip.Size = new System.Drawing.Size(0, 15);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btn_Cancel, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_Save, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 394);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(370, 45);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Cancel.Location = new System.Drawing.Point(189, 4);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(177, 37);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Save.Location = new System.Drawing.Point(4, 4);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(177, 37);
            this.btn_Save.TabIndex = 0;
            this.btn_Save.Text = "确定";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // ValueEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 477);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ValueEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ValueEditForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip_In.ResumeLayout(false);
            this.toolStrip_In.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;

        private ToolStrip toolStrip_In;

        public ToolStripButton tsBtn_NewLine_In;

        public ToolStripButton tsBtn_DeleteLine_In;

        private DataGridView dgv;

        private StatusStrip statusStrip1;

        private ToolStripStatusLabel Tip;

        private TableLayoutPanel tableLayoutPanel2;

        private Button btn_Cancel;

        private Button btn_Save;
    }
}