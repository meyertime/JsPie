using System;

namespace JsPie.Scripting
{
    public interface IScriptEngine : IDisposable
    {
        ScriptOutcome Initialize();

        ScriptResult<IScriptOutput> Run(IScriptInput input);
    }
}
