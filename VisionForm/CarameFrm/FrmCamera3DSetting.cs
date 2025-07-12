using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using KeyenceLib;
using Kitware.VTK;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Helper;
using NovaVision.Hardware;
using NovaVision.UserControlLibrary;

namespace NovaVision.VisionForm.CarameFrm;

partial class FrmCamera3DSetting : Form
{
    private LJX8IF_ETHERNET_CONFIG ethernetConfig = default(LJX8IF_ETHERNET_CONFIG);

    private ushort highSpeedPortNo;

    private byte bySendPosition;

    private uint _profileCount;

    private CameraConfigData mCameraConfigData;

    private BaseInfo baseInfo = new BaseInfo();

    private string XMLpath = "ParametersConfig.xml";

    private string cameraType = "";

    private string cameraIP = "";

    private bool camSNState = false;

    private string nowCameraType = "";

    private CameraConfigData cameraConfig = new CameraConfigData();

    private string snip = "(IP:";

    public static readonly List<string> acqLR_LJX = new List<string>
    {
        "10Hz", "20Hz", "50Hz", "100Hz", "200Hz", "500Hz", "1KHz", "2KHz", "4KHz", "8KHz",
        "16KHz"
    };

    public static readonly List<string> acqLR_LJV = new List<string>
    {
        "10Hz", "20Hz", "50Hz", "100Hz", "200Hz", "500Hz", "1KHz", "2KHz", "4KHz", "4.13KHz",
        "8KHz", "16KHz", "32KHz", "64KHz"
    };

    public static readonly List<string> acqLR_LJVB = new List<string>
    {
        "10Hz", "20Hz", "50Hz", "100Hz", "200Hz", "500Hz", "1KHz", "2KHz", "4KHz", "4.13KHz",
        "8KHz"
    };

    public static readonly List<string> expo_LJV = new List<string> { "15us", "30us", "60us", "120us", "240us", "480us", "960us", "1920us", "5ms", "10ms" };

    public static readonly List<string> expo_LJX = new List<string> { "15us", "30us", "60us", "120us", "240us", "480us", "960us", "1700us", "4.8ms", "9.6ms" };

    public static readonly List<string> expo_SSZN = new List<string>
    {
        "10us", "15us", "30us", "60us", "120us", "240us", "480us", "960us", "1920us", "2400us",
        "4900us", "9800us"
    };

    private Camera3DBase camera;

    private TriggerMode3D acqTriggerMode;

    private int acqLineRateIndex;

    private double acqLineRateValue;

    private double scanLength;

    private int scanLines;

    private double expoValue;

    private double encoderPitch;

    private double speed;

    private double yPitch;

    private double timeout;

    private bool isConnected;

    private int roi_Top;

    private int roi_Buttom;

    private RenderWindowControl _vtkControl = new RenderWindowControl();

    private vtkActor pointcloudActor = null;


    public FrmCamera3DSetting(BaseInfo mBaseInfo, string pathStr)
    {
        SetControlState(state: true);
        baseInfo = mBaseInfo;
        camSNState = false;
        XMLpath = pathStr;
        LogUtil.Log("3D参数设置：从外部菜单进入设置参数");
    }

    public FrmCamera3DSetting(CameraConfigData camConfigData, BaseInfo mBaseInfo, string pathStr)
    {
        SetControlState(state: false);
        mCameraConfigData = camConfigData;
        baseInfo = mBaseInfo;
        XMLpath = pathStr;
        if (mCameraConfigData.CamSN != null && mCameraConfigData.CamSN != "")
        {
            camSNState = true;
            CameraConfigData cam3DConfigData = mBaseInfo.Query(mCameraConfigData.CamSN);
            if (cam3DConfigData == null)
            {
                cam3DConfigData = new CameraConfigData();
            }
            string[] cam3DVendor = CameraBase.Cam3DVendor;
            foreach (string item in cam3DVendor)
            {
                if (item == cam3DConfigData.CamVendor)
                {
                    cameraType = item;
                }
                try
                {
                    ethernetConfig.wPortNo = Convert.ToUInt16(cam3DConfigData.Prot);
                    highSpeedPortNo = Convert.ToUInt16(cam3DConfigData.HighProt);
                    if (cam3DConfigData.CamIP != "" && cam3DConfigData.CamIP != null)
                    {
                        cameraIP = cam3DConfigData.CamIP;
                        string[] Ipstr = cam3DConfigData.CamIP.Split('.');
                        ethernetConfig.abyIpAddress = new byte[4]
                        {
                            Convert.ToByte(Ipstr[0]),
                            Convert.ToByte(Ipstr[1]),
                            Convert.ToByte(Ipstr[2]),
                            Convert.ToByte(Ipstr[3])
                        };
                    }
                    if (mCameraConfigData.SettingParams != null && mCameraConfigData.SettingParams.ScanLines != 0)
                    {
                        _profileCount = (uint)mCameraConfigData.SettingParams.ScanLines;
                    }
                    else if (cam3DConfigData.SettingParams != null && cam3DConfigData.SettingParams.ScanLines != 0)
                    {
                        _profileCount = (uint)cam3DConfigData.SettingParams.ScanLines;
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        LogUtil.Log("3D参数设置：从配置页面进入设置参数");
    }

    public void SetControlState(bool state)
    {
        InitializeComponent();
        _vtkControl.Parent = tabPage1;
        _vtkControl.Dock = DockStyle.Fill;
        InitialControl();
        nUDAcqLineRate.DecimalPlaces = 1;
        nUDScanLength.DecimalPlaces = 1;
        nUDExpo.DecimalPlaces = 1;
        nUDEncoderPitch.DecimalPlaces = 4;
        nUDSpeed.DecimalPlaces = 2;
        nUDSpacing.DecimalPlaces = 4;
        nUDTimeout.DecimalPlaces = 1;
        if (state)
        {
            cbVendors.Enabled = true;
            btnAddCam.Enabled = true;
            btnAddCam.Visible = true;
            btnSaveParams.Visible = false;
            btnSaveParams.Enabled = false;
            button2.Enabled = true;
        }
        else
        {
            cbVendors.Enabled = false;
            btnAddCam.Enabled = false;
            btnAddCam.Visible = false;
            btnSaveParams.Visible = true;
            btnSaveParams.Enabled = true;
            button2.Enabled = false;
        }
    }

    public void InitialControl()
    {
        cbVendors.Items.Clear();
        ComboBox.ObjectCollection items = cbVendors.Items;
        object[] cam3DVendor = CameraBase.Cam3DVendor;
        items.AddRange(cam3DVendor);
        cbAcqMode.Items.Clear();
        cbAcqMode.Items.AddRange(new object[5] { "Time-ExternTrigger", "Time-Software", "Encoder-ExternTrigger", "Encoder-Software", "Test-Time" });
        label7.Visible = false;
        tbIp.Visible = false;
        InitChangeCamOFF();
    }

    private void FrmCamera3DSetting_Load(object sender, EventArgs e)
    {
        VTKDisplay();
        nUDTimeout.Value = 5000m;
        timeout = Convert.ToDouble(nUDTimeout.Value);
        InitChangeCamOFF();
        UpdateCCDList();
        if (camSNState)
        {
            cbVendors.SelectedItem = cameraType;
            if (cameraIP != "")
            {
                tbIp.Text = cameraIP;
                tbIp.Enabled = false;
            }
            camera = CameraOperator.Find3DCamera(mCameraConfigData.CamSN);
            if (camera != null && camera.camErrCode == CamErrCode.ConnectSuccess)
            {
                camera.ShowPointCloudDelegate = ShowPointCloud;
                camera.UpdateImage = UpdateUIImage;
                toolStripStatusLabel1.ForeColor = Color.Green;
                toolStripStatusLabel1.Text = "相机连接成功";
                btnConnect.Text = "断开连接";
                UpdateSensorInfo();
                InitChangeCamON();
            }
        }
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
            if (item.CamCategory == CameraBase.CameraType["3D"])
            {
                CCDList.Items.Add(item.CamSN + snip + item.CamIP + ")");
            }
        }
    }

    private void InitChangeCamOFF()
    {
        gBAcqControl.Enabled = false;
        gBEncoder.Enabled = false;
        gBSensorInfo.Enabled = false;
        btnAddCam.Enabled = false;
    }

    private void InitChangeCamON()
    {
        gBAcqControl.Enabled = true;
        gBEncoder.Enabled = true;
        gBSensorInfo.Enabled = true;
        btnAddCam.Enabled = true;
    }

    private void ViewDirection(vtkRenderer renderer, double lookX, double lookY, double lookZ, double upX, double upY, double upZ)
    {
        renderer.GetActiveCamera().SetPosition(0.0, 0.0, 0.0);
        renderer.GetActiveCamera().SetFocalPoint(lookX, lookY, lookZ);
        renderer.GetActiveCamera().SetViewUp(upX, upY, upZ);
        renderer.GetActiveCamera().SetParallelProjection(1);
        renderer.ResetCamera();
    }

    private void ViewPositiveZ(vtkRenderer renderer)
    {
        ViewDirection(renderer, 0.0, 0.0, -1.0, 0.0, 1.0, 0.0);
    }

    private void ViewNegativeZ(vtkRenderer renderer)
    {
        ViewDirection(renderer, 0.0, 0.0, 1.0, 0.0, 1.0, 0.0);
    }

    private void VTKDisplay()
    {
        vtkAxesActor axes = vtkAxesActor.New();
        axes.SetAxisLabels(0);
        _vtkControl.RenderWindow.GetRenderers().GetFirstRenderer().AddActor(axes);
        _vtkControl.RenderWindow.GetRenderers().GetFirstRenderer().ResetCamera();
        _vtkControl.RenderWindow.GetRenderers().GetFirstRenderer().Render();
    }

    private void ShowPointCloud(double[,] data, double xInterval, double yInterval)
    {
        Thread.CurrentThread.IsBackground = true;
        if (_vtkControl.InvokeRequired)
        {
            _vtkControl.BeginInvoke(new Action<double[,], double, double>(ShowPointCloud), data, xInterval, yInterval);
            return;
        }
        if (pointcloudActor != null)
        {
            _vtkControl.RenderWindow.GetRenderers().GetFirstRenderer().RemoveActor(pointcloudActor);
        }
        _vtkControl.RenderWindow.GetRenderers().GetFirstRenderer().Clear();
        double lowz = double.MaxValue;
        double highz = double.MinValue;
        vtkPoints points = vtkPoints.New();
        vtkCellArray vertex = vtkCellArray.New();
        double mXscale = (double)XscaleUpDown.Value;
        double mYscale = (double)YscaleUpDown.Value;
        double mZscale = (double)ZscaleUpDown.Value;
        for (int i = 0; i < data.GetLongLength(0); i++)
        {
            for (int j = 0; j < data.GetLongLength(1); j++)
            {
                if (!double.IsNaN(data[i, j]))
                {
                    if (lowz > data[i, j])
                    {
                        lowz = data[i, j];
                    }
                    if (highz < data[i, j])
                    {
                        highz = data[i, j];
                    }
                    int k = i * data.GetLength(1) + j;
                    points.InsertPoint(k, (double)j * xInterval * mXscale, (double)i * yInterval * mYscale, data[i, j] * mZscale);
                    vertex.InsertNextCell(1);
                    vertex.InsertCellPoint(k);
                }
            }
        }
        vtkPolyData polyData = vtkPolyData.New();
        polyData.SetPoints(points);
        polyData.SetVerts(vertex);
        polyData.GetPointData().SetActiveScalars("pointCloud");
        vtkElevationFilter elevationFilter = vtkElevationFilter.New();
        elevationFilter.SetInput(polyData);
        elevationFilter.SetLowPoint(0.0, 0.0, lowz);
        elevationFilter.SetHighPoint(0.0, 0.0, highz);
        vtkPolyDataMapper polyDataMapper = vtkPolyDataMapper.New();
        polyDataMapper.SetInputConnection(elevationFilter.GetOutputPort());
        pointcloudActor = vtkActor.New();
        pointcloudActor.SetMapper(polyDataMapper);
        pointcloudActor.SetVisibility(1);
        pointcloudActor.GetProperty().SetRepresentationToSurface();
        _vtkControl.RenderWindow.GetRenderers().GetFirstRenderer().AddActor(pointcloudActor);
        _vtkControl.RenderWindow.GetRenderers().GetFirstRenderer().ResetCamera();
        _vtkControl.RenderWindow.GetRenderers().GetFirstRenderer().Render();
        _vtkControl.Refresh();
        data.Initialize();
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

    private void btn_ZPositive_Click(object sender, EventArgs e)
    {
        ViewPositiveZ(_vtkControl.RenderWindow.GetRenderers().GetFirstRenderer());
        _vtkControl.Refresh();
    }

    private void btn_ZNegative_Click(object sender, EventArgs e)
    {
        ViewNegativeZ(_vtkControl.RenderWindow.GetRenderers().GetFirstRenderer());
        _vtkControl.Refresh();
    }

    private void btn_ClearDisplay_Click(object sender, EventArgs e)
    {
        if (pointcloudActor != null)
        {
            _vtkControl.RenderWindow.GetRenderers().GetFirstRenderer().RemoveActor(pointcloudActor);
        }
        _vtkControl.RenderWindow.GetRenderers().GetFirstRenderer().Clear();
        _vtkControl.RenderWindow.GetRenderers().GetFirstRenderer().Render();
        _vtkControl.Refresh();
        imageDisplay1.CogImage = null;
    }

    private void btnConnect_Click(object sender, EventArgs e)
    {
        if (btnConnect.Text == "连接")
        {
            isConnected = false;
            if (cbVendors.SelectedItem == null)
            {
                MessageBox.Show("相机品牌不能为空！！！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                if (cbVendors.SelectedIndex == 1)
                {
                    using (OpenEthernetForm openEthernetForm = new OpenEthernetForm())
                    {
                        openEthernetForm.EthernetConfig = ethernetConfig;
                        if (DialogResult.OK != openEthernetForm.ShowDialog())
                        {
                            return;
                        }
                        ethernetConfig = openEthernetForm.EthernetConfig;
                        using HighSpeedInitializeForm highSpeedInitializeForm = new HighSpeedInitializeForm();
                        highSpeedInitializeForm.EthernetConfig = ethernetConfig;
                        highSpeedInitializeForm.HighSpeedPortNo = highSpeedPortNo;
                        highSpeedInitializeForm.ProfileCount = _profileCount;
                        if (DialogResult.OK != highSpeedInitializeForm.ShowDialog())
                        {
                            return;
                        }
                        ethernetConfig = highSpeedInitializeForm.EthernetConfig;
                        highSpeedPortNo = highSpeedInitializeForm.HighSpeedPortNo;
                        _profileCount = highSpeedInitializeForm.ProfileCount;
                        using PreStartHighSpeedForm preStartHighSpeedForm = new PreStartHighSpeedForm();
                        if (DialogResult.OK != preStartHighSpeedForm.ShowDialog())
                        {
                            return;
                        }
                        bySendPosition = preStartHighSpeedForm.Request.bySendPosition;
                    }
                    CameraConfigData mCamConfigData2 = new CameraConfigData();
                    mCamConfigData2.CamIP = string.Join(".", ethernetConfig.abyIpAddress);
                    mCamConfigData2.Prot = ethernetConfig.wPortNo.ToString();
                    mCamConfigData2.HighProt = highSpeedPortNo.ToString();
                    mCamConfigData2.SettingParams = new CamParams();
                    mCamConfigData2.SettingParams.ScanLines = (int)_profileCount;
                    mCamConfigData2.CamVendor = CameraBase.Cam3DVendor[1];
                    if (CameraOperator.Open3DCamera(mCamConfigData2) == -1)
                    {
                        MessageBox.Show("相机打开失败！");
                        return;
                    }
                    camera = CameraOperator.Find3DIPCamera(mCamConfigData2.CamIP);
                    if (camera == null)
                    {
                        MessageBox.Show("Ethernet通讯连接失败");
                        return;
                    }
                    isConnected = true;
                    MessageBox.Show("Ethernet通讯连接成功");
                }
                else
                {
                    CameraConfigData mCamConfigData = new CameraConfigData();
                    mCamConfigData.CamIP = tbIp.Text;
                    mCamConfigData.CamVendor = CameraBase.Cam3DVendor[cbVendors.SelectedIndex];
                    if (CameraOperator.Open3DCamera(mCamConfigData) == -1)
                    {
                        MessageBox.Show(mCamConfigData.CamVendor + "相机打开失败！");
                        return;
                    }
                    camera = CameraOperator.Find3DIPCamera(mCamConfigData.CamIP);
                    if (camera == null)
                    {
                        MessageBox.Show(mCamConfigData.CamVendor + "相机开启失败");
                        InitChangeCamOFF();
                        return;
                    }
                    isConnected = true;
                    MessageBox.Show(mCamConfigData.CamVendor + "相机开启成功");
                }
                camera.ShowPointCloudDelegate = ShowPointCloud;
                camera.UpdateImage = UpdateUIImage;
                toolStripStatusLabel1.ForeColor = Color.Green;
                toolStripStatusLabel1.Text = "相机连接成功";
                btnConnect.Text = "断开连接";
                UpdateSensorInfo();
                InitChangeCamON();
            }
            catch
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "相机连接失败";
                btnConnect.Text = "连接";
                isConnected = false;
                InitChangeCamOFF();
            }
            tbIp.Enabled = false;
            LogUtil.Log("3D参数设置：连接相机" + CameraBase.Cam3DVendor[cbVendors.SelectedIndex]);
        }
        else
        {
            camera.Close_Sensor();
            btnConnect.Text = "连接";
            tbIp.Enabled = true;
            SetCtrlDefault();
            LogUtil.Log("3D参数设置：断开相机" + CameraBase.Cam3DVendor[cbVendors.SelectedIndex]);
        }
    }

    public void SetCtrlDefault()
    {
        lbVendor.Text = "N/A";
        lbModel.Text = "N/A";
        lbVersion.Text = "N/A";
        lbSerialNum.Text = "N/A";
        lbIP.Text = "N/A";
        InitChangeCamOFF();
    }

    private void btnRecOrSto_Click(object sender, EventArgs e)
    {
        if (camera == null)
        {
            return;
        }
        if (btnRecOrSto.Text == "开始接收")
        {
            if (camera.Start_Grab(state: true) == 0)
            {
                btnRecOrSto.Text = "停止接收";
                btnConnect.Enabled = false;
                InitChangeCamOFF();
                if (camera._cameraVendor == CameraBase.Cam3DVendor[1])
                {
                    label30.Text = Convert.ToString(camera.GetXPitch());
                }
            }
            else
            {
                MessageBox.Show("开始采集失败");
                InitChangeCamON();
            }
            LogUtil.Log("3D参数设置：开始接收数据（" + camera._cameraIp + "）");
        }
        else if (btnRecOrSto.Text == "停止接收")
        {
            if (camera.Stop_Grab(state: true) == 0)
            {
                btnRecOrSto.Text = "开始接收";
                InitChangeCamON();
                btnConnect.Enabled = true;
            }
            else
            {
                MessageBox.Show("结束采集失败");
                InitChangeCamOFF();
            }
            LogUtil.Log("3D参数设置：停止接收数据（" + camera._cameraIp + "）");
        }
    }

    public void UpdateSensorInfo()
    {
        if (camera == null)
        {
            return;
        }
        cameraConfig = baseInfo.Query(camera._cameraSn);
        if (camSNState && mCameraConfigData.CamSN == camera._cameraSn)
        {
            cameraConfig = mCameraConfigData;
        }
        if (cameraConfig == null)
        {
            cameraConfig = new CameraConfigData();
        }
        if (cameraConfig.SettingParams != null)
        {
            camera.triggerMode3D = (TriggerMode3D)cameraConfig.SettingParams.TriggerMode;
            camera._acqLineRate_index = cameraConfig.SettingParams.AcqLineRateIndex;
            camera._acqLineRate = cameraConfig.SettingParams.AcqLineRate;
            camera._scanLength = cameraConfig.SettingParams.ScanLength;
            if (camera._cameraVendor != CameraBase.Cam3DVendor[1])
            {
                camera._profileCount = (uint)cameraConfig.SettingParams.ScanLines;
            }
            else
            {
                camera._profileCount = _profileCount;
            }
            camera._expourse_index = cameraConfig.SettingParams.ExposureIndex;
            camera.exposure = cameraConfig.SettingParams.ExposureTime;
            camera.timeout = cameraConfig.SettingParams.Timeout;
            camera.encoderResolution = cameraConfig.SettingParams.EncoderResolution;
            camera.speed = cameraConfig.SettingParams.Speed;
            camera.y_pitch_mm = cameraConfig.SettingParams.y_pitch_mm;
            camera.SettingParams.ROI_Top = cameraConfig.SettingParams.ROI_Top;
            camera.SettingParams.ROI_Buttom = cameraConfig.SettingParams.ROI_Buttom;
        }
        lbVendor.Text = camera._cameraVendor;
        lbModel.Text = camera._cameraModelName;
        lbVersion.Text = camera._version;
        lbSerialNum.Text = camera._cameraSn;
        cbAcqLineRate.Items.Clear();
        cbExposure.Items.Clear();
        if (camera._cameraVendor == CameraBase.Cam3DVendor[1])
        {
            if (camera.headModel.StartsWith("LJ-V"))
            {
                ComboBox.ObjectCollection items = cbExposure.Items;
                object[] items2 = expo_LJV.ToArray();
                items.AddRange(items2);
            }
            else if (camera.headModel.StartsWith("LJ-X"))
            {
                ComboBox.ObjectCollection items3 = cbExposure.Items;
                object[] items2 = expo_LJX.ToArray();
                items3.AddRange(items2);
                ComboBox.ObjectCollection items4 = cbAcqLineRate.Items;
                items2 = acqLR_LJX.ToArray();
                items4.AddRange(items2);
            }
            if (camera.headModel.StartsWith("LJ-V") && camera.headModel.EndsWith("B"))
            {
                ComboBox.ObjectCollection items5 = cbAcqLineRate.Items;
                object[] items2 = acqLR_LJVB.ToArray();
                items5.AddRange(items2);
            }
            else if (camera.headModel.StartsWith("LJ-V"))
            {
                ComboBox.ObjectCollection items6 = cbAcqLineRate.Items;
                object[] items2 = acqLR_LJV.ToArray();
                items6.AddRange(items2);
            }
            cbAcqLineRate.SelectedIndex = camera._acqLineRate_index;
            cbExposure.SelectedIndex = camera._expourse_index;
            label30.Text = "N/A";
        }
        if (camera._cameraVendor == CameraBase.Cam3DVendor[2] || camera._cameraVendor == CameraBase.Cam3DVendor[0])
        {
            nROITop.Value = camera.SettingParams.ROI_Top;
            nROIButtom.Value = camera.SettingParams.ROI_Buttom;
        }
        if (camera._cameraVendor == CameraBase.Cam3DVendor[3])
        {
            ComboBox.ObjectCollection items7 = cbExposure.Items;
            object[] items2 = expo_SSZN.ToArray();
            items7.AddRange(items2);
            cbExposure.SelectedIndex = camera._expourse_index;
        }
        if (camera._cameraVendor == CameraBase.Cam3DVendor[4] && cameraConfig.ACQSettingParams != null)
        {
            camera.CCDSettingParams = cameraConfig.ACQSettingParams;
            camera.SetCameraInfo(camera.CCDSettingParams);
        }
        lbIP.Text = camera._cameraIp;
        cameraIP = camera._cameraIp;
        nUDScanLength.Value = (decimal)camera._scanLength;
        scanLength = camera._scanLength;
        nUDScanLines.Value = camera._profileCount;
        if (camera.triggerMode3D == TriggerMode3D.None)
        {
            camera.triggerMode3D = TriggerMode3D.Time_Software;
        }
        try
        {
            cbAcqMode.SelectedIndex = (int)camera.triggerMode3D;
        }
        catch
        {
            cbAcqMode.SelectedIndex = 1;
        }
        try
        {
            nUDExpo.Value = (decimal)camera.exposure;
            expoValue = camera.exposure;
        }
        catch
        {
        }
        try
        {
            nUDAcqLineRate.Value = (decimal)camera._acqLineRate;
            acqLineRateValue = camera._acqLineRate;
        }
        catch
        {
        }
        try
        {
            nUDEncoderPitch.Value = (decimal)camera.encoderResolution;
            encoderPitch = camera.encoderResolution;
        }
        catch
        {
        }
        try
        {
            nUDSpeed.Value = (decimal)camera.speed;
            speed = camera.speed;
        }
        catch
        {
        }
        try
        {
            nUDSpacing.Value = (decimal)camera.y_pitch_mm;
            yPitch = camera.y_pitch_mm;
        }
        catch
        {
        }
        try
        {
            if (camera.timeout > 0.0)
            {
                nUDTimeout.Value = (decimal)camera.timeout;
                timeout = camera.timeout;
                camera.SetTimeOut(ref timeout);
            }
        }
        catch
        {
        }
    }

    private void FrmCamera3DSetting_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (CameraOperator.camera3DCollection.Count <= 0)
        {
            return;
        }
        foreach (KeyValuePair<string, Camera3DBase> _3DCamera in CameraOperator.camera3DCollection._3DCameras)
        {
            Camera3DBase camera3D = _3DCamera.Value;
            if (camera3D != null)
            {
                camera3D.ShowPointCloudDelegate = null;
            }
        }
    }

    private void cbVendors_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbVendors.SelectedItem == null || nowCameraType == (string)cbVendors.SelectedItem)
        {
            return;
        }
        if (cameraType == (string)cbVendors.SelectedItem && btnConnect.Text == "断开连接")
        {
            DialogResult dr = MessageBox.Show("是否保存并切换当前相机！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (dr == DialogResult.OK)
            {
                btnAddCam_Click(null, null);
            }
        }
        nowCameraType = (string)cbVendors.SelectedItem;
        tbIp.Enabled = true;
        setCCDParamsInfo.Visible = false;
        cbAcqMode.Enabled = true;
        nUDScanLines.Enabled = true;
        nUDSpacing.Enabled = true;
        switch (cbVendors.SelectedIndex)
        {
            case 0:
                label7.Visible = true;
                tbIp.Visible = true;
                if (cameraType == CameraBase.Cam3DVendor[0] && cameraIP != "")
                {
                    tbIp.Text = cameraIP;
                }
                else
                {
                    tbIp.Text = "192.168.0.13";
                }
                cbAcqLineRate.Enabled = false;
                nUDAcqLineRate.Enabled = true;
                nUDScanLength.Enabled = true;
                nUDScanLines.Enabled = false;
                cbExposure.Enabled = false;
                nUDExpo.Enabled = true;
                nUDEncoderPitch.Enabled = true;
                nUDSpeed.Enabled = true;
                label30.Visible = false;
                nROITop.Enabled = true;
                nROIButtom.Enabled = true;
                label32.Text = "测量范围";
                label33.Text = "Z轴起点";
                break;
            case 1:
                label7.Visible = false;
                tbIp.Visible = false;
                cbAcqLineRate.Enabled = true;
                nUDScanLength.Enabled = false;
                nUDScanLines.Enabled = true;
                nUDAcqLineRate.Enabled = false;
                cbExposure.Enabled = true;
                nUDExpo.Enabled = false;
                nUDEncoderPitch.Enabled = false;
                nUDSpeed.Enabled = false;
                label30.Visible = true;
                nROITop.Enabled = false;
                nROIButtom.Enabled = false;
                break;
            case 2:
                label7.Visible = true;
                tbIp.Visible = true;
                if (cameraType == CameraBase.Cam3DVendor[2] && cameraIP != "")
                {
                    tbIp.Text = cameraIP;
                }
                else
                {
                    tbIp.Text = "192.168.1.3";
                }
                cbAcqLineRate.Enabled = false;
                nUDScanLength.Enabled = false;
                nUDScanLines.Enabled = true;
                nUDAcqLineRate.Enabled = true;
                cbExposure.Enabled = false;
                nUDExpo.Enabled = true;
                nUDEncoderPitch.Enabled = false;
                nUDSpeed.Enabled = false;
                label30.Visible = false;
                nROITop.Enabled = true;
                nROIButtom.Enabled = true;
                label32.Text = "ROITop";
                label33.Text = "ROIButtom";
                break;
            case 3:
                label7.Visible = true;
                tbIp.Visible = true;
                if (cameraType == CameraBase.Cam3DVendor[3] && cameraIP != "")
                {
                    tbIp.Text = cameraIP;
                }
                else
                {
                    tbIp.Text = "192.168.0.10";
                }
                cbAcqLineRate.Enabled = false;
                nUDScanLength.Enabled = false;
                nUDScanLines.Enabled = true;
                nUDAcqLineRate.Enabled = true;
                cbExposure.Enabled = true;
                nUDExpo.Enabled = false;
                nUDEncoderPitch.Enabled = false;
                nUDSpeed.Enabled = false;
                label30.Visible = false;
                nROITop.Enabled = false;
                nROIButtom.Enabled = false;
                break;
            case 4:
                label7.Visible = true;
                tbIp.Visible = true;
                if (cameraType == CameraBase.Cam3DVendor[4] && cameraIP != "")
                {
                    tbIp.Text = cameraIP;
                }
                else
                {
                    tbIp.Text = "192.168.3.200";
                }
                cbAcqLineRate.Enabled = false;
                nUDScanLength.Enabled = false;
                nUDScanLines.Enabled = false;
                nUDAcqLineRate.Enabled = false;
                cbExposure.Enabled = false;
                nUDExpo.Enabled = false;
                nUDEncoderPitch.Enabled = false;
                nUDSpeed.Enabled = false;
                setCCDParamsInfo.Visible = true;
                cbAcqMode.Enabled = false;
                nUDScanLines.Enabled = false;
                nUDSpacing.Enabled = false;
                label30.Visible = false;
                nROITop.Enabled = false;
                nROIButtom.Enabled = false;
                break;
        }
        InitChangeCamOFF();
        btnConnect.Text = "连接";
        btnConnect.Enabled = true;
        btnRecOrSto.Text = "开始接收";
        btnRecOrSto.Enabled = true;
        LogUtil.Log("3D参数设置：切换相机" + nowCameraType);
    }

    private void btnSnap_Click(object sender, EventArgs e)
    {
        if (camera != null)
        {
            LogUtil.Log("3D参数设置：启动批处理一次（" + camera._cameraIp + "）");
            if (camera.SoftTriggerOnce() == 0)
            {
                MessageBox.Show("启动批处理");
            }
            else
            {
                MessageBox.Show("启动批处理失败");
            }
        }
    }

    private void cbAcqMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbAcqMode.SelectedIndex == 0)
        {
            btnSnap.Enabled = false;
            camera.SetTriggerMode(TriggerMode3D.Time_ExternTrigger);
            acqTriggerMode = TriggerMode3D.Time_ExternTrigger;
        }
        else if (cbAcqMode.SelectedIndex == 1)
        {
            btnSnap.Enabled = true;
            camera.SetTriggerMode(TriggerMode3D.Time_Software);
            acqTriggerMode = TriggerMode3D.Time_Software;
        }
        else if (cbAcqMode.SelectedIndex == 2)
        {
            btnSnap.Enabled = false;
            camera.SetTriggerMode(TriggerMode3D.Encoder_ExternTrigger);
            acqTriggerMode = TriggerMode3D.Encoder_ExternTrigger;
        }
        else if (cbAcqMode.SelectedIndex == 3)
        {
            btnSnap.Enabled = true;
            camera.SetTriggerMode(TriggerMode3D.Encoder_Software);
            acqTriggerMode = TriggerMode3D.Encoder_Software;
        }
        else if (cbAcqMode.SelectedIndex == 4)
        {
            btnSnap.Enabled = false;
            camera.SetTriggerMode(TriggerMode3D.Test_Time);
            acqTriggerMode = TriggerMode3D.Test_Time;
        }
        acqTriggerMode = TriggerMode3D.None;
    }

    private void nUDScanLength_ValueChanged(object sender, EventArgs e)
    {
        double temp = Convert.ToDouble(nUDScanLength.Value);
        if (scanLength != temp)
        {
            scanLength = temp;
            camera.SetScanLength(ref scanLength);
        }
    }

    private void nUDScanLines_ValueChanged(object sender, EventArgs e)
    {
        int temp = Convert.ToInt32(nUDScanLines.Value);
        if (scanLines != temp && camera != null)
        {
            scanLines = temp;
            camera.SetScanLines(ref scanLines);
            if (camera._cameraVendor == CameraBase.Cam3DVendor[1])
            {
                _profileCount = (uint)scanLines;
            }
        }
    }

    private void nUDAcqLineRate_ValueChanged(object sender, EventArgs e)
    {
        double temp = Convert.ToDouble(nUDAcqLineRate.Value);
        if (acqLineRateValue != temp)
        {
            acqLineRateValue = temp;
            camera.SetAcqLineRate(ref acqLineRateValue);
        }
    }

    private void nUDExpo_ValueChanged(object sender, EventArgs e)
    {
        double temp = Convert.ToDouble(nUDExpo.Value);
        if (expoValue != temp)
        {
            expoValue = temp;
            camera.SetExposure(ref expoValue);
        }
    }

    private void nUDEncoderPitch_ValueChanged(object sender, EventArgs e)
    {
        double temp = Convert.ToDouble(nUDEncoderPitch.Value);
        if (encoderPitch != temp)
        {
            encoderPitch = temp;
            camera.SetEncoderResolution(ref encoderPitch);
        }
    }

    private void nUDSpeed_ValueChanged(object sender, EventArgs e)
    {
        double temp = Convert.ToDouble(nUDSpeed.Value);
        if (speed != temp)
        {
            speed = temp;
            camera.SetTravelSpeed(ref speed);
        }
    }

    private void nUDSpacing_ValueChanged(object sender, EventArgs e)
    {
        double temp = Convert.ToDouble(nUDSpacing.Value);
        if (yPitch != temp)
        {
            yPitch = temp;
            camera.SetYPitch(ref yPitch);
        }
    }

    private void nROITop_ValueChanged(object sender, EventArgs e)
    {
        int temp = Convert.ToInt32(nROITop.Value);
        if (roi_Top != temp)
        {
            roi_Top = temp;
            camera.SetROI_Top(ref roi_Top);
        }
    }

    private void nROIButtom_ValueChanged(object sender, EventArgs e)
    {
        int temp = Convert.ToInt32(nROIButtom.Value);
        if (roi_Buttom != temp)
        {
            roi_Buttom = temp;
            camera.SetROI_Buttom(ref roi_Buttom);
        }
    }

    private void cbAcqLineRate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbAcqLineRate.SelectedItem != null)
        {
            int index = cbAcqLineRate.SelectedIndex;
            camera.SetAcqLineRateIndex(ref index);
        }
    }

    private void cbExposure_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbExposure.SelectedItem != null)
        {
            int index = cbExposure.SelectedIndex;
            camera.SetExpoIndex(ref index);
        }
    }

    private void nUDTimeout_ValueChanged(object sender, EventArgs e)
    {
        double temp = Convert.ToDouble(nUDTimeout.Value);
        if (timeout != temp && camera != null)
        {
            camera.SetTimeOut(ref temp);
            timeout = temp;
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
            camParams.TriggerMode = (int)camera.triggerMode3D;
            camParams.AcqLineRateIndex = camera._acqLineRate_index;
            camParams.AcqLineRate = (int)camera._acqLineRate;
            camParams.ScanLength = camera._scanLength;
            camParams.ScanLines = (int)camera._profileCount;
            camParams.ExposureIndex = camera._expourse_index;
            camParams.ExposureTime = (int)camera.exposure;
            camParams.Timeout = camera.timeout;
            camParams.EncoderResolution = camera.encoderResolution;
            camParams.Speed = camera.speed;
            camParams.y_pitch_mm = camera.y_pitch_mm;
            camParams.ROI_Top = camera.SettingParams.ROI_Top;
            camParams.ROI_Buttom = camera.SettingParams.ROI_Buttom;
            cameraConfigData = new CameraConfigData();
            cameraConfigData.CamCategory = camera.CamCategory;
            cameraConfigData.CamSN = camera._cameraSn;
            cameraConfigData.CamVendor = camera._cameraVendor;
            cameraConfigData.CamIP = camera._cameraIp;
            cameraConfigData.CameraModel = camera._cameraModelName;
            if (camera._cameraVendor == CameraBase.Cam3DVendor[1])
            {
                cameraConfigData.Prot = ethernetConfig.wPortNo.ToString();
                cameraConfigData.HighProt = highSpeedPortNo.ToString();
            }
            cameraConfigData.SettingParams = camParams;
            if (camera._cameraVendor == CameraBase.Cam3DVendor[4])
            {
                cameraConfigData.ACQSettingParams = camera.CCDSettingParams;
            }
            else
            {
                cameraConfigData.ACQSettingParams = null;
            }
            if (baseInfo.CCDList == null)
            {
                baseInfo.CCDList = new List<CameraConfigData>();
            }
            List<string> CanSNList = baseInfo.SnList_3D;
            if (CanSNList == null)
            {
                CanSNList = new List<string>();
            }
            if (CanSNList.Contains(cameraConfigData.CamSN))
            {
                if (!baseInfo.Modify(cameraConfigData))
                {
                    MessageBox.Show("参数更新失败");
                    return;
                }
            }
            else
            {
                DialogResult dr = MessageBox.Show("确定是否将当前相机添加至配置文件！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (dr != DialogResult.OK)
                {
                    return;
                }
                if (!baseInfo.Add(cameraConfigData))
                {
                    MessageBox.Show("相机添加失败");
                    return;
                }
            }
            if (XmlHelper.WriteXML(XMLpath, baseInfo.CCDList))
            {
                MessageBox.Show("相机添加到XML文件成功");
                LogUtil.Log("3D参数设置：相机添加到XML文件成功( " + camera._cameraIp + ")");
            }
            else
            {
                MessageBox.Show("相机添加XML文件失败");
                LogUtil.LogError("3D参数设置：相机添加XML文件失败( " + camera._cameraIp + ")");
            }
        }
        catch (Exception)
        {
            MessageBox.Show("相机添加XML文件失败");
            LogUtil.LogError("3D参数设置：相机添加XML文件失败( " + camera._cameraIp + ")");
        }
        UpdateCCDList();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        if (CCDList.SelectedItem == null || CCDList.SelectedItem.ToString() == "")
        {
            MessageBox.Show("请先选择需要删除的相机序列号");
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
                MessageBox.Show("将相机移除XML成功");
                LogUtil.Log("3D参数设置：将相机移除XML成功( " + sn + ")");
            }
            else
            {
                MessageBox.Show("将相机移除XML失败");
                LogUtil.LogError("3D参数设置：将相机移除XML失败( " + sn + ")");
            }
        }
        else
        {
            MessageBox.Show("将相机移除XML失败");
            LogUtil.LogError("3D参数设置：将相机移除XML失败( " + sn + ")");
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
            DialogResult dr = MessageBox.Show("是否将修改的参数写入主配置！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (dr != DialogResult.OK)
            {
                return;
            }
            if (camSNState && mCameraConfigData.CamSN == camera._cameraSn)
            {
                mCameraConfigData.CamIP = camera._cameraIp;
                mCameraConfigData.CamCategory = camera.CamCategory;
                mCameraConfigData.CamSN = camera._cameraSn;
                mCameraConfigData.CamVendor = camera._cameraVendor;
                mCameraConfigData.CameraModel = camera._cameraModelName;
                if (camera._cameraVendor == CameraBase.Cam3DVendor[1])
                {
                    mCameraConfigData.Prot = ethernetConfig.wPortNo.ToString();
                    mCameraConfigData.HighProt = highSpeedPortNo.ToString();
                }
                if (camera._cameraVendor == CameraBase.Cam3DVendor[4])
                {
                    mCameraConfigData.ACQSettingParams = camera.CCDSettingParams;
                }
                else
                {
                    mCameraConfigData.ACQSettingParams = null;
                }
                if (mCameraConfigData.SettingParams == null)
                {
                    mCameraConfigData.SettingParams = new CamParams();
                }
                mCameraConfigData.SettingParams.TriggerMode = (int)camera.triggerMode3D;
                mCameraConfigData.SettingParams.AcqLineRateIndex = camera._acqLineRate_index;
                mCameraConfigData.SettingParams.AcqLineRate = (int)camera._acqLineRate;
                mCameraConfigData.SettingParams.ScanLength = camera._scanLength;
                mCameraConfigData.SettingParams.ScanLines = (int)camera._profileCount;
                mCameraConfigData.SettingParams.ExposureIndex = camera._expourse_index;
                mCameraConfigData.SettingParams.ExposureTime = (int)camera.exposure;
                mCameraConfigData.SettingParams.Timeout = camera.timeout;
                mCameraConfigData.SettingParams.EncoderResolution = camera.encoderResolution;
                mCameraConfigData.SettingParams.Speed = camera.speed;
                mCameraConfigData.SettingParams.y_pitch_mm = camera.y_pitch_mm;
                mCameraConfigData.SettingParams.ROI_Top = camera.SettingParams.ROI_Top;
                mCameraConfigData.SettingParams.ROI_Buttom = camera.SettingParams.ROI_Buttom;
            }
            LogUtil.Log("3D参数设置：保存相机参数成功( " + camera._cameraIp + ")");
        }
        catch (Exception)
        {
            MessageBox.Show("写入失败");
        }
    }

    private void CCDList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!isConnected && !camSNState && CCDList.SelectedItem != null)
        {
            string item = CCDList.SelectedItem.ToString();
            string[] SNAndIP = item.Split(':');
            string ip = SNAndIP[SNAndIP.Length - 1];
            tbIp.Text = ip.Split(')')[0];
        }
    }

    private void setCCDParamsInfo_Click(object sender, EventArgs e)
    {
        if (camera.CCD != null)
        {
            ACQfifoTool acqFifo = new ACQfifoTool(camera.CCD);
            acqFifo.ShowDialog();
            Cognex.VisionPro.ICogAcqFifo acq = acqFifo.cogAcqFifoCtlV21.Subject;
            camera.GetCameraInfo();
        }
    }

}