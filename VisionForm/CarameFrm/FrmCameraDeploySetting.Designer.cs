using System.Windows.Forms;

namespace NovaVision.VisionForm.CarameFrm
{
    partial class FrmCameraDeploySetting
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
            this.ALLVendor2DAndLine = new System.Windows.Forms.ListBox();
            this.AddVendor2DAndLine = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.ALLVendor2DAndLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ALLVendor2DAndLine.FormattingEnabled = true;
            this.ALLVendor2DAndLine.ItemHeight = 16;
            this.ALLVendor2DAndLine.Location = new System.Drawing.Point(10, 22);
            this.ALLVendor2DAndLine.Name = "ALLVendor2DAndLine";
            this.ALLVendor2DAndLine.Size = new System.Drawing.Size(242, 325);
            this.ALLVendor2DAndLine.TabIndex = 0;
            this.ALLVendor2DAndLine.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(ALLVendor2DAndLine_MouseDoubleClick);
            this.AddVendor2DAndLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AddVendor2DAndLine.FormattingEnabled = true;
            this.AddVendor2DAndLine.ItemHeight = 16;
            this.AddVendor2DAndLine.Location = new System.Drawing.Point(10, 22);
            this.AddVendor2DAndLine.Name = "AddVendor2DAndLine";
            this.AddVendor2DAndLine.Size = new System.Drawing.Size(278, 325);
            this.AddVendor2DAndLine.TabIndex = 1;
            this.AddVendor2DAndLine.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(AddVendor2DAndLine_MouseDoubleClick);
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(10);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(10);
            this.splitContainer1.Size = new System.Drawing.Size(604, 370);
            this.splitContainer1.SplitterDistance = 282;
            this.splitContainer1.TabIndex = 2;
            this.groupBox1.Controls.Add(this.ALLVendor2DAndLine);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.groupBox1.Size = new System.Drawing.Size(262, 350);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "所有2D和线扫相机类型";
            this.groupBox2.Controls.Add(this.AddVendor2DAndLine);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            this.groupBox2.Location = new System.Drawing.Point(10, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.groupBox2.Size = new System.Drawing.Size(298, 350);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "已添加的2D和线扫相机类型";
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(604, 370);
            base.Controls.Add(this.splitContainer1);
            base.Name = "FrmCameraDeploySetting";
            this.Text = "相机配置信息";
            base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(FrmCameraDeploySetting_FormClosed);
            base.Load += new System.EventHandler(FrmCameraDeploySetting_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        #endregion

        private ListBox ALLVendor2DAndLine;

        private ListBox AddVendor2DAndLine;

        private SplitContainer splitContainer1;

        private GroupBox groupBox1;

        private GroupBox groupBox2;
    }
}