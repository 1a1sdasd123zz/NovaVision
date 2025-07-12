using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Authority;
using NovaVision.BaseClass.DataBase;

namespace NovaVision.VisionForm.LoginFrm
{
    public partial class Frm_UserManage : Form
    {
        private SQLiteInfoBll mSQLiteInfoBll = new SQLiteInfoBll();

        private string filename;

        private string DB_path;

        private string authorityName;

        private AuthorityInfo mAuthorityInfo;

        public Frm_UserManage(string path, AuthorityInfo _mAuthorityInfo, string _authorityName)
        {
            InitializeComponent();
            authorityName = _authorityName;
            mAuthorityInfo = _mAuthorityInfo;
            DB_path = path;
            filename = path + "UserInfo.db";
            LoadAuth();
        }

        private void LoadAuth()
        {
            if (authorityName == "空")
            {
                if (!File.Exists(filename))
                {
                    btn_create.Enabled = false;
                    btn_create.Visible = false;
                    btn_delete.Enabled = false;
                    btn_delete.Visible = false;
                    return;
                }
                DataSet ds = mSQLiteInfoBll.GetList("UserInfo");
                List<VisionForm.LoginFrm.UserInfo> list = DataBaseHelper.DataTableToList<VisionForm.LoginFrm.UserInfo>(ds.Tables[0]);
                if (list != null && list.Count > 0)
                {
                    LoadData();
                }
                btn_Init.Enabled = false;
                btn_Init.Visible = false;
            }
            else
            {
                btn_Init.Visible = false;
                btn_delete.Enabled = mAuthorityInfo.Dicauth[authorityName].Delete;
                btn_create.Enabled = mAuthorityInfo.Dicauth[authorityName].Create;
                LoadData();
            }
        }

        private void LoadData()
        {
            dgv_List.DataSource = null;
            DataSet ds = mSQLiteInfoBll.GetList("UserInfo");
            List<VisionForm.LoginFrm.UserInfo> list = DataBaseHelper.DataTableToList<VisionForm.LoginFrm.UserInfo>(ds.Tables[0]);
            if (list != null)
            {
                dgv_List.DataSource = list;
                dgv_List.Columns[2].Visible = false;
                dgv_List.Columns[0].Visible = false;
                dgv_List.Columns[1].HeaderText = "用户名";
                dgv_List.Columns[3].HeaderText = "权限";
                dgv_List.Columns[4].HeaderText = "创建时间";
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            string selectedUserName = dgv_List.SelectedRows[0].Cells["UserName"].Value.ToString();
            try
            {
                DialogResult dialogResult = MessageBox.Show("您确定要删除用户_" + selectedUserName + "吗", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (dialogResult == DialogResult.Yes)
                {
                    string selectedId = dgv_List.SelectedRows[0].Cells["ID"].Value.ToString();
                    mSQLiteInfoBll.Delete(int.Parse(selectedId), "UserInfo");
                    LogUtil.LogError(selectedUserName + "_用户删除成功");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogError(selectedUserName + "_用户删除失败_" + ex.Message);
                MessageBox.Show(selectedUserName + "_用户删除失败_" + ex.Message);
            }
        }

        private void btn_Init_Click(object sender, EventArgs e)
        {
            try
            {
                List<TableFieldInfo> mlistfiled = new List<TableFieldInfo>();
                mlistfiled.Add(Field_info("UserName", "TEXT", "20"));
                mlistfiled.Add(Field_info("Pwd", "TEXT", "20"));
                mlistfiled.Add(Field_info("Authority", "TEXT", "20"));
                mlistfiled.Add(Field_info("CreateTime", "DATETIME", "20"));
                if (!Directory.Exists(DB_path))
                {
                    Directory.CreateDirectory(DB_path);
                }
                if (mSQLiteInfoBll.Create(DB_path, "UserInfo", mlistfiled))
                {
                    btn_Init.Visible = false;
                    btn_create.Enabled = true;
                    btn_create.Visible = true;
                    btn_delete.Enabled = true;
                    btn_delete.Visible = true;
                    LogUtil.Log("用户数据库表初始化完成");
                    MessageBox.Show(@"用户数据库表初始化完成");
                }
                else
                {
                    btn_create.Enabled = false;
                    btn_create.Visible = false;
                    btn_delete.Enabled = false;
                    btn_delete.Visible = false;
                    LogUtil.Log("用户数据库表初始化失败");
                    MessageBox.Show(@"用户数据库表初始化失败");
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogError("用户数据库表初始化异常，详情" + ex.Message);
                MessageBox.Show("用户数据库表初始化异常，" + ex.Message);
            }
        }

        public TableFieldInfo Field_info(string field, string type, string length)
        {
            TableFieldInfo mfield = new TableFieldInfo();
            mfield.FieldName = field;
            mfield.DbDataType = (DBDataType)Enum.Parse(typeof(DBDataType), type);
            mfield.DataLength = Convert.ToInt32(length);
            mfield.IsNull = true;
            return mfield;
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            Frm_Register frm_Register = new Frm_Register();
            if (frm_Register.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void Frm_UserManage_FormClosed(object sender, FormClosedEventArgs e)
        {
            base.DialogResult = DialogResult.OK;
        }
    }
}
