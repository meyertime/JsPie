using JsPie.Core.Util;

namespace JsPie.Scripting
{
    public class ScriptEngineSettings
    {
        public string MainScriptPath { get; }

        public ScriptEngineSettings(string mainScriptPath)
        {
            MainScriptPath = Guard.NotNullOrEmpty(mainScriptPath, nameof(mainScriptPath));
        }
    }
}
