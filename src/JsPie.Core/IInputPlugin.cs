using System.Collections.Generic;

namespace JsPie.Core
{
    public interface IInputPlugin
    {
        IEnumerable<ControllerInfo> GetControllers();

        event ControlEventHandler ControlEvent;
        event ControlEventsHandler ControlEvents;
    }
}
