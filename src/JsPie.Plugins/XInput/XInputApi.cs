using System;
using System.Runtime.InteropServices;

namespace JsPie.Plugins.XInput
{
    public static class XInputApi
    {
        public const uint ERROR_SUCCESS = 0;
        public const uint ERROR_DEVICE_NOT_CONNECTED = 1167;

        //
        // Device types available in XINPUT_CAPABILITIES
        //
        public const byte XINPUT_DEVTYPE_GAMEPAD = 0x01;

        //
        // Device subtypes available in XINPUT_CAPABILITIES
        //
        public const byte XINPUT_DEVSUBTYPE_GAMEPAD = 0x01;

        public const byte XINPUT_DEVSUBTYPE_UNKNOWN = 0x00;
        public const byte XINPUT_DEVSUBTYPE_WHEEL = 0x02;
        public const byte XINPUT_DEVSUBTYPE_ARCADE_STICK = 0x03;
        public const byte XINPUT_DEVSUBTYPE_FLIGHT_STICK = 0x04;
        public const byte XINPUT_DEVSUBTYPE_DANCE_PAD = 0x05;
        public const byte XINPUT_DEVSUBTYPE_GUITAR = 0x06;
        public const byte XINPUT_DEVSUBTYPE_GUITAR_ALTERNATE = 0x07;
        public const byte XINPUT_DEVSUBTYPE_DRUM_KIT = 0x08;
        public const byte XINPUT_DEVSUBTYPE_GUITAR_BASS = 0x0B;
        public const byte XINPUT_DEVSUBTYPE_ARCADE_PAD = 0x13;

        //
        // Flags for XINPUT_CAPABILITIES
        //
        public const ushort XINPUT_CAPS_VOICE_SUPPORTED = 0x0004;

        public const ushort XINPUT_CAPS_FFB_SUPPORTED = 0x0001;
        public const ushort XINPUT_CAPS_WIRELESS = 0x0002;
        public const ushort XINPUT_CAPS_PMD_SUPPORTED = 0x0008;
        public const ushort XINPUT_CAPS_NO_NAVIGATION = 0x0010;

        //
        // Constants for gamepad buttons
        //
        public const ushort XINPUT_GAMEPAD_DPAD_UP = 0x0001;
        public const ushort XINPUT_GAMEPAD_DPAD_DOWN = 0x0002;
        public const ushort XINPUT_GAMEPAD_DPAD_LEFT = 0x0004;
        public const ushort XINPUT_GAMEPAD_DPAD_RIGHT = 0x0008;
        public const ushort XINPUT_GAMEPAD_START = 0x0010;
        public const ushort XINPUT_GAMEPAD_BACK = 0x0020;
        public const ushort XINPUT_GAMEPAD_LEFT_THUMB = 0x0040;
        public const ushort XINPUT_GAMEPAD_RIGHT_THUMB = 0x0080;
        public const ushort XINPUT_GAMEPAD_LEFT_SHOULDER = 0x0100;
        public const ushort XINPUT_GAMEPAD_RIGHT_SHOULDER = 0x0200;
        public const ushort XINPUT_GAMEPAD_A = 0x1000;
        public const ushort XINPUT_GAMEPAD_B = 0x2000;
        public const ushort XINPUT_GAMEPAD_X = 0x4000;
        public const ushort XINPUT_GAMEPAD_Y = 0x8000;

        //
        // Gamepad thresholds
        //
        public const int XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE = 7849;
        public const int XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE = 8689;
        public const int XINPUT_GAMEPAD_TRIGGER_THRESHOLD = 30;

        //
        // Flags to pass to XInputGetCapabilities
        //
        public const uint XINPUT_FLAG_GAMEPAD = 0x00000001;

        //
        // Devices that support batteries
        //
        public const byte BATTERY_DEVTYPE_GAMEPAD = 0x00;
        public const byte BATTERY_DEVTYPE_HEADSET = 0x01;

        //
        // Flags for battery status level
        //
        public const byte BATTERY_TYPE_DISCONNECTED = 0x00;    // This device is not connected
        public const byte BATTERY_TYPE_WIRED = 0x01;    // Wired device, no battery
        public const byte BATTERY_TYPE_ALKALINE = 0x02;    // Alkaline battery source
        public const byte BATTERY_TYPE_NIMH = 0x03;    // Nickel Metal Hydride battery source
        public const byte BATTERY_TYPE_UNKNOWN = 0xFF;    // Cannot determine the battery type

        // These are only valid for wireless, connected devices, with known battery types
        // The amount of use time remaining depends on the type of device.
        public const byte BATTERY_LEVEL_EMPTY = 0x00;
        public const byte BATTERY_LEVEL_LOW = 0x01;
        public const byte BATTERY_LEVEL_MEDIUM = 0x02;
        public const byte BATTERY_LEVEL_FULL = 0x03;

        // User index definitions
        public const uint XUSER_MAX_COUNT = 4;

        public const uint XUSER_INDEX_ANY = 0x000000FF;

        //
        // Codes returned for the gamepad keystroke
        //

        public const ushort VK_PAD_A = 0x5800;
        public const ushort VK_PAD_B = 0x5801;
        public const ushort VK_PAD_X = 0x5802;
        public const ushort VK_PAD_Y = 0x5803;
        public const ushort VK_PAD_RSHOULDER = 0x5804;
        public const ushort VK_PAD_LSHOULDER = 0x5805;
        public const ushort VK_PAD_LTRIGGER = 0x5806;
        public const ushort VK_PAD_RTRIGGER = 0x5807;

        public const ushort VK_PAD_DPAD_UP = 0x5810;
        public const ushort VK_PAD_DPAD_DOWN = 0x5811;
        public const ushort VK_PAD_DPAD_LEFT = 0x5812;
        public const ushort VK_PAD_DPAD_RIGHT = 0x5813;
        public const ushort VK_PAD_START = 0x5814;
        public const ushort VK_PAD_BACK = 0x5815;
        public const ushort VK_PAD_LTHUMB_PRESS = 0x5816;
        public const ushort VK_PAD_RTHUMB_PRESS = 0x5817;

        public const ushort VK_PAD_LTHUMB_UP = 0x5820;
        public const ushort VK_PAD_LTHUMB_DOWN = 0x5821;
        public const ushort VK_PAD_LTHUMB_RIGHT = 0x5822;
        public const ushort VK_PAD_LTHUMB_LEFT = 0x5823;
        public const ushort VK_PAD_LTHUMB_UPLEFT = 0x5824;
        public const ushort VK_PAD_LTHUMB_UPRIGHT = 0x5825;
        public const ushort VK_PAD_LTHUMB_DOWNRIGHT = 0x5826;
        public const ushort VK_PAD_LTHUMB_DOWNLEFT = 0x5827;

        public const ushort VK_PAD_RTHUMB_UP = 0x5830;
        public const ushort VK_PAD_RTHUMB_DOWN = 0x5831;
        public const ushort VK_PAD_RTHUMB_RIGHT = 0x5832;
        public const ushort VK_PAD_RTHUMB_LEFT = 0x5833;
        public const ushort VK_PAD_RTHUMB_UPLEFT = 0x5834;
        public const ushort VK_PAD_RTHUMB_UPRIGHT = 0x5835;
        public const ushort VK_PAD_RTHUMB_DOWNRIGHT = 0x5836;
        public const ushort VK_PAD_RTHUMB_DOWNLEFT = 0x5837;

        //
        // Flags used in XINPUT_KEYSTROKE
        //
        public const ushort XINPUT_KEYSTROKE_KEYDOWN = 0x0001;
        public const ushort XINPUT_KEYSTROKE_KEYUP = 0x0002;
        public const ushort XINPUT_KEYSTROKE_REPEAT = 0x0004;


        //
        // Structures used by XInput APIs
        //
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public class XINPUT_GAMEPAD
        {
            public ushort wButtons;
            public byte bLeftTrigger;
            public byte bRightTrigger;
            public short sThumbLX;
            public short sThumbLY;
            public short sThumbRX;
            public short sThumbRY;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public class XINPUT_STATE
        {
            public uint dwPacketNumber;
            public XINPUT_GAMEPAD Gamepad;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public class XINPUT_VIBRATION
        {
            public ushort wLeftMotorSpeed;
            public ushort wRightMotorSpeed;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public class XINPUT_CAPABILITIES
        {
            public byte Type;
            public byte SubType;
            public short Flags;
            XINPUT_GAMEPAD Gamepad;
            XINPUT_VIBRATION Vibration;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public class XINPUT_BATTERY_INFORMATION
        {
            public byte BatteryType;
            public byte BatteryLevel;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public class XINPUT_KEYSTROKE
        {
            public ushort VirtualKey;
            public char Unicode;
            public ushort Flags;
            public byte UserIndex;
            public byte HidCode;
        }

        //
        // XInput APIs
        //

        [DllImport("xinput1_4.dll", CharSet = CharSet.Auto)]
        public static extern uint XInputGetState
        (
            [In] uint dwUserIndex,     // Index of the gamer associated with the device
            [Out] XINPUT_STATE pState  // Receives the current state
        );

        [DllImport("xinput1_4.dll", CharSet = CharSet.Auto)]
        public static extern uint XInputSetState
        (
            [In] uint dwUserIndex,            // Index of the gamer associated with the device
            [In] XINPUT_VIBRATION pVibration  // The vibration information to send to the controller
        );

        [DllImport("xinput1_4.dll", CharSet = CharSet.Auto)]
        public static extern uint XInputGetCapabilities
        (
            [In] uint dwUserIndex,  // Index of the gamer associated with the device
            [In] uint dwFlags,      // Input flags that identify the device type
            [Out] XINPUT_CAPABILITIES pCapabilities  // Receives the capabilities
        );

        [DllImport("xinput1_4.dll", CharSet = CharSet.Auto)]
        public static extern void XInputEnable
        (
            [In] bool enable     // [in] Indicates whether xinput is enabled or disabled. 
        );

        [DllImport("xinput1_4.dll", CharSet = CharSet.Auto)]
        public static extern uint XInputGetAudioDeviceIds
        (
            [In] uint dwUserIndex,  // Index of the gamer associated with the device
            [Out, Optional, MarshalAs(UnmanagedType.LPTStr)] string pRenderDeviceId,    // Windows Core Audio device ID string for render (speakers)
            [In, Out, Optional] ref uint pRenderCount,       // Size of render device ID string buffer (in wide-chars)
            [Out, Optional, MarshalAs(UnmanagedType.LPTStr)] string pCaptureDeviceId,   // Windows Core Audio device ID string for capture (microphone)
            [In, Out, Optional] ref uint pCaptureCount       // Size of capture device ID string buffer (in wide-chars)
        );

        [DllImport("xinput1_4.dll", CharSet = CharSet.Auto)]
        public static extern uint XInputGetBatteryInformation
        (
            [In] uint dwUserIndex,        // Index of the gamer associated with the device
            [In] byte devType,            // Which device on this user index
            [Out] XINPUT_BATTERY_INFORMATION pBatteryInformation // Contains the level and types of batteries
        );

        [DllImport("xinput1_4.dll", CharSet = CharSet.Auto)]
        public static extern uint XInputGetKeystroke
        (
            [In] uint dwUserIndex,             // Index of the gamer associated with the device
            uint dwReserved,                   // Reserved for future use
            [Out] XINPUT_KEYSTROKE pKeystroke  // Pointer to an XINPUT_KEYSTROKE structure that receives an input event.
        );

        [DllImport("xinput1_4.dll", CharSet = CharSet.Auto)]
        public static extern uint XInputGetDSoundAudioDeviceGuids
        (
            [In] uint dwUserIndex,          // Index of the gamer associated with the device
            [Out, MarshalAs(UnmanagedType.LPStruct)] out Guid pDSoundRenderGuid,    // DSound device ID for render (speakers)
            [Out, MarshalAs(UnmanagedType.LPStruct)] out Guid pDSoundCaptureGuid    // DSound device ID for capture (microphone)
        );
    }
}
