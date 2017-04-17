using JsPie.Core.Util;
using System;

namespace JsPie.Scripting
{
    public class ScriptObservation
    {
        public ScriptSeverity Severity { get; }
        public string Message { get; }
        public string Source { get; }
        public Exception Exception { get; }

        public ScriptObservation(ScriptSeverity severity, string message, string source = null, Exception exception = null)
        {
            Guard.NotNullOrEmpty(message, nameof(message));

            Severity = severity;
            Message = message;
            Source = source;
            Exception = exception;
        }
    }
}
