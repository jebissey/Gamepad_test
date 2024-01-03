using System.Runtime.InteropServices;
using XInputLib.Enums;

namespace XInputLib;

[StructLayout(LayoutKind.Explicit)]
public struct XInputBatteryInformation
{
    [MarshalAs(UnmanagedType.I1)]
    [FieldOffset(0)]
    public byte BatteryType;

    [MarshalAs(UnmanagedType.I1)]
    [FieldOffset(1)]
    public byte BatteryLevel;

    public override string ToString()
    {
        return string.Format("{0} {1}", (BatteryTypes)BatteryType, (BatteryLevel)BatteryLevel);
    }
}
