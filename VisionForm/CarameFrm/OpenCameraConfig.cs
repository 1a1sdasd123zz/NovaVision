using System.Windows.Forms;
using NovaVision.Hardware;

namespace NovaVision.VisionForm.CarameFrm;

public class OpenCameraConfig
{
    public static void OpenDiffTypeCamera(CameraConfigData camConfigData, BaseInfo mBaseInfo, string path)
    {
        if (camConfigData == null)
        {
            return;
        }
        if (camConfigData.CamCategory == CameraBase.CameraType["2D"])
        {
            if (CameraOperator.camera2DCollection._2DCameras.ContainsKey(camConfigData.CamSN))
            {
                FrmCamera2DSetting frm_2DSetting = new FrmCamera2DSetting(camConfigData, mBaseInfo, path);
                frm_2DSetting.ShowDialog();
            }
            else
            {
                MessageBox.Show("网络中未查找到序列号为" + camConfigData.CamSN + "的2D相机或该相机故障！");
            }
        }
        else if (camConfigData.CamCategory == CameraBase.CameraType["2D_LineScan"])
        {
            if (CameraOperator.camera2DLineCollection._2DLineCameras.ContainsKey(camConfigData.CamSN))
            {
                FrmCameraLinear2DSetting frmCameraLinear2DSetting = new FrmCameraLinear2DSetting(camConfigData, mBaseInfo, path);
                frmCameraLinear2DSetting.ShowDialog();
            }
            else
            {
                MessageBox.Show("网络中未查找到序列号为" + camConfigData.CamSN + "的线扫相机或该相机故障！");
            }
        }
        else if (camConfigData.CamCategory == CameraBase.CameraType["3D"])
        {
            FrmCamera3DSetting frmCamera3DSetting = new FrmCamera3DSetting(camConfigData, mBaseInfo, path);
            frmCamera3DSetting.ShowDialog();
        }
    }

    public static void OpenDiffTypeFrameGrabber(FrameGrabberConfigData paramValues)
    {
        if (paramValues != null)
        {
            switch (paramValues.VendorNameKey.Split(',')[0])
            {
                //case "Xtium-CL-MX4":
                //    {
                //        FrmXtiumFrameGrabber frmXtiumFrameGrabber = new FrmXtiumFrameGrabber(paramValues, flag: true);
                //        frmXtiumFrameGrabber.ShowDialog();
                //        break;
                //    }
                //case "Matrox":
                //    {
                //        FrmMatroxFrameGrabber frmMatroxFrameGrabber = new FrmMatroxFrameGrabber(paramValues, flag: true);
                //        frmMatroxFrameGrabber.ShowDialog();
                //        break;
                //    }
                case "IKap":
                {
                    FrmVulcanFrameGrabber frmVulcanFrameGrabber = new FrmVulcanFrameGrabber(paramValues, flag: true);
                    frmVulcanFrameGrabber.ShowDialog();
                    break;
                }
                //case "Cognex2DLineGige":
                //    {
                //        FrmCameraCognexLinear2DSetting frmCameraCognexLinear2DSetting = new FrmCameraCognexLinear2DSetting(paramValues, flag: true);
                //        frmCameraCognexLinear2DSetting.ShowDialog();
                //        break;
                //    }
                case "Hikrobort2DLineGige":
                {
                    FrmCameraHikLinear2DSetting frmCameraHikLinear2DSetting = new FrmCameraHikLinear2DSetting(paramValues, flag: true);
                    frmCameraHikLinear2DSetting.ShowDialog();
                    break;
                }
                case "Dahua2DLineGige":
                {
                    FrmCameraDahuaLinear2DSetting frmCameraDahuaLinear2DSetting = new FrmCameraDahuaLinear2DSetting(paramValues, flag: true);
                    frmCameraDahuaLinear2DSetting.ShowDialog();
                    break;
                }
                case "HikCL":
                {
                    FrmHikFrameGrabber frmHikFrameGrabber = new FrmHikFrameGrabber(paramValues, flag: true);
                    frmHikFrameGrabber.ShowDialog();
                    break;
                }
            }
        }
    }
}