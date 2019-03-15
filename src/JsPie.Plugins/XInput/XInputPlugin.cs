using System;
using System.Collections.Generic;
using JsPie.Core;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace JsPie.Plugins.XInput
{
    public class XInputPlugin : IInputPlugin, IDisposable
    {
        private static readonly ControllerId ControllerId = new ControllerId("xinput");

        private readonly XInputControlSet _controlSet;
        private readonly ControllerInfo _controllerInfo;

        private readonly Task _pollTask;

        private uint _lastResult;
        private uint _lastPacketNumber;
        private bool _isDisposed;
        
        public XInputPlugin()
        {
            _controlSet = new XInputControlSet(ControllerId);
            _controllerInfo = new ControllerInfo(ControllerId.Name, 1, "XInput gamepad", _controlSet.Controls.Select(c => c.XInputControlInfo.ControlInfo));

            // TODO: Multiple controllers and detect when controllers are connected / disconnected

            _lastResult = uint.MaxValue;

            _pollTask = Task.Factory.StartNew(Poll, TaskCreationOptions.LongRunning);
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            _pollTask.Wait();
        }

        public event ControlEventHandler ControlEvent;
        public event ControlEventsHandler ControlEvents;
                
        public IEnumerable<ControllerInfo> GetControllers()
        {
            yield return _controllerInfo;
        }

        private void Poll()
        {
            while (!_isDisposed)
            {
                var state = new XInputApi.XINPUT_STATE();
                var result = XInputApi.XInputGetState(0, state);

                var lastResult = _lastResult;
                _lastResult = result;

                if (_isDisposed)
                    break;

                if (result != XInputApi.ERROR_SUCCESS)
                {
                    if (result != lastResult)
                    {
                        if (result == XInputApi.ERROR_DEVICE_NOT_CONNECTED)
                        {
                            Console.WriteLine("No XInput gamepad connected.");
                        }
                        else
                        {
                            Console.WriteLine("Unexpected error result from XInputGetState: " + result);
                        }
                    }

                    Thread.Sleep(1000);
                    continue;
                }

                if (result != lastResult)
                {
                    Console.WriteLine("XInput gamepad connected.");

                    // Wait a second after connection to give JsPie a chance to initialize so that the queue does not back up
                    Thread.Sleep(1000);
                    continue;
                }

                if (_lastPacketNumber == state.dwPacketNumber)
                {
                    Thread.Sleep(5);
                    continue;
                }

                _lastPacketNumber = state.dwPacketNumber;

                var events = (List<ControlEvent>)null;
                var gamepad = state.Gamepad;

                foreach (var button in _controlSet.ButtonControls)
                {
                    ProcessEvent(button.UpdateValue(gamepad.wButtons), ref events);
                }

                ProcessEvent(_controlSet.LeftTriggerControl.UpdateValue(gamepad.bLeftTrigger), ref events);
                ProcessEvent(_controlSet.RightTriggerControl.UpdateValue(gamepad.bRightTrigger), ref events);
                ProcessEvent(_controlSet.LeftThumbXControl.UpdateValue(gamepad.sThumbLX), ref events);
                ProcessEvent(_controlSet.LeftThumbYControl.UpdateValue(gamepad.sThumbLY), ref events);
                ProcessEvent(_controlSet.RightThumbXControl.UpdateValue(gamepad.sThumbRX), ref events);
                ProcessEvent(_controlSet.RightThumbYControl.UpdateValue(gamepad.sThumbRY), ref events);

                if (events != null)
                {
                    ControlEvents?.Invoke(this, events);
                }

                Thread.Sleep(5);
            }
        }

        private void ProcessEvent(ControlEvent @event, ref List<ControlEvent> events)
        {
            if (@event == null) return;

            if (events == null)
            {
                events = new List<ControlEvent>();
            }

            events.Add(@event);
        }
    }
}
