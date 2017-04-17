using JsPie.Core;
using JsPie.Scripting;
using JsPie.Scripting.V8;

namespace JsPie.Runtime
{
    public class DefaultJsPieServiceProvider : JsPieServiceProvider
    {
        public DefaultJsPieServiceProvider()
        {
            Register<IControlStateMachine>(() => new ControlStateMachine());
            Register<IScriptConsole>(() => new ScriptConsole());
            Register<IScriptEngine>(() => new V8ScriptEngine(GetRequiredService<IScriptEnvironment>()));
            Register<IScriptEnvironment>(() => new ScriptEnvironment(this));
            Register<IScriptRepository>(() => new ScriptRepository(GetRequiredService<ScriptEngineSettings>()));
        }
    }
}
