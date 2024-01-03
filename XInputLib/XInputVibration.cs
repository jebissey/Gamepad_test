using System.Runtime.InteropServices;

namespace XInputLib;

[StructLayout(LayoutKind.Sequential)]
public struct XInputVibration
{
    [MarshalAs(UnmanagedType.I2)]
    public ushort LeftMotorSpeed;

    [MarshalAs(UnmanagedType.I2)]
    public ushort RightMotorSpeed;
}
