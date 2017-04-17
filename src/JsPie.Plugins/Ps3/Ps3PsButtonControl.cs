using JsPie.Core;

namespace JsPie.Plugins.Ps3
{
    public class Ps3PsButtonControl : Ps3Control
    {
        private readonly int _index;

        private bool _bit;
        private float _value;

        public Ps3PsButtonControl(Ps3ControlInfo ps3ControlInfo, int index)
            : base(ps3ControlInfo)
        {
            _index = index;
            _bit = false;
            _value = 0;
        }

        public override float Value { get { return _value; } }

        public override ControlEvent UpdateValue(Ps3InputData data)
        {
            var b = data.GetByte(_index) != 0;

            if (b == _bit)
                return null;

            _bit = b;
            var value = _value = b ? 1f : 0f;
            return new ControlEvent(Ps3ControlInfo.ControlId, value);
        }
    }
}
