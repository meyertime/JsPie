#pragma once

namespace JsPie { namespace Scripting { namespace V8 {

	class V8ScriptEngineData
	{
	public:
		V8ScriptEngineData(IScriptEnvironment^ environment);
		~V8ScriptEngineData();

		IScriptEnvironment^ GetEnvironment();
		v8::Local<v8::ObjectTemplate> CreateGlobalTemplate(v8::Isolate* pIsolate);

		v8::Eternal<v8::Context> hContext;
		v8::Eternal<v8::Script> hMainScript;

	private:
		void* _hEnvironment;
		GlobalCallbacks* _pGlobalCallbacks;
	};

} } }
