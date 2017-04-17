using System;
using System.Collections.Generic;
using JsPie.Core;
using System.Linq;

namespace JsPie.Plugins.Keyboard
{
    public class KeyboardPlugin : IInputPlugin, IOutputPlugin, IDisposable
    {
        private static ControllerId ControllerId = new ControllerId("keyboard");
        private static KeyboardControlInfo[] Controls = new[]
        {
            Key("zero", 48),
            Key("one", 49),
            Key("two", 50),
            Key("three", 51),
            Key("four", 52),
            Key("five", 53),
            Key("six", 54),
            Key("seven", 55),
            Key("eight", 56),
            Key("nine", 57),

            Key("a", 65),
            Key("b", 66),
            Key("c", 67),
            Key("d", 68),
            Key("e", 69),
            Key("f", 70),
            Key("g", 71),
            Key("h", 72),
            Key("i", 73),
            Key("j", 74),
            Key("k", 75),
            Key("l", 76),
            Key("m", 77),
            Key("n", 78),
            Key("o", 79),
            Key("p", 80),
            Key("q", 81),
            Key("r", 82),
            Key("s", 83),
            Key("t", 84),
            Key("u", 85),
            Key("v", 86),
            Key("w", 87),
            Key("x", 88),
            Key("y", 89),
            Key("z", 90)
        };

        private static ControllerInfo ControllerInfo = new ControllerInfo(ControllerId.Name, 1, "The keyboard.", Controls.Select(c => c.ControlInfo));

        private static KeyboardControlInfo Key(string name, int keyCode, string description = null)
        {
            return new KeyboardControlInfo(ControllerId, new ControlInfo(name, description), keyCode);
        }


        private readonly KeyboardControl[] _controlsByKeyCode;
        private readonly KeyboardHook _hook;
        private readonly bool[] _keyState;

        public KeyboardPlugin()
        {
            _controlsByKeyCode = new KeyboardControl[Controls.Max(c => c.KeyCode) + 1];
            foreach (var control in Controls)
            {
                _controlsByKeyCode[control.KeyCode] = new KeyboardControl(control);
            }

            _hook = new KeyboardHook();
            _keyState = new bool[Controls.Max(c => c.KeyCode) + 1];

            _hook.KeyboardHookEvent += OnKeyboardHookEvent;
        }

        public void Dispose()
        {
            _hook.Dispose();
        }

        public IEnumerable<ControllerInfo> GetControllers()
        {
            yield return ControllerInfo;
        }

        public event ControlEventHandler ControlEvent;
        public event ControlEventsHandler ControlEvents;

        public void ProcessEvents(IEnumerable<ControlEvent> events)
        {
            throw new NotImplementedException();
        }

        private void OnKeyboardHookEvent(int keyCode, bool value)
        {
            if (keyCode >= _controlsByKeyCode.Length)
                return;

            var control = _controlsByKeyCode[keyCode];
            if (control == null)
                return;

            var lastValue = control.KeyState;
            if (value == lastValue)
                return;

            control.KeyState = value;

            var @event = new ControlEvent(control.KeyboardControlInfo.ControlId, value ? 1f : 0f);

            var handler = ControlEvent;
            if (handler != null)
            {
                handler(this, @event);
            }
        }
    }
}
