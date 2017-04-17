using JsPie.Core;
using System.Collections.Generic;

namespace JsPie.Scripting
{
    public interface IScriptInput
    {
        ControlEvent ControlEvent { get; }
        IReadOnlyList<ControlEvent> ControlEvents { get; }
    }
}