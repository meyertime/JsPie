#include "stdafx.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace JsPie { namespace Scripting { namespace V8 {

	V8ScriptEngineData::V8ScriptEngineData(IScriptEnvironment^ environment)
	{
		_hEnvironment = (void*)(IntPtr)GCHandle::Alloc(environment);
		_pGlobalCallbacks = new GlobalCallbacks(environment);
	}

	V8ScriptEngineData::~V8ScriptEngineData()
	{
		if (_hEnvironment)
		{
			GCHandle handle = (GCHandle)(IntPtr)_hEnvironment;
			handle.Free();
			_hEnvironment = NULL;
		}

		delete _pGlobalCallbacks;
	}

	IScriptEnvironment^ V8ScriptEngineData::GetEnvironment()
	{
		return (IScriptEnvironment^)((GCHandle)(IntPtr)_hEnvironment).Target;
	}

	v8::Local<v8::ObjectTemplate> V8ScriptEngineData::CreateGlobalTemplate(v8::Isolate* pIsolate)
	{
		return _pGlobalCallbacks->CreateTemplate(pIsolate);
	}

} } }