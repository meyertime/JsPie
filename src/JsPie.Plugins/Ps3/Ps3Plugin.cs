using System;
using System.Collections.Generic;
using JsPie.Core;
using System.Linq;
using System.Threading.Tasks;

namespace JsPie.Plugins.Ps3
{
    public class Ps3Plugin : IInputPlugin, IDisposable
    {
        private static readonly ControllerId ControllerId = new ControllerId("ps3");

        private readonly Ps3ControlSet _controlSet;
        private readonly ControllerInfo _controllerInfo;

        private readonly Ps3UsbDeviceInterface _device;
        private readonly Task _pollTask;

        private bool _isDisposed;
        
        public Ps3Plugin()
        {
            _controlSet = new Ps3ControlSet(ControllerId);
            _controllerInfo = new ControllerInfo(ControllerId.Name, 1, "PlayStation3 controller", _controlSet.Controls.Select(c => c.Ps3ControlInfo.ControlInfo));
            
            // TODO: Multiple controllers and detect when controllers are connected / disconnected
            var enumerator = new Ps3UsbDeviceEnumerator();
            var devices = enumerator.GetDevices();
            if (devices.Count == 0)
            {
                Console.WriteLine("No PS3 controller detected.");
                return;
            }
            _device = new Ps3UsbDeviceInterface(devices[0]);

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
                var data = _device.Read();

                if (_isDisposed)
                    break;

                var events = (List<ControlEvent>)null;

                foreach (var control in _controlSet.Controls)
                {
                    var @event = control.UpdateValue(data);
                    if (@event != null)
                    {
                        if (events == null)
                            events = new List<ControlEvent>();

                        events.Add(@event);
                    }
                }

                if (events != null)
                {
                    var handler = ControlEvents;
                    if (handler != null)
                        handler(this, events);
                }
            }
        }
    }
}
