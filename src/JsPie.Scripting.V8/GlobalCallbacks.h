#pragma once

namespace JsPie { namespace Scripting { namespace V8 {

	class GlobalCallbacks : Util
	{
	public:
		GlobalCallbacks(IScriptEnvironment^ environment);
		~GlobalCallbacks();

		v8::Local<v8::ObjectTemplate> CreateTemplate(v8::Isolate* pIsolate);

	private:
		ConsoleCallbacks* _pConsoleCallbacks;

		ControllerCallbacks** _pControllerCallbacks;
	};

} } }
