using System.Windows.Forms;
using Cognex.VisionPro;

namespace NovaVision.UserControlLibrary
{
    public partial class MaxDisplay : Form
    {
        private CogRecordDisplay mRecordDisplay;
        public MaxDisplay(CogRecordDisplay recordDisplay, string name)
        {
            InitializeComponent();
            mRecordDisplay = recordDisplay;
            base.MinimizeBox = false;
            Text = name;
            //base.WindowState = FormWindowState.Maximized;
        }

        private void MaxDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (this.ReturnDispaly != null)
            //{
            //    CogRecordDisplay recordDisplay = mRecordDisplay;
            //    base.Controls.Remove(recordDisplay);
            //    this.ReturnDispaly(recordDisplay);
            //}
        }

        private void MaxDisplay_Shown(object sender, System.EventArgs e)
        {
            if (mRecordDisplay.Record != null)
            {
                this.cogRecordDisplay.Record = mRecordDisplay.Record;
                this.cogRecordDisplay.Fit();
            }
        }
    }
}
