using JsPie.Core;
using JsPie.Core.Util;

namespace JsPie.Plugins.Ps3
{
    public abstract class Ps3Control
    {
        public Ps3ControlInfo Ps3ControlInfo { get; }
        public abstract float Value { get; }

        public Ps3Control(Ps3ControlInfo ps3ControlInfo)
        {
            Ps3ControlInfo = Guard.NotNull(ps3ControlInfo, nameof(ps3ControlInfo));
        }

        public abstract ControlEvent UpdateValue(Ps3InputData data);
    }
}
