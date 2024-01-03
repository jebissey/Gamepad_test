using XInputLib.Enums;

//https://www.codeproject.com/Articles/492473/Using-XInput-to-access-an-Xbox-360-Controller-in-M

namespace XInputLib;

public class GameController
{
    private XInputState gamepadStatePrev = new();
    private XInputState gamepadStateCurrent = new();
    private bool _stopMotorTimerActive;
    private DateTime _stopMotorTime;

    public const int FIRST_CONTROLLER_INDEX = 0;

    private static GameController Controller = new(FIRST_CONTROLLER_INDEX);

    private static readonly object SyncLock = new();
    private static readonly int updateFrequency = 2500;

    public event EventHandler<GameControllerStateChangedEventArgs>? StateChanged = null;

    private GameController(int playerIndex)
    {
        gamepadStatePrev.Copy(gamepadStateCurrent);
    }


    public XInputCapabilities GetCapabilities()
    {
        XInputCapabilities capabilities = new();
        _ = XInput.XInputGetCapabilities(FIRST_CONTROLLER_INDEX, XInputConstants.XINPUT_FLAG_GAMEPAD, ref capabilities);
        return capabilities;
    }

    public static GameController RetrieveController()
    {
        return Controller;
    }

    #region Polling
    private static bool isRunning;
    private static Thread pollingThread;
    private static bool keepRunning;

    public bool IsConnected { get; internal set; }
    public XInputBatteryInformation BatteryInformationHeadset { get; internal set; }
    public XInputBatteryInformation BatteryInformationGamepad { get; internal set; }

    public static void StartPolling()
    {
        if (!isRunning)
        {
            lock (SyncLock)
            {
                if (!isRunning)
                {
                    pollingThread = new Thread(PollerLoop);
                    pollingThread.Start();
                }
            }
        }
    }

    public static void StopPolling()
    {
        if (isRunning) keepRunning = false;
    }

    static void PollerLoop()
    {
        lock (SyncLock)
        {
            if (isRunning == true) return;
            isRunning = true;
        }
        keepRunning = true;
        while (keepRunning)
        {
            Controller.UpdateState();
            Thread.Sleep(updateFrequency);
        }
        lock (SyncLock)
        {
            isRunning = false;
        }
    }

    public void UpdateBatteryState()
    {
        XInputBatteryInformation headset = new XInputBatteryInformation(),
        gamepad = new XInputBatteryInformation();

        _ = XInput.XInputGetBatteryInformation(FIRST_CONTROLLER_INDEX, (byte)BatteryDeviceType.BATTERY_DEVTYPE_GAMEPAD, ref gamepad);
        _ = XInput.XInputGetBatteryInformation(FIRST_CONTROLLER_INDEX, (byte)BatteryDeviceType.BATTERY_DEVTYPE_HEADSET, ref headset);

        BatteryInformationHeadset = headset;
        BatteryInformationGamepad = gamepad;
    }

    public void UpdateState()
    {
        XInputCapabilities X = new XInputCapabilities();
        int result = XInput.XInputGetState(4, ref gamepadStateCurrent);
        IsConnected = result == 0;

        UpdateBatteryState();
        if (gamepadStateCurrent.PacketNumber != gamepadStatePrev.PacketNumber) OnStateChanged();
        gamepadStatePrev.Copy(gamepadStateCurrent);

        if (_stopMotorTimerActive && (DateTime.Now >= _stopMotorTime))
        {
            XInputVibration stopStrength = new() { LeftMotorSpeed = 0, RightMotorSpeed = 0 };
            _ = XInput.XInputSetState(FIRST_CONTROLLER_INDEX, ref stopStrength);
        }
    }
    #endregion

    protected void OnStateChanged()
    {
        StateChanged?.Invoke(this, new GameControllerStateChangedEventArgs() { CurrentInputState = gamepadStateCurrent, PreviousInputState = gamepadStatePrev });
    }
}
