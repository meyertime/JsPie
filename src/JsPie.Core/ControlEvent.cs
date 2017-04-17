using JsPie.Core.Util;

namespace JsPie.Core
{
    public class ControlEvent
    {
        public ControlId ControlId { get; }
        public float Value { get; }

        public ControlEvent(ControlId controlId, float value)
        {
            ControlId = Guard.NotNull(controlId, nameof(controlId));
            Value = value;
        }
    }
}
