using JsPie.Core;
using System.Collections.Generic;

namespace JsPie.Scripting
{
    public interface IScriptOutput
    {
        IEnumerable<ControlEvent> ControlEvents { get; }
    }
}
