using System;

namespace NovaVision.BaseClass.Communication.TCP
{
    public class TcpEventArgs : EventArgs
    {
        public int Channel { get; set; }

        public byte StatusValue { get; set; } = 1;

    }
}
