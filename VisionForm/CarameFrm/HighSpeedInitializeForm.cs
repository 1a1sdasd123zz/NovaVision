using System;
using System.ComponentModel;
using System.Windows.Forms;
using KeyenceLib;

namespace NovaVision.VisionForm.CarameFrm;

public partial class HighSpeedInitializeForm : Form
{
    private LJX8IF_ETHERNET_CONFIG _ethernetConfig;

    private ushort _highSpeedPortNo;

    private uint _profileCount;


    public LJX8IF_ETHERNET_CONFIG EthernetConfig
    {
        get
        {
            return _ethernetConfig;
        }
        set
        {
            _ethernetConfig = value;
            if (_ethernetConfig.abyIpAddress != null)
            {
                Utility.UpdateTextFromEthernetSetting(_ethernetConfig, _textBoxIpFirstSegment, _textBoxIpSecondSegment, _textBoxIpThirdSegment, _textBoxIpFourthSegment);
            }
            if (_ethernetConfig.wPortNo != 0)
            {
                _textBoxPort.Text = _ethernetConfig.wPortNo.ToString();
            }
        }
    }

    public ushort HighSpeedPortNo
    {
        get
        {
            return _highSpeedPortNo;
        }
        set
        {
            _highSpeedPortNo = value;
            if (_highSpeedPortNo != 0)
            {
                _textBoxHighSpeedPortNo.Text = _highSpeedPortNo.ToString();
            }
        }
    }

    public uint ProfileCount
    {
        get
        {
            return _profileCount;
        }
        set
        {
            _profileCount = value;
            if (_profileCount != 0)
            {
                _textBoxProfileCnt.Text = _profileCount.ToString();
            }
        }
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        if (base.DialogResult == DialogResult.OK)
        {
            try
            {
                _ethernetConfig.abyIpAddress = Utility.GetIpAddressFromTextBox(_textBoxIpFirstSegment, _textBoxIpSecondSegment, _textBoxIpThirdSegment, _textBoxIpFourthSegment);
                _ethernetConfig.wPortNo = Convert.ToUInt16(_textBoxPort.Text);
                _highSpeedPortNo = Convert.ToUInt16(_textBoxHighSpeedPortNo.Text);
                _profileCount = Convert.ToUInt32(_textBoxProfileCnt.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                e.Cancel = true;
                return;
            }
            base.OnClosing(e);
        }
    }

    public HighSpeedInitializeForm()
    {
        InitializeComponent();
        _ethernetConfig = default(LJX8IF_ETHERNET_CONFIG);
        _highSpeedPortNo = 0;
        _profileCount = 0u;
    }
}