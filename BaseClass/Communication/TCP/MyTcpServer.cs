using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NovaVision.BaseClass.Collection;
using NovaVision.BaseClass.Communication.CommData;
using NovaVision.BaseClass.Module;

namespace NovaVision.BaseClass.Communication.TCP;

public class MyTcpServer : IFlowState
{
    public delegate void ValueChangedEventHandler(object sender, ValueChangeEventArgs e);

    // 定义接收到客户端消息时的委托类型，传递客户端套接字和消息内容
    public delegate void SocketMessage(Socket clientSocket, string str);

    public class ValueChangeEventArgs : EventArgs
    {
        public byte oldValue { get; set; }

        public byte newValue { get; set; }

        public int Index { get; set; }

        public string Description { get; set; }
    }

    private int _maxClient;

    private string _serialNum;

    private EndianEnum _endian;

    private Socket _serverSock;

    private int _clientCount;

    private int _bufferSize = 4096;

    private Semaphore _maxAcceptedClients;

    private AsyncUserTokenPool _userTokenPool;

    public Dictionary<string, AsyncUserToken> D_userToken = new Dictionary<string, AsyncUserToken>();

    private Dictionary<string, ClientInfo> Dic_ClientInfo;

    public static Dictionary<string, MyTcpServer> Dic_server = new Dictionary<string, MyTcpServer>();

    private bool disposed = false;

    public TcpMode mode;

    public FixedPrefixFrame prefixFrame;

    public byte[] fixedPrefixFrameArray;

    private InputsOutputs<Comm_Element, Communication.CommData.Info> _mCommData;

    public int startByte_dataInputs;

    public int length_dataInputs;

    public int startByte_dataOutputs;

    public int length_dataOutputs;

    private Dictionary<string, byte[]> inputByteDic;

    private Dictionary<string, string> inputTypeDic;

    private List<string>[] triggerKey;

    private List<string> inputKeys1;

    public List<string> outputKeys;

    public List<string> inputKeys;

    private Dictionary<string, List<string>> dataBindingsDic;

    public MyDictionaryEx<Comm_Element> dataInputs;

    public MyDictionaryEx<Comm_Element> dataOutputs;

    public string _localIp = "";

    public int _localPort = 1000;

    public string _hbstr = "HB";

    public bool _hbflag;

    private AutoResetEvent autoReset = new AutoResetEvent(initialState: true);

    private object lockObj = new object();

    private bool ProtocolFlag = false;

    public static int CilentStart = 1;


    private EventWaitHandle[] handleSetuserdataAck = new EventWaitHandle[8];

    private EventWaitHandle[] handleTriggercameraAck = new EventWaitHandle[8];

    public int HBTimeout { get; set; } = 15;


    public bool IsRunning { get; private set; }

    public IPAddress Address { get; private set; }

    public int Port { get; private set; }

    public Encoding Encoding { get; set; }

    public int BufferSize
    {
        get { return _bufferSize; }
        set { _bufferSize = value; }
    }

    public string SerialNum => _serialNum;

    public bool IsConnected => IsRunning;

    public string CommTypeName => "TcpServer";

    public EndianEnum Endian
    {
        get { return _endian; }
        set { _endian = value; }
    }

    public event Action<string> ReceiveMsgEventHandler;

    public event Action<int> JobChanged;

    public event ConnectedEventHandler CommConnected;

    public event TriggerEventHandler Trigger;

    public void SetTcpMode(TcpMode _mode)
    {
        mode = _mode;
    }

    public static void CreateNewServer(string localIp, int listenPort, int maxClient, string sn, string HBStr,
        bool HBFlag)
    {
        string[] localIpArray = localIp.Split('.');
        byte[] address = new byte[4];
        for (int i = 0; i < 4; i++)
        {
            address[i] = BitConverter.GetBytes(Convert.ToInt16(localIpArray[i]))[0];
        }

        IPAddress localIPAddress = new IPAddress(address);
        CreateNewServer(localIPAddress, listenPort, maxClient, sn, HBStr, HBFlag);
    }

    public static void CreateNewServer(IPAddress localIPAddress, int listenPort, int maxClient, string sn,
        string HBStr, bool HBFlag)
    {
        if (Dic_server.ContainsKey(sn))
        {
            if (!Dic_server[sn].Address.ToString().Equals(localIPAddress.ToString()) ||
                Dic_server[sn]._localPort != listenPort || Dic_server[sn]._maxClient != maxClient)
            {
                new MyTcpServer(localIPAddress, listenPort, maxClient, sn, HBStr, HBFlag);
            }
        }
        else
        {
            new MyTcpServer(localIPAddress, listenPort, maxClient, sn, HBStr, HBFlag);
        }
    }

    public static MyTcpServer GetServerInstance(string sn)
    {
        if (Dic_server.ContainsKey(sn))
        {
            return Dic_server[sn];
        }

        return null;
    }

    public MyTcpServer(int listenPort, int maxClient, string sn, string HBStr, bool HBFlag)
        : this(IPAddress.Any, listenPort, maxClient, sn, HBStr, HBFlag)
    {
    }

    public MyTcpServer(IPEndPoint localEP, int maxClient, string sn, string HBStr, bool HBFlag)
        : this(localEP.Address, localEP.Port, maxClient, sn, HBStr, HBFlag)
    {
    }

    private MyTcpServer(IPAddress localIPAddress, int listenPort, int maxClient, string sn, string HBStr,
        bool HBFlag)
    {
        Address = localIPAddress;
        Port = listenPort;
        Encoding = Encoding.Default;
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
        _serialNum = sn;
        _maxClient = maxClient;
        _endian = EndianEnum.LittleEndian;
        _serverSock = new Socket(localIPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        _userTokenPool = new AsyncUserTokenPool(_maxClient);
        _maxAcceptedClients = new Semaphore(_maxClient, _maxClient);
        byte[] tempBytes = localIPAddress.GetAddressBytes();
        _localIp = $"{tempBytes[0]}.{tempBytes[1]}.{tempBytes[2]}.{tempBytes[3]}";
        _localPort = listenPort;
        if (Dic_server.ContainsKey(_serialNum))
        {
            Dic_server[_serialNum] = this;
        }
        else
        {
            Dic_server.Add(_serialNum, this);
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

    public void Init()
    {
        D_userToken.Clear();
        for (int i = 0; i < _maxClient; i++)
        {
            AsyncUserToken userToken = new AsyncUserToken(_bufferSize);
            userToken.ReceiveEventArgs.Completed += OnIOCompleted;
            userToken.SendEventArgs.Completed += OnIOCompleted;
            _userTokenPool.Push(userToken);
        }

        if (ProtocolFlag)
        {
            RegisterEvent();
        }
    }

    public void Start()
    {
        try
        {
            if (!IsRunning)
            {
                Init();
                IsRunning = true;
                IPEndPoint localEndPoint = new IPEndPoint(Address, Port);
                _serverSock = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                if (localEndPoint.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    _serverSock.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only,
                        optionValue: false);
                    _serverSock.Bind(new IPEndPoint(IPAddress.IPv6Any, localEndPoint.Port));
                }
                else
                {
                    _serverSock.Bind(localEndPoint);
                }

                _serverSock.Listen(_maxClient);
                if (this.CommConnected != null)
                {
                    this.CommConnected(this, IsRunning);
                }

                StartAccept(null);
                Dic_ClientInfo = new Dictionary<string, ClientInfo>();
            }
        }
        catch (Exception ex)
        {
            IsRunning = false;
            if (this.CommConnected != null)
            {
                this.CommConnected(this, isConnected: false);
            }

            LogUtil.LogError("服务器（" + _serialNum + "）开启异常，异常信息：" + ex.Message);
        }
    }

    public void Stop()
    {
        try
        {
            if (IsRunning)
            {
                IsRunning = false;
                _serverSock.Close();
                Dic_server.Remove(_serialNum);
            }
        }
        catch (Exception ex)
        {
            LogUtil.LogError("服务器（" + _serialNum + "）关闭异常，异常信息：" + ex.Message);
        }
    }

    private void StartAccept(SocketAsyncEventArgs asyniar)
    {
        if (asyniar == null)
        {
            asyniar = new SocketAsyncEventArgs();
            asyniar.Completed += OnAcceptCompleted;
        }
        else
        {
            asyniar.AcceptSocket = null;
        }

        _maxAcceptedClients.WaitOne();
        if (!_serverSock.AcceptAsync(asyniar))
        {
            ProcessAccept(asyniar);
        }
    }

    private void OnAcceptCompleted(object sender, SocketAsyncEventArgs e)
    {
        ProcessAccept(e);
    }

    private void ProcessAccept(SocketAsyncEventArgs e)
    {
        if (e.SocketError != 0)
        {
            return;
        }

        Socket sock = e.AcceptSocket;
        if (!sock.Connected)
        {
            return;
        }

        try
        {
            if (Dic_ClientInfo == null)
            {
                Dic_ClientInfo = new Dictionary<string, ClientInfo>();
            }

            Interlocked.Increment(ref _clientCount);
            AsyncUserToken userToken = _userTokenPool.Pop();
            string remoteIP = sock.RemoteEndPoint.ToString().Split(':')[0];
            userToken.ConnectSocket = sock;
            if (!D_userToken.ContainsKey(remoteIP))
            {
                D_userToken.Add(remoteIP, userToken);
            }

            lock (lockObj)
            {
                if (Dic_ClientInfo.ContainsKey(remoteIP))
                {
                    Dic_ClientInfo[remoteIP] = new ClientInfo
                    {
                        ClientIp = remoteIP,
                        LastHeartbeatTime = DateTime.Now,
                        State = true
                    };
                }
                else
                {
                    ClientInfo ci = new ClientInfo
                    {
                        ClientIp = remoteIP,
                        LastHeartbeatTime = DateTime.Now,
                        State = true
                    };
                    Dic_ClientInfo.Add(remoteIP, ci);
                }
            }

            LogUtil.Log($"客户 {sock.RemoteEndPoint.ToString()} 连入, 共有 {_clientCount} 个连接。");
            if (this.ReceiveMsgEventHandler != null)
            {
                this.ReceiveMsgEventHandler.BeginInvoke(
                    $"{DateTime.Now.ToLocalTime()}:客户 {sock.RemoteEndPoint.ToString()} 连入, 共有 {_clientCount} 个连接。",
                    null, null);
            }

            if (!sock.ReceiveAsync(userToken.ReceiveEventArgs))
            {
                ProcessReceive(userToken.ReceiveEventArgs);
            }
        }
        catch (SocketException ex)
        {
            LogUtil.LogError($"接收客户 {sock.RemoteEndPoint} 数据出错, 异常信息： {ex.ToString()} 。");
        }

        StartAccept(e);
    }

    public void Send(string remoteIp, byte[] resultData, int? offSet, int channel)
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

        Send(remoteIp, startByteArray);
    }

    public void Send(SocketAsyncEventArgs e, byte[] data)
    {
        AsyncUserToken userToken = e.UserToken as AsyncUserToken;
        if (userToken != null)
        {
            userToken.SendBuffer.WriteBuffer(data, 0, data.Length);
            if (userToken.SendEventArgs.SocketError == SocketError.Success)
            {
                if (userToken.ConnectSocket.Connected)
                {
                    Array.Copy(data, 0, e.Buffer, 0, data.Length);
                    if (!userToken.ConnectSocket.SendAsync(userToken.SendEventArgs))
                    {
                        ProcessSend(userToken.SendEventArgs);
                    }

                    userToken.SendBuffer.Clear();
                }
                else
                {
                    CloseClientSocket(userToken);
                }
            }
            else
            {
                CloseClientSocket(userToken);
            }
        }
    }

    public void Send(string remoteIp, byte[] data)
    {
        autoReset.WaitOne(2000);
        if (D_userToken.ContainsKey(remoteIp))
        {
            D_userToken[remoteIp].ClearAsyncSendBuffer();
            D_userToken[remoteIp].SendEventArgs.SetBuffer(D_userToken[remoteIp].AsyncSendBuffer, 0, data.Length);
            Send(D_userToken[remoteIp].SendEventArgs, data);
            return;
        }

        throw new Exception("指定客户端Ip未连接");
    }

    public void Send(string remoteIp, string dataStr)
    {
        byte[] data = Encoding.ASCII.GetBytes(dataStr);
        Send(remoteIp, data);
    }

    public void Send(Socket socket, byte[] buffer, int offset, int size, int timeout)
    {
        socket.SendTimeout = 0;
        int startTickCount = Environment.TickCount;
        int sent = 0;
        do
        {
            if (Environment.TickCount > startTickCount + timeout)
            {
            }

            try
            {
                sent += socket.Send(buffer, offset + sent, size - sent, SocketFlags.None);
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.WouldBlock || ex.SocketErrorCode == SocketError.IOPending ||
                    ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                {
                    Thread.Sleep(30);
                    continue;
                }

                throw ex;
            }
        } while (sent < size);
    }

    private void ProcessSend(SocketAsyncEventArgs e)
    {
        autoReset.Set();
        AsyncUserToken userToken = e.UserToken as AsyncUserToken;
        if (userToken.SendEventArgs.SocketError != 0)
        {
            CloseClientSocket(userToken);
        }
    }

    private void ProcessReceive(SocketAsyncEventArgs e)
    {
        AsyncUserToken userToken = e.UserToken as AsyncUserToken;
        if (userToken.ReceiveEventArgs.BytesTransferred > 0 &&
            userToken.ReceiveEventArgs.SocketError == SocketError.Success)
        {
            Socket sock = userToken.ConnectSocket;
            if (sock.Available == 0)
            {
                userToken.ReceiveBuffer.WriteBuffer(e.Buffer, e.Offset, e.BytesTransferred);
                if (!ProtocolFlag)
                {
                    string info = Encoding.ASCII.GetString(e.Buffer, e.Offset, e.BytesTransferred);
                    if (info.Contains(_hbstr + "\r\n"))
                    {
                        string remoteIp = sock.RemoteEndPoint.ToString().Split(':')[0];
                        lock (lockObj)
                        {
                            if (Dic_ClientInfo.ContainsKey(remoteIp))
                            {
                                Dic_ClientInfo[remoteIp].LastHeartbeatTime = DateTime.Now;
                                Dic_ClientInfo[remoteIp].State = true;
                            }
                        }
                    }

                    string info2 = info.Replace(_hbstr + "\r\n", "");
                    if (info2.Length > 0)
                    {
                        LogUtil.Log($">>服务器{Address},端口号为{Port},收到 {sock.RemoteEndPoint.ToString()} 数据为 {info2}");
                        if (this.Trigger != null)
                        {
                            this.Trigger(this, "触发", "1");
                        }
                        byte[] data = new byte[info2.Length];
                        Array.Copy(e.Buffer, info.IndexOf(info2), data, 0, info2.Length);
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
                                        for (int l = 0; l < triggerKey.Length; l++)
                                        {
                                            if (triggerKey[l].Count <= 0)
                                            {
                                                continue;
                                            }

                                            foreach (string key3 in triggerKey[l])
                                            {
                                                int offset3 = _mCommData.Outputs[key3].ByteOffset +
                                                              startByte_dataInputs;
                                                int size3 = _mCommData.Outputs[key3].TypeLength;
                                                string typeName3 = _mCommData.Outputs[key3].Type;
                                                byte[] tempData3 = new byte[size3];
                                                Array.Copy(data, 16 + offset3, tempData3, 0, size3);
                                                if (!inputByteDic.ContainsKey(key3))
                                                {
                                                    inputByteDic.Add(key3, tempData3);
                                                    inputTypeDic.Add(key3, typeName3);
                                                }
                                                else
                                                {
                                                    inputByteDic[key3] = tempData3;
                                                    inputTypeDic[key3] = typeName3;
                                                }
                                            }
                                        }

                                        for (int k = 0; k < acqTrigger.Length; k++)
                                        {
                                            int k3 = k;
                                            if (acqTrigger[k3])
                                            {
                                                Task.Run(delegate { TriggerAcquisition(k3); });
                                            }
                                        }

                                        foreach (string key4 in inputKeys1)
                                        {
                                            if (_mCommData.Outputs[key4].Channel == channel)
                                            {
                                                int offset4 = _mCommData.Outputs[key4].ByteOffset +
                                                              startByte_dataInputs;
                                                int size4 = _mCommData.Outputs[key4].TypeLength;
                                                string typeName4 = _mCommData.Outputs[key4].Type;
                                                byte[] tempData4 = new byte[size4];
                                                Array.Copy(data, 16 + offset4, tempData4, 0, size4);
                                                switch (typeName4)
                                                {
                                                    case "Boolean":
                                                        _mCommData.Outputs[key4].Value.mValue =
                                                            (tempData4[0] & 1) == 1;
                                                        break;
                                                    case "Byte":
                                                        _mCommData.Outputs[key4].Value.mValue = tempData4[0];
                                                        break;
                                                    case "Char":
                                                        _mCommData.Outputs[key4].Value.mValue =
                                                            (char)((uint)((tempData4[0] & 0xFF) << 8) |
                                                                   (tempData4[1] & 0xFFu));
                                                        break;
                                                    case "UInt16":
                                                        _mCommData.Outputs[key4].Value.mValue =
                                                            BitConverter.ToUInt16(tempData4, 0);
                                                        break;
                                                    case "UInt32":
                                                        _mCommData.Outputs[key4].Value.mValue =
                                                            BitConverter.ToUInt32(tempData4, 0);
                                                        break;
                                                    case "UInt64":
                                                        _mCommData.Outputs[key4].Value.mValue =
                                                            BitConverter.ToUInt64(tempData4, 0);
                                                        break;
                                                    case "Int16":
                                                        _mCommData.Outputs[key4].Value.mValue =
                                                            BitConverter.ToInt16(tempData4, 0);
                                                        break;
                                                    case "Int32":
                                                        _mCommData.Outputs[key4].Value.mValue =
                                                            BitConverter.ToInt32(tempData4, 0);
                                                        break;
                                                    case "Int64":
                                                        _mCommData.Outputs[key4].Value.mValue =
                                                            BitConverter.ToInt64(tempData4, 0);
                                                        break;
                                                    case "Single":
                                                        _mCommData.Outputs[key4].Value.mValue =
                                                            BitConverter.ToSingle(tempData4, 0);
                                                        break;
                                                    case "Double":
                                                        _mCommData.Outputs[key4].Value.mValue =
                                                            BitConverter.ToDouble(tempData4, 0);
                                                        break;
                                                    case "String":
                                                        _mCommData.Outputs[key4].Value.mValue =
                                                            Encoding.ASCII.GetString(tempData4);
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
                                        for (int n = 0; n < triggerReady.Length; n++)
                                        {
                                            int k5 = n;
                                            if (triggerReady[k5])
                                            {
                                                Task.Run(delegate { TriggerReady(k5); });
                                            }
                                        }

                                        for (int m = 0; m < acqCompleted.Length; m++)
                                        {
                                            int k4 = m;
                                            if (acqCompleted[k4])
                                            {
                                                Task.Run(delegate { AcquisitionCompleted(k4); });
                                            }
                                        }

                                        foreach (string key5 in inputKeys1)
                                        {
                                            if (_mCommData.Outputs[key5].Channel == channel)
                                            {
                                                int offset5 = _mCommData.Outputs[key5].ByteOffset +
                                                              startByte_dataInputs;
                                                int size5 = _mCommData.Outputs[key5].TypeLength;
                                                string typeName5 = _mCommData.Outputs[key5].Type;
                                                byte[] tempData5 = new byte[size5];
                                                Array.Copy(data, 20 + offset5, tempData5, 0, size5);
                                                switch (typeName5)
                                                {
                                                    case "Boolean":
                                                        _mCommData.Outputs[key5].Value.mValue =
                                                            (tempData5[0] & 1) == 1;
                                                        break;
                                                    case "Byte":
                                                        _mCommData.Outputs[key5].Value.mValue = tempData5[0];
                                                        break;
                                                    case "Char":
                                                        _mCommData.Outputs[key5].Value.mValue =
                                                            (char)((uint)((tempData5[0] & 0xFF) << 8) |
                                                                   (tempData5[1] & 0xFFu));
                                                        break;
                                                    case "UInt16":
                                                        _mCommData.Outputs[key5].Value.mValue =
                                                            BitConverter.ToUInt16(tempData5, 0);
                                                        break;
                                                    case "UInt32":
                                                        _mCommData.Outputs[key5].Value.mValue =
                                                            BitConverter.ToUInt32(tempData5, 0);
                                                        break;
                                                    case "UInt64":
                                                        _mCommData.Outputs[key5].Value.mValue =
                                                            BitConverter.ToUInt64(tempData5, 0);
                                                        break;
                                                    case "Int16":
                                                        _mCommData.Outputs[key5].Value.mValue =
                                                            BitConverter.ToInt16(tempData5, 0);
                                                        break;
                                                    case "Int32":
                                                        _mCommData.Outputs[key5].Value.mValue =
                                                            BitConverter.ToInt32(tempData5, 0);
                                                        break;
                                                    case "Int64":
                                                        _mCommData.Outputs[key5].Value.mValue =
                                                            BitConverter.ToInt64(tempData5, 0);
                                                        break;
                                                    case "Single":
                                                        _mCommData.Outputs[key5].Value.mValue =
                                                            BitConverter.ToSingle(tempData5, 0);
                                                        break;
                                                    case "Double":
                                                        _mCommData.Outputs[key5].Value.mValue =
                                                            BitConverter.ToDouble(tempData5, 0);
                                                        break;
                                                    case "String":
                                                        _mCommData.Outputs[key5].Value.mValue =
                                                            Encoding.ASCII.GetString(tempData5);
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
                                        for (int i3 = 0; i3 < triggerKey.Length; i3++)
                                        {
                                            if (triggerKey[i3].Count <= 0)
                                            {
                                                continue;
                                            }

                                            foreach (string key6 in triggerKey[i3])
                                            {
                                                int offset6 = _mCommData.Outputs[key6].ByteOffset +
                                                              startByte_dataInputs;
                                                int size6 = _mCommData.Outputs[key6].TypeLength;
                                                string typeName6 = _mCommData.Outputs[key6].Type;
                                                byte[] tempData6 = new byte[size6];
                                                Array.Copy(data, 16 + offset6, tempData6, 0, size6);
                                                if (!inputByteDic.ContainsKey(key6))
                                                {
                                                    inputByteDic.Add(key6, tempData6);
                                                    inputTypeDic.Add(key6, typeName6);
                                                }
                                                else
                                                {
                                                    inputByteDic[key6] = tempData6;
                                                    inputTypeDic[key6] = typeName6;
                                                }
                                            }
                                        }

                                        for (int i2 = 0; i2 < acqTrigger.Length; i2++)
                                        {
                                            int k2 = i2;
                                            if (acqTrigger[k2])
                                            {
                                                Task.Run(delegate { TriggerAcquisition(k2); });
                                            }
                                        }

                                        foreach (string key8 in inputKeys1)
                                        {
                                            if (_mCommData.Outputs[key8].Channel == channel)
                                            {
                                                int offset8 = _mCommData.Outputs[key8].ByteOffset +
                                                              startByte_dataInputs;
                                                int size8 = _mCommData.Outputs[key8].TypeLength;
                                                string typeName7 = _mCommData.Outputs[key8].Type;
                                                byte[] tempData7 = new byte[size8];
                                                Array.Copy(data, 16 + offset8, tempData7, 0, size8);
                                                switch (typeName7)
                                                {
                                                    case "Boolean":
                                                        _mCommData.Outputs[key8].Value.mValue =
                                                            (tempData7[0] & 1) == 1;
                                                        break;
                                                    case "Byte":
                                                        _mCommData.Outputs[key8].Value.mValue = tempData7[0];
                                                        break;
                                                    case "Char":
                                                        _mCommData.Outputs[key8].Value.mValue =
                                                            (char)((uint)((tempData7[0] & 0xFF) << 8) |
                                                                   (tempData7[1] & 0xFFu));
                                                        break;
                                                    case "UInt16":
                                                        _mCommData.Outputs[key8].Value.mValue =
                                                            BitConverter.ToUInt16(tempData7, 0);
                                                        break;
                                                    case "UInt32":
                                                        _mCommData.Outputs[key8].Value.mValue =
                                                            BitConverter.ToUInt32(tempData7, 0);
                                                        break;
                                                    case "UInt64":
                                                        _mCommData.Outputs[key8].Value.mValue =
                                                            BitConverter.ToUInt64(tempData7, 0);
                                                        break;
                                                    case "Int16":
                                                        _mCommData.Outputs[key8].Value.mValue =
                                                            BitConverter.ToInt16(tempData7, 0);
                                                        break;
                                                    case "Int32":
                                                        _mCommData.Outputs[key8].Value.mValue =
                                                            BitConverter.ToInt32(tempData7, 0);
                                                        break;
                                                    case "Int64":
                                                        _mCommData.Outputs[key8].Value.mValue =
                                                            BitConverter.ToInt64(tempData7, 0);
                                                        break;
                                                    case "Single":
                                                        _mCommData.Outputs[key8].Value.mValue =
                                                            BitConverter.ToSingle(tempData7, 0);
                                                        break;
                                                    case "Double":
                                                        _mCommData.Outputs[key8].Value.mValue =
                                                            BitConverter.ToDouble(tempData7, 0);
                                                        break;
                                                    case "String":
                                                        _mCommData.Outputs[key8].Value.mValue =
                                                            Encoding.ASCII.GetString(tempData7);
                                                        break;
                                                }
                                            }
                                        }

                                        break;
                                    }
                                    case TcpMode.DIY:
                                    {
                                        foreach (string key7 in inputKeys1)
                                        {
                                            int offset7 = _mCommData.Outputs[key7].ByteOffset +
                                                          startByte_dataInputs;
                                            int size7 = _mCommData.Outputs[key7].TypeLength;
                                            if (size7 > data.Length - offset7)
                                            {
                                                size7 = data.Length - offset7;
                                            }

                                            string typeName8 = _mCommData.Outputs[key7].Type;
                                            byte[] tempData8 = new byte[size7];
                                            Array.Copy(data, offset7, tempData8, 0, size7);
                                            switch (typeName8)
                                            {
                                                case "Boolean":
                                                    _mCommData.Outputs[key7].Value.mValue = (tempData8[0] & 1) == 1;
                                                    break;
                                                case "SByte":
                                                    if (tempData8[0] > 127)
                                                    {
                                                        _mCommData.Outputs[key7].Value.mValue =
                                                            (sbyte)(tempData8[0] - 256);
                                                    }
                                                    else
                                                    {
                                                        _mCommData.Outputs[key7].Value.mValue = (sbyte)tempData8[0];
                                                    }

                                                    break;
                                                case "Byte":
                                                    _mCommData.Outputs[key7].Value.mValue = tempData8[0];
                                                    break;
                                                case "Char":
                                                    _mCommData.Outputs[key7].Value.mValue =
                                                        (char)((uint)((tempData8[0] & 0xFF) << 8) |
                                                               (tempData8[1] & 0xFFu));
                                                    break;
                                                case "UInt16":
                                                    _mCommData.Outputs[key7].Value.mValue =
                                                        BitConverter.ToUInt16(tempData8, 0);
                                                    break;
                                                case "UInt32":
                                                    _mCommData.Outputs[key7].Value.mValue =
                                                        BitConverter.ToUInt32(tempData8, 0);
                                                    break;
                                                case "UInt64":
                                                    _mCommData.Outputs[key7].Value.mValue =
                                                        BitConverter.ToUInt64(tempData8, 0);
                                                    break;
                                                case "Int16":
                                                    _mCommData.Outputs[key7].Value.mValue =
                                                        BitConverter.ToInt16(tempData8, 0);
                                                    break;
                                                case "Int32":
                                                    _mCommData.Outputs[key7].Value.mValue =
                                                        BitConverter.ToInt32(tempData8, 0);
                                                    break;
                                                case "Int64":
                                                    _mCommData.Outputs[key7].Value.mValue =
                                                        BitConverter.ToInt64(tempData8, 0);
                                                    break;
                                                case "Single":
                                                    _mCommData.Outputs[key7].Value.mValue =
                                                        BitConverter.ToSingle(tempData8, 0);
                                                    break;
                                                case "Double":
                                                    _mCommData.Outputs[key7].Value.mValue =
                                                        BitConverter.ToDouble(tempData8, 0);
                                                    break;
                                                case "String":
                                                    _mCommData.Outputs[key7].Value.mValue =
                                                        Encoding.ASCII.GetString(tempData8);
                                                    break;
                                            }
                                        }

                                        for (int i4 = 0; i4 < triggerKey.Length; i4++)
                                        {
                                            if (triggerKey[i4].Count <= 0)
                                            {
                                                continue;
                                            }

                                            foreach (string key9 in triggerKey[i4])
                                            {
                                                int offset9 = _mCommData.Outputs[key9].ByteOffset +
                                                              startByte_dataInputs;
                                                int size9 = _mCommData.Outputs[key9].TypeLength;
                                                if (size9 > data.Length - offset9)
                                                {
                                                    size9 = data.Length - offset9;
                                                }

                                                string typeName9 = _mCommData.Outputs[key9].Type;
                                                byte[] tempData9 = new byte[size9];
                                                Array.Copy(data, offset9, tempData9, 0, size9);
                                                switch (typeName9)
                                                {
                                                    case "Boolean":
                                                        _mCommData.Outputs[key9].Value.mValue =
                                                            (tempData9[0] & 1) == 1;
                                                        break;
                                                    case "Byte":
                                                        _mCommData.Outputs[key9].Value.mValue = tempData9[0];
                                                        break;
                                                    case "Char":
                                                        _mCommData.Outputs[key9].Value.mValue =
                                                            (char)((uint)((tempData9[0] & 0xFF) << 8) |
                                                                   (tempData9[1] & 0xFFu));
                                                        break;
                                                    case "UInt16":
                                                        _mCommData.Outputs[key9].Value.mValue =
                                                            BitConverter.ToUInt16(tempData9, 0);
                                                        break;
                                                    case "UInt32":
                                                        _mCommData.Outputs[key9].Value.mValue =
                                                            BitConverter.ToUInt32(tempData9, 0);
                                                        break;
                                                    case "UInt64":
                                                        _mCommData.Outputs[key9].Value.mValue =
                                                            BitConverter.ToUInt64(tempData9, 0);
                                                        break;
                                                    case "Int16":
                                                        _mCommData.Outputs[key9].Value.mValue =
                                                            BitConverter.ToInt16(tempData9, 0);
                                                        break;
                                                    case "Int32":
                                                        _mCommData.Outputs[key9].Value.mValue =
                                                            BitConverter.ToInt32(tempData9, 0);
                                                        break;
                                                    case "Int64":
                                                        _mCommData.Outputs[key9].Value.mValue =
                                                            BitConverter.ToInt64(tempData9, 0);
                                                        break;
                                                    case "Single":
                                                        _mCommData.Outputs[key9].Value.mValue =
                                                            BitConverter.ToSingle(tempData9, 0);
                                                        break;
                                                    case "Double":
                                                        _mCommData.Outputs[key9].Value.mValue =
                                                            BitConverter.ToDouble(tempData9, 0);
                                                        break;
                                                    case "String":
                                                        _mCommData.Outputs[key9].Value.mValue =
                                                            Encoding.ASCII.GetString(tempData9);
                                                        break;
                                                }
                                            }
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LogUtil.LogError("解析来自" + sock.RemoteEndPoint.ToString() + "数据出现异常，异常信息：" + ex.Message);
                        }
                    }
                }
                else
                {
                }
            }

            if (!sock.ReceiveAsync(userToken.ReceiveEventArgs))
            {
                ProcessReceive(userToken.ReceiveEventArgs);
            }
        }
        else
        {
            CloseClientSocket(userToken);
        }
    }

    private void OnIOCompleted(object sender, SocketAsyncEventArgs e)
    {
        AsyncUserToken userToken = e.UserToken as AsyncUserToken;
        lock (userToken)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Accept:
                    ProcessAccept(e);
                    break;
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend(e);
                    break;
            }
        }
    }

    private void ScanClientHB()
    {
        if (Dic_ClientInfo == null)
        {
            return;
        }

        while (IsRunning)
        {
            Thread.Sleep(1000);
            lock (lockObj)
            {
                foreach (string key in Dic_ClientInfo.Keys)
                {
                    if (!Dic_ClientInfo[key].State ||
                        (DateTime.Now - Dic_ClientInfo[key].LastHeartbeatTime).Seconds <= HBTimeout)
                    {
                        continue;
                    }

                    try
                    {
                        D_userToken.Remove(key);
                        Dic_ClientInfo[key].State = false;
                    }
                    catch
                    {
                    }
                    finally
                    {
                        LogUtil.LogError("客户端:" + key + "已掉线！！！");
                    }
                }
            }
        }
    }

    private void CloseClientSocket(AsyncUserToken userToken)
    {
        if (userToken.ConnectSocket == null)
        {
            return;
        }

        if (this.ReceiveMsgEventHandler != null)
        {
            this.ReceiveMsgEventHandler.BeginInvoke(
                $"{DateTime.Now.ToLocalTime()}:客户 {userToken.ConnectSocket.RemoteEndPoint} 断开连接!", null,
                null);
        }

        try
        {
            userToken.ConnectSocket.Shutdown(SocketShutdown.Send);
            string remoteIP = userToken.ConnectSocket.RemoteEndPoint.ToString().Split(':')[0];
            if (Dic_ClientInfo.ContainsKey(remoteIP))
            {
                Dic_ClientInfo.Remove(remoteIP);
            }
        }
        catch (Exception)
        {
        }
        finally
        {
            userToken.ConnectSocket.Close();
            foreach (KeyValuePair<string, AsyncUserToken> item in D_userToken)
            {
                if (item.Value == userToken)
                {
                    D_userToken.Remove(item.Key);
                    break;
                }
            }
        }

        Interlocked.Decrement(ref _clientCount);
        userToken.ConnectSocket = null;
        _maxAcceptedClients.Release();
        _userTokenPool.Push(userToken);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
        {
            return;
        }

        if (disposing)
        {
            try
            {
                Stop();
                if (_serverSock != null)
                {
                    _serverSock = null;
                }
            }
            catch (SocketException)
            {
            }
        }

        disposed = true;
    }

    public void AcqCompeleted(Comm_Element comm_Element)
    {
        if (D_userToken.Count > 0)
        {
            string remoteIp = D_userToken.Keys.ToList()[0];
            Send(remoteIp, null, null, comm_Element.Channel);
        }
    }

    public void InspectCompeleted(List<Comm_Element> comm_Element)
    {
        if (D_userToken.Count <= 0)
        {
            return;
        }

        string remoteIp = D_userToken.Keys.ToList()[0];
        if (!ProtocolFlag)
        {
            switch (mode)
            {
                case TcpMode.Software2Plc:
                    SendData0(remoteIp, comm_Element);
                    break;
                case TcpMode.MasterSoft2Soft:
                    SendData1(remoteIp, comm_Element);
                    break;
                case TcpMode.ControledSoft2Soft:
                    SendData2(remoteIp, comm_Element);
                    break;
                case TcpMode.DIY:
                    SendData3(remoteIp, comm_Element);
                    break;
            }
        }
        else
        {
            SendBytes(remoteIp, comm_Element);
        }
    }

    public void JobChangeCompeleted(int currentJobId)
    {
        if (D_userToken.Count > 0)
        {
            string remoteIp = D_userToken.Keys.ToList()[0];
            prefixFrame.JobId = currentJobId;
            switch (mode)
            {
                case TcpMode.Software2Plc:
                    AssemblePrefixFrame0();
                    Send(remoteIp, fixedPrefixFrameArray);
                    break;
                case TcpMode.MasterSoft2Soft:
                    AssemblePrefixFrame1();
                    Send(remoteIp, fixedPrefixFrameArray);
                    break;
                case TcpMode.ControledSoft2Soft:
                    AssemblePrefixFrame0();
                    Send(remoteIp, fixedPrefixFrameArray);
                    break;
                case TcpMode.DIY:
                    Send(remoteIp, BitConverter.GetBytes(currentJobId));
                    break;
            }
        }
    }

    public void ResetSystemState()
    {
    }

    public void Close()
    {
        Stop();
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
                        _mCommData.Outputs[key].Value.mValue = (char)((uint)((inputByteDic[key][0] & 0xFF) << 8) |
                                                                      (inputByteDic[key][1] & 0xFFu));
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

        throw new Exception($"通道{acqChannel}触发点位为空");
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
        int len = channelByteArray.Length + jobIdArray.Length + acqCompletedArray.Length +
                  acqCompletedArray.Length + systemStatusArray.Length;
        fixedPrefixFrameArray = new byte[len];
        Array.Copy(channelByteArray, 0, fixedPrefixFrameArray, 0, channelByteArray.Length);
        Array.Copy(jobIdArray, 0, fixedPrefixFrameArray, channelByteArray.Length, jobIdArray.Length);
        Array.Copy(acqReadyArray, 0, fixedPrefixFrameArray, channelByteArray.Length + jobIdArray.Length,
            acqReadyArray.Length);
        Array.Copy(acqCompletedArray, 0, fixedPrefixFrameArray,
            channelByteArray.Length + jobIdArray.Length + acqReadyArray.Length, acqCompletedArray.Length);
        Array.Copy(systemStatusArray, 0, fixedPrefixFrameArray,
            channelByteArray.Length + jobIdArray.Length + acqReadyArray.Length + acqCompletedArray.Length,
            systemStatusArray.Length);
    }

    private void AssemblePrefixFrame1()
    {
        byte[] channelByteArray = BitConverter.GetBytes(prefixFrame.Channel);
        byte[] jobIdArray = BitConverter.GetBytes(prefixFrame.JobId);
        byte[] triggerSignalArray = prefixFrame.TriggerSignal;
        byte[] changeJobExecArray = BitConverter.GetBytes(prefixFrame.ChangeJobExec);
        int len = channelByteArray.Length + jobIdArray.Length + triggerSignalArray.Length +
                  changeJobExecArray.Length;
        fixedPrefixFrameArray = new byte[len];
        Array.Copy(channelByteArray, 0, fixedPrefixFrameArray, 0, channelByteArray.Length);
        Array.Copy(jobIdArray, 0, fixedPrefixFrameArray, channelByteArray.Length, jobIdArray.Length);
        Array.Copy(triggerSignalArray, 0, fixedPrefixFrameArray, channelByteArray.Length + jobIdArray.Length,
            triggerSignalArray.Length);
        Array.Copy(changeJobExecArray, 0, fixedPrefixFrameArray,
            channelByteArray.Length + jobIdArray.Length + triggerSignalArray.Length, changeJobExecArray.Length);
    }

    private void SendData0(string remoteIp, List<Comm_Element> elements)
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

        Send(remoteIp, startArray);
    }

    public void SendData1(string remoteIp, List<Comm_Element> elements)
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

        Send(remoteIp, startArray);
    }

    public void SendData2(string remoteIp, List<Comm_Element> elements)
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

        Send(remoteIp, startArray);
    }

    public void SendData3(string remoteIp, List<Comm_Element> elements)
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
                    resultData = Encoding.ASCII.GetBytes((string)value);
                    break;
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
        byte[] startArray = new byte[len];
        foreach (KeyValuePair<int, byte[]> item in dic)
        {
            Array.Copy(item.Value, 0, startArray, item.Key, item.Value.Length);
        }

        Send(remoteIp, startArray);
    }

    public void SendCommElements(List<Comm_Element> elements, string remoteIp = null)
    {
        if (!ProtocolFlag)
        {
            SendData3(remoteIp, elements);
        }
        else
        {
            SendBytes(remoteIp, elements);
        }
    }

    public void SystemOffLine()
    {
    }

    public void SystemOnLine()
    {
    }

    public void RegisterEvent()
    {
        //for (int i = 0; i < pointInfoList.Count; i++)
        //{
        //    pointInfoList[i].valueChange += pointInfo_valueChange;
        //}
    }

    private void pointInfo_valueChange(object sender, ValueChangeEventArgs e)
    {
        if (e.newValue == 1)
        {
            LogUtil.Log("收到" + e.Description);
            if (e.Description.StartsWith("SetuserdataAck"))
            {
                handleSetuserdataAck[e.Index - 9].Set();
            }
            else if (e.Description.StartsWith("TriggercameraAck"))
            {
                handleTriggercameraAck[e.Index - 1].Set();
            }
        }
    }

    public void SendBytes(string remoteIp, List<Comm_Element> elements)
    {
        Dictionary<int, byte[]> dic = new Dictionary<int, byte[]>();
        List<int> Channellist = new List<int>();
        int maxOffSet = 0;
        if (elements.Count <= 0)
        {
            return;
        }

        foreach (Comm_Element element in elements)
        {
            object value = element.Value.mValue;
            string typeName = element.Type;
            if (!Channellist.Contains(element.Channel))
            {
                Channellist.Add(element.Channel);
            }

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
                    resultData = Encoding.ASCII.GetBytes((string)value);
                    break;
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
        byte[] startArray = new byte[len];
        foreach (KeyValuePair<int, byte[]> item in dic)
        {
            Array.Copy(item.Value, 0, startArray, item.Key, item.Value.Length);
        }

        if (Channellist.Max() >= 8 || Channellist.Min() < 0)
        {
            return;
        }

        byte[] dataTopic = new byte[1] { 81 };
        byte[] dataStart = BitConverter.GetBytes(CilentStart);
        byte[] dataStartL = BitConverter.GetBytes(dataStart.Length);
        byte[] dataControl = new byte[21]
        {
            1, 1, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0
        };
        List<byte[]> byteArrays = new List<byte[]>();
        for (int j = 0; j < handleSetuserdataAck.Length; j++)
        {
            handleSetuserdataAck[j] = new AutoResetEvent(initialState: false);
        }

        for (int i = 0; i < handleSetuserdataAck.Length; i++)
        {
            handleTriggercameraAck[i] = new AutoResetEvent(initialState: false);
        }

        foreach (int index3 in Channellist)
        {
            dataControl[index3 * 2 + 6] = 1;
        }

        byteArrays = new List<byte[]>
        {
            dataTopic,
            dataStartL,
            dataStart,
            BitConverter.GetBytes(dataControl.Length + startArray.Length),
            dataControl,
            startArray
        };
        byte[] data1 = byteArrays.SelectMany((byte[] a) => a).ToArray();
        Send(remoteIp, data1);
        foreach (int index2 in Channellist)
        {
            handleSetuserdataAck[index2].WaitOne(1000);
            dataControl[index2 * 2 + 5] = 1;
        }

        byteArrays = new List<byte[]>
        {
            dataTopic,
            dataStartL,
            dataStart,
            BitConverter.GetBytes(dataControl.Length),
            dataControl
        };
        byte[] data2 = byteArrays.SelectMany((byte[] a) => a).ToArray();
        Send(remoteIp, data2);
        foreach (int index in Channellist)
        {
            handleTriggercameraAck[index].WaitOne(1000);
            dataControl[index * 2 + 5] = 0;
            dataControl[index * 2 + 6] = 0;
        }

        byteArrays = new List<byte[]>
        {
            dataTopic,
            dataStartL,
            dataStart,
            BitConverter.GetBytes(dataControl.Length),
            dataControl
        };
        byte[] dataClear = byteArrays.SelectMany((byte[] a) => a).ToArray();
        Send(remoteIp, dataClear);
    }

    public void SendData(string data)
    {
        if (Dic_ClientInfo is { Count: > 0 })
        {
            string ip = Dic_ClientInfo.Keys.ToArray()[0];
            Send(ip, data);
        }
        else
        {
                LogUtil.Log("没有连接的客户端!");
        }
    }
}