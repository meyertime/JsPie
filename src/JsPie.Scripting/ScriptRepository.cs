using JsPie.Core.Util;
using System.IO;

namespace JsPie.Scripting
{
    public class ScriptRepository : IScriptRepository
    {
        private readonly ScriptResource _mainScript;

        public ScriptRepository(ScriptEngineSettings settings)
        {
            Guard.NotNull(settings, nameof(settings));

            _mainScript = ReadScript(settings.MainScriptPath, Path.GetFileName(settings.MainScriptPath));
        }

        public ScriptResource GetMainScript()
        {
            return _mainScript;
        }

        private ScriptResource ReadScript(string scriptPath, string scriptName)
        {
            using (var file = File.OpenRead(scriptPath))
            {
                var buffer = new byte[(int)file.Length];
                file.Read(buffer, 0, buffer.Length);

                return new ScriptResource(scriptPath, scriptName, buffer);
            }
        }
    }
}
