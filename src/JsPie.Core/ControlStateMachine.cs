using JsPie.Core.Util;
using System.Collections.Generic;

namespace JsPie.Core
{
    public class ControlStateMachine : IControlStateMachine
    {
        private readonly Dictionary<ControlId, ControlState> _controls;

        public ControlStateMachine()
        {
            _controls = new Dictionary<ControlId, ControlState>();
        }

        public event ControlEventHandler OutputEvent;

        public void ApplyInputEvent(ControlEvent e)
        {
            Guard.NotNull(e, nameof(e));

            ControlState state;
            if (!_controls.TryGetValue(e.ControlId, out state))
            {
                state = new ControlState();
                _controls.Add(e.ControlId, state);
            }

            state.InputValue = e.Value;
            state.OutputValue = e.Value;
        }

        public float GetInputValue(ControlId controlId)
        {
            Guard.NotNull(controlId, nameof(controlId));

            ControlState state;
            if (!_controls.TryGetValue(controlId, out state))
                return 0;

            return state.InputValue;
        }

        public float GetOutputValue(ControlId controlId)
        {
            Guard.NotNull(controlId, nameof(controlId));

            ControlState state;
            if (!_controls.TryGetValue(controlId, out state))
                return 0;

            return state.OutputValue;
        }

        public void SetOutputValue(ControlId controlId, float value)
        {
            Guard.NotNull(controlId, nameof(controlId));

            ControlState state;
            if (!_controls.TryGetValue(controlId, out state))
            {
                state = new ControlState();
                _controls.Add(controlId, state);
            }

            if (state.OutputValue == value)
                return;

            state.OutputValue = value;
            RaiseOutputEvent(controlId, value);
        }

        private void RaiseOutputEvent(ControlId controlId, float value)
        {
            var handler = OutputEvent;
            if (handler != null)
                handler(this, new ControlEvent(controlId, value));
        }


        private class ControlState
        {
            public float InputValue { get; set; }
            public float OutputValue { get; set; }
        }
    }
}
