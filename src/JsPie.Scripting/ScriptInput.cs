using System.Collections.Generic;
using JsPie.Core;
using JsPie.Core.Util;

namespace JsPie.Scripting
{
    public class ScriptInput : IScriptInput
    {
        private static readonly IReadOnlyList<ControlEvent> EmptyControlEvents = new ControlEvent[0];

        public ControlEvent ControlEvent { get; }
        public IReadOnlyList<ControlEvent> ControlEvents { get; }

        public ScriptInput(ControlEvent controlEvent)
        {
            ControlEvent = controlEvent;
            ControlEvents = EmptyControlEvents;
        }

        public ScriptInput(IReadOnlyList<ControlEvent> controlEvents)
        {
            ControlEvent = null;
            ControlEvents = Guard.NotNull(controlEvents, nameof(controlEvents));
        }
    }
}
