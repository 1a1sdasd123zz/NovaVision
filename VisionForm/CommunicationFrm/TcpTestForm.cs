using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using NovaVision.BaseClass.Collection;
using NovaVision.BaseClass.Communication;
using NovaVision.BaseClass.Communication.CommData;
using NovaVision.BaseClass.Communication.TCP;

namespace NovaVision.VisionForm.CommunicationFrm
{
    public partial class TcpTestForm : Form
    {
        private IFlowState _commTcp;

        private string roleName = "";

        private MyDictionaryEx<Comm_Element> dataInputs;

        private MyDictionaryEx<Comm_Element> dataOutputs;

        private Dictionary<string, Comm_Element> Dic_elements_in = new Dictionary<string, Comm_Element>();

        private Dictionary<string, int> Dic_rows_in = new Dictionary<string, int>();

        private Dictionary<string, Comm_Element> Dic_elements_out = new Dictionary<string, Comm_Element>();

        private Dictionary<string, int> Dic_rows_out = new Dictionary<string, int>();



        public TcpTestForm(IFlowState commTcp)
        {
            InitializeComponent();
            _commTcp = commTcp;
            roleName = _commTcp.CommTypeName;
            InitDgv(dgv_In);
            InitDgv(dgv_Out);
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            Dic_elements_in.Clear();
            Dic_rows_in.Clear();
            tabControl1.SelectedTab = tpInput;
            for (int i = 0; i < dgv_In.Rows.Count; i++)
            {
                if ((bool)dgv_In[6, i].Value)
                {
                    string key = (string)dgv_In[0, i].Value;
                    Dic_elements_in.Add(key, dataInputs[key]);
                    Dic_rows_in.Add(key, i);
                }
            }
            foreach (KeyValuePair<string, Comm_Element> item in Dic_elements_in)
            {
                string key2 = item.Key;
                int row = Dic_rows_in[key2];
                dgv_In[5, row].Value = Convert.ToString(item.Value.Value.mValue);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Dic_elements_out.Clear();
            Dic_rows_out.Clear();
            tabControl1.SelectedTab = tpOutput;
            for (int i = 0; i < dgv_Out.Rows.Count; i++)
            {
                if ((bool)dgv_Out[6, i].Value)
                {
                    string key = (string)dgv_Out[0, i].Value;
                    Dic_elements_out.Add(key, dataOutputs[key]);
                    Dic_rows_out.Add(key, i);
                }
            }
            List<Comm_Element> elements = new List<Comm_Element>();
            foreach (KeyValuePair<string, Comm_Element> item in Dic_elements_out)
            {
                string key2 = item.Key;
                int row = Dic_rows_out[key2];
                switch (item.Value.Type)
                {
                    case "Boolean":
                        item.Value.Value.mValue = bool.Parse(Convert.ToString(dgv_Out[5, row].Value));
                        break;
                    case "SByte":
                        item.Value.Value.mValue = int.Parse(Convert.ToString(dgv_Out[5, row].Value));
                        break;
                    case "Byte":
                        item.Value.Value.mValue = int.Parse(Convert.ToString(dgv_Out[5, row].Value));
                        break;
                    case "Char":
                        item.Value.Value.mValue = char.Parse(Convert.ToString(dgv_Out[5, row].Value));
                        break;
                    case "UInt16":
                        item.Value.Value.mValue = ushort.Parse(Convert.ToString(dgv_Out[5, row].Value));
                        break;
                    case "UInt32":
                        item.Value.Value.mValue = uint.Parse(Convert.ToString(dgv_Out[5, row].Value));
                        break;
                    case "UInt64":
                        item.Value.Value.mValue = ulong.Parse(Convert.ToString(dgv_Out[5, row].Value));
                        break;
                    case "Int16":
                        item.Value.Value.mValue = short.Parse(Convert.ToString(dgv_Out[5, row].Value));
                        break;
                    case "Int32":
                        item.Value.Value.mValue = int.Parse(Convert.ToString(dgv_Out[5, row].Value));
                        break;
                    case "Int64":
                        item.Value.Value.mValue = long.Parse(Convert.ToString(dgv_Out[5, row].Value));
                        break;
                    case "Single":
                        item.Value.Value.mValue = float.Parse(Convert.ToString(dgv_Out[5, row].Value));
                        break;
                    case "Double":
                        item.Value.Value.mValue = double.Parse(Convert.ToString(dgv_Out[5, row].Value));
                        break;
                    case "String":
                        {
                            string tempStr = Convert.ToString(dgv_Out[5, row].Value);
                            if (tempStr.Length < item.Value.TypeLength)
                            {
                                item.Value.Value.mValue = tempStr.PadRight(item.Value.TypeLength);
                            }
                            else
                            {
                                item.Value.Value.mValue = tempStr;
                            }
                            break;
                        }
                    default:
                        item.Value.Value.mValue = Convert.ToString(dgv_Out[5, row].Value);
                        break;
                }
                elements.Add(item.Value);
            }
            if (roleName == "TcpServer")
            {
                string remoteIp = "";
                using (Dictionary<string, AsyncUserToken>.Enumerator enumerator2 = ((MyTcpServer)_commTcp).D_userToken.GetEnumerator())
                {
                    if (enumerator2.MoveNext())
                    {
                        remoteIp = enumerator2.Current.Key;
                    }
                }
                _commTcp.SendCommElements(elements, remoteIp);
            }
            else
            {
                _commTcp.SendCommElements(elements);
            }
        }

        private void TcpTestForm_Load(object sender, EventArgs e)
        {
            if (roleName == "TcpServer")
            {
                dataInputs = ((MyTcpServer)_commTcp).dataInputs;
                dataOutputs = ((MyTcpServer)_commTcp).dataOutputs;
            }
            else if (roleName == "TcpClient")
            {
                dataInputs = ((MyTcpClient)_commTcp).dataInputs;
                dataOutputs = ((MyTcpClient)_commTcp).dataOutputs;
            }
            Task.Run(delegate
            {
                if (dataInputs != null)
                {
                    WriteToDgv(dgv_In, dataInputs);
                }
                if (dataOutputs != null)
                {
                    WriteToDgv(dgv_Out, dataOutputs);
                }
            });
        }

        private void InitDgv(DataGridView dgv)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.RowHeadersVisible = false;
            dgv.Columns.Add("Name", "变量名");
            dgv.Columns[0].Width = 80;
            dgv.Columns[0].ReadOnly = true;
            DataGridViewComboBoxColumn cmbCol = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxCell.ObjectCollection items = cmbCol.Items;
            object[] items2 = new string[13]
            {
            "Boolean", "SByte", "Byte", "Char", "UInt16", "UInt32", "UInt64", "Int16", "Int32", "Int64",
            "Single", "Double", "String"
            };
            items.AddRange(items2);
            cmbCol.DefaultCellStyle.NullValue = "Int32";
            dgv.Columns.Add(cmbCol);
            dgv.Columns[1].Name = "ValueType";
            dgv.Columns[1].HeaderText = "变量类型";
            dgv.Columns[1].Width = 80;
            dgv.Columns[1].ReadOnly = true;
            dgv.Columns.Add("ByteOffset", "字节偏移量");
            dgv.Columns[2].ValueType = typeof(int);
            dgv.Columns[2].Width = 80;
            dgv.Columns[2].ReadOnly = true;
            dgv.Columns.Add("ByteNum", "占用字节数");
            dgv.Columns[3].ValueType = typeof(int);
            dgv.Columns[3].Width = 80;
            dgv.Columns[3].ReadOnly = true;
            dgv.Columns.Add("Channel", "通道号");
            dgv.Columns[4].ValueType = typeof(int);
            dgv.Columns[4].Width = 80;
            dgv.Columns[4].ReadOnly = true;
            dgv.Columns.Add("Value", "值");
            dgv.Columns[5].ValueType = typeof(object);
            dgv.Columns[5].Width = 80;
            if (dgv == dgv_In)
            {
                dgv.Columns[5].ReadOnly = true;
            }
            else
            {
                dgv.Columns[5].ReadOnly = false;
            }
            DataGridViewCheckBoxColumn ckbCol = new DataGridViewCheckBoxColumn();
            ckbCol.DefaultCellStyle.NullValue = false;
            dgv.Columns.Add(ckbCol);
            dgv.Columns[6].Name = "Selected";
            dgv.Columns[6].HeaderText = "是否选择";
            dgv.Columns[6].Width = 80;
            dgv.Columns[6].ReadOnly = false;
        }

        private void WriteToDgv(DataGridView dgv, MyDictionaryEx<Comm_Element> elements)
        {
            Action action = delegate
            {
                dgv.Rows.Clear();
                for (int i = 0; i < elements.Count; i++)
                {
                    dgv.Rows.Add();
                    dgv[0, i].Value = elements[i].Name;
                    dgv[1, i].Style.NullValue = elements[i].Type;
                    dgv[2, i].Value = elements[i].ByteOffset;
                    dgv[3, i].Value = elements[i].TypeLength;
                    if (elements[i].Type != "String")
                    {
                        dgv[3, i].ReadOnly = true;
                        dgv[3, i].Style.BackColor = Color.LightGray;
                    }
                    dgv[4, i].Value = elements[i].Channel;
                    dgv[5, i].Value = 0;
                    dgv[6, i].Value = false;
                }
            };
            if (dgv.InvokeRequired)
            {
                dgv.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
