using JsPie.Core.Util;
using System;
using System.Runtime.CompilerServices;

namespace JsPie.Scripting
{
    public class ScriptObservation
    {
        public ScriptSeverity Severity { get; }
        public string Source { get; }
        public Exception Exception { get; }

        private readonly FormattableString _formattableMessage;

        private string _message;

        private ScriptObservation(ScriptSeverity severity, Exception exception, string source, string message)
        {
            Guard.NotNullOrEmpty(message, nameof(message));
            Severity = severity;
            Source = source;
            Exception = exception;

            _message = message;
        }

        private ScriptObservation(ScriptSeverity severity, Exception exception, string source, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));
            Severity = severity;
            Source = source;
            Exception = exception;

            _formattableMessage = message;
        }

        public string Message
        {
            get
            {
                if (_message != null)
                    return _message;

                return _message = _formattableMessage.ToString();
            }
        }

        public static ScriptObservation Create(ScriptSeverity severity, object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(severity, null, null, message.ToString());
        }
        
        public static ScriptObservation Create(ScriptSeverity severity, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(severity, null, null, message);
        }

        public static ScriptObservation Create(ScriptSeverity severity, Exception exception, object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(severity, exception, null, message.ToString());
        }
        
        public static ScriptObservation Create(ScriptSeverity severity, Exception exception, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(severity, exception, null, message);
        }

        public static ScriptObservation Create(ScriptSeverity severity, string source, object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(severity, null, source, message.ToString());
        }
               
        public static ScriptObservation Create(ScriptSeverity severity, string source, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(severity, null, source, message);
        }

        public static ScriptObservation Create(ScriptSeverity severity, Exception exception, string source, object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(severity, exception, source, message.ToString());
        }

        public static ScriptObservation Create(ScriptSeverity severity, Exception exception, string source, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(severity, exception, source, message);
        }

        public static ScriptObservation CreateFormat(ScriptSeverity severity, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(severity, null, null, FormattableStringFactory.Create(format, args));
        }

        public static ScriptObservation CreateFormat(ScriptSeverity severity, Exception exception, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(severity, exception, null, FormattableStringFactory.Create(format, args));
        }

        public static ScriptObservation CreateFormat(ScriptSeverity severity, string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(severity, null, source, FormattableStringFactory.Create(format, args));
        }

        public static ScriptObservation CreateFormat(ScriptSeverity severity, Exception exception, string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(severity, exception, source, FormattableStringFactory.Create(format, args));
        }
        
        public static ScriptObservation Trace(object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Trace, null, null, message.ToString());
        }

        public static ScriptObservation Trace(FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Trace, null, null, message);
        }

        public static ScriptObservation Trace(Exception exception, object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Trace, exception, null, message.ToString());
        }

        public static ScriptObservation Trace(Exception exception, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Trace, exception, null, message);
        }

        public static ScriptObservation Trace(string source, object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Trace, null, source, message.ToString());
        }
        
        public static ScriptObservation Trace(string source, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Trace, null, source, message);
        }

        public static ScriptObservation Trace(Exception exception, string source, object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Trace, exception, source, message.ToString());
        }

        public static ScriptObservation Trace(Exception exception, string source, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Trace, exception, source, message);
        }

        public static ScriptObservation TraceFormat(string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Trace, null, null, FormattableStringFactory.Create(format, args));
        }

        public static ScriptObservation TraceFormat(Exception exception, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Trace, exception, null, FormattableStringFactory.Create(format, args));
        }

        public static ScriptObservation TraceFormat(string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Trace, null, source, FormattableStringFactory.Create(format, args));
        }

        public static ScriptObservation TraceFormat(Exception exception, string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Trace, exception, source, FormattableStringFactory.Create(format, args));
        }
        
        public static ScriptObservation Debug(object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Debug, null, null, message.ToString());
        }

        public static ScriptObservation Debug(FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Debug, null, null, message);
        }

        public static ScriptObservation Debug(Exception exception, object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Debug, exception, null, message.ToString());
        }

        public static ScriptObservation Debug(Exception exception, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Debug, exception, null, message);
        }

        public static ScriptObservation Debug(string source, object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Debug, null, source, message.ToString());
        }

        public static ScriptObservation Debug(string source, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Debug, null, source, message);
        }

        public static ScriptObservation Debug(Exception exception, string source, object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Debug, exception, source, message.ToString());
        }

        public static ScriptObservation Debug(Exception exception, string source, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Debug, exception, source, message);
        }

        public static ScriptObservation DebugFormat(string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Debug, null, null, FormattableStringFactory.Create(format, args));
        }

        public static ScriptObservation DebugFormat(Exception exception, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Debug, exception, null, FormattableStringFactory.Create(format, args));
        }

        public static ScriptObservation DebugFormat(string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Debug, null, source, FormattableStringFactory.Create(format, args));
        }

        public static ScriptObservation DebugFormat(Exception exception, string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Debug, exception, source, FormattableStringFactory.Create(format, args));
        }
                       
        public static ScriptObservation Log(object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Log, null, null, message.ToString());
        }

        public static ScriptObservation Log(FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Log, null, null, message);
        }

        public static ScriptObservation Log(Exception exception, object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Log, exception, null, message.ToString());
        }

        public static ScriptObservation Log(Exception exception, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Log, exception, null, message);
        }

        public static ScriptObservation Log(string source, object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Log, null, source, message.ToString());
        }

        public static ScriptObservation Log(string source, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Log, null, source, message);
        }

        public static ScriptObservation Log(Exception exception, string source, object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Log, exception, source, message.ToString());
        }

        public static ScriptObservation Log(Exception exception, string source, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Log, exception, source, message);
        }

        public static ScriptObservation LogFormat(string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Log, null, null, FormattableStringFactory.Create(format, args));
        }

        public static ScriptObservation LogFormat(Exception exception, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Log, exception, null, FormattableStringFactory.Create(format, args));
        }

        public static ScriptObservation LogFormat(string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Log, null, source, FormattableStringFactory.Create(format, args));
        }

        public static ScriptObservation LogFormat(Exception exception, string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Log, exception, source, FormattableStringFactory.Create(format, args));
        }
        
        public static ScriptObservation Info(object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Info, null, null, message.ToString());
        }

        public static ScriptObservation Info(FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Info, null, null, message);
        }

        public static ScriptObservation Info(Exception exception, object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Info, exception, null, message.ToString());
        }

        public static ScriptObservation Info(Exception exception, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Info, exception, null, message);
        }

        public static ScriptObservation Info(string source, object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Info, null, source, message.ToString());
        }

        public static ScriptObservation Info(string source, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Info, null, source, message);
        }

        public static ScriptObservation Info(Exception exception, string source, object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Info, exception, source, message.ToString());
        }

        public static ScriptObservation Info(Exception exception, string source, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Info, exception, source, message);
        }

        public static ScriptObservation InfoFormat(string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Info, null, null, FormattableStringFactory.Create(format, args));
        }

        public static ScriptObservation InfoFormat(Exception exception, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Info, exception, null, FormattableStringFactory.Create(format, args));
        }

        public static ScriptObservation InfoFormat(string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Info, null, source, FormattableStringFactory.Create(format, args));
        }

        public static ScriptObservation InfoFormat(Exception exception, string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Info, exception, source, FormattableStringFactory.Create(format, args));
        }
        
        public static ScriptObservation Warning(object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Warning, null, null, message.ToString());
        }

        public static ScriptObservation Warning(FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Warning, null, null, message);
        }

        public static ScriptObservation Warning(Exception exception, object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Warning, exception, null, message.ToString());
        }

        public static ScriptObservation Warning(Exception exception, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Warning, exception, null, message);
        }

        public static ScriptObservation Warning(string source, object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Warning, null, source, message.ToString());
        }

        public static ScriptObservation Warning(string source, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Warning, null, source, message);
        }

        public static ScriptObservation Warning(Exception exception, string source, object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Warning, exception, source, message.ToString());
        }

        public static ScriptObservation Warning(Exception exception, string source, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Warning, exception, source, message);
        }

        public static ScriptObservation WarningFormat(string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Warning, null, null, FormattableStringFactory.Create(format, args));
        }

        public static ScriptObservation WarningFormat(Exception exception, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Warning, exception, null, FormattableStringFactory.Create(format, args));
        }

        public static ScriptObservation WarningFormat(string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Warning, null, source, FormattableStringFactory.Create(format, args));
        }

        public static ScriptObservation WarningFormat(Exception exception, string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Warning, exception, source, FormattableStringFactory.Create(format, args));
        }
        
        public static ScriptObservation Error(object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Error, null, null, message.ToString());
        }

        public static ScriptObservation Error(FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Error, null, null, message);
        }

        public static ScriptObservation Error(Exception exception, object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Error, exception, null, message.ToString());
        }

        public static ScriptObservation Error(Exception exception, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Error, exception, null, message);
        }

        public static ScriptObservation Error(string source, object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Error, null, source, message.ToString());
        }

        public static ScriptObservation Error(string source, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Error, null, source, message);
        }

        public static ScriptObservation Error(Exception exception, string source, object message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Error, exception, source, message.ToString());
        }

        public static ScriptObservation Error(Exception exception, string source, FormattableString message)
        {
            Guard.NotNull(message, nameof(message));

            return new ScriptObservation(ScriptSeverity.Error, exception, source, message);
        }

        public static ScriptObservation ErrorFormat(string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Error, null, null, FormattableStringFactory.Create(format, args));
        }

        public static ScriptObservation ErrorFormat(Exception exception, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Error, exception, null, FormattableStringFactory.Create(format, args));
        }

        public static ScriptObservation ErrorFormat(string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Error, null, source, FormattableStringFactory.Create(format, args));
        }

        public static ScriptObservation ErrorFormat(Exception exception, string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            return new ScriptObservation(ScriptSeverity.Error, exception, source, FormattableStringFactory.Create(format, args));
        }
    }
}
