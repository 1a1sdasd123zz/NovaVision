using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using NovaVision.BaseClass.Helper;
using NovaVision.Hardware;
using NovaVision.Hardware.Frame_Grabber_CameraLink_._03_IKap;

namespace NovaVision.VisionForm.CarameFrm;

public partial class FrmVulcanFrameGrabber : Form
{
    private Bv_Camera camera;

    private string m_SerialNum = string.Empty;

    private bool m_ConfigFileEnable;

    private bool m_configFileAvailable;

    private string m_currentConfigDir;

    private string m_ProductDir = "";

    private string m_ConfigFile = "";

    private string m_currentConfigFileName = "";

    private int m_currentConfigFileIndex;

    private ArrayList vlcffiles = new ArrayList();

    private bool m_init = false;

    private bool enterFlag = false;

    private FrameGrabberConfigData m_ParamValues;

    private FrameGrabberConfigData out_ParamValues;

    private static string ConfigKeyName = "Camera Name";

    private static string CompanyKeyName = "Company Name";

    private static string ModelKeyName = "Model Name";

    private static string VicName = "Vic Name";

    private static string Acquisition_Default_folder = "\\CamFiles\\User";



    public FrmVulcanFrameGrabber(FrameGrabberConfigData paramValues)
        : this(paramValues, flag: false)
    {
    }

    public FrmVulcanFrameGrabber(FrameGrabberConfigData paramValues, bool flag)
    {
        InitializeComponent();
        enterFlag = flag;
        out_ParamValues = paramValues;
        m_ParamValues = paramValues;
        if (enterFlag)
        {
            m_SerialNum = m_ParamValues["Serial"].Value.ToString();
            m_ConfigFile = m_ParamValues["ConfigPath"].Value.ToString();
        }
    }

    private void FrmFrameGrabberSetting_Load(object sender, EventArgs e)
    {
        cbSerial.Items.Clear();
        listDevice.Items.Clear();
        InitialControlStatus();
        cbCameraVendor.SelectedIndex = 0;
        tsslStatus.Text = "正在枚举Vulcan卡设备";
        tsslStatus.BackColor = Color.Orange;
        DateTime now = DateTime.Now;
        TimeSpan timeSpan = default(TimeSpan);
        if (!enterFlag)
        {
            Bv_Vulcan.EnumCards();
        }
        listDevice.Invoke((MethodInvoker)delegate
        {
            if (FrameGrabberOperator.dicVendorNameKey.ContainsKey("IKap"))
            {
                ListBox.ObjectCollection items3 = listDevice.Items;
                object[] items4 = FrameGrabberOperator.dicVendorNameKey["IKap"].ToArray();
                items3.AddRange(items4);
            }
            if (listDevice.Items.Count > 0 && !enterFlag)
            {
                btnRemove.Enabled = true;
            }
        });
        cbSerial.Invoke((MethodInvoker)delegate
        {
            ComboBox.ObjectCollection items = cbSerial.Items;
            object[] items2 = Bv_Vulcan.vulcanSerialList.Select((string o) => new MyListBoxItem(o.ToString())).ToArray();
            items.AddRange(items2);
            if (cbSerial.Items.Count > 0)
            {
                if (enterFlag)
                {
                    foreach (object current in cbSerial.Items)
                    {
                        if (m_SerialNum.Equals(current.ToString()))
                        {
                            cbSerial.SelectedItem = current;
                            break;
                        }
                    }
                }
                else
                {
                    cbSerial.SelectedIndex = 0;
                }
                m_SerialNum = cbSerial.SelectedItem.ToString();
                if (!enterFlag)
                {
                    m_ParamValues = FrameGrabberOperator.FindDeviceConfigByVendorKey("IKap," + m_SerialNum);
                }
                if (m_ParamValues != null)
                {
                    m_ConfigFile = m_ParamValues["ConfigPath"].Value.ToString();
                }
                timeSpan = DateTime.Now - now;
                tsslStatus.Text = $"查找到{cbSerial.Items.Count}张Vulcan卡";
                tsslStatus.BackColor = Color.Green;
                if (enterFlag)
                {
                    cbSerial.Enabled = false;
                }
                else
                {
                    cbSerial.Enabled = true;
                }
                gbConfigFile.Enabled = true;
                tsslElapsedTime.Text = $"{Math.Round(timeSpan.TotalMilliseconds, 2)}ms";
                SetDirectories();
                UpdateNames();
            }
            else
            {
                timeSpan = DateTime.Now - now;
                tsslStatus.Text = "未找到Vulcan卡";
                tsslStatus.BackColor = Color.Red;
                tsslElapsedTime.Text = $"{Math.Round(timeSpan.TotalMilliseconds, 2)}ms";
            }
        });
        btnOpenClose.Invoke((MethodInvoker)delegate
        {
            if (cbSerial.SelectedItem != null)
            {
                if (FrameGrabberOperator.dicCameras.ContainsKey(m_SerialNum))
                {
                    camera = FrameGrabberOperator.dicCameras[m_SerialNum];
                    Bv_Vulcan obj = (Bv_Vulcan)camera;
                    obj.UpdateImage = (Action<ImageData>)Delegate.Combine(obj.UpdateImage, new Action<ImageData>(UpdateUIImage));
                    ((Bv_Vulcan)camera).UpdateStartStopStatus += RefreshStartStopStatus;
                    if (((Bv_Vulcan)camera).ObjectCreated)
                    {
                        camera.SetParams(m_ParamValues);
                        btnOpenClose.Text = "Close Card";
                        btnSnap.Enabled = true;
                        btnStartGrab.Enabled = ((Bv_Vulcan)camera).StopGrabFlag;
                        btnStopGrab.Enabled = !((Bv_Vulcan)camera).StopGrabFlag;
                        ChangeControlEnable(flag: true);
                        Task.Run((Action)UpdateControlsValue);
                    }
                    else
                    {
                        btnOpenClose.Text = "Open Card";
                        btnSnap.Enabled = false;
                        btnStartGrab.Enabled = false;
                        btnStopGrab.Enabled = false;
                        ChangeControlEnable(flag: false);
                        m_init = true;
                    }
                }
                else
                {
                    m_init = true;
                }
            }
        });
        if (btnOpenClose.Text.Equals("Close Card") && enterFlag)
        {
            camera.SetParams(out_ParamValues);
        }
    }

    private void InitialControlStatus()
    {
        if (enterFlag)
        {
            listDevice.Enabled = false;
        }
        else
        {
            listDevice.Enabled = true;
        }
        btnAdd.Enabled = false;
        btnRemove.Enabled = false;
        btnSave.Enabled = false;
        cbSerial.Enabled = false;
        gbConfigFile.Enabled = false;
        gbControl.Enabled = false;
        gbBoard.Enabled = false;
        gbCamera.Enabled = false;
    }

    private void ChangeControlEnable(bool flag)
    {
        if (enterFlag)
        {
            btnAdd.Enabled = false;
            btnRemove.Enabled = false;
            gbLoc.Enabled = false;
            listDevice.Enabled = false;
            btnSave.Enabled = true;
        }
        else
        {
            btnAdd.Enabled = flag;
            if (listDevice.Items.Count > 0)
            {
                btnRemove.Enabled = true;
            }
            else
            {
                btnRemove.Enabled = false;
            }
            btnSave.Enabled = false;
            gbLoc.Enabled = !flag;
        }
        cbConfigfile.Enabled = !flag;
        tbConfigfile.Enabled = !flag;
        btnBrowse.Enabled = !flag;
        gbControl.Enabled = flag;
        gbBoard.Enabled = flag;
        gbCamera.Enabled = flag;
    }

    private void cbSerial_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (m_init)
        {
            m_SerialNum = cbSerial.SelectedItem.ToString();
            m_ParamValues = FrameGrabberOperator.FindDeviceConfigByVendorKey("IKap," + m_SerialNum);
            if (m_ParamValues != null)
            {
                m_ConfigFile = m_ParamValues["ConfigPath"].Value.ToString();
            }
            SetDirectories();
            UpdateNames();
            UpdateBoxAvailability();
        }
    }

    private void cbConfigfile_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_currentConfigFileIndex = cbConfigfile.SelectedIndex;
        m_currentConfigFileName = (string)vlcffiles[m_currentConfigFileIndex];
    }

    private void checkConfigFile_CheckedChanged(object sender, EventArgs e)
    {
        m_ConfigFileEnable = checkConfigFile.Checked;
        btnBrowse.Enabled = checkConfigFile.Checked;
        cbConfigfile.Enabled = checkConfigFile.Checked;
        tbConfigfile.Enabled = checkConfigFile.Checked;
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
        folderBrowserDialog1.Description = "请选择一个路径！";
        folderBrowserDialog1.SelectedPath = m_currentConfigDir;
        if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
        {
            if (!string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
            {
                UpdateCurrentDir(folderBrowserDialog1.SelectedPath);
            }
            cbConfigfile.Items.Clear();
            UpdateNames();
        }
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        if (btnOpenClose.Text.Equals("Open Card"))
        {
            if (tbConfigfile.Text.Length > 0)
            {
                m_ConfigFile = m_currentConfigDir + "\\" + m_currentConfigFileName;
            }
            else
            {
                m_ConfigFile = "";
            }
            if (Bv_Vulcan.dic_Cards.ContainsKey(m_SerialNum))
            {
                camera = Bv_Vulcan.dic_Cards[m_SerialNum];
            }
            else
            {
                camera = new Bv_Vulcan(m_SerialNum, m_ParamValues);
                Bv_Vulcan obj = (Bv_Vulcan)camera;
                obj.UpdateImage = (Action<ImageData>)Delegate.Combine(obj.UpdateImage, new Action<ImageData>(UpdateUIImage));
                ((Bv_Vulcan)camera).UpdateStartStopStatus += RefreshStartStopStatus;
            }
            ((Bv_Vulcan)camera).LoadConfig(m_ConfigFile);
            m_ParamValues = camera.ConfigDatas;
            m_ParamValues["CameraVendorName"].Value.mValue = cbCameraVendor.SelectedItem.ToString();
            m_ParamValues["ModelName"].Value.mValue = "IKap(" + cbSerial.SelectedItem.ToString() + ")";
            m_ParamValues["PortName"].Value.mValue = tbPortName.Text;
            if (!((Bv_Vulcan)camera).ObjectCreated)
            {
                if (camera.OpenDevice())
                {
                    btnOpenClose.Text = "Close Card";
                    btnSnap.Enabled = true;
                    btnStartGrab.Enabled = ((Bv_Vulcan)camera).StopGrabFlag;
                    btnStopGrab.Enabled = !((Bv_Vulcan)camera).StopGrabFlag;
                    ChangeControlEnable(flag: true);
                    Task.Run((Action)UpdateControlsValue);
                }
                else
                {
                    btnOpenClose.Text = "Open Card";
                    btnSnap.Enabled = false;
                    btnStartGrab.Enabled = false;
                    btnStopGrab.Enabled = false;
                    ChangeControlEnable(flag: false);
                }
            }
            else
            {
                btnOpenClose.Text = "Close Card";
                btnSnap.Enabled = true;
                btnStartGrab.Enabled = ((Bv_Vulcan)camera).StopGrabFlag;
                btnStopGrab.Enabled = !((Bv_Vulcan)camera).StopGrabFlag;
                ChangeControlEnable(flag: true);
                Task.Run((Action)UpdateControlsValue);
            }
            if (FrameGrabberOperator.dicCameras.ContainsKey(m_SerialNum))
            {
                FrameGrabberOperator.dicCameras[m_SerialNum] = Bv_Vulcan.dic_Cards[m_SerialNum];
            }
            else
            {
                FrameGrabberOperator.dicCameras.Add(m_SerialNum, Bv_Vulcan.dic_Cards[m_SerialNum]);
            }
        }
        else
        {
            if (camera != null)
            {
                Bv_Vulcan obj2 = (Bv_Vulcan)camera;
                obj2.UpdateImage = (Action<ImageData>)Delegate.Remove(obj2.UpdateImage, new Action<ImageData>(UpdateUIImage));
                ((Bv_Vulcan)camera).UpdateStartStopStatus -= RefreshStartStopStatus;
                camera.CloseDevice();
            }
            btnOpenClose.Text = "Open Card";
            gbControl.Enabled = false;
            ChangeControlEnable(flag: false);
            if (FrameGrabberOperator.dicCameras.ContainsKey(m_SerialNum))
            {
                FrameGrabberOperator.dicCameras.Remove(m_SerialNum);
            }
        }
    }

    private void UpdateNames()
    {
        vlcffiles.Clear();
        string currentDir = m_currentConfigDir;
        string keyName = ConfigKeyName;
        string curSerialNum = cbSerial.SelectedItem.ToString();
        DirectoryInfo dir = new DirectoryInfo(currentDir);
        if (dir.Exists)
        {
            FileInfo[] dcffileInfo = dir.GetFiles("*.vlcf");
            cbConfigfile.Items.Clear();
            FileInfo[] array = dcffileInfo;
            foreach (FileInfo f in array)
            {
                vlcffiles.Add(f.Name);
                string cameraDesc = f.Name;
                MyListBoxItem item = new MyListBoxItem(cameraDesc, ItemData: true);
                cbConfigfile.Items.Add(item);
            }
        }
        if (cbConfigfile.Items.Count != 0)
        {
            m_configFileAvailable = true;
            int newFileIndex = 0;
            for (int i = 0; i < vlcffiles.Count; i++)
            {
                string currentdcf = (string)vlcffiles[i];
                if (string.Compare(m_currentConfigFileName, currentdcf, StringComparison.Ordinal) == 0)
                {
                    newFileIndex = i;
                }
            }
            cbConfigfile.SelectedIndex = newFileIndex;
        }
        else
        {
            m_configFileAvailable = false;
        }
        UpdateBoxAvailability();
    }

    private void UpdateBoxAvailability()
    {
        MyListBoxItem item = (MyListBoxItem)cbSerial.SelectedItem;
        bool configFileRequired = item.ItemData;
        checkConfigFile.Enabled = !configFileRequired;
        if (configFileRequired)
        {
            m_ConfigFileEnable = configFileRequired;
            checkConfigFile.Checked = configFileRequired;
        }
        else
        {
            m_ConfigFileEnable = checkConfigFile.Checked;
        }
        cbConfigfile.Enabled = m_ConfigFileEnable && m_configFileAvailable;
        tbConfigfile.Enabled = m_ConfigFileEnable;
        btnBrowse.Enabled = m_ConfigFileEnable || checkConfigFile.Checked;
        btnOpenClose.Enabled = !m_ConfigFileEnable || m_configFileAvailable;
    }

    private void UpdateControlsValue()
    {
        if (base.InvokeRequired)
        {
            Invoke((MethodInvoker)delegate
            {
                nudDrop.Value = Convert.ToDecimal(m_ParamValues["Divider"].Value.mValue);
                cbMultiplier.SelectedIndex = Convert.ToInt32(m_ParamValues["Multiplier"].Value.mValue);
                int num = Convert.ToInt32(m_ParamValues["WorkMode"].Value.mValue);
                cbWorkMode.SelectedIndex = num - 1;
                nudTimeout.Value = Convert.ToDecimal(m_ParamValues["Timeout"].Value.mValue);
                tbPortName.Text = m_ParamValues["PortName"].Value.ToString();
                nudFrameCount.Value = Convert.ToDecimal(m_ParamValues["FrameCount"].Value.mValue);
                nudBufferFrameCount.Value = Convert.ToDecimal(m_ParamValues["BufferFrameCount"].Value.mValue);
                nudTapNum.Value = Convert.ToDecimal(m_ParamValues["TapNum"].Value.mValue);
                int selectedIndex = 0;
                for (int j = 0; j < cbCameraVendor.Items.Count; j++)
                {
                    if (cbCameraVendor.Items[j].ToString().Equals(m_ParamValues["CameraVendorName"].Value.ToString()))
                    {
                        selectedIndex = j;
                        break;
                    }
                }
                cbCameraVendor.SelectedIndex = selectedIndex;
                nudExposure.Value = Convert.ToDecimal(m_ParamValues["ExposureTime"].Value.mValue);
                nudGain.Value = Convert.ToDecimal(m_ParamValues["Gain"].Value.mValue);
                cbScanDirection.SelectedIndex = Convert.ToInt32(m_ParamValues["ScanDirection"].Value.mValue);
            });
        }
        else
        {
            nudDrop.Value = Convert.ToDecimal(m_ParamValues["Divider"].Value.mValue);
            cbMultiplier.SelectedIndex = Convert.ToInt32(m_ParamValues["Multiplier"].Value.mValue);
            int workMode = Convert.ToInt32(m_ParamValues["WorkMode"].Value.mValue);
            cbWorkMode.SelectedIndex = workMode - 1;
            nudTimeout.Value = Convert.ToDecimal(m_ParamValues["Timeout"].Value.mValue);
            tbPortName.Text = m_ParamValues["PortName"].Value.ToString();
            nudFrameCount.Value = Convert.ToDecimal(m_ParamValues["FrameCount"].Value.mValue);
            nudBufferFrameCount.Value = Convert.ToDecimal(m_ParamValues["BufferFrameCount"].Value.mValue);
            nudTapNum.Value = Convert.ToDecimal(m_ParamValues["TapNum"].Value.mValue);
            int index = 0;
            for (int i = 0; i < cbCameraVendor.Items.Count; i++)
            {
                if (cbCameraVendor.Items[i].ToString().Equals(m_ParamValues["CameraVendorName"].Value.ToString()))
                {
                    index = i;
                    break;
                }
            }
            cbCameraVendor.SelectedIndex = index;
            nudExposure.Value = Convert.ToDecimal(m_ParamValues["ExposureTime"].Value.mValue);
            nudGain.Value = Convert.ToDecimal(m_ParamValues["Gain"].Value.mValue);
            cbScanDirection.SelectedIndex = Convert.ToInt32(m_ParamValues["ScanDirection"].Value.mValue);
        }
        m_init = true;
    }

    private void LoadSettings(int index)
    {
        string KeyPath = $"Software\\BTW Vision\\IKap\\{Application.ProductName}\\Device{index}";
        RegistryKey RegKey = Registry.CurrentUser.OpenSubKey(KeyPath);
        if (RegKey != null)
        {
            m_SerialNum = RegKey.GetValue("SN", "").ToString();
            if (File.Exists(RegKey.GetValue("ConfigFile", "").ToString()))
            {
                m_ConfigFile = RegKey.GetValue("ConfigFile", "").ToString();
            }
        }
    }

    private void SaveSettings(int index)
    {
        string KeyPath = $"Software\\BTW Vision\\IKap\\{Application.ProductName}\\SapAcquisition{index}";
        RegistryKey RegKey = Registry.CurrentUser.CreateSubKey(KeyPath);
        RegKey.SetValue("SN", m_SerialNum);
        RegKey.SetValue("ConfigFile", m_ConfigFile);
    }

    private void UpdateCurrentDir(string newCurrentDir)
    {
        if (string.Compare(newCurrentDir, m_currentConfigDir, StringComparison.Ordinal) != 0)
        {
            tbConfigfile.Text = newCurrentDir;
            m_currentConfigDir = newCurrentDir;
        }
    }

    private void SetDirectories()
    {
        string productDirectory = "";
        if (!string.IsNullOrEmpty(m_ProductDir))
        {
            productDirectory = Environment.GetEnvironmentVariable(m_ProductDir);
        }
        string saperaDir = Environment.CurrentDirectory;
        if (m_ConfigFile.Length != 0)
        {
            m_currentConfigDir = Path.GetDirectoryName(m_ConfigFile);
            m_currentConfigFileName = Path.GetFileName(m_ConfigFile);
            tbConfigfile.Text = m_currentConfigDir;
            return;
        }
        m_currentConfigFileName = "";
        if (!string.IsNullOrEmpty(productDirectory))
        {
            m_currentConfigDir = productDirectory;
        }
        else if (saperaDir.Length != 0)
        {
            m_currentConfigDir = saperaDir + Acquisition_Default_folder;
        }
        tbConfigfile.Text = m_currentConfigDir;
    }

    private void btnSnap_Click(object sender, EventArgs e)
    {
        ((IAcquisition2DLineScan3D)camera).Snap();
    }

    private void RefreshStartStopStatus(bool status)
    {
        if (base.InvokeRequired)
        {
            Invoke((MethodInvoker)delegate
            {
                btnStartGrab.Enabled = status;
                btnStopGrab.Enabled = !status;
            });
        }
        else
        {
            btnStartGrab.Enabled = status;
            btnStopGrab.Enabled = !status;
        }
    }

    private void btnStartGrab_Click(object sender, EventArgs e)
    {
        ((IAcquisition2DLineScan3D)camera).StartGrab();
        btnStartGrab.Enabled = false;
        btnStopGrab.Enabled = true;
    }

    private void btnStopGrab_Click(object sender, EventArgs e)
    {
        ((IAcquisition2DLineScan3D)camera).StopGrab();
        btnStartGrab.Enabled = true;
        btnStopGrab.Enabled = false;
    }

    public void UpdateUIImage(ImageData imageData)
    {
        if (imageDisplay1.InvokeRequired)
        {
            imageDisplay1.Invoke(new Action<ImageData>(UpdateUIImage), imageData);
        }
        else
        {
            imageDisplay1.CogImage = imageData.CogImage;
        }
    }

    private void nud_ValueChanged(object sender, EventArgs e)
    {
        if (m_init && camera != null)
        {
            NumericUpDown nud = sender as NumericUpDown;
            switch (nud.Name)
            {
                case "nudDrop":
                    m_ParamValues["Divider"] = new ParamElement
                    {
                        Name = "Divider",
                        Type = "Int32",
                        Value = new XmlObject
                        {
                            mValue = Convert.ToInt32(nud.Value)
                        }
                    };
                    ((Bv_Vulcan)camera).GetCameraParam();
                    nud.Value = Convert.ToDecimal(m_ParamValues["Divider"].Value.mValue);
                    nudBufferFrameCount.Value = Convert.ToDecimal(m_ParamValues["BufferFrameCount"].Value.mValue);
                    nudExposure.Value = Convert.ToDecimal(m_ParamValues["ExposureTime"].Value.ToString());
                    nudGain.Value = Convert.ToDecimal(m_ParamValues["Gain"].Value.ToString());
                    cbScanDirection.SelectedIndex = Convert.ToInt32(m_ParamValues["ScanDirection"].Value.ToString());
                    break;
                case "nudBufferFrameCount":
                    m_ParamValues.SetElementValue("BufferFrameCount", Convert.ToInt32(nud.Value));
                    break;
                case "nudTimeout":
                    m_ParamValues["Timeout"] = new ParamElement
                    {
                        Name = "Timeout",
                        Type = "Int32",
                        Value = new XmlObject
                        {
                            mValue = Convert.ToInt32(nud.Value)
                        }
                    };
                    break;
                case "nudExposure":
                    m_ParamValues["ExposureTime"] = new ParamElement
                    {
                        Name = "ExposureTime",
                        Type = "Int32",
                        Value = new XmlObject
                        {
                            mValue = Convert.ToInt32(nud.Value)
                        }
                    };
                    ((Bv_Vulcan)camera).GetCameraParam();
                    nud.Value = Convert.ToDecimal(m_ParamValues["ExposureTime"].Value.ToString());
                    nudDrop.Value = Convert.ToDecimal(m_ParamValues["Divider"].Value.ToString());
                    nudBufferFrameCount.Value = Convert.ToDecimal(m_ParamValues["BufferFrameCount"].Value.ToString());
                    nudGain.Value = Convert.ToDecimal(m_ParamValues["Gain"].Value.ToString());
                    cbScanDirection.SelectedIndex = Convert.ToInt32(m_ParamValues["ScanDirection"].Value.ToString());
                    break;
                case "nudGain":
                    m_ParamValues["Gain"] = new ParamElement
                    {
                        Name = "Gain",
                        Type = "Single",
                        Value = new XmlObject
                        {
                            mValue = Convert.ToSingle(nud.Value)
                        }
                    };
                    ((Bv_Vulcan)camera).GetCameraParam();
                    nud.Value = Convert.ToDecimal(m_ParamValues["Gain"].Value.ToString());
                    nudDrop.Value = Convert.ToDecimal(m_ParamValues["Divider"].Value.ToString());
                    nudBufferFrameCount.Value = Convert.ToDecimal(m_ParamValues["BufferFrameCount"].Value.ToString());
                    nudExposure.Value = Convert.ToDecimal(m_ParamValues["ExposureTime"].Value.ToString());
                    cbScanDirection.SelectedIndex = Convert.ToInt32(m_ParamValues["ScanDirection"].Value.ToString());
                    break;
                case "nudFrameCount":
                    m_ParamValues.SetElementValue("FrameCount", Convert.ToInt32(nud.Value));
                    break;
                case "nudTapNum":
                    m_ParamValues["TapNum"] = new ParamElement
                    {
                        Name = "TapNum",
                        Type = "Int32",
                        Value = new XmlObject
                        {
                            mValue = Convert.ToInt32(nud.Value)
                        }
                    };
                    ((Bv_Vulcan)camera).GetCameraParam();
                    nud.Value = Convert.ToDecimal(m_ParamValues["TapNum"].Value.ToString());
                    nudExposure.Value = Convert.ToDecimal(m_ParamValues["ExposureTime"].Value.ToString());
                    nudDrop.Value = Convert.ToDecimal(m_ParamValues["Divider"].Value.ToString());
                    nudBufferFrameCount.Value = Convert.ToDecimal(m_ParamValues["BufferFrameCount"].Value.ToString());
                    nudGain.Value = Convert.ToDecimal(m_ParamValues["Gain"].Value.ToString());
                    cbScanDirection.SelectedIndex = Convert.ToInt32(m_ParamValues["ScanDirection"].Value.ToString());
                    break;
            }
        }
    }

    private void cbWorkMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (m_init && m_ParamValues != null)
        {
            m_ParamValues["WorkMode"] = new ParamElement
            {
                Name = "WorkMode",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = cbWorkMode.SelectedIndex + 1
                }
            };
        }
        if (cbWorkMode.SelectedIndex == 5)
        {
            label14.Visible = true;
            nudFrameCount.Visible = true;
        }
        else
        {
            label14.Visible = false;
            nudFrameCount.Visible = false;
        }
    }

    private void tbPortName_TextChanged(object sender, EventArgs e)
    {
        if (m_init && m_ParamValues != null)
        {
            m_ParamValues["PortName"] = new ParamElement
            {
                Name = "PortName",
                Type = "String",
                Value = new XmlObject
                {
                    mValue = tbPortName.Text
                }
            };
        }
    }

    private void cbCameraVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (m_init && m_ParamValues != null)
        {
            m_ParamValues["CameraVendorName"] = new ParamElement
            {
                Name = "CameraVendorName",
                Type = "String",
                Value = new XmlObject
                {
                    mValue = cbCameraVendor.SelectedItem.ToString()
                }
            };
        }
    }

    private void cbMultiplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (m_init && m_ParamValues != null)
        {
            m_ParamValues["Multiplier"] = new ParamElement
            {
                Name = "Multiplier",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = cbMultiplier.SelectedIndex
                }
            };
        }
    }

    private void cbScanDirection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (m_init && m_ParamValues != null)
        {
            m_ParamValues["ScanDirection"] = new ParamElement
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

    private void btnAdd_Click(object sender, EventArgs e)
    {
        if (camera != null)
        {
            FrameGrabberOperator.AddDevice("IKap", m_ParamValues);
            FrameGrabberOperator.SerializeParamObjectToXml(FrameGrabberOperator.ConfigFilePath);
            if (!listDevice.Items.Contains(m_ParamValues.VendorNameKey))
            {
                listDevice.Items.Add(m_ParamValues.VendorNameKey);
            }
        }
    }

    private void btnBackToMenu_Click(object sender, EventArgs e)
    {
        tabControl1.SelectedTab = tpMain;
    }

    private void btnConfig_Click(object sender, EventArgs e)
    {
        tabControl1.SelectedTab = tpConfig;
    }

    private void btnBoard_Click(object sender, EventArgs e)
    {
        tabControl1.SelectedTab = tpBoardParam;
    }

    private void btnCamera_Click(object sender, EventArgs e)
    {
        tabControl1.SelectedTab = tpCameraParam;
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
        if (listDevice.SelectedItem != null)
        {
            object selectObject = listDevice.SelectedItem;
            if (FrameGrabberOperator.RemoveDeviceByVendorSerial(selectObject.ToString()))
            {
                listDevice.Items.Remove(selectObject);
            }
        }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        base.DialogResult = DialogResult.OK;
        try
        {
            for (int i = 0; i < out_ParamValues.CameraOrGrabberParams.Count; i++)
            {
                out_ParamValues[i] = m_ParamValues[i];
            }
            MessageBox.Show(@"保存参数成功！");
        }
        catch (Exception ex)
        {
            MessageBox.Show("保存参数失败！异常信息：" + ex.Message);
        }
        Close();
    }

    private void FrmFrameGrabberSetting_FormClosing(object sender, FormClosingEventArgs e)
    {
        m_init = false;
        if (camera != null)
        {
            Bv_Camera bv_Camera = camera;
            bv_Camera.UpdateImage = (Action<ImageData>)Delegate.Remove(bv_Camera.UpdateImage, new Action<ImageData>(UpdateUIImage));
            ((Bv_Vulcan)camera).UpdateStartStopStatus -= RefreshStartStopStatus;
        }
    }
}