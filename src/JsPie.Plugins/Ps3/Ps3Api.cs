using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace JsPie.Plugins.Ps3
{
    public static class Ps3Api
    {
        private const uint FACILITY_HID_ERROR_CODE = 0x11;

        public const uint DIGCF_DEFAULT = 0x00000001;
        public const uint DIGCF_PRESENT = 0x00000002;
        public const uint DIGCF_ALLCLASSES = 0x00000004;
        public const uint DIGCF_PROFILE = 0x00000008;
        public const uint DIGCF_DEVICEINTERFACE = 0x00000010;

        public static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        public const int ERROR_INSUFFICIENT_BUFFER = 0x7A;
        public const int ERROR_NO_MORE_ITEMS = 0x103;

        public const uint GENERIC_READ = 0x80000000;
        public const uint GENERIC_WRITE = 0x40000000;
        public const uint FILE_SHARE_READ = 1;
        public const uint FILE_SHARE_WRITE = 2;
        public const uint OPEN_EXISTING = 3;
        public const uint FILE_FLAG_OVERLAPPED = 0x40000000;

        public const byte USB_DIR_IN = 0x80;
        public const byte USB_TYPE_CLASS = 0x01 << 5;
        public const byte USB_RECIP_INTERFACE = 0x01;
        public const byte HID_REQ_GET_REPORT = 0x01;

        public enum NTSTATUS : uint
        { 
            HIDP_STATUS_SUCCESS                  = ((uint)0x0 << 28) | (FACILITY_HID_ERROR_CODE << 16) | 0,
            HIDP_STATUS_NULL                     = ((uint)0x8 << 28) | (FACILITY_HID_ERROR_CODE << 16) | 1,
            HIDP_STATUS_INVALID_PREPARSED_DATA   = ((uint)0xC << 28) | (FACILITY_HID_ERROR_CODE << 16) | 1,
            HIDP_STATUS_INVALID_REPORT_TYPE      = ((uint)0xC << 28) | (FACILITY_HID_ERROR_CODE << 16) | 2,
            HIDP_STATUS_INVALID_REPORT_LENGTH    = ((uint)0xC << 28) | (FACILITY_HID_ERROR_CODE << 16) | 3,
            HIDP_STATUS_USAGE_NOT_FOUND          = ((uint)0xC << 28) | (FACILITY_HID_ERROR_CODE << 16) | 4,
            HIDP_STATUS_VALUE_OUT_OF_RANGE       = ((uint)0xC << 28) | (FACILITY_HID_ERROR_CODE << 16) | 5,
            HIDP_STATUS_BAD_LOG_PHY_VALUES       = ((uint)0xC << 28) | (FACILITY_HID_ERROR_CODE << 16) | 6,
            HIDP_STATUS_BUFFER_TOO_SMALL         = ((uint)0xC << 28) | (FACILITY_HID_ERROR_CODE << 16) | 7,
            HIDP_STATUS_INTERNAL_ERROR           = ((uint)0xC << 28) | (FACILITY_HID_ERROR_CODE << 16) | 8,
            HIDP_STATUS_I8242_TRANS_UNKNOWN      = ((uint)0xC << 28) | (FACILITY_HID_ERROR_CODE << 16) | 9,
            HIDP_STATUS_INCOMPATIBLE_REPORT_ID   = ((uint)0xC << 28) | (FACILITY_HID_ERROR_CODE << 16) | 0xA,
            HIDP_STATUS_NOT_VALUE_ARRAY          = ((uint)0xC << 28) | (FACILITY_HID_ERROR_CODE << 16) | 0xB,
            HIDP_STATUS_IS_VALUE_ARRAY           = ((uint)0xC << 28) | (FACILITY_HID_ERROR_CODE << 16) | 0xC,
            HIDP_STATUS_DATA_INDEX_NOT_FOUND     = ((uint)0xC << 28) | (FACILITY_HID_ERROR_CODE << 16) | 0xD,
            HIDP_STATUS_DATA_INDEX_OUT_OF_RANGE  = ((uint)0xC << 28) | (FACILITY_HID_ERROR_CODE << 16) | 0xE,
            HIDP_STATUS_BUTTON_NOT_PRESSED       = ((uint)0xC << 28) | (FACILITY_HID_ERROR_CODE << 16) | 0xF,
            HIDP_STATUS_REPORT_DOES_NOT_EXIST    = ((uint)0xC << 28) | (FACILITY_HID_ERROR_CODE << 16) | 0x10,
            HIDP_STATUS_NOT_IMPLEMENTED          = ((uint)0xC << 28) | (FACILITY_HID_ERROR_CODE << 16) | 0x20
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public class SP_DEVINFO_DATA
        {
            public readonly uint cbSize;
            public Guid ClassGuid;
            public uint DevInst;
            public ulong Reserved;

            public SP_DEVINFO_DATA()
            {
                cbSize = (uint)Marshal.SizeOf(this);
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public class SP_DEVICE_INTERFACE_DATA
        {
            public readonly uint cbSize;
            public Guid InterfaceClassGuid;
            public uint Flags;
            public ulong Reserved;

            public SP_DEVICE_INTERFACE_DATA()
            {
                cbSize = (uint)Marshal.SizeOf(this);
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public class SP_DEVICE_INTERFACE_DETAIL_DATA
        {
            public readonly uint cbSize;
            public char DevicePath;

            public SP_DEVICE_INTERFACE_DETAIL_DATA()
            {
                cbSize = (uint)Marshal.SizeOf(this);
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public class WINUSB_SETUP_PACKET
        {
            public byte RequestType;
            public byte Request;
            public ushort Value;
            public ushort Index;
            public ushort Length;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public class HIDP_CAPS
        {
            public ushort Usage;
            public ushort UsagePage;
            public ushort InputReportByteLength;
            public ushort OutputReportByteLength;
            public ushort FeatureReportByteLength;
            public ushort Reserved1;
            public ushort Reserved2;
            public ushort Reserved3;
            public ushort Reserved4;
            public ushort Reserved5;
            public ushort Reserved6;
            public ushort Reserved7;
            public ushort Reserved8;
            public ushort Reserved9;
            public ushort Reserved10;
            public ushort Reserved11;
            public ushort Reserved12;
            public ushort Reserved13;
            public ushort Reserved14;
            public ushort Reserved15;
            public ushort Reserved16;
            public ushort Reserved17;
            public ushort NumberLinkCollectionNodes;
            public ushort NumberInputButtonCaps;
            public ushort NumberInputValueCaps;
            public ushort NumberInputDataIndices;
            public ushort NumberOutputButtonCaps;
            public ushort NumberOutputValueCaps;
            public ushort NumberOutputDataIndices;
            public ushort NumberFeatureButtonCaps;
            public ushort NumberFeatureValueCaps;
            public ushort NumberFeatureDataIndices;
        }

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetupDiGetClassDevs(
            [In, Optional, MarshalAs(UnmanagedType.LPStruct)] Guid ClassGuid, 
            [In, Optional, MarshalAs(UnmanagedType.LPTStr)] string Enumerator, 
            [In, Optional] IntPtr hwndParent, 
            [In] uint Flags
        );
        /*HDEVINFO SetupDiGetClassDevs(
          _In_opt_ const GUID* ClassGuid,
          _In_opt_       PCTSTR Enumerator,
          _In_opt_       HWND hwndParent,
          _In_           DWORD Flags
        );*/

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetupDiDestroyDeviceInfoList(
            [In] IntPtr DeviceInfoSet
        );
        /*BOOL SetupDiDestroysDeviceInfoList(
          _In_ HDEVINFO DeviceInfoSet
        );*/

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetupDiEnumDeviceInterfaces(
            [In] IntPtr DeviceInfoSet, 
            [In, Optional] SP_DEVINFO_DATA DeviceInfoData, 
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid InterfaceClassGuid, 
            [In] uint MemberIndex, 
            [In, Out] SP_DEVICE_INTERFACE_DATA DeviceInterfaceData
        );
        /*BOOL SetupDiEnumDeviceInterfaces(
          _In_           HDEVINFO                  DeviceInfoSet,
          _In_opt_       PSP_DEVINFO_DATA          DeviceInfoData,
          _In_     const GUID                      *InterfaceClassGuid,
          _In_           DWORD                     MemberIndex,
          _Out_          PSP_DEVICE_INTERFACE_DATA DeviceInterfaceData
        );*/

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetupDiGetDeviceInterfaceDetail(
            [In] IntPtr DeviceInfoSet, 
            [In] SP_DEVICE_INTERFACE_DATA DeviceInterfaceData, 
            [In, Out, Optional] IntPtr DeviceInterfaceDetailData,
            [In] uint DeviceInterfaceDetailDataSize,
            [Out, Optional] out uint RequiredSize,
            [In, Out, Optional] SP_DEVINFO_DATA DeviceInfoData
        );
        /*BOOL SetupDiGetDeviceInterfaceDetail(
          _In_      HDEVINFO                         DeviceInfoSet,
          _In_      PSP_DEVICE_INTERFACE_DATA        DeviceInterfaceData,
          _Out_opt_ PSP_DEVICE_INTERFACE_DETAIL_DATA DeviceInterfaceDetailData,
          _In_      DWORD                            DeviceInterfaceDetailDataSize,
          _Out_opt_ PDWORD                           RequiredSize,
          _Out_opt_ PSP_DEVINFO_DATA                 DeviceInfoData
        );*/

        
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateFile(
          [In, MarshalAs(UnmanagedType.LPTStr)] string lpFileName,
          [In] uint dwDesiredAccess,
          [In] uint dwShareMode,
          [In, Optional] IntPtr lpSecurityAttributes,
          [In] uint dwCreationDisposition,
          [In] uint dwFlagsAndAttributes,
          [In, Optional] IntPtr hTemplateFile
        );
        /*HANDLE WINAPI CreateFile(
          _In_     LPCTSTR               lpFileName,
          _In_     DWORD                 dwDesiredAccess,
          _In_     DWORD                 dwShareMode,
          _In_opt_ LPSECURITY_ATTRIBUTES lpSecurityAttributes,
          _In_     DWORD                 dwCreationDisposition,
          _In_     DWORD                 dwFlagsAndAttributes,
          _In_opt_ HANDLE                hTemplateFile
        );*/

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CloseHandle(
            [In] IntPtr hObject
        );
        /*BOOL WINAPI CloseHandle(
          _In_ HANDLE hObject
        );*/


        [DllImport("winusb.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool WinUsb_Initialize(
            [In] IntPtr DeviceHandle,
            [Out] out IntPtr InterfaceHandle
        );
        /*BOOL __stdcall WinUsb_Initialize(
          _In_ HANDLE                   DeviceHandle,
          _Out_ PWINUSB_INTERFACE_HANDLE InterfaceHandle
        );*/

        [DllImport("winusb.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool WinUsb_Free(
          [In] IntPtr InterfaceHandle
        );
        /*BOOL __stdcall WinUsb_Free(
          _In_ WINUSB_INTERFACE_HANDLE InterfaceHandle
        );*/

        [DllImport("winusb.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool WinUsb_ControlTransfer(
          [In] IntPtr InterfaceHandle,
          [In] WINUSB_SETUP_PACKET SetupPacket,
          [Out] IntPtr Buffer,
          [In] ulong BufferLength,
          [Out, Optional] out ulong LengthTransferred,
          [In, Optional] IntPtr Overlapped
        );
        /*BOOL __stdcall WinUsb_ControlTransfer(
          _In_ WINUSB_INTERFACE_HANDLE InterfaceHandle,
          _In_ WINUSB_SETUP_PACKET     SetupPacket,
          _Out_ PUCHAR                  Buffer,
          _In_ ULONG                   BufferLength,
          _Out_opt_ PULONG                  LengthTransferred,
          _In_opt_ LPOVERLAPPED            Overlapped
        );*/


        [DllImport("hid.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool HidD_GetPreparsedData(
            [In] IntPtr HidDeviceObject,
            [Out] out IntPtr PreparsedData
        );
        /*BOOLEAN __stdcall HidD_GetPreparsedData(
          _In_ HANDLE               HidDeviceObject,
          _Out_ PHIDP_PREPARSED_DATA *PreparsedData
        );*/

        [DllImport("hid.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool HidD_FreePreparsedData(
            [In] IntPtr PreparsedData
        );
        /*BOOLEAN __stdcall HidD_FreePreparsedData(
          _In_ PHIDP_PREPARSED_DATA PreparsedData
        );*/

        [DllImport("hid.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern NTSTATUS HidP_GetCaps(
            [In] IntPtr PreparsedData,
            [In, Out] HIDP_CAPS Capabilities
        );
        /*NTSTATUS __stdcall HidP_GetCaps(
          _In_  PHIDP_PREPARSED_DATA PreparsedData,
          _Out_ PHIDP_CAPS           Capabilities
        );*/

        [DllImport("hid.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void HidD_GetHidGuid(
            [Out] out Guid HidGuid
        );
        /*void __stdcall HidD_GetHidGuid(
          _Out_ LPGUID HidGuid
        );*/



        public static void CheckError(bool condition)
        {
            if (condition)
                return;

            throw new Win32Exception(Marshal.GetLastWin32Error());
        }
    }
}
