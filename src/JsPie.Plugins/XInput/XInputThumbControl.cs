using JsPie.Core;
using System;

namespace JsPie.Plugins.XInput
{
    public class XInputThumbControl : XInputControl
    {
        public XInputThumbControl(XInputControlInfo xInputControlInfo)
            : base(xInputControlInfo)
        { }

        public ControlEvent UpdateValue(short value)
        {
            var newValue = (float)Math.Max((short)-32767, value) / 32767f;
            return UpdateValue(newValue);
        }
    }
}
