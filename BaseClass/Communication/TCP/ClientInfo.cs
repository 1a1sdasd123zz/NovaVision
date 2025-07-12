using System;

namespace NovaVision.BaseClass.Communication.TCP
{
    public class ClientInfo
    {
        public string ClientIp { get; set; }

        public DateTime LastHeartbeatTime { get; set; }

        public bool State { get; set; }
    }
}
