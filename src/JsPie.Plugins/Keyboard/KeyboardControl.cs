using JsPie.Core.Util;

namespace JsPie.Plugins.Keyboard
{
    public class KeyboardControl
    {
        public KeyboardControlInfo KeyboardControlInfo { get; }
        public bool KeyState { get; set; }

        public KeyboardControl(KeyboardControlInfo keyboardControlInfo)
        {
            KeyboardControlInfo = Guard.NotNull(keyboardControlInfo, nameof(keyboardControlInfo));
        }
    }
}
