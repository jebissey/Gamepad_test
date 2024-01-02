using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gamepad;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Gamepad_test;

internal class MainWindowViewModel : ObservableObject
{
    private GamePad gamePad = new();

    public MainWindowViewModel()
    {
        if (!gamePad.StartListening())
        {
            MessageBox.Show("No gamepad found");
        }
    }

    public ICommand WindowClosing => new RelayCommand<CancelEventArgs>((args) =>
    {
        GamePad.StopListening();
    });
}
