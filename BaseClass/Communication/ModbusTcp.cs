using HslCommunication;
using HslCommunication.ModBus;

namespace NovaVision.BaseClass.Communication
{
    public class ModbusTcp : CommunicationBase
    {
        HslCommunication.Core.DataFormat DataFormat { get; set; }
        bool AddressStartWithZero { get; set; }

        public HslCommunication.ModBus.ModbusTcpNet _clintent = null;

        public ModbusTcp(string Ip, int port)
        {
            _clintent = new ModbusTcpNet();
            this.IP = Ip;
            this.Port = port;
        }
        public override int Connect()
        {
            try
            {
                _clintent.ConnectTimeOut = ConnectTimeOut;
                _clintent.ReceiveTimeOut = ReceiveTimeOut;
                _clintent.IpAddress = IP;
                _clintent.Port = Port;
                _clintent.Station = Station;
                _clintent.DataFormat = DataFormat;
                _clintent.AddressStartWithZero = AddressStartWithZero;

                OperateResult connect = _clintent.ConnectServer();
                if (connect.IsSuccess)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return -1;
            }

        }

        public override void ConnectClose()
        {
            try
            {
                _clintent.ConnectClose();
            }
            catch { }
        }
    }
}
