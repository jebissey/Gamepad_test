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

    public int Axis1 => GamePad.GetAxes(0) / 64;
    public int Axis2 => GamePad.GetAxes(1) / 64;
    public int Axis3 => GamePad.GetAxes(2) / 64;
    public int Axis4 => GamePad.GetAxes(3) / 64;


    public MainWindowViewModel()
    {
        WeakReferenceMessenger.Default.Register<short[]>(this, (_, axes) =>
        {
            OnPropertyChanged(nameof(Axis1));
            OnPropertyChanged(nameof(Axis2));
            OnPropertyChanged(nameof(Axis3));
            OnPropertyChanged(nameof(Axis4));
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
}
