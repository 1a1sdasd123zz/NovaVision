using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NovaVision.BaseClass.Collection;
using NovaVision.BaseClass.Communication.CommData;
using NovaVision.BaseClass.Module;
using static NovaVision.BaseClass.Communication.TCP.MyTcpServer;

namespace NovaVision.BaseClass.Communication.TCP
{
    public delegate void TcpEventHandler(object sender, TcpEventArgs e);
    public delegate void TcpJobChangeRequestedEventHandler(object sender, TcpJobChangeRequestedEventArgs e);

    public class MyTcpClient : IFlowState
    {
        private Socket _clientSock;

        private string _serialNum;

        private EndianEnum _endian;

        public static Dictionary<string, MyTcpClient> Dic_client = new Dictionary<string, MyTcpClient>();

        private int _bufferSize = 1024;

        private bool _connected = false;

        public TcpMode mode;

        public FixedPrefixFrame prefixFrame;

        public byte[] fixedPrefixFrameArray;

        private InputsOutputs<Comm_Element, Communication.CommData.Info> _mCommData;

        private Dictionary<string, byte[]> inputByteDic;

        private Dictionary<string, string> inputTypeDic;

        public int startByte_dataInputs;

        public int length_dataInputs;

        public int startByte_dataOutputs;

        public int length_dataOutputs;

        private List<string>[] triggerKey;

        private List<string> inputKeys1;

        public List<string> outputKeys;

        public List<string> inputKeys;

        private Dictionary<string, List<string>> dataBindingsDic;

        private const int ReceiveOperation = 1;

        private const int SendOperation = 0;

        public MyDictionaryEx<Comm_Element> dataInputs;

        public MyDictionaryEx<Comm_Element> dataOutputs;

        private static AutoResetEvent[] autoSendReceiveEvents = new AutoResetEvent[2]
        {
        new AutoResetEvent(initialState: false),
        new AutoResetEvent(initialState: false)
        };

        private ManualResetEvent TimeoutObject = new ManualResetEvent(initialState: false);

        private IPEndPoint _remoteEndPoint;

        private IPEndPoint local;

        private SocketAsyncEventArgs connectArgs;

        public string _localIp = "";

        public int _localPort = 1000;

        public string _remoteIp = "";

        public int _remotePort = 1000;

        public string _hbstr = "HB";

        public bool _hbflag;

        private Dictionary<int, byte[]> AskValue = new Dictionary<int, byte[]>();

        private bool ProtocolFlag = false;

        private int ProtocolTimeOut = 200;

        private object ProtocolLock = new object();

        public TcpBlockElementsCollection controlStatusBlock;

        public bool bvTcpFlag;

        public static int Start = 1;

        public byte[] ScanDataBytes;

        public byte[] AckDataBytes = new byte[25];

        public string SerialNum => _serialNum;

        public string CommTypeName => "TcpClient";

        public bool ControlFlag { get; set; } = false;


        public int BufferSize
        {
            get
            {
                return _bufferSize;
            }
            set
            {
                _bufferSize = value;
            }
        }

        public bool IsConnected => _connected;

        public EndianEnum Endian
        {
            get
            {
                return _endian;
            }
            set
            {
                _endian = value;
            }
        }

        public event Action<string> ReceiveMsgEventHandler;

        public event Action<int> JobChanged;

        public event ConnectedEventHandler CommConnected;

        public event TriggerEventHandler Trigger;

        public event TcpEventHandler SetUserDataEvent;

        public event TcpEventHandler AckUserDataEvent;

        public event TcpEventHandler TriggerAcq;

        public event TcpJobChangeRequestedEventHandler InitialJobLoadEvent;

        public static void CreateNewClient(int localPort, string remoteIp, int remotePort, string sn, string HBStr, bool HBFlag)
        {
            if (Dic_client.ContainsKey(sn))
            {
                if (Dic_client[sn]._localPort != localPort || Dic_client[sn]._remoteIp != remoteIp || Dic_client[sn]._remotePort != remotePort)
                {
                    new MyTcpClient(localPort, remoteIp, remotePort, sn, HBStr, HBFlag);
                }
            }
            else
            {
                new MyTcpClient(localPort, remoteIp, remotePort, sn, HBStr, HBFlag);
            }
        }

        public static void CreateNewClient(string localIp, int localPort, string remoteIp, int remotePort, string sn, string HBStr, bool HBFlag)
        {
            if (Dic_client.ContainsKey(sn))
            {
                if (Dic_client[sn]._localIp != localIp || Dic_client[sn]._localPort != localPort || Dic_client[sn]._remoteIp != remoteIp || Dic_client[sn]._remotePort != remotePort)
                {
                    new MyTcpClient(localIp, localPort, remoteIp, remotePort, sn, HBStr, HBFlag);
                }
            }
            else
            {
                new MyTcpClient(localIp, localPort, remoteIp, remotePort, sn, HBStr, HBFlag);
            }
        }

        public void SetTcpMode(TcpMode _mode)
        {
            mode = _mode;
        }

        public static MyTcpClient GetClientInstance(string sn)
        {
            if (Dic_client.ContainsKey(sn))
            {
                return Dic_client[sn];
            }
            return null;
        }

        private MyTcpClient(int localPort, string remoteIp, int remotePort, string sn, string HBStr, bool HBFlag)
        {
            local = new IPEndPoint(IPAddress.Any, localPort);
            _clientSock = new Socket(local.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            connectArgs = new SocketAsyncEventArgs();
            _remoteEndPoint = new IPEndPoint(IPAddress.Parse(remoteIp), remotePort);
            _serialNum = sn;
            _localPort = localPort;
            _remoteIp = remoteIp;
            _remotePort = remotePort;
            _endian = EndianEnum.LittleEndian;
            mode = TcpMode.DIY;
            ProtocolFlag = false;
            if (HBStr.Contains("|") && HBStr.Split('|').Length == 3)
            {
                _hbstr = HBStr.Split('|')[0];
                _bufferSize = 1024 * Convert.ToInt32(HBStr.Split('|')[1]);
                if (HBStr.Split('|')[2] == "4")
                {
                    ProtocolFlag = true;
                }
            }
            _hbflag = HBFlag;
            if (Dic_client.ContainsKey(_serialNum))
            {
                Dic_client[_serialNum] = this;
            }
            else
            {
                Dic_client.Add(_serialNum, this);
            }
            if (!CommunicationOperator.commCollection.ListKeys.Contains(_serialNum))
            {
                CommunicationOperator.commCollection.Add(_serialNum, this);
            }
            else
            {
                CommunicationOperator.commCollection[_serialNum] = this;
            }
        }

        private MyTcpClient(string localIp, int localPort, string remoteIp, int remotePort, string sn, string HBStr, bool HBFlag)
        {
            local = new IPEndPoint(IPAddress.Parse(localIp), localPort);
            _clientSock = new Socket(local.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            connectArgs = new SocketAsyncEventArgs();
            _localIp = localIp;
            _localPort = localPort;
            _remoteIp = remoteIp;
            _remotePort = remotePort;
            _remoteEndPoint = new IPEndPoint(IPAddress.Parse(remoteIp), remotePort);
            _serialNum = sn;
            _endian = EndianEnum.LittleEndian;
            mode = TcpMode.DIY;
            ProtocolFlag = false;
            if (HBStr.Contains("|") && HBStr.Split('|').Length == 3)
            {
                _hbstr = HBStr.Split('|')[0];
                _bufferSize = 1024 * Convert.ToInt32(HBStr.Split('|')[1]);
                if (HBStr.Split('|')[2] == "4")
                {
                    ProtocolFlag = true;
                }
            }
            _hbflag = HBFlag;
            if (Dic_client.ContainsKey(_serialNum))
            {
                Dic_client[_serialNum] = this;
            }
            else
            {
                Dic_client.Add(_serialNum, this);
            }
            if (!CommunicationOperator.commCollection.ListKeys.Contains(_serialNum))
            {
                CommunicationOperator.commCollection.Add(_serialNum, this);
            }
            else
            {
                CommunicationOperator.commCollection[_serialNum] = this;
            }
        }

        public MyTcpClient(IPEndPoint _local, IPEndPoint remote, string sn)
        {
            local = _local;
            _clientSock = new Socket(local.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            connectArgs = new SocketAsyncEventArgs();
            _remoteEndPoint = remote;
            _serialNum = sn;
            _endian = EndianEnum.LittleEndian;
            mode = TcpMode.DIY;
            if (Dic_client.ContainsKey(_serialNum))
            {
                Dic_client[_serialNum] = this;
            }
            else
            {
                Dic_client.Add(_serialNum, this);
            }
            if (!CommunicationOperator.commCollection.ListKeys.Contains(_serialNum))
            {
                CommunicationOperator.commCollection.Add(_serialNum, this);
            }
            else
            {
                CommunicationOperator.commCollection[_serialNum] = this;
            }
        }

        public void SetInputsOutputs(InputsOutputs<Comm_Element, Communication.CommData.Info> moduleData)
        {
            _mCommData = moduleData;
            Analysis();
            IniProtocolTcp();
        }

        public void Analysis()
        {
            dataInputs = _mCommData.Outputs;
            dataOutputs = _mCommData.Inputs;
            DataBindingCollection dataBindings = _mCommData.DataBindings;
            startByte_dataInputs = _mCommData.OutputsInfo.StartByte;
            length_dataInputs = _mCommData.OutputsInfo.GetBufferLength();
            startByte_dataOutputs = _mCommData.InputsInfo.StartByte;
            length_dataOutputs = _mCommData.InputsInfo.GetBufferLength();
            if (triggerKey == null)
            {
                triggerKey = new List<string>[32];
            }
            for (int j = 0; j < triggerKey.Length; j++)
            {
                if (triggerKey[j] == null)
                {
                    triggerKey[j] = new List<string>();
                }
                else
                {
                    triggerKey[j].Clear();
                }
            }
            if (inputKeys == null)
            {
                inputKeys = new List<string>();
            }
            else
            {
                inputKeys.Clear();
            }
            if (inputKeys1 == null)
            {
                inputKeys1 = new List<string>();
            }
            else
            {
                inputKeys1.Clear();
            }
            if (outputKeys == null)
            {
                outputKeys = new List<string>();
            }
            else
            {
                outputKeys.Clear();
            }
            for (int i = 0; i < dataInputs.Count; i++)
            {
                if (dataInputs[i].IsTriggerPoint)
                {
                    triggerKey[dataInputs[i].Channel].Add(dataInputs.KeyofIndex(i));
                }
                else
                {
                    inputKeys1.Add(dataInputs.KeyofIndex(i));
                }
                inputKeys.Add(dataInputs.KeyofIndex(i));
            }
            for (int k = 0; k < dataOutputs.Count; k++)
            {
                outputKeys.Add(dataOutputs.KeyofIndex(k));
            }
            if (dataBindingsDic == null)
            {
                dataBindingsDic = new Dictionary<string, List<string>>();
            }
            else
            {
                dataBindingsDic.Clear();
            }
            foreach (DataBinding item in (IEnumerable<DataBinding>)dataBindings)
            {
                if (!dataBindingsDic.ContainsKey(item.SourcePath))
                {
                    dataBindingsDic.Add(item.SourcePath, new List<string>());
                }
                dataBindingsDic[item.SourcePath].Add(item.DestinationPath);
            }
        }

        public void Connect()
        {
            if (IsConnected)
            {
                return;
            }
            try
            {
                if (_clientSock == null)
                {
                    throw new Exception("通信对象未初始化");
                }
                if (connectArgs == null)
                {
                    connectArgs = new SocketAsyncEventArgs();
                }
                connectArgs.UserToken = _clientSock;
                connectArgs.RemoteEndPoint = _remoteEndPoint;
                connectArgs.Completed += OnConnected;
                if (!_clientSock.ConnectAsync(connectArgs))
                {
                    ProcessConnected(connectArgs);
                }
            }
            catch (Exception ex)
            {
                _connected = false;
                if (this.CommConnected != null)
                {
                    this.CommConnected(this, isConnected: false);
                }
                LogUtil.LogError("客户端（" + _serialNum + "）开启连接异常，请检查服务器是否正常开启！异常信息：" + ex.Message);
            }
        }

        public bool Connect(int timeout)
        {
            TimeoutObject.Reset();
            try
            {
                if (_clientSock == null)
                {
                    throw new Exception("通信对象未初始化");
                }
                int count = 0;
                while (count < timeout)
                {
                    if (_clientSock.Connected && (!_clientSock.Poll(200, SelectMode.SelectRead) || _clientSock.Available != 0))
                    {
                        return true;
                    }
                    try
                    {
                        _clientSock?.Close();
                        _clientSock.Dispose();
                        _clientSock = null;
                        _clientSock = new Socket(local.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                        if (connectArgs == null)
                        {
                            connectArgs = new SocketAsyncEventArgs();
                        }
                        else
                        {
                            connectArgs.Completed -= OnConnected;
                        }
                        connectArgs.UserToken = _clientSock;
                        connectArgs.RemoteEndPoint = _remoteEndPoint;
                        connectArgs.Completed += OnConnected;
                        if (!_clientSock.ConnectAsync(connectArgs))
                        {
                            ProcessConnected(connectArgs);
                        }
                        TimeoutObject.WaitOne(2000, exitContext: false);
                        if (!_connected)
                        {
                            throw new Exception();
                        }
                    }
                    catch (SocketException ex2)
                    {
                        if (ex2.ErrorCode == 10060)
                        {
                            throw new Exception(ex2.Message);
                        }
                        continue;
                    }
                    finally
                    {
                        count++;
                    }
                    break;
                }
                if (_clientSock == null || !_clientSock.Connected || (_clientSock.Poll(200, SelectMode.SelectRead) && _clientSock.Available == 0))
                {
                    throw new Exception("网络连接失败");
                }
                return true;
            }
            catch (Exception ex)
            {
                _connected = false;
                if (this.CommConnected != null)
                {
                    this.CommConnected(this, isConnected: false);
                }
                LogUtil.LogError("客户端（" + _serialNum + "）开启连接异常，异常信息：" + ex.Message);
                return false;
            }
        }

        private void OnConnected(object sender, SocketAsyncEventArgs e)
        {
            Console.WriteLine("连接回调的线程ID为" + Thread.CurrentThread.ManagedThreadId.ToString("00"));
            ProcessConnected(e);
        }

        private void ProcessConnected(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                _connected = true;
                if (this.CommConnected != null)
                {
                    this.CommConnected(this, _connected);
                }
                Task.Run(delegate
                {
                    while (_connected && !ControlFlag)
                    {
                        if (_hbflag && !ProtocolFlag)
                        {
                            Thread.Sleep(5000);
                            Send(_hbstr + "\r\n");
                        }
                    }
                });
                SocketAsyncEventArgs asyniar = new SocketAsyncEventArgs();
                asyniar.UserToken = _clientSock;
                asyniar.RemoteEndPoint = _remoteEndPoint;
                StartRecive(asyniar);
            }
            else
            {
                _connected = false;
                bvTcpFlag = false;
                if (this.CommConnected != null)
                {
                    this.CommConnected(this, _connected);
                }
                LogUtil.LogError("客户端（" + _serialNum + "）开启连接异常，请检查服务器是否正常开启！");
            }
            TimeoutObject.Set();
        }

        public void Send(byte[] resultData, int? offSet, int channel)
        {
            byte[] channelByteArray = BitConverter.GetBytes(channel);
            byte[] startByteArray2;
            if (offSet.HasValue)
            {
                if (offSet == 0)
                {
                    startByteArray2 = channelByteArray;
                }
                else
                {
                    byte[] placeHolderArray = new byte[offSet.Value];
                    startByteArray2 = new byte[channelByteArray.Length + placeHolderArray.Length];
                    startByteArray2 = channelByteArray.Concat(placeHolderArray).ToArray();
                }
            }
            else
            {
                startByteArray2 = channelByteArray;
            }
            byte[] startByteArray;
            if (resultData != null)
            {
                startByteArray = new byte[startByteArray2.Length + resultData.Length];
                startByteArray = startByteArray2.Concat(resultData).ToArray();
            }
            else
            {
                startByteArray = startByteArray2;
            }
            Send(startByteArray);
        }

        public void Send(byte[] data)
        {
            if (!Connect(50))
            {
                _connected = false;
                return;
            }
            SocketAsyncEventArgs asyniar = new SocketAsyncEventArgs();
            asyniar.Completed += OnSendComplete;
            asyniar.SetBuffer(data, 0, data.Length);
            asyniar.UserToken = _clientSock;
            asyniar.RemoteEndPoint = _remoteEndPoint;
            if (!_clientSock.SendAsync(asyniar))
            {
                ProcessSend(asyniar);
            }
            autoSendReceiveEvents[0].WaitOne();
        }

        public void Send(string dataStr)
        {
            byte[] data = Encoding.ASCII.GetBytes(dataStr);
            Send(data);
        }

        public void SendData(string data)
        {
            Send(data);
        }

        private void OnSendComplete(object sender, SocketAsyncEventArgs e)
        {
            ProcessSend(e);
        }

        private void ProcessSend(SocketAsyncEventArgs e)
        {
            autoSendReceiveEvents[0].Set();
        }

        public void StartRecive(SocketAsyncEventArgs e)
        {
            Socket s = e.UserToken as Socket;
            byte[] receiveBuffer = new byte[_bufferSize];
            e.SetBuffer(receiveBuffer, 0, receiveBuffer.Length);
            e.Completed += OnReceiveComplete;
            if (!s.ReceiveAsync(e))
            {
                ProcessReceive(e);
            }
            autoSendReceiveEvents[1].WaitOne();
        }

        private void OnReceiveComplete(object sender, SocketAsyncEventArgs e)
        {
            ProcessReceive(e);
        }

        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                if (e.BytesTransferred > 0)
                {
                    Socket s = (Socket)e.UserToken;
                    if (s.Available == 0)
                    {
                        byte[] data = new byte[e.BytesTransferred];
                        Array.Copy(e.Buffer, e.Offset, data, 0, data.Length);
                        if (!ProtocolFlag)
                        {
                            string str = Encoding.ASCII.GetString(e.Buffer, e.Offset, e.BytesTransferred);
                            LogUtil.Log($">客户端收到 {s.RemoteEndPoint.ToString()} 数据为 {str}");
                            try
                            {
                                if (_mCommData != null)
                                {
                                    if (inputByteDic == null)
                                    {
                                        inputByteDic = new Dictionary<string, byte[]>();
                                    }
                                    if (inputTypeDic == null)
                                    {
                                        inputTypeDic = new Dictionary<string, string>();
                                    }
                                    int channel = 0;
                                    int jobId = 0;
                                    int changeJobCommand = 0;
                                    byte[] tempArray = new byte[4];
                                    List<bool> list = new List<bool>();
                                    byte[] acqTriggerArray = new byte[4];
                                    switch (mode)
                                    {
                                        case TcpMode.Software2Plc:
                                            {
                                                Array.Copy(data, tempArray, 4);
                                                channel = BitConverter.ToInt32(tempArray, 0);
                                                Array.Copy(data, 4, tempArray, 0, 4);
                                                jobId = BitConverter.ToInt32(tempArray, 0);
                                                Array.Copy(data, 8, acqTriggerArray, 0, 4);
                                                Array.Copy(data, 12, tempArray, 0, 4);
                                                changeJobCommand = BitConverter.ToInt32(tempArray, 0);
                                                list.Clear();
                                                list.Add((acqTriggerArray[0] & 1) == 1);
                                                list.Add((acqTriggerArray[0] & 2) == 2);
                                                list.Add((acqTriggerArray[0] & 4) == 4);
                                                list.Add((acqTriggerArray[0] & 8) == 8);
                                                list.Add((acqTriggerArray[0] & 0x10) == 16);
                                                list.Add((acqTriggerArray[0] & 0x20) == 32);
                                                list.Add((acqTriggerArray[0] & 0x40) == 64);
                                                list.Add((acqTriggerArray[0] & 0x80) == 128);
                                                list.Add((acqTriggerArray[1] & 1) == 1);
                                                list.Add((acqTriggerArray[1] & 2) == 2);
                                                list.Add((acqTriggerArray[1] & 4) == 4);
                                                list.Add((acqTriggerArray[1] & 8) == 8);
                                                list.Add((acqTriggerArray[1] & 0x10) == 16);
                                                list.Add((acqTriggerArray[1] & 0x20) == 32);
                                                list.Add((acqTriggerArray[1] & 0x40) == 64);
                                                list.Add((acqTriggerArray[1] & 0x80) == 128);
                                                list.Add((acqTriggerArray[2] & 1) == 1);
                                                list.Add((acqTriggerArray[2] & 2) == 2);
                                                list.Add((acqTriggerArray[2] & 4) == 4);
                                                list.Add((acqTriggerArray[2] & 8) == 8);
                                                list.Add((acqTriggerArray[2] & 0x10) == 16);
                                                list.Add((acqTriggerArray[2] & 0x20) == 32);
                                                list.Add((acqTriggerArray[2] & 0x40) == 64);
                                                list.Add((acqTriggerArray[2] & 0x80) == 128);
                                                list.Add((acqTriggerArray[3] & 1) == 1);
                                                list.Add((acqTriggerArray[3] & 2) == 2);
                                                list.Add((acqTriggerArray[3] & 4) == 4);
                                                list.Add((acqTriggerArray[3] & 8) == 8);
                                                list.Add((acqTriggerArray[3] & 0x10) == 16);
                                                list.Add((acqTriggerArray[3] & 0x20) == 32);
                                                list.Add((acqTriggerArray[3] & 0x40) == 64);
                                                list.Add((acqTriggerArray[3] & 0x80) == 128);
                                                bool[] acqTrigger = list.ToArray();
                                                for (int j = 0; j < triggerKey.Length; j++)
                                                {
                                                    if (triggerKey[j].Count <= 0)
                                                    {
                                                        continue;
                                                    }
                                                    foreach (string key in triggerKey[j])
                                                    {
                                                        int offset = _mCommData.Outputs[key].ByteOffset + startByte_dataInputs;
                                                        int size = _mCommData.Outputs[key].TypeLength;
                                                        string typeName = _mCommData.Outputs[key].Type;
                                                        byte[] tempData = new byte[size];
                                                        Array.Copy(data, 16 + offset, tempData, 0, size);
                                                        if (!inputByteDic.ContainsKey(key))
                                                        {
                                                            inputByteDic.Add(key, tempData);
                                                            inputTypeDic.Add(key, typeName);
                                                        }
                                                        else
                                                        {
                                                            inputByteDic[key] = tempData;
                                                            inputTypeDic[key] = typeName;
                                                        }
                                                    }
                                                }
                                                for (int i = 0; i < acqTrigger.Length; i++)
                                                {
                                                    int k3 = i;
                                                    if (acqTrigger[k3])
                                                    {
                                                        Task.Run(delegate
                                                        {
                                                            TriggerAcquisition(k3);
                                                        });
                                                    }
                                                }
                                                foreach (string key2 in inputKeys1)
                                                {
                                                    if (_mCommData.Outputs[key2].Channel == channel)
                                                    {
                                                        int offset2 = _mCommData.Outputs[key2].ByteOffset + startByte_dataInputs;
                                                        int size2 = _mCommData.Outputs[key2].TypeLength;
                                                        string typeName2 = _mCommData.Outputs[key2].Type;
                                                        byte[] tempData2 = new byte[size2];
                                                        Array.Copy(data, 16 + offset2, tempData2, 0, size2);
                                                        switch (typeName2)
                                                        {
                                                            case "Boolean":
                                                                _mCommData.Outputs[key2].Value.mValue = (tempData2[0] & 1) == 1;
                                                                break;
                                                            case "Byte":
                                                                _mCommData.Outputs[key2].Value.mValue = tempData2[0];
                                                                break;
                                                            case "Char":
                                                                _mCommData.Outputs[key2].Value.mValue = (char)((uint)((tempData2[0] & 0xFF) << 8) | (tempData2[1] & 0xFFu));
                                                                break;
                                                            case "UInt16":
                                                                _mCommData.Outputs[key2].Value.mValue = BitConverter.ToUInt16(tempData2, 0);
                                                                break;
                                                            case "UInt32":
                                                                _mCommData.Outputs[key2].Value.mValue = BitConverter.ToUInt32(tempData2, 0);
                                                                break;
                                                            case "UInt64":
                                                                _mCommData.Outputs[key2].Value.mValue = BitConverter.ToUInt64(tempData2, 0);
                                                                break;
                                                            case "Int16":
                                                                _mCommData.Outputs[key2].Value.mValue = BitConverter.ToInt16(tempData2, 0);
                                                                break;
                                                            case "Int32":
                                                                _mCommData.Outputs[key2].Value.mValue = BitConverter.ToInt32(tempData2, 0);
                                                                break;
                                                            case "Int64":
                                                                _mCommData.Outputs[key2].Value.mValue = BitConverter.ToInt64(tempData2, 0);
                                                                break;
                                                            case "Single":
                                                                _mCommData.Outputs[key2].Value.mValue = BitConverter.ToSingle(tempData2, 0);
                                                                break;
                                                            case "Double":
                                                                _mCommData.Outputs[key2].Value.mValue = BitConverter.ToDouble(tempData2, 0);
                                                                break;
                                                            case "String":
                                                                _mCommData.Outputs[key2].Value.mValue = Encoding.ASCII.GetString(tempData2);
                                                                break;
                                                        }
                                                    }
                                                }
                                                break;
                                            }
                                        case TcpMode.MasterSoft2Soft:
                                            {
                                                Array.Copy(data, tempArray, 4);
                                                channel = BitConverter.ToInt32(tempArray, 0);
                                                Array.Copy(data, 4, tempArray, 0, 4);
                                                jobId = BitConverter.ToInt32(tempArray, 0);
                                                byte[] acqTriggerReadyArray = new byte[4];
                                                Array.Copy(data, 8, acqTriggerReadyArray, 0, 4);
                                                byte[] acqCompletedArray = new byte[4];
                                                Array.Copy(data, 12, acqCompletedArray, 0, 4);
                                                Array.Copy(data, 16, tempArray, 0, 4);
                                                int systemStatus = BitConverter.ToInt32(tempArray, 0);
                                                list.Clear();
                                                list.Add((acqTriggerReadyArray[0] & 1) == 1);
                                                list.Add((acqTriggerReadyArray[0] & 2) == 2);
                                                list.Add((acqTriggerReadyArray[0] & 4) == 4);
                                                list.Add((acqTriggerReadyArray[0] & 8) == 8);
                                                list.Add((acqTriggerReadyArray[0] & 0x10) == 16);
                                                list.Add((acqTriggerReadyArray[0] & 0x20) == 32);
                                                list.Add((acqTriggerReadyArray[0] & 0x40) == 64);
                                                list.Add((acqTriggerReadyArray[0] & 0x80) == 128);
                                                list.Add((acqTriggerReadyArray[1] & 1) == 1);
                                                list.Add((acqTriggerReadyArray[1] & 2) == 2);
                                                list.Add((acqTriggerReadyArray[1] & 4) == 4);
                                                list.Add((acqTriggerReadyArray[1] & 8) == 8);
                                                list.Add((acqTriggerReadyArray[1] & 0x10) == 16);
                                                list.Add((acqTriggerReadyArray[1] & 0x20) == 32);
                                                list.Add((acqTriggerReadyArray[1] & 0x40) == 64);
                                                list.Add((acqTriggerReadyArray[1] & 0x80) == 128);
                                                list.Add((acqTriggerReadyArray[2] & 1) == 1);
                                                list.Add((acqTriggerReadyArray[2] & 2) == 2);
                                                list.Add((acqTriggerReadyArray[2] & 4) == 4);
                                                list.Add((acqTriggerReadyArray[2] & 8) == 8);
                                                list.Add((acqTriggerReadyArray[2] & 0x10) == 16);
                                                list.Add((acqTriggerReadyArray[2] & 0x20) == 32);
                                                list.Add((acqTriggerReadyArray[2] & 0x40) == 64);
                                                list.Add((acqTriggerReadyArray[2] & 0x80) == 128);
                                                list.Add((acqTriggerReadyArray[3] & 1) == 1);
                                                list.Add((acqTriggerReadyArray[3] & 2) == 2);
                                                list.Add((acqTriggerReadyArray[3] & 4) == 4);
                                                list.Add((acqTriggerReadyArray[3] & 8) == 8);
                                                list.Add((acqTriggerReadyArray[3] & 0x10) == 16);
                                                list.Add((acqTriggerReadyArray[3] & 0x20) == 32);
                                                list.Add((acqTriggerReadyArray[3] & 0x40) == 64);
                                                list.Add((acqTriggerReadyArray[3] & 0x80) == 128);
                                                bool[] triggerReady = list.ToArray();
                                                list.Clear();
                                                list.Add((acqCompletedArray[0] & 1) == 1);
                                                list.Add((acqCompletedArray[0] & 2) == 2);
                                                list.Add((acqCompletedArray[0] & 4) == 4);
                                                list.Add((acqCompletedArray[0] & 8) == 8);
                                                list.Add((acqCompletedArray[0] & 0x10) == 16);
                                                list.Add((acqCompletedArray[0] & 0x20) == 32);
                                                list.Add((acqCompletedArray[0] & 0x40) == 64);
                                                list.Add((acqCompletedArray[0] & 0x80) == 128);
                                                list.Add((acqCompletedArray[1] & 1) == 1);
                                                list.Add((acqCompletedArray[1] & 2) == 2);
                                                list.Add((acqCompletedArray[1] & 4) == 4);
                                                list.Add((acqCompletedArray[1] & 8) == 8);
                                                list.Add((acqCompletedArray[1] & 0x10) == 16);
                                                list.Add((acqCompletedArray[1] & 0x20) == 32);
                                                list.Add((acqCompletedArray[1] & 0x40) == 64);
                                                list.Add((acqCompletedArray[1] & 0x80) == 128);
                                                list.Add((acqCompletedArray[2] & 1) == 1);
                                                list.Add((acqCompletedArray[2] & 2) == 2);
                                                list.Add((acqCompletedArray[2] & 4) == 4);
                                                list.Add((acqCompletedArray[2] & 8) == 8);
                                                list.Add((acqCompletedArray[2] & 0x10) == 16);
                                                list.Add((acqCompletedArray[2] & 0x20) == 32);
                                                list.Add((acqCompletedArray[2] & 0x40) == 64);
                                                list.Add((acqCompletedArray[2] & 0x80) == 128);
                                                list.Add((acqCompletedArray[3] & 1) == 1);
                                                list.Add((acqCompletedArray[3] & 2) == 2);
                                                list.Add((acqCompletedArray[3] & 4) == 4);
                                                list.Add((acqCompletedArray[3] & 8) == 8);
                                                list.Add((acqCompletedArray[3] & 0x10) == 16);
                                                list.Add((acqCompletedArray[3] & 0x20) == 32);
                                                list.Add((acqCompletedArray[3] & 0x40) == 64);
                                                list.Add((acqCompletedArray[3] & 0x80) == 128);
                                                bool[] acqCompleted = list.ToArray();
                                                for (int l = 0; l < triggerReady.Length; l++)
                                                {
                                                    int k5 = l;
                                                    if (triggerReady[k5])
                                                    {
                                                        Task.Run(delegate
                                                        {
                                                            TriggerReady(k5);
                                                        });
                                                    }
                                                }
                                                for (int k = 0; k < acqCompleted.Length; k++)
                                                {
                                                    int k4 = k;
                                                    if (acqCompleted[k4])
                                                    {
                                                        Task.Run(delegate
                                                        {
                                                            AcquisitionCompleted(k4);
                                                        });
                                                    }
                                                }
                                                foreach (string key3 in inputKeys1)
                                                {
                                                    if (_mCommData.Outputs[key3].Channel == channel)
                                                    {
                                                        int offset3 = _mCommData.Outputs[key3].ByteOffset + startByte_dataInputs;
                                                        int size3 = _mCommData.Outputs[key3].TypeLength;
                                                        string typeName3 = _mCommData.Outputs[key3].Type;
                                                        byte[] tempData3 = new byte[size3];
                                                        Array.Copy(data, 20 + offset3, tempData3, 0, size3);
                                                        switch (typeName3)
                                                        {
                                                            case "Boolean":
                                                                _mCommData.Outputs[key3].Value.mValue = (tempData3[0] & 1) == 1;
                                                                break;
                                                            case "Byte":
                                                                _mCommData.Outputs[key3].Value.mValue = tempData3[0];
                                                                break;
                                                            case "Char":
                                                                _mCommData.Outputs[key3].Value.mValue = (char)((uint)((tempData3[0] & 0xFF) << 8) | (tempData3[1] & 0xFFu));
                                                                break;
                                                            case "UInt16":
                                                                _mCommData.Outputs[key3].Value.mValue = BitConverter.ToUInt16(tempData3, 0);
                                                                break;
                                                            case "UInt32":
                                                                _mCommData.Outputs[key3].Value.mValue = BitConverter.ToUInt32(tempData3, 0);
                                                                break;
                                                            case "UInt64":
                                                                _mCommData.Outputs[key3].Value.mValue = BitConverter.ToUInt64(tempData3, 0);
                                                                break;
                                                            case "Int16":
                                                                _mCommData.Outputs[key3].Value.mValue = BitConverter.ToInt16(tempData3, 0);
                                                                break;
                                                            case "Int32":
                                                                _mCommData.Outputs[key3].Value.mValue = BitConverter.ToInt32(tempData3, 0);
                                                                break;
                                                            case "Int64":
                                                                _mCommData.Outputs[key3].Value.mValue = BitConverter.ToInt64(tempData3, 0);
                                                                break;
                                                            case "Single":
                                                                _mCommData.Outputs[key3].Value.mValue = BitConverter.ToSingle(tempData3, 0);
                                                                break;
                                                            case "Double":
                                                                _mCommData.Outputs[key3].Value.mValue = BitConverter.ToDouble(tempData3, 0);
                                                                break;
                                                            case "String":
                                                                _mCommData.Outputs[key3].Value.mValue = Encoding.ASCII.GetString(tempData3);
                                                                break;
                                                        }
                                                    }
                                                }
                                                break;
                                            }
                                        case TcpMode.ControledSoft2Soft:
                                            {
                                                Array.Copy(data, tempArray, 4);
                                                channel = BitConverter.ToInt32(tempArray, 0);
                                                Array.Copy(data, 4, tempArray, 0, 4);
                                                jobId = BitConverter.ToInt32(tempArray, 0);
                                                Array.Copy(data, 8, acqTriggerArray, 0, 4);
                                                Array.Copy(data, 12, tempArray, 0, 4);
                                                changeJobCommand = BitConverter.ToInt32(tempArray, 0);
                                                list.Clear();
                                                list.Add((acqTriggerArray[0] & 1) == 1);
                                                list.Add((acqTriggerArray[0] & 2) == 2);
                                                list.Add((acqTriggerArray[0] & 4) == 4);
                                                list.Add((acqTriggerArray[0] & 8) == 8);
                                                list.Add((acqTriggerArray[0] & 0x10) == 16);
                                                list.Add((acqTriggerArray[0] & 0x20) == 32);
                                                list.Add((acqTriggerArray[0] & 0x40) == 64);
                                                list.Add((acqTriggerArray[0] & 0x80) == 128);
                                                list.Add((acqTriggerArray[1] & 1) == 1);
                                                list.Add((acqTriggerArray[1] & 2) == 2);
                                                list.Add((acqTriggerArray[1] & 4) == 4);
                                                list.Add((acqTriggerArray[1] & 8) == 8);
                                                list.Add((acqTriggerArray[1] & 0x10) == 16);
                                                list.Add((acqTriggerArray[1] & 0x20) == 32);
                                                list.Add((acqTriggerArray[1] & 0x40) == 64);
                                                list.Add((acqTriggerArray[1] & 0x80) == 128);
                                                list.Add((acqTriggerArray[2] & 1) == 1);
                                                list.Add((acqTriggerArray[2] & 2) == 2);
                                                list.Add((acqTriggerArray[2] & 4) == 4);
                                                list.Add((acqTriggerArray[2] & 8) == 8);
                                                list.Add((acqTriggerArray[2] & 0x10) == 16);
                                                list.Add((acqTriggerArray[2] & 0x20) == 32);
                                                list.Add((acqTriggerArray[2] & 0x40) == 64);
                                                list.Add((acqTriggerArray[2] & 0x80) == 128);
                                                list.Add((acqTriggerArray[3] & 1) == 1);
                                                list.Add((acqTriggerArray[3] & 2) == 2);
                                                list.Add((acqTriggerArray[3] & 4) == 4);
                                                list.Add((acqTriggerArray[3] & 8) == 8);
                                                list.Add((acqTriggerArray[3] & 0x10) == 16);
                                                list.Add((acqTriggerArray[3] & 0x20) == 32);
                                                list.Add((acqTriggerArray[3] & 0x40) == 64);
                                                list.Add((acqTriggerArray[3] & 0x80) == 128);
                                                bool[] acqTrigger = list.ToArray();
                                                for (int n = 0; n < triggerKey.Length; n++)
                                                {
                                                    if (triggerKey[n].Count <= 0)
                                                    {
                                                        continue;
                                                    }
                                                    foreach (string key4 in triggerKey[n])
                                                    {
                                                        int offset4 = _mCommData.Outputs[key4].ByteOffset + startByte_dataInputs;
                                                        int size4 = _mCommData.Outputs[key4].TypeLength;
                                                        string typeName4 = _mCommData.Outputs[key4].Type;
                                                        byte[] tempData4 = new byte[size4];
                                                        Array.Copy(data, 16 + offset4, tempData4, 0, size4);
                                                        if (!inputByteDic.ContainsKey(key4))
                                                        {
                                                            inputByteDic.Add(key4, tempData4);
                                                            inputTypeDic.Add(key4, typeName4);
                                                        }
                                                        else
                                                        {
                                                            inputByteDic[key4] = tempData4;
                                                            inputTypeDic[key4] = typeName4;
                                                        }
                                                    }
                                                }
                                                for (int m = 0; m < acqTrigger.Length; m++)
                                                {
                                                    int k2 = m;
                                                    if (acqTrigger[k2])
                                                    {
                                                        Task.Run(delegate
                                                        {
                                                            TriggerAcquisition(k2);
                                                        });
                                                    }
                                                }
                                                foreach (string key5 in inputKeys1)
                                                {
                                                    if (_mCommData.Outputs[key5].Channel == channel)
                                                    {
                                                        int offset5 = _mCommData.Outputs[key5].ByteOffset + startByte_dataInputs;
                                                        int size5 = _mCommData.Outputs[key5].TypeLength;
                                                        string typeName5 = _mCommData.Outputs[key5].Type;
                                                        byte[] tempData5 = new byte[size5];
                                                        Array.Copy(data, 16 + offset5, tempData5, 0, size5);
                                                        switch (typeName5)
                                                        {
                                                            case "Boolean":
                                                                _mCommData.Outputs[key5].Value.mValue = (tempData5[0] & 1) == 1;
                                                                break;
                                                            case "Byte":
                                                                _mCommData.Outputs[key5].Value.mValue = tempData5[0];
                                                                break;
                                                            case "Char":
                                                                _mCommData.Outputs[key5].Value.mValue = (char)((uint)((tempData5[0] & 0xFF) << 8) | (tempData5[1] & 0xFFu));
                                                                break;
                                                            case "UInt16":
                                                                _mCommData.Outputs[key5].Value.mValue = BitConverter.ToUInt16(tempData5, 0);
                                                                break;
                                                            case "UInt32":
                                                                _mCommData.Outputs[key5].Value.mValue = BitConverter.ToUInt32(tempData5, 0);
                                                                break;
                                                            case "UInt64":
                                                                _mCommData.Outputs[key5].Value.mValue = BitConverter.ToUInt64(tempData5, 0);
                                                                break;
                                                            case "Int16":
                                                                _mCommData.Outputs[key5].Value.mValue = BitConverter.ToInt16(tempData5, 0);
                                                                break;
                                                            case "Int32":
                                                                _mCommData.Outputs[key5].Value.mValue = BitConverter.ToInt32(tempData5, 0);
                                                                break;
                                                            case "Int64":
                                                                _mCommData.Outputs[key5].Value.mValue = BitConverter.ToInt64(tempData5, 0);
                                                                break;
                                                            case "Single":
                                                                _mCommData.Outputs[key5].Value.mValue = BitConverter.ToSingle(tempData5, 0);
                                                                break;
                                                            case "Double":
                                                                _mCommData.Outputs[key5].Value.mValue = BitConverter.ToDouble(tempData5, 0);
                                                                break;
                                                            case "String":
                                                                _mCommData.Outputs[key5].Value.mValue = Encoding.ASCII.GetString(tempData5);
                                                                break;
                                                        }
                                                    }
                                                }
                                                break;
                                            }
                                        case TcpMode.DIY:
                                            {
                                                foreach (string key6 in inputKeys1)
                                                {
                                                    if (_mCommData.Outputs[key6].Channel != channel)
                                                    {
                                                        continue;
                                                    }
                                                    int offset7 = _mCommData.Outputs[key6].ByteOffset + startByte_dataInputs;
                                                    int size7 = _mCommData.Outputs[key6].TypeLength;
                                                    if (size7 > data.Length - offset7)
                                                    {
                                                        size7 = data.Length - offset7;
                                                    }
                                                    string typeName7 = _mCommData.Outputs[key6].Type;
                                                    byte[] tempData7 = new byte[size7];
                                                    Array.Copy(data, offset7, tempData7, 0, size7);
                                                    switch (typeName7)
                                                    {
                                                        case "Boolean":
                                                            _mCommData.Outputs[key6].Value.mValue = (tempData7[0] & 1) == 1;
                                                            break;
                                                        case "SByte":
                                                            if (tempData7[0] > 127)
                                                            {
                                                                _mCommData.Outputs[key6].Value.mValue = (sbyte)(tempData7[0] - 256);
                                                            }
                                                            else
                                                            {
                                                                _mCommData.Outputs[key6].Value.mValue = (sbyte)tempData7[0];
                                                            }
                                                            break;
                                                        case "Byte":
                                                            _mCommData.Outputs[key6].Value.mValue = tempData7[0];
                                                            break;
                                                        case "Char":
                                                            _mCommData.Outputs[key6].Value.mValue = (char)((uint)((tempData7[0] & 0xFF) << 8) | (tempData7[1] & 0xFFu));
                                                            break;
                                                        case "UInt16":
                                                            _mCommData.Outputs[key6].Value.mValue = BitConverter.ToUInt16(tempData7, 0);
                                                            break;
                                                        case "UInt32":
                                                            _mCommData.Outputs[key6].Value.mValue = BitConverter.ToUInt32(tempData7, 0);
                                                            break;
                                                        case "UInt64":
                                                            _mCommData.Outputs[key6].Value.mValue = BitConverter.ToUInt64(tempData7, 0);
                                                            break;
                                                        case "Int16":
                                                            _mCommData.Outputs[key6].Value.mValue = BitConverter.ToInt16(tempData7, 0);
                                                            break;
                                                        case "Int32":
                                                            _mCommData.Outputs[key6].Value.mValue = BitConverter.ToInt32(tempData7, 0);
                                                            break;
                                                        case "Int64":
                                                            _mCommData.Outputs[key6].Value.mValue = BitConverter.ToInt64(tempData7, 0);
                                                            break;
                                                        case "Single":
                                                            _mCommData.Outputs[key6].Value.mValue = BitConverter.ToSingle(tempData7, 0);
                                                            break;
                                                        case "Double":
                                                            _mCommData.Outputs[key6].Value.mValue = BitConverter.ToDouble(tempData7, 0);
                                                            break;
                                                        case "String":
                                                            _mCommData.Outputs[key6].Value.mValue = Encoding.ASCII.GetString(tempData7);
                                                            break;
                                                    }
                                                }
                                                for (int i2 = 0; i2 < triggerKey.Length; i2++)
                                                {
                                                    if (triggerKey[i2].Count <= 0)
                                                    {
                                                        continue;
                                                    }
                                                    foreach (string key7 in triggerKey[i2])
                                                    {
                                                        if (_mCommData.Outputs[key7].Channel != channel)
                                                        {
                                                            continue;
                                                        }
                                                        int offset6 = _mCommData.Outputs[key7].ByteOffset + startByte_dataInputs;
                                                        int size6 = _mCommData.Outputs[key7].TypeLength;
                                                        if (size6 > data.Length - offset6)
                                                        {
                                                            size6 = data.Length - offset6;
                                                        }
                                                        string typeName6 = _mCommData.Outputs[key7].Type;
                                                        byte[] tempData6 = new byte[size6];
                                                        Array.Copy(data, offset6, tempData6, 0, size6);
                                                        switch (typeName6)
                                                        {
                                                            case "Boolean":
                                                                _mCommData.Outputs[key7].Value.mValue = (tempData6[0] & 1) == 1;
                                                                break;
                                                            case "SByte":
                                                                if (tempData6[0] > 127)
                                                                {
                                                                    _mCommData.Outputs[key7].Value.mValue = (sbyte)(tempData6[0] - 256);
                                                                }
                                                                else
                                                                {
                                                                    _mCommData.Outputs[key7].Value.mValue = (sbyte)tempData6[0];
                                                                }
                                                                break;
                                                            case "Byte":
                                                                _mCommData.Outputs[key7].Value.mValue = tempData6[0];
                                                                break;
                                                            case "Char":
                                                                _mCommData.Outputs[key7].Value.mValue = (char)((uint)((tempData6[0] & 0xFF) << 8) | (tempData6[1] & 0xFFu));
                                                                break;
                                                            case "UInt16":
                                                                _mCommData.Outputs[key7].Value.mValue = BitConverter.ToUInt16(tempData6, 0);
                                                                break;
                                                            case "UInt32":
                                                                _mCommData.Outputs[key7].Value.mValue = BitConverter.ToUInt32(tempData6, 0);
                                                                break;
                                                            case "UInt64":
                                                                _mCommData.Outputs[key7].Value.mValue = BitConverter.ToUInt64(tempData6, 0);
                                                                break;
                                                            case "Int16":
                                                                _mCommData.Outputs[key7].Value.mValue = BitConverter.ToInt16(tempData6, 0);
                                                                break;
                                                            case "Int32":
                                                                _mCommData.Outputs[key7].Value.mValue = BitConverter.ToInt32(tempData6, 0);
                                                                break;
                                                            case "Int64":
                                                                _mCommData.Outputs[key7].Value.mValue = BitConverter.ToInt64(tempData6, 0);
                                                                break;
                                                            case "Single":
                                                                _mCommData.Outputs[key7].Value.mValue = BitConverter.ToSingle(tempData6, 0);
                                                                break;
                                                            case "Double":
                                                                _mCommData.Outputs[key7].Value.mValue = BitConverter.ToDouble(tempData6, 0);
                                                                break;
                                                            case "String":
                                                                _mCommData.Outputs[key7].Value.mValue = Encoding.ASCII.GetString(tempData6);
                                                                break;
                                                        }
                                                    }
                                                }
                                                break;
                                            }
                                    }
                                }
                                if (this.Trigger != null)
                                {
                                    //this.Trigger(this, 0,0);
                                }
                            }
                            catch (Exception ex)
                            {
                                LogUtil.LogError("解析数据出现异常，异常信息：" + ex.Message);
                            }
                        }
                        else
                        {
                            GetAsk(data);
                        }
                    }
                    if (!s.ReceiveAsync(e))
                    {
                        ProcessReceive(e);
                    }
                }
                else
                {
                    _connected = false;
                    bvTcpFlag = false;
                    if (this.CommConnected != null)
                    {
                        this.CommConnected(this, isConnected: false);
                    }
                    LogUtil.LogError("远程主机关闭连接，请检查服务器状态！");
                }
            }
            autoSendReceiveEvents[1].Set();
        }

        private void ProcessError(SocketAsyncEventArgs e)
        {
            Socket s = e.UserToken as Socket;
            if (s.Connected)
            {
                try
                {
                    s.Shutdown(SocketShutdown.Both);
                }
                catch (Exception)
                {
                }
                finally
                {
                    if (s.Connected)
                    {
                        s.Close();
                    }
                }
            }
            throw new SocketException((int)e.SocketError);
        }

        public void Dispose()
        {
            autoSendReceiveEvents[0].Close();
            autoSendReceiveEvents[1].Close();
            if (_clientSock.Connected)
            {
                _clientSock.Close();
                _connected = false;
                bvTcpFlag = false;
            }
        }

        public void Close()
        {
            try
            {
                _clientSock.Disconnect(reuseSocket: false);
                _connected = false;
                bvTcpFlag = false;
                Dic_client.Remove(_serialNum);
            }
            catch (Exception ex)
            {
                LogUtil.LogError("客户端（" + _serialNum + "）关闭连接异常，异常信息：" + ex.Message);
            }
        }

        public void AcqCompeleted(Comm_Element comm_Element)
        {
        }

        public void InspectCompeleted(List<Comm_Element> comm_Element)
        {
            if (!ProtocolFlag)
            {
                switch (mode)
                {
                    case TcpMode.Software2Plc:
                        SendData0(comm_Element);
                        break;
                    case TcpMode.MasterSoft2Soft:
                        SendData1(comm_Element);
                        break;
                    case TcpMode.ControledSoft2Soft:
                        SendData2(comm_Element);
                        break;
                    case TcpMode.DIY:
                        SendData3(comm_Element);
                        break;
                }
            }
            else
            {
                WriteBytes(comm_Element);
            }
        }

        public void JobChangeCompeleted(int currentJobId)
        {
            prefixFrame.JobId = currentJobId;
            switch (mode)
            {
                case TcpMode.Software2Plc:
                    AssemblePrefixFrame0();
                    Send(fixedPrefixFrameArray);
                    break;
                case TcpMode.MasterSoft2Soft:
                    AssemblePrefixFrame1();
                    Send(fixedPrefixFrameArray);
                    break;
                case TcpMode.ControledSoft2Soft:
                    AssemblePrefixFrame0();
                    Send(fixedPrefixFrameArray);
                    break;
                case TcpMode.DIY:
                    Send(BitConverter.GetBytes(currentJobId));
                    break;
            }
        }


        public void ResetSystemState()
        {
        }

        public void TriggerAcquisition(int acqChannel)
        {
            if (triggerKey[acqChannel] != null)
            {
                foreach (string key in triggerKey[acqChannel])
                {
                    switch (inputTypeDic[key])
                    {
                        case "Boolean":
                            _mCommData.Outputs[key].Value.mValue = (inputByteDic[key][0] & 1) == 1;
                            break;
                        case "SByte":
                            if (inputByteDic[key][0] > 127)
                            {
                                _mCommData.Outputs[key].Value.mValue = (sbyte)(inputByteDic[key][0] - 256);
                            }
                            else
                            {
                                _mCommData.Outputs[key].Value.mValue = (sbyte)inputByteDic[key][0];
                            }
                            break;
                        case "Byte":
                            _mCommData.Outputs[key].Value.mValue = inputByteDic[key][0];
                            break;
                        case "Char":
                            _mCommData.Outputs[key].Value.mValue = (char)((uint)((inputByteDic[key][0] & 0xFF) << 8) | (inputByteDic[key][1] & 0xFFu));
                            break;
                        case "UInt16":
                            _mCommData.Outputs[key].Value.mValue = BitConverter.ToUInt16(inputByteDic[key], 0);
                            break;
                        case "UInt32":
                            _mCommData.Outputs[key].Value.mValue = BitConverter.ToUInt32(inputByteDic[key], 0);
                            break;
                        case "UInt64":
                            _mCommData.Outputs[key].Value.mValue = BitConverter.ToUInt64(inputByteDic[key], 0);
                            break;
                        case "Int16":
                            _mCommData.Outputs[key].Value.mValue = BitConverter.ToInt16(inputByteDic[key], 0);
                            break;
                        case "Int32":
                            _mCommData.Outputs[key].Value.mValue = BitConverter.ToInt32(inputByteDic[key], 0);
                            break;
                        case "Int64":
                            _mCommData.Outputs[key].Value.mValue = BitConverter.ToInt64(inputByteDic[key], 0);
                            break;
                        case "Single":
                            _mCommData.Outputs[key].Value.mValue = BitConverter.ToSingle(inputByteDic[key], 0);
                            break;
                        case "Double":
                            _mCommData.Outputs[key].Value.mValue = BitConverter.ToDouble(inputByteDic[key], 0);
                            break;
                        case "String":
                            _mCommData.Outputs[key].Value.mValue = Encoding.ASCII.GetString(inputByteDic[key]);
                            break;
                    }
                }
                return;
            }
            LogUtil.LogError($"通道{acqChannel}触发点位为空");
        }

        public void TriggerReady(int triggerChannel)
        {
        }

        public void AcquisitionCompleted(int acqChannel)
        {
        }

        private void AssemblePrefixFrame0()
        {
            byte[] channelByteArray = BitConverter.GetBytes(prefixFrame.Channel);
            byte[] jobIdArray = BitConverter.GetBytes(prefixFrame.JobId);
            byte[] acqReadyArray = prefixFrame.AcqReady;
            byte[] acqCompletedArray = prefixFrame.AcqCompleted;
            byte[] systemStatusArray = BitConverter.GetBytes(prefixFrame.SystemStatus);
            int len = channelByteArray.Length + jobIdArray.Length + acqCompletedArray.Length + acqCompletedArray.Length + systemStatusArray.Length;
            fixedPrefixFrameArray = new byte[len];
            Array.Copy(channelByteArray, 0, fixedPrefixFrameArray, 0, channelByteArray.Length);
            Array.Copy(jobIdArray, 0, fixedPrefixFrameArray, channelByteArray.Length, jobIdArray.Length);
            Array.Copy(acqReadyArray, 0, fixedPrefixFrameArray, channelByteArray.Length + jobIdArray.Length, acqReadyArray.Length);
            Array.Copy(acqCompletedArray, 0, fixedPrefixFrameArray, channelByteArray.Length + jobIdArray.Length + acqReadyArray.Length, acqCompletedArray.Length);
            Array.Copy(systemStatusArray, 0, fixedPrefixFrameArray, channelByteArray.Length + jobIdArray.Length + acqReadyArray.Length + acqCompletedArray.Length, systemStatusArray.Length);
        }

        private void AssemblePrefixFrame1()
        {
            byte[] channelByteArray = BitConverter.GetBytes(prefixFrame.Channel);
            byte[] jobIdArray = BitConverter.GetBytes(prefixFrame.JobId);
            byte[] triggerSignalArray = prefixFrame.TriggerSignal;
            byte[] changeJobExecArray = BitConverter.GetBytes(prefixFrame.ChangeJobExec);
            int len = channelByteArray.Length + jobIdArray.Length + triggerSignalArray.Length + changeJobExecArray.Length;
            fixedPrefixFrameArray = new byte[len];
            Array.Copy(channelByteArray, 0, fixedPrefixFrameArray, 0, channelByteArray.Length);
            Array.Copy(jobIdArray, 0, fixedPrefixFrameArray, channelByteArray.Length, jobIdArray.Length);
            Array.Copy(triggerSignalArray, 0, fixedPrefixFrameArray, channelByteArray.Length + jobIdArray.Length, triggerSignalArray.Length);
            Array.Copy(changeJobExecArray, 0, fixedPrefixFrameArray, channelByteArray.Length + jobIdArray.Length + triggerSignalArray.Length, changeJobExecArray.Length);
        }

        private void SendData0(List<Comm_Element> elements)
        {
            prefixFrame.Channel = elements[0].Channel;
            AssemblePrefixFrame0();
            Dictionary<int, byte[]> dic = new Dictionary<int, byte[]>();
            int maxOffSet = 0;
            if (elements.Count <= 0)
            {
                return;
            }
            foreach (Comm_Element element in elements)
            {
                object value = element.Value.mValue;
                string typeName = element.Type;
                int resultDataOffset = element.ByteOffset + startByte_dataOutputs;
                if (resultDataOffset > maxOffSet)
                {
                    maxOffSet = resultDataOffset;
                }
                byte[] resultData = typeName switch
                {
                    "Boolean" => (!(bool)value) ? new byte[1] : new byte[1] { 1 },
                    "Byte" => new byte[1] { (byte)Convert.ToInt32(value) },
                    "Char" => BitConverter.GetBytes((char)value),
                    "UInt16" => BitConverter.GetBytes((ushort)value),
                    "UInt32" => BitConverter.GetBytes((uint)value),
                    "UInt64" => BitConverter.GetBytes((ulong)value),
                    "Int16" => BitConverter.GetBytes((short)value),
                    "Int32" => BitConverter.GetBytes((int)value),
                    "Int64" => BitConverter.GetBytes((long)value),
                    "Single" => BitConverter.GetBytes((float)value),
                    "Double" => BitConverter.GetBytes((double)value),
                    "String" => Encoding.ASCII.GetBytes((string)value),
                    _ => new byte[1],
                };
                if (!dic.ContainsKey(resultDataOffset))
                {
                    dic.Add(resultDataOffset, resultData);
                }
                else
                {
                    dic[resultDataOffset] = resultData;
                }
            }
            int len = fixedPrefixFrameArray.Length + maxOffSet + dic[maxOffSet].Length;
            byte[] startArray = new byte[len];
            Array.Copy(fixedPrefixFrameArray, 0, startArray, 0, fixedPrefixFrameArray.Length);
            foreach (KeyValuePair<int, byte[]> item in dic)
            {
                Array.Copy(item.Value, 0, startArray, item.Key + fixedPrefixFrameArray.Length, item.Value.Length);
            }
            Send(startArray);
        }

        public void SendData1(List<Comm_Element> elements)
        {
            prefixFrame.Channel = elements[0].Channel;
            AssemblePrefixFrame1();
            Dictionary<int, byte[]> dic = new Dictionary<int, byte[]>();
            int maxOffSet = 0;
            if (elements.Count <= 0)
            {
                return;
            }
            foreach (Comm_Element element in elements)
            {
                object value = element.Value.mValue;
                string typeName = element.Type;
                int resultDataOffset = element.ByteOffset + startByte_dataOutputs;
                if (resultDataOffset > maxOffSet)
                {
                    maxOffSet = resultDataOffset;
                }
                byte[] resultData = typeName switch
                {
                    "Boolean" => (!(bool)value) ? new byte[1] : new byte[1] { 1 },
                    "Byte" => new byte[1] { (byte)Convert.ToInt32(value) },
                    "Char" => BitConverter.GetBytes((char)value),
                    "UInt16" => BitConverter.GetBytes((ushort)value),
                    "UInt32" => BitConverter.GetBytes((uint)value),
                    "UInt64" => BitConverter.GetBytes((ulong)value),
                    "Int16" => BitConverter.GetBytes((short)value),
                    "Int32" => BitConverter.GetBytes((int)value),
                    "Int64" => BitConverter.GetBytes((long)value),
                    "Single" => BitConverter.GetBytes((float)value),
                    "Double" => BitConverter.GetBytes((double)value),
                    "String" => Encoding.ASCII.GetBytes((string)value),
                    _ => new byte[1],
                };
                if (!dic.ContainsKey(resultDataOffset))
                {
                    dic.Add(resultDataOffset, resultData);
                }
                else
                {
                    dic[resultDataOffset] = resultData;
                }
            }
            int len = fixedPrefixFrameArray.Length + maxOffSet + dic[maxOffSet].Length;
            byte[] startArray = new byte[len];
            Array.Copy(fixedPrefixFrameArray, 0, startArray, 0, fixedPrefixFrameArray.Length);
            foreach (KeyValuePair<int, byte[]> item in dic)
            {
                Array.Copy(item.Value, 0, startArray, item.Key + fixedPrefixFrameArray.Length, item.Value.Length);
            }
            Send(startArray);
        }

        public void SendData2(List<Comm_Element> elements)
        {
            prefixFrame.Channel = elements[0].Channel;
            AssemblePrefixFrame0();
            Dictionary<int, byte[]> dic = new Dictionary<int, byte[]>();
            int maxOffSet = 0;
            if (elements.Count <= 0)
            {
                return;
            }
            foreach (Comm_Element element in elements)
            {
                object value = element.Value.mValue;
                string typeName = element.Type;
                int resultDataOffset = element.ByteOffset + startByte_dataOutputs;
                if (resultDataOffset > maxOffSet)
                {
                    maxOffSet = resultDataOffset;
                }
                byte[] resultData = typeName switch
                {
                    "Boolean" => (!(bool)value) ? new byte[1] : new byte[1] { 1 },
                    "Byte" => new byte[1] { (byte)Convert.ToInt32(value) },
                    "Char" => BitConverter.GetBytes((char)value),
                    "UInt16" => BitConverter.GetBytes((ushort)value),
                    "UInt32" => BitConverter.GetBytes((uint)value),
                    "UInt64" => BitConverter.GetBytes((ulong)value),
                    "Int16" => BitConverter.GetBytes((short)value),
                    "Int32" => BitConverter.GetBytes((int)value),
                    "Int64" => BitConverter.GetBytes((long)value),
                    "Single" => BitConverter.GetBytes((float)value),
                    "Double" => BitConverter.GetBytes((double)value),
                    "String" => Encoding.ASCII.GetBytes((string)value),
                    _ => new byte[1],
                };
                if (!dic.ContainsKey(resultDataOffset))
                {
                    dic.Add(resultDataOffset, resultData);
                }
                else
                {
                    dic[resultDataOffset] = resultData;
                }
            }
            int len = fixedPrefixFrameArray.Length + maxOffSet + dic[maxOffSet].Length;
            byte[] startArray = new byte[len];
            Array.Copy(fixedPrefixFrameArray, 0, startArray, 0, fixedPrefixFrameArray.Length);
            foreach (KeyValuePair<int, byte[]> item in dic)
            {
                Array.Copy(item.Value, 0, startArray, item.Key + fixedPrefixFrameArray.Length, item.Value.Length);
            }
            Send(startArray);
        }

        public void SendData3(List<Comm_Element> elements)
        {
            Dictionary<int, byte[]> dic = new Dictionary<int, byte[]>();
            int maxOffSet = 0;
            if (elements.Count <= 0)
            {
                return;
            }
            foreach (Comm_Element element in elements)
            {
                object value = element.Value.mValue;
                string typeName = element.Type;
                int length = element.TypeLength;
                int resultDataOffset = element.ByteOffset + startByte_dataOutputs;
                if (resultDataOffset > maxOffSet)
                {
                    maxOffSet = resultDataOffset;
                }
                byte[] resultData;
                switch (typeName)
                {
                    case "Boolean":
                        resultData = ((!(bool)value) ? new byte[1] : new byte[1] { 1 });
                        break;
                    case "SByte":
                        {
                            byte temp = (((sbyte)value >= 0) ? ((byte)(sbyte)value) : ((byte)((sbyte)value + 256)));
                            try
                            {
                                resultData = new byte[1] { temp };
                            }
                            catch (InvalidCastException)
                            {
                                resultData = new byte[1] { temp };
                            }
                            break;
                        }
                    case "Byte":
                        resultData = new byte[1] { (byte)Convert.ToInt32(value) };
                        break;
                    case "Char":
                        resultData = BitConverter.GetBytes((char)value);
                        break;
                    case "UInt16":
                        resultData = BitConverter.GetBytes((ushort)value);
                        break;
                    case "UInt32":
                        resultData = BitConverter.GetBytes((uint)value);
                        break;
                    case "UInt64":
                        resultData = BitConverter.GetBytes((ulong)value);
                        break;
                    case "Int16":
                        resultData = BitConverter.GetBytes((short)value);
                        break;
                    case "Int32":
                        resultData = BitConverter.GetBytes((int)value);
                        break;
                    case "Int64":
                        resultData = BitConverter.GetBytes((long)value);
                        break;
                    case "Single":
                        resultData = BitConverter.GetBytes((float)value);
                        break;
                    case "Double":
                        resultData = BitConverter.GetBytes((double)value);
                        break;
                    case "String":
                        {
                            string tempStr = (string)value;
                            if (tempStr.Length < length)
                            {
                                tempStr = tempStr.PadRight(length);
                            }
                            resultData = Encoding.ASCII.GetBytes(tempStr);
                            break;
                        }
                    default:
                        resultData = new byte[1];
                        break;
                }
                if (!dic.ContainsKey(resultDataOffset))
                {
                    dic.Add(resultDataOffset, resultData);
                }
                else
                {
                    dic[resultDataOffset] = resultData;
                }
            }
            int len = maxOffSet + dic[maxOffSet].Length;
            byte[] startArray = new byte[len + 2];
            byte[] endArray = Encoding.ASCII.GetBytes("\r\n");
            foreach (KeyValuePair<int, byte[]> item in dic)
            {
                Array.Copy(item.Value, 0, startArray, item.Key, item.Value.Length);
            }
            Array.Copy(endArray, 0, startArray, len, 2);
            Send(startArray);
        }

        public void SendCommElements(List<Comm_Element> elements, string remoteIp = null)
        {
            if (!ProtocolFlag)
            {
                SendData3(elements);
            }
            else
            {
                WriteBytes(elements);
            }
        }

        public void SystemOffLine()
        {
        }

        public void SystemOnLine()
        {
        }

        public byte[] ReadBytes(int start, int length)
        {
            lock (ProtocolLock)
            {
                AskData(start, length);
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                while (stopwatch.ElapsedMilliseconds < ProtocolTimeOut)
                {
                    if (AskValue.ContainsKey(start) && AskValue[start] != null)
                    {
                        int Asklength = AskValue[start].Length;
                        byte[] value = new byte[Asklength];
                        Array.Copy(AskValue[start], value, Asklength);
                        AskValue.Remove(start);
                        return value;
                    }
                }
                stopwatch.Stop();
                return null;
            }
        }

        public void WriteBytes(List<Comm_Element> elements)
        {
            for (int i = 17; i < 25; i++)
            {
                AckDataBytes[i] = 0;
            }
            Dictionary<int, byte[]> dic = new Dictionary<int, byte[]>();
            int maxOffSet = 0;
            List<int> Channellist = new List<int>();
            if (elements.Count <= 0)
            {
                return;
            }
            foreach (Comm_Element element in elements)
            {
                object value = element.Value.mValue;
                string typeName = element.Type;
                int length = element.TypeLength;
                int resultDataOffset = element.ByteOffset + startByte_dataOutputs;
                if (resultDataOffset > maxOffSet)
                {
                    maxOffSet = resultDataOffset;
                }
                if (!Channellist.Contains(element.Channel))
                {
                    Channellist.Add(element.Channel);
                }
                byte[] resultData;
                switch (typeName)
                {
                    case "Boolean":
                        resultData = ((!(bool)value) ? new byte[1] : new byte[1] { 1 });
                        break;
                    case "SByte":
                        {
                            byte temp = (((sbyte)value >= 0) ? ((byte)(sbyte)value) : ((byte)((sbyte)value + 256)));
                            try
                            {
                                resultData = new byte[1] { temp };
                            }
                            catch (InvalidCastException)
                            {
                                resultData = new byte[1] { temp };
                            }
                            break;
                        }
                    case "Byte":
                        resultData = new byte[1] { (byte)Convert.ToInt32(value) };
                        break;
                    case "Char":
                        resultData = BitConverter.GetBytes((char)value);
                        break;
                    case "UInt16":
                        resultData = BitConverter.GetBytes((ushort)value);
                        break;
                    case "UInt32":
                        resultData = BitConverter.GetBytes((uint)value);
                        break;
                    case "UInt64":
                        resultData = BitConverter.GetBytes((ulong)value);
                        break;
                    case "Int16":
                        resultData = BitConverter.GetBytes((short)value);
                        break;
                    case "Int32":
                        resultData = BitConverter.GetBytes((int)value);
                        break;
                    case "Int64":
                        resultData = BitConverter.GetBytes((long)value);
                        break;
                    case "Single":
                        resultData = BitConverter.GetBytes((float)value);
                        break;
                    case "Double":
                        resultData = BitConverter.GetBytes((double)value);
                        break;
                    case "String":
                        {
                            string tempStr = (string)value;
                            if (tempStr.Length < length)
                            {
                                tempStr = tempStr.PadRight(length);
                            }
                            resultData = Encoding.ASCII.GetBytes(tempStr);
                            break;
                        }
                    default:
                        resultData = new byte[1];
                        break;
                }
                if (!dic.ContainsKey(resultDataOffset))
                {
                    dic.Add(resultDataOffset, resultData);
                }
                else
                {
                    dic[resultDataOffset] = resultData;
                }
            }
            int len = maxOffSet + dic[maxOffSet].Length;
            byte[] startArray = new byte[len + 2];
            byte[] endArray = Encoding.ASCII.GetBytes("\r\n");
            foreach (KeyValuePair<int, byte[]> item2 in dic)
            {
                Array.Copy(item2.Value, 0, startArray, item2.Key, item2.Value.Length);
            }
            Array.Copy(endArray, 0, startArray, len, 2);
            byte[] dataTopic = new byte[1] { 82 };
            List<byte[]> byteArrays = new List<byte[]> { dataTopic, startArray };
            byte[] data = byteArrays.SelectMany((byte[] a) => a).ToArray();
            Send(data);
            if (Channellist.Max() >= 8 || Channellist.Min() < 0)
            {
                return;
            }
            Thread.Sleep(100);
            foreach (int item in Channellist)
            {
                AckDataBytes[17 + item] = 1;
            }
        }

        private void AskData(int start, int length)
        {
            byte[] dataTopic = new byte[1] { 80 };
            byte[] dataStart = BitConverter.GetBytes(start);
            byte[] dataStartL = BitConverter.GetBytes(dataStart.Length);
            byte[] dataLength = BitConverter.GetBytes(length);
            byte[] dataLengthL = BitConverter.GetBytes(dataLength.Length);
            List<byte[]> byteArrays = new List<byte[]> { dataTopic, dataStartL, dataStart, dataLengthL, dataLength, AckDataBytes };
            byte[] data = byteArrays.SelectMany((byte[] a) => a).ToArray();
            if (AskValue.ContainsKey(start))
            {
                AskValue.Remove(start);
            }
            AskValue.Add(start, null);
            Send(data);
        }

        private void GetAsk(byte[] byteArray)
        {
            if (byteArray.Length <= 8 || byteArray[0] != 81)
            {
                return;
            }
            int part2Length = BitConverter.ToInt32(byteArray, 1);
            if (byteArray.Length < 4 + part2Length)
            {
                return;
            }
            byte[] part2Bytes = new byte[part2Length];
            Array.Copy(byteArray, 5, part2Bytes, 0, part2Length);
            int part2 = BitConverter.ToInt32(part2Bytes, 0);
            int part3Length = BitConverter.ToInt32(byteArray, 5 + part2Length);
            if (byteArray.Length >= 9 + part2Length + part3Length)
            {
                byte[] part3Bytes = new byte[part3Length];
                Array.Copy(byteArray, 9 + part2Length, part3Bytes, 0, part3Length);
                if (AskValue.ContainsKey(part2))
                {
                    AskValue[part2] = part3Bytes;
                }
                else
                {
                    AskValue.Add(part2, part3Bytes);
                }
            }
        }

        public void IniProtocolTcp()
        {
            if (ProtocolFlag)
            {
                bvTcpFlag = false;
                Thread.Sleep(10);
                RemoveEvent();
                RemoveValueChangeEvents();
                AddValueChangeEvents();
                RegisterEvent();
                BvTcpStart(800);
            }
        }

        public void BvTcpStart(int length)
        {
            bvTcpFlag = true;
            Task.Factory.StartNew(delegate
            {
                while (bvTcpFlag)
                {
                    try
                    {
                        ScanSignalData(length);
                        Thread.Sleep(10);
                    }
                    catch
                    {
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }

        private void ScanSignalData(int length)
        {
            ScanDataBytes = ReadBytes(Start, length);
            if (ScanDataBytes != null)
            {
                controlStatusBlock.TriggerEnable.Value = (byte)((ScanDataBytes.Count() > 0) ? ScanDataBytes[0] : 0);
                controlStatusBlock.SetOffline.Value = (byte)((ScanDataBytes.Count() > 1) ? ScanDataBytes[1] : 0);
                controlStatusBlock.InitialJobLoad.Value = (byte)((ScanDataBytes.Count() > 2) ? ScanDataBytes[2] : 0);
                for (int i = 3; i < 19; i++)
                {
                    controlStatusBlock[i].Value = (byte)((ScanDataBytes.Count() > controlStatusBlock[i].ByteOffset) ? ScanDataBytes[controlStatusBlock[i].ByteOffset] : 0);
                }
            }
        }

        public void AddValueChangeEvents()
        {
            controlStatusBlock = new TcpBlockElementsCollection();
            for (int i = 0; i < 19; i++)
            {
                controlStatusBlock[i].valueChange += Tcp_ValueChanged;
            }
        }

        public void RemoveValueChangeEvents()
        {
            if (controlStatusBlock == null)
            {
                return;
            }
            for (int i = 0; i < 19; i++)
            {
                if (controlStatusBlock[i] != null)
                {
                    controlStatusBlock[i].valueChange -= Tcp_ValueChanged;
                }
            }
        }

        private void Tcp_ValueChanged(object sender, ValueChangeEventArgs e)
        {
            if (controlStatusBlock[e.Index].Value == 1)
            {
                switch (e.Index)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        if (this.InitialJobLoadEvent != null)
                        {
                            int currentId = -1;
                            currentId = ((ScanDataBytes[3] & 0xFF) << 8) | (ScanDataBytes[4] & 0xFF);
                            this.InitialJobLoadEvent(this, new TcpJobChangeRequestedEventArgs(currentId));
                            AckDataBytes[e.Index - 3 + 1] = 1;
                        }
                        break;
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        if (this.TriggerAcq != null && controlStatusBlock.TriggerEnable.Value == 1 && controlStatusBlock.SetOffline.Value == 1 && controlStatusBlock[e.Index].Value == 1)
                        {
                            this.TriggerAcq(this, new TcpEventArgs
                            {
                                Channel = e.Index - 3
                            });
                            AckDataBytes[e.Index - 3 + 1] = 1;
                        }
                        break;
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                    case 17:
                    case 18:
                        if (this.SetUserDataEvent != null)
                        {
                            this.SetUserDataEvent(this, new TcpEventArgs
                            {
                                Channel = e.Index - 11
                            });
                            AckDataBytes[e.Index - 3 + 1] = 1;
                        }
                        break;
                }
            }
            else
            {
                switch (e.Index)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        AckDataBytes[e.Index - 3 + 1] = 0;
                        break;
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        AckDataBytes[e.Index - 3 + 1] = 0;
                        break;
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                    case 17:
                    case 18:
                        AckDataBytes[e.Index - 3 + 1] = 0;
                        break;
                }
            }
        }

        public void RegisterEvent()
        {
            TriggerAcq += slave_TriggerAcquisition;
            SetUserDataEvent += slave_NewUserData;
            InitialJobLoadEvent += slave_InitialJobLoad;
        }

        public void RemoveEvent()
        {
            if (this.TriggerAcq != null)
            {
                TriggerAcq -= slave_TriggerAcquisition;
            }
            if (this.SetUserDataEvent != null)
            {
                SetUserDataEvent -= slave_NewUserData;
            }
            if (this.InitialJobLoadEvent != null)
            {
                InitialJobLoadEvent -= slave_InitialJobLoad;
            }
        }

        private void slave_TriggerAcquisition(object sender, TcpEventArgs e)
        {
            LogUtil.Log($"TcpSlave({SerialNum})通道{e.Channel}收到触发信号");
            if (this.Trigger != null)
            {
                //this.Trigger(this, e.Channel);
            }
        }

        private void slave_NewUserData(object sender, TcpEventArgs e)
        {
            LogUtil.Log($"TcpSlave({SerialNum})通道{e.Channel}收到数据");
            if (_mCommData != null)
            {
                foreach (string key in inputKeys)
                {
                    if (_mCommData.Outputs[key].Channel == e.Channel)
                    {
                        int offset = _mCommData.Outputs[key].ByteOffset + startByte_dataInputs;
                        int size = _mCommData.Outputs[key].TypeLength;
                        string typeName = _mCommData.Outputs[key].Type;
                        byte[] data = new byte[size];
                        if (ScanDataBytes.Length < 21 + offset + size)
                        {
                            break;
                        }
                        Array.Copy(ScanDataBytes, 21 + offset, data, 0, size);
                        if (data.Length == size && inputKeys.Contains(key))
                        {
                            ElementBase tempObj = new ElementBase();
                            switch (typeName)
                            {
                                case "Boolean":
                                    tempObj.Value.mValue = (data[0] & 1) == 1;
                                    break;
                                case "SByte":
                                    if (data[0] > 127)
                                    {
                                        tempObj.Value.mValue = (sbyte)(data[0] - 256);
                                    }
                                    else
                                    {
                                        tempObj.Value.mValue = (sbyte)data[0];
                                    }
                                    break;
                                case "Byte":
                                    tempObj.Value.mValue = data[0];
                                    break;
                                case "Char":
                                    tempObj.Value.mValue = (char)data[0];
                                    break;
                                case "UInt16":
                                    if (_endian == EndianEnum.BigEndian)
                                    {
                                        DataFormat.BytesExchange(ref data);
                                    }
                                    tempObj.Value.mValue = BitConverter.ToUInt16(data, 0);
                                    break;
                                case "UInt32":
                                    if (_endian == EndianEnum.BigEndian)
                                    {
                                        DataFormat.BytesExchange(ref data);
                                    }
                                    tempObj.Value.mValue = BitConverter.ToUInt32(data, 0);
                                    break;
                                case "UInt64":
                                    if (_endian == EndianEnum.BigEndian)
                                    {
                                        DataFormat.BytesExchange(ref data);
                                    }
                                    tempObj.Value.mValue = BitConverter.ToUInt64(data, 0);
                                    break;
                                case "Int16":
                                    if (_endian == EndianEnum.BigEndian)
                                    {
                                        DataFormat.BytesExchange(ref data);
                                    }
                                    tempObj.Value.mValue = BitConverter.ToInt16(data, 0);
                                    break;
                                case "Int32":
                                    if (_endian == EndianEnum.BigEndian)
                                    {
                                        DataFormat.BytesExchange(ref data);
                                    }
                                    tempObj.Value.mValue = BitConverter.ToInt32(data, 0);
                                    break;
                                case "Int64":
                                    if (_endian == EndianEnum.BigEndian)
                                    {
                                        DataFormat.BytesExchange(ref data);
                                    }
                                    tempObj.Value.mValue = BitConverter.ToInt64(data, 0);
                                    break;
                                case "Single":
                                    if (_endian == EndianEnum.BigEndian)
                                    {
                                        DataFormat.BytesExchange(ref data);
                                    }
                                    tempObj.Value.mValue = BitConverter.ToSingle(data, 0);
                                    break;
                                case "Double":
                                    if (_endian == EndianEnum.BigEndian)
                                    {
                                        DataFormat.BytesExchange(ref data);
                                    }
                                    tempObj.Value.mValue = BitConverter.ToDouble(data, 0);
                                    break;
                                case "String":
                                    tempObj.Value.mValue = Encoding.ASCII.GetString(data).Trim(default(char));
                                    break;
                            }
                            _mCommData.Outputs[key].Value = tempObj.Value;
                            LogUtil.Log("点位" + key + ", 收到数据: " + _mCommData.Outputs[key].Value.ToString());
                        }
                    }
                }
                return;
            }
            LogUtil.LogError("通讯表为空!");
        }

        private void slave_InitialJobLoad(object sender, TcpJobChangeRequestedEventArgs e)
        {
            if (this.JobChanged != null)
            {
                this.JobChanged(e.JobId);
            }
        }
    }
}
