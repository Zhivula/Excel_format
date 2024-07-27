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
        private int countWidth;
        public int CountWidth
        {
            get => countWidth;
            set
            {
                countWidth = value;
                OnPropertyChanged(nameof(CountWidth));
                Settings.Default["CountWidth"] = CountWidth;
                Settings.Default.Save();
            }
        }
        private string fontName;
        public string FontName
        {
            get => fontName;
            set
            {
                fontName = value;
                OnPropertyChanged(nameof(FontName));
                Settings.Default["FontName"] = FontName;
                Settings.Default.Save();
            }
        }
        private int position_1;
        public int Position_1
        {
            get => position_1;
            set
            {
                position_1 = value;
                OnPropertyChanged(nameof(Position_1));
                Settings.Default["Position_1"] = Position_1;
                Settings.Default.Save();
            }
        }
        private int position_2;
        public int Position_2
        {
            get => position_2;
            set
            {
                position_2 = value;
                OnPropertyChanged(nameof(Position_2));
                Settings.Default["Position_2"] = Position_2;
                Settings.Default.Save();
            }
        }
        private int position_3;
        public int Position_3
        {
            get => position_3;
            set
            {
                position_3 = value;
                OnPropertyChanged(nameof(Position_3));
                Settings.Default["Position_3"] = Position_3;
                Settings.Default.Save();
            }
        }
        private int position_4;
        public int Position_4
        {
            get => position_4;
            set
            {
                position_4 = value;
                OnPropertyChanged(nameof(Position_4));
                Settings.Default["Position_4"] = Position_4;
                Settings.Default.Save();
            }
        }
        private int position_5;
        public int Position_5
        {
            get => position_5;
            set
            {
                position_5 = value;
                OnPropertyChanged(nameof(Position_5));
                Settings.Default["Position_5"] = Position_5;
                Settings.Default.Save();
            }
        }
        public SettingsViewModel()
        {
            Width = (double)Settings.Default["Width"];
            Height = (double)Settings.Default["Height"];
            CountWidth = (int)Settings.Default["CountWidth"];    
            FontName = (string)Settings.Default["FontName"];
            Position_1 = (int)Settings.Default["Position_1"];
            Position_2 = (int)Settings.Default["Position_2"];
            Position_3 = (int)Settings.Default["Position_3"];
            Position_4 = (int)Settings.Default["Position_4"];
            Position_5 = (int)Settings.Default["Position_5"];
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
