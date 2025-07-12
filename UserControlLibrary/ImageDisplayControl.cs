using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace NovaVision.UserControlLibrary
{
    [Description("图像显示窗体设置")]
    public partial class ImageDisplayControl : UserControl
    {
        public event EventHandler BtnSaveClick;


        public ImageDisplayControl()
        {
            InitializeComponent();
        }

        private void btnSaveDC_Click(object sender, EventArgs e)
        {
            if (this.BtnSaveClick != null)
            {
                this.BtnSaveClick(this, e);
            }
        }
    }
}
