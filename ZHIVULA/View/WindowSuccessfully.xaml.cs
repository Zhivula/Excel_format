using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZHIVULA.View
{
    /// <summary>
    /// Логика взаимодействия для WindowSuccessfully.xaml
    /// </summary>
    public partial class WindowSuccessfully : Window
    {
        public WindowSuccessfully()
        {
            InitializeComponent();
        }
        public void ClosedThisWindow(object sender, EventArgs e)
        {
            Close();
        }
    }
}
