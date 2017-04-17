#pragma once

namespace JsPie { namespace Scripting { namespace V8 {

	class ConsoleCallbacks : Util
	{
	public:
		v8::Local<v8::ObjectTemplate> CreateTemplate(v8::Isolate* pIsolate);

	private:
		static void Trace(const v8::FunctionCallbackInfo<v8::Value>& args);
		static void Debug(const v8::FunctionCallbackInfo<v8::Value>& args);
		static void Log(const v8::FunctionCallbackInfo<v8::Value>& args);
		static void Info(const v8::FunctionCallbackInfo<v8::Value>& args);
		static void Warn(const v8::FunctionCallbackInfo<v8::Value>& args);
		static void Error(const v8::FunctionCallbackInfo<v8::Value>& args);

		static void WriteConsole(ScriptSeverity severity, const v8::FunctionCallbackInfo<v8::Value>& args);
	};

} } }
