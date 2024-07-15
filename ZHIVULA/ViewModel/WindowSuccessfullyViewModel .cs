using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using ZHIVULA.View;

namespace ZHIVULA.ViewModel
{
    class WindowSuccessfullyViewModel : UserControl
    {
        public static void Successfully()
        {
            WindowSuccessfully window = new WindowSuccessfully();
            window.Show();
            DoubleAnimation windowAnimation = new DoubleAnimation();
            windowAnimation.From = window.Opacity;
            windowAnimation.To = 0;
            windowAnimation.Duration = TimeSpan.FromSeconds(1.5);
            windowAnimation.Completed += window.ClosedThisWindow;
            window.BeginAnimation(OpacityProperty, windowAnimation);
        }
        public static void NotSuccessfully()
        {
            WindowSuccessfully window = new WindowSuccessfully();
            window.TextLabel.Content = "Ошибка!";
            window.Icon.Kind = MaterialDesignThemes.Wpf.PackIconKind.CloseCircle;
            window.Icon.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            window.Show();
            DoubleAnimation windowAnimation = new DoubleAnimation();
            windowAnimation.From = window.Opacity;
            windowAnimation.To = 0;
            windowAnimation.Duration = TimeSpan.FromSeconds(1.5);
            windowAnimation.Completed += window.ClosedThisWindow;
            window.BeginAnimation(OpacityProperty, windowAnimation);
        }
    }
}