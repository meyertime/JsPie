#include "stdafx.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace JsPie { namespace Scripting { namespace V8 {

	v8::Local<v8::ObjectTemplate> ConsoleCallbacks::CreateTemplate(v8::Isolate* pIsolate)
	{
		v8::EscapableHandleScope handle_scope(pIsolate);

		auto console = v8::ObjectTemplate::New(pIsolate);
		console->Set(v8::String::NewFromUtf8(pIsolate, "trace"), v8::FunctionTemplate::New(pIsolate, Trace));
		console->Set(v8::String::NewFromUtf8(pIsolate, "debug"), v8::FunctionTemplate::New(pIsolate, Debug));
		console->Set(v8::String::NewFromUtf8(pIsolate, "log"), v8::FunctionTemplate::New(pIsolate, Log));
		console->Set(v8::String::NewFromUtf8(pIsolate, "info"), v8::FunctionTemplate::New(pIsolate, Info));
		console->Set(v8::String::NewFromUtf8(pIsolate, "warn"), v8::FunctionTemplate::New(pIsolate, Warn));
		console->Set(v8::String::NewFromUtf8(pIsolate, "error"), v8::FunctionTemplate::New(pIsolate, Error));

		return handle_scope.Escape(console);
	}

	void ConsoleCallbacks::Trace(const v8::FunctionCallbackInfo<v8::Value>& args)
	{
		WriteConsole(ScriptSeverity::Trace, args);
	}

	void ConsoleCallbacks::Debug(const v8::FunctionCallbackInfo<v8::Value>& args)
	{
		WriteConsole(ScriptSeverity::Debug, args);
	}

	void ConsoleCallbacks::Log(const v8::FunctionCallbackInfo<v8::Value>& args)
	{
		WriteConsole(ScriptSeverity::Log, args);
	}

	void ConsoleCallbacks::Info(const v8::FunctionCallbackInfo<v8::Value>& args)
	{
		WriteConsole(ScriptSeverity::Info, args);
	}

	void ConsoleCallbacks::Warn(const v8::FunctionCallbackInfo<v8::Value>& args)
	{
		WriteConsole(ScriptSeverity::Warning, args);
	}

	void ConsoleCallbacks::Error(const v8::FunctionCallbackInfo<v8::Value>& args)
	{
		WriteConsole(ScriptSeverity::Error, args);
	}

	void ConsoleCallbacks::WriteConsole(ScriptSeverity severity, const v8::FunctionCallbackInfo<v8::Value>& args)
	{
		// TODO: If severity is less than threshold, return.

		if (args.Length() < 1)
			return;

		auto pIsolate = args.GetIsolate();
		v8::HandleScope handle_scope(pIsolate);
		auto pData = GetData(pIsolate);
		auto oEnvironment = pData->GetEnvironment();

		// TODO: If argument is function, call it to get the message

		auto message = ToSystemString(pIsolate, args[0]);
		if (message == nullptr)
			return;

		auto source = (System::String^)nullptr; // TODO: Get source / stack trace			

		ScriptConsoleExtensions::Write(oEnvironment->Console, (ScriptSeverity)severity, source, message);
	}

} } }