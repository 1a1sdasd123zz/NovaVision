using System.Windows.Forms;

namespace NovaVision.VisionForm.CommunicationFrm
{
    partial class TcpTestForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpInput = new System.Windows.Forms.TabPage();
            this.dgv_In = new System.Windows.Forms.DataGridView();
            this.tpOutput = new System.Windows.Forms.TabPage();
            this.dgv_Out = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.dgv_In).BeginInit();
            this.tpOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.dgv_Out).BeginInit();
            base.SuspendLayout();
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.btnSend);
            this.splitContainer1.Panel1.Controls.Add(this.btnRead);
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(708, 395);
            this.splitContainer1.SplitterDistance = 133;
            this.splitContainer1.TabIndex = 0;
            this.btnSend.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.btnSend.Location = new System.Drawing.Point(32, 243);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "发送数据";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(btnSend_Click);
            this.btnRead.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.btnRead.Location = new System.Drawing.Point(32, 129);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 0;
            this.btnRead.Text = "读取数据";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(btnRead_Click);
            this.tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.tabControl1.Controls.Add(this.tpInput);
            this.tabControl1.Controls.Add(this.tpOutput);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(556, 389);
            this.tabControl1.TabIndex = 0;
            this.tpInput.Controls.Add(this.dgv_In);
            this.tpInput.Location = new System.Drawing.Point(4, 22);
            this.tpInput.Name = "tpInput";
            this.tpInput.Padding = new System.Windows.Forms.Padding(3);
            this.tpInput.Size = new System.Drawing.Size(548, 363);
            this.tpInput.TabIndex = 0;
            this.tpInput.Text = "InputDatas";
            this.tpInput.UseVisualStyleBackColor = true;
            this.dgv_In.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgv_In.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_In.Location = new System.Drawing.Point(3, 3);
            this.dgv_In.Name = "dgv_In";
            this.dgv_In.RowTemplate.Height = 23;
            this.dgv_In.Size = new System.Drawing.Size(542, 357);
            this.dgv_In.TabIndex = 0;
            this.tpOutput.Controls.Add(this.dgv_Out);
            this.tpOutput.Location = new System.Drawing.Point(4, 22);
            this.tpOutput.Name = "tpOutput";
            this.tpOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tpOutput.Size = new System.Drawing.Size(548, 363);
            this.tpOutput.TabIndex = 1;
            this.tpOutput.Text = "OutputDatas";
            this.tpOutput.UseVisualStyleBackColor = true;
            this.dgv_Out.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgv_Out.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Out.Location = new System.Drawing.Point(2, 2);
            this.dgv_Out.Name = "dgv_Out";
            this.dgv_Out.RowTemplate.Height = 23;
            this.dgv_Out.Size = new System.Drawing.Size(546, 358);
            this.dgv_Out.TabIndex = 1;
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(708, 395);
            base.Controls.Add(this.splitContainer1);
            base.Name = "TcpTestForm";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tcp测试界面";
            base.Load += new System.EventHandler(TcpTestForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpInput.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.dgv_In).EndInit();
            this.tpOutput.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.dgv_Out).EndInit();
            base.ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;

        private Button btnSend;

        private Button btnRead;

        private TabControl tabControl1;

        private TabPage tpInput;

        private DataGridView dgv_In;

        private TabPage tpOutput;

        private DataGridView dgv_Out;
    }
}