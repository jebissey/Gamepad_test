using System.Runtime.InteropServices;

namespace XInputLib;


[StructLayout(LayoutKind.Explicit)]
public struct XInputCapabilities
{
    [MarshalAs(UnmanagedType.I1)]
    [FieldOffset(0)]
    byte Type;

    [MarshalAs(UnmanagedType.I1)]
    [FieldOffset(1)]
    public byte SubType;

    [MarshalAs(UnmanagedType.I2)]
    [FieldOffset(2)]
    public short Flags;


    [FieldOffset(4)]
    public XInputGamepad Gamepad;

    [FieldOffset(16)]
    public XInputVibration Vibration;
}
