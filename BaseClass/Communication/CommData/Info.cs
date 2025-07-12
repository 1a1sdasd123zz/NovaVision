using NovaVision.BaseClass.Module;

namespace NovaVision.BaseClass.Communication.CommData
{
    public class Info : InfoBase
    {
        public int StartByte;

        public int EndByte;
        public int GetBufferLength()
        {
            return this.EndByte - this.StartByte + 1;
        }
    }
}
