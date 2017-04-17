using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using static JsPie.Plugins.Ps3.Ps3Api;

namespace JsPie.Plugins.Ps3
{
    public class Ps3UsbDeviceEnumerator
    {
        private const string Ps3VidPid = "vid_054c&pid_0268";

        public IReadOnlyList<Ps3UsbDeviceInfo> GetDevices()
        {
            var hidGuid = /*Guid.Parse("{7EB949FC-3BED-4809-9D5C-AADC563F45B1}"); //*/Guid.Empty;
            var deviceInfoSet = INVALID_HANDLE_VALUE;
            var deviceInterfaceData = new SP_DEVICE_INTERFACE_DATA();
            var deviceInterfaceDetailData = new SP_DEVICE_INTERFACE_DETAIL_DATA();
            var deviceInfoData = new SP_DEVINFO_DATA();
            var buffer = IntPtr.Zero;

            var results = new List<Ps3UsbDeviceInfo>();

            try
            {
                HidD_GetHidGuid(out hidGuid);

                deviceInfoSet = SetupDiGetClassDevs(hidGuid, null, IntPtr.Zero, DIGCF_DEVICEINTERFACE | DIGCF_PRESENT);
                CheckError(deviceInfoSet != INVALID_HANDLE_VALUE);

                var memberIndex = (uint)0;
                while (true)
                {
                    if (!SetupDiEnumDeviceInterfaces(deviceInfoSet, null, hidGuid, memberIndex, deviceInterfaceData))
                    {
                        var lastError = Marshal.GetLastWin32Error();
                        if (lastError == ERROR_NO_MORE_ITEMS)
                            break;

                        throw new Win32Exception(lastError);
                    }

                    uint requiredSize;
                    if (!SetupDiGetDeviceInterfaceDetail(deviceInfoSet, deviceInterfaceData, IntPtr.Zero, 0, out requiredSize, null))
                    {
                        var lastError = Marshal.GetLastWin32Error();
                        if (lastError != ERROR_INSUFFICIENT_BUFFER)
                            throw new Win32Exception(lastError);
                    }

                    buffer = Marshal.AllocHGlobal((int)requiredSize);
                    Marshal.StructureToPtr(deviceInterfaceDetailData, buffer, false);

                    CheckError(SetupDiGetDeviceInterfaceDetail(deviceInfoSet, deviceInterfaceData, buffer, requiredSize, out requiredSize, deviceInfoData));

                    var str = buffer + sizeof(uint);
                    var devicePath = Marshal.PtrToStringAuto(str);

                    if (devicePath.Contains(Ps3VidPid))
                    {
                        results.Add(new Ps3UsbDeviceInfo(
                            devicePath: devicePath
                        ));
                    }

                    memberIndex++;
                }
            }
            finally
            {
                if (deviceInfoSet != INVALID_HANDLE_VALUE)
                    SetupDiDestroyDeviceInfoList(deviceInfoSet);

                if (buffer != IntPtr.Zero)
                    Marshal.FreeHGlobal(buffer);
            }

            return results;
        }
    }
}
