using JsPie.Core.Util;

namespace JsPie.Core
{
    public class ControlId : EquatableByValueBase<ControlId>
    {
        public ControllerId ControllerId { get; }
        public string Name { get; }

        public ControlId(ControllerId controllerId, string name)
        {
            ControllerId = Guard.NotNull(controllerId, nameof(controllerId));
            Name = Guard.NotNullOrEmpty(name, nameof(name));
        }

        public override string ToString()
        {
            return $"{ControllerId}.{Name}";
        }
    }
}
