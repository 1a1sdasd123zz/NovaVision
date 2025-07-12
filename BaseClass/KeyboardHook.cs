using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NovaVision.BaseClass
{

    public class KeyboardHook
    {
        public delegate int HookProc(int nCode, int wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        public class KeyboardHookStruct
        {
            public int vkCode;

            public int scanCode;

            public int flags;

            public int time;

            public int dwExtraInfo;
        }

        private static int hKeyboardHook;

        public const int WH_KEYBOARD_LL = 13;

        private HookProc KeyboardHookProcedure;

        private const int WM_KEYDOWN = 256;

        private const int WM_KEYUP = 257;

        private const int WM_SYSKEYDOWN = 260;

        private const int WM_SYSKEYUP = 261;

        public event KeyEventHandler KeyDownEvent;

        public event KeyPressEventHandler KeyPressEvent;

        public event KeyEventHandler KeyUpEvent;

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
        public static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern int GetCurrentThreadId();

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string name);

        public void Start()
        {
            if (hKeyboardHook == 0)
            {
                KeyboardHookProcedure = KeyboardHookProc;
                hKeyboardHook = SetWindowsHookEx(13, KeyboardHookProcedure, GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);
                SetWindowsHookEx(13, KeyboardHookProcedure, IntPtr.Zero, GetCurrentThreadId());
                if (hKeyboardHook == 0)
                {
                    Stop();
                    throw new Exception("安装键盘钩子失败");
                }
            }
        }

        public void Stop()
        {
            bool retKeyboard = true;
            if (hKeyboardHook != 0)
            {
                retKeyboard = UnhookWindowsHookEx(hKeyboardHook);
                hKeyboardHook = 0;
            }
            if (!retKeyboard)
            {
                throw new Exception("卸载钩子失败！");
            }
        }

        [DllImport("user32")]
        public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);

        [DllImport("user32")]
        public static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
        private static extern short GetKeyState(int vKey);

        private int KeyboardHookProc(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (this.KeyDownEvent != null || this.KeyUpEvent != null || this.KeyPressEvent != null))
            {
                KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                if (this.KeyDownEvent != null && (wParam == 256 || wParam == 260))
                {
                    Keys keyData2 = (Keys)MyKeyboardHookStruct.vkCode;
                    KeyEventArgs e3 = new KeyEventArgs(keyData2);
                    this.KeyDownEvent(this, e3);
                }
                if (this.KeyPressEvent != null && wParam == 256)
                {
                    byte[] keyState = new byte[256];
                    GetKeyboardState(keyState);
                    byte[] inBuffer = new byte[2];
                    if (ToAscii(MyKeyboardHookStruct.vkCode, MyKeyboardHookStruct.scanCode, keyState, inBuffer, MyKeyboardHookStruct.flags) == 1)
                    {
                        KeyPressEventArgs e2 = new KeyPressEventArgs((char)inBuffer[0]);
                        this.KeyPressEvent(this, e2);
                    }
                }
                if (this.KeyUpEvent != null && (wParam == 257 || wParam == 261))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    this.KeyUpEvent(this, e);
                }
            }
            return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
        }

        ~KeyboardHook()
        {
            Stop();
        }
    }
}
