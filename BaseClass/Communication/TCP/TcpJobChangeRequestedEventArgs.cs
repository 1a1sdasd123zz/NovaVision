using System;

namespace NovaVision.BaseClass.Communication.TCP
{
    public class TcpJobChangeRequestedEventArgs : EventArgs
    {
        public int JobId { get; }

        public TcpJobChangeRequestedEventArgs(int currentJobId)
        {
            JobId = currentJobId;
        }
    }

}
