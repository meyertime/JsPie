using System.Collections.Generic;
using JsPie.Core;
using System.Linq;

namespace JsPie.Scripting
{
    public class ScriptOutput : IScriptOutput
    {
        public IEnumerable<ControlEvent> ControlEvents { get; }

        public ScriptOutput(IEnumerable<ControlEvent> controlEvents)
        {
            ControlEvents = controlEvents ?? Enumerable.Empty<ControlEvent>();
        }
    }
}
