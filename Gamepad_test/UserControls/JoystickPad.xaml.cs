using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Gamepad_test.UserControls;

public partial class JoystickPad : UserControl
{
    public JoystickPad()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty CircleDiameterProperty = DependencyProperty.Register("CircleDiameter", typeof(double), typeof(JoystickPad), new PropertyMetadata(default(double)));
    public double CircleDiameter
    {
        get { return (double)GetValue(CircleDiameterProperty); }
        set { SetValue(CircleDiameterProperty, value); }
    }
    public double HalfCircleDiameter => CircleDiameter / 2.0;

    public static readonly DependencyProperty LineStrokeColorProperty = DependencyProperty.Register("LineStrokeColor", typeof(Brush), typeof(JoystickPad), new PropertyMetadata(default(Brush)));
    public Brush LineStrokeColor
    {
        get { return (Brush)GetValue(LineStrokeColorProperty); }
        set { SetValue(LineStrokeColorProperty, value); }
    }

    public static readonly DependencyProperty PadColorProperty = DependencyProperty.Register("PadColor", typeof(Brush), typeof(JoystickPad), new PropertyMetadata(default(Brush)));
    public Brush PadColor
    {
        get { return (Brush)GetValue(PadColorProperty); }
        set { SetValue(PadColorProperty, value); }
    }

    public static readonly DependencyProperty PadDiameterProperty = DependencyProperty.Register("PadDiameter", typeof(double), typeof(JoystickPad), new PropertyMetadata(default(double)));
    public double PadDiameter
    {
        get { return (double)GetValue(PadDiameterProperty); }
        set { SetValue(PadDiameterProperty, value); }
    }

    public static readonly DependencyProperty PadLeftProperty = DependencyProperty.Register("PadLeft", typeof(double), typeof(JoystickPad), new PropertyMetadata(default(double)));
    public double PadLeft
    {
        get { return (double)GetValue(PadLeftProperty); }
        set { SetValue(PadLeftProperty, value); }
    }

    public static readonly DependencyProperty PadTopProperty = DependencyProperty.Register("PadTop", typeof(double), typeof(JoystickPad), new PropertyMetadata(default(double)));
    public double PadTop
    {
        get { return (double)GetValue(PadTopProperty); }
        set { SetValue(PadTopProperty, value); }
    }
}
