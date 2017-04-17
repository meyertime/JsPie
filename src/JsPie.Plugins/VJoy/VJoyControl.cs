using JsPie.Core;
using JsPie.Core.Util;
using System.Linq.Expressions;
using System.Reflection;
using vJoyInterfaceWrap;

namespace JsPie.Plugins.VJoy
{
    public abstract class VJoyControl
    {
        public delegate T FieldSelector<T>(vJoy.JoystickState state);
        public delegate void SetValueDelegate(ref vJoy.JoystickState state, uint value);
        public delegate uint GetValueDelegate(ref vJoy.JoystickState state);

        public ControlInfo ControlInfo { get; }
        public ControlId ControlId { get; }

        public VJoyControl(ControlInfo controlInfo, ControlId controlId)
        {
            ControlInfo = Guard.NotNull(controlInfo, nameof(controlInfo));
            ControlId = Guard.NotNull(controlId, nameof(controlId));
        }

        public abstract void SetValue(ref vJoy.JoystickState state, float value);

        protected static SetValueDelegate MakeSetValueDelegate<T>(Expression<FieldSelector<T>> selector)
        {
            var field = (FieldInfo)(((MemberExpression)selector.Body).Member);

            var stateParam = Expression.Parameter(typeof(vJoy.JoystickState).MakeByRefType(), "state");
            var valueParam = Expression.Parameter(typeof(uint), "value");
            var valueExp = (Expression)valueParam;
            if (field.FieldType != typeof(uint))
            {
                valueExp = Expression.Convert(valueExp, field.FieldType);
            }
            var body = Expression.Assign(Expression.Field(stateParam, field), valueExp);
            var lambda = Expression.Lambda<SetValueDelegate>(body, stateParam, valueParam);

            return lambda.Compile();
        }

        protected static GetValueDelegate MakeGetValueDelegate<T>(Expression<FieldSelector<T>> selector)
        {
            var field = (FieldInfo)(((MemberExpression)selector.Body).Member);

            var stateParam = Expression.Parameter(typeof(vJoy.JoystickState).MakeByRefType(), "state");
            var valueExp = (Expression)Expression.Field(stateParam, field);
            if (field.FieldType != typeof(uint))
            {
                valueExp = Expression.Convert(valueExp, typeof(uint));
            }
            var lambda = Expression.Lambda<GetValueDelegate>(valueExp, stateParam);

            return lambda.Compile();
        }
    }
}
