namespace XInputLib;

public class GameControllerStateChangedEventArgs : EventArgs
{
    public XInputState CurrentInputState { get; set; }
    public XInputState PreviousInputState { get; set; }
}
