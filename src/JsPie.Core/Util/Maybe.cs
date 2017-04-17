using System;

namespace JsPie.Core.Util
{
    public static class Maybe
    {
        public static Maybe<T> ToMaybe<T>(this T value)
        {
            return new Maybe<T>(value);
        }

        public static Maybe<T> ToMaybe<T>(this T? value)
            where T : struct
        {
            return value.HasValue
                ? new Maybe<T>(value.Value)
                : default(Maybe<T>);
        }

        public static T? ToNullable<T>(this Maybe<T> value)
            where T : struct
        {
            return value.HasValue
                ? (T?)value.Value
                : null;
        }
    }

    public struct Maybe<T>
    {
        public static readonly Maybe<T> NoValue = default(Maybe<T>);

        private readonly bool _hasValue;
        private readonly T _value;
        private readonly Func<Maybe<T>> _computation;        

        public Maybe(T value)
        {
            _value = value;
            _hasValue = !IsNull(value);
            _computation = null;
        }

        public Maybe(Func<Maybe<T>> computation)
        {
            Guard.NotNull(computation, nameof(computation));

            _value = default(T);
            _hasValue = false;

            var memo = default(Maybe<T>);
            var computed = false;
            _computation = () =>
            {
                if (computed)
                    return memo;

                memo = computation();
                computed = true;

                return memo;
            };
        }

        public bool HasValue
        {
            get
            {
                if (_computation != null)
                    return _computation().HasValue;

                return _hasValue;
            }
        }

        public T Value
        {
            get
            {
                if (_computation != null)
                    return _computation().Value;

                if (!_hasValue)
                    throw new InvalidOperationException("No value can be computed.");

                return _value;
            }
        }

        public T GetValueOrDefault(T defaultValue = default(T))
        {
            if (_computation != null)
                return _computation().GetValueOrDefault(defaultValue);

            return _hasValue
                ? _value
                : defaultValue;
        }


        private static bool IsNull(T value)
        {
            return !typeof(T).IsValueType && ReferenceEquals(null, value);
        }
    }
}
