using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NovaVision.BaseClass;
using NovaVision.BaseClass.VisionConfig;

namespace NovaVision.VisionForm.MainForm
{
    public partial class Frm_DisplaySet : Form
    {
        private UIControl mUIControl;

        private string mType;



        public Frm_DisplaySet()
        {
            InitializeComponent();
        }

        public Frm_DisplaySet(UIControl uIControl, string type)
        {
            mUIControl = uIControl;
            mType = type;
            InitializeComponent();
            Text = type + "显示窗口设置";
            Init(type);
        }

        private void Init(string type)
        {
            DataGridView dgv = imageDisplayControl1.Dgv_Names;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.RowHeadersVisible = false;
            dgv.Columns.Add("name", "名字");
            dgv.Columns.Add("displayNmae", "显示名");
            dgv.Columns[0].ReadOnly = true;
            dgv.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            if (type == "2D")
            {
                imageDisplayControl1.CmbDisplayRow.Text = mUIControl.mAppConfig.DS_2D_Row.ToString();
                imageDisplayControl1.CmbDisplayCol.Text = mUIControl.mAppConfig.DS_2D_Column.ToString();
                imageDisplayControl1.ChkDisplay.Checked = mUIControl.mAppConfig.DS_Frm_2D_Dsiplay;
                for (int i = 0; i < mUIControl.mImageDisplays.Count; i++)
                {
                    if (mUIControl.mImageDisplays[i].Name[0] == '2')
                    {
                        dgv.Rows.Add();
                        dgv.Rows[dgv.Rows.Count - 1].Cells[0].Value = mUIControl.mImageDisplays[i].Name;
                        dgv.Rows[dgv.Rows.Count - 1].Cells[1].Value = mUIControl.mImageDisplays[i].DisplayName;
                    }
                }
                return;
            }
            imageDisplayControl1.CmbDisplayRow.Text = mUIControl.mAppConfig.DS_3D_Row.ToString();
            imageDisplayControl1.CmbDisplayCol.Text = mUIControl.mAppConfig.DS_3D_Column.ToString();
            imageDisplayControl1.ChkDisplay.Checked = mUIControl.mAppConfig.DS_Frm_3D_Dsiplay;
            for (int j = 0; j < mUIControl.mImageDisplays.Count; j++)
            {
                if (mUIControl.mImageDisplays[j].Name[0] == '3')
                {
                    dgv.Rows.Add();
                    dgv.Rows[dgv.Rows.Count - 1].Cells[0].Value = mUIControl.mImageDisplays[j].Name;
                    dgv.Rows[dgv.Rows.Count - 1].Cells[1].Value = mUIControl.mImageDisplays[j].DisplayName;
                }
            }
        }

        private void imageDisplayControl1_BtnSaveClick(object sender, EventArgs e)
        {
            DataGridView dgv = imageDisplayControl1.Dgv_Names;
            if (mType == "2D")
            {
                mUIControl.mAppConfig.DS_2D_Row = Convert.ToInt32(imageDisplayControl1.CmbDisplayRow.Text);
                mUIControl.mAppConfig.DS_2D_Column = Convert.ToInt32(imageDisplayControl1.CmbDisplayCol.Text);
                mUIControl.mAppConfig.DS_Frm_2D_Dsiplay = imageDisplayControl1.ChkDisplay.Checked;
                if (mUIControl.mAppConfig.Rows_2D_Height == null || mUIControl.mAppConfig.Rows_2D_Height.Count != mUIControl.mAppConfig.DS_2D_Row)
                {
                    mUIControl.mAppConfig.Rows_2D_Height = new List<float>();
                    for (int j = 0; j < mUIControl.mAppConfig.DS_2D_Row; j++)
                    {
                        mUIControl.mAppConfig.Rows_2D_Height.Add(100f / (float)mUIControl.mAppConfig.DS_2D_Row);
                    }
                }
                if (mUIControl.mAppConfig.Cols_2D_Width == null || mUIControl.mAppConfig.Cols_2D_Width.Count != mUIControl.mAppConfig.DS_2D_Column)
                {
                    mUIControl.mAppConfig.Cols_2D_Width = new List<float>();
                    for (int k = 0; k < mUIControl.mAppConfig.DS_2D_Column; k++)
                    {
                        mUIControl.mAppConfig.Cols_2D_Width.Add(100f / (float)mUIControl.mAppConfig.DS_2D_Column);
                    }
                }
                int num2 = 0;
                mUIControl.mAppConfig.DS_2D_Names = "";
                for (int n = 0; n < mUIControl.mImageDisplays.Count; n++)
                {
                    if (mUIControl.mImageDisplays[n].Name[0] == '2')
                    {
                        if (num2 < dgv.Rows.Count)
                        {
                            mUIControl.mImageDisplays[n].DisplayName = dgv.Rows[num2].Cells[1].Value.ToString();
                        }
                        else
                        {
                            mUIControl.mImageDisplays[n].DisplayName = "2D_" + (num2 + 1);
                        }
                        AppConfig mAppConfig = mUIControl.mAppConfig;
                        mAppConfig.DS_2D_Names = mAppConfig.DS_2D_Names + mUIControl.mImageDisplays[n].DisplayName + "|";
                        num2++;
                    }
                }
                mUIControl.mAppConfig.DS_2D_Names = mUIControl.mAppConfig.DS_2D_Names.Substring(0, mUIControl.mAppConfig.DS_2D_Names.Length - 1);
            }
            else
            {
                mUIControl.mAppConfig.DS_3D_Row = Convert.ToInt32(imageDisplayControl1.CmbDisplayRow.Text);
                mUIControl.mAppConfig.DS_3D_Column = Convert.ToInt32(imageDisplayControl1.CmbDisplayCol.Text);
                mUIControl.mAppConfig.DS_Frm_3D_Dsiplay = imageDisplayControl1.ChkDisplay.Checked;
                if (mUIControl.mAppConfig.Rows_3D_Height == null || mUIControl.mAppConfig.Rows_3D_Height.Count != mUIControl.mAppConfig.DS_3D_Row)
                {
                    mUIControl.mAppConfig.Rows_3D_Height = new List<float>();
                    for (int m = 0; m < mUIControl.mAppConfig.DS_3D_Row; m++)
                    {
                        mUIControl.mAppConfig.Rows_3D_Height.Add(100f / (float)mUIControl.mAppConfig.DS_3D_Row);
                    }
                }
                if (mUIControl.mAppConfig.Cols_3D_Width == null || mUIControl.mAppConfig.Cols_3D_Width.Count != mUIControl.mAppConfig.DS_3D_Column)
                {
                    mUIControl.mAppConfig.Cols_3D_Width = new List<float>();
                    for (int l = 0; l < mUIControl.mAppConfig.DS_3D_Column; l++)
                    {
                        mUIControl.mAppConfig.Cols_3D_Width.Add(100f / (float)mUIControl.mAppConfig.DS_3D_Column);
                    }
                }
                int num = 0;
                mUIControl.mAppConfig.DS_3D_Names = "";
                for (int i = 0; i < mUIControl.mImageDisplays.Count; i++)
                {
                    if (mUIControl.mImageDisplays[i].Name[0] == '3')
                    {
                        if (num < dgv.Rows.Count)
                        {
                            mUIControl.mImageDisplays[i].DisplayName = dgv.Rows[num].Cells[1].Value.ToString();
                        }
                        else
                        {
                            mUIControl.mImageDisplays[i].DisplayName = "3D_" + (num + 1);
                        }
                        AppConfig mAppConfig2 = mUIControl.mAppConfig;
                        mAppConfig2.DS_3D_Names = mAppConfig2.DS_3D_Names + mUIControl.mImageDisplays[i].DisplayName + "|";
                        num++;
                    }
                }
                mUIControl.mAppConfig.DS_3D_Names = mUIControl.mAppConfig.DS_3D_Names.Substring(0, mUIControl.mAppConfig.DS_3D_Names.Length - 1);
            }
            if (mUIControl.SaveAppConfig())
            {
                MessageBox.Show(@"参数保存成功！");
            }
            else
            {
                MessageBox.Show(@"参数保存失败！");
            }
        }

        private void imageDisplayControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
