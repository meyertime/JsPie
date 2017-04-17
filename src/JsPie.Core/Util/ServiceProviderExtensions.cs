using System;

namespace JsPie.Core.Util
{
    public static class ServiceProviderExtensions
    {
        public static T GetService<T>(this IServiceProvider serviceProvider)
        {
            Guard.NotNull(serviceProvider, nameof(serviceProvider));

            var jsPieServiceProvider = serviceProvider as IJsPieServiceProvider;
            if (jsPieServiceProvider != null)
                return jsPieServiceProvider.GetService<T>();

            var service = serviceProvider.GetService(typeof(T));

            if (ReferenceEquals(null, service))
            {
                if (typeof(T).IsValueType)
                    throw new InvalidOperationException($"Requested service of type {typeof(T).FullName}, which is a value type, but the service provider returned null, which cannot be casted to a value type.");

                return default(T);
            }

            return CastService<T>(service);
        }

        public static T GetRequiredService<T>(this IServiceProvider serviceProvider)
        {
            Guard.NotNull(serviceProvider, nameof(serviceProvider));

            var service = GetRequiredService(serviceProvider, typeof(T));

            return CastService<T>(service);
        }

        public static object GetRequiredService(this IServiceProvider serviceProvider, Type serviceType)
        {
            Guard.NotNull(serviceProvider, nameof(serviceProvider));
            Guard.NotNull(serviceType, nameof(serviceType));

            var jsPieServiceProvider = serviceProvider as IJsPieServiceProvider;
            if (jsPieServiceProvider != null)
                return jsPieServiceProvider.GetRequiredService(serviceType);

            var service = serviceProvider.GetService(serviceType);

            if (ReferenceEquals(service, null))
                throw new InvalidOperationException($"Requested service of type {serviceType.FullName}, but the service provider returned null.");

            return service;
        }

        private static T CastService<T>(object service)
        {
            if (!(service is T))
                throw new InvalidCastException($"Requested service of type {typeof(T).FullName}, but the service provider returned an object of type {service.GetType().FullName}.");

            return (T)service;
        }
    }
}
