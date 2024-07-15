using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZHIVULA.Properties;

namespace ZHIVULA.ViewModel
{
    class SettingsViewModel
    {
        private double height;
        public double Height
        {
            get => height;
            set
            {
                height = value;
                OnPropertyChanged(nameof(Height));
                Settings.Default["Height"] = Height;
                Settings.Default.Save();
            }
        }
        private double width;
        public double Width
        {
            get => width;
            set
            {
                width = value;
                OnPropertyChanged(nameof(Width));
                Settings.Default["Width"] = Width;
                Settings.Default.Save();
            }
        }
        public SettingsViewModel()
        {
            if ((double)Settings.Default["Height"] == 0 && (double)Settings.Default["Width"] == 0)
            {
                Width = 24.22;
                Height = 113.40;
            }
            else
            {
                Width = (double)Settings.Default["Width"];
                Height = (double)Settings.Default["Height"];
            }
        }
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion  
    }
}
