using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Authority;

namespace NovaVision.VisionForm.LoginFrm
{
    public partial class Frm_Authority : Form
    {

        private string authorityName;

        private AuthorityInfo authorityInfo;

        private string AuthorityFilePath;

        private string AuthType = ConfigurationManager.AppSettings["AuthType"];

        public Frm_Authority(AuthorityInfo _mAuthorityInfo, string _authorityName, string path)
        {
            authorityInfo = _mAuthorityInfo;
            authorityName = _authorityName;
            AuthorityFilePath = path;
            InitializeComponent();
        }

        private void Frm_Authority_Load(object sender, EventArgs e)
        {
            btn_save.Enabled = authorityInfo.Dicauth[authorityName].AuthoritySave;
            IniCombobox();
            comboBox_name.Text = "OPN";
            LoadInit(comboBox_name.Text);
        }

        private void LoadInit(string _authorityName)
        {
            try
            {
                SystemSetModule.Checked = authorityInfo.Dicauth[_authorityName].SystemSetModule;
                JobConfig.Checked = authorityInfo.Dicauth[_authorityName].JobConfig;
                StationSet.Checked = authorityInfo.Dicauth[_authorityName].StationSet;
                SystemPar.Checked = authorityInfo.Dicauth[_authorityName].SystemPar;
                AuthoritySet.Checked = authorityInfo.Dicauth[_authorityName].AuthoritySet;
                AuthoritySave.Checked = authorityInfo.Dicauth[_authorityName].AuthoritySave;
                InspectParamsSet.Checked = authorityInfo.Dicauth[_authorityName].InspectParamsSet;
                SystemState.Checked = authorityInfo.Dicauth[_authorityName].SystemState;
                UserManagement.Checked = authorityInfo.Dicauth[_authorityName].UserManagement;
                PicPlayBack.Checked = authorityInfo.Dicauth[_authorityName].PicPlayBack;
                CommModule.Checked = authorityInfo.Dicauth[_authorityName].CommModule;
                CommType.Checked = authorityInfo.Dicauth[_authorityName].CommType;
                CommSet.Checked = authorityInfo.Dicauth[_authorityName].CommSet;
                CameraModule.Checked = authorityInfo.Dicauth[_authorityName].CameraModule;
                CameraSet.Checked = authorityInfo.Dicauth[_authorityName].CameraSet;
                Camera_2Dset.Checked = authorityInfo.Dicauth[_authorityName].Camera_2Dset;
                Camera_2DLset.Checked = authorityInfo.Dicauth[_authorityName].Camera_2DLset;
                Camera_3Dset.Checked = authorityInfo.Dicauth[_authorityName].Camera_3Dset;
                AlgorithmModule.Checked = authorityInfo.Dicauth[_authorityName].AlgorithmModule;
                AlgorithmVpp.Checked = authorityInfo.Dicauth[_authorityName].AlgorithmVpp;
                AlgorithmParam.Checked = authorityInfo.Dicauth[_authorityName].AlgorithmParam;
                MesModule.Checked = authorityInfo.Dicauth[_authorityName].MesModule;
                MesData.Checked = authorityInfo.Dicauth[_authorityName].MesData;
                MesParam.Checked = authorityInfo.Dicauth[_authorityName].MesParam;
                MesData_btn_init.Checked = authorityInfo.Dicauth[_authorityName].MesData_btn_init;
                MesData_btn_save.Checked = authorityInfo.Dicauth[_authorityName].MesData_btn_save;
                MesParam_btn_save.Checked = authorityInfo.Dicauth[_authorityName].MesParam_btn_save;
                MesDataManual.Checked = authorityInfo.Dicauth[_authorityName].MesData_Manual;
                MesDataModify.Checked = authorityInfo.Dicauth[_authorityName].MesData_Modify;
                DataModule.Checked = authorityInfo.Dicauth[_authorityName].DataModule;
                DataBaseSet.Checked = authorityInfo.Dicauth[_authorityName].DataBaseSet;
                ViewModule.Checked = authorityInfo.Dicauth[_authorityName].ViewModule;
                ViewAdaptation.Checked = authorityInfo.Dicauth[_authorityName].ViewAdaptation;
                Display2D.Checked = authorityInfo.Dicauth[_authorityName].Display2D;
                Display3D.Checked = authorityInfo.Dicauth[_authorityName].Display3D;
                Delete.Checked = authorityInfo.Dicauth[_authorityName].Delete;
                Create.Checked = authorityInfo.Dicauth[_authorityName].Create;
                MonitorPlat.Checked = authorityInfo.Dicauth[_authorityName].MonitorPlat;
            }
            catch (Exception ex)
            {
                MessageBox.Show("配置参数加载异常，" + ex.Message);
            }
        }

        private void IniCombobox()
        {
            if (AuthType == "0")
            {
                comboBox_name.Items.Clear();
                List<string> objList = new List<string> { "操作员", "工程师", "管理员" };
                ComboBox.ObjectCollection items = comboBox_name.Items;
                object[] items2 = objList.ToArray();
                items.AddRange(items2);
            }
            comboBox_name.SelectedIndex = 0;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                AuthorityData authorityData = new AuthorityData();
                authorityData.SystemSetModule = SystemSetModule.Checked;
                authorityData.JobConfig = JobConfig.Checked;
                authorityData.StationSet = StationSet.Checked;
                authorityData.SystemPar = SystemPar.Checked;
                authorityData.AuthoritySet = AuthoritySet.Checked;
                authorityData.AuthoritySave = AuthoritySave.Checked;
                authorityData.InspectParamsSet = InspectParamsSet.Checked;
                authorityData.SystemState = SystemState.Checked;
                authorityData.UserManagement = UserManagement.Checked;
                authorityData.CommModule = CommModule.Checked;
                authorityData.CommType = CommType.Checked;
                authorityData.CommSet = CommSet.Checked;
                authorityData.PicPlayBack = PicPlayBack.Checked;
                authorityData.CameraModule = CameraModule.Checked;
                authorityData.CameraSet = CameraSet.Checked;
                authorityData.Camera_2Dset = Camera_2Dset.Checked;
                authorityData.Camera_2DLset = Camera_2DLset.Checked;
                authorityData.Camera_3Dset = Camera_3Dset.Checked;
                authorityData.AlgorithmModule = AlgorithmModule.Checked;
                authorityData.AlgorithmVpp = AlgorithmVpp.Checked;
                authorityData.AlgorithmParam = AlgorithmParam.Checked;
                authorityData.MesModule = MesModule.Checked;
                authorityData.MesData = MesData.Checked;
                authorityData.MesParam = MesParam.Checked;
                authorityData.MesData_btn_init = MesData_btn_init.Checked;
                authorityData.MesData_btn_save = MesData_btn_save.Checked;
                authorityData.MesParam_btn_save = MesParam_btn_save.Checked;
                authorityData.MesData_Manual = MesDataManual.Checked;
                authorityData.MesData_Modify = MesDataModify.Checked;
                authorityData.DataModule = DataModule.Checked;
                authorityData.DataBaseSet = DataBaseSet.Checked;
                authorityData.ViewModule = ViewModule.Checked;
                authorityData.ViewAdaptation = ViewAdaptation.Checked;
                authorityData.Display2D = Display2D.Checked;
                authorityData.Display3D = Display3D.Checked;
                authorityData.Delete = Delete.Checked;
                authorityData.Create = Create.Checked;
                authorityData.MonitorPlat = MonitorPlat.Checked;
                authorityInfo.Dicauth.Remove(comboBox_name.Text);
                authorityInfo.Dicauth.Add(comboBox_name.Text, authorityData);
                XmlHelp.WriteXML(authorityInfo, AuthorityFilePath, typeof(AuthorityInfo));
                MessageBox.Show(@"配置保存成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("配置保存异常，" + ex.Message);
            }
        }

        private void comboBox_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInit(comboBox_name.Text);
        }

        private void Frm_Authority_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }

        private void chk_SelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_SelectAll.Checked)
            {
                SetAllState(state: true);
            }
            else
            {
                SetAllState(state: false);
            }
        }

        private void SetAllState(bool state)
        {
            SystemSetModule.Checked = state;
            JobConfig.Checked = state;
            StationSet.Checked = state;
            SystemPar.Checked = state;
            AuthoritySet.Checked = state;
            AuthoritySave.Checked = state;
            InspectParamsSet.Checked = state;
            SystemState.Checked = state;
            UserManagement.Checked = state;
            PicPlayBack.Checked = state;
            CommModule.Checked = state;
            CommType.Checked = state;
            CommSet.Checked = state;
            CameraModule.Checked = state;
            CameraSet.Checked = state;
            Camera_2Dset.Checked = state;
            Camera_2DLset.Checked = state;
            Camera_3Dset.Checked = state;
            AlgorithmModule.Checked = state;
            AlgorithmVpp.Checked = state;
            AlgorithmParam.Checked = state;
            MesModule.Checked = state;
            MesData.Checked = state;
            MesParam.Checked = state;
            MesData_btn_init.Checked = state;
            MesData_btn_save.Checked = state;
            MesParam_btn_save.Checked = state;
            MesDataManual.Checked = state;
            DataModule.Checked = state;
            DataBaseSet.Checked = state;
            ViewModule.Checked = state;
            ViewAdaptation.Checked = state;
            Display2D.Checked = state;
            Display3D.Checked = state;
            Delete.Checked = state;
            Create.Checked = state;
            MonitorPlat.Checked = state;
        }

    }
}
