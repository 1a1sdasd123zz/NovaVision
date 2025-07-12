namespace NovaVision.BaseClass.Communication.TCP
{
    public class FixedPrefixFrame
    {
        public int Channel { get; set; }

        public int JobId { get; set; }

        public byte[] AcqReady { get; set; }

        public byte[] AcqCompleted { get; set; }

        public int SystemStatus { get; set; }

        public byte[] TriggerSignal { get; set; }

        public int ChangeJobExec { get; set; }
    }
}
