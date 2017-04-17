using JsPie.Core;
using JsPie.Core.Util;

namespace JsPie.Plugins.Ps3
{
    public class Ps3ControlInfo
    {
        public ControlInfo ControlInfo { get; }
        public ControlId ControlId { get; }

        public Ps3ControlInfo(ControllerId controllerId, ControlInfo controlInfo)
        {
            Guard.NotNull(controllerId, nameof(controllerId));
            ControlInfo = Guard.NotNull(controlInfo, nameof(controlInfo));
            ControlId = new ControlId(controllerId, controlInfo.Name);
        }
    }
}
