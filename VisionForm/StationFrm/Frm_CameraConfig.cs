using System;
using System.Windows.Forms;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Collection;
using NovaVision.Hardware;

namespace NovaVision.VisionForm.StationFrm
{

    public partial class FrmCameraConfig : Form
    {
        private readonly MyDictionaryEx<CameraConfigData> _mCameraConfigData;

        private readonly BaseInfo _mCamBaseInfo;

        private readonly MyDictionaryEx<FrameGrabberConfigData> _mCameraDataCl;

        public FrmCameraConfig(MyDictionaryEx<CameraConfigData> cameraData, BaseInfo baseInfo, MyDictionaryEx<FrameGrabberConfigData> cameraDataCl)
        {
            this.InitializeComponent();
            this._mCameraConfigData = cameraData;
            this._mCamBaseInfo = baseInfo;
            this._mCameraDataCl = cameraDataCl;
            this.InitDgv(this._mCameraConfigData);
            this.InitDgv(this._mCameraDataCl);
            this.AddSn();
            this.AddSn2();
        }

        private void InitDgv(MyDictionaryEx<CameraConfigData> cameraData)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.RowHeadersVisible = false;
            dgv.Columns.Add("name", "配置名");
            dgv.Columns[0].ReadOnly = true;
            dgv.Columns[0].ValueType = typeof(string);
            dgv.Columns[0].Width = 80;
            dgv.Columns.Add("SN", "序列号");
            dgv.Columns[1].ReadOnly = true;
            dgv.Columns[1].ValueType = typeof(string);
            dgv.Columns[1].Width = 100;
            dgv.Columns.Add("cameraType", "相机类型");
            dgv.Columns[2].ReadOnly = true;
            dgv.Columns[2].ValueType = typeof(string);
            dgv.Columns[2].Width = 60;
            dgv.Columns.Add("ip", "IP");
            dgv.Columns[3].ReadOnly = true;
            dgv.Columns[3].ValueType = typeof(string);
            dgv.Columns[3].Width = 120;
            dgv.Columns.Add("expourse", "曝光");
            dgv.Columns[4].ReadOnly = true;
            dgv.Columns[4].ValueType = typeof(string);
            dgv.Columns[4].Width = 60;
            dgv.Rows.Clear();
            var keys = cameraData.GetKeys();
            for (var i = 0; i < cameraData.Count; i++)
            {
                WriteOnePieceData(cameraData[i], keys[i]);
            }
        }

        private void InitDgv(MyDictionaryEx<FrameGrabberConfigData> cameraData)
        {
            var keys = cameraData.GetKeys();
            for (var i = 0; i < cameraData.Count; i++)
            {
                WriteOnePieceData(cameraData[i], keys[i]);
            }
        }

        private void WriteOnePieceData(CameraConfigData data, string key)
        {
            if (data.SettingParams != null)
            {
                dgv.Rows.Add(key, data.CamSN, data.CamCategory, data.CamIP, data.SettingParams.ExposureTime, "配置");
            }
            else
            {
                dgv.Rows.Add(key, data.CamSN, data.CamCategory, data.CamIP);
            }
        }

        private void WriteOnePieceData(FrameGrabberConfigData data, string key)
        {
            dgv.Rows.Add(key, data["Serial"].Value, data["Category"].Value);
        }

        private void AddSn()
        {
            cmb_SN.Items.Clear();
            if (_mCamBaseInfo.SnList == null) return;
            var items = cmb_SN.Items;
            object[] items2 = _mCamBaseInfo.SnList.ToArray();
            items.AddRange(items2);
        }

        private void AddSn2()
        {
            if (FrameGrabberOperator.dicCamerasConfig == null)
            {
                return;
            }
            foreach (var item3 in FrameGrabberOperator.dicCamerasConfig)
            {
                foreach (var item2 in item3.Value)
                {
                    cmb_SN.Items.Add(item2.VendorNameKey.Split(',')[1]);
                }
            }
        }

        private void tsBtn_NewLine_Click(object sender, EventArgs e)
        {
            var key = txt_Name.Text.Trim();
            var index = cmb_SN.SelectedIndex;
            if (key != "" && cmb_SN.SelectedIndex >= 0)
            {
                var camtype = "";
                if (_mCamBaseInfo.CCDList != null && index < _mCamBaseInfo.CCDList.Count)
                {
                    camtype = _mCamBaseInfo.CCDList[index].CamCategory;
                }
                if (FrameGrabberOperator.dicSerialConfig != null)
                {
                    var serial2 = cmb_SN.SelectedItem.ToString();
                    if (FrameGrabberOperator.dicSerialConfig.ContainsKey(serial2))
                    {
                        camtype = FrameGrabberOperator.dicSerialConfig[serial2]["Category"].Value.ToString();
                    }
                }
                if (!_mCameraConfigData.ContainsKey(key) && (camtype == "2D" || camtype == "2D_LineScan" || camtype == "3D"))
                {
                    if (_mCamBaseInfo.CCDList != null && index < _mCamBaseInfo.CCDList.Count)
                    {
                        var data2 = _mCamBaseInfo.CCDList[index].Clone();
                        _mCameraConfigData.Add(key, data2);
                        WriteOnePieceData(data2, key);
                    }
                }
                else if (!_mCameraDataCl.ContainsKey(key) && (camtype == "C_2DLineCL" || camtype == "C_2DLineGige"))
                {
                    var serial = cmb_SN.SelectedItem.ToString();
                    if (FrameGrabberOperator.dicSerialConfig != null && FrameGrabberOperator.dicSerialConfig.ContainsKey(serial))
                    {
                        var data = DeepCopy.DeepCopyByXml(FrameGrabberOperator.dicSerialConfig[serial]);
                        _mCameraDataCl.Add(key, data);
                        WriteOnePieceData(data, key);
                    }
                }
                else
                {
                    MessageBox.Show(@"与已有配置重名！");
                }
            }
            else
            {
                MessageBox.Show(@"配置名为空，或SN号未选择！");
            }
        }

        private void tsBtn_DeleteLine_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                return;
            }
            var index = dgv.CurrentCell.RowIndex;
            var key = dgv[0, index].Value.ToString();
            if (key != "")
            {
                if (_mCameraConfigData.ContainsKey(key))
                {
                    _mCameraConfigData.Remove(key);
                    dgv.Rows.RemoveAt(index);
                }
                if (_mCameraDataCl.ContainsKey(key))
                {
                    _mCameraDataCl.Remove(key);
                    dgv.Rows.RemoveAt(index);
                }
            }
        }

        private void cmb_SN_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = cmb_SN.SelectedIndex;
            var serial = cmb_SN.SelectedItem.ToString();
            if (_mCamBaseInfo.CCDList != null && index >= 0 && index < _mCamBaseInfo.CCDList.Count)
            {
                lbl_CamType.Text = @"相机类型：" + _mCamBaseInfo.CCDList[index].CamCategory;
            }

            if (FrameGrabberOperator.dicSerialConfig == null ||
                !FrameGrabberOperator.dicSerialConfig.ContainsKey(serial)) return;
            var camtype = FrameGrabberOperator.dicSerialConfig[serial]["Category"].Value.ToString();
            lbl_CamType.Text = @"相机类型：" + camtype;
        }

        private void Frm_CameraConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
