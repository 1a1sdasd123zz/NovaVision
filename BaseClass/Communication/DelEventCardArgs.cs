using System;

namespace NovaVision.BaseClass.Communication
{
    public delegate void DelCardEventHandler(object sender, DelEventCardArgs e);
    public class DelEventCardArgs : EventArgs
    {
        public DelEventCardArgs(string sn)
        {
            this.SN = sn;
        }

        public readonly object SN;
    }
}
