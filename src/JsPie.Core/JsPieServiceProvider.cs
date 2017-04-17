using JsPie.Core.Util;
using System;
using System.Collections.Generic;

namespace JsPie.Core
{
    public class JsPieServiceProvider : IJsPieServiceProvider
    {
        private Dictionary<Type, Func<object>> _activators;

        public JsPieServiceProvider()
        {
            _activators = new Dictionary<Type, Func<object>>();
        }

        public void Register<T>(Func<T> activator)
        {
            Guard.NotNull(activator, nameof(activator));

            _activators[typeof(T)] = () => activator();
        }

        public object GetService(Type serviceType)
        {
            return GetService(serviceType, false);
        }

        public T GetService<T>()
        {
            return CastService<T>(GetService(typeof(T), false));
        }

        public object GetRequiredService(Type serviceType)
        {
            return GetService(serviceType, true);
        }

        public T GetRequiredService<T>()
        {
            return CastService<T>(GetService(typeof(T), true));
        }

        private object GetService(Type serviceType, bool isRequired)
        {
            Guard.NotNull(serviceType, nameof(serviceType));

            Func<object> activator;
            if (!_activators.TryGetValue(serviceType, out activator))
            {
                if (isRequired)
                    throw new InvalidOperationException($"Requested service of type {serviceType.FullName}, but none was registered with the service provider.");

                return null;
            }

            var service = activator();

            if (service == null)
            {
                if (isRequired)
                    throw new InvalidOperationException($"Requested service of type {serviceType.FullName}, but the registered activator for that type returned null.");

                return null;
            }

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
