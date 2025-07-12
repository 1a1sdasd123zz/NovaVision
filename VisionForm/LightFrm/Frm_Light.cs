using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Collection;
using NovaVision.BaseClass.Helper;
using NovaVision.BaseClass.Light;
using NovaVision.BaseClass.VisionConfig;

namespace NovaVision.VisionForm.LightFrm
{
    public partial class Frm_Light : Form
    {
        JobData mJobData;

        private string[] m_oldSerialPortNames;
        List<LightInfo> mLights = new();

        TextBox dgv_Type;
        public Frm_Light(JobData job)
        {
            mJobData = job;
            InitializeComponent();
            InitSerialPort();
            InitDgv();
        }

        private void InitSerialPort()
        {
            string[] portNames = SerialPort.GetPortNames();
            if (portNames.Length != 0)
            {
                m_oldSerialPortNames = portNames;
                ComboBox.ObjectCollection items = cmb_ComNames.Items;
                object[] items2 = portNames;
                items.AddRange(items2);
                cmb_ComNames.SelectedIndex = 0;
            }
        }

        private void InitDgv()
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.RowHeadersVisible = false;
            //dgv.Columns.Add("Name", "名称");
            dgv.Columns.Add("COM", "串口");
            DataGridViewComboBoxColumn comBoxCol = new DataGridViewComboBoxColumn();
            comBoxCol.Items.AddRange(new object[] { "2", "4", "8" });
            dgv.Columns.Add(comBoxCol);
            dgv.Columns[1].Name = "ControlChannel";
            dgv.Columns[1].HeaderText = "控制器通道数量";
            dgv.Columns.Add("ControlChannel", "光源控制通道");
            dgv.Columns.Add("Explain", "注释");

            //dgv.Columns[0].Width = 100;
            //dgv.Columns[0].ReadOnly = true;
            //dgv.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[0].Width = 70;
            dgv.Columns[0].ReadOnly = false;
            dgv.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[1].Width = 70;
            dgv.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[2].Width = 150;
            dgv.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[3].Width = 200;
            dgv.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void tsBtn_NewLine_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_oldSerialPortNames.Length != 0 && cmb_ComNames.SelectedItem != null)
                {
                    dgv.Rows.Add();
                    dgv[0, dgv.Rows.Count - 1].Value = cmb_ComNames.SelectedItem.ToString();
                }
                else
                {
                    MessageBox.Show(@"请选择串口!", "Warning", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogError("光源配置新增异常!" + ex.ToString());
                MessageBox.Show(@"新增一行失败!", "ERROR", MessageBoxButtons.OK);
            }
        }

        private void dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgv.CurrentCell is DataGridViewTextBoxCell)
            {
                dgv_Type = e.Control as TextBox;
                dgv_Type.MouseLeave -= new EventHandler(TextBox_TextChanged);
                dgv_Type.MouseLeave += new EventHandler(TextBox_TextChanged);
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                string strs = textBox.Text;
                int row = dgv.CurrentCell.RowIndex;
                int cell = dgv.CurrentCell.ColumnIndex;

                DataGridViewComboBoxCell cmb = dgv.Rows[row].Cells[2] as DataGridViewComboBoxCell;

                switch (cmb.Value)
                {
                    case "2":
                        {
                            if (strs.All(char.IsDigit))
                            {
                                foreach (char c in strs)
                                {
                                    if (c - '0' > 2)
                                    {
                                        dgv.Rows[row].Cells[3].Value = "";
                                        MessageBox.Show(@"输入的光源控制通道超出控制器的通道数", "ERROR", MessageBoxButtons.OK);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                dgv.Rows[row].Cells[3].Value = "";
                                MessageBox.Show(@"输入的格式错误，请输入整数!", "ERROR", MessageBoxButtons.OK);
                                break;
                            }
                            break;
                        }
                    case "4":
                        {
                            if (strs.All(char.IsDigit))
                            {
                                foreach (char c in strs)
                                {
                                    if (c - '0' > 4)
                                    {
                                        dgv.Rows[row].Cells[3].Value = "";
                                        MessageBox.Show(@"输入的光源控制通道超出控制器的通道数", "ERROR", MessageBoxButtons.OK);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                dgv.Rows[row].Cells[3].Value = "";
                                MessageBox.Show(@"输入的格式错误，请输入整数!", "ERROR", MessageBoxButtons.OK);
                                break;
                            }
                            break;
                        }
                    case "8":
                        {
                            if (strs.All(char.IsDigit))
                            {
                                for (int i = 0; i < strs.Length; i++)
                                {
                                    if (Convert.ToInt32(strs[i]) > 2)
                                    {
                                        MessageBox.Show(@"输入的光源控制通道超出控制器的通道数", "ERROR", MessageBoxButtons.OK);
                                        textBox.Text = "";
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(@"输入的格式错误，请输入整数!", "ERROR", MessageBoxButtons.OK);
                                textBox.Text = "";
                                break;
                            }
                            break;
                        }
                }
            }
            catch { }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            UpdateLightInfo();

            if (!File.Exists(mJobData.LightCogfigPath))
            {
                using (File.Create(mJobData.LightCogfigPath)) { }
            }
            if (XmlHelper.WriteXML(mJobData.mLightInfo, mJobData.LightCogfigPath, typeof(MyDictionaryEx<LightInfo>)))
            {
                MessageBox.Show(@"参数保存成功！");
                LogUtil.Log("参数保存成功！");
            }
            else
            {
                MessageBox.Show(@"参数保存失败！");
                LogUtil.Log("参数保存失败！");
            }
        }

        private void UpdateLightInfo()
        {
            try
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    //string name = dgv[0, i].Value.ToString();
                    string com = dgv[0, i].Value.ToString();
                    string num = dgv[1, i].Value.ToString();
                    string str = dgv[2, i].Value.ToString();
                    string explain = dgv[3, i].Value.ToString();

                    if (mJobData.mLightInfo.ContainsKey(com))
                    {
                        mJobData.mLightInfo[com].ComName = com;
                        mJobData.mLightInfo[com].ControlChannelNum = Convert.ToInt32(num);
                        string[] strs = str.TrimEnd(',').Split(',');
                        int[] ints = new int[strs.Length];
                        for (int n = 0; n < strs.Length; n++)
                        {
                            ints[n] = Convert.ToInt32(strs[n]);
                        }
                        mJobData.mLightInfo[com].ControlLightChannel = ints;
                        mJobData.mLightInfo[com].Explain = explain;
                    }
                    else
                    {
                        LightInfo lightInfo = new LightInfo();
                        lightInfo.ComName = com;
                        lightInfo.ControlChannelNum = Convert.ToInt32(num);
                        string[] strs = str.TrimEnd(',').Split(',');
                        int[] ints = new int[strs.Length];
                        for (int n = 0; n < strs.Length; n++)
                        {
                            ints[n] = Convert.ToInt32(strs[n]);
                        }
                        lightInfo.ControlLightChannel = ints;
                        lightInfo.Explain = explain;
                        mJobData.mLightInfo.Add(lightInfo);
                    }
                }
            }
            catch { }
        }
    }
}
