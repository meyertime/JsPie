using JsPie.Core.Util;
using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.IO;
using static JsPie.Plugins.Ps3.Ps3Api;

namespace JsPie.Plugins.Ps3
{
    public class Ps3UsbDeviceInterface : IDisposable
    {
        private readonly Ps3UsbDeviceInfo _deviceInfo;
        private readonly IntPtr _fileHandle;
        private readonly HIDP_CAPS _caps;
        private readonly FileStream _fileStream;
        private readonly byte[] _inputBuffer;
        private readonly Ps3InputData _inputData;

        private bool _isDisposed;

        public Ps3UsbDeviceInterface(Ps3UsbDeviceInfo deviceInfo)
        {
            _deviceInfo = Guard.NotNull(deviceInfo, nameof(deviceInfo));

            var fileHandle = INVALID_HANDLE_VALUE;
            var preparsedData = INVALID_HANDLE_VALUE;
            var caps = new HIDP_CAPS();
            var success = false;

            try
            {
                fileHandle = CreateFile(
                    deviceInfo.DevicePath,
                    GENERIC_READ | GENERIC_WRITE,
                    FILE_SHARE_READ | FILE_SHARE_WRITE,
                    IntPtr.Zero,
                    OPEN_EXISTING,
                    FILE_FLAG_OVERLAPPED,
                    IntPtr.Zero
                );

                if (fileHandle == INVALID_HANDLE_VALUE)
                {
                    CheckError(false);
                }

                CheckError(HidD_GetPreparsedData(fileHandle, out preparsedData));

                var status = HidP_GetCaps(preparsedData, caps);
                if (status != NTSTATUS.HIDP_STATUS_SUCCESS)
                {
                    throw new Win32Exception($"HidP_GetCaps returned non-success status code {status}.");
                }

                _fileHandle = fileHandle;
                _caps = caps;
                _fileStream = new FileStream(new SafeFileHandle(_fileHandle, false), FileAccess.ReadWrite, 4096, true);
                _inputBuffer = new byte[caps.InputReportByteLength];
                _inputData = new Ps3InputData(_inputBuffer);

                success = true;
            }
            finally
            {
                if (preparsedData != INVALID_HANDLE_VALUE)
                    HidD_FreePreparsedData(preparsedData);

                if (!success)
                {
                    if (fileHandle != INVALID_HANDLE_VALUE)
                        CloseHandle(fileHandle);
                }
            }
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            if (_fileStream != null)
                _fileStream.Dispose();

            if (_fileHandle != INVALID_HANDLE_VALUE)
                CloseHandle(_fileHandle);
        }

        public Ps3InputData Read()
        {
            _fileStream.Read(_inputBuffer, 0, _inputBuffer.Length);

            _inputData.Reset();
            return _inputData;
        }
    }
}
