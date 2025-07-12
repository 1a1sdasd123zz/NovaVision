using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Helper;
using NovaVision.Hardware;

namespace NovaVision.VisionForm.CarameFrm
{
    public partial class FrmCameraDeploySetting : Form
    {
        private Dictionary<string, CameraDeploy> CameraDeployData = CameraOperator.CameraDeployData;

        private Dictionary<string, bool> CameraVendorState = new Dictionary<string, bool>();

        public FrmCameraDeploySetting()
        {
            InitializeComponent();
        }

        private void FrmCameraDeploySetting_Load(object sender, EventArgs e)
        {
            ALLVendor2DAndLine.Items.Clear();
            CameraVendorState.Clear();
            foreach (KeyValuePair<string, string> item3 in CameraOperator.Camera2DVendor)
            {
                ALLVendor2DAndLine.Items.Add(item3.Key);
                CameraVendorState.Add(item3.Key, value: false);
            }
            foreach (KeyValuePair<string, string> item2 in CameraOperator.Camera2DListVendor)
            {
                ALLVendor2DAndLine.Items.Add(item2.Key);
                CameraVendorState.Add(item2.Key, value: false);
            }
            AddVendor2DAndLine.Items.Clear();
            foreach (KeyValuePair<string, CameraDeploy> item in CameraDeployData)
            {
                CameraDeploy cameraDeploy = item.Value;
                if (CameraVendorState.ContainsKey(item.Key) && cameraDeploy.state)
                {
                    AddVendor2DAndLine.Items.Add(item.Key);
                    CameraVendorState[item.Key] = true;
                }
            }
        }

        private void ALLVendor2DAndLine_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ALLVendor2DAndLine.SelectedItem != null)
            {
                string nValue = ALLVendor2DAndLine.SelectedItem.ToString();
                if (CameraVendorState.ContainsKey(nValue) && !CameraVendorState[nValue])
                {
                    AddVendor2DAndLine.Items.Add(nValue);
                    CameraVendorState[nValue] = true;
                }
            }
        }

        private void AddVendor2DAndLine_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (AddVendor2DAndLine.SelectedItem != null)
            {
                string nValue = AddVendor2DAndLine.SelectedItem.ToString();
                if (CameraVendorState.ContainsKey(nValue) && CameraVendorState[nValue])
                {
                    AddVendor2DAndLine.Items.Remove(nValue);
                    CameraVendorState[nValue] = false;
                }
            }
        }

        private void FrmCameraDeploySetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            List<CameraDeploy> cameraDeploys = new List<CameraDeploy>();
            CameraDeploy cameraDeploy = new CameraDeploy();
            foreach (KeyValuePair<string, bool> item in CameraVendorState)
            {
                cameraDeploy = new CameraDeploy();
                cameraDeploy.VendorName = item.Key;
                cameraDeploy.state = item.Value;
                cameraDeploys.Add(cameraDeploy);
            }
            if (XmlHelper.WriteXML(CameraOperator.CameraDeployPath, cameraDeploys))
            {
                LogUtil.Log("相机配置添加成功");
            }
            CameraOperator.GetCameraDeploy();
            CamEnumSingleton.Instance().ResetAllEventHandlers();
        }
    }
}
