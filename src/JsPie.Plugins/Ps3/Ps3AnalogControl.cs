using JsPie.Core;

namespace JsPie.Plugins.Ps3
{
    public class Ps3AnalogControl : Ps3Control
    {
        private readonly int _index;

        private byte _byte;
        private float _value;

        public Ps3AnalogControl(Ps3ControlInfo ps3ControlInfo, int index)
            : base(ps3ControlInfo)
        {
            _index = index;
            _byte = 127;
            _value = 0;
        }

        public override float Value { get { return _value; } }

        public override ControlEvent UpdateValue(Ps3InputData data)
        {
            var b = data.GetByte(_index);

            if (b == _byte)
                return null;

            _byte = b;
            var value = _value = (b - 127.5f) / 127.5f;
            return new ControlEvent(Ps3ControlInfo.ControlId, value);
        }
    }
}
