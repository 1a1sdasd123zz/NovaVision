using static NovaVision.BaseClass.Communication.TCP.MyTcpServer;

namespace NovaVision.BaseClass.Communication.TCP
{
    public class TcpBlockElement
    {
        private int _index;

        private int _byteOffset;

        private byte _value;

        public int ByteOffset
        {
            get
            {
                return _byteOffset;
            }
            set
            {
                _byteOffset = value;
            }
        }

        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
            }
        }

        public byte Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value == value)
                {
                    return;
                }
                _value = value;
                try
                {
                    if (this.valueChange != null)
                    {
                        this.valueChange(this, new ValueChangeEventArgs
                        {
                            Index = _index
                        });
                    }
                }
                catch
                {
                }
            }
        }

        public event ValueChangedEventHandler valueChange;

        public void SetValue(byte val)
        {
            _value = val;
        }
    }
}
