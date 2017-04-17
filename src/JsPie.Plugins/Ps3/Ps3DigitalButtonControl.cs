using JsPie.Core;

namespace JsPie.Plugins.Ps3
{
    public class Ps3DigitalButtonControl : Ps3Control
    {
        private readonly int _index;
        private readonly int _bitMask;

        private bool _bit;
        private float _value;

        public Ps3DigitalButtonControl(Ps3ControlInfo ps3ControlInfo, int index, int bit)
            : base(ps3ControlInfo)
        {
            _index = index;
            _bitMask = 1 << bit;
            _bit = false;
            _value = 0;
        }

        public override float Value { get { return _value; } }

        public override ControlEvent UpdateValue(Ps3InputData data)
        {
            var b = data.GetBit(_index, _bitMask);

            if (b == _bit)
                return null;

            _bit = b;
            var value = _value = b ? 1f : 0f;
            return new ControlEvent(Ps3ControlInfo.ControlId, value);
        }
    }
}
