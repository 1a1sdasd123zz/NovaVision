using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NovaVision.BaseClass.Helper;
using NovaVision.Hardware;
using NovaVision.Hardware.Frame_Grabber_CameraLink_._05_HIK_CL;

namespace NovaVision.VisionForm.CarameFrm;

public partial class FrmHikFrameGrabber : Form
{
    private Bv_Camera camera;

    private string m_SerialCamera = string.Empty;

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



    public FrmHikFrameGrabber(FrameGrabberConfigData paramValues)
        : this(paramValues, flag: false)
    {
    }

    public FrmHikFrameGrabber(FrameGrabberConfigData paramValues, bool flag)
    {
        InitializeComponent();
        enterFlag = flag;
        out_ParamValues = paramValues;
        m_ParamValues = paramValues;
        if (enterFlag)
        {
            m_SerialCamera = m_ParamValues["Serial"].Value.ToString();
        }
    }

    private void FrmFrameGrabberSetting_Load(object sender, EventArgs e)
    {
        cbCameraSerial.Items.Clear();
        listDevice.Items.Clear();
        InitialControlStatus();
        tsslStatus.Text = "正在枚举HikCL卡设备";
        if (FrameGrabberOperator.hardwareDeployments[18].state != 262144)
        {
            MessageBox.Show(@"请到硬件部署界面勾选对应相机");
            Close();
            return;
        }
        tsslStatus.BackColor = Color.Orange;
        DateTime now = DateTime.Now;
        TimeSpan timeSpan = default(TimeSpan);
        if (!enterFlag)
        {
            HikCL.EnumCards();
        }
        listDevice.Invoke((MethodInvoker)delegate
        {
            if (FrameGrabberOperator.dicVendorNameKey.ContainsKey("HikCL"))
            {
                ListBox.ObjectCollection items3 = listDevice.Items;
                object[] items4 = FrameGrabberOperator.dicVendorNameKey["HikCL"].ToArray();
                items3.AddRange(items4);
            }
            if (listDevice.Items.Count > 0 && !enterFlag)
            {
                btnRemove.Enabled = true;
            }
        });
        cbCameraSerial.Invoke((MethodInvoker)delegate
        {
            ComboBox.ObjectCollection items = cbCameraSerial.Items;
            object[] items2 = HikCL.cameraSN.Select((string o) => new MyListBoxItem(o.ToString())).ToArray();
            items.AddRange(items2);
            if (cbCameraSerial.Items.Count > 0)
            {
                if (enterFlag)
                {
                    foreach (object current in cbCameraSerial.Items)
                    {
                        if (m_SerialCamera.Equals(current.ToString()))
                        {
                            cbCameraSerial.SelectedItem = current;
                            break;
                        }
                    }
                }
                else
                {
                    cbCameraSerial.SelectedIndex = 0;
                }
                m_SerialCamera = cbCameraSerial.SelectedItem.ToString();
                if (!enterFlag)
                {
                    m_ParamValues = FrameGrabberOperator.FindDeviceConfigByVendorKey("HikCL," + m_SerialCamera);
                }
                timeSpan = DateTime.Now - now;
                tsslStatus.Text = $"查找到{cbCameraSerial.Items.Count}个相机";
                tsslStatus.BackColor = Color.Green;
                if (enterFlag)
                {
                    cbCameraSerial.Enabled = false;
                }
                else
                {
                    cbCameraSerial.Enabled = true;
                }
                btnOpenClose.Enabled = true;
                tsslElapsedTime.Text = $"{Math.Round(timeSpan.TotalMilliseconds, 2)}ms";
            }
            else
            {
                timeSpan = DateTime.Now - now;
                tsslStatus.Text = "未找到HikCL相机";
                tsslStatus.BackColor = Color.Red;
                tsslElapsedTime.Text = $"{Math.Round(timeSpan.TotalMilliseconds, 2)}ms";
            }
        });
        btnOpenClose.Invoke((MethodInvoker)delegate
        {
            if (cbCameraSerial.SelectedItem != null)
            {
                if (FrameGrabberOperator.dicCameras.ContainsKey(m_SerialCamera))
                {
                    camera = FrameGrabberOperator.dicCameras[m_SerialCamera];
                    HikCL obj = (HikCL)camera;
                    obj.UpdateImage = (Action<ImageData>)Delegate.Combine(obj.UpdateImage, new Action<ImageData>(UpdateUIImage));
                    ((HikCL)camera).UpdateStartStopStatus += RefreshStartStopStatus;
                    m_ParamValues = camera.ConfigDatas;
                    if (((HikCL)camera).ObjectCreated)
                    {
                        camera.SetParams(m_ParamValues);
                        btnOpenClose.Text = "Close Card";
                        btnSnap.Enabled = true;
                        btnStartGrab.Enabled = ((HikCL)camera).StopGrabFlag;
                        btnStopGrab.Enabled = !((HikCL)camera).StopGrabFlag;
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
        cbCameraSerial.Enabled = false;
        btnOpenClose.Enabled = false;
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
        gbControl.Enabled = flag;
        gbBoard.Enabled = flag;
        gbCamera.Enabled = flag;
    }

    private void cbCameraSerial_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_SerialCamera = cbCameraSerial.SelectedItem.ToString();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        if (m_SerialCamera == "" || m_SerialCamera == null)
        {
            MessageBox.Show(@"请选择相机");
        }
        else if (btnOpenClose.Text.Equals("Open Card"))
        {
            if (HikCL.devicesOpened.ContainsKey(m_SerialCamera))
            {
                camera = HikCL.devicesOpened[m_SerialCamera];
            }
            else
            {
                camera = new HikCL(m_SerialCamera, m_ParamValues);
            }
            HikCL obj = (HikCL)camera;
            obj.UpdateImage = (Action<ImageData>)Delegate.Combine(obj.UpdateImage, new Action<ImageData>(UpdateUIImage));
            ((HikCL)camera).UpdateStartStopStatus += RefreshStartStopStatus;
            m_ParamValues = camera.ConfigDatas;
            if (!((HikCL)camera).ObjectCreated)
            {
                if (camera.OpenDevice())
                {
                    btnOpenClose.Text = "Close Card";
                    btnSnap.Enabled = true;
                    btnStartGrab.Enabled = ((HikCL)camera).StopGrabFlag;
                    btnStopGrab.Enabled = !((HikCL)camera).StopGrabFlag;
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
                btnStartGrab.Enabled = ((HikCL)camera).StopGrabFlag;
                btnStopGrab.Enabled = !((HikCL)camera).StopGrabFlag;
                ChangeControlEnable(flag: true);
                Task.Run((Action)UpdateControlsValue);
            }
            if (FrameGrabberOperator.dicCameras.ContainsKey(m_SerialCamera))
            {
                FrameGrabberOperator.dicCameras[m_SerialCamera] = HikCL.devicesOpened[m_SerialCamera];
            }
            else
            {
                FrameGrabberOperator.dicCameras.Add(m_SerialCamera, HikCL.devicesOpened[m_SerialCamera]);
            }
        }
        else
        {
            if (camera != null)
            {
                HikCL obj2 = (HikCL)camera;
                obj2.UpdateImage = (Action<ImageData>)Delegate.Remove(obj2.UpdateImage, new Action<ImageData>(UpdateUIImage));
                ((HikCL)camera).UpdateStartStopStatus -= RefreshStartStopStatus;
                camera.CloseDevice();
            }
            btnOpenClose.Text = "Open Card";
            gbControl.Enabled = false;
            ChangeControlEnable(flag: false);
            if (FrameGrabberOperator.dicCameras.ContainsKey(m_SerialCamera))
            {
                FrameGrabberOperator.dicCameras.Remove(m_SerialCamera);
            }
        }
    }

    private void UpdateControlsValue()
    {
        if (base.InvokeRequired)
        {
            Invoke((MethodInvoker)delegate
            {
                int num = Convert.ToInt32(m_ParamValues["WorkMode"].Value.mValue);
                cbWorkMode.SelectedIndex = num - 1;
                nudTimeout.Value = Convert.ToDecimal(m_ParamValues["Timeout"].Value.mValue);
                nudExposure.Value = Convert.ToDecimal(m_ParamValues["ExposureTime"].Value.mValue);
                nudGain.Value = Convert.ToDecimal(m_ParamValues["Gain"].Value.mValue);
                cbScanDirection.SelectedIndex = Convert.ToInt32(m_ParamValues["ScanDirection"].Value.mValue);
                nudScanWidth.Value = Convert.ToDecimal(m_ParamValues["ScanWidth"].Value.mValue);
                nudScanHeight.Value = Convert.ToDecimal(m_ParamValues["ScanHeight"].Value.mValue);
            });
        }
        else
        {
            int workMode = Convert.ToInt32(m_ParamValues["WorkMode"].Value.mValue);
            cbWorkMode.SelectedIndex = workMode - 1;
            nudTimeout.Value = Convert.ToDecimal(m_ParamValues["Timeout"].Value.mValue);
            nudExposure.Value = Convert.ToDecimal(m_ParamValues["ExposureTime"].Value.mValue);
            nudGain.Value = Convert.ToDecimal(m_ParamValues["Gain"].Value.mValue);
            cbScanDirection.SelectedIndex = Convert.ToInt32(m_ParamValues["ScanDirection"].Value.mValue);
            nudScanWidth.Value = Convert.ToDecimal(m_ParamValues["ScanWidth"].Value.mValue);
            nudScanHeight.Value = Convert.ToDecimal(m_ParamValues["ScanHeight"].Value.mValue);
        }
        m_init = true;
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
        cbWorkMode.Enabled = false;
        cbScanDirection.Enabled = false;
        nudTimeout.Enabled = false;
        nudExposure.Enabled = false;
        nudGain.Enabled = false;
        nudScanHeight.Enabled = false;
        nudScanWidth.Enabled = false;
    }

    private void btnStopGrab_Click(object sender, EventArgs e)
    {
        ((IAcquisition2DLineScan3D)camera).StopGrab();
        btnStartGrab.Enabled = true;
        btnStopGrab.Enabled = false;
        cbWorkMode.Enabled = true;
        cbScanDirection.Enabled = true;
        nudTimeout.Enabled = true;
        nudExposure.Enabled = true;
        nudGain.Enabled = true;
        nudScanHeight.Enabled = true;
        nudScanWidth.Enabled = true;
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
                    ((HikCL)camera).GetCameraParam();
                    nud.Value = Convert.ToDecimal(m_ParamValues["ExposureTime"].Value.ToString());
                    nudGain.Value = Convert.ToDecimal(m_ParamValues["Gain"].Value.ToString());
                    cbScanDirection.SelectedIndex = Convert.ToInt32(m_ParamValues["ScanDirection"].Value.ToString());
                    nudScanWidth.Value = Convert.ToDecimal(m_ParamValues["ScanWidth"].Value.mValue);
                    nudScanHeight.Value = Convert.ToDecimal(m_ParamValues["ScanHeight"].Value.mValue);
                    cbWorkMode.SelectedIndex = Convert.ToInt32(m_ParamValues["WorkMode"].Value.mValue) - 1;
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
                    ((HikCL)camera).GetCameraParam();
                    nud.Value = Convert.ToDecimal(m_ParamValues["Gain"].Value.ToString());
                    nudExposure.Value = Convert.ToDecimal(m_ParamValues["ExposureTime"].Value.ToString());
                    cbScanDirection.SelectedIndex = Convert.ToInt32(m_ParamValues["ScanDirection"].Value.ToString());
                    nudScanWidth.Value = Convert.ToDecimal(m_ParamValues["ScanWidth"].Value.mValue);
                    nudScanHeight.Value = Convert.ToDecimal(m_ParamValues["ScanHeight"].Value.mValue);
                    cbWorkMode.SelectedIndex = Convert.ToInt32(m_ParamValues["WorkMode"].Value.mValue) - 1;
                    break;
                case "nudScanWidth":
                    m_ParamValues["ScanWidth"] = new ParamElement
                    {
                        Name = "ScanWidth",
                        Type = "Int32",
                        Value = new XmlObject
                        {
                            mValue = Convert.ToInt32(nud.Value)
                        }
                    };
                    ((HikCL)camera).GetCameraParam();
                    nud.Value = Convert.ToDecimal(m_ParamValues["ScanWidth"].Value.ToString());
                    nudGain.Value = Convert.ToDecimal(m_ParamValues["Gain"].Value.ToString());
                    nudExposure.Value = Convert.ToDecimal(m_ParamValues["ExposureTime"].Value.ToString());
                    cbScanDirection.SelectedIndex = Convert.ToInt32(m_ParamValues["ScanDirection"].Value.ToString());
                    nudScanHeight.Value = Convert.ToDecimal(m_ParamValues["ScanHeight"].Value.mValue);
                    cbWorkMode.SelectedIndex = Convert.ToInt32(m_ParamValues["WorkMode"].Value.mValue) - 1;
                    break;
                case "nudScanHeight":
                    m_ParamValues["ScanHeight"] = new ParamElement
                    {
                        Name = "ScanHeight",
                        Type = "Int32",
                        Value = new XmlObject
                        {
                            mValue = Convert.ToInt32(nud.Value)
                        }
                    };
                    ((HikCL)camera).GetCameraParam();
                    nud.Value = Convert.ToDecimal(m_ParamValues["ScanHeight"].Value.ToString());
                    nudGain.Value = Convert.ToDecimal(m_ParamValues["Gain"].Value.ToString());
                    nudExposure.Value = Convert.ToDecimal(m_ParamValues["ExposureTime"].Value.ToString());
                    cbScanDirection.SelectedIndex = Convert.ToInt32(m_ParamValues["ScanDirection"].Value.ToString());
                    nudScanWidth.Value = Convert.ToDecimal(m_ParamValues["ScanWidth"].Value.mValue);
                    cbWorkMode.SelectedIndex = Convert.ToInt32(m_ParamValues["WorkMode"].Value.mValue) - 1;
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
            FrameGrabberOperator.AddDevice("HikCL", m_ParamValues);
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
            ((HikCL)camera).UpdateStartStopStatus -= RefreshStartStopStatus;
        }
    }

    private void radioButton1_CheckedChanged(object sender, EventArgs e)
    {
        if (radioButton1.Checked)
        {
            listBox1.Visible = true;
            button1.Visible = true;
            button5.Visible = true;
        }
        else
        {
            listBox1.Visible = false;
            button1.Visible = false;
            button5.Visible = false;
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (listBox1.Items.Count > 0 && listBox1.SelectedIndex >= 0)
        {
            if (HikCL.Save(listBox1.SelectedItem.ToString()))
            {
                MessageBox.Show(@"保存成功");
            }
            else
            {
                MessageBox.Show(@"保存失败");
            }
        }
        else
        {
            MessageBox.Show(@"未选中采集卡");
        }
    }

    private void button5_Click(object sender, EventArgs e)
    {
        listBox1.Items.Clear();
        HikCL.EnumInterface(ReadXml: false);
        if (HikCL.cardSN.Count > 0)
        {
            MessageBox.Show(@"枚举成功");
        }
        else
        {
            MessageBox.Show(@"枚举失败");
        }
        ListBox.ObjectCollection items = listBox1.Items;
        object[] items2 = HikCL.cardSN.ToArray();
        items.AddRange(items2);
    }
}