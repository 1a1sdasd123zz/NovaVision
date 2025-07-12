using System;
using System.Runtime.InteropServices;

namespace NovaVision.Hardware._011_SDK_SSZN3DTool
{
    public sealed class PinnedObject : IDisposable
    {
        private GCHandle _Handle;

        public IntPtr Pointer => _Handle.AddrOfPinnedObject();

        public PinnedObject(object target)
        {
            _Handle = GCHandle.Alloc(target, GCHandleType.Pinned);
        }

        public void Dispose()
        {
            _Handle.Free();
            _Handle = default(GCHandle);
        }
    }
}
