#include "stdafx.h"

#include <stdlib.h>
#include <string.h>

namespace JsPie { namespace Scripting { namespace V8 {

	void* ArrayBufferAllocator::Allocate(size_t length)
	{
		void* data = malloc(length);
		return data == NULL ? data : memset(data, 0, length);
	}

	void* ArrayBufferAllocator::AllocateUninitialized(size_t length)
	{ 
		return malloc(length);
	}

	void ArrayBufferAllocator::Free(void* data, size_t)
	{
		free(data);
	}

} } }