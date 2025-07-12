using System.Windows.Forms;
using NovaVision.UserControlLibrary;

namespace NovaVision.VisionForm.StationFrm
{
    partial class Frm_FileParam
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
            this.btn_SaveConfig = new System.Windows.Forms.Button();
            this.chk_SaveRawImage = new System.Windows.Forms.CheckBox();
            this.chk_SaveDealImage = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.cmb_ImageToolRemoteType = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cmb_ImageToolType = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.cmb_ImageRemoteType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_days_Deal = new System.Windows.Forms.TextBox();
            this.chk_SaveOKNGGlobal = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_ImageType = new System.Windows.Forms.ComboBox();
            this.chk_Delete = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_days = new System.Windows.Forms.TextBox();
            this.chk_SaveRemoteDealImage = new System.Windows.Forms.CheckBox();
            this.chk_SaveRemoteRawImage = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dtp_PollTime1 = new System.Windows.Forms.DateTimePicker();
            this.dtp_PollTime2 = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.nud_Threshold = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.rtn_false = new System.Windows.Forms.RadioButton();
            this.rtn_true = new System.Windows.Forms.RadioButton();
            this.cbb_ThumbPercent = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cbb_DiskThumbPercent = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.rb_ftp = new System.Windows.Forms.RadioButton();
            this.rb_disk = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_userName = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_pwd = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btn_connect = new System.Windows.Forms.Button();
            this.pathCtrl_Pic = new UserControlLibrary.PathCtrl();
            this.pathCtrl_PicRemoteDisk = new UserControlLibrary.PathCtrl();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cbb_ThumbPercentRes = new System.Windows.Forms.ComboBox();
            this.txt_userNameRes = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txt_pwdRes = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.cbb_DiskThumbPercentRes = new System.Windows.Forms.ComboBox();
            this.btn_connectRes = new System.Windows.Forms.Button();
            this.pathCtrl_PicRes = new UserControlLibrary.PathCtrl();
            this.label21 = new System.Windows.Forms.Label();
            this.pathCtrl_PicRemoteDiskRes = new UserControlLibrary.PathCtrl();
            this.rb_ftpRes = new System.Windows.Forms.RadioButton();
            this.rb_diskRes = new System.Windows.Forms.RadioButton();
            this.pathCtrl_MesLog = new UserControlLibrary.PathCtrl();
            this.pathCtrl_Data = new UserControlLibrary.PathCtrl();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Threshold)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_SaveConfig
            // 
            this.btn_SaveConfig.Location = new System.Drawing.Point(892, 909);
            this.btn_SaveConfig.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_SaveConfig.Name = "btn_SaveConfig";
            this.btn_SaveConfig.Size = new System.Drawing.Size(112, 34);
            this.btn_SaveConfig.TabIndex = 1;
            this.btn_SaveConfig.Text = "保存";
            this.btn_SaveConfig.UseVisualStyleBackColor = true;
            this.btn_SaveConfig.Click += new System.EventHandler(this.btn_SaveConfig_Click);
            // 
            // chk_SaveRawImage
            // 
            this.chk_SaveRawImage.AutoSize = true;
            this.chk_SaveRawImage.Location = new System.Drawing.Point(9, 33);
            this.chk_SaveRawImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chk_SaveRawImage.Name = "chk_SaveRawImage";
            this.chk_SaveRawImage.Size = new System.Drawing.Size(142, 22);
            this.chk_SaveRawImage.TabIndex = 4;
            this.chk_SaveRawImage.Text = "保存本地原图";
            this.chk_SaveRawImage.UseVisualStyleBackColor = true;
            // 
            // chk_SaveDealImage
            // 
            this.chk_SaveDealImage.AutoSize = true;
            this.chk_SaveDealImage.Location = new System.Drawing.Point(9, 68);
            this.chk_SaveDealImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chk_SaveDealImage.Name = "chk_SaveDealImage";
            this.chk_SaveDealImage.Size = new System.Drawing.Size(160, 22);
            this.chk_SaveDealImage.TabIndex = 5;
            this.chk_SaveDealImage.Text = "保存本地结果图";
            this.chk_SaveDealImage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.cmb_ImageToolRemoteType);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.cmb_ImageToolType);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.cmb_ImageRemoteType);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_days_Deal);
            this.groupBox1.Controls.Add(this.chk_SaveOKNGGlobal);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmb_ImageType);
            this.groupBox1.Controls.Add(this.chk_Delete);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_days);
            this.groupBox1.Controls.Add(this.chk_SaveRemoteDealImage);
            this.groupBox1.Controls.Add(this.chk_SaveDealImage);
            this.groupBox1.Controls.Add(this.chk_SaveRemoteRawImage);
            this.groupBox1.Controls.Add(this.chk_SaveRawImage);
            this.groupBox1.Location = new System.Drawing.Point(21, 664);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(645, 279);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "存储设置";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(354, 165);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(143, 18);
            this.label24.TabIndex = 21;
            this.label24.Text = "处理图网盘类型:";
            // 
            // cmb_ImageToolRemoteType
            // 
            this.cmb_ImageToolRemoteType.FormattingEnabled = true;
            this.cmb_ImageToolRemoteType.Location = new System.Drawing.Point(504, 160);
            this.cmb_ImageToolRemoteType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmb_ImageToolRemoteType.Name = "cmb_ImageToolRemoteType";
            this.cmb_ImageToolRemoteType.Size = new System.Drawing.Size(115, 26);
            this.cmb_ImageToolRemoteType.TabIndex = 20;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(390, 123);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(107, 18);
            this.label23.TabIndex = 19;
            this.label23.Text = "处理图类型:";
            // 
            // cmb_ImageToolType
            // 
            this.cmb_ImageToolType.FormattingEnabled = true;
            this.cmb_ImageToolType.Location = new System.Drawing.Point(504, 118);
            this.cmb_ImageToolType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmb_ImageToolType.Name = "cmb_ImageToolType";
            this.cmb_ImageToolType.Size = new System.Drawing.Size(115, 26);
            this.cmb_ImageToolType.TabIndex = 18;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(374, 80);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(125, 18);
            this.label22.TabIndex = 17;
            this.label22.Text = "原图网盘类型:";
            // 
            // cmb_ImageRemoteType
            // 
            this.cmb_ImageRemoteType.FormattingEnabled = true;
            this.cmb_ImageRemoteType.Location = new System.Drawing.Point(504, 75);
            this.cmb_ImageRemoteType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmb_ImageRemoteType.Name = "cmb_ImageRemoteType";
            this.cmb_ImageRemoteType.Size = new System.Drawing.Size(115, 26);
            this.cmb_ImageRemoteType.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(270, 228);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 18);
            this.label4.TabIndex = 15;
            this.label4.Text = "单位/(天)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 228);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 18);
            this.label5.TabIndex = 14;
            this.label5.Text = "效果图保留时间：";
            // 
            // txt_days_Deal
            // 
            this.txt_days_Deal.Location = new System.Drawing.Point(194, 222);
            this.txt_days_Deal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_days_Deal.Name = "txt_days_Deal";
            this.txt_days_Deal.Size = new System.Drawing.Size(60, 28);
            this.txt_days_Deal.TabIndex = 13;
            // 
            // chk_SaveOKNGGlobal
            // 
            this.chk_SaveOKNGGlobal.AutoSize = true;
            this.chk_SaveOKNGGlobal.Location = new System.Drawing.Point(9, 140);
            this.chk_SaveOKNGGlobal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chk_SaveOKNGGlobal.Name = "chk_SaveOKNGGlobal";
            this.chk_SaveOKNGGlobal.Size = new System.Drawing.Size(286, 22);
            this.chk_SaveOKNGGlobal.TabIndex = 12;
            this.chk_SaveOKNGGlobal.Text = "区分OK、NG、Global文件夹存储";
            this.chk_SaveOKNGGlobal.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(410, 33);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 18);
            this.label3.TabIndex = 11;
            this.label3.Text = "原图类型:";
            // 
            // cmb_ImageType
            // 
            this.cmb_ImageType.FormattingEnabled = true;
            this.cmb_ImageType.Location = new System.Drawing.Point(504, 30);
            this.cmb_ImageType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmb_ImageType.Name = "cmb_ImageType";
            this.cmb_ImageType.Size = new System.Drawing.Size(115, 26);
            this.cmb_ImageType.TabIndex = 7;
            // 
            // chk_Delete
            // 
            this.chk_Delete.AutoSize = true;
            this.chk_Delete.Location = new System.Drawing.Point(9, 104);
            this.chk_Delete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chk_Delete.Name = "chk_Delete";
            this.chk_Delete.Size = new System.Drawing.Size(142, 22);
            this.chk_Delete.TabIndex = 10;
            this.chk_Delete.Text = "是否删除图片";
            this.chk_Delete.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(270, 184);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 18);
            this.label2.TabIndex = 9;
            this.label2.Text = "单位/(天)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 184);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "原图保留时间：";
            // 
            // txt_days
            // 
            this.txt_days.Location = new System.Drawing.Point(194, 178);
            this.txt_days.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_days.Name = "txt_days";
            this.txt_days.Size = new System.Drawing.Size(60, 28);
            this.txt_days.TabIndex = 7;
            // 
            // chk_SaveRemoteDealImage
            // 
            this.chk_SaveRemoteDealImage.AutoSize = true;
            this.chk_SaveRemoteDealImage.Location = new System.Drawing.Point(178, 75);
            this.chk_SaveRemoteDealImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chk_SaveRemoteDealImage.Name = "chk_SaveRemoteDealImage";
            this.chk_SaveRemoteDealImage.Size = new System.Drawing.Size(160, 22);
            this.chk_SaveRemoteDealImage.TabIndex = 5;
            this.chk_SaveRemoteDealImage.Text = "保存网盘结果图";
            this.chk_SaveRemoteDealImage.UseVisualStyleBackColor = true;
            // 
            // chk_SaveRemoteRawImage
            // 
            this.chk_SaveRemoteRawImage.AutoSize = true;
            this.chk_SaveRemoteRawImage.Location = new System.Drawing.Point(178, 36);
            this.chk_SaveRemoteRawImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chk_SaveRemoteRawImage.Name = "chk_SaveRemoteRawImage";
            this.chk_SaveRemoteRawImage.Size = new System.Drawing.Size(142, 22);
            this.chk_SaveRemoteRawImage.TabIndex = 4;
            this.chk_SaveRemoteRawImage.Text = "保存网盘原图";
            this.chk_SaveRemoteRawImage.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.dtp_PollTime1);
            this.groupBox2.Controls.Add(this.dtp_PollTime2);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.nud_Threshold);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.rtn_false);
            this.groupBox2.Controls.Add(this.rtn_true);
            this.groupBox2.Location = new System.Drawing.Point(675, 664);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(411, 236);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "磁盘报警设置";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 201);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(260, 18);
            this.label9.TabIndex = 38;
            this.label9.Text = "注：内存最大值包含当前输入值";
            // 
            // dtp_PollTime1
            // 
            this.dtp_PollTime1.CustomFormat = "HH:mm:ss ";
            this.dtp_PollTime1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_PollTime1.Location = new System.Drawing.Point(210, 110);
            this.dtp_PollTime1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtp_PollTime1.Name = "dtp_PollTime1";
            this.dtp_PollTime1.ShowUpDown = true;
            this.dtp_PollTime1.Size = new System.Drawing.Size(106, 28);
            this.dtp_PollTime1.TabIndex = 37;
            this.dtp_PollTime1.Value = new System.DateTime(2022, 4, 19, 8, 0, 0, 0);
            // 
            // dtp_PollTime2
            // 
            this.dtp_PollTime2.CustomFormat = "HH:mm:ss ";
            this.dtp_PollTime2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_PollTime2.Location = new System.Drawing.Point(210, 152);
            this.dtp_PollTime2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtp_PollTime2.Name = "dtp_PollTime2";
            this.dtp_PollTime2.ShowUpDown = true;
            this.dtp_PollTime2.Size = new System.Drawing.Size(106, 28);
            this.dtp_PollTime2.TabIndex = 36;
            this.dtp_PollTime2.Value = new System.DateTime(2022, 4, 19, 20, 0, 0, 0);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 159);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(107, 18);
            this.label11.TabIndex = 16;
            this.label11.Text = "检测时间2：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 117);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 18);
            this.label10.TabIndex = 15;
            this.label10.Text = "检测时间1：";
            // 
            // nud_Threshold
            // 
            this.nud_Threshold.Location = new System.Drawing.Point(210, 68);
            this.nud_Threshold.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nud_Threshold.Maximum = new decimal(new int[] {
            8000000,
            0,
            0,
            0});
            this.nud_Threshold.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_Threshold.Name = "nud_Threshold";
            this.nud_Threshold.Size = new System.Drawing.Size(108, 28);
            this.nud_Threshold.TabIndex = 13;
            this.nud_Threshold.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(326, 75);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 18);
            this.label7.TabIndex = 12;
            this.label7.Text = "单位/(M)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 75);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 18);
            this.label8.TabIndex = 11;
            this.label8.Text = "报警阈值：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 33);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(170, 18);
            this.label6.TabIndex = 1;
            this.label6.Text = "磁盘检测是否开启：";
            // 
            // rtn_false
            // 
            this.rtn_false.AutoSize = true;
            this.rtn_false.Checked = true;
            this.rtn_false.Location = new System.Drawing.Point(272, 34);
            this.rtn_false.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rtn_false.Name = "rtn_false";
            this.rtn_false.Size = new System.Drawing.Size(51, 22);
            this.rtn_false.TabIndex = 0;
            this.rtn_false.TabStop = true;
            this.rtn_false.Text = "否";
            this.rtn_false.UseVisualStyleBackColor = true;
            // 
            // rtn_true
            // 
            this.rtn_true.AutoSize = true;
            this.rtn_true.Location = new System.Drawing.Point(210, 33);
            this.rtn_true.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rtn_true.Name = "rtn_true";
            this.rtn_true.Size = new System.Drawing.Size(51, 22);
            this.rtn_true.TabIndex = 0;
            this.rtn_true.Text = "是";
            this.rtn_true.UseVisualStyleBackColor = true;
            // 
            // cbb_ThumbPercent
            // 
            this.cbb_ThumbPercent.FormattingEnabled = true;
            this.cbb_ThumbPercent.Items.AddRange(new object[] {
            "10%",
            "20%",
            "40%",
            "60%",
            "80%",
            "100%"});
            this.cbb_ThumbPercent.Location = new System.Drawing.Point(867, 44);
            this.cbb_ThumbPercent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbb_ThumbPercent.Name = "cbb_ThumbPercent";
            this.cbb_ThumbPercent.Size = new System.Drawing.Size(115, 26);
            this.cbb_ThumbPercent.TabIndex = 10;
            this.cbb_ThumbPercent.Text = "100%";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(766, 48);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 18);
            this.label12.TabIndex = 11;
            this.label12.Text = "存图比例：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(766, 99);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(98, 18);
            this.label13.TabIndex = 13;
            this.label13.Text = "存图比例：";
            // 
            // cbb_DiskThumbPercent
            // 
            this.cbb_DiskThumbPercent.FormattingEnabled = true;
            this.cbb_DiskThumbPercent.Items.AddRange(new object[] {
            "10%",
            "20%",
            "40%",
            "60%",
            "80%",
            "100%"});
            this.cbb_DiskThumbPercent.Location = new System.Drawing.Point(867, 96);
            this.cbb_DiskThumbPercent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbb_DiskThumbPercent.Name = "cbb_DiskThumbPercent";
            this.cbb_DiskThumbPercent.Size = new System.Drawing.Size(115, 26);
            this.cbb_DiskThumbPercent.TabIndex = 12;
            this.cbb_DiskThumbPercent.Text = "100%";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(21, 147);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(98, 18);
            this.label14.TabIndex = 16;
            this.label14.Text = "存图类型：";
            // 
            // rb_ftp
            // 
            this.rb_ftp.AutoSize = true;
            this.rb_ftp.Location = new System.Drawing.Point(123, 192);
            this.rb_ftp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rb_ftp.Name = "rb_ftp";
            this.rb_ftp.Size = new System.Drawing.Size(60, 22);
            this.rb_ftp.TabIndex = 14;
            this.rb_ftp.Text = "FTP";
            this.rb_ftp.UseVisualStyleBackColor = true;
            this.rb_ftp.CheckedChanged += new System.EventHandler(this.rb_ftp_CheckedChanged);
            // 
            // rb_disk
            // 
            this.rb_disk.AutoSize = true;
            this.rb_disk.Checked = true;
            this.rb_disk.Location = new System.Drawing.Point(123, 147);
            this.rb_disk.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rb_disk.Name = "rb_disk";
            this.rb_disk.Size = new System.Drawing.Size(69, 22);
            this.rb_disk.TabIndex = 15;
            this.rb_disk.TabStop = true;
            this.rb_disk.Text = "磁盘";
            this.rb_disk.UseVisualStyleBackColor = true;
            this.rb_disk.CheckedChanged += new System.EventHandler(this.rb_ftp_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.cbb_ThumbPercent);
            this.groupBox3.Controls.Add(this.txt_userName);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.txt_pwd);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.cbb_DiskThumbPercent);
            this.groupBox3.Controls.Add(this.btn_connect);
            this.groupBox3.Controls.Add(this.pathCtrl_Pic);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.pathCtrl_PicRemoteDisk);
            this.groupBox3.Controls.Add(this.rb_ftp);
            this.groupBox3.Controls.Add(this.rb_disk);
            this.groupBox3.Location = new System.Drawing.Point(26, 18);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(1070, 249);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "原图设置";
            // 
            // txt_userName
            // 
            this.txt_userName.Enabled = false;
            this.txt_userName.Location = new System.Drawing.Point(324, 142);
            this.txt_userName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_userName.Name = "txt_userName";
            this.txt_userName.Size = new System.Drawing.Size(266, 28);
            this.txt_userName.TabIndex = 114;
            this.txt_userName.Text = "LCTPHLPJC";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(244, 147);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 18);
            this.label15.TabIndex = 115;
            this.label15.Text = "用户名:";
            // 
            // txt_pwd
            // 
            this.txt_pwd.Enabled = false;
            this.txt_pwd.Location = new System.Drawing.Point(324, 190);
            this.txt_pwd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_pwd.Name = "txt_pwd";
            this.txt_pwd.Size = new System.Drawing.Size(266, 28);
            this.txt_pwd.TabIndex = 112;
            this.txt_pwd.Text = "Svolt@2022";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(262, 196);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 18);
            this.label16.TabIndex = 113;
            this.label16.Text = "密码:";
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(622, 189);
            this.btn_connect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(112, 34);
            this.btn_connect.TabIndex = 1;
            this.btn_connect.Text = "连接";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // pathCtrl_Pic
            // 
            this.pathCtrl_Pic.AutoSize = true;
            this.pathCtrl_Pic.Label_Text = "本地图片存储路径配置：";
            this.pathCtrl_Pic.Location = new System.Drawing.Point(14, 32);
            this.pathCtrl_Pic.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pathCtrl_Pic.Name = "pathCtrl_Pic";
            this.pathCtrl_Pic.Size = new System.Drawing.Size(873, 68);
            this.pathCtrl_Pic.TabIndex = 0;
            this.pathCtrl_Pic.TextBoxWidth = 320;
            // 
            // pathCtrl_PicRemoteDisk
            // 
            this.pathCtrl_PicRemoteDisk.AutoSize = true;
            this.pathCtrl_PicRemoteDisk.Label_Text = "公共盘图片存储路径配置：";
            this.pathCtrl_PicRemoteDisk.Location = new System.Drawing.Point(12, 84);
            this.pathCtrl_PicRemoteDisk.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pathCtrl_PicRemoteDisk.Name = "pathCtrl_PicRemoteDisk";
            this.pathCtrl_PicRemoteDisk.Size = new System.Drawing.Size(885, 68);
            this.pathCtrl_PicRemoteDisk.TabIndex = 8;
            this.pathCtrl_PicRemoteDisk.TextBoxWidth = 310;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.cbb_ThumbPercentRes);
            this.groupBox4.Controls.Add(this.txt_userNameRes);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.txt_pwdRes);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.cbb_DiskThumbPercentRes);
            this.groupBox4.Controls.Add(this.btn_connectRes);
            this.groupBox4.Controls.Add(this.pathCtrl_PicRes);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.pathCtrl_PicRemoteDiskRes);
            this.groupBox4.Controls.Add(this.rb_ftpRes);
            this.groupBox4.Controls.Add(this.rb_diskRes);
            this.groupBox4.Location = new System.Drawing.Point(26, 288);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(1070, 249);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "结果图设置";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(762, 48);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(98, 18);
            this.label17.TabIndex = 11;
            this.label17.Text = "存图比例：";
            // 
            // cbb_ThumbPercentRes
            // 
            this.cbb_ThumbPercentRes.FormattingEnabled = true;
            this.cbb_ThumbPercentRes.Items.AddRange(new object[] {
            "10%",
            "20%",
            "40%",
            "60%",
            "80%",
            "100%"});
            this.cbb_ThumbPercentRes.Location = new System.Drawing.Point(862, 44);
            this.cbb_ThumbPercentRes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbb_ThumbPercentRes.Name = "cbb_ThumbPercentRes";
            this.cbb_ThumbPercentRes.Size = new System.Drawing.Size(115, 26);
            this.cbb_ThumbPercentRes.TabIndex = 10;
            this.cbb_ThumbPercentRes.Text = "100%";
            // 
            // txt_userNameRes
            // 
            this.txt_userNameRes.Enabled = false;
            this.txt_userNameRes.Location = new System.Drawing.Point(324, 142);
            this.txt_userNameRes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_userNameRes.Name = "txt_userNameRes";
            this.txt_userNameRes.Size = new System.Drawing.Size(266, 28);
            this.txt_userNameRes.TabIndex = 114;
            this.txt_userNameRes.Text = "LCTPHLPJC";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(244, 147);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(71, 18);
            this.label18.TabIndex = 115;
            this.label18.Text = "用户名:";
            // 
            // txt_pwdRes
            // 
            this.txt_pwdRes.Enabled = false;
            this.txt_pwdRes.Location = new System.Drawing.Point(324, 190);
            this.txt_pwdRes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_pwdRes.Name = "txt_pwdRes";
            this.txt_pwdRes.Size = new System.Drawing.Size(266, 28);
            this.txt_pwdRes.TabIndex = 112;
            this.txt_pwdRes.Text = "Svolt@2022";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(262, 196);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 18);
            this.label19.TabIndex = 113;
            this.label19.Text = "密码:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(766, 100);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(98, 18);
            this.label20.TabIndex = 13;
            this.label20.Text = "存图比例：";
            // 
            // cbb_DiskThumbPercentRes
            // 
            this.cbb_DiskThumbPercentRes.FormattingEnabled = true;
            this.cbb_DiskThumbPercentRes.Items.AddRange(new object[] {
            "10%",
            "20%",
            "40%",
            "60%",
            "80%",
            "100%"});
            this.cbb_DiskThumbPercentRes.Location = new System.Drawing.Point(867, 98);
            this.cbb_DiskThumbPercentRes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbb_DiskThumbPercentRes.Name = "cbb_DiskThumbPercentRes";
            this.cbb_DiskThumbPercentRes.Size = new System.Drawing.Size(115, 26);
            this.cbb_DiskThumbPercentRes.TabIndex = 12;
            this.cbb_DiskThumbPercentRes.Text = "100%";
            // 
            // btn_connectRes
            // 
            this.btn_connectRes.Location = new System.Drawing.Point(622, 188);
            this.btn_connectRes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_connectRes.Name = "btn_connectRes";
            this.btn_connectRes.Size = new System.Drawing.Size(112, 34);
            this.btn_connectRes.TabIndex = 1;
            this.btn_connectRes.Text = "连接";
            this.btn_connectRes.UseVisualStyleBackColor = true;
            this.btn_connectRes.Click += new System.EventHandler(this.btn_connectRes_Click);
            // 
            // pathCtrl_PicRes
            // 
            this.pathCtrl_PicRes.AutoSize = true;
            this.pathCtrl_PicRes.Label_Text = "本地图片存储路径配置：";
            this.pathCtrl_PicRes.Location = new System.Drawing.Point(14, 32);
            this.pathCtrl_PicRes.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pathCtrl_PicRes.Name = "pathCtrl_PicRes";
            this.pathCtrl_PicRes.Size = new System.Drawing.Size(873, 68);
            this.pathCtrl_PicRes.TabIndex = 0;
            this.pathCtrl_PicRes.TextBoxWidth = 320;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(21, 147);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(98, 18);
            this.label21.TabIndex = 16;
            this.label21.Text = "存图类型：";
            // 
            // pathCtrl_PicRemoteDiskRes
            // 
            this.pathCtrl_PicRemoteDiskRes.AutoSize = true;
            this.pathCtrl_PicRemoteDiskRes.Label_Text = "公共盘图片存储路径配置：";
            this.pathCtrl_PicRemoteDiskRes.Location = new System.Drawing.Point(12, 84);
            this.pathCtrl_PicRemoteDiskRes.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pathCtrl_PicRemoteDiskRes.Name = "pathCtrl_PicRemoteDiskRes";
            this.pathCtrl_PicRemoteDiskRes.Size = new System.Drawing.Size(885, 68);
            this.pathCtrl_PicRemoteDiskRes.TabIndex = 8;
            this.pathCtrl_PicRemoteDiskRes.TextBoxWidth = 310;
            // 
            // rb_ftpRes
            // 
            this.rb_ftpRes.AutoSize = true;
            this.rb_ftpRes.Location = new System.Drawing.Point(123, 192);
            this.rb_ftpRes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rb_ftpRes.Name = "rb_ftpRes";
            this.rb_ftpRes.Size = new System.Drawing.Size(60, 22);
            this.rb_ftpRes.TabIndex = 14;
            this.rb_ftpRes.Text = "FTP";
            this.rb_ftpRes.UseVisualStyleBackColor = true;
            this.rb_ftpRes.CheckedChanged += new System.EventHandler(this.rb_diskRes_CheckedChanged);
            // 
            // rb_diskRes
            // 
            this.rb_diskRes.AutoSize = true;
            this.rb_diskRes.Checked = true;
            this.rb_diskRes.Location = new System.Drawing.Point(123, 147);
            this.rb_diskRes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rb_diskRes.Name = "rb_diskRes";
            this.rb_diskRes.Size = new System.Drawing.Size(69, 22);
            this.rb_diskRes.TabIndex = 15;
            this.rb_diskRes.TabStop = true;
            this.rb_diskRes.Text = "磁盘";
            this.rb_diskRes.UseVisualStyleBackColor = true;
            this.rb_diskRes.CheckedChanged += new System.EventHandler(this.rb_diskRes_CheckedChanged);
            // 
            // pathCtrl_MesLog
            // 
            this.pathCtrl_MesLog.AutoSize = true;
            this.pathCtrl_MesLog.Label_Text = "MesLog保存路径：";
            this.pathCtrl_MesLog.Location = new System.Drawing.Point(21, 591);
            this.pathCtrl_MesLog.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pathCtrl_MesLog.Name = "pathCtrl_MesLog";
            this.pathCtrl_MesLog.Size = new System.Drawing.Size(693, 50);
            this.pathCtrl_MesLog.TabIndex = 7;
            this.pathCtrl_MesLog.TextBoxWidth = 260;
            // 
            // pathCtrl_Data
            // 
            this.pathCtrl_Data.AutoSize = true;
            this.pathCtrl_Data.Label_Text = "数据保存路径：";
            this.pathCtrl_Data.Location = new System.Drawing.Point(21, 548);
            this.pathCtrl_Data.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pathCtrl_Data.Name = "pathCtrl_Data";
            this.pathCtrl_Data.Size = new System.Drawing.Size(666, 50);
            this.pathCtrl_Data.TabIndex = 2;
            this.pathCtrl_Data.TextBoxWidth = 260;
            // 
            // Frm_FileParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 950);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pathCtrl_MesLog);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pathCtrl_Data);
            this.Controls.Add(this.btn_SaveConfig);
            this.Controls.Add(this.groupBox3);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Frm_FileParam";
            this.Text = "Frm_Param";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Threshold)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PathCtrl pathCtrl_Pic;

        private Button btn_SaveConfig;

        private PathCtrl pathCtrl_Data;

        private CheckBox chk_SaveRawImage;

        private CheckBox chk_SaveDealImage;

        private GroupBox groupBox1;

        private Label label2;

        private Label label1;

        private TextBox txt_days;

        private CheckBox chk_Delete;

        private Label label3;

        private ComboBox cmb_ImageType;

        private CheckBox chk_SaveOKNGGlobal;

        private PathCtrl pathCtrl_MesLog;

        private Label label4;

        private Label label5;

        private TextBox txt_days_Deal;

        private PathCtrl pathCtrl_PicRemoteDisk;

        private GroupBox groupBox2;

        private Label label6;

        private RadioButton rtn_false;

        private RadioButton rtn_true;

        private NumericUpDown nud_Threshold;

        private Label label7;

        private Label label8;

        private Label label11;

        private Label label10;

        private DateTimePicker dtp_PollTime2;

        private DateTimePicker dtp_PollTime1;

        private Label label9;

        private ComboBox cbb_ThumbPercent;

        private Label label12;

        private Label label13;

        private ComboBox cbb_DiskThumbPercent;

        private Label label14;

        private RadioButton rb_ftp;

        private RadioButton rb_disk;

        private GroupBox groupBox3;

        private TextBox txt_userName;

        private Label label15;

        private TextBox txt_pwd;

        private Label label16;

        private Button btn_connect;

        private GroupBox groupBox4;

        private Label label17;

        private ComboBox cbb_ThumbPercentRes;

        private TextBox txt_userNameRes;

        private Label label18;

        private TextBox txt_pwdRes;

        private Label label19;

        private Label label20;

        private ComboBox cbb_DiskThumbPercentRes;

        private Button btn_connectRes;

        private PathCtrl pathCtrl_PicRes;

        private Label label21;

        private PathCtrl pathCtrl_PicRemoteDiskRes;

        private RadioButton rb_ftpRes;

        private RadioButton rb_diskRes;

        private CheckBox chk_SaveRemoteDealImage;

        private CheckBox chk_SaveRemoteRawImage;

        private Label label22;

        private ComboBox cmb_ImageRemoteType;

        private Label label24;

        private ComboBox cmb_ImageToolRemoteType;

        private Label label23;

        private ComboBox cmb_ImageToolType;
    }
}