using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ZHIVULA.Properties;

namespace ZHIVULA
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //using (var context = new MyDbContext())
            //{
            //    context.Database.Delete();
            //}

            if (string.IsNullOrEmpty((string)Settings.Default["FontName"]))
            {
                Settings.Default["FontName"] = "Times New Roman";
            }
            if ((double)Settings.Default["Height"] == 0)
            {
                Settings.Default["Height"] = 113.40;
            }
            if ((double)Settings.Default["Width"] == 0)
            {
                Settings.Default["Width"] = 24.22;
            }
            if ((int)Settings.Default["CountWidth"] == 0)
            {
                Settings.Default["CountWidth"] = 3;
            }



            if ((int)Settings.Default["Position_1"] == 0)
            {
                Settings.Default["Position_1"] = 48;
            }
            if ((int)Settings.Default["Position_2"] == 0)
            {
                Settings.Default["Position_2"] = 26;
            }
            if ((int)Settings.Default["Position_3"] == 0)
            {
                Settings.Default["Position_3"] = 48;
            }
            if ((int)Settings.Default["Position_4"] == 0)
            {
                Settings.Default["Position_4"] = 36;
            }
            if ((int)Settings.Default["Position_5"] == 0)
            {
                Settings.Default["Position_5"] = 90;
            }
            Settings.Default.Save();
        }
    }
}
