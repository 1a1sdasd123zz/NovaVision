using System;
using System.Windows.Forms;
using NovaVision.BaseClass.VisionConfig;
using WeifenLuo.WinFormsUI.Docking;

namespace NovaVision.VisionForm.MainForm
{
    public partial class Frm_3D : DockContent
    {
        private static UIControl mUIControl;

        private static Frm_3D frm_3D;

        private static Frm_Main frm_Main;

        private int ImageNum3D;

        private TableLayoutPanel tableLayoutPanel1;

        public Frm_3D()
        {
            InitializeComponent();
            Controladd();
        }

        private void Controladd()
        {
            base.Controls.Clear();
            ImageNum3D = 1;
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowCount = mUIControl.mAppConfig.DS_3D_Row;
            tableLayoutPanel1.ColumnCount = mUIControl.mAppConfig.DS_3D_Column;
            tableLayoutPanel1.Dock = DockStyle.Fill;
            for (int i = 0; i < mUIControl.mAppConfig.DS_3D_Row; i++)
            {
                if (mUIControl.mAppConfig.Rows_3D_Height.Count == mUIControl.mAppConfig.DS_3D_Row)
                {
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, mUIControl.mAppConfig.Rows_3D_Height[i]));
                }
                else
                {
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / mUIControl.mAppConfig.DS_3D_Row));
                }
            }
            for (int k = 0; k < mUIControl.mAppConfig.DS_3D_Column; k++)
            {
                if (mUIControl.mAppConfig.Cols_3D_Width.Count == mUIControl.mAppConfig.DS_3D_Column)
                {
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, mUIControl.mAppConfig.Cols_3D_Width[k]));
                }
                else
                {
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / mUIControl.mAppConfig.DS_3D_Column));
                }
            }
            for (int j = 0; j < mUIControl.mAppConfig.DS_3D_Row; j++)
            {
                for (int l = 0; l < mUIControl.mAppConfig.DS_3D_Column; l++)
                {
                    string key = "3D_" + ImageNum3D;
                    mUIControl.mImageDisplays[key].Margin = new Padding
                    {
                        All = 1
                    };
                    tableLayoutPanel1.Controls.Add(mUIControl.mImageDisplays[key], l, j);
                    ImageNum3D++;
                }
            }
            base.Controls.Add(tableLayoutPanel1);
        }

        private void TableLayoutPanel1_RowColSizeChanged(object sender, EventArgs e)
        {
            if (mUIControl.mAppConfig.Rows_3D_Height != null)
            {
                int[] rowHeights = tableLayoutPanel1.GetRowHeights();
                int height = tableLayoutPanel1.ClientSize.Height;
                for (int j = 0; j < rowHeights.Length; j++)
                {
                    mUIControl.mAppConfig.Rows_3D_Height[j] = (float)rowHeights[j] / (float)height * 100f;
                }
            }
            if (mUIControl.mAppConfig.Cols_3D_Width != null)
            {
                int[] colWidths = tableLayoutPanel1.GetColumnWidths();
                int width = tableLayoutPanel1.ClientSize.Width;
                for (int i = 0; i < colWidths.Length; i++)
                {
                    mUIControl.mAppConfig.Cols_3D_Width[i] = (float)colWidths[i] / (float)width * 100f;
                }
            }
        }

        public static Frm_3D GetInstance(Frm_Main frm_main, UIControl uIControl)
        {
            mUIControl = uIControl;
            frm_Main = frm_main;
            if (frm_3D == null)
            {
                frm_3D = new Frm_3D();
            }
            else
            {
                frm_3D.Hide();
                frm_3D.Controladd();
            }
            return frm_3D;
        }

        private void Frm_3D_DockStateChanged(object sender, EventArgs e)
        {
            if (frm_3D == null)
            {
                return;
            }
            if (base.DockState == DockState.Unknown || base.DockState == DockState.Hidden)
            {
                if (base.DockState == DockState.Hidden)
                {
                    frm_Main.tsmi_3D.Checked = false;
                }
            }
            else
            {
                mUIControl.mAppConfig.DS_Frm_3D = base.DockState;
            }
        }

        private void Frm_3D_FormClosing(object sender, FormClosingEventArgs e)
        {
            frm_3D = null;
        }
    }
}
