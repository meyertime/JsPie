using JsPie.Core;
using JsPie.Core.Util;
using System.Collections.Generic;
using System.Linq;

namespace JsPie.Plugins.XInput
{
    public class XInputControlSet
    {
        private readonly ControllerId _controllerId;

        public XInputControlSet(ControllerId controllerId)
        {
            _controllerId = Guard.NotNull(controllerId, nameof(controllerId));

            ButtonControls = new[]
            {
                Button("up", XInputApi.XINPUT_GAMEPAD_DPAD_UP),
                Button("down", XInputApi.XINPUT_GAMEPAD_DPAD_DOWN),
                Button("left", XInputApi.XINPUT_GAMEPAD_DPAD_LEFT),
                Button("right", XInputApi.XINPUT_GAMEPAD_DPAD_RIGHT),
                Button("start", XInputApi.XINPUT_GAMEPAD_START),
                Button("back", XInputApi.XINPUT_GAMEPAD_BACK),
                Button("leftThumb", XInputApi.XINPUT_GAMEPAD_LEFT_THUMB),
                Button("rightThumb", XInputApi.XINPUT_GAMEPAD_RIGHT_THUMB),
                Button("leftShoulder", XInputApi.XINPUT_GAMEPAD_LEFT_SHOULDER),
                Button("rightShoulder", XInputApi.XINPUT_GAMEPAD_RIGHT_SHOULDER),
                Button("a", XInputApi.XINPUT_GAMEPAD_A),
                Button("b", XInputApi.XINPUT_GAMEPAD_B),
                Button("x", XInputApi.XINPUT_GAMEPAD_X),
                Button("y", XInputApi.XINPUT_GAMEPAD_Y)
            };

            LeftTriggerControl = Trigger("leftTrigger");
            RightTriggerControl = Trigger("rightTrigger");
            LeftThumbXControl = Thumb("leftThumbX");
            LeftThumbYControl = Thumb("leftThumbY");
            RightThumbXControl = Thumb("rightThumbX");
            RightThumbYControl = Thumb("rightThumbY");
        }

        public IReadOnlyList<XInputButtonControl> ButtonControls { get; private set; }
        public XInputTriggerControl LeftTriggerControl { get; private set; }
        public XInputTriggerControl RightTriggerControl { get; private set; }
        public XInputThumbControl LeftThumbXControl { get; private set; }
        public XInputThumbControl LeftThumbYControl { get; private set; }
        public XInputThumbControl RightThumbXControl { get; private set; }
        public XInputThumbControl RightThumbYControl { get; private set; }

        public IReadOnlyList<XInputControl> Controls { get
            {
                return ButtonControls.Concat(new XInputControl[]
                {
                    LeftTriggerControl,
                    RightTriggerControl,
                    LeftThumbXControl,
                    LeftThumbYControl,
                    RightThumbXControl,
                    RightThumbYControl
                }).ToList();
            }
        }

        private XInputButtonControl Button(string name, ushort buttonMask, string description = null)
        {
            return new XInputButtonControl(new XInputControlInfo(_controllerId, new ControlInfo(name, description)), buttonMask);
        }

        private XInputTriggerControl Trigger(string name, string description = null)
        {
            return new XInputTriggerControl(new XInputControlInfo(_controllerId, new ControlInfo(name, description)));
        }

        private XInputThumbControl Thumb(string name, string description = null)
        {
            return new XInputThumbControl(new XInputControlInfo(_controllerId, new ControlInfo(name, description)));
        }
    }
}
