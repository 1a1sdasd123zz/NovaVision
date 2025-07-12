using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NovaVision.VisionForm.StationFrm
{
    public partial class PathEditForm : Form
    {
        private List<string> imagePath;
        public PathEditForm(List<string> paths)
        {
            this.InitializeComponent();
            this.InitDgv(paths);
        }

        private void InitDgv(List<string> paths)
        {
            this.imagePath = paths;
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToOrderColumns = false;
            this.dgv.RowHeadersVisible = false;
            this.dgv.Columns.Add("Path", "路径");
            DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
            btnCol.DefaultCellStyle.NullValue = "...";
            this.dgv.Columns.Add(btnCol);
            this.dgv.Columns[1].Name = "EditValue";
            this.dgv.Columns[1].HeaderText = "选择";
            this.dgv.Columns[0].ReadOnly = true;
            this.dgv.Columns[0].Width = 430;
            this.dgv.Columns[1].ReadOnly = true;
            this.dgv.Columns[1].Width = 40;
            this.dgv.Rows.Clear();
            for (int i = 0; i < paths.Count; i++)
            {
                this.dgv.Rows.Add();
                this.dgv[0, i].Value = paths[i];
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dgv.Rows.Count; i++)
            {
                bool flag = this.dgv[0, i].Value == null;
                if (flag)
                {
                    MessageBox.Show(string.Format("请检查第行{0}内容是否为空", i + 1));
                    return;
                }
            }
            base.Close();
        }

        private void tsBtn_NewLine_In_Click(object sender, EventArgs e)
        {
            this.dgv.Rows.Add();
            this.imagePath.Add("");
        }

        private void tsBtn_DeleteLine_In_Click(object sender, EventArgs e)
        {
            bool flag = this.dgv.Rows.Count > 0;
            if (flag)
            {
                int selectIndex = this.dgv.CurrentCell.RowIndex;
                this.dgv.Rows.RemoveAt(selectIndex);
                this.imagePath.RemoveAt(selectIndex);
            }
        }

        private void Dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            bool flag = this.dgv.CurrentCell != null && this.dgv.CurrentCell.ColumnIndex == 1;
            if (flag)
            {
                int index = this.dgv.CurrentCell.RowIndex;
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "(*.JPEG,*.jpg,*.jpeg,*.bmp,*.cdb,*.txt)|*.JPEG;*.jpg;*.jpeg;*.bmp;*.cdb;*.txt";
                bool flag2 = ofd.ShowDialog() == DialogResult.OK;
                if (flag2)
                {
                    string fileName = ofd.FileName;
                    this.dgv[0, index].Value = fileName;
                    this.imagePath[index] = fileName;
                }
            }
        }
    }
}
