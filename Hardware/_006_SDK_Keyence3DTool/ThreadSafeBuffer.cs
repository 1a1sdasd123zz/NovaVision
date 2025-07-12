using System.Collections.Generic;

namespace NovaVision.Hardware._006_SDK_Keyence3DTool
{
    public static class ThreadSafeBuffer
    {
        private const int BatchFinalizeFlagBitCount = 16;

        private static List<int[]>[] _buffer;

        private static uint[] _count;

        private static object[] _syncObject;

        private static uint[] _notify;

        private static int[] _batchNo;

        static ThreadSafeBuffer()
        {
            _buffer = new List<int[]>[NativeMethods.DeviceCount];
            _count = new uint[NativeMethods.DeviceCount];
            _syncObject = new object[NativeMethods.DeviceCount];
            _notify = new uint[NativeMethods.DeviceCount];
            _batchNo = new int[NativeMethods.DeviceCount];
            for (int i = 0; i < NativeMethods.DeviceCount; i++)
            {
                _buffer[i] = new List<int[]>();
                _syncObject[i] = new object();
            }
        }

        public static int GetBufferDataCount(int index)
        {
            return _buffer[index].Count;
        }

        public static void Add(int index, List<int[]> value, uint notify)
        {
            lock (_syncObject[index])
            {
                _buffer[index].AddRange(value);
                _count[index] += (uint)value.Count;
                _notify[index] |= notify;
                if ((notify & 0x10000u) != 0)
                {
                    _batchNo[index]++;
                }
            }
        }

        public static void Clear(int index)
        {
            lock (_syncObject[index])
            {
                _buffer[index].Clear();
            }
        }

        public static void ClearBuffer(int index)
        {
            Clear(index);
            ClearCount(index);
            _batchNo[index] = 0;
            ClearNotify(index);
        }

        public static List<int[]> Get(int index, out uint notify, out int batchNo)
        {
            List<int[]> value = new List<int[]>();
            lock (_syncObject[index])
            {
                value.AddRange(_buffer[index]);
                _buffer[index].Clear();
                notify = _notify[index];
                _notify[index] = 0u;
                batchNo = _batchNo[index];
            }
            return value;
        }

        internal static void AddCount(int index, uint count, uint notify)
        {
            lock (_syncObject[index])
            {
                _count[index] += count;
                _notify[index] |= notify;
                if ((notify & 0x10000u) != 0)
                {
                    _batchNo[index]++;
                }
            }
        }

        internal static uint GetCount(int index, out uint notify, out int batchNo)
        {
            lock (_syncObject[index])
            {
                notify = _notify[index];
                _notify[index] = 0u;
                batchNo = _batchNo[index];
                return _count[index];
            }
        }

        private static void ClearCount(int index)
        {
            lock (_syncObject[index])
            {
                _count[index] = 0u;
            }
        }

        private static void ClearNotify(int index)
        {
            lock (_syncObject[index])
            {
                _notify[index] = 0u;
            }
        }
    }
}
