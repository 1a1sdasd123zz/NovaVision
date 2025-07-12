using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Communication;
using NovaVision.BaseClass.Communication.TCP;
using NovaVision.BaseClass.Helper;

namespace NovaVision.VisionForm.CommunicationFrm
{
    public partial class TcpConfigForm : Form
    {
        private const string PREFIXNAME = "Tcp-";

        private static string[] roleArray = new string[2] { "Server", "Client" };

        private static string[] modeArray = new string[5] { "软件->PLC:受控方", "软件->软件:主控方", "软件->软件:受控方", "用户自定义", "带协议版本" };

        private string XMLpath = "CardConfig.xml";

        private CommConfigData mconfigData;

        private CommBaseInfo commBaseInfo;

        private IFlowState comm_Tcp;

        private string SerialNum;

        private int modeIndex;

        private string roleName;

        private int tcpMaxIndex;

        private string localIp;

        private string localIp_1st;

        private string localIp_2nd;

        private string localIp_3rd;

        private string localIp_4th;

        private string localPort;

        private string remoteIp;

        private string remoteIp_1st;

        private string remoteIp_2nd;

        private string remoteIp_3rd;

        private string remoteIp_4th;

        private string remotePort;

        private bool isConnected;

        private Task T_EnumTcp;

        private string HBStr = "HB|1|3";

        private bool HBFlag = false;

        private bool tcpSNState = false;

        private bool tcpState = false;

        private List<string> tcpSnListXml = new List<string>();



        public TcpConfigForm(CommBaseInfo baseInfo, string pathStr)
        {
            InitializeComponent();
            InitialSettingParams();
            XMLpath = pathStr;
            commBaseInfo = baseInfo;
            isConnected = false;
            SetControlState(state: true);
            tcpSNState = false;
        }

        public TcpConfigForm(CommConfigData mCommData, CommBaseInfo baseInfo, string pathStr)
        {
            InitializeComponent();
            InitialSettingParams();
            XMLpath = pathStr;
            mconfigData = mCommData;
            commBaseInfo = baseInfo;
            isConnected = false;
            SetControlState(state: false);
            if (mconfigData.SerialNum != null && mconfigData.SerialNum != "")
            {
                tcpSNState = true;
            }
        }

        private void InitialSettingParams()
        {
            SerialNum = "Tcp-";
            localIp_1st = "0";
            localIp_2nd = "0";
            localIp_3rd = "0";
            localIp_4th = "0";
            localPort = "1000";
            remoteIp_1st = "0";
            remoteIp_2nd = "0";
            remoteIp_3rd = "0";
            remoteIp_4th = "0";
            remotePort = "1000";
        }

        private void SetControlState(bool state)
        {
            cbRole.Items.Clear();
            ComboBox.ObjectCollection items = cbRole.Items;
            object[] items2 = roleArray;
            items.AddRange(items2);
            cbMode.Items.Clear();
            ComboBox.ObjectCollection items3 = cbMode.Items;
            items2 = modeArray;
            items3.AddRange(items2);
            btnStartOrStop.Visible = true;
            if (state)
            {
                btnSaveParams.Enabled = false;
                btnSaveParams.Visible = false;
                btnAddTcp.Enabled = true;
                btnAddTcp.Visible = true;
                btnNewTcp.Enabled = true;
                btnNewTcp.Visible = true;
                nUDSn.Enabled = true;
                groupBox2.Enabled = true;
                btnTest.Enabled = false;
                btnTest.Visible = false;
            }
            else
            {
                cbTcp.Enabled = false;
                btnSaveParams.Visible = true;
                btnAddTcp.Visible = false;
                btnNewTcp.Enabled = false;
                btnNewTcp.Visible = true;
                nUDSn.Enabled = false;
                groupBox2.Enabled = false;
                btnTest.Enabled = false;
                btnTest.Visible = true;
            }
        }

        private void TcpConfigForm_Load(object sender, EventArgs e)
        {
            tSSLStateText.ForeColor = Color.Black;
            tSSLStateText.Text = "正在查找配置中的Tcp...";
            T_EnumTcp = new Task(delegate
            {
                tcpState = false;
                Invoke((EventHandler)delegate
                {
                    cbTcp.Items.Clear();
                    if (commBaseInfo.CardList == null)
                    {
                        commBaseInfo.CardList = new List<CommConfigData>();
                    }
                    List<string> list = commBaseInfo.SnList_Tcp;
                    if (list == null)
                    {
                        list = new List<string>();
                    }
                    List<string> list2 = new List<string>();
                    foreach (string current in list)
                    {
                        list2.Add(current + "(已添加)");
                    }
                    foreach (string current2 in list2)
                    {
                        cbTcp.Items.Add(current2);
                    }
                    if (tcpSNState && list.Contains(mconfigData.SerialNum))
                    {
                        tcpState = true;
                    }
                    if (cbTcp.Items.Count > 0)
                    {
                        tSSLStateText.ForeColor = Color.Blue;
                        tSSLStateText.Text = "Tcp检索完成！";
                        groupBox2.Enabled = true;
                        if (tcpState)
                        {
                            cbTcp.SelectedItem = mconfigData.SerialNum + "(已添加)";
                        }
                        else
                        {
                            cbTcp.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        Console.WriteLine("XML中未找到Tcp配置");
                        tSSLStateText.ForeColor = Color.Red;
                        tSSLStateText.Text = "XML中未找到Tcp配置！";
                        groupBox2.Enabled = false;
                        cbRole.Enabled = false;
                        cbMode.Enabled = false;
                        btnStartOrStop.Enabled = false;
                        btnRemove.Enabled = false;
                    }
                    tcpMaxIndex = commBaseInfo.GetTcpMaxIndex();
                    nUDSn.Value = Convert.ToDecimal(tcpMaxIndex + 1);
                });
            });
            T_EnumTcp.Start();
        }

        private void cbTcp_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            object item = cb.SelectedItem;
            if (item == null)
            {
                cbRole.Enabled = false;
                cbMode.Enabled = false;
                btnStartOrStop.Enabled = false;
                btnTest.Enabled = false;
                groupBox2.Enabled = false;
                btnAddTcp.Enabled = false;
                btnSaveParams.Enabled = false;
                btnRemove.Enabled = false;
                return;
            }
            cbRole.Enabled = true;
            cbMode.Enabled = true;
            btnStartOrStop.Enabled = true;
            groupBox2.Enabled = true;
            btnRemove.Enabled = true;
            if (tcpSNState)
            {
                btnAddTcp.Visible = false;
                btnSaveParams.Visible = true;
            }
            else
            {
                btnAddTcp.Visible = true;
                btnSaveParams.Visible = false;
            }
            if (SerialNum == GetMyString(item.ToString()))
            {
                return;
            }
            if (item.ToString() != "")
            {
                SerialNum = GetMyString(item.ToString());
                CommConfigData configData = commBaseInfo.Query(SerialNum);
                roleName = configData.RoleName;
                modeIndex = configData.ModeIndex;
                localIp = configData.LocalIp;
                string[] localIpArray = localIp.Split('.');
                localPort = configData.LocalPort;
                remoteIp = configData.RemoteIp;
                string[] remoteIpArray = remoteIp.Split('.');
                remotePort = configData.RemotePort;
                cbMode.SelectedIndex = modeIndex;
                if (configData.HBStr.Contains("|") && configData.HBStr.Split('|').Length == 3)
                {
                    tbHB.Text = configData.HBStr.Split('|')[0];
                    numericUpDownBufferSize.Value = Convert.ToInt32(configData.HBStr.Split('|')[1]);
                    cbMode.SelectedIndex = Convert.ToInt32(configData.HBStr.Split('|')[2]);
                }
                cbHB.Checked = configData.HBFlag;
                if (roleName == "Server")
                {
                    cbRole.SelectedIndex = 0;
                    tbLocalIp_1st.Text = localIpArray[0];
                    tbLocalIp_2nd.Text = localIpArray[1];
                    tbLocalIp_3rd.Text = localIpArray[2];
                    tbLocalIp_4th.Text = localIpArray[3];
                    nUDLocalPort.Value = Convert.ToDecimal(localPort);
                }
                else
                {
                    cbRole.SelectedIndex = 1;
                    tbLocalIp_1st.Text = localIpArray[0];
                    tbLocalIp_2nd.Text = localIpArray[1];
                    tbLocalIp_3rd.Text = localIpArray[2];
                    tbLocalIp_4th.Text = localIpArray[3];
                    nUDLocalPort.Value = Convert.ToDecimal(localPort);
                    tbRemoteIp_1st.Text = remoteIpArray[0];
                    tbRemoteIp_2nd.Text = remoteIpArray[1];
                    tbRemoteIp_3rd.Text = remoteIpArray[2];
                    tbRemoteIp_4th.Text = remoteIpArray[3];
                    nUDRemotePort.Value = Convert.ToDecimal(remotePort);
                }
                if (MyTcpClient.Dic_client.ContainsKey(SerialNum))
                {
                    comm_Tcp = MyTcpClient.GetClientInstance(SerialNum);
                    isConnected = comm_Tcp.IsConnected;
                    if (((MyTcpClient)comm_Tcp)._localIp != "")
                    {
                        localIp_1st = ((MyTcpClient)comm_Tcp)._localIp.Split('.')[0];
                        localIp_2nd = ((MyTcpClient)comm_Tcp)._localIp.Split('.')[1];
                        localIp_3rd = ((MyTcpClient)comm_Tcp)._localIp.Split('.')[2];
                        localIp_4th = ((MyTcpClient)comm_Tcp)._localIp.Split('.')[3];
                        tbLocalIp_1st.Text = localIp_1st;
                        tbLocalIp_2nd.Text = localIp_2nd;
                        tbLocalIp_3rd.Text = localIp_3rd;
                        tbLocalIp_4th.Text = localIp_4th;
                    }
                    else
                    {
                        localIp_1st = "0";
                        localIp_2nd = "0";
                        localIp_3rd = "0";
                        localIp_4th = "0";
                        tbLocalIp_1st.Text = localIp_1st;
                        tbLocalIp_2nd.Text = localIp_2nd;
                        tbLocalIp_3rd.Text = localIp_3rd;
                        tbLocalIp_4th.Text = localIp_4th;
                    }
                    localPort = ((MyTcpClient)comm_Tcp)._localPort.ToString();
                    nUDLocalPort.Value = Convert.ToDecimal(localPort);
                    if (((MyTcpClient)comm_Tcp)._remoteIp != "")
                    {
                        remoteIp_1st = ((MyTcpClient)comm_Tcp)._remoteIp.Split('.')[0];
                        remoteIp_2nd = ((MyTcpClient)comm_Tcp)._remoteIp.Split('.')[1];
                        remoteIp_3rd = ((MyTcpClient)comm_Tcp)._remoteIp.Split('.')[2];
                        remoteIp_4th = ((MyTcpClient)comm_Tcp)._remoteIp.Split('.')[3];
                        tbRemoteIp_1st.Text = remoteIp_1st;
                        tbRemoteIp_2nd.Text = remoteIp_2nd;
                        tbRemoteIp_3rd.Text = remoteIp_3rd;
                        tbRemoteIp_4th.Text = remoteIp_4th;
                    }
                    else
                    {
                        remoteIp_1st = "0";
                        remoteIp_2nd = "0";
                        remoteIp_3rd = "0";
                        remoteIp_4th = "0";
                        tbRemoteIp_1st.Text = remoteIp_1st;
                        tbRemoteIp_2nd.Text = remoteIp_2nd;
                        tbRemoteIp_3rd.Text = remoteIp_3rd;
                        tbRemoteIp_4th.Text = remoteIp_4th;
                    }
                    remotePort = ((MyTcpClient)comm_Tcp)._remotePort.ToString();
                    nUDRemotePort.Value = Convert.ToDecimal(remotePort);
                    string lang2 = MultiLanguage.GetDefaultLanguage();
                    if (isConnected)
                    {
                        btnStartOrStop.Text = ((lang2 == "Chinese") ? "断开连接" : ((lang2 == "English") ? "Disconnect" : "Trennen"));
                        cbRole.Enabled = false;
                        cbMode.Enabled = false;
                        groupBox2.Enabled = false;
                        if (tcpSNState)
                        {
                            btnSaveParams.Enabled = false;
                        }
                        else
                        {
                            btnAddTcp.Enabled = false;
                        }
                        btnRemove.Enabled = false;
                        btnTest.Enabled = true;
                    }
                    else
                    {
                        btnStartOrStop.Text = ((lang2 == "Chinese") ? "开启连接" : ((lang2 == "English") ? "Open Connection" : "Verbindung öffnen"));
                        cbMode.Enabled = true;
                        groupBox2.Enabled = true;
                        tbRemoteIp_1st.Enabled = true;
                        tbRemoteIp_2nd.Enabled = true;
                        tbRemoteIp_3rd.Enabled = true;
                        tbRemoteIp_4th.Enabled = true;
                        if (tcpSNState)
                        {
                            btnSaveParams.Enabled = true;
                            cbRole.Enabled = false;
                        }
                        else
                        {
                            btnAddTcp.Enabled = true;
                            cbRole.Enabled = true;
                        }
                        btnRemove.Enabled = false;
                        btnTest.Enabled = false;
                    }
                }
                if (MyTcpServer.Dic_server.ContainsKey(SerialNum))
                {
                    comm_Tcp = MyTcpServer.GetServerInstance(SerialNum);
                    isConnected = comm_Tcp.IsConnected;
                    if (((MyTcpServer)comm_Tcp)._localIp != "")
                    {
                        localIp_1st = ((MyTcpServer)comm_Tcp)._localIp.Split('.')[0];
                        localIp_2nd = ((MyTcpServer)comm_Tcp)._localIp.Split('.')[1];
                        localIp_3rd = ((MyTcpServer)comm_Tcp)._localIp.Split('.')[2];
                        localIp_4th = ((MyTcpServer)comm_Tcp)._localIp.Split('.')[3];
                        tbLocalIp_1st.Text = localIp_1st;
                        tbLocalIp_2nd.Text = localIp_2nd;
                        tbLocalIp_3rd.Text = localIp_3rd;
                        tbLocalIp_4th.Text = localIp_4th;
                    }
                    else
                    {
                        localIp_1st = "127";
                        localIp_2nd = "0";
                        localIp_3rd = "0";
                        localIp_4th = "1";
                        tbLocalIp_1st.Text = localIp_1st;
                        tbLocalIp_2nd.Text = localIp_2nd;
                        tbLocalIp_3rd.Text = localIp_3rd;
                        tbLocalIp_4th.Text = localIp_4th;
                    }
                    localPort = ((MyTcpServer)comm_Tcp)._localPort.ToString();
                    nUDLocalPort.Value = Convert.ToDecimal(localPort);
                    remoteIp_1st = "0";
                    remoteIp_2nd = "0";
                    remoteIp_3rd = "0";
                    remoteIp_4th = "0";
                    tbRemoteIp_1st.Text = remoteIp_1st;
                    tbRemoteIp_2nd.Text = remoteIp_2nd;
                    tbRemoteIp_3rd.Text = remoteIp_3rd;
                    tbRemoteIp_4th.Text = remoteIp_4th;
                    string lang = MultiLanguage.GetDefaultLanguage();
                    if (isConnected)
                    {
                        btnStartOrStop.Text = ((lang == "Chinese") ? "停止监听" : ((lang == "English") ? "Stop listening" : "Hör auf zuzuhören."));
                        cbRole.Enabled = false;
                        cbMode.Enabled = false;
                        groupBox2.Enabled = false;
                        if (tcpSNState)
                        {
                            btnSaveParams.Enabled = false;
                        }
                        else
                        {
                            btnAddTcp.Enabled = false;
                        }
                        btnRemove.Enabled = false;
                        btnTest.Enabled = true;
                    }
                    else
                    {
                        btnStartOrStop.Text = ((lang == "Chinese") ? "开启监听" : ((lang == "English") ? "Start listening" : "Hören Sie zu"));
                        cbMode.Enabled = true;
                        groupBox2.Enabled = true;
                        tbRemoteIp_1st.Enabled = false;
                        tbRemoteIp_2nd.Enabled = false;
                        tbRemoteIp_3rd.Enabled = false;
                        tbRemoteIp_4th.Enabled = false;
                        if (tcpSNState)
                        {
                            btnSaveParams.Enabled = true;
                            cbRole.Enabled = false;
                        }
                        else
                        {
                            btnAddTcp.Enabled = true;
                            cbRole.Enabled = true;
                        }
                        btnRemove.Enabled = false;
                        btnTest.Enabled = false;
                    }
                }
                if (!isConnected)
                {
                    cbRole.Enabled = true;
                    cbMode.Enabled = true;
                    groupBox2.Enabled = true;
                    if (tcpSNState)
                    {
                        btnNewTcp.Enabled = false;
                        btnSaveParams.Enabled = true;
                        btnRemove.Enabled = true;
                    }
                    else
                    {
                        btnNewTcp.Enabled = true;
                        btnAddTcp.Enabled = true;
                        btnRemove.Enabled = true;
                    }
                }
            }
            if (item.ToString().Contains("已添加"))
            {
                btnAddTcp.Enabled = false;
                btnRemove.Enabled = true;
            }
        }

        private void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRole.SelectedItem == null)
            {
                return;
            }
            string lang = MultiLanguage.GetDefaultLanguage();
            roleName = cbRole.SelectedItem.ToString();
            if (cbRole.SelectedIndex == 0)
            {
                tbRemoteIp_1st.Enabled = false;
                tbRemoteIp_2nd.Enabled = false;
                tbRemoteIp_3rd.Enabled = false;
                tbRemoteIp_4th.Enabled = false;
                nUDRemotePort.Enabled = false;
                tbHB.Enabled = false;
                cbHB.Enabled = false;
                if (isConnected)
                {
                    btnStartOrStop.Text = ((lang == "Chinese") ? "停止监听" : ((lang == "English") ? "Stop listening" : "Hör auf zuzuhören."));
                }
                else
                {
                    btnStartOrStop.Text = ((lang == "Chinese") ? "开启监听" : ((lang == "English") ? "Start listening" : "Hören Sie zu"));
                }
            }
            else
            {
                tbRemoteIp_1st.Enabled = true;
                tbRemoteIp_2nd.Enabled = true;
                tbRemoteIp_3rd.Enabled = true;
                tbRemoteIp_4th.Enabled = true;
                nUDRemotePort.Enabled = true;
                tbHB.Enabled = true;
                cbHB.Enabled = true;
                if (isConnected)
                {
                    btnStartOrStop.Text = ((lang == "Chinese") ? "断开连接" : ((lang == "English") ? "Disconnect" : "Trennen"));
                }
                else
                {
                    btnStartOrStop.Text = ((lang == "Chinese") ? "开启连接" : ((lang == "English") ? "Open Connection" : "Verbindung öffnen"));
                }
            }
        }

        private void cbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMode.SelectedItem != null)
            {
                modeIndex = cbMode.SelectedIndex;
                HBStr = tbHB.Text + "|" + (int)numericUpDownBufferSize.Value + "|" + cbMode.SelectedIndex;
            }
        }

        private void tbLocalIp_1st_TextChanged(object sender, EventArgs e)
        {
            if (tbLocalIp_1st.Text != localIp_1st && tbLocalIp_1st.Text != "")
            {
                localIp_1st = tbLocalIp_1st.Text;
            }
        }

        private void tbLocalIp_2nd_TextChanged(object sender, EventArgs e)
        {
            if (tbLocalIp_2nd.Text != localIp_2nd && tbLocalIp_2nd.Text != "")
            {
                localIp_2nd = tbLocalIp_2nd.Text;
            }
        }

        private void tbLocalIp_3rd_TextChanged(object sender, EventArgs e)
        {
            if (tbLocalIp_3rd.Text != localIp_3rd && tbLocalIp_3rd.Text != "")
            {
                localIp_3rd = tbLocalIp_3rd.Text;
            }
        }

        private void tbLocalIp_4th_TextChanged(object sender, EventArgs e)
        {
            if (tbLocalIp_4th.Text != localIp_4th && tbLocalIp_4th.Text != "")
            {
                localIp_4th = tbLocalIp_4th.Text;
            }
        }

        private void nUDLocalPort_ValueChanged(object sender, EventArgs e)
        {
            if (nUDLocalPort.Value.ToString() != localPort)
            {
                localPort = nUDLocalPort.Value.ToString();
            }
        }

        private void tbRemoteIp_1st_TextChanged(object sender, EventArgs e)
        {
            if (tbRemoteIp_1st.Text != remoteIp_1st && tbRemoteIp_1st.Text != "")
            {
                remoteIp_1st = tbRemoteIp_1st.Text;
            }
        }

        private void tbRemoteIp_2nd_TextChanged(object sender, EventArgs e)
        {
            if (tbRemoteIp_2nd.Text != remoteIp_2nd && tbRemoteIp_2nd.Text != "")
            {
                remoteIp_2nd = tbRemoteIp_2nd.Text;
            }
        }

        private void tbRemoteIp_3rd_TextChanged(object sender, EventArgs e)
        {
            if (tbRemoteIp_3rd.Text != remoteIp_3rd && tbRemoteIp_3rd.Text != "")
            {
                remoteIp_3rd = tbRemoteIp_3rd.Text;
            }
        }

        private void tbRemoteIp_4th_TextChanged(object sender, EventArgs e)
        {
            if (tbRemoteIp_4th.Text != remoteIp_4th && tbRemoteIp_4th.Text != "")
            {
                remoteIp_4th = tbRemoteIp_4th.Text;
            }
        }

        private void nUDRemotePort_ValueChanged(object sender, EventArgs e)
        {
            if (nUDRemotePort.Value.ToString() != remotePort)
            {
                remotePort = nUDRemotePort.Value.ToString();
            }
        }

        private void btnAddTcp_Click(object sender, EventArgs e)
        {
            try
            {
                CommConfigData commConfigData = new CommConfigData();
                commConfigData.CommCategory = "Tcp";
                commConfigData.RoleName = roleName;
                commConfigData.ModeIndex = modeIndex;
                commConfigData.SerialNum = SerialNum;
                localIp = $"{localIp_1st}.{localIp_2nd}.{localIp_3rd}.{localIp_4th}";
                commConfigData.LocalIp = localIp;
                commConfigData.LocalPort = localPort;
                remoteIp = $"{remoteIp_1st}.{remoteIp_2nd}.{remoteIp_3rd}.{remoteIp_4th}";
                commConfigData.RemoteIp = remoteIp;
                commConfigData.RemotePort = remotePort;
                commConfigData.HBFlag = HBFlag;
                if (tbHB.Text == null)
                {
                    tbHB.Text = "";
                }
                commConfigData.HBStr = tbHB.Text + "|" + (int)numericUpDownBufferSize.Value + "|" + cbMode.SelectedIndex;
                if (commBaseInfo.CardList == null)
                {
                    commBaseInfo.CardList = new List<CommConfigData>();
                }
                List<string> cardSnList = commBaseInfo.SnList_Tcp;
                if (cardSnList == null)
                {
                    cardSnList = new List<string>();
                }
                if (cardSnList.Contains(commConfigData.SerialNum))
                {
                    if (!commBaseInfo.Modify(commConfigData))
                    {
                        MessageBox.Show(@"参数更新失败");
                        return;
                    }
                }
                else
                {
                    DialogResult dr = MessageBox.Show(@"确定是否将当前Tcp添加至配置文件！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    if (dr != DialogResult.OK)
                    {
                        return;
                    }
                    if (!commBaseInfo.Add(commConfigData))
                    {
                        MessageBox.Show(@"Tcp添加失败");
                        return;
                    }
                    cbTcp.Items.Add(commConfigData.SerialNum + "(已添加)");
                    cbTcp.Update();
                }
                if (XmlHelper.WriteXML(XMLpath, commBaseInfo.CardList))
                {
                    cbTcp.Text += "(已添加)";
                    nUDSn.Value = Convert.ToDecimal(++tcpMaxIndex + 1);
                    btnAddTcp.Enabled = false;
                    btnRemove.Enabled = true;
                    MessageBox.Show(@"Tcp添加到XML文件成功");
                }
                else
                {
                    MessageBox.Show(@"Tcp添加XML文件失败");
                }
            }
            catch
            {
                MessageBox.Show(@"Tcp添加XML文件失败");
            }
        }

        private void btnSaveParams_Click(object sender, EventArgs e)
        {
            try
            {
                if (comm_Tcp != null)
                {
                    DialogResult dr = MessageBox.Show(@"是否将修改的参数写入主配置！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    if (dr == DialogResult.OK && tcpSNState && mconfigData.SerialNum == comm_Tcp.SerialNum)
                    {
                        mconfigData.CommCategory = "Tcp";
                        mconfigData.RoleName = roleName;
                        mconfigData.ModeIndex = modeIndex;
                        mconfigData.SerialNum = SerialNum;
                        localIp = $"{localIp_1st}.{localIp_2nd}.{localIp_3rd}.{localIp_4th}";
                        mconfigData.LocalIp = localIp;
                        mconfigData.LocalPort = localPort;
                        remoteIp = $"{remoteIp_1st}.{remoteIp_2nd}.{remoteIp_3rd}.{remoteIp_4th}";
                        mconfigData.RemoteIp = remoteIp;
                        mconfigData.RemotePort = remotePort;
                        mconfigData.HBFlag = HBFlag;
                        mconfigData.HBStr = HBStr + "|" + (int)numericUpDownBufferSize.Value + "|" + cbMode.SelectedIndex;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(@"写入失败");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (SerialNum == null || SerialNum == "Tcp-")
            {
                return;
            }
            if (!cbTcp.Text.Contains("已添加"))
            {
                cbTcp.Items.Remove(cbTcp.Text ?? "");
                cbTcp.Text = "";
                cbRole.Text = "";
                cbRole.Enabled = false;
                cbMode.Text = "";
                cbMode.Enabled = false;
                groupBox2.Enabled = false;
                btnAddTcp.Enabled = false;
                btnRemove.Enabled = false;
                MessageBox.Show(@"新增Tcp已删除");
            }
            else if (commBaseInfo.Delete(SerialNum))
            {
                cbTcp.Items.Remove(SerialNum + "(已添加)");
                if (cbTcp.Items.Count > 0)
                {
                    cbTcp.SelectedIndex = 0;
                    if (tcpSNState)
                    {
                        cbTcp.SelectedItem = null;
                    }
                }
                else
                {
                    cbTcp.Text = "";
                    groupBox1.Enabled = true;
                    cbRole.Text = "";
                    cbRole.Enabled = false;
                    cbMode.Text = "";
                    cbMode.Enabled = false;
                    groupBox2.Enabled = false;
                    tbLocalIp_1st.Text = "0";
                    tbLocalIp_2nd.Text = "0";
                    tbLocalIp_3rd.Text = "0";
                    tbLocalIp_4th.Text = "0";
                    nUDLocalPort.Value = 1000m;
                    tbRemoteIp_1st.Text = "0";
                    tbRemoteIp_2nd.Text = "0";
                    tbRemoteIp_3rd.Text = "0";
                    tbRemoteIp_4th.Text = "0";
                    nUDRemotePort.Value = 1000m;
                    btnAddTcp.Enabled = false;
                    btnRemove.Enabled = false;
                    btnStartOrStop.Enabled = false;
                    tbHB.Text = "";
                    cbHB.Checked = false;
                }
                if (XmlHelper.WriteXML(XMLpath, commBaseInfo.CardList))
                {
                    tcpMaxIndex = commBaseInfo.GetTcpMaxIndex();
                    nUDSn.Value = Convert.ToDecimal(tcpMaxIndex + 1);
                    MessageBox.Show(@"将Tcp移除XML成功");
                }
                else
                {
                    MessageBox.Show(@"将Tcp移除XML失败");
                }
            }
            else
            {
                MessageBox.Show(@"将Tcp移除XML失败");
            }
        }

        private void btnNewTcp_Click(object sender, EventArgs e)
        {
            cbTcp.Text = string.Format("{0}{1}", "Tcp-", nUDSn.Value);
            SerialNum = string.Format("{0}{1}", "Tcp-", nUDSn.Value);
            cbRole.Enabled = true;
            cbMode.Enabled = true;
            cbRole.SelectedIndex = 0;
            cbRole.Text = "Server";
            cbMode.SelectedIndex = 3;
            cbMode.Text = "用户自定义";
            groupBox2.Enabled = true;
            btnStartOrStop.Enabled = true;
            btnAddTcp.Enabled = true;
            btnRemove.Enabled = true;
        }

        private string GetMyString(string val)
        {
            int index = val.IndexOf('(');
            if (index > 0)
            {
                return val.Substring(0, index);
            }
            return val;
        }

        private void btnStartOrStop_Click(object sender, EventArgs e)
        {
            switch (btnStartOrStop.Text)
            {
                case "开启连接":
                case "Open Connection":
                case "Verbindung öffnen":
                    {
                        MyTcpClient.CreateNewClient(Convert.ToInt32(localPort), remoteIp, Convert.ToInt32(remotePort), SerialNum, HBStr, HBFlag);
                        comm_Tcp = MyTcpClient.GetClientInstance(SerialNum);
                        bool flag = ((MyTcpClient)comm_Tcp).Connect(50);
                        isConnected = comm_Tcp.IsConnected;
                        if (flag)
                        {
                            btnStartOrStop.Text = getText();
                            tSSLStateText.ForeColor = Color.Green;
                            tSSLStateText.Text = "Tcp客户端与服务器连接成功！";
                            groupBox2.Enabled = false;
                            cbRole.Enabled = false;
                            cbMode.Enabled = false;
                            btnTest.Enabled = true;
                            btnRemove.Enabled = false;
                            btnSaveParams.Enabled = false;
                            LogUtil.Log("Tcp客户端(" + cbTcp.Text + ")与服务器连接成功！");
                        }
                        else
                        {
                            btnStartOrStop.Text = getText();
                            tSSLStateText.ForeColor = Color.Red;
                            tSSLStateText.Text = "Tcp客户端与服务器连接失败！";
                            groupBox2.Enabled = true;
                            cbRole.Enabled = true;
                            cbMode.Enabled = true;
                            btnTest.Enabled = false;
                            btnRemove.Enabled = true;
                            btnSaveParams.Enabled = true;
                            LogUtil.LogError("Tcp客户端(" + cbTcp.Text + ")与服务器连接失败！");
                        }
                        break;
                    }
                case "断开连接":
                case "Disconnect":
                case "Trennen":
                    if (comm_Tcp != null)
                    {
                        ((MyTcpClient)comm_Tcp).ControlFlag = true;
                        comm_Tcp.Close();
                    }
                    btnStartOrStop.Text = getText();
                    isConnected = false;
                    tSSLStateText.ForeColor = Color.Blue;
                    tSSLStateText.Text = "Tcp客户端与服务器断开连接！";
                    groupBox2.Enabled = true;
                    cbRole.Enabled = true;
                    cbMode.Enabled = true;
                    btnTest.Enabled = false;
                    btnRemove.Enabled = true;
                    btnSaveParams.Enabled = true;
                    LogUtil.Log("Tcp客户端(" + cbTcp.Text + ")与服务器断开连接！");
                    break;
                case "开启监听":
                case "Start listening":
                case "Hören Sie zu":
                    MyTcpServer.CreateNewServer(localIp, Convert.ToInt32(localPort), 2, SerialNum, HBStr, HBFlag);
                    comm_Tcp = MyTcpServer.GetServerInstance(SerialNum);
                    ((MyTcpServer)comm_Tcp).Start();
                    btnStartOrStop.Text = getText();
                    isConnected = comm_Tcp.IsConnected;
                    tSSLStateText.ForeColor = Color.Green;
                    tSSLStateText.Text = "Tcp服务器启动监听！";
                    groupBox2.Enabled = false;
                    cbRole.Enabled = false;
                    cbMode.Enabled = false;
                    btnTest.Enabled = true;
                    btnRemove.Enabled = false;
                    btnSaveParams.Enabled = false;
                    break;
                case "停止监听":
                case "Stop listening":
                case "Hör auf zuzuhören.":
                    if (comm_Tcp != null)
                    {
                        comm_Tcp.Close();
                    }
                    btnStartOrStop.Text = getText();
                    isConnected = false;
                    tSSLStateText.ForeColor = Color.Blue;
                    tSSLStateText.Text = "Tcp服务器停止监听！";
                    groupBox2.Enabled = true;
                    cbRole.Enabled = true;
                    cbMode.Enabled = true;
                    btnTest.Enabled = false;
                    btnRemove.Enabled = true;
                    btnSaveParams.Enabled = true;
                    break;
            }
        }

        private string getText()
        {
            string lang = MultiLanguage.GetDefaultLanguage();
            switch (btnStartOrStop.Text)
            {
                case "开启连接":
                case "Open Connection":
                case "Verbindung öffnen":
                    return (lang == "Chinese") ? "断开连接" : ((lang == "English") ? "Disconnect" : "Trennen");
                case "断开连接":
                case "Disconnect":
                case "Trennen":
                    return (lang == "Chinese") ? "开启连接" : ((lang == "English") ? "Open Connection" : "Verbindung öffnen");
                case "开启监听":
                case "Start listening":
                case "Hören Sie zu":
                    return (lang == "Chinese") ? "停止监听" : ((lang == "English") ? "Stop listening" : "Hör auf zuzuhören.");
                case "停止监听":
                case "Stop listening":
                case "Hör auf zuzuhören.":
                    return (lang == "Chinese") ? "开启监听" : ((lang == "English") ? "Start listening" : "Hören Sie zu");
                default:
                    return (lang == "Chinese") ? "开启连接" : ((lang == "English") ? "Open Connection" : "Verbindung öffnen");
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            TcpTestForm testForm = new TcpTestForm(comm_Tcp);
            testForm.ShowDialog();
        }

        private void TcpConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cbRole.SelectedItem != null && cbRole.SelectedIndex != 0 && comm_Tcp != null)
            {
                ((MyTcpClient)comm_Tcp).ControlFlag = false;
            }
        }

        private void cbHB_CheckedChanged(object sender, EventArgs e)
        {
            HBFlag = cbHB.Checked;
        }

        private void tbHB_TextChanged(object sender, EventArgs e)
        {
            if (tbHB.Text == null)
            {
                tbHB.Text = "";
            }
            HBStr = tbHB.Text + "|" + (int)numericUpDownBufferSize.Value + "|" + cbMode.SelectedIndex;
        }

        private void numericUpDownBufferSize_ValueChanged(object sender, EventArgs e)
        {
            if (tbHB.Text == null)
            {
                tbHB.Text = "";
            }
            HBStr = tbHB.Text + "|" + (int)numericUpDownBufferSize.Value + "|" + cbMode.SelectedIndex;
        }
    }
}
