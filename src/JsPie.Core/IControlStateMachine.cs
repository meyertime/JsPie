namespace JsPie.Core
{
    public interface IControlStateMachine
    {
        event ControlEventHandler OutputEvent;

        void ApplyInputEvent(ControlEvent e);

        float GetInputValue(ControlId controlId);
        float GetOutputValue(ControlId controlId);
        void SetOutputValue(ControlId controlId, float value);
    }
}
