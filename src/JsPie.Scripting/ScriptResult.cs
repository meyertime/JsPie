using JsPie.Core.Util;
using System.Collections.Generic;

namespace JsPie.Scripting
{
    public class ScriptResult<T> : ScriptOutcome
    {
        private readonly Maybe<T> _value;

        public ScriptResult(Maybe<T> value, bool wasSuccessful, IEnumerable<ScriptObservation> observations = null)
            : base(wasSuccessful, observations)
        {
            _value = value;
        }

        public bool HasValue => _value.HasValue;
        public T Value => _value.Value;

        public Maybe<T> ToMaybe()
        {
            return _value;
        }
    }
}
