using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Collection;
using NovaVision.BaseClass.Communication.CommData;
using NovaVision.BaseClass.Module;
using NovaVision.BaseClass.VisionConfig;
using Info = NovaVision.BaseClass.Communication.CommData.Info;

namespace NovaVision.VisionForm.CommunicationFrm
{
    public partial class CommInOutConfigForm : Form
    {
        private JobData mJobData;

        private ModuleData<Comm_Element, Info> mCommData;

        private string key = "";

        private ComboBox cmb;

        private int LastCmbPositionX = 0;

        private int LastCmbPositionY = 0;

        private string Item = "";

        private int LastVerticalScrollingOffset;

        private int LastHorizontalScrollingOffset;

        public CommInOutConfigForm(JobData jobData)
        {
            this.InitializeComponent();
            this.mJobData = jobData;
            this.mCommData = this.mJobData.mCommData;
            this.LineCtrl_Enable(false);
            this.InitDgv(this.mCommDataConfig_Ctrl.DgvIn, "in");
            this.InitDgv(this.mCommDataConfig_Ctrl.DgvOut, "out");
            this.InitListBoxNames();
        }

        private void LineCtrl_Enable(bool state)
        {
            this.mCommDataConfig_Ctrl.tsBtn_NewLine_In.Enabled = state;
            this.mCommDataConfig_Ctrl.tsBtn_DeleteLine_In.Enabled = state;
            this.mCommDataConfig_Ctrl.tsBtn_NewLine_out.Enabled = state;
            this.mCommDataConfig_Ctrl.tsBtn_DeleteLine_out.Enabled = state;
            this.mCommDataConfig_Ctrl.tsBtn_Up_In.Enabled = state;
            this.mCommDataConfig_Ctrl.tsBtn_Down_In.Enabled = state;
            this.mCommDataConfig_Ctrl.tsBtn_Up_Out.Enabled = state;
            this.mCommDataConfig_Ctrl.tsBtn_Down_Out.Enabled = state;
            this.mCommDataConfig_Ctrl.Txt_StartByteIn.Enabled = state;
            this.mCommDataConfig_Ctrl.Txt_StartByteOut.Enabled = state;
            this.mCommDataConfig_Ctrl.Txt_EndByteIn.Enabled = state;
            this.mCommDataConfig_Ctrl.Txt_EndByteOut.Enabled = state;
        }

        private void InitListBoxNames()
        {
            this.mCommDataConfig_Ctrl.ListBoxNames.Items.Clear();
            string[] keys = this.mCommData.Dic.GetKeys().ToArray();
            bool flag = keys != null;
            if (flag)
            {
                ListBox.ObjectCollection items = this.mCommDataConfig_Ctrl.ListBoxNames.Items;
                object[] items2 = keys;
                items.AddRange(items2);
            }
            this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex = -1;
        }

        private void InitDgv(DataGridView dgv, string strInOut)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.RowHeadersVisible = false;
            dgv.Columns.Add("Name", "变量名");
            dgv.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            DataGridViewComboBoxColumn cmbCol = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxCell.ObjectCollection items = cmbCol.Items;
            object[] items2 = new string[]
            {
                "Boolean",
                "SByte",
                "Byte",
                "Char",
                "UInt16",
                "UInt32",
                "UInt64",
                "Int16",
                "Int32",
                "Int64",
                "Single",
                "Double",
                "String"
            };
            items.AddRange(items2);
            cmbCol.DefaultCellStyle.NullValue = "Byte";
            dgv.Columns.Add(cmbCol);
            dgv.Columns[1].Name = "ValueType";
            dgv.Columns[1].HeaderText = "变量类型";
            dgv.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns.Add("ByteOffset", "字节偏移量");
            dgv.Columns[2].ValueType = typeof(int);
            dgv.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns.Add("ByteNum", "占用字节数");
            dgv.Columns[3].ValueType = typeof(int);
            dgv.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns.Add("Channel", "通道号");
            dgv.Columns[4].ValueType = typeof(int);
            dgv.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            bool flag = strInOut == "in";
            if (flag)
            {
                DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
                btnCol.DefaultCellStyle.NullValue = "编辑";
                dgv.Columns.Add(btnCol);
                dgv.Columns[5].Name = "EditValue";
                dgv.Columns[5].HeaderText = "编辑值";
                dgv.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgv.Columns.Add("explain", "注释");
                dgv.Columns[6].ValueType = typeof(string);
                dgv.Columns[6].Width = 150;
                dgv.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            bool flag2 = strInOut == "out";
            if (flag2)
            {
                DataGridViewCheckBoxColumn CheckBoxCol = new DataGridViewCheckBoxColumn();
                dgv.Columns.Add(CheckBoxCol);
                dgv.Columns[5].HeaderText = "是否配置为反馈点";
                dgv.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgv.Columns.Add("In", "输入项");
                dgv.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
                DataGridViewButtonColumn btnCol2 = new DataGridViewButtonColumn();
                btnCol2.DefaultCellStyle.NullValue = "编辑";
                dgv.Columns.Add(btnCol2);
                dgv.Columns[7].Name = "EditValue";
                dgv.Columns[7].HeaderText = "编辑值";
                dgv.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgv.Columns.Add("explain", "注释");
                dgv.Columns[8].ValueType = typeof(string);
                dgv.Columns[8].Width = 150;
                dgv.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void mCommDataConfig_Ctrl_BtnAddClick(object sender, EventArgs e)
        {
            ConfigNameForm configNameForm = new ConfigNameForm();
            configNameForm.ShowDialog();
            string name = configNameForm.Name;
            bool flag = configNameForm.Flag;
            if (flag)
            {
                bool flag2 = !this.mCommData.Dic.ContainsKey(name);
                if (flag2)
                {
                    this.mCommData.Dic.Add(name, new InputsOutputs<Comm_Element, Info>());
                    bool flag3 = this.key != "" && this.mCommData.Dic.ContainsKey(this.key);
                    if (flag3)
                    {
                        this.Read(this.key);
                        this.mJobData.SaveAllData();
                    }
                    this.mCommDataConfig_Ctrl.ListBoxNames.Items.Add(name);
                }
                else
                {
                    MessageBox.Show(@"通讯配置名与已有配置重名，请重新输入");
                }
            }
        }

        private void mCommDataConfig_Ctrl_BtnDeleteClick(object sender, EventArgs e)
        {
            bool flag = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex < 0;
            if (flag)
            {
                MessageBox.Show(@"请选择要删除的通讯配置！");
            }
            else
            {
                DialogResult dr = MessageBox.Show("是否删除配置：" + this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem.ToString() + " ?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                bool flag2 = dr == DialogResult.OK;
                if (flag2)
                {
                    bool flag3 = Directory.Exists(this.mJobData.mSystemConfigData.CommunicationMoudlePath);
                    if (flag3)
                    {
                        bool flag4 = File.Exists(this.mJobData.CommDataFilePath);
                        if (flag4)
                        {
                            int index = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex;
                            bool flag5 = this.mCommData.Dic.Count > 1;
                            if (flag5)
                            {
                                this.mCommData.Dic.Remove(index);
                                this.mCommDataConfig_Ctrl.ListBoxNames.Items.RemoveAt(index);
                                this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex = index - 1;
                                this.mJobData.SaveAllData();
                            }
                            else
                            {
                                this.mCommData.Dic.Remove(index);
                                this.mCommDataConfig_Ctrl.ListBoxNames.Items.Clear();
                                File.Delete(this.mJobData.CommDataFilePath);
                                this.mCommDataConfig_Ctrl.DgvIn.Rows.Clear();
                                this.mCommDataConfig_Ctrl.DgvOut.Rows.Clear();
                                this.LineCtrl_Enable(false);
                                this.key = "";
                            }
                        }
                    }
                }
            }
        }

        private void mCommDataConfig_Ctrl_BtnSaveClick(object sender, EventArgs e)
        {
            bool flag = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex >= 0;
            if (flag)
            {
                int index = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex;
                string key = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem.ToString();
                this.Read(key);
                this.mJobData.SaveAllData();
                MessageBox.Show("通讯配置" + key + "保存成功!");
            }
            else
            {
                MessageBox.Show(@"请选择保存项？");
            }
        }

        private void mCommDataConfig_Ctrl_BtnMoveUpClick(object sender, EventArgs e)
        {
            int index = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex;
            bool flag = this.mCommData.Dic.MoveUp(index);
            if (flag)
            {
                this.mCommDataConfig_Ctrl.SelectIndexChanged -= this.mCommDataConfig_Ctrl_SelectIndexChanged;
                this.mCommDataConfig_Ctrl.ListBoxNames.Items.Clear();
                ListBox.ObjectCollection items = this.mCommDataConfig_Ctrl.ListBoxNames.Items;
                object[] items2 = this.mCommData.Dic.GetKeys().ToArray();
                items.AddRange(items2);
                this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex = index - 1;
                this.mJobData.SaveAllData();
                this.mCommDataConfig_Ctrl.SelectIndexChanged += this.mCommDataConfig_Ctrl_SelectIndexChanged;
            }
        }

        private void mCommDataConfig_Ctrl_BtnMoveDownClick(object sender, EventArgs e)
        {
            int index = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex;
            bool flag = this.mCommData.Dic.MoveDown(index);
            if (flag)
            {
                this.mCommDataConfig_Ctrl.SelectIndexChanged -= this.mCommDataConfig_Ctrl_SelectIndexChanged;
                this.mCommDataConfig_Ctrl.ListBoxNames.Items.Clear();
                ListBox.ObjectCollection items = this.mCommDataConfig_Ctrl.ListBoxNames.Items;
                object[] items2 = this.mCommData.Dic.GetKeys().ToArray();
                items.AddRange(items2);
                this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex = index + 1;
                this.mJobData.SaveAllData();
                this.mCommDataConfig_Ctrl.SelectIndexChanged += this.mCommDataConfig_Ctrl_SelectIndexChanged;
            }
        }

        private void mCommDataConfig_Ctrl_SelectIndexChanged(object sender, EventArgs e)
        {
            bool flag = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem != null;
            if (flag)
            {
                bool flag2 = this.key != this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem.ToString();
                if (flag2)
                {
                    bool flag3 = this.key != "" && this.mCommData.Dic.ContainsKey(this.key);
                    if (flag3)
                    {
                        this.Read(this.key);
                        this.mJobData.SaveAllData();
                    }
                    this.key = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem.ToString();
                    this.UnRegisterEvent();
                    this.Write(this.key);
                    this.LineCtrl_Enable(true);
                    this.RegisterEvent();
                    this.OperaterChange(OperaterType.SelectIndexChanged);
                }
            }
        }

        private void mCommDataConfig_Ctrl_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int index = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex;
            string oldKey = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem.ToString();
            string newKey = textBox.Text;
            bool flag2 = oldKey != newKey;
            if (flag2)
            {
                bool flag = this.mCommData.Dic.Replace(oldKey, newKey);
                bool flag3 = flag;
                if (flag3)
                {
                    this.mCommDataConfig_Ctrl.SelectIndexChanged -= this.mCommDataConfig_Ctrl_SelectIndexChanged;
                    this.key = newKey;
                    this.Read(this.key);
                    this.mJobData.SaveAllData();
                    this.mCommDataConfig_Ctrl.ListBoxNames.Items.Clear();
                    ListBox.ObjectCollection items = this.mCommDataConfig_Ctrl.ListBoxNames.Items;
                    object[] items2 = this.mCommData.Dic.GetKeys().ToArray();
                    items.AddRange(items2);
                    this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex = index;
                    this.mCommDataConfig_Ctrl.SelectIndexChanged += this.mCommDataConfig_Ctrl_SelectIndexChanged;
                }
            }
        }

        private void Read(string key)
        {
            this.ReadTextBox(key, this.mCommDataConfig_Ctrl.Txt_StartByteIn, this.mCommDataConfig_Ctrl.Txt_EndByteIn, this.mCommData.Dic[key].OutputsInfo);
            this.ReadTextBox(key, this.mCommDataConfig_Ctrl.Txt_StartByteOut, this.mCommDataConfig_Ctrl.Txt_EndByteOut, this.mCommData.Dic[key].InputsInfo);
            this.ReadFromDgv_In(key, this.mCommDataConfig_Ctrl.DgvIn, this.mCommData.Dic[key].Outputs);
            this.ReadFromDgv_Out(key, this.mCommDataConfig_Ctrl.DgvOut, this.mCommData.Dic[key].Inputs);
        }

        private void Write(string key)
        {
            this.WriteTextBox(key, this.mCommDataConfig_Ctrl.Txt_StartByteIn, this.mCommDataConfig_Ctrl.Txt_EndByteIn, this.mCommData.Dic[key].OutputsInfo);
            this.WriteTextBox(key, this.mCommDataConfig_Ctrl.Txt_StartByteOut, this.mCommDataConfig_Ctrl.Txt_EndByteOut, this.mCommData.Dic[key].InputsInfo);
            this.WriteToDgv_In(this.mCommDataConfig_Ctrl.DgvIn, this.mCommData.Dic[key].Outputs);
            this.WriteToDgv_Out(this.mCommDataConfig_Ctrl.DgvOut, this.mCommData.Dic[key].Inputs, this.mCommData.Dic[key].DataBindings);
        }

        private void WriteTextBox(string key, TextBox StartByte, TextBox EndByte, Info info)
        {
            StartByte.Text = info.StartByte.ToString();
            EndByte.Text = info.EndByte.ToString();
        }

        private void ReadTextBox(string key, TextBox StartByte, TextBox EndByte, Info info)
        {
            bool flag = StartByte.Text != "" && EndByte.Text != "";
            if (flag)
            {
                info.StartByte = Convert.ToInt32(StartByte.Text);
                info.EndByte = Convert.ToInt32(EndByte.Text);
            }
        }

        private void WriteToDgv_Out(DataGridView dgv, MyDictionaryEx<Comm_Element> elements, DataBindingCollection dataBindings)
        {
            dgv.Rows.Clear();
            for (int i = 0; i < elements.Count; i++)
            {
                dgv.Rows.Add();
                dgv[0, i].Value = elements[i].Name;
                dgv[1, i].Style.NullValue = elements[i].Type;
                dgv[2, i].Value = elements[i].ByteOffset;
                dgv[3, i].Value = elements[i].TypeLength;
                bool flag = elements[i].Type != "String";
                if (flag)
                {
                    dgv[3, i].ReadOnly = true;
                    dgv[3, i].Style.BackColor = Color.LightGray;
                }
                dgv[4, i].Value = elements[i].Channel;
                string sourcePath = "";
                int index = dataBindings.GetSourcePath(DataSource.Communication, elements[i].Name, out sourcePath);
                bool flag2 = index >= 0;
                if (flag2)
                {
                    dgv[5, i].Value = true;
                    dgv[6, i].Value = sourcePath;
                }
                dgv[8, i].Value = elements[i].Explain;
            }
        }

        private void WriteToDgv_In(DataGridView dgv, MyDictionaryEx<Comm_Element> elements)
        {
            dgv.Rows.Clear();
            for (int i = 0; i < elements.Count; i++)
            {
                dgv.Rows.Add();
                dgv[0, i].Value = elements[i].Name;
                dgv[1, i].Style.NullValue = elements[i].Type;
                dgv[2, i].Value = elements[i].ByteOffset;
                dgv[3, i].Value = elements[i].TypeLength;
                bool flag = elements[i].Type != "String";
                if (flag)
                {
                    dgv[3, i].ReadOnly = true;
                    dgv[3, i].Style.BackColor = Color.LightGray;
                }
                dgv[4, i].Value = elements[i].Channel;
                dgv[6, i].Value = elements[i].Explain;
            }
        }

        private void ReadFromDgv_Out(string key, DataGridView dgv, MyDictionaryEx<Comm_Element> elements)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                elements[i].Type = dgv[1, i].EditedFormattedValue.ToString();
                elements[i].ByteOffset = Convert.ToInt32(dgv[2, i].Value);
                elements[i].TypeLength = Convert.ToInt32(dgv[3, i].Value);
                elements[i].Channel = Convert.ToInt32(dgv[4, i].Value);
                bool flag = dgv[8, i].Value != null;
                if (flag)
                {
                    elements[i].Explain = dgv[8, i].Value.ToString();
                }
            }
        }

        private void ReadFromDgv_In(string key, DataGridView dgv, MyDictionaryEx<Comm_Element> elements)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                elements[i].Type = dgv[1, i].EditedFormattedValue.ToString();
                elements[i].ByteOffset = Convert.ToInt32(dgv[2, i].Value);
                elements[i].TypeLength = Convert.ToInt32(dgv[3, i].Value);
                elements[i].Channel = Convert.ToInt32(dgv[4, i].Value);
                bool flag = dgv[6, i].Value != null;
                if (flag)
                {
                    elements[i].Explain = dgv[6, i].Value.ToString();
                }
            }
        }

        private void RegisterEvent()
        {
            this.mCommDataConfig_Ctrl.Txt_StartByteIn.TextChanged += this.Txt_StartByteIn_TextChanged;
            this.mCommDataConfig_Ctrl.Txt_EndByteIn.TextChanged += this.Txt_EndByteIn_TextChanged;
            this.mCommDataConfig_Ctrl.Txt_StartByteOut.TextChanged += this.Txt_StartByteOut_TextChanged;
            this.mCommDataConfig_Ctrl.Txt_EndByteOut.TextChanged += this.Txt_EndByteOut_TextChanged;
            this.mCommDataConfig_Ctrl.DgvIn.EditingControlShowing += this.DgvIn_EditingControlShowing;
            this.mCommDataConfig_Ctrl.DgvOut.EditingControlShowing += this.DgvOut_EditingControlShowing;
            this.mCommDataConfig_Ctrl.DgvIn.CellContentClick += this.DgvIn_CellContentClick;
            this.mCommDataConfig_Ctrl.DgvOut.CellContentClick += this.DgvOut_CellContentClick;
            this.mCommDataConfig_Ctrl.DgvIn.CellEndEdit += this.DgvIn_CellEndEdit;
            this.mCommDataConfig_Ctrl.DgvOut.CellEndEdit += this.DgvOut_CellEndEdit;
            this.mCommDataConfig_Ctrl.DgvOut.Click += this.Dgv_Click;
            this.mCommDataConfig_Ctrl.DgvOut.Scroll += this.Dgv_Scroll;
            this.mCommDataConfig_Ctrl.DgvOut.SizeChanged += this.Dgv_SizeChanged;
        }

        private void UnRegisterEvent()
        {
            this.mCommDataConfig_Ctrl.Txt_StartByteIn.TextChanged -= this.Txt_StartByteIn_TextChanged;
            this.mCommDataConfig_Ctrl.Txt_EndByteIn.TextChanged -= this.Txt_EndByteIn_TextChanged;
            this.mCommDataConfig_Ctrl.Txt_StartByteOut.TextChanged -= this.Txt_StartByteOut_TextChanged;
            this.mCommDataConfig_Ctrl.Txt_EndByteOut.TextChanged -= this.Txt_EndByteOut_TextChanged;
            this.mCommDataConfig_Ctrl.DgvIn.EditingControlShowing -= this.DgvIn_EditingControlShowing;
            this.mCommDataConfig_Ctrl.DgvOut.EditingControlShowing -= this.DgvOut_EditingControlShowing;
            this.mCommDataConfig_Ctrl.DgvIn.CellContentClick -= this.DgvIn_CellContentClick;
            this.mCommDataConfig_Ctrl.DgvOut.CellContentClick -= this.DgvOut_CellContentClick;
            this.mCommDataConfig_Ctrl.DgvIn.CellEndEdit -= this.DgvIn_CellEndEdit;
            this.mCommDataConfig_Ctrl.DgvOut.CellEndEdit -= this.DgvOut_CellEndEdit;
            this.mCommDataConfig_Ctrl.DgvOut.Click -= this.Dgv_Click;
            this.mCommDataConfig_Ctrl.DgvOut.Scroll -= this.Dgv_Scroll;
            this.mCommDataConfig_Ctrl.DgvOut.SizeChanged -= this.Dgv_SizeChanged;
        }

        private void mCommDataConfig_Ctrl_BtnNewLineIn_Click(object sender, EventArgs e)
        {
            int i = 1;
            int selectedIndex = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex;
            DataGridView dgv = this.mCommDataConfig_Ctrl.DgvIn;
            dgv.Rows.Add();
            int RowCount = dgv.RowCount;
            string key = "Value" + i.ToString();
            while (this.mCommData.Dic[selectedIndex].Outputs.ContainsKey(key))
            {
                key = "Value" + i.ToString();
                i++;
            }
            this.mCommData.Dic[selectedIndex].Outputs.Add(key, new Comm_Element());
            this.mCommData.Dic[selectedIndex].Outputs[key].Name = key;
            this.mCommData.Dic[selectedIndex].Outputs[key].Type = "Byte";
            dgv[0, RowCount - 1].Value = key;
            bool flag = RowCount == 1;
            int byteOffset;
            if (flag)
            {
                byteOffset = 0;
            }
            else
            {
                byteOffset = Convert.ToInt32(dgv[2, RowCount - 2].Value) + Convert.ToInt32(dgv[3, RowCount - 2].Value);
            }
            this.mCommData.Dic[selectedIndex].Outputs[key].ByteOffset = byteOffset;
            this.mCommData.Dic[selectedIndex].Outputs[key].TypeLength = 1;
            dgv[2, RowCount - 1].Value = byteOffset;
            dgv[3, RowCount - 1].Value = 1;
            dgv[3, RowCount - 1].ReadOnly = true;
            dgv[3, RowCount - 1].Style.BackColor = Color.LightGray;
            this.OperaterChange(OperaterType.BtnNewLine_In);
        }

        private void mCommDataConfig_Ctrl_BtnNewLineOut_Click(object sender, EventArgs e)
        {
            int i = 1;
            int selectedIndex = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex;
            DataGridView dgv = this.mCommDataConfig_Ctrl.DgvOut;
            dgv.Rows.Add();
            int RowCount = dgv.RowCount;
            string key = "Value" + i.ToString();
            while (this.mCommData.Dic[selectedIndex].Inputs.ContainsKey(key))
            {
                key = "Value" + i.ToString();
                i++;
            }
            this.mCommData.Dic[selectedIndex].Inputs.Add(key, new Comm_Element());
            this.mCommData.Dic[selectedIndex].Inputs[key].Name = key;
            this.mCommData.Dic[selectedIndex].Inputs[key].Type = "Byte";
            dgv[0, RowCount - 1].Value = key;
            bool flag = RowCount == 1;
            int byteOffset;
            if (flag)
            {
                byteOffset = 0;
            }
            else
            {
                byteOffset = Convert.ToInt32(dgv[2, RowCount - 2].Value) + Convert.ToInt32(dgv[3, RowCount - 2].Value);
            }
            this.mCommData.Dic[selectedIndex].Inputs[key].ByteOffset = byteOffset;
            this.mCommData.Dic[selectedIndex].Inputs[key].TypeLength = 1;
            dgv[2, RowCount - 1].Value = byteOffset;
            dgv[3, RowCount - 1].Value = 1;
            dgv[3, RowCount - 1].ReadOnly = true;
            dgv[3, RowCount - 1].Style.BackColor = Color.LightGray;
            this.OperaterChange(OperaterType.BtnNewLine_Out);
        }

        private void mCommDataConfig_Ctrl_BtnDeleteLineIn_Click(object sender, EventArgs e)
        {
            DataGridView dgv = this.mCommDataConfig_Ctrl.DgvIn;
            bool flag = dgv.Rows.Count > 0;
            if (flag)
            {
                int selectedIndex = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex;
                int outputSelectedIndex = dgv.SelectedCells[0].RowIndex;
                this.RemovingBindngBySource(this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem.ToString(), outputSelectedIndex);
                this.WriteToDgv_Out(this.mCommDataConfig_Ctrl.DgvOut, this.mCommData.Dic[this.key].Inputs, this.mCommData.Dic[this.key].DataBindings);
                dgv.Rows.RemoveAt(outputSelectedIndex);
                this.mCommData.Dic[selectedIndex].Outputs.Remove(outputSelectedIndex);
                this.OperaterChange(OperaterType.BtnDeleteLine_In);
            }
        }

        private void mCommDataConfig_Ctrl_BtnDeleteLineOut_Click(object sender, EventArgs e)
        {
            DataGridView dgv = this.mCommDataConfig_Ctrl.DgvOut;
            bool flag = dgv.Rows.Count > 0;
            if (flag)
            {
                int selectedIndex = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex;
                int inputSelectedIndex = dgv.SelectedCells[0].RowIndex;
                this.RemoveBinding(this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem.ToString(), inputSelectedIndex);
                dgv.Rows.RemoveAt(inputSelectedIndex);
                this.mCommData.Dic[selectedIndex].Inputs.Remove(inputSelectedIndex);
                this.OperaterChange(OperaterType.BtnDeleteLine_Out);
            }
        }

        private void mCommDataConfig_Ctrl_BtnDownLineIn_Click(object sender, EventArgs e)
        {
            DataGridView dgv = this.mCommDataConfig_Ctrl.DgvIn;
            bool flag = dgv.Rows.Count > 0;
            if (flag)
            {
                int selectedIndex = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex;
                int colIndex = dgv.CurrentCell.ColumnIndex;
                int rowIndex = dgv.CurrentCell.RowIndex;
                bool flag2 = rowIndex + 1 < dgv.Rows.Count && this.mCommData.Dic[selectedIndex].Outputs.MoveDown(rowIndex);
                if (flag2)
                {
                    DataGridViewRow tempRowUp = dgv.Rows[rowIndex];
                    DataGridViewRow tempRowDown = dgv.Rows[rowIndex + 1];
                    dgv.Rows.RemoveAt(rowIndex);
                    dgv.Rows.RemoveAt(rowIndex);
                    dgv.Rows.InsertRange(rowIndex, new DataGridViewRow[]
                    {
                        tempRowDown,
                        tempRowUp
                    });
                    dgv.CurrentCell = dgv[colIndex, rowIndex + 1];
                    this.OperaterChange(OperaterType.BtnLineDown_In);
                }
            }
        }

        private void mCommDataConfig_Ctrl_BtnDownLineOut_Click(object sender, EventArgs e)
        {
            DataGridView dgv = this.mCommDataConfig_Ctrl.DgvOut;
            bool flag = dgv.Rows.Count > 0;
            if (flag)
            {
                int selectedIndex = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex;
                int colIndex = dgv.CurrentCell.ColumnIndex;
                int rowIndex = dgv.CurrentCell.RowIndex;
                bool flag2 = rowIndex + 1 < dgv.Rows.Count && this.mCommData.Dic[selectedIndex].Inputs.MoveDown(rowIndex);
                if (flag2)
                {
                    DataGridViewRow tempRowUp = dgv.Rows[rowIndex];
                    DataGridViewRow tempRowDown = dgv.Rows[rowIndex + 1];
                    dgv.Rows.RemoveAt(rowIndex);
                    dgv.Rows.RemoveAt(rowIndex);
                    dgv.Rows.InsertRange(rowIndex, new DataGridViewRow[]
                    {
                        tempRowDown,
                        tempRowUp
                    });
                    dgv.CurrentCell = dgv[colIndex, rowIndex + 1];
                    this.OperaterChange(OperaterType.BtnLineDown_Out);
                }
            }
        }

        private void mCommDataConfig_Ctrl_BtnUpLineIn_Click(object sender, EventArgs e)
        {
            DataGridView dgv = this.mCommDataConfig_Ctrl.DgvIn;
            bool flag = dgv.Rows.Count > 0;
            if (flag)
            {
                int selectedIndex = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex;
                int colIndex = dgv.CurrentCell.ColumnIndex;
                int rowIndex = dgv.CurrentCell.RowIndex;
                bool flag2 = rowIndex - 1 >= 0 && this.mCommData.Dic[selectedIndex].Outputs.MoveUp(rowIndex);
                if (flag2)
                {
                    DataGridViewRow tempRowUp = dgv.Rows[rowIndex - 1];
                    DataGridViewRow tempRowDown = dgv.Rows[rowIndex];
                    dgv.Rows.RemoveAt(rowIndex - 1);
                    dgv.Rows.RemoveAt(rowIndex - 1);
                    dgv.Rows.InsertRange(rowIndex - 1, new DataGridViewRow[]
                    {
                        tempRowDown,
                        tempRowUp
                    });
                    dgv.CurrentCell = dgv[colIndex, rowIndex - 1];
                    this.OperaterChange(OperaterType.BtnLineUp_In);
                }
            }
        }

        private void mCommDataConfig_Ctrl_BtnUpLineOut_Click(object sender, EventArgs e)
        {
            DataGridView dgv = this.mCommDataConfig_Ctrl.DgvOut;
            bool flag = dgv.Rows.Count > 0;
            if (flag)
            {
                int selectedIndex = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex;
                int colIndex = dgv.CurrentCell.ColumnIndex;
                int rowIndex = dgv.CurrentCell.RowIndex;
                bool flag2 = rowIndex - 1 >= 0 && this.mCommData.Dic[selectedIndex].Inputs.MoveUp(rowIndex);
                if (flag2)
                {
                    DataGridViewRow tempRowUp = dgv.Rows[rowIndex - 1];
                    DataGridViewRow tempRowDown = dgv.Rows[rowIndex];
                    dgv.Rows.RemoveAt(rowIndex - 1);
                    dgv.Rows.RemoveAt(rowIndex - 1);
                    dgv.Rows.InsertRange(rowIndex - 1, new DataGridViewRow[]
                    {
                        tempRowDown,
                        tempRowUp
                    });
                    dgv.CurrentCell = dgv[colIndex, rowIndex - 1];
                    this.OperaterChange(OperaterType.BtnLineUp_Out);
                }
            }
        }

        private void Txt_StartByteIn_TextChanged(object sender, EventArgs e)
        {
            bool flag = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem != null;
            if (flag)
            {
                string key = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem.ToString();
                this.ReadTextBox(key, this.mCommDataConfig_Ctrl.Txt_StartByteIn, this.mCommDataConfig_Ctrl.Txt_EndByteIn, this.mCommData.Dic[key].OutputsInfo);
            }
            this.OperaterChange(OperaterType.StartByteTextChanged_In);
        }

        private void Txt_EndByteIn_TextChanged(object sender, EventArgs e)
        {
            bool flag = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem != null;
            if (flag)
            {
                string key = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem.ToString();
                this.ReadTextBox(key, this.mCommDataConfig_Ctrl.Txt_StartByteIn, this.mCommDataConfig_Ctrl.Txt_EndByteIn, this.mCommData.Dic[key].OutputsInfo);
            }
            this.OperaterChange(OperaterType.EndByteTextChanged_In);
        }

        private void Txt_StartByteOut_TextChanged(object sender, EventArgs e)
        {
            bool flag = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem != null;
            if (flag)
            {
                string key = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem.ToString();
                this.ReadTextBox(key, this.mCommDataConfig_Ctrl.Txt_StartByteOut, this.mCommDataConfig_Ctrl.Txt_EndByteOut, this.mCommData.Dic[key].InputsInfo);
            }
            this.OperaterChange(OperaterType.StartByteTextChanged_Out);
        }

        private void Txt_EndByteOut_TextChanged(object sender, EventArgs e)
        {
            bool flag = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem != null;
            if (flag)
            {
                string key = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem.ToString();
                this.ReadTextBox(key, this.mCommDataConfig_Ctrl.Txt_StartByteOut, this.mCommDataConfig_Ctrl.Txt_EndByteOut, this.mCommData.Dic[key].InputsInfo);
            }
            this.OperaterChange(OperaterType.EndByteTextChanged_Out);
        }

        private void DgvIn_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dgv = this.mCommDataConfig_Ctrl.DgvIn;
            bool flag = dgv.CurrentCell.ColumnIndex == 1 && e.Control is ComboBox;
            if (flag)
            {
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.SelectedIndexChanged -= this.ComboBox_SelectedIndexChangedIn;
                comboBox.SelectedIndexChanged += this.ComboBox_SelectedIndexChangedIn;
            }
        }

        private void ComboBox_SelectedIndexChangedIn(object sender, EventArgs e)
        {
            DataGridView dgv = this.mCommDataConfig_Ctrl.DgvIn;
            ComboBox comboBox = sender as ComboBox;
            int selectedIndex = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex;
            int outputSelectedIndex = dgv.CurrentCell.RowIndex;
            int typeLength = MyTypeConvert.GetTypeLength(comboBox.SelectedItem.ToString());
            string type = comboBox.SelectedItem.ToString();
            bool flag = type != "String";
            if (flag)
            {
                dgv[3, dgv.CurrentCell.RowIndex].ReadOnly = true;
                dgv[3, dgv.CurrentCell.RowIndex].Style.BackColor = Color.LightGray;
            }
            else
            {
                dgv[3, dgv.CurrentCell.RowIndex].ReadOnly = false;
                dgv[3, dgv.CurrentCell.RowIndex].Style.BackColor = Color.White;
            }
            bool flag2 = this.mCommData.Dic[selectedIndex].Outputs[outputSelectedIndex].Type != type;
            if (flag2)
            {
                this.mCommData.Dic[selectedIndex].Outputs[outputSelectedIndex].SettingValues.Clear();
                this.RemovingBindngBySource(this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem.ToString(), outputSelectedIndex);
                this.WriteToDgv_Out(this.mCommDataConfig_Ctrl.DgvOut, this.mCommData.Dic[this.key].Inputs, this.mCommData.Dic[this.key].DataBindings);
                string sourcePath = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem.ToString() + "." + this.mCommData.Dic[this.key].Outputs.GetKeys()[outputSelectedIndex];
                this.mJobData.OutputsRemoving(sourcePath, DataSource.Communication);
            }
            dgv[3, dgv.CurrentCell.RowIndex].Value = typeLength;
            this.mCommData.Dic[selectedIndex].Outputs[outputSelectedIndex].TypeLength = typeLength;
            this.mCommData.Dic[selectedIndex].Outputs[outputSelectedIndex].Type = type;
            this.OperaterChange(OperaterType.DgvCellSelectedIndexChanged_In);
        }

        private void DgvOut_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dgv = this.mCommDataConfig_Ctrl.DgvOut;
            bool flag = dgv.CurrentCell.ColumnIndex == 1 && e.Control is ComboBox;
            if (flag)
            {
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.SelectedIndexChanged -= this.ComboBox_SelectedIndexChangedOut;
                comboBox.SelectedIndexChanged += this.ComboBox_SelectedIndexChangedOut;
            }
        }

        private void ComboBox_SelectedIndexChangedOut(object sender, EventArgs e)
        {
            DataGridView dgv = this.mCommDataConfig_Ctrl.DgvOut;
            ComboBox comboBox = sender as ComboBox;
            int selectedIndex = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex;
            int inputSelectedIndex = dgv.CurrentCell.RowIndex;
            int typeLength = MyTypeConvert.GetTypeLength(comboBox.SelectedItem.ToString());
            string type = comboBox.SelectedItem.ToString();
            bool flag = type != "String";
            if (flag)
            {
                dgv[3, dgv.CurrentCell.RowIndex].ReadOnly = true;
                dgv[3, dgv.CurrentCell.RowIndex].Style.BackColor = Color.LightGray;
            }
            else
            {
                dgv[3, dgv.CurrentCell.RowIndex].ReadOnly = false;
                dgv[3, dgv.CurrentCell.RowIndex].Style.BackColor = Color.White;
            }
            bool flag2 = this.mCommData.Dic[selectedIndex].Inputs[inputSelectedIndex].Type != type;
            if (flag2)
            {
                this.mCommData.Dic[selectedIndex].Inputs[inputSelectedIndex].SettingValues.Clear();
                this.RemoveBinding(this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem.ToString(), inputSelectedIndex);
                string destinationPath = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem.ToString() + "." + this.mCommData.Dic[this.key].Inputs.GetKeys()[inputSelectedIndex];
                this.mJobData.InputsRemoving(destinationPath, DataSource.Communication);
                dgv[5, inputSelectedIndex].Value = false;
                dgv[6, inputSelectedIndex].Value = null;
            }
            dgv[3, dgv.CurrentCell.RowIndex].Value = typeLength;
            this.mCommData.Dic[selectedIndex].Inputs[inputSelectedIndex].TypeLength = typeLength;
            this.mCommData.Dic[selectedIndex].Inputs[inputSelectedIndex].Type = type;
            this.OperaterChange(OperaterType.DgvCellSelectedIndexChanged_Out);
        }

        private void DgvIn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            bool flag = e.ColumnIndex == 5;
            if (flag)
            {
                int index = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex;
                int index_In = this.mCommDataConfig_Ctrl.DgvIn.CurrentCell.RowIndex;
                ValueEditForm valueEditForm = new ValueEditForm(this.mCommData.Dic[index].Outputs[index_In]);
                valueEditForm.ShowDialog();
            }
        }

        private void DgvOut_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = this.mCommDataConfig_Ctrl.DgvOut;
            bool flag = e.ColumnIndex == 5;
            if (flag)
            {
                string key = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem.ToString();
                int index_Out = dgv.CurrentCell.RowIndex;
                bool flag2 = !(bool)dgv.CurrentCell.EditedFormattedValue;
                if (flag2)
                {
                    this.RemoveBinding(key, index_Out);
                    dgv[6, index_Out].Value = null;
                }
            }
            bool flag3 = e.ColumnIndex == 7;
            if (flag3)
            {
                int index = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex;
                int index_Out2 = dgv.CurrentCell.RowIndex;
                ValueEditForm valueEditForm = new ValueEditForm(this.mCommData.Dic[index].Inputs[index_Out2]);
                valueEditForm.ShowDialog();
            }
        }

        private void DgvIn_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = this.mCommDataConfig_Ctrl.DgvIn;
            int selectedIndex = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex;
            int outputSelectedIndex = dgv.CurrentCell.RowIndex;
            bool flag = dgv.CurrentCell.ColumnIndex == 0;
            if (flag)
            {
                string oldKey = this.mCommData.Dic[selectedIndex].Outputs.GetKeys()[outputSelectedIndex];
                string newKey = dgv[0, outputSelectedIndex].Value.ToString();
                bool flag2 = oldKey != newKey;
                if (flag2)
                {
                    bool state = this.mCommData.Dic[selectedIndex].Outputs.Replace(oldKey, newKey);
                    bool flag3 = !state;
                    if (flag3)
                    {
                        MessageBox.Show(@"与现有项同名，请重新命名！");
                        dgv[0, outputSelectedIndex].Value = oldKey;
                    }
                    else
                    {
                        this.mCommData.Dic[selectedIndex].Outputs[newKey].Name = newKey;
                        this.ReplaceBindingBySource(this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem.ToString(), oldKey, newKey);
                        this.WriteToDgv_Out(this.mCommDataConfig_Ctrl.DgvOut, this.mCommData.Dic[this.key].Inputs, this.mCommData.Dic[this.key].DataBindings);
                    }
                }
            }
            else
            {
                bool flag4 = dgv.CurrentCell.ColumnIndex == 2 || dgv.CurrentCell.ColumnIndex == 3;
                if (flag4)
                {
                    this.OperaterChange(OperaterType.DgvCellEndEdit_In);
                }
            }
        }

        private void DgvOut_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = this.mCommDataConfig_Ctrl.DgvOut;
            int selectedIndex = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex;
            int inputSelectedIndex = dgv.CurrentCell.RowIndex;
            bool flag = dgv.CurrentCell.ColumnIndex == 0;
            if (flag)
            {
                string oldKey = this.mCommData.Dic[selectedIndex].Inputs.GetKeys()[inputSelectedIndex];
                string newKey = dgv[0, inputSelectedIndex].Value.ToString();
                bool flag2 = oldKey != newKey;
                if (flag2)
                {
                    bool state = this.mCommData.Dic[selectedIndex].Inputs.Replace(oldKey, newKey);
                    bool flag3 = !state;
                    if (flag3)
                    {
                        MessageBox.Show(@"与现有项同名，请重新命名！");
                        dgv[0, inputSelectedIndex].Value = oldKey;
                    }
                    else
                    {
                        this.mCommData.Dic[selectedIndex].Inputs[newKey].Name = newKey;
                        this.ReplaceBinding(this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem.ToString(), oldKey, newKey);
                    }
                }
            }
            else
            {
                bool flag4 = dgv.CurrentCell.ColumnIndex == 2 || dgv.CurrentCell.ColumnIndex == 3;
                if (flag4)
                {
                    this.OperaterChange(OperaterType.DgvCellEndEdit_Out);
                }
            }
        }

        private void Dgv_Click(object sender, EventArgs e)
        {
            DataGridView dgv = this.mCommDataConfig_Ctrl.DgvOut;
            bool flag = dgv.CurrentCell == null;
            if (!flag)
            {
                int index = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedIndex;
                int index2 = dgv.CurrentCell.RowIndex;
                bool flag2 = dgv.CurrentCell.ColumnIndex == 6 && (bool)dgv[5, index2].EditedFormattedValue;
                if (flag2)
                {
                    this.AddCmbInDgvCell(dgv, ref this.cmb, 6, index2, delegate
                    {
                        List<string> paths_Comm = this.mCommData.Dic[index].GetDataPaths((string)dgv[1, index2].EditedFormattedValue, "");
                        string[] item_Comm = paths_Comm.ToArray();
                        string[] item = new string[item_Comm.Length];
                        Array.Copy(item_Comm, item, item_Comm.Length);
                        return item;
                    });
                }
                else
                {
                    dgv.Controls.RemoveByKey("cmb");
                }
            }
        }

        private void AddCmbInDgvCell(DataGridView dgv, ref ComboBox cmb, int Col, int Row, Func<object[]> func)
        {
            bool flag = dgv.Controls.ContainsKey("cmb");
            if (flag)
            {
                dgv.Controls.RemoveByKey("cmb");
            }
            cmb = new ComboBox();
            cmb.Name = "cmb";
            cmb.SelectedIndexChanged -= this.Cmb_SelectedIndexChanged;
            cmb.SelectedIndexChanged += this.Cmb_SelectedIndexChanged;
            cmb.Items.Clear();
            cmb.Items.AddRange(func());
            this.LastCmbPositionX = dgv.SelectedCells[0].RowIndex;
            this.LastCmbPositionY = dgv.SelectedCells[0].ColumnIndex;
            cmb.BackColor = Color.LightGray;
            this.Dgv_AddControl(dgv, cmb, Col, Row);
        }

        private void Dgv_AddControl(DataGridView dgv, Control Ctrl, int Col, int Row)
        {
            Rectangle rect = dgv.GetCellDisplayRectangle(Col, Row, true);
            Ctrl.Location = new Point(rect.Left, rect.Top);
            Ctrl.Size = rect.Size;
            Type dgvType = Ctrl.GetType();
            PropertyInfo properInfo = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            properInfo.SetValue(Ctrl, true, null);
            dgv.Controls.Add(Ctrl);
        }

        private void Cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataGridView dgv = this.mCommDataConfig_Ctrl.DgvOut;
            bool flag = (bool)dgv[5, this.LastCmbPositionX].Value;
            if (flag)
            {
                bool flag2 = this.Item != this.cmb.SelectedItem.ToString();
                if (flag2)
                {
                    this.Item = this.cmb.SelectedItem.ToString();
                    dgv[this.LastCmbPositionY, this.LastCmbPositionX].Value = this.cmb.SelectedItem;
                    string Name = this.cmb.SelectedItem.ToString();
                    string key = this.mCommDataConfig_Ctrl.ListBoxNames.SelectedItem.ToString();
                    this.RemoveBinding(key, this.LastCmbPositionX);
                    bool flag3 = this.Item != "";
                    if (flag3)
                    {
                        this.AddBinding(key, this.LastCmbPositionX, this.Item);
                    }
                }
            }
        }

        private void Dgv_Scroll(object sender, ScrollEventArgs e)
        {
            DataGridView dgv = this.mCommDataConfig_Ctrl.DgvOut;
            bool flag = dgv.CurrentCell == null;
            if (!flag)
            {
                foreach (object obj in dgv.Controls)
                {
                    Control ctl = (Control)obj;
                    ctl.Left -= dgv.HorizontalScrollingOffset - this.LastHorizontalScrollingOffset;
                    ctl.Top -= dgv.VerticalScrollingOffset - this.LastVerticalScrollingOffset;
                    bool flag2 = dgv.FirstDisplayedScrollingRowIndex > dgv.CurrentCell.RowIndex;
                    if (flag2)
                    {
                        ctl.Visible = false;
                    }
                    else
                    {
                        ctl.Visible = true;
                    }
                }
                this.LastVerticalScrollingOffset = dgv.VerticalScrollingOffset;
                this.LastHorizontalScrollingOffset = dgv.HorizontalScrollingOffset;
            }
        }

        private void Dgv_SizeChanged(object sender, EventArgs e)
        {
            DataGridView dgv = this.mCommDataConfig_Ctrl.DgvOut;
            foreach (object obj in dgv.Controls)
            {
                Control ctl = (Control)obj;
                ctl.Left -= dgv.HorizontalScrollingOffset - this.LastHorizontalScrollingOffset;
                ctl.Top -= dgv.VerticalScrollingOffset - this.LastVerticalScrollingOffset;
                bool flag = dgv.CurrentCell != null;
                if (flag)
                {
                    bool flag2 = dgv.FirstDisplayedScrollingRowIndex > dgv.CurrentCell.RowIndex;
                    if (flag2)
                    {
                        ctl.Visible = false;
                    }
                    else
                    {
                        ctl.Visible = true;
                    }
                }
            }
            this.LastVerticalScrollingOffset = dgv.VerticalScrollingOffset;
            this.LastHorizontalScrollingOffset = dgv.HorizontalScrollingOffset;
        }

        private void AddBinding(string key, int elementIndex, string bindingString)
        {
            string destinationPath = this.mCommData.Dic[key].Inputs.GetKeys()[elementIndex];
            DataBinding dataBinding = new DataBinding(bindingString, destinationPath);
            dataBinding.S_Source = DataSource.Communication;
            dataBinding.D_Source = DataSource.Communication;
            bool flag = !this.mCommData.Dic[key].DataBindings.Contains(dataBinding);
            if (flag)
            {
                this.mCommData.Dic[key].DataBindings.Add(dataBinding);
            }
        }

        private void ReplaceBindingBySource(string key, string oldName, string newName)
        {
            string destinationPath = "";
            int index = this.mCommData.Dic[key].DataBindings.GetDestinationPath(DataSource.Communication, oldName, out destinationPath);
            bool flag = index >= 0;
            if (flag)
            {
                this.mCommData.Dic[key].DataBindings[index].SourcePath = newName;
            }
        }

        private void ReplaceBinding(string key, string oldName, string newName)
        {
            string sourcePath = "";
            int index = this.mCommData.Dic[key].DataBindings.GetSourcePath(DataSource.Communication, oldName, out sourcePath);
            bool flag = index >= 0;
            if (flag)
            {
                this.mCommData.Dic[key].DataBindings[index].DestinationPath = newName;
            }
        }

        private void RemovingBindngBySource(string key, int elementIndex)
        {
            string destinationPath = "";
            string sourcePath = this.mCommData.Dic[key].Outputs.GetKeys()[elementIndex];
            int index = this.mCommData.Dic[key].DataBindings.GetDestinationPath(DataSource.Communication, sourcePath, out destinationPath);
            bool flag = index >= 0;
            if (flag)
            {
                this.mCommData.Dic[key].DataBindings.RemoveAt(index);
            }
        }

        private void RemoveBinding(string key, int elementIndex)
        {
            string sourcePath = "";
            string destinationPath = this.mCommData.Dic[key].Inputs.GetKeys()[elementIndex];
            int index = this.mCommData.Dic[key].DataBindings.GetSourcePath(DataSource.Communication, destinationPath, out sourcePath);
            bool flag = index >= 0;
            if (flag)
            {
                this.mCommData.Dic[key].DataBindings.RemoveAt(index);
            }
        }

        private void OperaterChange(OperaterType operaterType)
        {
            switch (operaterType)
            {
                case OperaterType.SelectIndexChanged:
                    {
                        DataGridView dgv = this.mCommDataConfig_Ctrl.DgvIn;
                        bool flag = this.mCommDataConfig_Ctrl.Txt_StartByteIn.Text == "";
                        if (flag)
                        {
                            this.mCommDataConfig_Ctrl.Txt_StartByteIn.Text = "0";
                        }
                        bool flag2 = this.mCommDataConfig_Ctrl.Txt_EndByteIn.Text == "";
                        if (flag2)
                        {
                            this.mCommDataConfig_Ctrl.Txt_EndByteIn.Text = "0";
                        }
                        int startByte = Convert.ToInt32(this.mCommDataConfig_Ctrl.Txt_StartByteIn.Text);
                        int endByte = Convert.ToInt32(this.mCommDataConfig_Ctrl.Txt_EndByteIn.Text);
                        bool isOverFlow_In = this.CheckIsOverFlow(dgv, startByte, endByte);
                        bool isOverlapIn = this.MarkCellColor(dgv, 2, 3);
                        dgv = this.mCommDataConfig_Ctrl.DgvOut;
                        bool flag3 = this.mCommDataConfig_Ctrl.Txt_StartByteOut.Text == "";
                        if (flag3)
                        {
                            this.mCommDataConfig_Ctrl.Txt_StartByteOut.Text = "0";
                        }
                        bool flag4 = this.mCommDataConfig_Ctrl.Txt_EndByteOut.Text == "";
                        if (flag4)
                        {
                            this.mCommDataConfig_Ctrl.Txt_EndByteOut.Text = "0";
                        }
                        startByte = Convert.ToInt32(this.mCommDataConfig_Ctrl.Txt_StartByteOut.Text);
                        endByte = Convert.ToInt32(this.mCommDataConfig_Ctrl.Txt_EndByteOut.Text);
                        bool isOverFlow_Out = this.CheckIsOverFlow(dgv, startByte, endByte);
                        bool isOverlapOut = this.MarkCellColor(dgv, 2, 3);
                        bool isOverFlow = isOverFlow_In && isOverFlow_Out;
                        bool isOverlap = isOverlapIn && isOverlapOut;
                        bool isError = isOverFlow && isOverlap;
                        this.MarkBtnSaveColor(isError);
                        break;
                    }
                case OperaterType.StartByteTextChanged_In:
                case OperaterType.EndByteTextChanged_In:
                case OperaterType.BtnNewLine_In:
                case OperaterType.BtnDeleteLine_In:
                case OperaterType.BtnLineUp_In:
                case OperaterType.BtnLineDown_In:
                case OperaterType.DgvCellEndEdit_In:
                case OperaterType.DgvCellSelectedIndexChanged_In:
                    {
                        DataGridView dgv = this.mCommDataConfig_Ctrl.DgvIn;
                        bool flag5 = this.mCommDataConfig_Ctrl.Txt_StartByteIn.Text == "";
                        if (flag5)
                        {
                            this.mCommDataConfig_Ctrl.Txt_StartByteIn.Text = "0";
                        }
                        bool flag6 = this.mCommDataConfig_Ctrl.Txt_EndByteIn.Text == "";
                        if (flag6)
                        {
                            this.mCommDataConfig_Ctrl.Txt_EndByteIn.Text = "0";
                        }
                        int startByte = Convert.ToInt32(this.mCommDataConfig_Ctrl.Txt_StartByteIn.Text);
                        int endByte = Convert.ToInt32(this.mCommDataConfig_Ctrl.Txt_EndByteIn.Text);
                        bool isOverFlow = this.CheckIsOverFlow(dgv, startByte, endByte);
                        bool isOverlap = this.MarkCellColor(dgv, 2, 3);
                        bool isError = isOverFlow && isOverlap;
                        this.MarkBtnSaveColor(isError);
                        break;
                    }
                case OperaterType.StartByteTextChanged_Out:
                case OperaterType.EndByteTextChanged_Out:
                case OperaterType.BtnNewLine_Out:
                case OperaterType.BtnDeleteLine_Out:
                case OperaterType.BtnLineUp_Out:
                case OperaterType.BtnLineDown_Out:
                case OperaterType.DgvCellEndEdit_Out:
                case OperaterType.DgvCellSelectedIndexChanged_Out:
                    {
                        DataGridView dgv = this.mCommDataConfig_Ctrl.DgvOut;
                        bool flag7 = this.mCommDataConfig_Ctrl.Txt_StartByteOut.Text == "";
                        if (flag7)
                        {
                            this.mCommDataConfig_Ctrl.Txt_StartByteOut.Text = "0";
                        }
                        bool flag8 = this.mCommDataConfig_Ctrl.Txt_EndByteOut.Text == "";
                        if (flag8)
                        {
                            this.mCommDataConfig_Ctrl.Txt_EndByteOut.Text = "0";
                        }
                        int startByte = Convert.ToInt32(this.mCommDataConfig_Ctrl.Txt_StartByteOut.Text);
                        int endByte = Convert.ToInt32(this.mCommDataConfig_Ctrl.Txt_EndByteOut.Text);
                        bool isOverFlow = this.CheckIsOverFlow(dgv, startByte, endByte);
                        bool isOverlap = this.MarkCellColor(dgv, 2, 3);
                        bool isError = isOverFlow && isOverlap;
                        this.MarkBtnSaveColor(isError);
                        break;
                    }
            }
        }

        private bool MarkCellColor(DataGridView dgv, int ColA, int ColB)
        {
            bool IsStateOK = true;
            List<int> conflict = new List<int>();
            bool flag = dgv.RowCount == 0;
            bool result;
            if (flag)
            {
                result = true;
            }
            else
            {
                for (int i = 0; i < dgv.RowCount - 1; i++)
                {
                    int value = Convert.ToInt32(dgv[ColA, i].Value) + Convert.ToInt32(dgv[ColB, i].Value);
                    bool flag2 = value > Convert.ToInt32(dgv[ColA, i + 1].Value);
                    if (flag2)
                    {
                        bool flag3 = !conflict.Contains(i);
                        if (flag3)
                        {
                            conflict.Add(i);
                        }
                        bool flag4 = !conflict.Contains(i + 1);
                        if (flag4)
                        {
                            conflict.Add(i + 1);
                        }
                    }
                }
                for (int j = 0; j < dgv.RowCount; j++)
                {
                    bool flag5 = !conflict.Contains(j);
                    if (flag5)
                    {
                        dgv[ColA, j].Style.BackColor = Color.White;
                    }
                    else
                    {
                        dgv[ColA, j].Style.BackColor = Color.Red;
                        IsStateOK = false;
                    }
                }
                result = IsStateOK;
            }
            return result;
        }

        private void MarkBtnSaveColor(bool state)
        {
            this.mCommDataConfig_Ctrl.BtnSave.Enabled = state;
            bool flag = !this.mCommDataConfig_Ctrl.BtnSave.Enabled;
            if (flag)
            {
                this.mCommDataConfig_Ctrl.BtnSave.BackColor = Color.Red;
            }
            else
            {
                this.mCommDataConfig_Ctrl.BtnSave.BackColor = Color.Transparent;
            }
        }

        private bool CheckIsOverFlow(DataGridView dgv, int startByte, int endByte)
        {
            int RowCount = dgv.RowCount;
            bool flag = RowCount > 0;
            bool result;
            if (flag)
            {
                int maxByteCount = Convert.ToInt32(dgv[2, dgv.RowCount - 1].Value) + Convert.ToInt32(dgv[3, dgv.RowCount - 1].Value);
                int SettingByteCount = endByte - startByte + 1;
                bool flag2 = maxByteCount > SettingByteCount;
                result = !flag2;
            }
            else
            {
                result = true;
            }
            return result;
        }

        private void CommInOutConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.UnRegisterEvent();
            bool flag = this.key != "" && this.mCommData.Dic.ContainsKey(this.key);
            if (flag)
            {
                this.Read(this.key);
                this.mJobData.SaveAllData();
            }
        }
    }
}
