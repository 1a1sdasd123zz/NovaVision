using System;
using NovaVision.BaseClass;
using WeifenLuo.WinFormsUI.Docking;

namespace NovaVision.VisionForm.MainForm
{
    public partial class Frm_Log : DockContent
    {
        private static AppConfig mAppConfig;

        private static Frm_Log frm_Log;

        private static Frm_Main frm_Main;

        private Frm_Log()
        {
            InitializeComponent();
        }

        public static Frm_Log GetInstance(Frm_Main frm_main, AppConfig appConfig)
        {
            mAppConfig = appConfig;
            frm_Main = frm_main;
            if (frm_Log == null)
            {
                frm_Log = new Frm_Log();
            }
            return frm_Log;
        }

        private void Frm_Log_DockStateChanged(object sender, EventArgs e)
        {
            if (frm_Log == null)
            {
                return;
            }
            if (base.DockState == DockState.Unknown || base.DockState == DockState.Hidden)
            {
                if (base.DockState == DockState.Hidden)
                {
                    frm_Main.tsmi_Log.Checked = false;
                }
            }
            else
            {
                mAppConfig.DS_Frm_Log = base.DockState;
            }
        }

        private void tsmi_Output_Click(object sender, EventArgs e)
        {
        }

        private void Frm_Log_Load(object sender, EventArgs e)
        {
        }
    }
}
