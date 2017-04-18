using System;
using System.Runtime.InteropServices;
using static JsPie.Plugins.Keyboard.KeyboardApi;

namespace JsPie.Plugins.Keyboard
{
    public class KeyboardHook : IDisposable
    {
        private readonly uint _mark;

        private readonly LowLevelKeyboardProc _lowLevelKeyboardProc;
        private readonly IntPtr _hHook;        

        public KeyboardHook(uint mark)
        {
            _mark = mark;

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
                var hookInfo = Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lParam);
                if (hookInfo.dwExtraInfo != _mark)
                {
                    switch ((int)wParam)
                    {
                        case WM_KEYDOWN:
                        case WM_SYSKEYDOWN:
                            RaiseEvent(hookInfo.vkCode, true);
                            break;

                        case WM_KEYUP:
                        case WM_SYSKEYUP:
                            RaiseEvent(hookInfo.vkCode, false);
                            break;
                    }
                }
            }

            return CallNextHookEx(_hHook, nCode, wParam, lParam);
        }

        private void RaiseEvent(uint keyCode, bool value)
        {
            var handler = KeyboardHookEvent;
            if (handler != null)
                handler(keyCode, value);
        }
    }
}
