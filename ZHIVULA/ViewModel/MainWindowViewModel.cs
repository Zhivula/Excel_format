using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ZHIVULA.Data;
using ZHIVULA.Properties;
using ZHIVULA.View;
using Excel = Microsoft.Office.Interop.Excel;

namespace ZHIVULA.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        readonly MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

        public MainWindowViewModel()
        {

        }

        public ICommand Block_1_Command => new DelegateCommand(o =>
        {
            window.ChangedGrid.Children.Clear();
            window.ChangedGrid.Children.Add(new BlockView(new Block_1_ViewModel()));
        });
        public ICommand Block_2_Command => new DelegateCommand(o =>
        {
            window.ChangedGrid.Children.Clear();
            window.ChangedGrid.Children.Add(new BlockView(new Block_2_ViewModel()));
        });
        public ICommand SettingsCommand => new DelegateCommand(o =>
        {
            window.ChangedGrid.Children.Clear();
            window.ChangedGrid.Children.Add(new SettingsView());
        });
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion  
    }
}
