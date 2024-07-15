using System.Windows.Controls;
using ZHIVULA.Data;
using ZHIVULA.ViewModel;

namespace ZHIVULA.View
{
    /// <summary>
    /// Логика взаимодействия для Block_1_View.xaml
    /// </summary>
    public partial class BlockView : UserControl
    {
        public BlockView(IBlockViewModel block)
        {
            InitializeComponent();
            DataContext = block;
        }
    }
}
