using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace JsPie.Core.Util
{
    public class EquatableByValueBase<T> : IEquatable<T>
        where T : EquatableByValueBase<T>
    {
        private static readonly Func<T, object[]> MemberAccessor;

        static EquatableByValueBase()
        {
            var instanceParam = Expression.Parameter(typeof(T), "instance");

            var members = typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => !p.IsDefined(typeof(IgnoreAttribute)))
                .Where(p => p.CanRead && p.GetGetMethod().IsPublic)
                .Select(p => Expression.Convert(Expression.Property(instanceParam, p), typeof(object)))
                .ToList();
            var array = Expression.NewArrayInit(typeof(object), members);

            MemberAccessor = Expression.Lambda<Func<T, object[]>>(array, instanceParam).Compile();
        }

        public bool Equals(T other)
        {
            return Equals((object)other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;

            if (obj.GetType() != GetType())
                return false;

            var mine = MemberAccessor((T)this);
            var theirs = MemberAccessor((T)obj);

            for (int i = 0; i < mine.Length; i++)
            {
                if (!EqualsInternal(mine[i], theirs[i]))
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            var result = 0;

            var mine = MemberAccessor((T)this);
            foreach (var value in mine)
            {
                var enumerable = value as IEnumerable;
                if (!ReferenceEquals(enumerable, null))
                {
                    var count = 0;
                    foreach (var element in enumerable)
                    {
                        result = result.MixHashCode(element);
                        count++;
                    }
                    result = result.MixHashCode((count == 0) ? -1 : count);
                    continue;
                }

                result = result.MixHashCode(value);
            }

            return result;
        }

        public static bool operator ==(EquatableByValueBase<T> a, EquatableByValueBase<T> b)
        {
            if (ReferenceEquals(a, null))
            {
                return ReferenceEquals(b, null);
            }

            return a.Equals(b);
        }

        public static bool operator !=(EquatableByValueBase<T> a, EquatableByValueBase<T> b)
        {
            return !(a == b);
        }

        private static bool EqualsInternal(object a, object b)
        {
            if (ReferenceEquals(a, null))
            {
                return ReferenceEquals(b, null);
            }

            var enumerableA = a as IEnumerable;
            if (!ReferenceEquals(enumerableA, null))
            {
                var enumerableB = b as IEnumerable;
                if (!ReferenceEquals(enumerableB, null))
                {
                    var enumeratorA = enumerableA.GetEnumerator();
                    var enumeratorB = enumerableB.GetEnumerator();

                    while (true)
                    {
                        var moreA = enumeratorA.MoveNext();
                        var moreB = enumeratorB.MoveNext();

                        if (moreA != moreB)
                            return false;

                        if (!moreA)
                            return true;

                        if (!EqualsInternal(enumeratorA.Current, enumeratorB.Current))
                            return false;
                    }
                }
            }

            return a.Equals(b);
        }
    }
}
