using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Helper;
using NovaVision.Hardware;

namespace NovaVision.VisionForm.CarameFrm;

public partial class FrmCameraLinear2DSetting : Form
{
    private string cameraSn;

    private CameraLine2DBase camera;

    private TriggerMode2DLinear triggerSelector;

    private int rotaryDirection;

    private CameraConfigData mCameraConfigData;

    private BaseInfo baseInfo;

    private string XMLpath = "ParametersConfig.xml";

    private bool camSNState = false;

    private bool camState = false;

    private List<string> CanSNListXMLON = new List<string>();

    private List<string> TotalSNList = new List<string>();

    private CamEnumSingleton mCamEnumSingleton;

    private CameraConfigData cameraConfig;

    private bool isConnected = false;

    public bool isLive = false;

    private Task T_EnumCameras;


    public FrmCameraLinear2DSetting(BaseInfo mBaseInfo, string pathStr)
    {
        InitializeComponent();
        baseInfo = new BaseInfo();
        XMLpath = pathStr;
        baseInfo = mBaseInfo;
        isConnected = false;
        camSNState = false;
        SetControlState(state: true);
    }

    public FrmCameraLinear2DSetting(CameraConfigData camConfigData, BaseInfo mBaseInfo, string pathStr)
    {
        InitializeComponent();
        baseInfo = new BaseInfo();
        XMLpath = pathStr;
        mCameraConfigData = camConfigData;
        baseInfo = mBaseInfo;
        isConnected = false;
        if (mCameraConfigData.CamSN != null && mCameraConfigData.CamSN != "")
        {
            camSNState = true;
        }
        SetControlState(state: false);
    }

    private void CameraLinear2DSetting_Load(object sender, EventArgs e)
    {
        if (camSNState)
        {
            cbCameras.Items.Clear();
            cbCameras.Items.Add(mCameraConfigData.CamSN);
            cbCameras.SelectedItem = mCameraConfigData.CamSN;
        }
        else
        {
            T_EnumCameras = new Task(EnumCameras);
            T_EnumCameras.Start();
        }
        UpdateCCDList();
    }

    private void SetControlState(bool state)
    {
        if (state)
        {
            cbCameras.Enabled = true;
            btnAddCam.Enabled = true;
            btnAddCam.Visible = true;
            btnSaveParams.Visible = false;
            btnSaveParams.Enabled = false;
            button1.Enabled = true;
            button1.Visible = true;
            DelCameraSettings.Enabled = true;
        }
        else
        {
            cbCameras.Enabled = false;
            btnAddCam.Enabled = false;
            btnAddCam.Visible = false;
            btnSaveParams.Visible = true;
            btnSaveParams.Enabled = true;
            button1.Enabled = false;
            button1.Visible = false;
            DelCameraSettings.Enabled = false;
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        T_EnumCameras = new Task(EnumCameras);
        T_EnumCameras.Start();
        LogUtil.Log("2DLine参数设置：查询设备！");
    }

    private void EnumCameras()
    {
        Thread.CurrentThread.IsBackground = true;
        cameraSn = "";
        isConnected = false;
        mCamEnumSingleton = CamEnumSingleton.Instance();
        Invoke((EventHandler)delegate
        {
            StatusLabelInfo.ForeColor = Color.Black;
            StatusLabelInfo.Text = "相机查询中...";
            SetControlIni();
        });
        UpdateDeviceList();
        Invoke((EventHandler)delegate
        {
            imageDisplay1.DisplayName = "";
            groupBox1.Enabled = true;
        });
    }

    private void UpdateDeviceList()
    {
        CameraOperator.Enum2DLineCameras();
        camState = false;
        Invoke((EventHandler)delegate
        {
            cbCameras.Items.Clear();
            cbCameras.SelectedIndex = -1;
            TotalSNList.Clear();
            CanSNListXMLON.Clear();
            if (baseInfo.CCDList == null)
            {
                baseInfo.CCDList = new List<CameraConfigData>();
            }
            List<string> list = baseInfo.SnList_2Dlinear;
            if (list == null)
            {
                list = new List<string>();
            }
            foreach (KeyValuePair<string, string> current in CameraOperator.camera2DLineSNList)
            {
                cbCameras.Items.Add(current.Key);
                TotalSNList.Add(current.Key);
                if (list.Contains(current.Key))
                {
                    CanSNListXMLON.Add(current.Key);
                }
            }
            if (camSNState && TotalSNList.Contains(mCameraConfigData.CamSN))
            {
                camState = true;
            }
            if (cbCameras.Items.Count > 0)
            {
                StatusLabelInfo.ForeColor = Color.Green;
                StatusLabelInfo.Text = "相机设备已查询成功";
                if (camState)
                {
                    cbCameras.SelectedItem = mCameraConfigData.CamSN;
                }
                else
                {
                    cbCameras.SelectedIndex = 0;
                }
            }
            else
            {
                LogUtil.LogError("2D线扫相机配置： 未查找到2D线扫相机设备");
                StatusLabelInfo.ForeColor = Color.Red;
                StatusLabelInfo.Text = "未查找到2D线扫相机设备";
            }
        });
    }

    private void UpdateCCDList()
    {
        if (baseInfo.CCDList == null)
        {
            baseInfo.CCDList = new List<CameraConfigData>();
        }
        CCDList.Items.Clear();
        foreach (CameraConfigData item in baseInfo.CCDList)
        {
            if (item.CamCategory == CameraBase.CameraType["2D_LineScan"])
            {
                CCDList.Items.Add(item.CamSN + "(IP:" + item.CamIP + ")");
            }
        }
    }

    public void SetControlIni()
    {
        groupBox1.Enabled = false;
        groupBox2.Enabled = false;
        groupBox3.Enabled = false;
        groupBox4.Enabled = false;
        btnSnap.Enabled = false;
        btnGrab.Enabled = false;
        btnFreeze.Enabled = false;
    }

    public void SetOpenControlIni(bool state)
    {
        if (state)
        {
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;
            groupBox4.Enabled = true;
            btnSnap.Enabled = true;
            btnGrab.Enabled = true;
            btnFreeze.Enabled = false;
        }
        else
        {
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            btnSnap.Enabled = false;
            btnGrab.Enabled = false;
            btnFreeze.Enabled = false;
        }
    }

    private void cbCameras_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (isLive)
        {
            btnFreeze.PerformClick();
        }
        if (camera != null)
        {
            camera = null;
        }
        ComboBox lb = (ComboBox)sender;
        object item = lb.SelectedItem;
        if (cameraSn == item.ToString())
        {
            return;
        }
        if (isConnected && !camSNState)
        {
            DialogResult dr = MessageBox.Show(@"是否保存并切换当前相机！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (dr == DialogResult.OK)
            {
                btnAddCam_Click(null, null);
            }
        }
        if (item.ToString() != "")
        {
            cameraSn = item.ToString();
            SetOpenControlIni(state: false);
            btnAddCam.Enabled = true;
            btnConnect.Enabled = true;
            camera = CameraOperator.Find2DLineCamera(cameraSn);
            btnConnect.Text = "连接";
            if (camera != null && camera.camErrCode == CamErrCode.ConnectSuccess)
            {
                btnConnect_Click(null, null);
            }
        }
        LogUtil.Log("2DLine参数设置：切换相机" + cameraSn);
    }

    private void InitCamSetting(CameraLine2DBase Line2DCamera)
    {
        if (Line2DCamera == null)
        {
            return;
        }
        lblVendor.Text = Line2DCamera.VendorName;
        lblModel.Text = Line2DCamera.ModelName;
        lblIp.Text = Line2DCamera.DeviceIP;
        cbTriggerSelector.SelectedIndex = (int)Line2DCamera.triggerSelectorMode;
        cbRotaryDirection.SelectedIndex = Line2DCamera.RotaryDirection;
        try
        {
            nUDTimeout.Value = (decimal)Line2DCamera.timeout;
        }
        catch
        {
        }
        try
        {
            nUD_LinePeriod.Value = Line2DCamera.LinePeriod;
        }
        catch
        {
        }
        try
        {
            nUD_exposure.Value = (decimal)Line2DCamera.Exposure;
        }
        catch
        {
        }
        try
        {
            nUD_gain.Value = (decimal)Line2DCamera.Gain;
        }
        catch
        {
        }
        try
        {
            nUD_scanWidth.Value = Line2DCamera.ScanWidth;
        }
        catch
        {
        }
        try
        {
            nUD_scanHeight.Value = Line2DCamera.ScanHeight;
        }
        catch
        {
        }
        try
        {
            nUD_offsetX.Value = Line2DCamera.OffsetX;
        }
        catch
        {
        }
        try
        {
            nUD_timerDuration.Value = Line2DCamera.TimerDuration;
        }
        catch
        {
        }
        try
        {
            nUD_AcqLineRate.Value = Line2DCamera.AcqLineRate;
        }
        catch
        {
        }
        try
        {
            nUD_TapNum.Value = Line2DCamera.TapNum;
        }
        catch
        {
        }
    }

    private void btnConnect_Click(object sender, EventArgs e)
    {
        if (btnConnect.Text == "关闭")
        {
            btnConnect.Text = "连接";
            isConnected = false;
            if (camera != null)
            {
                camera.DestroyObjects();
                SetOpenControlIni(state: false);
                LogUtil.Log("2DLine参数设置：关闭相机" + cameraSn);
            }
            return;
        }
        LogUtil.Log("2DLine参数设置：连接相机" + cameraSn);
        try
        {
            CameraOperator.Open2DLineCamera(cameraSn);
            camera = CameraOperator.Find2DLineCamera(cameraSn);
            if (camera == null)
            {
                StatusLabelInfo.ForeColor = Color.Red;
                StatusLabelInfo.Text = "相机连接失败";
                SetOpenControlIni(state: false);
                return;
            }
            StatusLabelInfo.ForeColor = Color.Green;
            StatusLabelInfo.Text = "相机连接成功";
            if (CanSNListXMLON.Contains(camera.cameraSN) || camSNState)
            {
                cameraConfig = baseInfo.Query(camera.cameraSN);
                if (camSNState && mCameraConfigData.CamSN == camera.cameraSN)
                {
                    cameraConfig = mCameraConfigData;
                }
                if (cameraConfig.SettingParams != null)
                {
                    camera.Exposure = cameraConfig.SettingParams.ExposureTime;
                    camera.Gain = cameraConfig.SettingParams.Gain;
                    camera.ScanWidth = cameraConfig.SettingParams.ScanWidth;
                    camera.ScanHeight = cameraConfig.SettingParams.ScanHeight;
                    camera.OffsetX = cameraConfig.SettingParams.OffsetX;
                    camera.TimerDuration = cameraConfig.SettingParams.TimerDuration;
                    camera.AcqLineRate = cameraConfig.SettingParams.AcqLineRate;
                    camera.TapNum = cameraConfig.SettingParams.TapNum;
                    camera.LinePeriod = cameraConfig.SettingParams.LinePeriod;
                    camera.triggerSelectorMode = (TriggerMode2DLinear)cameraConfig.SettingParams.TriggerMode;
                    camera.RotaryDirection = cameraConfig.SettingParams.RotaryDirection;
                    camera.timeout = cameraConfig.SettingParams.Timeout;
                }
            }
            InitCamSetting(camera);
            SetOpenControlIni(state: true);
            if (camera.VendorName == "Hikrobot")
            {
                nUD_timerDuration.Enabled = false;
            }
            else
            {
                nUD_timerDuration.Enabled = true;
            }
            camera.UpdateImage = UpdateUIImage;
            UpdateDefaultTriggerSelector();
            btnConnect.Text = "关闭";
            isConnected = true;
        }
        catch (Exception ex)
        {
            StatusLabelInfo.ForeColor = Color.Red;
            StatusLabelInfo.Text = "相机连接失败";
            SetOpenControlIni(state: false);
            btnConnect.Text = "连接";
            MessageBox.Show(ex.Message);
        }
    }

    public void UpdateDefaultTriggerSelector()
    {
        switch (triggerSelector)
        {
            case TriggerMode2DLinear.Time_Line1:
                cbTriggerSelector.SelectedIndex = 0;
                nUD_AcqLineRate.Enabled = true;
                cbRotaryDirection.Enabled = false;
                break;
            case TriggerMode2DLinear.Time_Software:
                cbTriggerSelector.SelectedIndex = 1;
                nUD_AcqLineRate.Enabled = true;
                cbRotaryDirection.Enabled = false;
                break;
            case TriggerMode2DLinear.RotaryEncoder:
                cbTriggerSelector.SelectedIndex = 2;
                nUD_AcqLineRate.Enabled = false;
                cbRotaryDirection.Enabled = true;
                break;
            case TriggerMode2DLinear.Test_Software:
                cbTriggerSelector.SelectedIndex = 3;
                nUD_AcqLineRate.Enabled = false;
                cbRotaryDirection.Enabled = true;
                break;
            case TriggerMode2DLinear.RotaryEncoder_Hardware:
                cbTriggerSelector.SelectedIndex = 4;
                nUD_AcqLineRate.Enabled = false;
                cbRotaryDirection.Enabled = true;
                break;
        }
    }

    private void UpdateCamSettings(object sender, EventArgs e)
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
                    camera.Exposure = (long)nUD_exposure.Value;
                    break;
                case "nUD_gain":
                    camera.Gain = (float)nUD_gain.Value;
                    break;
                case "nUD_scanWidth":
                    camera.ScanWidth = (long)nUD_scanWidth.Value;
                    break;
                case "nUD_scanHeight":
                    camera.ScanHeight = (long)nUD_scanHeight.Value;
                    break;
                case "nUD_offsetX":
                    camera.OffsetX = (long)nUD_offsetX.Value;
                    break;
                case "nUD_timerDuration":
                    camera.TimerDuration = (long)nUD_timerDuration.Value;
                    break;
                case "nUD_AcqLineRate":
                    camera.AcqLineRate = (long)nUD_AcqLineRate.Value;
                    break;
                case "nUD_TapNum":
                    camera.TapNum = (long)nUD_TapNum.Value;
                    break;
                case "nUD_LinePeriod":
                    camera.LinePeriod = (long)nUD_LinePeriod.Value;
                    break;
            }
            LogUtil.Log("2DLine参数设置：设置相机( " + cameraSn + ")的参数" + lb.Name);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
    }

    private void nUDTimeout_ValueChanged(object sender, EventArgs e)
    {
        if (camera != null)
        {
            double s = Convert.ToDouble(nUDTimeout.Text);
            camera.SetTimeOut(s);
        }
    }

    private void cbTriggerSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (cbTriggerSelector.SelectedIndex)
        {
            case 0:
                triggerSelector = TriggerMode2DLinear.Time_Line1;
                cbRotaryDirection.SelectedIndex = 0;
                cbRotaryDirection.Enabled = false;
                break;
            case 1:
                triggerSelector = TriggerMode2DLinear.Time_Software;
                cbRotaryDirection.SelectedIndex = 0;
                cbRotaryDirection.Enabled = false;
                break;
            case 2:
                triggerSelector = TriggerMode2DLinear.RotaryEncoder;
                cbRotaryDirection.Enabled = true;
                break;
            case 3:
                triggerSelector = TriggerMode2DLinear.Test_Software;
                cbRotaryDirection.Enabled = true;
                break;
            case 4:
                triggerSelector = TriggerMode2DLinear.RotaryEncoder_Hardware;
                cbRotaryDirection.Enabled = true;
                break;
        }
        if (camera != null)
        {
            camera.SetTriggerSelector(triggerSelector);
            LogUtil.Log($"2DLine参数设置：设置相机( {cameraSn})的采集模式：{(int)triggerSelector}");
        }
    }

    private void cbRotaryDirection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (camera != null)
        {
            rotaryDirection = cbRotaryDirection.SelectedIndex;
            camera.SetRotaryDirection(triggerSelector, rotaryDirection);
            LogUtil.Log($"2DLine参数设置：设置相机( {cameraSn})的编码器方向：{rotaryDirection}");
        }
    }

    private void btnSnap_Click(object sender, EventArgs e)
    {
        if (camera != null)
        {
            camera.SoftwareTriggerOnce();
            LogUtil.Log("2DLine参数设置：软触发一次 ( " + cameraSn + ")");
        }
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
        }
    }

    private void btnGrab_Click(object sender, EventArgs e)
    {
        if (camera != null)
        {
            cbTriggerSelector.SelectedIndex = 3;
            camera.Start_Grab(state: true);
            isLive = true;
            btnSnap.Enabled = false;
            btnGrab.Enabled = false;
            btnFreeze.Enabled = true;
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            LogUtil.Log("2DLine参数设置：开启实时 ( " + cameraSn + ")");
        }
    }

    private void btnFreeze_Click(object sender, EventArgs e)
    {
        if (camera != null)
        {
            camera.Stop_Grab(state: true);
            isLive = false;
            btnSnap.Enabled = true;
            btnGrab.Enabled = true;
            btnFreeze.Enabled = false;
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;
            groupBox4.Enabled = true;
            LogUtil.Log("2DLine参数设置：停止采集 ( " + cameraSn + ")");
        }
    }

    private void FrmCameraLinear2DSetting_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (isLive)
        {
            btnFreeze.PerformClick();
        }
        if (camera != null)
        {
            CameraLine2DBase cameraLine2DBase = camera;
            cameraLine2DBase.UpdateImage = (Action<ImageData>)Delegate.Remove(cameraLine2DBase.UpdateImage, new Action<ImageData>(UpdateUIImage));
        }
    }

    private void btnAddCam_Click(object sender, EventArgs e)
    {
        try
        {
            if (camera == null)
            {
                return;
            }
            CameraConfigData cameraConfigData = new CameraConfigData();
            CamParams camParams = new CamParams();
            camParams.TriggerMode = cbTriggerSelector.SelectedIndex;
            camParams.ExposureTime = (int)camera.Exposure;
            camParams.Gain = (int)camera.Gain;
            camParams.ScanWidth = (int)camera.ScanWidth;
            camParams.ScanHeight = (int)camera.ScanHeight;
            camParams.OffsetX = (int)camera.OffsetX;
            camParams.TimerDuration = (int)camera.TimerDuration;
            camParams.AcqLineRate = (int)camera.AcqLineRate;
            camParams.TapNum = (int)camera.TapNum;
            camParams.LinePeriod = (int)camera.LinePeriod;
            camParams.RotaryDirection = rotaryDirection;
            camParams.Timeout = Convert.ToDouble(camera.timeout);
            cameraConfigData = new CameraConfigData();
            cameraConfigData.CamCategory = camera.CamCategory;
            cameraConfigData.CamSN = camera.cameraSN;
            cameraConfigData.CamVendor = camera.VendorName;
            cameraConfigData.CamIP = camera.DeviceIP;
            cameraConfigData.CameraModel = camera.ModelName;
            cameraConfigData.SettingParams = camParams;
            if (baseInfo.CCDList == null)
            {
                baseInfo.CCDList = new List<CameraConfigData>();
            }
            List<string> CanSNList = baseInfo.SnList_2Dlinear;
            if (CanSNList == null)
            {
                CanSNList = new List<string>();
            }
            if (CanSNList.Contains(cameraConfigData.CamSN))
            {
                if (!baseInfo.Modify(cameraConfigData))
                {
                    MessageBox.Show(@"参数更新失败");
                    return;
                }
            }
            else
            {
                DialogResult dr = MessageBox.Show(@"是否把当前相机保存至配置文件中！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (dr != DialogResult.OK)
                {
                    return;
                }
                if (!baseInfo.Add(cameraConfigData))
                {
                    MessageBox.Show(@"相机添加失败");
                    return;
                }
            }
            if (XmlHelper.WriteXML(XMLpath, baseInfo.CCDList))
            {
                MessageBox.Show(@"相机添加到XML文件成功");
                LogUtil.Log("2DLine参数设置：相机添加到XML文件成功 " + cameraSn);
            }
            else
            {
                MessageBox.Show(@"相机添加XML文件失败");
                LogUtil.LogError("2DLine参数设置：相机添加XML文件失败 " + cameraSn);
            }
        }
        catch (Exception)
        {
            MessageBox.Show(@"相机添加XML文件失败");
            LogUtil.LogError("2DLine参数设置：相机添加XML文件失败 " + cameraSn);
        }
        UpdateCCDList();
    }

    private void DelCameraSettings_Click(object sender, EventArgs e)
    {
        if (CCDList.SelectedItem == null || CCDList.SelectedItem.ToString() == "")
        {
            MessageBox.Show(@"请先选择需要删除的相机序列号");
            return;
        }
        string item = CCDList.SelectedItem.ToString();
        string[] SNAndIP = item.Split(':');
        string snStr = SNAndIP[0];
        string sn = snStr.Substring(0, snStr.Length - 3);
        DialogResult dr = MessageBox.Show("请确认是否将相机：" + sn + " 从配置文件中删除！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
        if (dr != DialogResult.OK)
        {
            return;
        }
        if (baseInfo.Delete(sn))
        {
            if (XmlHelper.WriteXML(XMLpath, baseInfo.CCDList))
            {
                MessageBox.Show(@"将相机移除XML成功");
                LogUtil.Log("2DLine参数设置：将相机移除XML成功 " + sn);
            }
            else
            {
                MessageBox.Show(@"将相机移除XML失败");
                LogUtil.LogError("2DLine参数设置：将相机移除XML失败 " + sn);
            }
        }
        else
        {
            MessageBox.Show(@"将相机移除XML失败");
            LogUtil.LogError("2DLine参数设置：将相机移除XML失败 " + sn);
        }
        UpdateCCDList();
    }

    private void btnSaveParams_Click(object sender, EventArgs e)
    {
        try
        {
            if (camera == null)
            {
                return;
            }
            DialogResult dr = MessageBox.Show(@"是否将修改的参数写入主配置！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (dr != DialogResult.OK)
            {
                return;
            }
            if (camSNState && mCameraConfigData.CamSN == camera.cameraSN)
            {
                mCameraConfigData.CamIP = camera.DeviceIP;
                mCameraConfigData.CamCategory = camera.CamCategory;
                mCameraConfigData.CamSN = camera.cameraSN;
                mCameraConfigData.CamVendor = camera.VendorName;
                mCameraConfigData.CameraModel = camera.ModelName;
                if (mCameraConfigData.SettingParams == null)
                {
                    mCameraConfigData.SettingParams = new CamParams();
                }
                mCameraConfigData.SettingParams.TriggerMode = cbTriggerSelector.SelectedIndex;
                mCameraConfigData.SettingParams.ExposureTime = (int)camera.Exposure;
                mCameraConfigData.SettingParams.Gain = (int)camera.Gain;
                mCameraConfigData.SettingParams.ScanWidth = (int)camera.ScanWidth;
                mCameraConfigData.SettingParams.ScanHeight = (int)camera.ScanHeight;
                mCameraConfigData.SettingParams.OffsetX = (int)camera.OffsetX;
                mCameraConfigData.SettingParams.TimerDuration = (int)camera.TimerDuration;
                mCameraConfigData.SettingParams.AcqLineRate = (int)camera.AcqLineRate;
                mCameraConfigData.SettingParams.TapNum = (int)camera.TapNum;
                mCameraConfigData.SettingParams.LinePeriod = (int)camera.LinePeriod;
                mCameraConfigData.SettingParams.RotaryDirection = rotaryDirection;
                mCameraConfigData.SettingParams.Timeout = Convert.ToDouble(camera.timeout);
            }
            LogUtil.Log("2DLine参数设置：保存相机参数成功 " + cameraSn);
        }
        catch (Exception)
        {
            MessageBox.Show(@"写入失败");
        }
    }
}