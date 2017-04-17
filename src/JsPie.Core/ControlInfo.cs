using JsPie.Core.Util;

namespace JsPie.Core
{
    public class ControlInfo
    {
        public string Name { get; }
        public string Description { get; }

        public ControlInfo(string name, string description = null)
        {
            Name = Guard.NotNullOrEmpty(name, nameof(name));
            Description = description;
        }
    }
}
