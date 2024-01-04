using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Gamepad;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Gamepad_test;

internal class MainWindowViewModel : ObservableObject
{
    private GamePad gamePad = new();

    #region Properties
    public int Axis1 => GamePad.GetAxis(0) / 64;
    public int Axis2 => GamePad.GetAxis(1) / 64;
    public int Axis3 => GamePad.GetAxis(2) / 64;
    public int Axis4 => GamePad.GetAxis(3) / 64;

    public double Throttle => GetJoystickValue(0, true);
    public double Yaw => GetJoystickValue(3);
    public double Pitch => GetJoystickValue(2, true);
    public double Roll => GetJoystickValue(1);

    public int JoystickSize => 200;
    public int PadDiameter => 25;
    #endregion

    public MainWindowViewModel()
    {
        WeakReferenceMessenger.Default.Register<short[]>(this, (_, axes) =>
        {
            OnPropertyChanged(nameof(Axis1));
            OnPropertyChanged(nameof(Axis2));
            OnPropertyChanged(nameof(Axis3));
            OnPropertyChanged(nameof(Axis4));

            OnPropertyChanged(nameof(Throttle));
            OnPropertyChanged(nameof(Yaw));
            OnPropertyChanged(nameof(Pitch));
            OnPropertyChanged(nameof(Roll));
        });


        if (!gamePad.StartListening())
        {
            MessageBox.Show("No gamepad found");
        }
        else
        {
            MessageBox.Show($"Gamepad found:{GamePad.GetJoystickName()}");
        }
    }

    public ICommand WindowClosing => new RelayCommand<CancelEventArgs>((args) =>
    {
        GamePad.StopListening();
    });

    private double GetJoystickValue(int axis, bool invert = false) => (invert ? JoystickSize : 0) + (((GamePad.GetAxis(axis) / 64.0) + 512.0) / 1024.0 * JoystickSize * (invert ? -1 : 1)) - (PadDiameter / 2.0);
}
