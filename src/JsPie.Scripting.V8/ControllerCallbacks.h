#pragma once

using namespace JsPie::Core;

namespace JsPie { namespace Scripting { namespace V8 {

	class ControllerCallbacks : Util
	{
	public:
		ControllerCallbacks(ControllerInfo^ inputController, ControllerInfo^ outputController);
		ControllerCallbacks(ControllerInfo^ inputController, ControllerInfo^ outputController, int index);
		~ControllerCallbacks();
		v8::Local<v8::ObjectTemplate> CreateTemplate(v8::Isolate* pIsolate);

		ControllerInfo^ GetInputControllerInfo();
		ControllerInfo^ GetOutputControllerInfo();

	private:
		static void GetProperty(v8::Local<v8::String> property, const v8::PropertyCallbackInfo<v8::Value>& info);
		static void SetProperty(v8::Local<v8::String> property, v8::Local<v8::Value> value, const v8::PropertyCallbackInfo<void>& info);

		static void Value_Input_Get(v8::Local<v8::String> property, const v8::PropertyCallbackInfo<v8::Value>& info);
		static void Value_Output_Get(v8::Local<v8::String> property, const v8::PropertyCallbackInfo<v8::Value>& info);
		static void Value_Output_Set(v8::Local<v8::String> property, v8::Local<v8::Value> value, const v8::PropertyCallbackInfo<void>& info);

		static void Value_ToExponential(const v8::FunctionCallbackInfo<v8::Value>& args);
		static void Value_ToFixed(const v8::FunctionCallbackInfo<v8::Value>& args);
		static void Value_ToLocaleString(const v8::FunctionCallbackInfo<v8::Value>& args);
		static void Value_ToString(const v8::FunctionCallbackInfo<v8::Value>& args);
		static void Value_ToPrecision(const v8::FunctionCallbackInfo<v8::Value>& args);	
		static void Value_ValueOf(const v8::FunctionCallbackInfo<v8::Value>& args);

		static void ForwardValueCall(const v8::FunctionCallbackInfo<v8::Value>& args, const char* name);

		struct ControlData
		{
			void* hControlId;
			bool bSettable;
		};

		void* _hInputControllerInfo;
		void* _hOutputControllerInfo;
		ControlData* _pControlDatas;
		ControllerCallbacks** _pControllers;
	};

} } }
