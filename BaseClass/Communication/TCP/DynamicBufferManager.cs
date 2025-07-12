using System;
using System.Net;
using System.Text;

namespace NovaVision.BaseClass.Communication.TCP
{
    public class DynamicBufferManager
    {
        public byte[] Buffer { get; set; }

        public int DataCount { get; set; }

        public DynamicBufferManager(int bufferSize)
        {
            this.DataCount = 0;
            this.Buffer = new byte[bufferSize];
        }

        public int GetDataCount()
        {
            return this.DataCount;
        }

        public int GetReserveCount()
        {
            return this.Buffer.Length - this.DataCount;
        }

        public void Clear()
        {
            this.DataCount = 0;
        }

        public void Clear(int count)
        {
            bool flag = count >= this.DataCount;
            if (flag)
            {
                this.DataCount = 0;
            }
            else
            {
                for (int i = 0; i < this.DataCount - count; i++)
                {
                    this.Buffer[i] = this.Buffer[count + i];
                }
                this.DataCount -= count;
            }
        }

        public void SetBufferSize(int size)
        {
            bool flag = this.Buffer.Length < size;
            if (flag)
            {
                byte[] tmpBuffer = new byte[size];
                Array.Copy(this.Buffer, 0, tmpBuffer, 0, this.DataCount);
                this.Buffer = tmpBuffer;
            }
        }

        public void WriteBuffer(byte[] buffer, int offset, int count)
        {
            bool flag = this.GetReserveCount() >= count;
            if (flag)
            {
                Array.Copy(buffer, offset, this.Buffer, this.DataCount, count);
                this.DataCount += count;
            }
            else
            {
                int totalSize = this.Buffer.Length + count - this.GetReserveCount();
                byte[] tmpBuffer = new byte[totalSize];
                Array.Copy(this.Buffer, 0, tmpBuffer, 0, this.DataCount);
                Array.Copy(buffer, offset, tmpBuffer, this.DataCount, count);
                this.DataCount += count;
                this.Buffer = tmpBuffer;
            }
        }

        public void WriteBuffer(byte[] buffer)
        {
            this.WriteBuffer(buffer, 0, buffer.Length);
        }

        public void WriteShort(short value, bool convert)
        {
            if (convert)
            {
                value = IPAddress.HostToNetworkOrder(value);
            }
            byte[] tmpBuffer = BitConverter.GetBytes(value);
            this.WriteBuffer(tmpBuffer);
        }

        public void WriteInt(int value, bool convert)
        {
            if (convert)
            {
                value = IPAddress.HostToNetworkOrder(value);
            }
            byte[] tmpBuffer = BitConverter.GetBytes(value);
            this.WriteBuffer(tmpBuffer);
        }

        public void WriteLong(long value, bool convert)
        {
            if (convert)
            {
                value = IPAddress.HostToNetworkOrder(value);
            }
            byte[] tmpBuffer = BitConverter.GetBytes(value);
            this.WriteBuffer(tmpBuffer);
        }

        public void WriteString(string value)
        {
            byte[] tmpBuffer = Encoding.UTF8.GetBytes(value);
            this.WriteBuffer(tmpBuffer);
        }

        public void ClearBufferData()
        {
            Array.Clear(this.Buffer, 0, this.Buffer.Length);
        }
    }
}
