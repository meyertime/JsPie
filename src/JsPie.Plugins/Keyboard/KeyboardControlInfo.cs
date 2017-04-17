using JsPie.Core;
using JsPie.Core.Util;

namespace JsPie.Plugins.Keyboard
{
    public class KeyboardControlInfo
    {
        public ControlInfo ControlInfo { get; }
        public ControlId ControlId { get; }
        public int KeyCode { get; }

        public KeyboardControlInfo(ControllerId controllerId, ControlInfo controlInfo, int keyCode)
        {
            Guard.NotNull(controllerId, nameof(controllerId));
            ControlInfo = Guard.NotNull(controlInfo, nameof(controlInfo));
            ControlId = new ControlId(controllerId, controlInfo.Name);
            KeyCode = keyCode;
        }
    }
}
