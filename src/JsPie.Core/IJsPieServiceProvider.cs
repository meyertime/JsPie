using System;

namespace JsPie.Core
{
    public interface IJsPieServiceProvider : IServiceProvider
    {
        T GetService<T>();
        T GetRequiredService<T>();

        object GetRequiredService(Type serviceType);
    }
}
