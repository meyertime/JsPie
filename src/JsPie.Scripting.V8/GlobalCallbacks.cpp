#include "stdafx.h"

using namespace System::Linq;

namespace JsPie { namespace Scripting { namespace V8 {

	GlobalCallbacks::GlobalCallbacks(IScriptEnvironment^ environment)
	{
		_pConsoleCallbacks = new ConsoleCallbacks();
		
		auto controllerIds = Enumerable::ToList(Enumerable::Union(
			environment->ControllerDirectory->InputControllers->Keys, 
			environment->ControllerDirectory->OutputControllers->Keys));

		auto count = controllerIds->Count;
		_pControllerCallbacks = new ControllerCallbacks*[count + 1];
		for (auto i = 0; i < count; i++)
		{
			auto controllerId = controllerIds[i];

			ControllerInfo^ inputController;
			if (!environment->ControllerDirectory->InputControllers->TryGetValue(controllerId, inputController))
				inputController = nullptr;

			ControllerInfo^ outputController;
			if (!environment->ControllerDirectory->OutputControllers->TryGetValue(controllerId, outputController))
				outputController = nullptr;

			_pControllerCallbacks[i] = new ControllerCallbacks(inputController, outputController);
		}
		_pControllerCallbacks[count] = NULL;
	}

	GlobalCallbacks::~GlobalCallbacks()
	{
		delete _pConsoleCallbacks;
		
		auto ppController = _pControllerCallbacks;
		while (true)
		{
			auto pController = *ppController++;
			if (pController == NULL)
				break;

			delete pController;
		}

		delete _pControllerCallbacks;
	}

	v8::Local<v8::ObjectTemplate> GlobalCallbacks::CreateTemplate(v8::Isolate* pIsolate)
	{
		v8::EscapableHandleScope handle_scope(pIsolate);

		auto global = v8::ObjectTemplate::New(pIsolate);

		auto ppController = _pControllerCallbacks;
		while (true)
		{
			auto pController = *ppController++;
			if (pController == NULL)
				break;

			auto oControllerInfo = pController->GetInputControllerInfo();
			if (oControllerInfo == nullptr)
				oControllerInfo = pController->GetOutputControllerInfo();

			global->Set(ToV8String(pIsolate, oControllerInfo->Name), pController->CreateTemplate(pIsolate));
		}

		global->Set(v8::String::NewFromUtf8(pIsolate, "console"), _pConsoleCallbacks->CreateTemplate(pIsolate));

		return handle_scope.Escape(global);
	}

} } }