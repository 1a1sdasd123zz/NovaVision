using System;
using System.ComponentModel;
using System.Windows.Forms;
using Cognex.VisionPro;

namespace NovaVision.UserControlLibrary
{
    [Description("视图窗体")]
    public partial class ImageDisplay : UserControl
    {
        private Cognex.VisionPro.ICogRecord _cogRecord;

        private Cognex.VisionPro.ICogImage _cogImage;

        private string _displayName;

        public Cognex.VisionPro.ICogImage CogImage
        {
            get
            {
                _cogImage = RecordDisplay.Image;
                return _cogImage;
            }
            set
            {
                RecordDisplay.StaticGraphics.Clear();
                RecordDisplay.InteractiveGraphics.Clear();
                _cogImage = value;
                RecordDisplay.Image = _cogImage;
                RecordDisplay.AutoFit = true;
            }
        }

        public Cognex.VisionPro.ICogRecord Record
        {
            get
            {
                _cogRecord = RecordDisplay.Record;
                return _cogRecord;
            }
            set
            {
                RecordDisplay.StaticGraphics.Clear();
                RecordDisplay.InteractiveGraphics.Clear();
                _cogRecord = value;
                RecordDisplay.Record = _cogRecord;
                RecordDisplay.AutoFit = true;
            }
        }

        public CogRecordDisplay RecordDisplay => cogRecordDisplay;
        public string DisplayName
        {
            get
            {
                _displayName = grb_ShowName.Text;
                return _displayName;
            }
            set
            {
                _displayName = value;
                grb_ShowName.Text = _displayName;
            }
        }

        public ImageDisplay()
        {
            InitializeComponent();
        }

        public ImageDisplay(string name)
        {
            InitializeComponent();
            grb_ShowName.Text = name;
        }

        private void cogRecordDisplay_DoubleClick(object sender, EventArgs e)
        {
            CogRecordDisplay recordDisplay = sender as CogRecordDisplay;
            MaxDisplay maxDisplay = new MaxDisplay(recordDisplay, DisplayName);
            maxDisplay.ShowDialog();
        }

        private void ImageDisplay_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
        }
    }
}
