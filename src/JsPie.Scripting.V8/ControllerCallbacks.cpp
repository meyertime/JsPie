#include "stdafx.h"

using namespace System;
using namespace System::Linq;
using namespace System::Runtime::InteropServices;
using namespace JsPie::Core::Util;

namespace JsPie { namespace Scripting { namespace V8 {

	ControllerCallbacks::ControllerCallbacks(ControllerInfo^ inputController, ControllerInfo^ outputController)
		: ControllerCallbacks(inputController, outputController, -1)
	{ }

	ControllerCallbacks::ControllerCallbacks(ControllerInfo^ inputController, ControllerInfo^ outputController, int index)
	{
		_hInputControllerInfo = (inputController == nullptr) ? NULL : (void*)(IntPtr)GCHandle::Alloc(inputController);
		_hOutputControllerInfo = (outputController == nullptr) ? NULL : (void*)(IntPtr)GCHandle::Alloc(outputController);

		auto controllerId = gcnew ControllerId((inputController ? inputController : outputController)->Name, (index < 0) ? 0 : index);

		auto controlNames = Enumerable::ToList((inputController == nullptr) 
			? (outputController == nullptr)
				? Enumerable::Empty<String^>()
				: outputController->Controls->Keys
			: (outputController == nullptr)
				? inputController->Controls->Keys
				: Enumerable::Union(inputController->Controls->Keys, outputController->Controls->Keys)
		);

		auto controlCount = controlNames->Count;
		_pControlDatas = new ControlData[controlCount + 1];
		int i = 0;
		for each (auto controlName in controlNames)
		{
			_pControlDatas[i].hControlId = (void*)(IntPtr)GCHandle::Alloc(gcnew ControlId(controllerId, controlName));
			_pControlDatas[i].bSettable = (outputController != nullptr) ? outputController->Controls->ContainsKey(controlName) : false;
			i++;
		}
		_pControlDatas[controlCount].hControlId = NULL;

		if (index < 0)
		{
			auto inputCount = inputController ? inputController->Count : 0;
			auto outputCount = outputController ? outputController->Count : 0;
			auto controllerCount = (inputCount > outputCount) ? inputCount : outputCount;

			_pControllers = new ControllerCallbacks*[controllerCount + 1];
			for (int i = 0; i < controllerCount; i++)
			{
				_pControllers[i] = new ControllerCallbacks((i < inputCount) ? inputController : nullptr, (i < outputCount) ? outputController : nullptr, i);
			}
			_pControllers[controllerCount] = NULL;
		}
		else 
		{
			_pControllers = NULL;
		}
	}

	ControllerCallbacks::~ControllerCallbacks()
	{
		((GCHandle)(IntPtr)_hInputControllerInfo).Free();
		((GCHandle)(IntPtr)_hOutputControllerInfo).Free();

		auto phControls = _pControlDatas;
		if (phControls != NULL)
		{
			while (true)
			{
				auto controlData = *phControls++;
				if (controlData.hControlId == NULL)
					break;

				((GCHandle)(IntPtr)controlData.hControlId).Free();
			}

			delete _pControlDatas;
		}

		auto ppControllers = _pControllers;
		if (ppControllers != NULL)
		{
			while (true)
			{
				auto pController = *ppControllers++;
				if (pController == NULL)
					break;

				delete pController;
			}

			delete _pControllers;
		}
	}	

	v8::Local<v8::ObjectTemplate> ControllerCallbacks::CreateTemplate(v8::Isolate* pIsolate)
	{
		v8::EscapableHandleScope handle_scope(pIsolate);

		auto controller = v8::ObjectTemplate::New(pIsolate);

		auto phControls = _pControlDatas;
		int i = 0;
		while (true)
		{
			auto controlData = *phControls++;
			if (controlData.hControlId == NULL)
				break;

			auto controlId = (ControlId^)((GCHandle)(IntPtr)controlData.hControlId).Target;

			auto name = Util::ToV8String(pIsolate, controlId->Name);
			auto data = PointerToV8Value(pIsolate, controlData.hControlId);

			controller->SetAccessor(name, GetProperty, controlData.bSettable ? SetProperty : NULL, data);

			i++;
		}

		auto ppControllers = _pControllers;
		if (ppControllers != NULL)
		{
			i = 0;
			while (true)
			{
				auto pController = *ppControllers++;
				if (pController == NULL)
					break;

				controller->Set(v8::Integer::New(pIsolate, i)->ToString(), pController->CreateTemplate(pIsolate), v8::PropertyAttribute::ReadOnly);

				i++;
			}

			controller->Set(v8::String::NewFromUtf8(pIsolate, "length"), v8::Integer::New(pIsolate, i), v8::PropertyAttribute::ReadOnly);
		}

		return handle_scope.Escape(controller);
	}

	ControllerInfo^ ControllerCallbacks::GetInputControllerInfo()
	{
		if (_hInputControllerInfo == 0)
			return nullptr;

		return (ControllerInfo^)((GCHandle)(IntPtr)_hInputControllerInfo).Target;
	}

	ControllerInfo^ ControllerCallbacks::GetOutputControllerInfo()
	{
		if (_hOutputControllerInfo == 0)
			return nullptr;

		return (ControllerInfo^)((GCHandle)(IntPtr)_hOutputControllerInfo).Target;
	}

	void ControllerCallbacks::GetProperty(v8::Local<v8::String> property, const v8::PropertyCallbackInfo<v8::Value>& info)
	{
		auto value = info.This()->GetHiddenValue(property);
		if (!value.IsEmpty())
		{
			info.GetReturnValue().Set(value);
			return;
		}
		
		auto pIsolate = info.GetIsolate();
		auto data = info.Data();

		v8::HandleScope handle_scope(pIsolate);

		auto control = v8::Local<v8::Object>::Cast(v8::NumberObject::New(pIsolate, 0));

		// Override Number members:
		control->Set(v8::String::NewFromUtf8(pIsolate, "toExponential"), v8::Function::New(pIsolate, Value_ToExponential, data));
		control->Set(v8::String::NewFromUtf8(pIsolate, "toFixed"), v8::Function::New(pIsolate, Value_ToFixed, data));
		control->Set(v8::String::NewFromUtf8(pIsolate, "toLocaleString"), v8::Function::New(pIsolate, Value_ToLocaleString, data));
		control->Set(v8::String::NewFromUtf8(pIsolate, "toString"), v8::Function::New(pIsolate, Value_ToString, data));
		control->Set(v8::String::NewFromUtf8(pIsolate, "toPrecision"), v8::Function::New(pIsolate, Value_ToPrecision, data));
		control->Set(v8::String::NewFromUtf8(pIsolate, "valueOf"), v8::Function::New(pIsolate, Value_ValueOf, data));

		// Custom properties:
		control->SetAccessor(v8::String::NewFromUtf8(pIsolate, "input"), Value_Input_Get, NULL, data, v8::AccessControl::DEFAULT, v8::PropertyAttribute::ReadOnly);
		control->SetAccessor(v8::String::NewFromUtf8(pIsolate, "output"), Value_Output_Get, Value_Output_Set, data);
		control->SetAccessor(v8::String::NewFromUtf8(pIsolate, "value"), Value_Output_Get, Value_Output_Set, data); // Value is an alias for output

		info.This()->SetHiddenValue(property, control);

		info.GetReturnValue().Set(control);
	}

	void ControllerCallbacks::SetProperty(v8::Local<v8::String> property, v8::Local<v8::Value> value, const v8::PropertyCallbackInfo<void>& info)
	{
		auto pIsolate = info.GetIsolate();
		auto pData = GetData(pIsolate);
		auto oControlState = pData->GetEnvironment()->ControlState;
		auto oControlId = (ControlId^)((GCHandle)(IntPtr)V8ValueToPointer(pIsolate, info.Data())).Target;

		v8::HandleScope handle_scope(pIsolate);

		float outputValue;
		if (value->IsNumber() || value->IsNumberObject())
		{
			outputValue = (float)value->NumberValue();
		}
		else if (value->IsBoolean() || value->IsBooleanObject())
		{
			outputValue = value->BooleanValue() ? (float)1 : 0;
		}
		else if (value->IsNull() || value->IsUndefined())
		{
			return;
		}
		else
		{
			pIsolate->ThrowException(v8::Exception::TypeError(v8::String::NewFromUtf8(pIsolate, "When setting a value to a control, the value must be a number, true, false, null, or undefined.", v8::NewStringType::kNormal).ToLocalChecked()));
		}

		oControlState->SetOutputValue(oControlId, outputValue);
	}

	void ControllerCallbacks::Value_Input_Get(v8::Local<v8::String> property, const v8::PropertyCallbackInfo<v8::Value>& info)
	{
		auto pIsolate = info.GetIsolate();
		auto pData = GetData(pIsolate);
		auto oControlState = pData->GetEnvironment()->ControlState;
		auto oControlId = (ControlId^)((GCHandle)(IntPtr)V8ValueToPointer(pIsolate, info.Data())).Target;

		v8::HandleScope handle_scope(pIsolate);

		auto value = oControlState->GetInputValue(oControlId);

		info.GetReturnValue().Set(value);
	}

	void ControllerCallbacks::Value_Output_Get(v8::Local<v8::String> property, const v8::PropertyCallbackInfo<v8::Value>& info)
	{
		auto pIsolate = info.GetIsolate();
		auto pData = GetData(pIsolate);
		auto oControlState = pData->GetEnvironment()->ControlState;
		auto oControlId = (ControlId^)((GCHandle)(IntPtr)V8ValueToPointer(pIsolate, info.Data())).Target;

		v8::HandleScope handle_scope(pIsolate);

		auto value = oControlState->GetOutputValue(oControlId);

		info.GetReturnValue().Set(value);
	}

	void ControllerCallbacks::Value_Output_Set(v8::Local<v8::String> property, v8::Local<v8::Value> value, const v8::PropertyCallbackInfo<void>& info)
	{
		auto pIsolate = info.GetIsolate();
		auto pData = GetData(pIsolate);
		auto oControlState = pData->GetEnvironment()->ControlState;
		auto oControlId = (ControlId^)((GCHandle)(IntPtr)V8ValueToPointer(pIsolate, info.Data())).Target;

		v8::HandleScope handle_scope(pIsolate);

		float outputValue;
		if (value->IsNumber() || value->IsNumberObject())
		{
			outputValue = (float)value->NumberValue();
		}
		else if (value->IsBoolean() || value->IsBooleanObject())
		{
			outputValue = value->BooleanValue() ? (float)1 : 0;
		}
		else if (value->IsNull() || value->IsUndefined())
		{
			return;
		}
		else
		{
			pIsolate->ThrowException(v8::Exception::TypeError(v8::String::NewFromUtf8(pIsolate, "When setting a control value, the value must be a number, true, false, null, or undefined.", v8::NewStringType::kNormal).ToLocalChecked()));
		}

		oControlState->SetOutputValue(oControlId, outputValue);
	}

	void ControllerCallbacks::Value_ToExponential(const v8::FunctionCallbackInfo<v8::Value>& args)
	{
		ForwardValueCall(args, "toExponential");
	}

	void ControllerCallbacks::Value_ToFixed(const v8::FunctionCallbackInfo<v8::Value>& args)
	{
		ForwardValueCall(args, "toFixed");
	}

	void ControllerCallbacks::Value_ToLocaleString(const v8::FunctionCallbackInfo<v8::Value>& args)
	{
		ForwardValueCall(args, "toLocaleString");
	}

	void ControllerCallbacks::Value_ToString(const v8::FunctionCallbackInfo<v8::Value>& args)
	{
		ForwardValueCall(args, "toString");
	}

	void ControllerCallbacks::Value_ToPrecision(const v8::FunctionCallbackInfo<v8::Value>& args)
	{
		ForwardValueCall(args, "toPrecision");
	}

	void ControllerCallbacks::Value_ValueOf(const v8::FunctionCallbackInfo<v8::Value>& args)
	{
		auto pIsolate = args.GetIsolate();
		auto pData = GetData(pIsolate);
		auto oControlState = pData->GetEnvironment()->ControlState;
		auto oControlId = (ControlId^)((GCHandle)(IntPtr)V8ValueToPointer(pIsolate, args.Data())).Target;

		v8::HandleScope handle_scope(pIsolate);

		auto value = oControlState->GetOutputValue(oControlId);

		args.GetReturnValue().Set(value);
	}

	void ControllerCallbacks::ForwardValueCall(const v8::FunctionCallbackInfo<v8::Value>& args, const char* name)
	{
		auto pIsolate = args.GetIsolate();
		auto pData = GetData(pIsolate);
		auto oControlState = pData->GetEnvironment()->ControlState;
		auto oControlId = (ControlId^)((GCHandle)(IntPtr)V8ValueToPointer(pIsolate, args.Data())).Target;

		v8::HandleScope handle_scope(pIsolate);

		auto value = oControlState->GetOutputValue(oControlId);
		auto number = v8::Local<v8::Object>::Cast(v8::NumberObject::New(pIsolate, value));
		auto func = v8::Local<v8::Function>::Cast(number->Get(v8::String::NewFromUtf8(pIsolate, name, v8::NewStringType::kNormal).ToLocalChecked()));
		auto result = ForwardFunctionCall(number, args, func);

		args.GetReturnValue().Set(result);
	}

} } }