using JsPie.Core.Util;
using System;
using System.Linq;

namespace JsPie.Scripting
{
    public class ScriptConsole : IScriptConsole
    {
        private static readonly SeveritySettings[] _severitySettings;

        static ScriptConsole()
        {
            _severitySettings = new SeveritySettings[((ScriptSeverity[])Enum.GetValues(typeof(ScriptSeverity))).Max(v => (int)v) + 1];

            _severitySettings[(int)ScriptSeverity.Trace] = new SeveritySettings(ConsoleColor.DarkGray, "[TRACE] ");
            _severitySettings[(int)ScriptSeverity.Debug] = new SeveritySettings(ConsoleColor.DarkGray, "[DEBUG] ");
            _severitySettings[(int)ScriptSeverity.Log] = new SeveritySettings(ConsoleColor.Gray, "[LOG] ");
            _severitySettings[(int)ScriptSeverity.Info] = new SeveritySettings(ConsoleColor.White, "[INFO] ");
            _severitySettings[(int)ScriptSeverity.Warning] = new SeveritySettings(ConsoleColor.Yellow, "[WARNING] ");
            _severitySettings[(int)ScriptSeverity.Error] = new SeveritySettings(ConsoleColor.Red, "[ERROR] ");
        }


        private readonly ScriptSeverity _minimumSeverity;

        public ScriptConsole(ScriptSeverity minimumSeverity = ScriptSeverity.Info)
        {
            _minimumSeverity = minimumSeverity;
        }

        public void Write(ScriptObservation observation)
        {
            Guard.NotNull(observation, nameof(observation));

            if (observation.Severity < _minimumSeverity)
                return;

            var severitySettings = ((int)observation.Severity < _severitySettings.Length)
                ? _severitySettings[(int)observation.Severity]
                : default(SeveritySettings);

            if (severitySettings.Color.HasValue)
            {
                var originalColor = Console.ForegroundColor;
                Console.ForegroundColor = severitySettings.Color.Value;
                WriteMessage(severitySettings.Prefix, observation);
                Console.ForegroundColor = originalColor;
            }
            else
            {
                WriteMessage(severitySettings.Prefix, observation);
            }
        }

        private void WriteMessage(string prefix, ScriptObservation observation)
        {
            Console.Write(prefix);
            Console.WriteLine(observation.Message);
            if (observation.Source != null)
            {
                Console.Write("Source: ");
                Console.WriteLine(observation.Source);
            }
        }

        private struct SeveritySettings
        {
            public ConsoleColor? Color { get; }
            public string Prefix { get; }

            public SeveritySettings(ConsoleColor? color, string prefix)
            {
                Color = color;
                Prefix = prefix;
            }
        }
    }
}
