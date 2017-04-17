using JsPie.Core;

namespace JsPie.Plugins.Ps3
{
    public class Ps3DPadControl : Ps3Control
    {
        private readonly int _dPadValue;

        private bool _bit;
        private float _value;

        public Ps3DPadControl(Ps3ControlInfo ps3ControlInfo, int dPadValue)
            : base(ps3ControlInfo)
        {
            _dPadValue = dPadValue;
            _bit = false;
            _value = 0;
        }

        public override float Value { get { return _value; } }

        public override ControlEvent UpdateValue(Ps3InputData data)
        {
            var b = data.GetDPad() == _dPadValue;

            if (b == _bit)
                return null;

            _bit = b;
            var value = _value = b ? 1f : 0f;
            return new ControlEvent(Ps3ControlInfo.ControlId, value);
        }
    }
}
