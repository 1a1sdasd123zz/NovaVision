using System;
using NovaVision.BaseClass;
using WeifenLuo.WinFormsUI.Docking;

namespace NovaVision.VisionForm.MainForm
{
    public partial class Frm_Error : DockContent
    {
        private static AppConfig mAppConfig;

        public static Frm_Error frm_Error;

        private static Frm_Main frm_Main;

        private Frm_Error()
        {
            InitializeComponent();
        }

        public static Frm_Error GetInstance(Frm_Main frm_main, AppConfig appConfig)
        {
            mAppConfig = appConfig;
            frm_Main = frm_main;
            if (frm_Error == null)
            {
                frm_Error = new Frm_Error();
            }
            return frm_Error;
        }

        private void Frm_Error_DockStateChanged(object sender, EventArgs e)
        {
            if (frm_Error == null)
            {
                return;
            }
            if (base.DockState == DockState.Unknown || base.DockState == DockState.Hidden)
            {
                if (base.DockState == DockState.Hidden)
                {
                    frm_Main.tsmi_Error.Checked = false;
                }
            }
            else
            {
                mAppConfig.DS_Frm_Error = base.DockState;
            }
        }

        private void tsmi_Output_Click(object sender, EventArgs e)
        {
        }

        private void Frm_Error_Load(object sender, EventArgs e)
        {
        }
    }
}
