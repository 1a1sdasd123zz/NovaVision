using HslCommunication.Core.Device;

namespace NovaVision.BaseClass.Communication
{
    public class CommunicationBase
    {
        public string IP;//IP地址
        public int Port;//端口号
        public byte Station;//站号
        public int ConnectTimeOut;
        public int ReceiveTimeOut;

        public DeviceCommunication dev = new();

        public virtual int Connect()
        {
            return -1;
        }

        public virtual void ConnectClose()
        {

        }

        #region [读操作]    

        public bool ReadBool(string address)
        {

            bool result;
            result = dev.ReadBool(address).Content;

            return result;
        }

        public bool[] ReadBool(string address, ushort length)
        {

            bool[] result;
            result = dev.ReadBool(address, length).Content;

            return result;
        }
        /// <summary>
        /// 读取short16位有符号数据
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <returns>读取结果，错误返回false</returns>
        public short ReadShort(string address)
        {
            short result;
            result = dev.ReadInt16(address).Content;
            return result;
        }
        /// <summary>
        /// 读取short16位有符号数据数组
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <returns>读取结果，错误返回false</returns>
        public short[] ReadShort(string address, ushort length)
        {

            short[] result = new short[] { };
            result = dev.ReadInt16(address, length).Content;
            return result;
        }


        /// <summary>
        /// 读取int数据
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <returns>=读取结果，错误返回false</returns>
        public int ReadInt(string address)
        {
            int result;
            //int tem = Convert.ToInt32(address);
            //tem -= 1;//因为下位机那边写入的时候会比实际填的地址多一位，所以这里-1处理
            //address = tem.ToString();
            result = dev.ReadInt32(address).Content;
            return result;
        }
        /// <summary>
        /// 读取int数据
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <returns>=读取结果，错误返回false</returns>
        public int[] ReadInt(string address, ushort length)
        {
            int[] result = new int[] { };
            result = dev.ReadInt32(address, length).Content;
            return result;
        }
        /// <summary>
        /// 读取Long数据
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <returns>读取结果，错误返回fasle</returns>
        public long ReadLong(string address)
        {
            long result;
            result = dev.ReadInt64(address).Content;
            return result;
        }
        /// <summary>
        /// 读取float数据
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <returns>返回读取结果，读取错误返回false</returns>
        public float ReadFloat(string address)
        {

            float result;
            result = dev.ReadFloat(address).Content;
            return result;
        }
        public float[] ReadFloat(string address, ushort length)
        {

            float[] result;
            result = dev.ReadFloat(address, length).Content;
            return result;
        }
        /// <summary>
        /// 读ushort16位无符号数据
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <returns>读取结果，错误返回Uint16最大值</returns>
        public ushort ReadUShort(string address)
        {
            ushort result;
            result = dev.ReadUInt16(address).Content;
            return result;
        }
        public ushort[] ReadUShort(string address, ushort length)
        {
            ushort[] result;
            result = dev.ReadUInt16(address, length).Content;
            return result;
        }
        /// <summary>
        /// 读取uint32位无符号整型数据
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <returns>读取结果，错误返回false</returns>
        public uint ReadUInt(string address)
        {
            uint result;
            result = dev.ReadUInt32(address).Content;
            return result;
        }
        public uint[] ReadUInt(string address, ushort length)
        {
            uint[] result;
            result = dev.ReadUInt32(address, length).Content;
            return result;
        }
        /// <summary>
        /// 读取ULong64位无符号整型数据
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <returns>读取结果，错误返回false</returns>
        public ulong ReadULong(string address)
        {

            ulong result;
            result = dev.ReadUInt64(address).Content;
            return result;
        }
        /// <summary>
        /// 读取double数据
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <returns>读取结果，错误返回false</returns>
        public double ReadDouble(string address)
        {

            double result;
            result = dev.ReadDouble(address).Content;
            return result;
        }
        public double[] ReadDouble(string address, ushort length)
        {

            double[] result;
            result = dev.ReadDouble(address, length).Content;
            return result;
        }
        /// <summary>
        /// 读取字符串
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <param name="strlength">读取长度</param>
        /// <returns>读取结果，错误返回false</returns>
        public string Readstring(string address, ushort length)
        {

            string result;
            result = dev.ReadString(address, length).Content;

            return result;
        }

        public string Readstring(string address, ushort length, System.Text.Encoding encoding)
        {

            string result;
            result = dev.ReadString(address, length, encoding).Content;

            return result;
        }
        #endregion


        #region 写Keyence数据
        /// <summary>
        /// 写bool型数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <returns>写入结果,true为写入成功，false为写入失败</returns>
        public bool WriteBool(string address, bool value)
        {
            return dev.Write(address, value).IsSuccess;
        }

        public bool WriteBool(string address, bool[] value)
        {
            return dev.Write(address, value).IsSuccess;
        }
        /// <summary>
        /// 写short型数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <param name="value">写入数据</param>
        /// <returns>写入结果，1为写入成功，false为写入失败</returns>
        public bool WriteShort(string address, short value)
        {
            return dev.Write(address, value).IsSuccess;
        }
        public bool WriteShort(string address, short[] value)
        {
            return dev.Write(address, value).IsSuccess;
        }
        /// <summary>
        /// 写int型数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <param name="value">写入数据</param>
        /// <returns>写入结果，1为成功，-1为失败</returns>
        public bool WriteInt(string address, int value)
        {
            return dev.Write(address, value).IsSuccess;
        }

        public bool WriteInt(string address, int[] value)
        {
            return dev.Write(address, value).IsSuccess;
        }
        /// <summary>
        /// 写long型数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <param name="value">写入数据</param>
        /// <returns>写入结果，成功为1，失败为-1</returns>
        public bool WriteLong(string address, long value)
        {
            return dev.Write(address, value).IsSuccess;
        }
        /// <summary>
        /// 写float型数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <param name="value">写入数据</param>
        /// <returns>写入结果，1为成功，-1为失败</returns>
        public bool WriteFloat(string address, float value)
        {
            return dev.Write(address, value).IsSuccess;
        }
        public bool WriteFloat(string address, float[] value)
        {
            return dev.Write(address, value).IsSuccess;
        }
        /// <summary>
        /// 写ushort型数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <param name="value">写入数据</param>
        /// <returns>写入结果，1为成功，-1为失败</returns>
        public bool WriteUShort(string address, ushort value)
        {
            return dev.Write(address, value).IsSuccess;
        }
        public bool WriteUShort(string address, ushort[] value)
        {
            return dev.Write(address, value).IsSuccess;
        }
        /// <summary>
        /// 写uint型数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <param name="value">写入数据</param>
        /// <returns>写入结果，1为成功，-1为失败</returns>
        public bool WriteUInt(string address, uint value)
        {
            return dev.Write(address, value).IsSuccess;
        }
        public bool WriteUInt(string address, uint[] value)
        {
            return dev.Write(address, value).IsSuccess;
        }
        /// <summary>
        /// 写入ulong型数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <param name="value">写入数据</param>
        /// <returns>写入结果，1为成功，-1为失败</returns>
        public bool WriteULong(string address, ulong value)
        {
            return dev.Write(address, value).IsSuccess;
        }
        public bool WriteULong(string address, ulong[] value)
        {
            return dev.Write(address, value).IsSuccess;
        }
        /// <summary>
        /// 写入double型数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <param name="value">写入数据</param>
        /// <returns>写入结果，1为成功，-1为失败</returns>
        public bool WriteDouble(string address, double value)
        {
            return dev.Write(address, value).IsSuccess;
        }
        public bool WriteDouble(string address, double[] value)
        {
            return dev.Write(address, value).IsSuccess;
        }
        /// <summary>
        /// 写入string型数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <param name="value">写入数据</param>
        /// <returns>写入结果，1为成功，-1为失败</returns>
        public bool WriteString(string address, string value)
        {
            return dev.Write(address, value).IsSuccess;
        }
        public bool WriteString(string address, string value, System.Text.Encoding encoding)
        {
            return dev.Write(address, value, encoding).IsSuccess;
        }
        public bool WriteString(string address, string value, int length)
        {
            return dev.Write(address, value, length).IsSuccess;
        }
        public bool WriteString(string address, string value, int length, System.Text.Encoding encoding)
        {
            return dev.Write(address, value, length, encoding).IsSuccess;
        }
        #endregion
    }
}
