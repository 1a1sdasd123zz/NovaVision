using System.Windows.Forms;
using NovaVision.UserControlLibrary;

namespace NovaVision.VisionForm.CarameFrm
{
    partial class FrmHardwareDeployment
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
            this.cbBasler2DGige = new UserControlLibrary.MyCheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbBetterway2DGige = new UserControlLibrary.MyCheckBox();
            this.cbDahua2DGige = new UserControlLibrary.MyCheckBox();
            this.cbDaheng2DGige = new UserControlLibrary.MyCheckBox();
            this.cbHikrobot2DGige = new UserControlLibrary.MyCheckBox();
            this.cbCognex2DGige = new UserControlLibrary.MyCheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbDaheng2DUsb = new UserControlLibrary.MyCheckBox();
            this.cbHikrobot2DUsb = new UserControlLibrary.MyCheckBox();
            this.cbBasler2DUsb = new UserControlLibrary.MyCheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbDahua2DLineGige = new UserControlLibrary.MyCheckBox();
            this.cbHikrobot2DLineGige = new UserControlLibrary.MyCheckBox();
            this.cbDalsa2DLineGige = new UserControlLibrary.MyCheckBox();
            this.cbBasler2DLineGige = new UserControlLibrary.MyCheckBox();
            this.cbCognex2DLineGige = new UserControlLibrary.MyCheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbHikCL = new UserControlLibrary.MyCheckBox();
            this.cbMatrox = new UserControlLibrary.MyCheckBox();
            this.cbIkap = new UserControlLibrary.MyCheckBox();
            this.cbAurora = new UserControlLibrary.MyCheckBox();
            this.cbXtium = new UserControlLibrary.MyCheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            base.SuspendLayout();
            this.cbBasler2DGige.ActiveColor = System.Drawing.Color.DodgerBlue;
            this.cbBasler2DGige.CheckColor = System.Drawing.Color.Blue;
            this.cbBasler2DGige.Checked = false;
            this.cbBasler2DGige.isAvtice = false;
            this.cbBasler2DGige.Location = new System.Drawing.Point(17, 23);
            this.cbBasler2DGige.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbBasler2DGige.Name = "cbBasler2DGige";
            this.cbBasler2DGige.Size = new System.Drawing.Size(92, 26);
            this.cbBasler2DGige.TabIndex = 0;
            this.cbBasler2DGige.Value = "Basler2DGige";
            this.cbBasler2DGige.CheckedChanged += new UserControlLibrary.MyCheckBox.CheckedHandle(checkbox_CheckedChanged);
            this.groupBox1.Controls.Add(this.cbBetterway2DGige);
            this.groupBox1.Controls.Add(this.cbDahua2DGige);
            this.groupBox1.Controls.Add(this.cbDaheng2DGige);
            this.groupBox1.Controls.Add(this.cbHikrobot2DGige);
            this.groupBox1.Controls.Add(this.cbCognex2DGige);
            this.groupBox1.Controls.Add(this.cbBasler2DGige);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(149, 245);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "2DGige相机";
            this.cbBetterway2DGige.ActiveColor = System.Drawing.Color.DodgerBlue;
            this.cbBetterway2DGige.CheckColor = System.Drawing.Color.Blue;
            this.cbBetterway2DGige.Checked = false;
            this.cbBetterway2DGige.isAvtice = false;
            this.cbBetterway2DGige.Location = new System.Drawing.Point(17, 161);
            this.cbBetterway2DGige.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbBetterway2DGige.Name = "cbBetterway2DGige";
            this.cbBetterway2DGige.Size = new System.Drawing.Size(110, 26);
            this.cbBetterway2DGige.TabIndex = 5;
            this.cbBetterway2DGige.Value = "Betterway2DGige";
            this.cbBetterway2DGige.CheckedChanged += new UserControlLibrary.MyCheckBox.CheckedHandle(checkbox_CheckedChanged);
            this.cbDahua2DGige.ActiveColor = System.Drawing.Color.DodgerBlue;
            this.cbDahua2DGige.CheckColor = System.Drawing.Color.Blue;
            this.cbDahua2DGige.Checked = false;
            this.cbDahua2DGige.isAvtice = false;
            this.cbDahua2DGige.Location = new System.Drawing.Point(17, 135);
            this.cbDahua2DGige.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbDahua2DGige.Name = "cbDahua2DGige";
            this.cbDahua2DGige.Size = new System.Drawing.Size(86, 26);
            this.cbDahua2DGige.TabIndex = 4;
            this.cbDahua2DGige.Value = "Dahua2DGige";
            this.cbDahua2DGige.CheckedChanged += new UserControlLibrary.MyCheckBox.CheckedHandle(checkbox_CheckedChanged);
            this.cbDaheng2DGige.ActiveColor = System.Drawing.Color.DodgerBlue;
            this.cbDaheng2DGige.CheckColor = System.Drawing.Color.Blue;
            this.cbDaheng2DGige.Checked = false;
            this.cbDaheng2DGige.isAvtice = false;
            this.cbDaheng2DGige.Location = new System.Drawing.Point(17, 109);
            this.cbDaheng2DGige.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbDaheng2DGige.Name = "cbDaheng2DGige";
            this.cbDaheng2DGige.Size = new System.Drawing.Size(92, 26);
            this.cbDaheng2DGige.TabIndex = 3;
            this.cbDaheng2DGige.Value = "Daheng2DGige";
            this.cbDaheng2DGige.CheckedChanged += new UserControlLibrary.MyCheckBox.CheckedHandle(checkbox_CheckedChanged);
            this.cbHikrobot2DGige.ActiveColor = System.Drawing.Color.DodgerBlue;
            this.cbHikrobot2DGige.CheckColor = System.Drawing.Color.Blue;
            this.cbHikrobot2DGige.Checked = false;
            this.cbHikrobot2DGige.isAvtice = false;
            this.cbHikrobot2DGige.Location = new System.Drawing.Point(17, 81);
            this.cbHikrobot2DGige.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbHikrobot2DGige.Name = "cbHikrobot2DGige";
            this.cbHikrobot2DGige.Size = new System.Drawing.Size(104, 26);
            this.cbHikrobot2DGige.TabIndex = 2;
            this.cbHikrobot2DGige.Value = "Hikrobot2DGige";
            this.cbHikrobot2DGige.CheckedChanged += new UserControlLibrary.MyCheckBox.CheckedHandle(checkbox_CheckedChanged);
            this.cbCognex2DGige.ActiveColor = System.Drawing.Color.DodgerBlue;
            this.cbCognex2DGige.CheckColor = System.Drawing.Color.Blue;
            this.cbCognex2DGige.Checked = false;
            this.cbCognex2DGige.isAvtice = false;
            this.cbCognex2DGige.Location = new System.Drawing.Point(17, 52);
            this.cbCognex2DGige.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbCognex2DGige.Name = "cbCognex2DGige";
            this.cbCognex2DGige.Size = new System.Drawing.Size(92, 26);
            this.cbCognex2DGige.TabIndex = 1;
            this.cbCognex2DGige.Value = "Cognex2DGige";
            this.cbCognex2DGige.CheckedChanged += new UserControlLibrary.MyCheckBox.CheckedHandle(checkbox_CheckedChanged);
            this.groupBox2.Controls.Add(this.cbDaheng2DUsb);
            this.groupBox2.Controls.Add(this.cbHikrobot2DUsb);
            this.groupBox2.Controls.Add(this.cbBasler2DUsb);
            this.groupBox2.Location = new System.Drawing.Point(178, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(149, 245);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "2DUsb3.0相机";
            this.cbDaheng2DUsb.ActiveColor = System.Drawing.Color.DodgerBlue;
            this.cbDaheng2DUsb.CheckColor = System.Drawing.Color.Blue;
            this.cbDaheng2DUsb.Checked = false;
            this.cbDaheng2DUsb.isAvtice = false;
            this.cbDaheng2DUsb.Location = new System.Drawing.Point(17, 81);
            this.cbDaheng2DUsb.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbDaheng2DUsb.Name = "cbDaheng2DUsb";
            this.cbDaheng2DUsb.Size = new System.Drawing.Size(104, 26);
            this.cbDaheng2DUsb.TabIndex = 2;
            this.cbDaheng2DUsb.Value = "Daheng2DUsb3.0";
            this.cbDaheng2DUsb.CheckedChanged += new UserControlLibrary.MyCheckBox.CheckedHandle(checkbox_CheckedChanged);
            this.cbHikrobot2DUsb.ActiveColor = System.Drawing.Color.DodgerBlue;
            this.cbHikrobot2DUsb.CheckColor = System.Drawing.Color.Blue;
            this.cbHikrobot2DUsb.Checked = false;
            this.cbHikrobot2DUsb.isAvtice = false;
            this.cbHikrobot2DUsb.Location = new System.Drawing.Point(17, 52);
            this.cbHikrobot2DUsb.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbHikrobot2DUsb.Name = "cbHikrobot2DUsb";
            this.cbHikrobot2DUsb.Size = new System.Drawing.Size(116, 26);
            this.cbHikrobot2DUsb.TabIndex = 1;
            this.cbHikrobot2DUsb.Value = "Hikrobot2DUsb3.0";
            this.cbHikrobot2DUsb.CheckedChanged += new UserControlLibrary.MyCheckBox.CheckedHandle(checkbox_CheckedChanged);
            this.cbBasler2DUsb.ActiveColor = System.Drawing.Color.DodgerBlue;
            this.cbBasler2DUsb.CheckColor = System.Drawing.Color.Blue;
            this.cbBasler2DUsb.Checked = false;
            this.cbBasler2DUsb.isAvtice = false;
            this.cbBasler2DUsb.Location = new System.Drawing.Point(17, 23);
            this.cbBasler2DUsb.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbBasler2DUsb.Name = "cbBasler2DUsb";
            this.cbBasler2DUsb.Size = new System.Drawing.Size(104, 26);
            this.cbBasler2DUsb.TabIndex = 0;
            this.cbBasler2DUsb.Value = "Basler2DUsb3.0";
            this.cbBasler2DUsb.CheckedChanged += new UserControlLibrary.MyCheckBox.CheckedHandle(checkbox_CheckedChanged);
            this.groupBox3.Controls.Add(this.cbDahua2DLineGige);
            this.groupBox3.Controls.Add(this.cbHikrobot2DLineGige);
            this.groupBox3.Controls.Add(this.cbDalsa2DLineGige);
            this.groupBox3.Controls.Add(this.cbBasler2DLineGige);
            this.groupBox3.Controls.Add(this.cbCognex2DLineGige);
            this.groupBox3.Location = new System.Drawing.Point(346, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(157, 245);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "2DLineGige相机";
            this.cbDahua2DLineGige.ActiveColor = System.Drawing.Color.DodgerBlue;
            this.cbDahua2DLineGige.CheckColor = System.Drawing.Color.Blue;
            this.cbDahua2DLineGige.Checked = false;
            this.cbDahua2DLineGige.isAvtice = false;
            this.cbDahua2DLineGige.Location = new System.Drawing.Point(17, 135);
            this.cbDahua2DLineGige.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbDahua2DLineGige.Name = "cbDahua2DLineGige";
            this.cbDahua2DLineGige.Size = new System.Drawing.Size(110, 26);
            this.cbDahua2DLineGige.TabIndex = 4;
            this.cbDahua2DLineGige.Value = "Dahua2DLineGige";
            this.cbDahua2DLineGige.CheckedChanged += new UserControlLibrary.MyCheckBox.CheckedHandle(checkbox_CheckedChanged);
            this.cbHikrobot2DLineGige.ActiveColor = System.Drawing.Color.DodgerBlue;
            this.cbHikrobot2DLineGige.CheckColor = System.Drawing.Color.Blue;
            this.cbHikrobot2DLineGige.Checked = false;
            this.cbHikrobot2DLineGige.isAvtice = false;
            this.cbHikrobot2DLineGige.Location = new System.Drawing.Point(17, 109);
            this.cbHikrobot2DLineGige.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbHikrobot2DLineGige.Name = "cbHikrobot2DLineGige";
            this.cbHikrobot2DLineGige.Size = new System.Drawing.Size(129, 26);
            this.cbHikrobot2DLineGige.TabIndex = 3;
            this.cbHikrobot2DLineGige.Value = "Hikrobot2DLineGige";
            this.cbHikrobot2DLineGige.CheckedChanged += new UserControlLibrary.MyCheckBox.CheckedHandle(checkbox_CheckedChanged);
            this.cbDalsa2DLineGige.ActiveColor = System.Drawing.Color.DodgerBlue;
            this.cbDalsa2DLineGige.CheckColor = System.Drawing.Color.Blue;
            this.cbDalsa2DLineGige.Checked = false;
            this.cbDalsa2DLineGige.isAvtice = false;
            this.cbDalsa2DLineGige.Location = new System.Drawing.Point(17, 81);
            this.cbDalsa2DLineGige.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbDalsa2DLineGige.Name = "cbDalsa2DLineGige";
            this.cbDalsa2DLineGige.Size = new System.Drawing.Size(110, 26);
            this.cbDalsa2DLineGige.TabIndex = 2;
            this.cbDalsa2DLineGige.Value = "Dalsa2DLineGige";
            this.cbDalsa2DLineGige.CheckedChanged += new UserControlLibrary.MyCheckBox.CheckedHandle(checkbox_CheckedChanged);
            this.cbBasler2DLineGige.ActiveColor = System.Drawing.Color.DodgerBlue;
            this.cbBasler2DLineGige.CheckColor = System.Drawing.Color.Blue;
            this.cbBasler2DLineGige.Checked = false;
            this.cbBasler2DLineGige.isAvtice = false;
            this.cbBasler2DLineGige.Location = new System.Drawing.Point(17, 52);
            this.cbBasler2DLineGige.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbBasler2DLineGige.Name = "cbBasler2DLineGige";
            this.cbBasler2DLineGige.Size = new System.Drawing.Size(116, 26);
            this.cbBasler2DLineGige.TabIndex = 1;
            this.cbBasler2DLineGige.Value = "Basler2DLineGige";
            this.cbBasler2DLineGige.CheckedChanged += new UserControlLibrary.MyCheckBox.CheckedHandle(checkbox_CheckedChanged);
            this.cbCognex2DLineGige.ActiveColor = System.Drawing.Color.DodgerBlue;
            this.cbCognex2DLineGige.CheckColor = System.Drawing.Color.Blue;
            this.cbCognex2DLineGige.Checked = false;
            this.cbCognex2DLineGige.isAvtice = false;
            this.cbCognex2DLineGige.Location = new System.Drawing.Point(17, 23);
            this.cbCognex2DLineGige.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbCognex2DLineGige.Name = "cbCognex2DLineGige";
            this.cbCognex2DLineGige.Size = new System.Drawing.Size(116, 26);
            this.cbCognex2DLineGige.TabIndex = 0;
            this.cbCognex2DLineGige.Value = "Cognex2DLineGige";
            this.cbCognex2DLineGige.CheckedChanged += new UserControlLibrary.MyCheckBox.CheckedHandle(checkbox_CheckedChanged);
            this.groupBox4.Controls.Add(this.cbHikCL);
            this.groupBox4.Controls.Add(this.cbMatrox);
            this.groupBox4.Controls.Add(this.cbIkap);
            this.groupBox4.Controls.Add(this.cbAurora);
            this.groupBox4.Controls.Add(this.cbXtium);
            this.groupBox4.Location = new System.Drawing.Point(524, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(149, 245);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "2DCameraLink采集卡";
            this.cbHikCL.ActiveColor = System.Drawing.Color.DodgerBlue;
            this.cbHikCL.CheckColor = System.Drawing.Color.Blue;
            this.cbHikCL.Checked = false;
            this.cbHikCL.isAvtice = false;
            this.cbHikCL.Location = new System.Drawing.Point(17, 135);
            this.cbHikCL.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbHikCL.Name = "cbHikCL";
            this.cbHikCL.Size = new System.Drawing.Size(73, 26);
            this.cbHikCL.TabIndex = 5;
            this.cbHikCL.Value = "Hik采集卡";
            this.cbHikCL.CheckedChanged += new UserControlLibrary.MyCheckBox.CheckedHandle(checkbox_CheckedChanged);
            this.cbMatrox.ActiveColor = System.Drawing.Color.DodgerBlue;
            this.cbMatrox.CheckColor = System.Drawing.Color.Blue;
            this.cbMatrox.Checked = false;
            this.cbMatrox.isAvtice = false;
            this.cbMatrox.Location = new System.Drawing.Point(17, 109);
            this.cbMatrox.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbMatrox.Name = "cbMatrox";
            this.cbMatrox.Size = new System.Drawing.Size(92, 26);
            this.cbMatrox.TabIndex = 3;
            this.cbMatrox.Value = "Matrox采集卡";
            this.cbMatrox.CheckedChanged += new UserControlLibrary.MyCheckBox.CheckedHandle(checkbox_CheckedChanged);
            this.cbIkap.ActiveColor = System.Drawing.Color.DodgerBlue;
            this.cbIkap.CheckColor = System.Drawing.Color.Blue;
            this.cbIkap.Checked = false;
            this.cbIkap.isAvtice = false;
            this.cbIkap.Location = new System.Drawing.Point(17, 81);
            this.cbIkap.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbIkap.Name = "cbIkap";
            this.cbIkap.Size = new System.Drawing.Size(79, 26);
            this.cbIkap.TabIndex = 2;
            this.cbIkap.Value = "IKap采集卡";
            this.cbIkap.CheckedChanged += new UserControlLibrary.MyCheckBox.CheckedHandle(checkbox_CheckedChanged);
            this.cbAurora.ActiveColor = System.Drawing.Color.DodgerBlue;
            this.cbAurora.CheckColor = System.Drawing.Color.Blue;
            this.cbAurora.Checked = false;
            this.cbAurora.isAvtice = false;
            this.cbAurora.Location = new System.Drawing.Point(17, 52);
            this.cbAurora.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbAurora.Name = "cbAurora";
            this.cbAurora.Size = new System.Drawing.Size(92, 26);
            this.cbAurora.TabIndex = 1;
            this.cbAurora.Value = "Aurora采集卡";
            this.cbAurora.CheckedChanged += new UserControlLibrary.MyCheckBox.CheckedHandle(checkbox_CheckedChanged);
            this.cbXtium.ActiveColor = System.Drawing.Color.DodgerBlue;
            this.cbXtium.CheckColor = System.Drawing.Color.Blue;
            this.cbXtium.Checked = false;
            this.cbXtium.isAvtice = false;
            this.cbXtium.Location = new System.Drawing.Point(17, 23);
            this.cbXtium.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.cbXtium.Name = "cbXtium";
            this.cbXtium.Size = new System.Drawing.Size(104, 26);
            this.cbXtium.TabIndex = 0;
            this.cbXtium.Value = "Xtium-CL采集卡";
            this.cbXtium.CheckedChanged += new UserControlLibrary.MyCheckBox.CheckedHandle(checkbox_CheckedChanged);
            this.btnSave.Location = new System.Drawing.Point(590, 263);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 29);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "保存配置";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(btnSave_Click);
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(684, 300);
            base.Controls.Add(this.btnSave);
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "FrmHardwareDeployment";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "硬件部署配置界面";
            base.Load += new System.EventHandler(FrmHardwareDeployment_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        #endregion

        private MyCheckBox cbBasler2DGige;

        private GroupBox groupBox1;

        private MyCheckBox cbCognex2DGige;

        private MyCheckBox cbHikrobot2DGige;

        private MyCheckBox cbDaheng2DGige;

        private MyCheckBox cbDahua2DGige;

        private MyCheckBox cbBetterway2DGige;

        private GroupBox groupBox2;

        private MyCheckBox cbDaheng2DUsb;

        private MyCheckBox cbHikrobot2DUsb;

        private MyCheckBox cbBasler2DUsb;

        private GroupBox groupBox3;

        private MyCheckBox cbDahua2DLineGige;

        private MyCheckBox cbHikrobot2DLineGige;

        private MyCheckBox cbDalsa2DLineGige;

        private MyCheckBox cbBasler2DLineGige;

        private MyCheckBox cbCognex2DLineGige;

        private GroupBox groupBox4;

        private MyCheckBox cbMatrox;

        private MyCheckBox cbIkap;

        private MyCheckBox cbAurora;

        private MyCheckBox cbXtium;

        private Button btnSave;

        private MyCheckBox cbHikCL;
    }
}