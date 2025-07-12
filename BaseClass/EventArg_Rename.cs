using System;

namespace NovaVision.BaseClass
{
    public class EventArg_Rename : EventArgs
    {
        public int index;

        public string oldNmae;

        public string newName;
    }
}
