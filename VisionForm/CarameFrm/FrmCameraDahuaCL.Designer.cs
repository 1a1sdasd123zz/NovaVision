namespace NovaVision.VisionForm.CarameFrm
{
    partial class FrmCameraDahuaCL
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
            this.btn_Save = new AntdUI.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.nUDGain = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.nUDTimeout = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nUDExposure = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFoundDev = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDExposure)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(522, 461);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox2.Controls.Add(this.cbFoundDev);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.btn_Save);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.nUDGain);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.nUDTimeout);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.nUDExposure);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(5, 5);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox2.Size = new System.Drawing.Size(512, 451);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "相机参数";
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(158, 375);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(124, 46);
            this.btn_Save.TabIndex = 11;
            this.btn_Save.Text = "保存";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(381, 241);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 24);
            this.label9.TabIndex = 10;
            this.label9.Text = "db";
            // 
            // nUDGain
            // 
            this.nUDGain.Location = new System.Drawing.Point(158, 237);
            this.nUDGain.Margin = new System.Windows.Forms.Padding(5);
            this.nUDGain.Maximum = new decimal(new int[] {
            300000,
            0,
            0,
            0});
            this.nUDGain.Name = "nUDGain";
            this.nUDGain.Size = new System.Drawing.Size(192, 35);
            this.nUDGain.TabIndex = 9;
            this.nUDGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(41, 245);
            this.label10.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 24);
            this.label10.TabIndex = 8;
            this.label10.Text = "增益：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(381, 304);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 24);
            this.label8.TabIndex = 7;
            this.label8.Text = "ms";
            this.label8.Visible = false;
            // 
            // nUDTimeout
            // 
            this.nUDTimeout.Location = new System.Drawing.Point(158, 299);
            this.nUDTimeout.Margin = new System.Windows.Forms.Padding(5);
            this.nUDTimeout.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nUDTimeout.Name = "nUDTimeout";
            this.nUDTimeout.Size = new System.Drawing.Size(192, 35);
            this.nUDTimeout.TabIndex = 6;
            this.nUDTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUDTimeout.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 304);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 24);
            this.label6.TabIndex = 5;
            this.label6.Text = "超时：";
            this.label6.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(381, 179);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "us";
            // 
            // nUDExposure
            // 
            this.nUDExposure.Location = new System.Drawing.Point(158, 175);
            this.nUDExposure.Margin = new System.Windows.Forms.Padding(5);
            this.nUDExposure.Maximum = new decimal(new int[] {
            300000,
            0,
            0,
            0});
            this.nUDExposure.Name = "nUDExposure";
            this.nUDExposure.Size = new System.Drawing.Size(192, 35);
            this.nUDExposure.TabIndex = 3;
            this.nUDExposure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 183);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "曝光：";
            // 
            // cbFoundDev
            // 
            this.cbFoundDev.FormattingEnabled = true;
            this.cbFoundDev.Location = new System.Drawing.Point(189, 44);
            this.cbFoundDev.Margin = new System.Windows.Forms.Padding(5);
            this.cbFoundDev.Name = "cbFoundDev";
            this.cbFoundDev.Size = new System.Drawing.Size(257, 32);
            this.cbFoundDev.TabIndex = 13;
            this.cbFoundDev.SelectedIndexChanged += new System.EventHandler(this.cbFoundDev_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(10, 44);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(169, 68);
            this.label7.TabIndex = 12;
            this.label7.Text = "相机序列号(含ModelName)：";
            // 
            // FrmCameraDahuaCL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 461);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmCameraDahuaCL";
            this.Text = "FrmCameraDahuaCL";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDExposure)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nUDGain;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nUDTimeout;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nUDExposure;
        private System.Windows.Forms.Label label1;
        private AntdUI.Button btn_Save;
        private System.Windows.Forms.ComboBox cbFoundDev;
        private System.Windows.Forms.Label label7;
    }
}