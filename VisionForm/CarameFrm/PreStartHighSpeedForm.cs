using System;
using System.ComponentModel;
using System.Windows.Forms;
using KeyenceLib;

namespace NovaVision.VisionForm.CarameFrm;

public partial class PreStartHighSpeedForm : Form
{
    private LJX8IF_HIGH_SPEED_PRE_START_REQUEST _request;


    public LJX8IF_HIGH_SPEED_PRE_START_REQUEST Request => _request;

    protected override void OnClosing(CancelEventArgs e)
    {
        if (base.DialogResult == DialogResult.OK)
        {
            try
            {
                _request.bySendPosition = Convert.ToByte(_textBoxSendPos.Text);
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

    public PreStartHighSpeedForm()
    {
        InitializeComponent();
        _request = default(LJX8IF_HIGH_SPEED_PRE_START_REQUEST);
    }
}