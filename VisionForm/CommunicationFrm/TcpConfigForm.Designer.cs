using System.Windows.Forms;

namespace NovaVision.VisionForm.CommunicationFrm
{
    partial class TcpConfigForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.cbMode = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.nUDSn = new System.Windows.Forms.NumericUpDown();
            this.btnNewTcp = new System.Windows.Forms.Button();
            this.btnStartOrStop = new System.Windows.Forms.Button();
            this.cbRole = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbTcp = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbHB = new System.Windows.Forms.CheckBox();
            this.tbHB = new System.Windows.Forms.TextBox();
            this.nUDRemotePort = new System.Windows.Forms.NumericUpDown();
            this.nUDLocalPort = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbRemoteIp_4th = new System.Windows.Forms.TextBox();
            this.tbRemoteIp_3rd = new System.Windows.Forms.TextBox();
            this.tbRemoteIp_2nd = new System.Windows.Forms.TextBox();
            this.tbRemoteIp_1st = new System.Windows.Forms.TextBox();
            this.tbLocalIp_4th = new System.Windows.Forms.TextBox();
            this.tbLocalIp_3rd = new System.Windows.Forms.TextBox();
            this.tbLocalIp_2nd = new System.Windows.Forms.TextBox();
            this.tbLocalIp_1st = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSaveParams = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.tStatusStrip = new System.Windows.Forms.StatusStrip();
            this.tSSLStateText = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnAddTcp = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.numericUpDownBufferSize = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.nUDSn).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.nUDRemotePort).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nUDLocalPort).BeginInit();
            this.tStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.numericUpDownBufferSize).BeginInit();
            base.SuspendLayout();
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.groupBox1.Controls.Add(this.btnTest);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.cbMode);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.nUDSn);
            this.groupBox1.Controls.Add(this.btnNewTcp);
            this.groupBox1.Controls.Add(this.btnStartOrStop);
            this.groupBox1.Controls.Add(this.cbRole);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbTcp);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(415, 149);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TCP设置";
            this.btnTest.Location = new System.Drawing.Point(269, 95);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(68, 23);
            this.btnTest.TabIndex = 10;
            this.btnTest.Text = "测试界面";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(btnTest_Click);
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(362, 11);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 12);
            this.label14.TabIndex = 9;
            this.label14.Text = "序号";
            this.cbMode.FormattingEnabled = true;
            this.cbMode.Location = new System.Drawing.Point(119, 95);
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(121, 20);
            this.cbMode.TabIndex = 8;
            this.cbMode.SelectedIndexChanged += new System.EventHandler(cbMode_SelectedIndexChanged);
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 98);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(89, 12);
            this.label13.TabIndex = 7;
            this.label13.Text = "数据交互模式：";
            this.nUDSn.Location = new System.Drawing.Point(351, 29);
            this.nUDSn.Maximum = new decimal(new int[4] { 300, 0, 0, 0 });
            this.nUDSn.Name = "nUDSn";
            this.nUDSn.Size = new System.Drawing.Size(51, 21);
            this.nUDSn.TabIndex = 6;
            this.nUDSn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnNewTcp.Location = new System.Drawing.Point(269, 27);
            this.btnNewTcp.Name = "btnNewTcp";
            this.btnNewTcp.Size = new System.Drawing.Size(75, 23);
            this.btnNewTcp.TabIndex = 5;
            this.btnNewTcp.Text = "新增Tcp";
            this.btnNewTcp.UseVisualStyleBackColor = true;
            this.btnNewTcp.Click += new System.EventHandler(btnNewTcp_Click);
            this.btnStartOrStop.Location = new System.Drawing.Point(269, 66);
            this.btnStartOrStop.Name = "btnStartOrStop";
            this.btnStartOrStop.Size = new System.Drawing.Size(68, 23);
            this.btnStartOrStop.TabIndex = 4;
            this.btnStartOrStop.Text = "开始监听";
            this.btnStartOrStop.UseVisualStyleBackColor = true;
            this.btnStartOrStop.Click += new System.EventHandler(btnStartOrStop_Click);
            this.cbRole.FormattingEnabled = true;
            this.cbRole.Location = new System.Drawing.Point(119, 63);
            this.cbRole.Name = "cbRole";
            this.cbRole.Size = new System.Drawing.Size(121, 20);
            this.cbRole.TabIndex = 3;
            this.cbRole.SelectedIndexChanged += new System.EventHandler(cbRole_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "TCP类型:";
            this.cbTcp.FormattingEnabled = true;
            this.cbTcp.Location = new System.Drawing.Point(119, 28);
            this.cbTcp.Name = "cbTcp";
            this.cbTcp.Size = new System.Drawing.Size(121, 20);
            this.cbTcp.TabIndex = 1;
            this.cbTcp.SelectedIndexChanged += new System.EventHandler(cbTcp_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "TCP名称（DIYSN）:";
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.groupBox2.Controls.Add(this.numericUpDownBufferSize);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.cbHB);
            this.groupBox2.Controls.Add(this.tbHB);
            this.groupBox2.Controls.Add(this.nUDRemotePort);
            this.groupBox2.Controls.Add(this.nUDLocalPort);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.tbRemoteIp_4th);
            this.groupBox2.Controls.Add(this.tbRemoteIp_3rd);
            this.groupBox2.Controls.Add(this.tbRemoteIp_2nd);
            this.groupBox2.Controls.Add(this.tbRemoteIp_1st);
            this.groupBox2.Controls.Add(this.tbLocalIp_4th);
            this.groupBox2.Controls.Add(this.tbLocalIp_3rd);
            this.groupBox2.Controls.Add(this.tbLocalIp_2nd);
            this.groupBox2.Controls.Add(this.tbLocalIp_1st);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 156);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(415, 156);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "网络参数";
            this.cbHB.AutoSize = true;
            this.cbHB.Location = new System.Drawing.Point(8, 128);
            this.cbHB.Name = "cbHB";
            this.cbHB.Size = new System.Drawing.Size(90, 16);
            this.cbHB.TabIndex = 22;
            this.cbHB.Text = "心跳字符串:";
            this.cbHB.UseVisualStyleBackColor = true;
            this.cbHB.CheckedChanged += new System.EventHandler(cbHB_CheckedChanged);
            this.tbHB.Location = new System.Drawing.Point(98, 126);
            this.tbHB.Name = "tbHB";
            this.tbHB.Size = new System.Drawing.Size(100, 21);
            this.tbHB.TabIndex = 21;
            this.tbHB.TextChanged += new System.EventHandler(tbHB_TextChanged);
            this.nUDRemotePort.Location = new System.Drawing.Point(77, 101);
            this.nUDRemotePort.Maximum = new decimal(new int[4] { 90000, 0, 0, 0 });
            this.nUDRemotePort.Minimum = new decimal(new int[4] { 500, 0, 0, 0 });
            this.nUDRemotePort.Name = "nUDRemotePort";
            this.nUDRemotePort.Size = new System.Drawing.Size(81, 21);
            this.nUDRemotePort.TabIndex = 19;
            this.nUDRemotePort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUDRemotePort.Value = new decimal(new int[4] { 1000, 0, 0, 0 });
            this.nUDRemotePort.ValueChanged += new System.EventHandler(nUDRemotePort_ValueChanged);
            this.nUDLocalPort.Location = new System.Drawing.Point(77, 52);
            this.nUDLocalPort.Maximum = new decimal(new int[4] { 90000, 0, 0, 0 });
            this.nUDLocalPort.Minimum = new decimal(new int[4] { 500, 0, 0, 0 });
            this.nUDLocalPort.Name = "nUDLocalPort";
            this.nUDLocalPort.Size = new System.Drawing.Size(81, 21);
            this.nUDLocalPort.TabIndex = 18;
            this.nUDLocalPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUDLocalPort.Value = new decimal(new int[4] { 1000, 0, 0, 0 });
            this.nUDLocalPort.ValueChanged += new System.EventHandler(nUDLocalPort_ValueChanged);
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(243, 82);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(11, 12);
            this.label12.TabIndex = 17;
            this.label12.Text = ".";
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(179, 82);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 12);
            this.label11.TabIndex = 16;
            this.label11.Text = ".";
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(116, 82);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 15;
            this.label10.Text = ".";
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(243, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 14;
            this.label9.Text = ".";
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(179, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = ".";
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(116, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = ".";
            this.tbRemoteIp_4th.Location = new System.Drawing.Point(258, 77);
            this.tbRemoteIp_4th.Name = "tbRemoteIp_4th";
            this.tbRemoteIp_4th.Size = new System.Drawing.Size(46, 21);
            this.tbRemoteIp_4th.TabIndex = 11;
            this.tbRemoteIp_4th.Text = "0";
            this.tbRemoteIp_4th.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbRemoteIp_4th.TextChanged += new System.EventHandler(tbRemoteIp_4th_TextChanged);
            this.tbRemoteIp_3rd.Location = new System.Drawing.Point(194, 77);
            this.tbRemoteIp_3rd.Name = "tbRemoteIp_3rd";
            this.tbRemoteIp_3rd.Size = new System.Drawing.Size(46, 21);
            this.tbRemoteIp_3rd.TabIndex = 10;
            this.tbRemoteIp_3rd.Text = "0";
            this.tbRemoteIp_3rd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbRemoteIp_3rd.TextChanged += new System.EventHandler(tbRemoteIp_3rd_TextChanged);
            this.tbRemoteIp_2nd.Location = new System.Drawing.Point(129, 77);
            this.tbRemoteIp_2nd.Name = "tbRemoteIp_2nd";
            this.tbRemoteIp_2nd.Size = new System.Drawing.Size(46, 21);
            this.tbRemoteIp_2nd.TabIndex = 9;
            this.tbRemoteIp_2nd.Text = "0";
            this.tbRemoteIp_2nd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbRemoteIp_2nd.TextChanged += new System.EventHandler(tbRemoteIp_2nd_TextChanged);
            this.tbRemoteIp_1st.Location = new System.Drawing.Point(67, 77);
            this.tbRemoteIp_1st.Name = "tbRemoteIp_1st";
            this.tbRemoteIp_1st.Size = new System.Drawing.Size(46, 21);
            this.tbRemoteIp_1st.TabIndex = 8;
            this.tbRemoteIp_1st.Text = "0";
            this.tbRemoteIp_1st.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbRemoteIp_1st.TextChanged += new System.EventHandler(tbRemoteIp_1st_TextChanged);
            this.tbLocalIp_4th.Location = new System.Drawing.Point(258, 27);
            this.tbLocalIp_4th.Name = "tbLocalIp_4th";
            this.tbLocalIp_4th.Size = new System.Drawing.Size(46, 21);
            this.tbLocalIp_4th.TabIndex = 7;
            this.tbLocalIp_4th.Text = "0";
            this.tbLocalIp_4th.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbLocalIp_4th.TextChanged += new System.EventHandler(tbLocalIp_4th_TextChanged);
            this.tbLocalIp_3rd.Location = new System.Drawing.Point(194, 27);
            this.tbLocalIp_3rd.Name = "tbLocalIp_3rd";
            this.tbLocalIp_3rd.Size = new System.Drawing.Size(46, 21);
            this.tbLocalIp_3rd.TabIndex = 6;
            this.tbLocalIp_3rd.Text = "0";
            this.tbLocalIp_3rd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbLocalIp_3rd.TextChanged += new System.EventHandler(tbLocalIp_3rd_TextChanged);
            this.tbLocalIp_2nd.Location = new System.Drawing.Point(129, 27);
            this.tbLocalIp_2nd.Name = "tbLocalIp_2nd";
            this.tbLocalIp_2nd.Size = new System.Drawing.Size(46, 21);
            this.tbLocalIp_2nd.TabIndex = 5;
            this.tbLocalIp_2nd.Text = "0";
            this.tbLocalIp_2nd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbLocalIp_2nd.TextChanged += new System.EventHandler(tbLocalIp_2nd_TextChanged);
            this.tbLocalIp_1st.Location = new System.Drawing.Point(67, 27);
            this.tbLocalIp_1st.Name = "tbLocalIp_1st";
            this.tbLocalIp_1st.Size = new System.Drawing.Size(46, 21);
            this.tbLocalIp_1st.TabIndex = 4;
            this.tbLocalIp_1st.Text = "0";
            this.tbLocalIp_1st.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbLocalIp_1st.TextChanged += new System.EventHandler(tbLocalIp_1st_TextChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "RemotePort:";
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "RemoteIp:";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "LocalPort:";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "LocalIp:";
            this.btnSaveParams.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnSaveParams.Location = new System.Drawing.Point(89, 318);
            this.btnSaveParams.Name = "btnSaveParams";
            this.btnSaveParams.Size = new System.Drawing.Size(75, 23);
            this.btnSaveParams.TabIndex = 2;
            this.btnSaveParams.Text = "保存参数";
            this.btnSaveParams.UseVisualStyleBackColor = true;
            this.btnSaveParams.Click += new System.EventHandler(btnSaveParams_Click);
            this.btnRemove.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.btnRemove.Location = new System.Drawing.Point(257, 319);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "移除TCP";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(btnRemove_Click);
            this.tStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.tSSLStateText });
            this.tStatusStrip.Location = new System.Drawing.Point(0, 355);
            this.tStatusStrip.Name = "tStatusStrip";
            this.tStatusStrip.Size = new System.Drawing.Size(436, 22);
            this.tStatusStrip.TabIndex = 4;
            this.tStatusStrip.Text = "statusStrip1";
            this.tSSLStateText.Name = "tSSLStateText";
            this.tSSLStateText.Size = new System.Drawing.Size(31, 17);
            this.tSSLStateText.Text = "N/A";
            this.btnAddTcp.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnAddTcp.Location = new System.Drawing.Point(89, 319);
            this.btnAddTcp.Name = "btnAddTcp";
            this.btnAddTcp.Size = new System.Drawing.Size(75, 23);
            this.btnAddTcp.TabIndex = 5;
            this.btnAddTcp.Text = "添加Tcp";
            this.btnAddTcp.UseVisualStyleBackColor = true;
            this.btnAddTcp.Click += new System.EventHandler(btnAddTcp_Click);
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(233, 129);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 12);
            this.label15.TabIndex = 23;
            this.label15.Text = "BufferSize:";
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(362, 129);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 12);
            this.label16.TabIndex = 24;
            this.label16.Text = "*1024";
            this.numericUpDownBufferSize.Location = new System.Drawing.Point(310, 127);
            this.numericUpDownBufferSize.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
            this.numericUpDownBufferSize.Name = "numericUpDownBufferSize";
            this.numericUpDownBufferSize.Size = new System.Drawing.Size(46, 21);
            this.numericUpDownBufferSize.TabIndex = 25;
            this.numericUpDownBufferSize.Value = new decimal(new int[4] { 1, 0, 0, 0 });
            this.numericUpDownBufferSize.ValueChanged += new System.EventHandler(numericUpDownBufferSize_ValueChanged);
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(436, 377);
            base.Controls.Add(this.btnAddTcp);
            base.Controls.Add(this.tStatusStrip);
            base.Controls.Add(this.btnRemove);
            base.Controls.Add(this.btnSaveParams);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.MaximizeBox = false;
            base.Name = "TcpConfigForm";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TcpConfigForm";
            base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(TcpConfigForm_FormClosing);
            base.Load += new System.EventHandler(TcpConfigForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.nUDSn).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.nUDRemotePort).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nUDLocalPort).EndInit();
            this.tStatusStrip.ResumeLayout(false);
            this.tStatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.numericUpDownBufferSize).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;

        private Label label1;

        private Button btnStartOrStop;

        private ComboBox cbRole;

        private Label label2;

        private ComboBox cbTcp;

        private NumericUpDown nUDSn;

        private Button btnNewTcp;

        private GroupBox groupBox2;

        private NumericUpDown nUDRemotePort;

        private NumericUpDown nUDLocalPort;

        private Label label12;

        private Label label11;

        private Label label10;

        private Label label9;

        private Label label8;

        private Label label7;

        private TextBox tbRemoteIp_4th;

        private TextBox tbRemoteIp_3rd;

        private TextBox tbRemoteIp_2nd;

        private TextBox tbRemoteIp_1st;

        private TextBox tbLocalIp_4th;

        private TextBox tbLocalIp_3rd;

        private TextBox tbLocalIp_2nd;

        private TextBox tbLocalIp_1st;

        private Label label6;

        private Label label5;

        private Label label4;

        private Label label3;

        private Button btnSaveParams;

        private Button btnRemove;

        private StatusStrip tStatusStrip;

        private ToolStripStatusLabel tSSLStateText;

        private ComboBox cbMode;

        private Label label13;

        private Label label14;

        private Button btnAddTcp;

        private Button btnTest;

        private CheckBox cbHB;

        private TextBox tbHB;

        private NumericUpDown numericUpDownBufferSize;

        private Label label16;

        private Label label15;
    }
}