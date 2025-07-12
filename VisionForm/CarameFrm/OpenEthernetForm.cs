using System;
using System.ComponentModel;
using System.Windows.Forms;
using KeyenceLib;

namespace NovaVision.VisionForm.CarameFrm;

public partial class OpenEthernetForm : Form
{
    private LJX8IF_ETHERNET_CONFIG _ethernetConfig;

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

    protected override void OnClosing(CancelEventArgs e)
    {
        if (base.DialogResult == DialogResult.OK)
        {
            try
            {
                _ethernetConfig.abyIpAddress = Utility.GetIpAddressFromTextBox(_textBoxIpFirstSegment, _textBoxIpSecondSegment, _textBoxIpThirdSegment, _textBoxIpFourthSegment);
                _ethernetConfig.wPortNo = Convert.ToUInt16(_textBoxPort.Text);
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

    public OpenEthernetForm()
    {
        InitializeComponent();
        _ethernetConfig = default(LJX8IF_ETHERNET_CONFIG);
    }
}