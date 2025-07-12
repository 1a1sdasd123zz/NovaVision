using System;
using System.Collections;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using NovaVision.BaseClass.Helper;
using NovaVision.Hardware;
using NovaVision.Hardware.C_2DGigeLineScan.iRAYPLE;

namespace NovaVision.VisionForm.CarameFrm;

public partial class FrmCameraDahuaLinear2DSetting : Form
{
    private Bv_DaHuaGigeLineScan camera;

    private string mSerialNum = string.Empty;

    private bool m_ConfigFileEnable;

    private bool m_configFileAvailable;

    private string m_currentConfigDir;

    private string m_ProductDir = "";

    private string m_ConfigFile = "";

    private string m_currentConfigFileName = "";

    private int m_currentConfigFileIndex;

    private ArrayList dcffiles = new ArrayList();

    private bool m_init = false;

    private bool internalIntoFlag = false;

    private FrameGrabberConfigData mParamValues;

    private bool bCameraExistAtDic = false;

    private bool bParamValuesExistAtDic = false;

    private bool isLive = false;

    private Camera2DLineWorkMode triggerSelector;


    public FrmCameraDahuaLinear2DSetting(FrameGrabberConfigData paramValues)
        : this(paramValues, flag: false)
    {
    }

    public FrmCameraDahuaLinear2DSetting(FrameGrabberConfigData paramValues, bool flag)
    {
        InitializeComponent();
        mParamValues = paramValues;
    }

    private void CameraLinear2DSetting_Load(object sender, EventArgs e)
    {
        CCDList.Items.Clear();
        cbCameras.Items.Clear();
        FrameGrabberOperator.EnumDevice(VendorEnum.Dahua2DLineGige);
        ComboBox.ObjectCollection items = cbCameras.Items;
        object[] items2 = Bv_DaHuaGigeLineScan.SerialNumList.ToArray();
        items.AddRange(items2);
        if (cbCameras.Items.Count > 0)
        {
            if (mParamValues == null)
            {
                internalIntoFlag = false;
                cbCameras.SelectedIndex = 0;
            }
            else
            {
                internalIntoFlag = true;
                SetControlByInternalInto();
                if (cbCameras.Items.Contains(mParamValues["Serial"].Value.ToString()))
                {
                    cbCameras.SelectedItem = mParamValues["Serial"].Value.ToString();
                    cbCameras_SelectedIndexChanged(null, null);
                }
                else
                {
                    MessageBox.Show(@"该配置的相机不在列表中！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    SetControlEnabled(this, state: false);
                }
            }
        }
        else
        {
            SetControlEnabled(this, state: false);
            btnEnumCameras.Enabled = true;
        }
        if (FrameGrabberOperator.dicVendorNameKey.ContainsKey("Dahua2DLineGige"))
        {
            ListBox.ObjectCollection items3 = CCDList.Items;
            items2 = FrameGrabberOperator.dicVendorNameKey["Dahua2DLineGige"].ToArray();
            items3.AddRange(items2);
        }
    }

    private void EnumCamerasFunction()
    {
        cbCameras.Items.Clear();
        mParamValues = null;
        FrameGrabberOperator.EnumDevice(VendorEnum.Dahua2DLineGige);
        ComboBox.ObjectCollection items = cbCameras.Items;
        object[] items2 = Bv_DaHuaGigeLineScan.SerialNumList.ToArray();
        items.AddRange(items2);
        if (cbCameras.Items.Count > 0)
        {
            cbCameras.SelectedIndex = 0;
            return;
        }
        SetControlEnabled(this, state: false);
        btnEnumCameras.Enabled = true;
    }

    private void SetControlEnabled(Control ctrl, bool state)
    {
        foreach (Control item in ctrl.Controls)
        {
            if (item.Controls.Count > 0)
            {
                SetControlEnabled(item, state);
            }
            else
            {
                item.Enabled = state;
            }
        }
    }

    private void SetControlByInternalInto()
    {
        btnEnumCameras.Enabled = false;
        cbCameras.Enabled = false;
        CCDList.Enabled = false;
        btnAddCam.Enabled = false;
        btnDelCameraSettings.Enabled = false;
        btnConnect.Enabled = false;
    }

    private void SetControlAfterCameraOpened(bool opened)
    {
        if (opened)
        {
            btnConnect.Text = "断开";
            SetControlEnabled(AcqParam_groupBox, state: true);
            cbTriggerSelector.Enabled = true;
            SetControlEnabled(panel_AcqFunction, state: true);
            lblVendor.Text = camera.VendorName;
            lblModel.Text = camera.Category.ToString();
            lblIp.Text = Convert.ToString(mParamValues["IP"].Value.mValue);
            StatusLabelInfo.Text = "相机连接";
            StatusLabelInfo.BackColor = Color.Green;
        }
        else
        {
            btnConnect.Text = "连接";
            SetControlEnabled(AcqParam_groupBox, state: false);
            cbTriggerSelector.Enabled = false;
            cbRotaryDirection.Enabled = false;
            SetControlEnabled(panel_AcqFunction, state: false);
            lblVendor.Text = "N/A";
            lblModel.Text = "N/A";
            lblIp.Text = "N/A";
            StatusLabelInfo.Text = "N/A";
            StatusLabelInfo.BackColor = Color.Red;
        }
    }

    private void cbCameras_SelectedIndexChanged(object sender, EventArgs e)
    {
        mSerialNum = cbCameras.Text;
        if (mSerialNum == string.Empty)
        {
            return;
        }
        if (!internalIntoFlag)
        {
            mParamValues = null;
        }
        if (mParamValues == null)
        {
            btnConnect.Enabled = true;
            if (FrameGrabberOperator.dicSerialConfig.ContainsKey(mSerialNum))
            {
                mParamValues = FrameGrabberOperator.dicSerialConfig[mSerialNum];
            }
        }
        if (FrameGrabberOperator.dicCameras.ContainsKey(mSerialNum))
        {
            camera = (Bv_DaHuaGigeLineScan)FrameGrabberOperator.dicCameras[mSerialNum];
            Bv_DaHuaGigeLineScan bv_DaHuaGigeLineScan = camera;
            bv_DaHuaGigeLineScan.UpdateImage = (Action<ImageData>)Delegate.Combine(bv_DaHuaGigeLineScan.UpdateImage, new Action<ImageData>(UpdateUIImage));
        }
        else
        {
            camera = new Bv_DaHuaGigeLineScan(mSerialNum, mParamValues);
            Bv_DaHuaGigeLineScan bv_DaHuaGigeLineScan2 = camera;
            bv_DaHuaGigeLineScan2.UpdateImage = (Action<ImageData>)Delegate.Combine(bv_DaHuaGigeLineScan2.UpdateImage, new Action<ImageData>(UpdateUIImage));
        }
        if (camera.ObjectCreated)
        {
            if (mParamValues == null)
            {
                mParamValues = camera.ConfigDatas;
            }
            else
            {
                camera.SetParams(mParamValues);
            }
            SetControlAfterCameraOpened(opened: true);
            Task.Run((Action)UpdateControlsValue);
        }
    }

    private void btnConnect_Click(object sender, EventArgs e)
    {
        if (btnConnect.Text.Equals("连接"))
        {
            if (camera.OpenDevice())
            {
                if (mParamValues == null)
                {
                    mParamValues = camera.ConfigDatas;
                }
                SetControlAfterCameraOpened(opened: true);
                Task.Run((Action)UpdateControlsValue);
            }
            else
            {
                SetControlAfterCameraOpened(opened: false);
            }
            if (!FrameGrabberOperator.dicCameras.ContainsKey(mSerialNum))
            {
                FrameGrabberOperator.dicCameras.Add(mSerialNum, camera);
            }
            return;
        }
        if (camera != null)
        {
            Bv_DaHuaGigeLineScan bv_DaHuaGigeLineScan = camera;
            bv_DaHuaGigeLineScan.UpdateImage = (Action<ImageData>)Delegate.Remove(bv_DaHuaGigeLineScan.UpdateImage, new Action<ImageData>(UpdateUIImage));
            camera.CloseDevice();
            if (FrameGrabberOperator.dicCameras.ContainsKey(mSerialNum))
            {
                FrameGrabberOperator.dicCameras.Remove(mSerialNum);
            }
        }
        SetControlAfterCameraOpened(opened: false);
    }

    private void UpdateControlsValue()
    {
        if (base.InvokeRequired)
        {
            Invoke((MethodInvoker)delegate
            {
                nUD_exposure.Value = Convert.ToDecimal(mParamValues["Exposure"].Value.mValue);
                nUD_gain.Value = Convert.ToDecimal(mParamValues["Gain"].Value.mValue);
                nUD_scanWidth.Value = Convert.ToDecimal(mParamValues["Width"].Value.mValue);
                nUD_scanHeight.Value = Convert.ToDecimal(mParamValues["Height"].Value.mValue);
                nUD_offsetX.Value = Convert.ToDecimal(mParamValues["OffsetX"].Value.mValue);
                nUD_lineDebouncerTime.Value = Convert.ToDecimal(mParamValues["LineDebouncerTime"].Value.mValue);
                nUD_AcqLineRate.Value = Convert.ToDecimal(mParamValues["AcquisitionLineRate"].Value.mValue);
                bool flag = Convert.ToBoolean(mParamValues["AcquisitionLineRateEnable"].Value.mValue);
                cmbAcqLineRateEnable.SelectedIndex = ((!flag) ? 1 : 0);
                nUDResultLineRate.Value = Convert.ToDecimal(mParamValues["ResultLineRate"].Value.mValue);
                nUD_Divider.Value = Convert.ToDecimal(mParamValues["Divider"].Value.mValue);
                nUD_Multiplier.Value = Convert.ToDecimal(mParamValues["Multiplier"].Value.mValue);
                nUD_Timeout.Value = Convert.ToDecimal(mParamValues["Timeout"].Value.mValue);
                int num = Convert.ToInt32(mParamValues["WorkMode"].Value.mValue);
                cbTriggerSelector.SelectedIndex = num - 1;
                cbRotaryDirection.SelectedIndex = Convert.ToInt32(mParamValues["RotaryEncoderMode"].Value.mValue);
                nUD_AcquisitionFrameCount.Value = Convert.ToDecimal(mParamValues["TriggerFrameCount"].Value.mValue);
                cmbReverseScanDirection.SelectedIndex = ((!(Convert.ToString(mParamValues["ReverseScanDirection"].Value.mValue) == "Off")) ? 1 : 0);
            });
        }
        else
        {
            nUD_exposure.Value = Convert.ToDecimal(mParamValues["Exposure"].Value.mValue);
            nUD_gain.Value = Convert.ToDecimal(mParamValues["Gain"].Value.mValue);
            nUD_scanWidth.Value = Convert.ToDecimal(mParamValues["Width"].Value.mValue);
            nUD_scanHeight.Value = Convert.ToDecimal(mParamValues["Height"].Value.mValue);
            nUD_offsetX.Value = Convert.ToDecimal(mParamValues["OffsetX"].Value.mValue);
            nUD_lineDebouncerTime.Value = Convert.ToDecimal(mParamValues["LineDebouncerTime"].Value.mValue);
            nUD_AcqLineRate.Value = Convert.ToDecimal(mParamValues["AcquisitionLineRate"].Value.mValue);
            bool acqLineRateEnable = Convert.ToBoolean(mParamValues["AcquisitionLineRateEnable"].Value.mValue);
            cmbAcqLineRateEnable.SelectedIndex = ((!acqLineRateEnable) ? 1 : 0);
            nUDResultLineRate.Value = Convert.ToDecimal(mParamValues["ResultLineRate"].Value.mValue);
            nUD_Divider.Value = Convert.ToDecimal(mParamValues["Divider"].Value.mValue);
            nUD_Multiplier.Value = Convert.ToDecimal(mParamValues["Multiplier"].Value.mValue);
            nUD_Timeout.Value = Convert.ToDecimal(mParamValues["Timeout"].Value.mValue);
            int workMode = Convert.ToInt32(mParamValues["WorkMode"].Value.mValue);
            cbTriggerSelector.SelectedIndex = workMode - 1;
            cbRotaryDirection.SelectedIndex = Convert.ToInt32(mParamValues["RotaryEncoderMode"].Value.mValue);
            nUD_AcquisitionFrameCount.Value = Convert.ToDecimal(mParamValues["TriggerFrameCount"].Value.mValue);
            cmbReverseScanDirection.SelectedIndex = ((!(Convert.ToString(mParamValues["ReverseScanDirection"].Value.mValue) == "Off")) ? 1 : 0);
        }
        m_init = true;
    }

    public void UpdateUIImage(ImageData imageData)
    {
        if (imageDisplay1.InvokeRequired)
        {
            imageDisplay1.BeginInvoke(new Action<ImageData>(UpdateUIImage), imageData);
        }
        else
        {
            imageDisplay1.CogImage = imageData.CogImage;
            label24.Text = camera.FrameNum.ToString();
        }
    }

    private void UpdateCamParamSettings(object sender, EventArgs e)
    {
        if (camera == null)
        {
            return;
        }
        try
        {
            NumericUpDown lb = (NumericUpDown)sender;
            switch (lb.Name)
            {
                case "nUD_exposure":
                {
                    double exposure = (double)nUD_exposure.Value;
                    camera.ConfigDatas["Exposure"] = new ParamElement
                    {
                        Name = "Exposure",
                        Type = "Double",
                        Value = new XmlObject
                        {
                            mValue = exposure
                        }
                    };
                    break;
                }
                case "nUD_gain":
                {
                    double gain = (double)nUD_gain.Value;
                    camera.ConfigDatas["Gain"] = new ParamElement
                    {
                        Name = "Gain",
                        Type = "Double",
                        Value = new XmlObject
                        {
                            mValue = gain
                        }
                    };
                    break;
                }
                case "nUD_scanWidth":
                {
                    int scanWidth = (int)nUD_scanWidth.Value;
                    camera.ConfigDatas["Width"] = new ParamElement
                    {
                        Name = "Width",
                        Type = "Int32",
                        Value = new XmlObject
                        {
                            mValue = scanWidth
                        }
                    };
                    break;
                }
                case "nUD_scanHeight":
                {
                    int scanHeight = (int)nUD_scanHeight.Value;
                    camera.ConfigDatas["Height"] = new ParamElement
                    {
                        Name = "Height",
                        Type = "Int32",
                        Value = new XmlObject
                        {
                            mValue = scanHeight
                        }
                    };
                    break;
                }
                case "nUD_offsetX":
                {
                    int offsetX = (int)nUD_offsetX.Value;
                    camera.ConfigDatas["OffsetX"] = new ParamElement
                    {
                        Name = "OffsetX",
                        Type = "Int32",
                        Value = new XmlObject
                        {
                            mValue = offsetX
                        }
                    };
                    break;
                }
                case "nUD_lineDebouncerTime":
                {
                    double lineDebouncerTime = (double)nUD_lineDebouncerTime.Value;
                    camera.ConfigDatas["LineDebouncerTime"] = new ParamElement
                    {
                        Name = "LineDebouncerTime",
                        Type = "Double",
                        Value = new XmlObject
                        {
                            mValue = lineDebouncerTime
                        }
                    };
                    break;
                }
                case "nUD_AcqLineRate":
                {
                    double acqLineRate = (double)nUD_AcqLineRate.Value;
                    camera.ConfigDatas["AcquisitionLineRate"] = new ParamElement
                    {
                        Name = "AcquisitionLineRate",
                        Type = "Double",
                        Value = new XmlObject
                        {
                            mValue = acqLineRate
                        }
                    };
                    break;
                }
                case "nUD_Divider":
                {
                    int divider = (int)nUD_Divider.Value;
                    camera.ConfigDatas["Divider"] = new ParamElement
                    {
                        Name = "Divider",
                        Type = "Int32",
                        Value = new XmlObject
                        {
                            mValue = divider
                        }
                    };
                    break;
                }
                case "nUD_Multiplier":
                {
                    int multiplier = (int)nUD_Multiplier.Value;
                    camera.ConfigDatas["Multiplier"] = new ParamElement
                    {
                        Name = "Multiplier",
                        Type = "Int32",
                        Value = new XmlObject
                        {
                            mValue = multiplier
                        }
                    };
                    break;
                }
                case "nUD_Timeout":
                {
                    int timeout = (int)nUD_Timeout.Value;
                    camera.ConfigDatas["Timeout"] = new ParamElement
                    {
                        Name = "Timeout",
                        Type = "Int32",
                        Value = new XmlObject
                        {
                            mValue = timeout
                        }
                    };
                    break;
                }
                case "nUD_AcquisitionFrameCount":
                {
                    int count = (int)nUD_AcquisitionFrameCount.Value;
                    camera.ConfigDatas["TriggerFrameCount"] = new ParamElement
                    {
                        Name = "TriggerFrameCount",
                        Type = "Int32",
                        Value = new XmlObject
                        {
                            mValue = count
                        }
                    };
                    break;
                }
            }
            mParamValues["ResultLineRate"].Value = camera.ConfigDatas["ResultLineRate"].Value;
            nUDResultLineRate.Value = Convert.ToDecimal(mParamValues["ResultLineRate"].Value.mValue);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
    }

    private void cbTriggerSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        nUD_AcquisitionFrameCount.Visible = false;
        switch (cbTriggerSelector.SelectedIndex)
        {
            case 0:
                triggerSelector = Camera2DLineWorkMode.FreeRun;
                cbRotaryDirection.Enabled = false;
                SetControlEnabled(btnSnap, state: false);
                SetControlEnabled(btnStartGrab, state: true);
                SetControlEnabled(btnStopGrab, state: true);
                SetControlEnabled(nUD_scanWidth, state: false);
                SetControlEnabled(nUD_scanHeight, state: false);
                SetControlEnabled(nUD_Divider, state: false);
                SetControlEnabled(nUD_Multiplier, state: false);
                SetControlEnabled(nUD_lineDebouncerTime, state: false);
                break;
            case 1:
                triggerSelector = Camera2DLineWorkMode.Time_Software;
                cbRotaryDirection.Enabled = false;
                SetControlEnabled(btnSnap, state: true);
                SetControlEnabled(btnStartGrab, state: true);
                SetControlEnabled(btnStopGrab, state: true);
                SetControlEnabled(nUD_offsetX, state: true);
                SetControlEnabled(nUD_scanWidth, state: true);
                SetControlEnabled(nUD_scanHeight, state: true);
                SetControlEnabled(nUD_Divider, state: false);
                SetControlEnabled(nUD_Multiplier, state: false);
                SetControlEnabled(nUD_lineDebouncerTime, state: false);
                break;
            case 2:
                triggerSelector = Camera2DLineWorkMode.Time_Hardware;
                cbRotaryDirection.Enabled = false;
                SetControlEnabled(btnSnap, state: true);
                SetControlEnabled(btnStartGrab, state: true);
                SetControlEnabled(btnStopGrab, state: true);
                SetControlEnabled(nUD_offsetX, state: false);
                SetControlEnabled(nUD_scanWidth, state: false);
                SetControlEnabled(nUD_scanHeight, state: false);
                SetControlEnabled(nUD_Divider, state: false);
                SetControlEnabled(nUD_Multiplier, state: false);
                SetControlEnabled(nUD_lineDebouncerTime, state: true);
                break;
            case 3:
                triggerSelector = Camera2DLineWorkMode.ShaftEncoder_Software;
                cbRotaryDirection.Enabled = true;
                SetControlEnabled(btnSnap, state: true);
                SetControlEnabled(btnStartGrab, state: true);
                SetControlEnabled(btnStopGrab, state: true);
                SetControlEnabled(nUD_offsetX, state: false);
                SetControlEnabled(nUD_scanWidth, state: false);
                SetControlEnabled(nUD_scanHeight, state: false);
                SetControlEnabled(nUD_Divider, state: true);
                SetControlEnabled(nUD_Multiplier, state: true);
                SetControlEnabled(nUD_lineDebouncerTime, state: false);
                break;
            case 4:
                triggerSelector = Camera2DLineWorkMode.ShaftEncoder_Hardware;
                cbRotaryDirection.Enabled = true;
                SetControlEnabled(btnSnap, state: true);
                SetControlEnabled(btnStartGrab, state: true);
                SetControlEnabled(btnStopGrab, state: true);
                SetControlEnabled(nUD_offsetX, state: false);
                SetControlEnabled(nUD_scanWidth, state: false);
                SetControlEnabled(nUD_scanHeight, state: false);
                SetControlEnabled(nUD_Divider, state: true);
                SetControlEnabled(nUD_Multiplier, state: true);
                SetControlEnabled(nUD_lineDebouncerTime, state: true);
                break;
            case 5:
                triggerSelector = Camera2DLineWorkMode.ShaftEncoder_Burst;
                cbRotaryDirection.Enabled = true;
                SetControlEnabled(btnSnap, state: true);
                SetControlEnabled(btnStartGrab, state: true);
                SetControlEnabled(btnStopGrab, state: true);
                SetControlEnabled(nUD_offsetX, state: false);
                SetControlEnabled(nUD_scanWidth, state: false);
                SetControlEnabled(nUD_scanHeight, state: false);
                SetControlEnabled(nUD_Divider, state: true);
                SetControlEnabled(nUD_Multiplier, state: true);
                SetControlEnabled(nUD_lineDebouncerTime, state: false);
                nUD_AcquisitionFrameCount.Visible = true;
                break;
        }
        int mode = (int)triggerSelector;
        camera.ConfigDatas["WorkMode"] = new ParamElement
        {
            Name = "WorkMode",
            Type = "Int32",
            Value = new XmlObject
            {
                mValue = mode
            }
        };
    }

    private void cbRotaryDirection_SelectedIndexChanged(object sender, EventArgs e)
    {
        int mode = cbRotaryDirection.SelectedIndex;
        camera.ConfigDatas["RotaryEncoderMode"] = new ParamElement
        {
            Name = "RotaryEncoderMode",
            Type = "Int32",
            Value = new XmlObject
            {
                mValue = mode
            }
        };
    }

    private void cmbAcqLineRateEnable_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool enable = cmbAcqLineRateEnable.SelectedIndex == 0;
        camera.ConfigDatas["AcquisitionLineRateEnable"] = new ParamElement
        {
            Name = "AcquisitionLineRateEnable",
            Type = "Boolean",
            Value = new XmlObject
            {
                mValue = enable
            }
        };
        mParamValues["ResultLineRate"].Value = camera.ConfigDatas["ResultLineRate"].Value;
        nUDResultLineRate.Value = Convert.ToDecimal(mParamValues["ResultLineRate"].Value.mValue);
    }

    private void cmbReverseScanDirection_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strValue = cmbReverseScanDirection.SelectedItem.ToString();
        if (string.IsNullOrEmpty(strValue))
        {
            strValue = "Off";
        }
        camera.ConfigDatas["ReverseScanDirection"] = new ParamElement
        {
            Name = "ReverseScanDirection",
            Type = "String",
            Value = new XmlObject
            {
                mValue = strValue
            }
        };
    }

    private void btnSnap_Click(object sender, EventArgs e)
    {
        camera.Snap();
    }

    private void btnGrab_Click(object sender, EventArgs e)
    {
        camera.StartGrab();
        isLive = true;
        btnStartGrab.Enabled = false;
        btnStopGrab.Enabled = true;
        nUD_offsetX.Enabled = false;
        nUD_scanWidth.Enabled = false;
        nUD_scanHeight.Enabled = false;
    }

    private void btnFreeze_Click(object sender, EventArgs e)
    {
        camera.StopGrab();
        isLive = false;
        btnStartGrab.Enabled = true;
        btnStopGrab.Enabled = false;
        if (triggerSelector == Camera2DLineWorkMode.Time_Software || triggerSelector == Camera2DLineWorkMode.ShaftEncoder_Software)
        {
            nUD_offsetX.Enabled = true;
            nUD_scanWidth.Enabled = true;
            nUD_scanHeight.Enabled = true;
        }
    }

    private void btnAddCam_Click(object sender, EventArgs e)
    {
        if (camera != null)
        {
            FrameGrabberOperator.AddDevice("Dahua2DLineGige", camera.ConfigDatas);
            FrameGrabberOperator.SerializeParamObjectToXml(FrameGrabberOperator.ConfigFilePath);
            if (!CCDList.Items.Contains(camera.ConfigDatas.VendorNameKey))
            {
                CCDList.Items.Add(camera.ConfigDatas.VendorNameKey);
            }
        }
    }

    private void DelCameraSettings_Click(object sender, EventArgs e)
    {
        if (CCDList.SelectedItem != null)
        {
            object selectObject = CCDList.SelectedItem;
            if (FrameGrabberOperator.RemoveDeviceByVendorSerial(selectObject.ToString()))
            {
                CCDList.Items.Remove(selectObject);
            }
        }
    }

    private void btnSaveParams_Click(object sender, EventArgs e)
    {
        base.DialogResult = DialogResult.OK;
        try
        {
            for (int i = 0; i < camera.ConfigDatas.CameraOrGrabberParams.Count; i++)
            {
                mParamValues[i] = camera.ConfigDatas[i];
            }
            MessageBox.Show(@"保存参数成功！");
        }
        catch (Exception ex)
        {
            MessageBox.Show("保存参数失败！异常信息：" + ex.Message);
        }
        Close();
    }

    private void CCDList_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        int index = CCDList.IndexFromPoint(e.Location);
        if (index == -1)
        {
            return;
        }
        try
        {
            string selectedItemName = CCDList.SelectedItem.ToString();
            string serialNum = selectedItemName.Split(',')[1];
            if (cbCameras.Items.Contains(serialNum))
            {
                cbCameras.SelectedItem = serialNum;
            }
            else
            {
                MessageBox.Show(@"在线相机列表中不存在选中的配置相机！");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("从CCD列表切换相机失败！异常信息：" + ex.Message);
        }
    }

    private void btnEnumCameras_Click(object sender, EventArgs e)
    {
        EnumCamerasFunction();
    }

    private void FrmCameraLinear2DSetting_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (isLive)
        {
            btnStopGrab.PerformClick();
        }
        if (camera != null)
        {
            Bv_DaHuaGigeLineScan bv_DaHuaGigeLineScan = camera;
            bv_DaHuaGigeLineScan.UpdateImage = (Action<ImageData>)Delegate.Remove(bv_DaHuaGigeLineScan.UpdateImage, new Action<ImageData>(UpdateUIImage));
        }
    }
}