using System.Collections.Generic;

namespace JsPie.Core
{
    public interface IOutputPlugin
    {
        IEnumerable<ControllerInfo> GetControllers();

        void ProcessEvents(IEnumerable<ControlEvent> events);
    }
}
