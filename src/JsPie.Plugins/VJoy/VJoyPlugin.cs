using JsPie.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using vJoyInterfaceWrap;

namespace JsPie.Plugins.VJoy
{
    public class VJoyPlugin : IOutputPlugin, IDisposable
    {
        private static readonly ControllerId ControllerId = new ControllerId("vjoy");

        private readonly VJoyControlSet _controlSet;
        private readonly ControllerInfo _controllerInfo;
        private readonly vJoy _joystick;
        private readonly Task _task;

        private vJoy.JoystickState _state;

        private bool _isAcquired;
        private bool _isDisposed;

        public VJoyPlugin()
        {
            _controlSet = new VJoyControlSet(ControllerId);
            _controllerInfo = new ControllerInfo(ControllerId.Name, 1, "vJoy joystick", _controlSet.Controls.Select(c => c.ControlInfo));

            _joystick = new vJoy();
            
            var state = new vJoy.JoystickState
            {
                bDevice = 1
            };

            foreach (var control in _controlSet.Controls)
            {
                control.SetValue(ref state, 0);
            }

            _state = state;

            _task = Task.Factory.StartNew(Poll, TaskCreationOptions.LongRunning);
        }

        public void Dispose()
        {
            if (!_isDisposed)
                return;

            _isDisposed = true;

            _task.Wait();
        }

        public IEnumerable<ControllerInfo> GetControllers()
        {
            yield return _controllerInfo;
        }

        public void ProcessEvents(IEnumerable<ControlEvent> events)
        {
            var state = _state;

            foreach (var @event in events)
            {
                if (@event.ControlId.ControllerId.Name != ControllerId.Name)
                    continue;

                var control = _controlSet.GetControlByName(@event.ControlId.Name);
                if (control != null)
                {
                    control.SetValue(ref state, @event.Value);
                }
            }

            _state = state;
        }

        private void Poll()
        {
            while (!_isDisposed)
            {
                if (!_isAcquired)
                {
                    if (!InitializeVJoy())
                        Thread.Sleep(1000);
                }
                else
                {
                    if (!FeedVJoy())
                        Thread.Sleep(1000);
                }

                Thread.Sleep(16);
            }

            if (_isAcquired)
                FreeVJoy();
        }

        private bool InitializeVJoy()
        {
            const int id = 1;

            // Get the driver attributes (Vendor ID, Product ID, Version Number)
            if (!_joystick.vJoyEnabled())
            {
                Console.WriteLine("vJoy driver not enabled; failed getting vJoy attributes.");
                return false;
            }

            // Get the state of the requested device
            var status = _joystick.GetVJDStatus(id);
            switch (status)
            {
                case VjdStat.VJD_STAT_OWN:
                    Console.WriteLine($"vJoy device {id} is already owned by this feeder.");
                    break;
                case VjdStat.VJD_STAT_FREE:
                    Console.WriteLine($"vJoy device {id} is free.");
                    break;
                case VjdStat.VJD_STAT_BUSY:
                    Console.WriteLine($"vJoy device {id} is already owned by another feeder.  Failed acquiring device.");
                    return false;
                case VjdStat.VJD_STAT_MISS:
                    Console.WriteLine($"vJoy device {id} is not installed or disabled.  Failed acquiring device.");
                    return false;
                default:
                    Console.WriteLine($"vJoy device {id} general error.  Failed acquiring device.");
                    return false;
            };

            // Test if DLL matches the driver
            uint dllVer = 0;
            uint drvVer = 0;
            bool match = _joystick.DriverMatch(ref dllVer, ref drvVer);
            if (!match)
            {
                Console.WriteLine($"Version of vJoy driver ({drvVer:X}) does not match DLL version ({dllVer:X})");
                return false;
            }

            // Acquire the target
            if ((status == VjdStat.VJD_STAT_OWN) || ((status == VjdStat.VJD_STAT_FREE) && (!_joystick.AcquireVJD(id))))
            {
                Console.WriteLine($"Failed to acquire vJoy device number {id}.");
                return false;
            }

            _isAcquired = true;
            Console.WriteLine($"Successfully acquired vJoy device number {id}.");

            return true;
        }
         
        private bool FeedVJoy()
        {
            const int id = 1;

            var state = _state;
            if (!_joystick.UpdateVJD(id, ref state))
            {
                Console.WriteLine("Feeding vJoy device number {0} failed.", id);
                _isAcquired = false;
                return false;
            }

            return true;
        }

        private bool FreeVJoy()
        {
            if (!_joystick.ResetVJD(1))
                return false;

            _isAcquired = false;
            return true;
        }
    }
}
