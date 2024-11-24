using RepairDepot.ViewModel;
using System.Windows.Controls;

namespace RepairDepot.View
{
    /// <summary>
    /// Логика взаимодействия для AdministrationForm.xaml
    /// </summary>
    public partial class AdministrationForm : UserControl
    {
        public AdministrationForm(AdministrationVM vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
