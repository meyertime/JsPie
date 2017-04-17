using System;
using System.Runtime.InteropServices;
using static JsPie.Plugins.Keyboard.KeyboardApi;

namespace JsPie.Plugins.Keyboard
{
    public class KeyboardHook : IDisposable
    {
        private readonly IntPtr _hHook;
        private readonly LowLevelKeyboardProc _lowLevelKeyboardProc;

        public KeyboardHook()
        {
            _lowLevelKeyboardProc = LowLevelKeyboardProc;

            _hHook = SetWindowsHookEx(WH_KEYBOARD_LL, _lowLevelKeyboardProc, IntPtr.Zero, 0);
        }

        public void Dispose()
        {
            UnhookWindowsHookEx(_hHook);
        }

        public event KeyboardHookEventHandler KeyboardHookEvent;
        
        private IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                switch ((int)wParam)
                {
                    case WM_KEYDOWN:
                    case WM_SYSKEYDOWN:
                        RaiseEvent(Marshal.ReadInt32(lParam), true);
                        break;

                    case WM_KEYUP:
                    case WM_SYSKEYUP:
                        RaiseEvent(Marshal.ReadInt32(lParam), false);
                        break;
                }
            }

            return CallNextHookEx(_hHook, nCode, wParam, lParam);
        }

        private void RaiseEvent(int keyCode, bool value)
        {
            var handler = KeyboardHookEvent;
            if (handler != null)
                handler(keyCode, value);
        }
    }
}
