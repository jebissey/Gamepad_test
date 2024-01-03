using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gamepad;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using XInputLib;

namespace Gamepad_test;

internal class MainWindowViewModel : ObservableObject
{
    //private GamePad gamePad = new();

    public MainWindowViewModel()
    {
        //if (!gamePad.StartListening())
        //{
        //    MessageBox.Show("No gamepad found");
        //}



        GameController _selectedController = GameController.RetrieveController();
        _selectedController.StateChanged += _selectedController_StateChanged;
        GameController.StartPolling();
    }

    public ICommand WindowClosing => new RelayCommand<CancelEventArgs>((args) =>
    {
        //GamePad.StopListening();
        GameController.StopPolling();
    });

    void _selectedController_StateChanged(object? sender, GameControllerStateChangedEventArgs e)
    {
        OnPropertyChanged("SelectedController");

    }
}
