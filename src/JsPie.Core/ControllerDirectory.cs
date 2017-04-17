using JsPie.Core.Util;
using System.Collections.Generic;

namespace JsPie.Core
{
    public class ControllerDirectory
    {
        private readonly Dictionary<string, ControllerInfo> _inputControllers;
        private readonly Dictionary<string, ControllerInfo> _outputControllers;

        public ControllerDirectory(IEnumerable<ControllerInfo> inputControllers, IEnumerable<ControllerInfo> outputControllers)
        {
            Guard.NotNull(inputControllers, nameof(inputControllers));
            Guard.NotNull(outputControllers, nameof(outputControllers));

            _inputControllers = new Dictionary<string, ControllerInfo>();
            foreach (var controller in inputControllers)
            {
                _inputControllers.Add(controller.Name, controller);
            }

            _outputControllers = new Dictionary<string, ControllerInfo>();
            foreach (var controller in outputControllers)
            {
                _outputControllers.Add(controller.Name, controller);
            }
        }

        public IReadOnlyDictionary<string, ControllerInfo> InputControllers => _inputControllers;
        public IReadOnlyDictionary<string, ControllerInfo> OutputControllers => _outputControllers;
    }
}
