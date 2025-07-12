using System;
using System.Windows.Forms;

namespace NovaVision.VisionForm.CommunicationFrm
{
    public partial class ConfigNameForm : Form
    {
        public bool Flag = false;
        public ConfigNameForm()
        {
            this.InitializeComponent();
        }

        private void btn_Yes_Click(object sender, EventArgs e)
        {
            this.Flag = true;
            bool flag = this.txt_Name.Text == "";
            if (flag)
            {
                MessageBox.Show(@"请输入配置名：");
            }
            else
            {
                this.Name = this.txt_Name.Text.Trim();
                base.Close();
            }
        }

        private void btn_No_Click(object sender, EventArgs e)
        {
            this.Flag = false;
            base.Close();
        }
    }
}
