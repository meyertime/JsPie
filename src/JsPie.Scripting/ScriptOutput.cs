using System.Collections.Generic;
using JsPie.Core;

namespace JsPie.Scripting
{
    public class ScriptOutput : IScriptOutput
    {
        private static readonly IReadOnlyList<ControlEvent> EmptyControlEvents = new ControlEvent[0];

        public IReadOnlyList<ControlEvent> ControlEvents { get; }

        public ScriptOutput(IReadOnlyList<ControlEvent> controlEvents)
        {
            ControlEvents = controlEvents ?? EmptyControlEvents;
        }
    }
}
