using System;

namespace JsPie.Core.Util
{
    public static class Guard
    {
        public static void Ensure(bool condition, string paramName, string message)
        {
            if (!condition)
                throw new ArgumentException(message, paramName);
        }

        public static T NotNull<T>(T value, string paramName, string message = null)
            where T : class
        {
            if (ReferenceEquals(value, null))
                throw (message == null)
                    ? new ArgumentNullException(paramName)
                    : new ArgumentNullException(paramName, message);

            return value;
        }

        public static string NotNullOrEmpty(string value, string paramName, string message = null)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException(message ?? "Argument cannot be null or empty.", paramName);

            return value;
        }
    }
}
