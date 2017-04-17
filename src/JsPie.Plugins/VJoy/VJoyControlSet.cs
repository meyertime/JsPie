using JsPie.Core;
using JsPie.Core.Util;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using vJoyInterfaceWrap;

namespace JsPie.Plugins.VJoy
{
    public class VJoyControlSet
    {
        private readonly ControllerId _controllerId;

        private readonly List<VJoyControl> _controls;
        private readonly Dictionary<string, VJoyControl> _controlDictionary;

        public VJoyControlSet(ControllerId controllerId)
        {
            _controllerId = Guard.NotNull(controllerId, nameof(controllerId));

            _controls = new List<VJoyControl>
            {
                Axis("axisX", s => s.AxisX),
                Axis("axisY", s => s.AxisY),
                Axis("axisZ", s => s.AxisZ),
                Axis("axisRX", s => s.AxisXRot),
                Axis("axisRY", s => s.AxisYRot),
                Axis("axisRZ", s => s.AxisZRot),
                Axis("slider", s => s.Slider),
                Axis("dial", s => s.Dial)
            };

            _controls.AddRange(Enumerable.Range(0, 32).Select(i => Button($"button{i + 1}", s => s.Buttons, i)));

            _controlDictionary = new Dictionary<string, VJoyControl>();
            foreach (var control in _controls)
            {
                _controlDictionary.Add(control.ControlId.Name, control);
            }
        }

        public IReadOnlyList<VJoyControl> Controls => _controls;

        public VJoyControl GetControlByName(string name)
        {
            VJoyControl result;
            if (!_controlDictionary.TryGetValue(name, out result))
                return null;

            return result;
        }

        private VJoyControl Axis(string name, Expression<VJoyControl.FieldSelector<int>> selector, string description = null)
        {
            return new VJoyAxisControl(new ControlInfo(name, description), new ControlId(_controllerId, name), selector);
        }

        private VJoyControl Button(string name, Expression<VJoyControl.FieldSelector<uint>> selector, int bit, string description = null)
        {
            return new VJoyButtonControl(new ControlInfo(name, description), new ControlId(_controllerId, name), selector, bit);
        }
    }
}
