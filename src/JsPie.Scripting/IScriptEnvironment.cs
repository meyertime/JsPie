using JsPie.Core;

namespace JsPie.Scripting
{
    public interface IScriptEnvironment
    {
        IScriptRepository Repository { get; }
        IScriptConsole Console { get; }
        ControllerDirectory ControllerDirectory { get; }
        IControlStateMachine ControlState { get; }
    }
}
