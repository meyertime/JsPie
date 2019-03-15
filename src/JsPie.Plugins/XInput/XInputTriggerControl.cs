using JsPie.Core;

namespace JsPie.Plugins.XInput
{
    public class XInputTriggerControl : XInputControl
    {
        public XInputTriggerControl(XInputControlInfo xInputControlInfo)
            : base(xInputControlInfo)
        { }

        public ControlEvent UpdateValue(byte value)
        {
            var newValue = (float)value / 255f;
            return UpdateValue(newValue);
        }
    }
}
