using JsPie.Core.Util;

namespace JsPie.Plugins.Ps3
{
    public class Ps3UsbDeviceInfo
    {
        public string DevicePath { get; }

        public Ps3UsbDeviceInfo(string devicePath)
        {
            DevicePath = Guard.NotNullOrEmpty(devicePath, nameof(devicePath));
        }
    }
}
