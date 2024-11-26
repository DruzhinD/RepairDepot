using Microsoft.VisualBasic;
using RepairDepot.ViewModel.DefinitionVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для TableEditForm.xaml
    /// </summary>
    public partial class TableEditForm : UserControl
    {
        public TableEditForm()
        {
            InitializeComponent();
        }

        public TableEditForm(BaseVM vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        #region Решение для отображения названий столбцов, заданных в атрибуте DisplayName свойств класса.
        //TODO: стоит подумать как это перенести в отдельный UserControl с DataGrid
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string displayName = GetPropertyDisplayName(e.PropertyDescriptor);
            //displayname
            if (!string.IsNullOrEmpty(displayName))
            {
                e.Column.Header = displayName;
            }
            //browsable
            if (((PropertyDescriptor)e.PropertyDescriptor).IsBrowsable == false)
            {
                e.Cancel = true;
            }
            //ID отображается первым
            if (displayName == "ID")
                e.Column.DisplayIndex = 0;
        }

        public static string GetPropertyDisplayName(object descriptor)
        {

            PropertyDescriptor pd = descriptor as PropertyDescriptor;
            if (pd != null)
            {
                DisplayNameAttribute dn = pd.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
                if (dn != null && dn != DisplayNameAttribute.Default)
                {
                    return dn.DisplayName;
                }
            }
            else
            {
                PropertyInfo pi = descriptor as PropertyInfo;
                if (pi != null)
                {
                    Object[] attributes = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                    for (int i = 0; i < attributes.Length; ++i)
                    {
                        DisplayNameAttribute dn = attributes[i] as DisplayNameAttribute;
                        if (dn != null && dn != DisplayNameAttribute.Default)
                        {
                            return dn.DisplayName;
                        }
                    }
                }
            }
            return null;
        }
        #endregion
    }
}
