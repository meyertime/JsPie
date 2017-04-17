#include "stdafx.h"

#include <vcclr.h>

using namespace System;
using namespace JsPie::Core::Util;

namespace JsPie { namespace Scripting { namespace V8 {

	V8ScriptEngineData* Util::GetData(v8::Isolate* pIsolate)
	{
		return (V8ScriptEngineData*)pIsolate->GetData(0);
	}

	v8::Local<v8::String> Util::ToV8String(v8::Isolate* pIsolate, System::String^ s)
	{
		// Because only the returned handle is created, we can just stay in the existing handle scope.

		Guard::NotNull(s, "s", nullptr);

		pin_ptr<const wchar_t> p = PtrToStringChars(s);
		return v8::String::NewFromTwoByte(pIsolate, (const uint16_t*)p, v8::NewStringType::kNormal, s->Length).ToLocalChecked();
	}

	v8::Local<v8::String> Util::ToV8String(v8::Isolate* pIsolate, array<unsigned char>^ a)
	{
		// Because only the returned handle is created, we can just stay in the existing handle scope.

		Guard::NotNull(a, "a", nullptr);

		pin_ptr<unsigned char> p = &a[0];
		return v8::String::NewFromUtf8(pIsolate, (char*)p, v8::NewStringType::kNormal, a->Length).ToLocalChecked();
	}

	v8::Local<v8::Value> Util::ToV8StringOrNull(v8::Isolate* pIsolate, System::String^ s)
	{
		if (s == nullptr)
			return v8::Null(pIsolate);

		return ToV8String(pIsolate, s);
	}

	v8::Local<v8::Value> Util::ToV8StringOrNull(v8::Isolate* pIsolate, array<unsigned char>^ a)
	{
		if (a == nullptr)
			return v8::Null(pIsolate);

		return ToV8String(pIsolate, a);
	}

	System::String^ Util::ToSystemString(v8::Isolate* pIsolate, v8::Local<v8::String> s)
	{
		int length = s->Length();
		auto result = gcnew System::String('\0', length);
		pin_ptr<const wchar_t> p = PtrToStringChars(result);
		s->Write((uint16_t*)p, 0, length);
		return result;
	}

	System::String^ Util::ToSystemString(v8::Isolate* pIsolate, v8::Local<v8::Value> v)
	{
		if (v->IsNull() || v->IsUndefined())
			return nullptr;

		v8::HandleScope handle_scope(pIsolate);
		v8::Local<v8::String> s;
		if (v->IsString())
		{
			s = v8::Local<v8::String>::Cast(v);
		}
		else
		{
			s = v->ToString(pIsolate);
		}

		return ToSystemString(pIsolate, s);
	}

	v8::Local<v8::Value> Util::PointerToV8Value(v8::Isolate* pIsolate, void* p)
	{
		return v8::External::New(pIsolate, p);
	}

	void* Util::V8ValueToPointer(v8::Isolate* pIsolate, v8::Local<v8::Value> v)
	{
		return v8::Local<v8::External>::Cast(v)->Value();
	}

	v8::Local<v8::Value> Util::ForwardFunctionCall(const v8::FunctionCallbackInfo<v8::Value>& info, v8::Local<v8::Function> func)
	{
		return ForwardFunctionCall(info.This(), info, func);
	}

	v8::Local<v8::Value> Util::ForwardFunctionCall(v8::Local<v8::Value> recv, const v8::FunctionCallbackInfo<v8::Value>& info, v8::Local<v8::Function> func)
	{
		auto argc = info.Length();
		if (argc == 0)
		{
			return info.IsConstructCall()
				? func->CallAsConstructor(0, NULL)
				: func->CallAsFunction(recv, 0, NULL);
		}

		auto pIsolate = info.GetIsolate();
		v8::EscapableHandleScope handle_scope(pIsolate);

		if (argc <= 8)
		{
			v8::Local<v8::Value> argv[8];
			for (auto i = 0; i < argc; i++)
			{
				argv[i] = info[i];
			}

			auto result = info.IsConstructCall()
				? func->CallAsConstructor(argc, argv)
				: func->CallAsFunction(recv, argc, argv);

			return handle_scope.Escape(result);
		}

		auto argv = new v8::Local<v8::Value>[argc];
		for (auto i = 0; i < argc; i++)
		{
			argv[i] = info[i];
		}

		auto result = info.IsConstructCall()
			? func->CallAsConstructor(argc, argv)
			: func->CallAsFunction(recv, argc, argv);

		delete argv;

		return handle_scope.Escape(result);
	}

} } }