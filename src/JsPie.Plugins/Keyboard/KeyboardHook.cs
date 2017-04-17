using System;
using System.Runtime.InteropServices;

namespace JsPie.Plugins.Keyboard
{
    public class KeyboardHook : IDisposable
    {
        private readonly IntPtr _hHook;

        public KeyboardHook()
        {
            _hHook = KeyboardApi.SetWindowsHookEx(KeyboardApi.WH_KEYBOARD_LL, LowLevelKeyboardProc, IntPtr.Zero, 0);
        }

        public void Dispose()
        {
            KeyboardApi.UnhookWindowsHookEx(_hHook);
        }

        public event KeyboardHookEventHandler KeyboardHookEvent;
        
        private IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                switch ((int)wParam)
                {
                    case KeyboardApi.WM_KEYDOWN:
                    case KeyboardApi.WM_SYSKEYDOWN:
                        RaiseEvent(Marshal.ReadInt32(lParam), true);
                        break;

                    case KeyboardApi.WM_KEYUP:
                    case KeyboardApi.WM_SYSKEYUP:
                        RaiseEvent(Marshal.ReadInt32(lParam), false);
                        break;
                }
            }

            return KeyboardApi.CallNextHookEx(_hHook, nCode, wParam, lParam);
        }

        private void RaiseEvent(int keyCode, bool value)
        {
            var handler = KeyboardHookEvent;
            if (handler != null)
                handler(keyCode, value);
        }
    }
}
