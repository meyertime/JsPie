#pragma once

namespace JsPie { namespace Scripting { namespace V8 {

	class ArrayBufferAllocator : public v8::ArrayBuffer::Allocator {
	public:
		virtual void* Allocate(size_t length);
		virtual void* AllocateUninitialized(size_t length);
		virtual void Free(void* data, size_t);
	};

} } }
