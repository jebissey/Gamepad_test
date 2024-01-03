namespace XInputLib.Enums;

// These are only valid for wireless, connected devices, with known battery types
// The amount of use time remaining depends on the type of device.
public enum BatteryLevel : byte
{
    BATTERY_LEVEL_EMPTY = 0x00,
    BATTERY_LEVEL_LOW = 0x01,
    BATTERY_LEVEL_MEDIUM = 0x02,
    BATTERY_LEVEL_FULL = 0x03
};
