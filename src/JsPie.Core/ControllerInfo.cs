using JsPie.Core.Util;
using System.Collections.Generic;

namespace JsPie.Core
{
    public class ControllerInfo
    {
        public string Name { get; }
        public int Count { get; }
        public string Description { get; }

        private readonly Dictionary<string, ControlInfo> _controls;

        public ControllerInfo(string name, int count, string description, IEnumerable<ControlInfo> controls)
        {
            Guard.Ensure(count > 0, nameof(count), "Count must be positive.");

            Name = Guard.NotNullOrEmpty(name, nameof(name));
            Count = count;
            Description = description;

            Guard.NotNull(controls, nameof(controls));

            _controls = new Dictionary<string, ControlInfo>();
            foreach (var control in controls)
            {
                _controls.Add(control.Name, control);
            }
        }

        public IReadOnlyDictionary<string, ControlInfo> Controls => _controls;
    }
}
