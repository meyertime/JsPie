// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently,
// but are changed infrequently

#pragma once

#include "..\..\external\v8\libplatform.h"
#include "..\..\external\v8\include\v8.h"

namespace JsPie { namespace Scripting { namespace V8 {
	class ArrayBufferAllocator;
	class V8ScriptEngineData;
	class Util;
	class GlobalCallbacks;
	class ConsoleCallbacks;
	class ControllerCallbacks;
	ref class V8ScriptEngine;
} } }

#include "ArrayBufferAllocator.h"
#include "V8ScriptEngineData.h"
#include "Util.h"
#include "GlobalCallbacks.h"
#include "ConsoleCallbacks.h"
#include "ControllerCallbacks.h"
#include "V8ScriptEngine.h"