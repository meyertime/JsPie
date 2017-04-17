using JsPie.Core.Util;
using System;

namespace JsPie.Scripting
{
    public static class ScriptConsoleExtensions
    {
        public static void Write(this IScriptConsole obj, ScriptSeverity severity, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Create(severity, message));
        }

        public static void Write(this IScriptConsole obj, ScriptSeverity severity, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Create(severity, message));
        }

        public static void Write(this IScriptConsole obj, ScriptSeverity severity, Exception exception, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Create(severity, exception, message));
        }

        public static void Write(this IScriptConsole obj, ScriptSeverity severity, Exception exception, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Create(severity, exception, message));
        }

        public static void Write(this IScriptConsole obj, ScriptSeverity severity, string source, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Create(severity, source, message));
        }

        public static void Write(this IScriptConsole obj, ScriptSeverity severity, string source, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Create(severity, source, message));
        }

        public static void Write(this IScriptConsole obj, ScriptSeverity severity, Exception exception, string source, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Create(severity, exception, source, message));
        }

        public static void Write(this IScriptConsole obj, ScriptSeverity severity, Exception exception, string source, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Create(severity, exception, source, message));
        }

        public static void WriteFormat(this IScriptConsole obj, ScriptSeverity severity, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.CreateFormat(severity, format, args));
        }

        public static void WriteFormat(this IScriptConsole obj, ScriptSeverity severity, Exception exception, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.CreateFormat(severity, format, args, exception));
        }

        public static void WriteFormat(this IScriptConsole obj, ScriptSeverity severity, string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.CreateFormat(severity, format, args, source));
        }

        public static void WriteFormat(this IScriptConsole obj, ScriptSeverity severity, Exception exception, string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.CreateFormat(severity, format, args, source));
        }

        public static void Trace(this IScriptConsole obj, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Trace(message));
        }

        public static void Trace(this IScriptConsole obj, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Trace(message));
        }

        public static void Trace(this IScriptConsole obj, Exception exception, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Trace(exception, message));
        }

        public static void Trace(this IScriptConsole obj, Exception exception, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Trace(exception, message));
        }

        public static void Trace(this IScriptConsole obj, string source, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Trace(source, message));
        }

        public static void Trace(this IScriptConsole obj, string source, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Trace(source, message));
        }

        public static void Trace(this IScriptConsole obj, Exception exception, string source, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Trace(exception, source, message));
        }

        public static void Trace(this IScriptConsole obj, Exception exception, string source, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Trace(exception, source, message));
        }

        public static void TraceFormat(this IScriptConsole obj, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.TraceFormat(format, args));
        }

        public static void TraceFormat(this IScriptConsole obj, Exception exception, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.TraceFormat(exception, format, args));
        }

        public static void TraceFormat(this IScriptConsole obj, string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.TraceFormat(source, format, args));
        }

        public static void TraceFormat(this IScriptConsole obj, Exception exception, string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.TraceFormat(exception, source, format, args));
        }

        public static void Debug(this IScriptConsole obj, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Debug(message));
        }

        public static void Debug(this IScriptConsole obj, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Debug(message));
        }

        public static void Debug(this IScriptConsole obj, Exception exception, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Debug(exception, message));
        }

        public static void Debug(this IScriptConsole obj, Exception exception, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Debug(exception, message));
        }

        public static void Debug(this IScriptConsole obj, string source, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Debug(source, message));
        }

        public static void Debug(this IScriptConsole obj, string source, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Debug(source, message));
        }

        public static void Debug(this IScriptConsole obj, Exception exception, string source, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Debug(exception, source, message));
        }

        public static void Debug(this IScriptConsole obj, Exception exception, string source, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Debug(exception, source, message));
        }

        public static void DebugFormat(this IScriptConsole obj, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.DebugFormat(format, args));
        }

        public static void DebugFormat(this IScriptConsole obj, Exception exception, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.DebugFormat(exception, format, args));
        }

        public static void DebugFormat(this IScriptConsole obj, string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.DebugFormat(source, format, args));
        }

        public static void DebugFormat(this IScriptConsole obj, Exception exception, string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.DebugFormat(exception, source, format, args));
        }

        public static void Log(this IScriptConsole obj, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Log(message));
        }

        public static void Log(this IScriptConsole obj, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Log(message));
        }

        public static void Log(this IScriptConsole obj, Exception exception, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Log(exception, message));
        }

        public static void Log(this IScriptConsole obj, Exception exception, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Log(exception, message));
        }

        public static void Log(this IScriptConsole obj, string source, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Log(source, message));
        }

        public static void Log(this IScriptConsole obj, string source, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Log(source, message));
        }

        public static void Log(this IScriptConsole obj, Exception exception, string source, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Log(exception, source, message));
        }

        public static void Log(this IScriptConsole obj, Exception exception, string source, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Log(exception, source, message));
        }

        public static void LogFormat(this IScriptConsole obj, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.LogFormat(format, args));
        }

        public static void LogFormat(this IScriptConsole obj, Exception exception, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.LogFormat(exception, format, args));
        }

        public static void LogFormat(this IScriptConsole obj, string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.LogFormat(source, format, args));
        }

        public static void LogFormat(this IScriptConsole obj, Exception exception, string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.LogFormat(exception, source, format, args));
        }

        public static void Info(this IScriptConsole obj, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Info(message));
        }

        public static void Info(this IScriptConsole obj, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Info(message));
        }

        public static void Info(this IScriptConsole obj, Exception exception, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Info(exception, message));
        }

        public static void Info(this IScriptConsole obj, Exception exception, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Info(exception, message));
        }

        public static void Info(this IScriptConsole obj, string source, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Info(source, message));
        }

        public static void Info(this IScriptConsole obj, string source, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Info(source, message));
        }

        public static void Info(this IScriptConsole obj, Exception exception, string source, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Info(exception, source, message));
        }

        public static void Info(this IScriptConsole obj, Exception exception, string source, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Info(exception, source, message));
        }

        public static void InfoFormat(this IScriptConsole obj, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.InfoFormat(format, args));
        }

        public static void InfoFormat(this IScriptConsole obj, Exception exception, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.InfoFormat(exception, format, args));
        }

        public static void InfoFormat(this IScriptConsole obj, string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.InfoFormat(source, format, args));
        }

        public static void InfoFormat(this IScriptConsole obj, Exception exception, string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.InfoFormat(exception, source, format, args));
        }

        public static void Warning(this IScriptConsole obj, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Warning(message));
        }

        public static void Warning(this IScriptConsole obj, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Warning(message));
        }

        public static void Warning(this IScriptConsole obj, Exception exception, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Warning(exception, message));
        }

        public static void Warning(this IScriptConsole obj, Exception exception, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Warning(exception, message));
        }

        public static void Warning(this IScriptConsole obj, string source, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Warning(source, message));
        }

        public static void Warning(this IScriptConsole obj, string source, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Warning(source, message));
        }

        public static void Warning(this IScriptConsole obj, Exception exception, string source, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Warning(exception, source, message));
        }

        public static void Warning(this IScriptConsole obj, Exception exception, string source, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Warning(exception, source, message));
        }

        public static void WarningFormat(this IScriptConsole obj, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.WarningFormat(format, args));
        }

        public static void WarningFormat(this IScriptConsole obj, Exception exception, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.WarningFormat(exception, format, args));
        }

        public static void WarningFormat(this IScriptConsole obj, string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.WarningFormat(source, format, args));
        }

        public static void WarningFormat(this IScriptConsole obj, Exception exception, string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.WarningFormat(exception, source, format, args));
        }

        public static void Error(this IScriptConsole obj, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Error(message));
        }

        public static void Error(this IScriptConsole obj, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Error(message));
        }

        public static void Error(this IScriptConsole obj, Exception exception, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Error(exception, message));
        }

        public static void Error(this IScriptConsole obj, Exception exception, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Error(exception, message));
        }

        public static void Error(this IScriptConsole obj, string source, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Error(source, message));
        }

        public static void Error(this IScriptConsole obj, string source, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Error(source, message));
        }

        public static void Error(this IScriptConsole obj, Exception exception, string source, object message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Error(exception, source, message));
        }

        public static void Error(this IScriptConsole obj, Exception exception, string source, FormattableString message)
        {
            Guard.NotNull(obj, nameof(obj));

            obj.Write(ScriptObservation.Error(exception, source, message));
        }

        public static void ErrorFormat(this IScriptConsole obj, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.ErrorFormat(format, args));
        }

        public static void ErrorFormat(this IScriptConsole obj, Exception exception, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.ErrorFormat(exception, format, args));
        }

        public static void ErrorFormat(this IScriptConsole obj, string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.ErrorFormat(source, format, args));
        }

        public static void ErrorFormat(this IScriptConsole obj, Exception exception, string source, string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, nameof(format));

            obj.Write(ScriptObservation.ErrorFormat(exception, source, format, args));
        }
    }
}
