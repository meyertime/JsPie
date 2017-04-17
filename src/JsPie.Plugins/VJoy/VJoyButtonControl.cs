using System;
using JsPie.Core;
using vJoyInterfaceWrap;
using JsPie.Core.Util;
using System.Linq.Expressions;

namespace JsPie.Plugins.VJoy
{
    public class VJoyButtonControl : VJoyControl
    {
        private readonly GetValueDelegate _getValueDelegate;
        private readonly SetValueDelegate _setValueDelegate;
        private readonly uint _setBitMask;
        private readonly uint _unsetBitMask;

        public VJoyButtonControl(ControlInfo controlInfo, ControlId controlId, Expression<FieldSelector<uint>> selector, int bit) 
            : base(controlInfo, controlId)
        {
            Guard.NotNull(selector, nameof(selector));

            _getValueDelegate = MakeGetValueDelegate(selector);
            _setValueDelegate = MakeSetValueDelegate(selector);

            _setBitMask = (uint)1 << bit;
            _unsetBitMask = ~_setBitMask;
        }

        public override void SetValue(ref vJoy.JoystickState state, float value)
        {
            var uintValue = _getValueDelegate(ref state);

            if (value != 0f)
            {
                uintValue |= _setBitMask;
            }
            else
            {
                uintValue &= _unsetBitMask;
            }

            _setValueDelegate(ref state, uintValue);
        }
    }
}
