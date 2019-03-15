using JsPie.Core;

namespace JsPie.Plugins.XInput
{
    public class XInputButtonControl : XInputControl
    {
        private readonly ushort _buttonMask;

        public XInputButtonControl(XInputControlInfo xInputControlInfo, ushort buttonMask)
            : base(xInputControlInfo)
        {
            _buttonMask = buttonMask;
        }

        public ControlEvent UpdateValue(ushort buttons)
        {
            var newValue = ((buttons & _buttonMask) != 0) ? 1f : 0f;
            return UpdateValue(newValue);
        }
    }
}
