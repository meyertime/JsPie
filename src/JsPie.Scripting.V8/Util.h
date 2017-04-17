#pragma once

namespace JsPie { namespace Scripting { namespace V8 {

	class Util
	{
	public:
		static V8ScriptEngineData* GetData(v8::Isolate* pIsolate);

		static v8::Local<v8::String> ToV8String(v8::Isolate* pIsolate, System::String^ s);
		static v8::Local<v8::String> ToV8String(v8::Isolate* pIsolate, array<unsigned char>^ a);
		static v8::Local<v8::Value> ToV8StringOrNull(v8::Isolate* pIsolate, System::String^ s);
		static v8::Local<v8::Value> ToV8StringOrNull(v8::Isolate* pIsolate, array<unsigned char>^ a);
		static System::String^ ToSystemString(v8::Isolate* pIsolate, v8::Local<v8::String> s);
		static System::String^ ToSystemString(v8::Isolate* pIsolate, v8::Local<v8::Value> v);

		static v8::Local<v8::Value> PointerToV8Value(v8::Isolate* pIsolate, void* p);
		static void* V8ValueToPointer(v8::Isolate* pIsolate, v8::Local<v8::Value> v);

		static v8::Local<v8::Value> ForwardFunctionCall(const v8::FunctionCallbackInfo<v8::Value>& info, v8::Local<v8::Function> func);
		static v8::Local<v8::Value> ForwardFunctionCall(v8::Local<v8::Value> recv, const v8::FunctionCallbackInfo<v8::Value>& info, v8::Local<v8::Function> func);
	};

} } }
