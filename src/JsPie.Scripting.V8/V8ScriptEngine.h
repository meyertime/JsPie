#pragma once

namespace JsPie { namespace Scripting { namespace V8 {

	public ref class V8ScriptEngine : public IScriptEngine
	{
		static V8ScriptEngine();

	public:
		V8ScriptEngine(IScriptEnvironment^ environment);
		~V8ScriptEngine();

		virtual ScriptOutcome^ Initialize();
		virtual ScriptResult<IScriptOutput^>^ Run(IScriptInput ^input);

	private:
		void OnControlEvent(Object^ sender, ControlEvent^ e);

		static v8::Platform* _pPlatform;

		bool _isInitialized;
		IScriptEnvironment^ _oEnvironment;
		ArrayBufferAllocator* _pArrayBufferAllocator;
		V8ScriptEngineData* _pData;
		v8::Isolate* _pIsolate;
		System::Collections::Generic::List<ControlEvent^>^ _oOutputEvents;
	};

} } }
