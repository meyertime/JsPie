using JsPie.Core;

namespace JsPie.Plugins.Ps3
{
    public class Ps3AnalogButtonControl : Ps3Control
    {
        private readonly int _index;

        private byte _byte;
        private float _value;

        public Ps3AnalogButtonControl(Ps3ControlInfo ps3ControlInfo, int index)
            : base(ps3ControlInfo)
        {
            _index = index;
            _byte = 0;
            _value = 0;
        }

        public override float Value { get { return _value; } }

        public override ControlEvent UpdateValue(Ps3InputData data)
        {
            var b = data.GetByte(_index);

            if (b == _byte)
                return null;

            _byte = b;
            var value = _value = b / 255f;
            return new ControlEvent(Ps3ControlInfo.ControlId, value);
        }
    }
}
