
using NovaVision.Hardware;
using System;
using System.Windows.Forms;

namespace NovaVision.VisionForm.CarameFrm
{
    public partial class FrmCameraDahuaCL : Form
    {
        private string Path;
        private CameraLine2DBase device;
        public FrmCameraDahuaCL(string path)
        {
            InitializeComponent();
            Path = path;
            
        }

        private void cbFoundDev_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = this.cbFoundDev.SelectedItem.ToString();
            device = CameraOperator.camera2DLineCollection[key];
            nUDExposure.Value = (decimal) device.Exposure;
            nUDGain.Value = (decimal)device.Gain;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                //string key = this.cbFoundDev.SelectedItem.ToString();
                //devices[key].ExposureTime = (double)nUDExposure.Value;
                //devices[key].ExposureTime = (double)nUDGain.Value;
                //XmlHelper.WriteXML(devices, Path, typeof(MyDictionaryEx<DahuaClParam>));
                //MessageBox.Show("保存成功");
            }
            catch (Exception exception)
            {
                MessageBox.Show("保存失败");
            }

        }
    }
}
