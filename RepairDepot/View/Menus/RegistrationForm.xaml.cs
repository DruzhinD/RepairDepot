using DatabaseAdapter.Models;
using RepairDepot.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RepairDepot.View
{
    /// <summary>
    /// Логика взаимодействия для RegistrationForm.xaml
    /// </summary>
    public partial class RegistrationForm : UserControl
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        public RegistrationForm(RegistrationVM registrationVM)
        {
            InitializeComponent();
            DataContext = registrationVM;
        }
    }
}
