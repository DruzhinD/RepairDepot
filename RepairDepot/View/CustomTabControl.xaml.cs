using RepairDepot.ViewModel;
using System.Windows.Controls;

namespace RepairDepot.View
{
    /// <summary>
    /// Логика взаимодействия для CustomTabControl.xaml
    /// </summary>
    public partial class CustomTabControl : UserControl
    {
        public CustomTabControl()
        {
            InitializeComponent();
        }

        public CustomTabControl(CustomTabControlVM vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
