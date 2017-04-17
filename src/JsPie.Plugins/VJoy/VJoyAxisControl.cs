using System;
using JsPie.Core;
using vJoyInterfaceWrap;
using JsPie.Core.Util;
using System.Linq.Expressions;

namespace JsPie.Plugins.VJoy
{
    public class VJoyAxisControl : VJoyControl
    {
        private readonly SetValueDelegate _setValueDelegate;

        public VJoyAxisControl(ControlInfo controlInfo, ControlId controlId, Expression<FieldSelector<int>> selector) 
            : base(controlInfo, controlId)
        {
            Guard.NotNull(selector, nameof(selector));

            _setValueDelegate = MakeSetValueDelegate(selector);
        }

        public override void SetValue(ref vJoy.JoystickState state, float value)
        {
            var uintValue = Math.Min(0x8000, Math.Max(1, (uint)((value * 16383.5f) + 16384.5f)));
            _setValueDelegate(ref state, uintValue);
        }
    }
}
