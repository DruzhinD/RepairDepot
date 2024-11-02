using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RepairDepot.Model;

namespace RepairDepot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DatabaseConnection con = new DatabaseConnection(Config.GetInstanse());
            int x = 0;
            int y = 0;

            //Console.WriteLine(x.ToString() + y.ToString());
            InitializeComponent();
        }
    }
}