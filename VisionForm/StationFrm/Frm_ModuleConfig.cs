using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Collection;
using NovaVision.BaseClass.Communication.CommData;
using NovaVision.BaseClass.Module;
using NovaVision.BaseClass.Module.Algorithm;
using NovaVision.BaseClass.VisionConfig;
using Info = NovaVision.BaseClass.Module.Algorithm.Info;

namespace NovaVision.VisionForm.StationFrm
{
    public partial class Frm_ModuleConfig : Form
    {
        private string StationName;

        private string InspectionName;

        private string ModuleType = "";

        private string Key = "";

        private JobData mJobData;

        private ModuleData<Terminal, Info> mAlgModuleData;

        private ModuleData<Comm_Element, BaseClass.Communication.CommData.Info> mCommData;

        private AlgInputsParamsCollection mParams;

        //private MesData mMesData;

        private StationCollection mStations;

        private int LastVerticalScrollingOffset;

        private int LastHorizontalScrollingOffset;

        private ComboBox cmb;

        private int LastCmbPositionX = 0;

        private int LastCmbPositionY = 0;

        private string Item = "";

        public Frm_ModuleConfig(string station, string inspection, string moduleType, string key, JobData jobData)
        {
            InitializeComponent();
            mJobData = jobData;
            mAlgModuleData = jobData.mAlgModuleData;
            mCommData = jobData.mCommData;
            mStations = jobData.mStations;
            mParams = jobData.mAlgParams;
            StationName = station;
            InspectionName = inspection;
            ModuleType = moduleType;
            Key = key;
            Text = station + "_" + inspection + "_" + moduleType + "_" + key;
            TableParam table = new TableParam
            {
                ColName = new List<string> { "Inputs", "Type", "Value", "ShowValue", "IsConfig", "In", "Explain" },
                HeaderTexts = new List<string> { "输入", "类型", "值", "查看值", "可否配置输入", "输入项", "注释" }
            };
            InitDgv(dgv_InOut, table);
            UpdateDgv(moduleType, key);
            dgv_InOut.ShowCellToolTips = true;
        }

        private void InitDgv(DataGridView dgv, TableParam table)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.RowHeadersVisible = false;
            dgv.Columns.Add(table.ColName[0], table.HeaderTexts[0]);
            dgv.Columns.Add(table.ColName[1], table.HeaderTexts[1]);
            dgv.Columns.Add(table.ColName[2], table.HeaderTexts[2]);
            DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
            btnCol.DefaultCellStyle.NullValue = "显示";
            dgv.Columns.Add(btnCol);
            dgv.Columns[3].Name = table.ColName[3];
            dgv.Columns[3].HeaderText = table.HeaderTexts[3];
            DataGridViewCheckBoxColumn CheckBoxCol = new DataGridViewCheckBoxColumn();
            dgv.Columns.Add(CheckBoxCol);
            dgv.Columns[4].Name = table.ColName[4];
            dgv.Columns[4].HeaderText = table.HeaderTexts[4];
            dgv.Columns.Add(table.ColName[5], table.HeaderTexts[5]);
            dgv.Columns.Add(table.ColName[6], table.HeaderTexts[6]);
            dgv.Columns[0].Width = 120;
            dgv.Columns[0].ReadOnly = true;
            dgv.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[1].Width = 120;
            dgv.Columns[1].ReadOnly = true;
            dgv.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[2].Width = 120;
            dgv.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[1].ReadOnly = true;
            dgv.Columns[3].Width = 120;
            dgv.Columns[3].ReadOnly = true;
            dgv.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[4].Width = 120;
            dgv.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[5].Width = 200;
            dgv.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[6].Width = 200;
            dgv.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void UpdateDgv(string moduleType, string key)
        {
            switch (moduleType)
            {
                case "Algorithm":
                    UpdateAlg_Dgv(key, dgv_InOut);
                    break;
                case "CommunicationTable":
                    UpdateComm_Dgv(key, dgv_InOut);
                    break;
                case "CommunicationTable_A":
                    UpdateComm_Dgv(key, dgv_InOut);
                    break;
            }
        }

        private void UpdateAlg_Dgv(string key, DataGridView dgv)
        {
            dgv.Rows.Clear();
            int count = mAlgModuleData.Dic[key].Inputs.Count;
            List<string> ListKeys = mAlgModuleData.Dic[key].Inputs.GetKeys();
            ClearErrorBinding(key, ListKeys, mStations[StationName][InspectionName].DataBindings, DataSource.Algorithm);
            MyDictionaryEx<Terminal> Terminals = mAlgModuleData.Dic[key].Inputs;
            DataBindingCollection dataBindings = mStations[StationName][InspectionName].DataBindings;
            for (int i = 0; i < count; i++)
            {
                dgv.Rows.Add();
                dgv.Rows[dgv.Rows.Count - 1].Cells[0].Value = ListKeys[i];
                dgv.Rows[dgv.Rows.Count - 1].Cells[1].Value = Terminals[i].Type;
                if (Terminals[i].Type == null)
                {
                    dgv.Rows[dgv.Rows.Count - 1].Cells[2].ReadOnly = true;
                    continue;
                }
                dgv.Rows[dgv.Rows.Count - 1].Cells[2].ValueType = MyTypeConvert.GetType(Terminals[ListKeys[i]].Type);
                string destinationPath = key + "." + ListKeys[i];
                string sourcePath = "";
                int index = dataBindings.GetSourcePath(DataSource.Algorithm, destinationPath, out sourcePath);
                if (index >= 0)
                {
                    string[] array = sourcePath.Split('.');
                    if (dataBindings[index].S_Source == DataSource.Algorithm)
                    {
                        dgv.Rows[dgv.Rows.Count - 1].Cells[2].Value = mAlgModuleData.Dic[array[0]].Outputs[array[1]].Value.mValue;
                        dgv.Rows[dgv.Rows.Count - 1].Cells[4].Value = true;
                        dgv.Rows[dgv.Rows.Count - 1].Cells[5].Value = "[A]." + sourcePath;
                    }
                    else if (dataBindings[index].S_Source == DataSource.AlgParam)
                    {
                        dgv.Rows[dgv.Rows.Count - 1].Cells[2].Value = mParams.Params[array[0]].Elements[array[1]].Value.mValue;
                        dgv.Rows[dgv.Rows.Count - 1].Cells[4].Value = true;
                        dgv.Rows[dgv.Rows.Count - 1].Cells[5].Value = "[In]." + sourcePath;
                    }
                    else if (dataBindings[index].S_Source == DataSource.Communication)
                    {
                        dgv.Rows[dgv.Rows.Count - 1].Cells[2].Value = mCommData.Dic[array[0]].Outputs[array[1]].Value.mValue;
                        dgv.Rows[dgv.Rows.Count - 1].Cells[4].Value = true;
                        dgv.Rows[dgv.Rows.Count - 1].Cells[5].Value = "[C]." + sourcePath;
                    }
                }
                else
                {
                    dgv.Rows[dgv.Rows.Count - 1].Cells[2].Value = Terminals[i].Value.mValue;
                    dgv.Rows[dgv.Rows.Count - 1].Cells[4].Value = false;
                }
            }
        }

        private void UpdateComm_Dgv(string key, DataGridView dgv)
        {
            dgv.Rows.Clear();
            int count = mCommData.Dic[key].Inputs.Count;
            List<string> ListKeys = mCommData.Dic[key].Inputs.GetKeys();
            MyDictionaryEx<Comm_Element> Terminals = mCommData.Dic[key].Inputs;
            DataBindingCollection dataBindings = mStations[StationName][InspectionName].DataBindings;
            for (int i = 0; i < count; i++)
            {
                dgv.Rows.Add();
                dgv.Rows[dgv.Rows.Count - 1].Cells[0].Value = ListKeys[i];
                dgv.Rows[dgv.Rows.Count - 1].Cells[1].Value = Terminals[i].Type;
                if (Terminals[i].Type == null)
                {
                    dgv.Rows[dgv.Rows.Count - 1].Cells[2].ReadOnly = true;
                    continue;
                }
                dgv.Rows[dgv.Rows.Count - 1].Cells[2].ValueType = MyTypeConvert.GetType(Terminals[ListKeys[i]].Type);
                string destinationPath = key + "." + ListKeys[i];
                string sourcePath = "";
                int index = dataBindings.GetSourcePath(DataSource.Communication, destinationPath, out sourcePath);
                if (index >= 0)
                {
                    string[] array = sourcePath.Split('.');
                    if (dataBindings[index].S_Source == DataSource.Algorithm)
                    {
                        dgv.Rows[dgv.Rows.Count - 1].Cells[2].Value = mAlgModuleData.Dic[array[0]].Outputs[array[1]].Value.mValue;
                        dgv.Rows[dgv.Rows.Count - 1].Cells[4].Value = true;
                        dgv.Rows[dgv.Rows.Count - 1].Cells[5].Value = "[A]." + sourcePath;
                    }
                    else if (dataBindings[index].S_Source == DataSource.AlgParam)
                    {
                        dgv.Rows[dgv.Rows.Count - 1].Cells[2].Value = mParams.Params[array[0]].Elements[array[1]].Value.mValue;
                        dgv.Rows[dgv.Rows.Count - 1].Cells[4].Value = true;
                        dgv.Rows[dgv.Rows.Count - 1].Cells[5].Value = "[In]." + sourcePath;
                    }
                    else if (dataBindings[index].S_Source == DataSource.Communication)
                    {
                        dgv.Rows[dgv.Rows.Count - 1].Cells[2].Value = mCommData.Dic[array[0]].Outputs[array[1]].Value.mValue;
                        dgv.Rows[dgv.Rows.Count - 1].Cells[4].Value = true;
                        dgv.Rows[dgv.Rows.Count - 1].Cells[5].Value = "[C]." + sourcePath;
                    }
                }
                else
                {
                    dgv.Rows[dgv.Rows.Count - 1].Cells[2].Value = Terminals[i].Value.mValue;
                    dgv.Rows[dgv.Rows.Count - 1].Cells[4].Value = false;
                }
            }
        }


        private void ClearErrorBinding(string key, List<string> listkey, DataBindingCollection dataBindings, DataSource dataSource)
        {
            List<string> destinationPaths = new List<string>();
            for (int i = 0; i < listkey.Count; i++)
            {
                destinationPaths.Add(key + "." + listkey[i]);
            }
            for (int j = 0; j < dataBindings.Count; j++)
            {
                if (dataSource == dataBindings[j].D_Source && !destinationPaths.Contains(dataBindings[j].DestinationPath))
                {
                    string str = dataBindings[j].DestinationPath;
                    dataBindings.RemoveAt(j);
                    j--;
                    LogUtil.Log("绑定中不存在目的路径" + str + "，移除此路径成功！");
                }
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            if (ModuleType == "Algorithm")
            {
                UpdateRelation_Alg();
            }
            else if (ModuleType == "CommunicationTable")
            {
                UpdateRelation_Comm();
            }
            else if (ModuleType == "CommunicationTable_A")
            {
                UpdateRelation_Comm();
            }
            UpdateDgv(ModuleType, Key);
        }

        private void UpdateRelation_Alg()
        {
            int count = mAlgModuleData.Dic[Key].Inputs.Count;
            DataGridView dgv = dgv_InOut;
            for (int i = 0; i < count; i++)
            {
                if (dgv.Rows[i].Cells[2].Value != null)
                {
                    Type type = dgv.Rows[i].Cells[2].ValueType;
                    mAlgModuleData.Dic[Key].Inputs[i].Value.mValue = dgv.Rows[i].Cells[2].Value;
                }
                if ((bool)dgv.Rows[i].Cells[4].Value && dgv.Rows[i].Cells[5].Value != null)
                {
                    mAlgModuleData.Dic[Key].Inputs[i].HasRelation = Convert.ToBoolean(dgv.Rows[i].Cells[4].Value);
                    string[] array = dgv.Rows[i].Cells[5].Value.ToString().Split('.');
                    string sourcePath3 = array[1] + "." + array[2];
                    string destinationPath3 = Key + "." + mAlgModuleData.Dic[Key].Inputs.GetKeys()[i];
                    DataBinding dataBinding = new DataBinding(sourcePath3, destinationPath3);
                    if (array[0] == "[A]")
                    {
                        dataBinding.S_Source = DataSource.Algorithm;
                    }
                    else if (array[0] == "[In]")
                    {
                        dataBinding.S_Source = DataSource.AlgParam;
                    }
                    else if (array[0] == "[C]")
                    {
                        dataBinding.S_Source = DataSource.Communication;
                    }
                    dataBinding.D_Source = DataSource.Algorithm;
                    if (!mStations[StationName][InspectionName].DataBindings.Contains(dataBinding))
                    {
                        mStations[StationName][InspectionName].DataBindings.Add(dataBinding);
                    }
                }
                else if (!(bool)dgv.Rows[i].Cells[4].Value && dgv.Rows[i].Cells[5].Value != null)
                {
                    mAlgModuleData.Dic[Key].Inputs[i].HasRelation = false;
                    string sourcePath2 = "";
                    string destinationPath2 = Key + "." + mAlgModuleData.Dic[Key].Inputs.GetKeys()[i];
                    int index2 = mJobData.mStations[StationName][InspectionName].DataBindings.GetSourcePath(DataSource.Algorithm, destinationPath2, out sourcePath2);
                    if (index2 >= 0)
                    {
                        mJobData.mStations[StationName][InspectionName].DataBindings.RemoveAt(index2);
                    }
                }
                else
                {
                    mAlgModuleData.Dic[Key].Inputs[i].HasRelation = false;
                    string sourcePath = "";
                    string destinationPath = Key + "." + mAlgModuleData.Dic[Key].Inputs.GetKeys()[i];
                    int index = mJobData.mStations[StationName][InspectionName].DataBindings.GetSourcePath(DataSource.Algorithm, destinationPath, out sourcePath);
                    if (index >= 0)
                    {
                        mJobData.mStations[StationName][InspectionName].DataBindings.RemoveAt(index);
                    }
                }
            }
        }

        private void UpdateRelation_Comm()
        {
            int count = mCommData.Dic[Key].Inputs.Count;
            DataGridView dgv = dgv_InOut;
            for (int i = 0; i < count; i++)
            {
                if (dgv.Rows[i].Cells[2].Value != null)
                {
                    Type type = dgv.Rows[i].Cells[2].ValueType;
                    mCommData.Dic[Key].Inputs[i].Value.mValue = dgv.Rows[i].Cells[2].Value;
                }
                if ((bool)dgv.Rows[i].Cells[4].Value && dgv.Rows[i].Cells[5].Value != null)
                {
                    mCommData.Dic[Key].Inputs[i].HasRelation = Convert.ToBoolean(dgv.Rows[i].Cells[4].Value);
                    string[] array = dgv.Rows[i].Cells[5].Value.ToString().Split('.');
                    string sourcePath3 = array[1] + "." + array[2];
                    string destinationPath3 = Key + "." + mCommData.Dic[Key].Inputs.GetKeys()[i];
                    DataBinding dataBinding = new DataBinding(sourcePath3, destinationPath3);
                    if (array[0] == "[A]")
                    {
                        dataBinding.S_Source = DataSource.Algorithm;
                    }
                    else if (array[0] == "[In]")
                    {
                        dataBinding.S_Source = DataSource.AlgParam;
                    }
                    else if (array[0] == "[C]")
                    {
                        dataBinding.S_Source = DataSource.Communication;
                    }
                    dataBinding.D_Source = DataSource.Communication;
                    if (!mStations[StationName][InspectionName].DataBindings.Contains(dataBinding))
                    {
                        mStations[StationName][InspectionName].DataBindings.Add(dataBinding);
                    }
                }
                else if (!(bool)dgv.Rows[i].Cells[4].Value && dgv.Rows[i].Cells[5].Value != null)
                {
                    mCommData.Dic[Key].Inputs[i].HasRelation = false;
                    string sourcePath2 = "";
                    string destinationPath2 = Key + "." + mCommData.Dic[Key].Inputs.GetKeys()[i];
                    int index2 = mJobData.mStations[StationName][InspectionName].DataBindings.GetSourcePath(DataSource.Communication, destinationPath2, out sourcePath2);
                    if (index2 >= 0)
                    {
                        mJobData.mStations[StationName][InspectionName].DataBindings.RemoveAt(index2);
                    }
                }
                else
                {
                    mCommData.Dic[Key].Inputs[i].HasRelation = false;
                    string sourcePath = "";
                    string destinationPath = Key + "." + mCommData.Dic[Key].Inputs.GetKeys()[i];
                    int index = mJobData.mStations[StationName][InspectionName].DataBindings.GetSourcePath(DataSource.Communication, destinationPath, out sourcePath);
                    if (index >= 0)
                    {
                        mJobData.mStations[StationName][InspectionName].DataBindings.RemoveAt(index);
                    }
                }
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Dgv_AddControl(DataGridView dgv, Control Ctrl, int Col, int Row)
        {
            Rectangle rect = dgv.GetCellDisplayRectangle(Col, Row, cutOverflow: true);
            Ctrl.Location = new Point(rect.Left, rect.Top);
            Ctrl.Size = rect.Size;
            Type dgvType = Ctrl.GetType();
            PropertyInfo properInfo = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            properInfo.SetValue(Ctrl, true, null);
            dgv.Controls.Add(Ctrl);
        }

        private void dgv_InOut_Scroll(object sender, ScrollEventArgs e)
        {
            DataGridView dgv = dgv_InOut;
            if (dgv.CurrentCell == null)
            {
                return;
            }
            foreach (Control ctl in dgv.Controls)
            {
                ctl.Left -= dgv.HorizontalScrollingOffset - LastHorizontalScrollingOffset;
                ctl.Top -= dgv.VerticalScrollingOffset - LastVerticalScrollingOffset;
                if (dgv.FirstDisplayedScrollingRowIndex > dgv.CurrentCell.RowIndex)
                {
                    ctl.Visible = false;
                }
                else
                {
                    ctl.Visible = true;
                }
            }
            LastVerticalScrollingOffset = dgv.VerticalScrollingOffset;
            LastHorizontalScrollingOffset = dgv.HorizontalScrollingOffset;
        }

        private void dgv_InOut_SizeChanged(object sender, EventArgs e)
        {
            DataGridView dgv = dgv_InOut;
            foreach (Control ctl in dgv.Controls)
            {
                ctl.Left -= dgv.HorizontalScrollingOffset - LastHorizontalScrollingOffset;
                ctl.Top -= dgv.VerticalScrollingOffset - LastVerticalScrollingOffset;
                if (dgv.CurrentCell != null)
                {
                    if (dgv.FirstDisplayedScrollingRowIndex > dgv.CurrentCell.RowIndex)
                    {
                        ctl.Visible = false;
                    }
                    else
                    {
                        ctl.Visible = true;
                    }
                    ctl.Refresh();
                }
            }
            LastVerticalScrollingOffset = dgv.VerticalScrollingOffset;
            LastHorizontalScrollingOffset = dgv.HorizontalScrollingOffset;
        }

        private void dgv_InOut_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgv_InOut;
            if (dgv.CurrentCell == null)
            {
                return;
            }
            if (dgv.CurrentCell.ValueType.Name == "Boolean" && dgv.CurrentCell.ColumnIndex == 2)
            {
                AddCmbInDgvCell(dgv, ref cmb, 2, dgv.CurrentCell.RowIndex, () => new object[2] { true, false });
                if (dgv.CurrentCell.Value == null)
                {
                    cmb.SelectedIndex = -1;
                    cmb.SelectedItem = null;
                }
                else if ((bool)dgv.CurrentCell.Value)
                {
                    cmb.SelectedIndex = 0;
                }
                else
                {
                    cmb.SelectedIndex = 1;
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 5 && dgv[dgv.CurrentCell.ColumnIndex - 1, dgv.CurrentCell.RowIndex].Value != null && (bool)dgv[dgv.CurrentCell.ColumnIndex - 1, dgv.CurrentCell.RowIndex].Value)
            {
                AddCmbInDgvCell(dgv, ref cmb, 5, dgv.CurrentCell.RowIndex, delegate
                {
                    string key = Key;
                    if (ModuleType != "Algorithm")
                    {
                        key = "";
                    }
                    List<string> dataPaths = mAlgModuleData.GetDataPaths((string)dgv[1, dgv.CurrentCell.RowIndex].Value, key);
                    for (int i = 0; i < dataPaths.Count; i++)
                    {
                        dataPaths[i] = "[A]." + dataPaths[i];
                    }
                    object[] array = dataPaths.ToArray();
                    object[] array2 = array;
                    List<string> dataPaths2 = mParams.GetDataPaths((string)dgv[1, dgv.CurrentCell.RowIndex].Value, Key);
                    for (int j = 0; j < dataPaths2.Count; j++)
                    {
                        dataPaths2[j] = "[In]." + dataPaths2[j];
                    }
                    array = dataPaths2.ToArray();
                    object[] array3 = array;
                    string key2 = Key;
                    if (ModuleType != "CommunicationTable" && ModuleType != "CommunicationTable_A")
                    {
                        key2 = "";
                    }
                    List<string> dataPaths3 = mCommData.GetDataPaths((string)dgv[1, dgv.CurrentCell.RowIndex].Value, key2);
                    for (int k = 0; k < dataPaths3.Count; k++)
                    {
                        dataPaths3[k] = "[C]." + dataPaths3[k];
                    }
                    array = dataPaths3.ToArray();
                    object[] array4 = array;
                    object[] array5 = new object[array2.Length + array3.Length + array4.Length];
                    Array.Copy(array2, array5, array2.Length);
                    Array.Copy(array3, 0, array5, array2.Length, array3.Length);
                    Array.Copy(array4, 0, array5, array2.Length + array3.Length, array4.Length);
                    return array5;
                });
            }
            else
            {
                dgv.Controls.RemoveByKey("cmb");
            }
        }

        private void AddCmbInDgvCell(DataGridView dgv, ref ComboBox cmb, int Col, int Row, Func<object[]> func)
        {
            if (dgv.Controls.ContainsKey("cmb"))
            {
                dgv.Controls.RemoveByKey("cmb");
            }
            cmb = new ComboBox();
            cmb.Name = "cmb";
            cmb.SelectedIndexChanged -= Cmb_SelectedIndexChanged;
            cmb.SelectedIndexChanged += Cmb_SelectedIndexChanged;
            cmb.Items.Clear();
            cmb.Items.AddRange(func());
            LastCmbPositionX = dgv.SelectedCells[0].RowIndex;
            LastCmbPositionY = dgv.SelectedCells[0].ColumnIndex;
            cmb.BackColor = Color.LightGray;
            Dgv_AddControl(dgv, cmb, Col, Row);
        }

        private void Cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataGridView dgv = dgv_InOut;
            if (!(bool)dgv_InOut[4, LastCmbPositionX].Value || !(Item != cmb.SelectedItem.ToString()))
            {
                return;
            }
            Item = cmb.SelectedItem.ToString();
            dgv_InOut[LastCmbPositionY, LastCmbPositionX].Value = cmb.SelectedItem;
            string[] Names = cmb.SelectedItem.ToString().Split('.');
            if (ModuleType == "Algorithm")
            {
                string sourcePath3 = "";
                string destinationPath3 = Key + "." + mAlgModuleData.Dic[Key].Inputs.GetKeys()[LastCmbPositionX];
                int index3 = mJobData.mStations[StationName][InspectionName].DataBindings.GetSourcePath(DataSource.Algorithm, destinationPath3, out sourcePath3);
                if (index3 >= 0)
                {
                    mJobData.mStations[StationName][InspectionName].DataBindings.RemoveAt(index3);
                }
            }
            else if (ModuleType == "CommunicationTable" || ModuleType == "CommunicationTable_A")
            {
                string sourcePath = "";
                string destinationPath = Key + "." + mCommData.Dic[Key].Inputs.GetKeys()[LastCmbPositionX];
                int index = mJobData.mStations[StationName][InspectionName].DataBindings.GetSourcePath(DataSource.Communication, destinationPath, out sourcePath);
                if (index >= 0)
                {
                    mJobData.mStations[StationName][InspectionName].DataBindings.RemoveAt(index);
                }
            }
            if (Names[0] == "[A]")
            {
                dgv_InOut[2, LastCmbPositionX].Value = mAlgModuleData.Dic[Names[1]].Outputs[Names[2]].Value.mValue;
            }
            else if (Names[0] == "[In]")
            {
                dgv_InOut[2, LastCmbPositionX].Value = mParams.Params[Names[1]].Elements[Names[2]].Value.mValue;
            }
            else if (Names[0] == "[C]")
            {
                dgv_InOut[2, LastCmbPositionX].Value = mCommData.Dic[Names[1]].Outputs[Names[2]].Value.mValue;
            }
        }

        private void dgv_InOut_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dgv_InOut;
            if (dgv.CurrentCell.ColumnIndex != 3)
            {
                return;
            }
            string type = dgv[1, dgv.CurrentCell.RowIndex].Value.ToString();
            if (type.Substring(0, 4) == "List" || type.Substring(type.Length - 2, 2) == "[]")
            {
                int index2 = dgv.CurrentCell.RowIndex;
                if (ModuleType == "Algorithm")
                {
                    ValueShowForm valueEditForm4 = new ValueShowForm(mAlgModuleData.Dic[Key].Inputs[index2]);
                    valueEditForm4.ShowDialog();
                }
                else if (ModuleType == "CommunicationTable")
                {
                    ValueShowForm valueEditForm3 = new ValueShowForm(mCommData.Dic[Key].Inputs[index2]);
                    valueEditForm3.ShowDialog();
                }
                else if (ModuleType == "CommunicationTable_A")
                {
                    ValueShowForm valueEditForm2 = new ValueShowForm(mCommData.Dic[Key].Inputs[index2]);
                    valueEditForm2.ShowDialog();
                }
            }
        }

        private void dgv_InOut_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && dgv_InOut.Rows.Count > 0 && e.ColumnIndex == 2)
            {
                dgv_InOut.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = MyTypeConvert.ToStringValue(dgv_InOut.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            }
        }
    }
}
