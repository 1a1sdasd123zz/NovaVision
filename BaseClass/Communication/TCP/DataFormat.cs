using System.Linq;

namespace NovaVision.BaseClass.Communication.TCP
{
    public class DataFormat
    {
        public static void BytesExchange(ref byte[] data)
        {
            switch (data.Length)
            {
                case 2:
                    {
                        byte tempByte = data[0];
                        data[0] = data[1];
                        data[1] = tempByte;
                        break;
                    }
                case 4:
                    data = data.Reverse().ToArray();
                    break;
                case 8:
                    data = data.Reverse().ToArray();
                    break;
            }
        }
    }
}
