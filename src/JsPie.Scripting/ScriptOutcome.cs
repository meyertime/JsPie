using JsPie.Core.Util;
using System.Collections.Generic;

namespace JsPie.Scripting
{
    public class ScriptOutcome
    {
        public bool WasSuccessful { get; }
        public IEnumerable<ScriptObservation> Observations;

        public ScriptOutcome(bool wasSuccessful, IEnumerable<ScriptObservation> observations = null)
        {
            WasSuccessful = wasSuccessful;
            Observations = observations;
        }

        public ScriptResult<T> WithValue<T>(Maybe<T> value)
        {
            return new ScriptResult<T>(value, WasSuccessful, Observations);
        }

        public ScriptResult<T> WithValue<T>(T value)
        {
            return WithValue(value.ToMaybe());
        }

        public ScriptResult<T> WithNoValue<T>()
        {
            return WithValue(Maybe<T>.NoValue);
        }


        public static ScriptOutcome Success(IEnumerable<ScriptObservation> observations)
        {
            return new ScriptOutcome(true, observations);
        }

        public static ScriptOutcome Success(params ScriptObservation[] observations)
        {
            return new ScriptOutcome(true, observations);
        }

        public static ScriptOutcome Failure(IEnumerable<ScriptObservation> observations)
        {
            return new ScriptOutcome(false, observations);
        }

        public static ScriptOutcome Failure(params ScriptObservation[] observations)
        {
            return new ScriptOutcome(false, observations);
        }
    }
}
