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
    private const int invalidAxis = -1;
    private int throttle = invalidAxis;
    private int yaw = invalidAxis;
    private int pitch = invalidAxis;
    private int roll = invalidAxis;


    #region Properties
    public int JoystickSize => 200;
    public int PadDiameter => 25;


    public int Axis1 => GamePad.GetAxis(0) / 64;
    public int Axis2 => GamePad.GetAxis(1) / 64;
    public int Axis3 => GamePad.GetAxis(2) / 64;
    public int Axis4 => GamePad.GetAxis(3) / 64;


    public double Throttle => GetJoystickValue(throttle, true);
    public double Yaw => GetJoystickValue(yaw);
    public double Pitch => GetJoystickValue(pitch, true);
    public double Roll => GetJoystickValue(roll);




    private AxisType selectedAxis;
    public AxisType SelectedAxis
    {
        get => selectedAxis;
        set
        {
            SetProperty(ref selectedAxis, value);
            MessageBox.Show("Move the pad");
            switch (selectedAxis)
            {
                case AxisType.Throttle:
                    throttle = GetCurrentMovingAxis();
                    break;
                case AxisType.Yaw:
                    yaw = GetCurrentMovingAxis();
                    break;
                case AxisType.Pitch:
                    pitch = GetCurrentMovingAxis();
                    break;
                case AxisType.Roll:
                    roll = GetCurrentMovingAxis();
                    break;
            }
        }
    }
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

    private double GetJoystickValue(int axis, bool invert = false)
        => axis == -1 ? 0 : (invert ? JoystickSize : 0) + (((GamePad.GetAxis(axis) / 64.0) + 512.0) / 1024.0 * JoystickSize * (invert ? -1 : 1)) - (PadDiameter / 2.0);
    private int GetCurrentMovingAxis()
    {
        int loop = 0;
        double[] lastValueFor = { 0, 0, 0, 0 };
        int[] difCounterFor = { 0, 0, 0, 0 };

        while (loop++ < 5_000)
        {
            double[] newValueFor0 = new double[4];
            for (int i = 0; i < 4; i++)
            {
                newValueFor0[i] = GetJoystickValue(i);
                if (newValueFor0[i] != lastValueFor[i])
                {
                    lastValueFor[i] = newValueFor0[i];
                    difCounterFor[i] += 1;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (difCounterFor[i] > 20) return i;
            }
            Thread.Sleep(10);
        }
        return invalidAxis;
    }
}
