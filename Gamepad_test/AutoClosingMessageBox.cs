﻿using System.Windows;

namespace Gamepad_test;

public class AutoClosingMessageBox
{
    const int WM_CLOSE = 0x0010;
    Timer _timeoutTimer;
    string _caption;
    
    AutoClosingMessageBox(string text, string caption, int timeout)
    {
        _caption = caption;
        _timeoutTimer = new Timer(OnTimerElapsed, null, timeout, Timeout.Infinite);
        using (_timeoutTimer) MessageBox.Show(text, caption);
    }
    
    public static void Show(string text, string caption, int timeout)
    {
        _ = new AutoClosingMessageBox(text, caption, timeout);
    }

    void OnTimerElapsed(object state)
    {
        IntPtr mbWnd = FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
        if (mbWnd != IntPtr.Zero) SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        _timeoutTimer.Dispose();
    }


    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
    static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
}
