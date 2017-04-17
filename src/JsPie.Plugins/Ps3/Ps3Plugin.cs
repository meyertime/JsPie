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
        private static readonly Ps3ControlSet ControlSet = new Ps3ControlSet(ControllerId);
        private static ControllerInfo ControllerInfo = new ControllerInfo(ControllerId.Name, 1, "PlayStation3 controller.", ControlSet.Controls.Select(c => c.Ps3ControlInfo.ControlInfo));

        private readonly Ps3UsbDeviceInterface _device;
        private readonly Task _pollTask;

        private bool _isDisposed;
        
        public Ps3Plugin()
        {
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
            yield return ControllerInfo;
        }

        private void Poll()
        {
            while (!_isDisposed)
            {
                var data = _device.Read();

                if (_isDisposed)
                    break;

                var events = (List<ControlEvent>)null;

                foreach (var control in ControlSet.Controls)
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
