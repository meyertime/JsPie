using System;
using System.Collections.Generic;
using JsPie.Core;
using System.Linq;
using static JsPie.Plugins.Keyboard.KeyboardApi;

namespace JsPie.Plugins.Keyboard
{
    public class KeyboardPlugin : IInputPlugin, IOutputPlugin, IDisposable
    {
        private static ControllerId ControllerId;
        private static List<KeyboardControlInfo> Controls;

        private static ControllerInfo ControllerInfo;

        static KeyboardPlugin()
        {
            ControllerId = new ControllerId("keyboard");

            Controls = new List<KeyboardControlInfo>
            {
                Key("backSpace", 8),
                Key("tab",  9),
                Key("enter", 13),
                Key("shift", 16),
                Key("ctrl", 17),
                Key("alt", 18),
                Key("pause", 19),
                Key("capsLock", 20),
                Key("esc", 27),
                Key("space", 32),
                Key("pageUp", 33),
                Key("pageDown", 34),
                Key("end", 35),
                Key("home", 36),
                Key("left", 37),
                Key("up", 38),
                Key("right", 39),
                Key("down", 40),
                Key("insert", 45),
                Key("delete", 46),

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
                Key("z", 90),

                Key("lwin", 91),
                Key("rwin", 92),
                Key("select", 93),
                Key("numpad0", 96),
                Key("numpad1", 97),
                Key("numpad2", 98),
                Key("numpad3", 99),
                Key("numpad4", 100),
                Key("numpad5", 101),
                Key("numpad6", 102),
                Key("numpad7", 103),
                Key("numpad8", 104),
                Key("numpad9", 105),
                Key("ultiply", 106),
                Key("add", 107),
                Key("subtract", 109),
                Key("decimal", 110),
                Key("divide", 111),
                Key("f1", 112),
                Key("f2", 113),
                Key("f3", 114),
                Key("f4", 115),
                Key("f5", 116),
                Key("f6", 117),
                Key("f7", 118),
                Key("f8", 119),
                Key("f9", 120),
                Key("f10", 121),
                Key("f11", 122),
                Key("f12", 123),
                Key("numLock", 144),
                Key("scrollLock", 145),
                Key("lshift", 160),
                Key("rshift", 161),
                Key("lctrl", 162),
                Key("rctrl", 163),
                Key("lalt", 164),
                Key("ralt", 165),
                Key("semicolon", 186),
                Key("equals", 187),
                Key("comma", 188),
                Key("dash", 189),
                Key("period", 190),
                Key("forwardSlash", 191),
                Key("tilde", 192),
                Key("openBracket", 219),
                Key("backSlash", 220),
                Key("closeBraket", 221),
                Key("apostrophe", 222),
                Key("fn", 255)
            };

            for (int i = 0; i <= 255; i++)
            {
                if (!Controls.Any(c => c.KeyCode == i))
                {
                    Controls.Add(Key($"keycode${i}", i));
                }
            }

            ControllerInfo = new ControllerInfo(ControllerId.Name, 1, "The keyboard.", Controls.Select(c => c.ControlInfo));
        }

        private static KeyboardControlInfo Key(string name, int keyCode, string description = null)
        {
            return new KeyboardControlInfo(ControllerId, new ControlInfo(name, description), keyCode);
        }


        private readonly KeyboardControl[] _controlsByKeyCode;
        private readonly Dictionary<string, KeyboardControl> _controlsByName;
        private readonly uint _mark;
        private readonly KeyboardHook _hook;
        private readonly bool[] _keyState;

        public KeyboardPlugin()
        {
            _controlsByKeyCode = new KeyboardControl[Controls.Max(c => c.KeyCode) + 1];
            _controlsByName = new Dictionary<string, KeyboardControl>();
            foreach (var controlInfo in Controls)
            {
                var control = new KeyboardControl(controlInfo);
                _controlsByKeyCode[controlInfo.KeyCode] = control;
                _controlsByName[controlInfo.ControlId.Name] = control;
            }

            _mark = (uint)new Random().Next(0, int.MaxValue);
            _hook = new KeyboardHook(_mark);
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
            foreach (var @event in events)
            {
                if (@event.ControlId.ControllerId.Name != ControllerId.Name)
                    continue;

                KeyboardControl control;
                if (!_controlsByName.TryGetValue(@event.ControlId.Name, out control))
                    continue;

                var keyCode = control.KeyboardControlInfo.KeyCode;
                var keyState = @event.Value != 0;
                control.KeyState = keyState;
                keybd_event((byte)keyCode, 0, keyState ? 0 : KEYEVENTF_KEYUP, _mark);
            }
        }

        private void OnKeyboardHookEvent(uint keyCode, bool value)
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
