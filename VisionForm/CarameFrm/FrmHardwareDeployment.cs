using System;
using System.Windows.Forms;
using NovaVision.Hardware;
using NovaVision.UserControlLibrary;

namespace NovaVision.VisionForm.CarameFrm
{
    public partial class FrmHardwareDeployment : Form
    {
        public FrmHardwareDeployment()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (FrameGrabberOperator.SerializeHardwareDeploymentToXml(FrameGrabberOperator.DeploymentFilePath))
            {
                MessageBox.Show("硬件部署配置保存成功！");
                Close();
            }
            else
            {
                MessageBox.Show("硬件部署配置保存失败！");
            }
        }

        private void checkbox_CheckedChanged(object sender, EventArgs e)
        {
            MyCheckBox check = sender as MyCheckBox;
            switch (check.Name)
            {
                case "cbBasler2DGige":
                    FrameGrabberOperator.hardwareDeployments[0].state = (check.Checked ? 1 : 0);
                    break;
                case "cbCognex2DGige":
                    FrameGrabberOperator.hardwareDeployments[1].state = (check.Checked ? 2 : 0);
                    break;
                case "cbHikrobot2DGige":
                    FrameGrabberOperator.hardwareDeployments[2].state = (check.Checked ? 4 : 0);
                    break;
                case "cbDaheng2DGige":
                    FrameGrabberOperator.hardwareDeployments[3].state = (check.Checked ? 8 : 0);
                    break;
                case "cbDahua2DGige":
                    FrameGrabberOperator.hardwareDeployments[4].state = (check.Checked ? 16 : 0);
                    break;
                case "cbBetterway2DGige":
                    FrameGrabberOperator.hardwareDeployments[5].state = (check.Checked ? 32 : 0);
                    break;
                case "cbBasler2DUsb":
                    FrameGrabberOperator.hardwareDeployments[6].state = (check.Checked ? 64 : 0);
                    break;
                case "cbHikrobot2DUsb":
                    FrameGrabberOperator.hardwareDeployments[7].state = (check.Checked ? 128 : 0);
                    break;
                case "cbDaheng2DUsb":
                    FrameGrabberOperator.hardwareDeployments[8].state = (check.Checked ? 256 : 0);
                    break;
                case "cbCognex2DLineGige":
                    FrameGrabberOperator.hardwareDeployments[9].state = (check.Checked ? 512 : 0);
                    break;
                case "cbBasler2DLineGige":
                    FrameGrabberOperator.hardwareDeployments[10].state = (check.Checked ? 1024 : 0);
                    break;
                case "cbDalsa2DLineGige":
                    FrameGrabberOperator.hardwareDeployments[11].state = (check.Checked ? 2048 : 0);
                    break;
                case "cbHikrobot2DLineGige":
                    FrameGrabberOperator.hardwareDeployments[12].state = (check.Checked ? 4096 : 0);
                    break;
                case "cbDahua2DLineGige":
                    FrameGrabberOperator.hardwareDeployments[13].state = (check.Checked ? 8192 : 0);
                    break;
                case "cbXtium":
                    FrameGrabberOperator.hardwareDeployments[14].state = (check.Checked ? 16384 : 0);
                    break;
                case "cbAurora":
                    FrameGrabberOperator.hardwareDeployments[15].state = (check.Checked ? 32768 : 0);
                    break;
                case "cbIkap":
                    FrameGrabberOperator.hardwareDeployments[16].state = (check.Checked ? 65536 : 0);
                    break;
                case "cbMatrox":
                    FrameGrabberOperator.hardwareDeployments[17].state = (check.Checked ? 131072 : 0);
                    break;
                case "cbHikCL":
                    FrameGrabberOperator.hardwareDeployments[18].state = (check.Checked ? 262144 : 0);
                    break;
            }
        }

        private void FrmHardwareDeployment_Load(object sender, EventArgs e)
        {
            cbBasler2DGige.Checked = FrameGrabberOperator.hardwareDeployments[0].state == 1;
            cbCognex2DGige.Checked = FrameGrabberOperator.hardwareDeployments[1].state == 2;
            cbHikrobot2DGige.Checked = FrameGrabberOperator.hardwareDeployments[2].state == 4;
            cbDaheng2DGige.Checked = FrameGrabberOperator.hardwareDeployments[3].state == 8;
            cbDahua2DGige.Checked = FrameGrabberOperator.hardwareDeployments[4].state == 16;
            cbBetterway2DGige.Checked = FrameGrabberOperator.hardwareDeployments[5].state == 32;
            cbBasler2DUsb.Checked = FrameGrabberOperator.hardwareDeployments[6].state == 64;
            cbHikrobot2DUsb.Checked = FrameGrabberOperator.hardwareDeployments[7].state == 128;
            cbDaheng2DUsb.Checked = FrameGrabberOperator.hardwareDeployments[8].state == 256;
            cbCognex2DLineGige.Checked = FrameGrabberOperator.hardwareDeployments[9].state == 512;
            cbBasler2DLineGige.Checked = FrameGrabberOperator.hardwareDeployments[10].state == 1024;
            cbDalsa2DLineGige.Checked = FrameGrabberOperator.hardwareDeployments[11].state == 2048;
            cbHikrobot2DLineGige.Checked = FrameGrabberOperator.hardwareDeployments[12].state == 4096;
            cbDahua2DLineGige.Checked = FrameGrabberOperator.hardwareDeployments[13].state == 8192;
            cbXtium.Checked = FrameGrabberOperator.hardwareDeployments[14].state == 16384;
            cbAurora.Checked = FrameGrabberOperator.hardwareDeployments[15].state == 32768;
            cbIkap.Checked = FrameGrabberOperator.hardwareDeployments[16].state == 65536;
            cbMatrox.Checked = FrameGrabberOperator.hardwareDeployments[17].state == 131072;
            cbHikCL.Checked = FrameGrabberOperator.hardwareDeployments[18].state == 262144;
        }

    }
}
