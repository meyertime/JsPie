using JsPie.Core;
using JsPie.Core.Util;
using System;

namespace JsPie.Scripting
{
    public class ScriptEnvironment : IScriptEnvironment
    {
        public IScriptRepository Repository { get; }
        public IScriptConsole Console { get; }
        public ControllerDirectory ControllerDirectory { get; }
        public IControlStateMachine ControlState { get; }        

        public ScriptEnvironment(IServiceProvider serviceProvider)
        {
            Guard.NotNull(serviceProvider, nameof(serviceProvider));

            Repository = serviceProvider.GetRequiredService<IScriptRepository>();
            Console = serviceProvider.GetRequiredService<IScriptConsole>();
            ControllerDirectory = serviceProvider.GetRequiredService<ControllerDirectory>();
            ControlState = serviceProvider.GetRequiredService<IControlStateMachine>();
        }
    }
}
