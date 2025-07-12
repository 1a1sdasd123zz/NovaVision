using System;
using System.IO;
using System.Windows.Forms;
using NovaVision.BaseClass;
using NovaVision.BaseClass.VisionConfig;

namespace NovaVision.VisionForm.StationFrm
{
    public partial class Frm_ProcedurePlayBack : Form
    {
        private WorkFlow.TaskFlow mTaskFlow;

        private JobData mJobData;

        private PlayBackList mPlayBackList;

        private static Frm_ProcedurePlayBack _instance;

        public static Frm_ProcedurePlayBack CreateInstance(JobData jobData)
        {
            bool flag = Frm_ProcedurePlayBack._instance == null;
            if (flag)
            {
                Frm_ProcedurePlayBack._instance = new Frm_ProcedurePlayBack(jobData);
            }
            return Frm_ProcedurePlayBack._instance;
        }

        private Frm_ProcedurePlayBack(JobData jobData)
        {
            this.InitializeComponent();
            this.mJobData = jobData;
            this.mPlayBackList = PlayBackList.Deserialzer(this.mJobData.mSystemConfigData.JobPath);
            this.mPlayBackList.mJobData = this.mJobData;
            this.mPlayBackList.mTaskFlow = this.mTaskFlow;
            this.Init();
            this.InitDgv();
            this.UpdateDgv();
        }

        private void Init()
        {
            this.chk_IsSaveResultImage.Checked = this.mPlayBackList.IsSavePic;
            this.chk_IsSaveResultData.Checked = this.mPlayBackList.IsSaveResult;
            this.txtResultImagePath.Text = this.mPlayBackList.SavePicPath;
            this.txtImageFilesPath.Text = this.mPlayBackList.RecordPath;
            this.txtCode.Text = this.mPlayBackList.Code;
        }

        private void InitDgv()
        {
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToOrderColumns = false;
            this.dgv.RowHeadersVisible = false;
            DataGridViewComboBoxColumn cmbCol = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxCell.ObjectCollection items = cmbCol.Items;
            object[] items2 = this.mJobData.mStations.GetKeys().ToArray();
            items.AddRange(items2);
            this.dgv.Columns.Add(cmbCol);
            this.dgv.Columns[0].Name = "Station";
            this.dgv.Columns[0].HeaderText = "工位";
            cmbCol = new DataGridViewComboBoxColumn();
            this.dgv.Columns.Add(cmbCol);
            this.dgv.Columns[1].Name = "Inspect";
            this.dgv.Columns[1].HeaderText = "检测";
            DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
            btnCol.DefaultCellStyle.NullValue = "编辑";
            this.dgv.Columns.Add(btnCol);
            this.dgv.Columns[2].Name = "EditValue";
            this.dgv.Columns[2].HeaderText = "图像路径";
            cmbCol = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxCell.ObjectCollection items3 = cmbCol.Items;
            items2 = new string[]
            {
                "图片回放",
                "单次执行"
            };
            items3.AddRange(items2);
            this.dgv.Columns.Add(cmbCol);
            this.dgv.Columns[3].Name = "Mode";
            this.dgv.Columns[3].HeaderText = "执行模式";
            btnCol = new DataGridViewButtonColumn();
            btnCol.DefaultCellStyle.NullValue = "检测";
            this.dgv.Columns.Add(btnCol);
            this.dgv.Columns[4].Name = "StartInspect";
            this.dgv.Columns[4].HeaderText = "执行";
            DataGridViewCheckBoxColumn checkCol = new DataGridViewCheckBoxColumn();
            this.dgv.Columns.Add(checkCol);
            this.dgv.Columns[5].Name = "Ignore";
            this.dgv.Columns[5].HeaderText = "屏蔽";
            this.dgv.Columns[0].Width = 100;
            this.dgv.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgv.Columns[1].Width = 100;
            this.dgv.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgv.Columns[2].Width = 80;
            this.dgv.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgv.Columns[3].Width = 80;
            this.dgv.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgv.Columns[4].Width = 60;
            this.dgv.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgv.Columns[5].Width = 40;
            this.dgv.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void UpdateDgv()
        {
            this.dgv.Rows.Clear();
            for (int i = 0; i < this.mPlayBackList.PlayBackInfo.Count; i++)
            {
                this.dgv.Rows.Add();
                string stationName = this.mPlayBackList.PlayBackInfo[i].StationName;
                bool flag = this.mJobData.mStations.ContainsKey(stationName);
                if (flag)
                {
                    this.dgv[0, i].Value = this.mPlayBackList.PlayBackInfo[i].StationName;
                    string inspectName = this.mPlayBackList.PlayBackInfo[i].InspectName;
                    ((DataGridViewComboBoxCell)this.dgv[1, i]).Items.Clear();
                    DataGridViewComboBoxCell.ObjectCollection items = ((DataGridViewComboBoxCell)this.dgv[1, i]).Items;
                    object[] items2 = this.mJobData.mStations[stationName].GetKeys().ToArray();
                    items.AddRange(items2);
                    bool flag2 = this.mJobData.mStations[stationName].ContainsKey(inspectName);
                    if (flag2)
                    {
                        this.dgv[1, i].Value = this.mPlayBackList.PlayBackInfo[i].InspectName;
                    }
                    else
                    {
                        this.dgv[1, i].Value = "";
                        this.mPlayBackList.PlayBackInfo[i].InspectName = "";
                    }
                }
                else
                {
                    this.dgv[0, i].Value = "";
                    this.mPlayBackList.PlayBackInfo[i].StationName = "";
                }
                this.dgv[3, i].Value = this.mPlayBackList.PlayBackInfo[i].ExcuteMode;
                this.dgv[5, i].Value = this.mPlayBackList.PlayBackInfo[i].IsIgnore;
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "图片|*.bmp;*.jpg;*.JPEG;*.cdb";
            bool flag = ofd.ShowDialog() == DialogResult.OK;
            if (flag)
            {
                string fileName = ofd.FileName;
                this.txtImageFilesPath.Text = Path.GetDirectoryName(fileName);
                this.mPlayBackList.RecordPath = this.txtImageFilesPath.Text;
                int index = this.txtImageFilesPath.Text.LastIndexOf('\\');
                this.txtCode.Text = this.txtImageFilesPath.Text.Substring(index + 1, this.txtImageFilesPath.Text.Length - index - 1);
                this.mPlayBackList.Code = this.txtCode.Text;
                this.mPlayBackList.Dispatch(ofd.FileNames);
            }
        }

        private void btnChooseResultImagePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            bool flag = fbd.ShowDialog() == DialogResult.OK;
            if (flag)
            {
                this.txtResultImagePath.Text = fbd.SelectedPath;
                this.mPlayBackList.SavePicPath = this.txtResultImagePath.Text;
            }
        }

        private void chk_IsSaveResultImage_CheckedChanged(object sender, EventArgs e)
        {
            this.mPlayBackList.IsSavePic = this.chk_IsSaveResultImage.Checked;
        }

        private void chk_IsSaveResultData_CheckedChanged(object sender, EventArgs e)
        {
            this.mPlayBackList.IsSaveResult = this.chk_IsSaveResultData.Checked;
        }

        private void btnExcuteAllProucedure_Click(object sender, EventArgs e)
        {
            this.mPlayBackList.ExcuteProucedureOnce(this.mPlayBackList.SavePicPath, this.mPlayBackList.IsSavePic, this.mPlayBackList.IsSaveResult);
            MessageBox.Show(@"单次回放所有流程完成！");
        }

        private void tsBtn_NewLine_Click(object sender, EventArgs e)
        {
            this.dgv.Rows.Add();
            int count = this.dgv.Rows.Count;
            this.mPlayBackList.PlayBackInfo.Add(new PlayBack());
        }

        private void tsBtn_DeleteLine_Click(object sender, EventArgs e)
        {
            bool flag = this.dgv.Rows.Count > 0;
            if (flag)
            {
                int selectIndex = this.dgv.CurrentCell.RowIndex;
                this.dgv.Rows.RemoveAt(selectIndex);
                this.mPlayBackList.PlayBackInfo.RemoveAt(selectIndex);
            }
        }

        private void tsBtn_Save_Click(object sender, EventArgs e)
        {
            int count = this.dgv.Rows.Count;
            int i = 0;
            while (i < count)
            {
                bool flag = !string.IsNullOrEmpty(this.dgv[0, i].EditedFormattedValue.ToString());
                if (flag)
                {
                    this.mPlayBackList.PlayBackInfo[i].StationName = this.dgv[0, i].EditedFormattedValue.ToString();
                    bool flag2 = !string.IsNullOrEmpty(this.dgv[1, i].EditedFormattedValue.ToString());
                    if (flag2)
                    {
                        this.mPlayBackList.PlayBackInfo[i].InspectName = this.dgv[1, i].EditedFormattedValue.ToString();
                        bool flag3 = !string.IsNullOrEmpty(this.dgv[3, i].EditedFormattedValue.ToString());
                        if (flag3)
                        {
                            this.mPlayBackList.PlayBackInfo[i].ExcuteMode = this.dgv[3, i].EditedFormattedValue.ToString();
                            this.mPlayBackList.PlayBackInfo[i].IsIgnore = (bool)this.dgv[5, i].EditedFormattedValue;
                            i++;
                            continue;
                        }
                        MessageBox.Show("回放参数 [执行模式] 为空保存失败！", "参数保存失败");
                    }
                    else
                    {
                        MessageBox.Show("回放参数 [检测名] 为空保存失败！", "参数保存失败");
                    }
                }
                else
                {
                    MessageBox.Show("回放参数 [工位名] 为空保存失败！", "参数保存失败");
                }
                return;
            }
            bool flag4 = XmlHelp.WriteXML(this.mPlayBackList, this.mJobData.mSystemConfigData.JobPath + "PlayBack.xml", typeof(PlayBackList));
            if (flag4)
            {
                MessageBox.Show("回放参数保存成功！", "参数保存成功");
                return;
            }
        }

        private void Dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            bool flag = this.dgv.CurrentCell.ColumnIndex == 0 && e.Control is ComboBox;
            if (flag)
            {
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.SelectedIndexChanged -= this.ComboBox_SelectedIndexChanged;
                comboBox.SelectedIndexChanged += this.ComboBox_SelectedIndexChanged;
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            bool flag = this.dgv.CurrentCell.ColumnIndex == 0;
            if (flag)
            {
                bool flag2 = comboBox.SelectedItem != null;
                if (flag2)
                {
                    string stationName = comboBox.SelectedItem.ToString();
                    DataGridViewComboBoxCell comboBoxCell = this.dgv[1, this.dgv.CurrentCell.RowIndex] as DataGridViewComboBoxCell;
                    comboBoxCell.Items.Clear();
                    DataGridViewComboBoxCell.ObjectCollection items = comboBoxCell.Items;
                    object[] items2 = this.mJobData.mStations[stationName].GetKeys().ToArray();
                    items.AddRange(items2);
                    comboBoxCell.Value = "";
                }
            }
        }

        private void Dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = this.dgv.CurrentCell.RowIndex;
            bool flag = this.dgv.CurrentCell.ColumnIndex == 2 && this.mPlayBackList.PlayBackInfo[index].ImagePaths != null;
            if (flag)
            {
                PathEditForm pathEditForm = new PathEditForm(this.mPlayBackList.PlayBackInfo[index].ImagePaths);
                MultiLanguage.LoadLanguage(pathEditForm, MultiLanguage.GetDefaultLanguage());
                pathEditForm.ShowDialog();
            }
        }

        private void Frm_ProcedurePlayBack_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.mTaskFlow.IsManual = false;
            //this.mTaskFlow.StopTaskFlow();
            Frm_ProcedurePlayBack._instance = null;
        }
    }
}
