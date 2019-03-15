using JsPie.Core;
using JsPie.Core.Util;

namespace JsPie.Plugins.XInput
{
    public class XInputControlInfo
    {
        public ControlInfo ControlInfo { get; }
        public ControlId ControlId { get; }

        public XInputControlInfo(ControllerId controllerId, ControlInfo controlInfo)
        {
            Guard.NotNull(controllerId, nameof(controllerId));
            ControlInfo = Guard.NotNull(controlInfo, nameof(controlInfo));
            ControlId = new ControlId(controllerId, controlInfo.Name);
        }
    }
}
