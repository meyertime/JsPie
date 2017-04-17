using JsPie.Core.Util;

namespace JsPie.Core
{
    public class ControllerId : EquatableByValueBase<ControllerId>
    {
        public string Name { get; }
        public int Index { get; }

        public ControllerId(string name, int index = 0)
        {
            Guard.Ensure(index >= 0, nameof(index), "Index cannot be negative.");

            Name = Guard.NotNull(name, nameof(name));
            Index = index;
        }

        public override string ToString()
        {
            return $"{Name}[{Index}]";
        }
    }
}
