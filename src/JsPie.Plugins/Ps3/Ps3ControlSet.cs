using JsPie.Core;
using JsPie.Core.Util;
using System.Collections.Generic;

namespace JsPie.Plugins.Ps3
{
    public class Ps3ControlSet
    {
        private readonly ControllerId _controllerId;

        private readonly Ps3Control[] _controls;

        public Ps3ControlSet(ControllerId controllerId)
        {
            _controllerId = Guard.NotNull(controllerId, nameof(controllerId));

            _controls = new[]
            {
                Analog("leftStickX",  1),
                Analog("leftStickY",  2),
                Analog("rightStickX", 3),
                Analog("rightStickY", 4),
                DPad("up",    dPadValue: 0),
                DPad("right", dPadValue: 2),
                DPad("down",  dPadValue: 4),
                DPad("left",  dPadValue: 6),
                DigitalButton("triangle", 5, bit: 4),
                DigitalButton("circle",   5, bit: 5),
                DigitalButton("cross",    5, bit: 6),
                DigitalButton("square",   5, bit: 7),
                DigitalButton("select",           6, bit: 0),
                DigitalButton("leftStickButton",  6, bit: 1),
                DigitalButton("rightStickButton", 6, bit: 2),
                DigitalButton("start",            6, bit: 3),
                DigitalButton("l2",               6, bit: 4),
                DigitalButton("r2",               6, bit: 5),
                DigitalButton("l1",               6, bit: 6),
                DigitalButton("r1",               6, bit: 7),
                PsButton("ps", 7),
                AnalogButton("l2Analog", 8),
                AnalogButton("r2Analog", 9),
                AnalogButton("upAnalog", 10),
                AnalogButton("rightAnalog", 11),
                AnalogButton("downAnalog", 12),
                AnalogButton("leftAnalog", 13),
                AnalogButton("l1Analog", 14),
                AnalogButton("r1Analog", 15),
                AnalogButton("triangleAnalog", 16),
                AnalogButton("circleAnalog", 17),
                AnalogButton("crossAnalog", 18),
                AnalogButton("squareAnalog", 19)
            };
        }

        public IReadOnlyList<Ps3Control> Controls => _controls;

        private Ps3Control Analog(string name, int index, string description = null)
        {
            return new Ps3AnalogControl(new Ps3ControlInfo(_controllerId, new ControlInfo(name, description)), index);
        }

        private Ps3Control AnalogButton(string name, int index, string description = null)
        {
            return new Ps3AnalogButtonControl(new Ps3ControlInfo(_controllerId, new ControlInfo(name, description)), index);
        }

        private Ps3Control DigitalButton(string name, int index, int bit, string description = null)
        {
            return new Ps3DigitalButtonControl(new Ps3ControlInfo(_controllerId, new ControlInfo(name, description)), index, bit);
        }

        private Ps3Control PsButton(string name, int index, string description = null)
        {
            return new Ps3PsButtonControl(new Ps3ControlInfo(_controllerId, new ControlInfo(name, description)), index);
        }

        private Ps3Control DPad(string name, int dPadValue, string description = null)
        {
            return new Ps3DPadControl(new Ps3ControlInfo(_controllerId, new ControlInfo(name, description)), dPadValue);
        }
    }
}
