using System;
using System.Runtime.InteropServices;

namespace NovaVision.Hardware
{
    public class SafeBufferExt : SafeBuffer
    {
        public SafeBufferExt(int size)
            : base(ownsHandle: true)
        {
            SetHandle(Marshal.AllocHGlobal(size));
            Initialize((ulong)size);
        }

        protected override bool ReleaseHandle()
        {
            Marshal.FreeHGlobal(handle);
            return true;
        }

        public static implicit operator IntPtr(SafeBufferExt SB)
        {
            return SB.handle;
        }
    }
}
