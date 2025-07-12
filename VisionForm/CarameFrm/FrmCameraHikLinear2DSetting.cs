using System;
using System.Collections;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using NovaVision.BaseClass.Helper;
using NovaVision.Hardware;
using NovaVision.Hardware.C_2DGigeLineScan.Hikrobot;

namespace NovaVision.VisionForm.CarameFrm;

public partial class FrmCameraHikLinear2DSetting : Form
{
    private Bv_HikrobotGigeLineScan camera;

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



    public FrmCameraHikLinear2DSetting(FrameGrabberConfigData paramValues)
        : this(paramValues, flag: false)
    {
    }

    public FrmCameraHikLinear2DSetting(FrameGrabberConfigData paramValues, bool flag)
    {
        InitializeComponent();
        mParamValues = paramValues;
    }

    private void CameraLinear2DSetting_Load(object sender, EventArgs e)
    {
        CCDList.Items.Clear();
        cbCameras.Items.Clear();
        FrameGrabberOperator.EnumDevice(VendorEnum.Hikrobort2DLineGige);
        ComboBox.ObjectCollection items = cbCameras.Items;
        object[] items2 = Bv_HikrobotGigeLineScan.SerialNumList.ToArray();
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
        if (FrameGrabberOperator.dicVendorNameKey.ContainsKey("Hikrobort2DLineGige"))
        {
            ListBox.ObjectCollection items3 = CCDList.Items;
            items2 = FrameGrabberOperator.dicVendorNameKey["Hikrobort2DLineGige"].ToArray();
            items3.AddRange(items2);
        }
        cbGain.SelectedIndex = 0;
    }

    private void EnumCamerasFunction()
    {
        cbCameras.Items.Clear();
        mParamValues = null;
        FrameGrabberOperator.EnumDevice(VendorEnum.Hikrobort2DLineGige);
        ComboBox.ObjectCollection items = cbCameras.Items;
        object[] items2 = Bv_HikrobotGigeLineScan.SerialNumList.ToArray();
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
            lblIp.Text = mParamValues["IP"].Value.mValue.ToString();
            StatusLabelInfo.Text = "相机连接";
            StatusLabelInfo.BackColor = Color.Green;
        }
        else
        {
            btnConnect.Text = "连接";
            SetControlEnabled(AcqParam_groupBox, state: false);
            cbTriggerSelector.Enabled = false;
            cbRotaryDirection.Enabled = false;
            cbScanDirection.Enabled = false;
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
            camera = (Bv_HikrobotGigeLineScan)FrameGrabberOperator.dicCameras[mSerialNum];
            Bv_HikrobotGigeLineScan bv_HikrobotGigeLineScan = camera;
            bv_HikrobotGigeLineScan.UpdateImage = (Action<ImageData>)Delegate.Combine(bv_HikrobotGigeLineScan.UpdateImage, new Action<ImageData>(UpdateUIImage));
        }
        else
        {
            camera = new Bv_HikrobotGigeLineScan(mSerialNum, mParamValues);
            Bv_HikrobotGigeLineScan bv_HikrobotGigeLineScan2 = camera;
            bv_HikrobotGigeLineScan2.UpdateImage = (Action<ImageData>)Delegate.Combine(bv_HikrobotGigeLineScan2.UpdateImage, new Action<ImageData>(UpdateUIImage));
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
            Bv_HikrobotGigeLineScan bv_HikrobotGigeLineScan = camera;
            bv_HikrobotGigeLineScan.UpdateImage = (Action<ImageData>)Delegate.Remove(bv_HikrobotGigeLineScan.UpdateImage, new Action<ImageData>(UpdateUIImage));
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
                nUD_scanWidth.Value = Convert.ToDecimal(mParamValues["Width"].Value.mValue);
                nUD_scanHeight.Value = Convert.ToDecimal(mParamValues["Height"].Value.mValue);
                nUD_offsetX.Value = Convert.ToDecimal(mParamValues["OffsetX"].Value.mValue);
                nUD_lineDebouncerTime.Value = Convert.ToDecimal(mParamValues["LineDebouncerTime"].Value.mValue);
                nUD_AcqLineRate.Value = Convert.ToDecimal(mParamValues["AcquisitionLineRate"].Value.mValue);
                nUDResultLineRate.Value = Convert.ToDecimal(mParamValues["ResultLineRate"].Value.mValue);
                nUD_PreDivider.Value = Convert.ToDecimal(mParamValues["PreDivider"].Value.mValue);
                nUD_Multiplier.Value = Convert.ToDecimal(mParamValues["Multiplier"].Value.mValue);
                nUD_PostDivider.Value = Convert.ToDecimal(mParamValues["PostDivider"].Value.mValue);
                nUD_Timeout.Value = Convert.ToDecimal(mParamValues["Timeout"].Value.mValue);
                int num = Convert.ToInt32(mParamValues["WorkMode"].Value.mValue);
                cbTriggerSelector.SelectedIndex = num - 1;
                cbRotaryDirection.SelectedIndex = Convert.ToInt32(mParamValues["EncoderOutputMode"].Value.mValue);
                cbScanDirection.SelectedIndex = Convert.ToInt32(mParamValues["ScanDirection"].Value.mValue);
                nUD_AcquisitionFrameCount.Value = Convert.ToDecimal(mParamValues["FrameCount"].Value.mValue);
                checkBox1.Checked = Convert.ToInt32(mParamValues["StitchFlag"].Value.mValue) == 1;
                nUD_ImgCount.Value = Convert.ToInt32(mParamValues["ImgCount"].Value.mValue);
                switch (Convert.ToString(mParamValues["Gain"].Value.mValue))
                {
                    case "gain_1200x":
                        cbGain.SelectedIndex = 0;
                        break;
                    case "gain_2700x":
                        cbGain.SelectedIndex = 1;
                        break;
                    case "gain_4600xX":
                        cbGain.SelectedIndex = 2;
                        break;
                }
            });
        }
        else
        {
            nUD_exposure.Value = Convert.ToDecimal(mParamValues["Exposure"].Value.mValue);
            nUD_scanWidth.Value = Convert.ToDecimal(mParamValues["Width"].Value.mValue);
            nUD_scanHeight.Value = Convert.ToDecimal(mParamValues["Height"].Value.mValue);
            nUD_offsetX.Value = Convert.ToDecimal(mParamValues["OffsetX"].Value.mValue);
            nUD_lineDebouncerTime.Value = Convert.ToDecimal(mParamValues["LineDebouncerTime"].Value.mValue);
            nUD_AcqLineRate.Value = Convert.ToDecimal(mParamValues["AcquisitionLineRate"].Value.mValue);
            nUDResultLineRate.Value = Convert.ToDecimal(mParamValues["ResultLineRate"].Value.mValue);
            nUD_PreDivider.Value = Convert.ToDecimal(mParamValues["PreDivider"].Value.mValue);
            nUD_Multiplier.Value = Convert.ToDecimal(mParamValues["Multiplier"].Value.mValue);
            nUD_PostDivider.Value = Convert.ToDecimal(mParamValues["PostDivider"].Value.mValue);
            nUD_Timeout.Value = Convert.ToDecimal(mParamValues["Timeout"].Value.mValue);
            int workMode = Convert.ToInt32(mParamValues["WorkMode"].Value.mValue);
            cbTriggerSelector.SelectedIndex = workMode - 1;
            cbRotaryDirection.SelectedIndex = Convert.ToInt32(mParamValues["EncoderOutputMode"].Value.mValue);
            cbScanDirection.SelectedIndex = Convert.ToInt32(mParamValues["ScanDirection"].Value.mValue);
            nUD_AcquisitionFrameCount.Value = Convert.ToDecimal(mParamValues["FrameCount"].Value.mValue);
            checkBox1.Checked = Convert.ToInt32(mParamValues["StitchFlag"].Value.mValue) == 1;
            nUD_ImgCount.Value = Convert.ToInt32(mParamValues["ImgCount"].Value.mValue);
            switch (Convert.ToString(mParamValues["Gain"].Value.mValue))
            {
                case "gain_1200x":
                    cbGain.SelectedIndex = 0;
                    break;
                case "gain_2700x":
                    cbGain.SelectedIndex = 1;
                    break;
                case "gain_4600xX":
                    cbGain.SelectedIndex = 2;
                    break;
            }
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
        camera.StopGrab();
        btnStartGrab.Enabled = true;
        btnStopGrab.Enabled = false;
        if (triggerSelector == Camera2DLineWorkMode.Time_Software || triggerSelector == Camera2DLineWorkMode.ShaftEncoder_Software)
        {
            nUD_offsetX.Enabled = true;
            nUD_scanWidth.Enabled = true;
            nUD_scanHeight.Enabled = true;
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
                    int lineDebouncerTime = (int)nUD_lineDebouncerTime.Value;
                    camera.ConfigDatas["LineDebouncerTime"] = new ParamElement
                    {
                        Name = "LineDebouncerTime",
                        Type = "Int32",
                        Value = new XmlObject
                        {
                            mValue = lineDebouncerTime
                        }
                    };
                    break;
                }
                case "nUD_AcqLineRate":
                {
                    int acqLineRate = (int)nUD_AcqLineRate.Value;
                    camera.ConfigDatas["AcquisitionLineRate"] = new ParamElement
                    {
                        Name = "AcquisitionLineRate",
                        Type = "Int32",
                        Value = new XmlObject
                        {
                            mValue = acqLineRate
                        }
                    };
                    break;
                }
                case "nUD_PreDivider":
                {
                    int preDivider = (int)nUD_PreDivider.Value;
                    camera.ConfigDatas["PreDivider"] = new ParamElement
                    {
                        Name = "PreDivider",
                        Type = "Int32",
                        Value = new XmlObject
                        {
                            mValue = preDivider
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
                case "nUD_PostDivider":
                {
                    int postDivider = (int)nUD_PostDivider.Value;
                    camera.ConfigDatas["PostDivider"] = new ParamElement
                    {
                        Name = "PostDivider",
                        Type = "Int32",
                        Value = new XmlObject
                        {
                            mValue = postDivider
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
                    camera.ConfigDatas["FrameCount"] = new ParamElement
                    {
                        Name = "FrameCount",
                        Type = "Int32",
                        Value = new XmlObject
                        {
                            mValue = count
                        }
                    };
                    break;
                }
                case "nUD_ImgCount":
                {
                    int ImgCount = (int)nUD_ImgCount.Value;
                    camera.ConfigDatas["ImgCount"] = new ParamElement
                    {
                        Name = "ImgCount",
                        Type = "Int32",
                        Value = new XmlObject
                        {
                            mValue = ImgCount
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

    private void checkBox1_Enter(object sender, EventArgs e)
    {
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        int count = 0;
        if (checkBox1.Checked)
        {
            count = 1;
        }
        camera.ConfigDatas["StitchFlag"] = new ParamElement
        {
            Name = "StitchFlag",
            Type = "Int32",
            Value = new XmlObject
            {
                mValue = count
            }
        };
    }

    private void cbTriggerSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (cbTriggerSelector.SelectedIndex)
        {
            case 0:
                triggerSelector = Camera2DLineWorkMode.FreeRun;
                cbRotaryDirection.Enabled = false;
                cbScanDirection.Enabled = false;
                SetControlEnabled(btnSnap, state: false);
                SetControlEnabled(btnStartGrab, state: true);
                SetControlEnabled(btnStopGrab, state: true);
                SetControlEnabled(nUD_scanWidth, state: false);
                SetControlEnabled(nUD_scanHeight, state: false);
                SetControlEnabled(nUD_PreDivider, state: false);
                SetControlEnabled(nUD_Multiplier, state: false);
                SetControlEnabled(nUD_PostDivider, state: false);
                SetControlEnabled(nUD_lineDebouncerTime, state: false);
                break;
            case 1:
                triggerSelector = Camera2DLineWorkMode.Time_Software;
                cbRotaryDirection.Enabled = false;
                cbScanDirection.Enabled = false;
                SetControlEnabled(btnSnap, state: true);
                SetControlEnabled(btnStartGrab, state: true);
                SetControlEnabled(btnStopGrab, state: true);
                SetControlEnabled(nUD_offsetX, state: true);
                SetControlEnabled(nUD_scanWidth, state: true);
                SetControlEnabled(nUD_scanHeight, state: true);
                SetControlEnabled(nUD_PreDivider, state: false);
                SetControlEnabled(nUD_Multiplier, state: false);
                SetControlEnabled(nUD_PostDivider, state: false);
                SetControlEnabled(nUD_lineDebouncerTime, state: false);
                break;
            case 2:
                triggerSelector = Camera2DLineWorkMode.Time_Hardware;
                cbRotaryDirection.Enabled = false;
                cbScanDirection.Enabled = false;
                SetControlEnabled(btnSnap, state: true);
                SetControlEnabled(btnStartGrab, state: true);
                SetControlEnabled(btnStopGrab, state: true);
                SetControlEnabled(nUD_offsetX, state: false);
                SetControlEnabled(nUD_scanWidth, state: false);
                SetControlEnabled(nUD_scanHeight, state: false);
                SetControlEnabled(nUD_PreDivider, state: false);
                SetControlEnabled(nUD_Multiplier, state: false);
                SetControlEnabled(nUD_PostDivider, state: false);
                SetControlEnabled(nUD_lineDebouncerTime, state: true);
                break;
            case 3:
                triggerSelector = Camera2DLineWorkMode.ShaftEncoder_Software;
                cbRotaryDirection.Enabled = true;
                cbScanDirection.Enabled = true;
                SetControlEnabled(btnSnap, state: true);
                SetControlEnabled(btnStartGrab, state: true);
                SetControlEnabled(btnStopGrab, state: true);
                SetControlEnabled(nUD_offsetX, state: false);
                SetControlEnabled(nUD_scanWidth, state: false);
                SetControlEnabled(nUD_scanHeight, state: false);
                SetControlEnabled(nUD_PreDivider, state: true);
                SetControlEnabled(nUD_Multiplier, state: true);
                SetControlEnabled(nUD_PostDivider, state: true);
                SetControlEnabled(nUD_lineDebouncerTime, state: false);
                break;
            case 4:
                triggerSelector = Camera2DLineWorkMode.ShaftEncoder_Hardware;
                cbRotaryDirection.Enabled = true;
                cbScanDirection.Enabled = true;
                SetControlEnabled(btnSnap, state: true);
                SetControlEnabled(btnStartGrab, state: true);
                SetControlEnabled(btnStopGrab, state: true);
                SetControlEnabled(nUD_offsetX, state: false);
                SetControlEnabled(nUD_scanWidth, state: false);
                SetControlEnabled(nUD_scanHeight, state: false);
                SetControlEnabled(nUD_PreDivider, state: true);
                SetControlEnabled(nUD_Multiplier, state: true);
                SetControlEnabled(nUD_PostDivider, state: true);
                SetControlEnabled(nUD_lineDebouncerTime, state: true);
                break;
            case 5:
                triggerSelector = Camera2DLineWorkMode.ShaftEncoder_Burst;
                cbRotaryDirection.Enabled = true;
                cbScanDirection.Enabled = true;
                SetControlEnabled(btnSnap, state: true);
                SetControlEnabled(btnStartGrab, state: true);
                SetControlEnabled(btnStopGrab, state: true);
                SetControlEnabled(nUD_offsetX, state: false);
                SetControlEnabled(nUD_scanWidth, state: false);
                SetControlEnabled(nUD_scanHeight, state: false);
                SetControlEnabled(nUD_PreDivider, state: true);
                SetControlEnabled(nUD_Multiplier, state: true);
                SetControlEnabled(nUD_PostDivider, state: true);
                SetControlEnabled(nUD_lineDebouncerTime, state: false);
                break;
        }
        camera.ConfigDatas["WorkMode"] = new ParamElement
        {
            Name = "WorkMode",
            Type = "Int32",
            Value = new XmlObject
            {
                mValue = triggerSelector
            }
        };
    }

    private void cbRotaryDirection_SelectedIndexChanged(object sender, EventArgs e)
    {
        int mode = cbRotaryDirection.SelectedIndex;
        camera.ConfigDatas["EncoderOutputMode"] = new ParamElement
        {
            Name = "EncoderOutputMode",
            Type = "Int32",
            Value = new XmlObject
            {
                mValue = mode
            }
        };
    }

    private void cbGain_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strGain = "gain_1200x";
        switch (cbGain.SelectedIndex)
        {
            case 0:
                strGain = "gain_1200x";
                break;
            case 1:
                strGain = "gain_2700x";
                break;
            case 2:
                strGain = "gain_4600x";
                break;
        }
        if (camera != null)
        {
            camera.ConfigDatas["Gain"] = new ParamElement
            {
                Name = "Gain",
                Type = "String",
                Value = new XmlObject
                {
                    mValue = strGain
                }
            };
        }
    }

    private void btnSnap_Click(object sender, EventArgs e)
    {
        camera.Snap();
    }

    private void btnGrab_Click(object sender, EventArgs e)
    {
        camera.StartGrab();
        btnStartGrab.Enabled = false;
        btnStopGrab.Enabled = true;
        nUD_offsetX.Enabled = false;
        nUD_scanWidth.Enabled = false;
        nUD_scanHeight.Enabled = false;
    }

    private void btnFreeze_Click(object sender, EventArgs e)
    {
        camera.StopGrab();
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
            FrameGrabberOperator.AddDevice("Hikrobort2DLineGige", camera.ConfigDatas);
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
            Bv_HikrobotGigeLineScan bv_HikrobotGigeLineScan = camera;
            bv_HikrobotGigeLineScan.UpdateImage = (Action<ImageData>)Delegate.Remove(bv_HikrobotGigeLineScan.UpdateImage, new Action<ImageData>(UpdateUIImage));
        }
    }

    private void cbScanDirection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (m_init && camera.ConfigDatas != null)
        {
            camera.ConfigDatas["ScanDirection"] = new ParamElement
            {
                Name = "ScanDirection",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = cbScanDirection.SelectedIndex
                }
            };
        }
    }
}