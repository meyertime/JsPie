using JsPie.Core;
using JsPie.Core.Util;

namespace JsPie.Plugins.XInput
{
    public class XInputControl
    {
        public XInputControlInfo XInputControlInfo { get; }
        public float Value { get; private set; }

        public XInputControl(XInputControlInfo xInputControlInfo)
        {
            XInputControlInfo = Guard.NotNull(xInputControlInfo, nameof(xInputControlInfo));
        }

        public ControlEvent UpdateValue(float newValue)
        {
            if (newValue == Value)
            {
                return null;
            }

            Value = newValue;
            return new ControlEvent(XInputControlInfo.ControlId, newValue);
        }
    }
}
