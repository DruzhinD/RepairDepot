using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RepairDepot.ViewModel
{
    public class BasePageVM : BaseVM
    {
        protected Visibility visible = Visibility.Visible;
        /// <summary>
        /// Отображать на экране или нет
        /// </summary>
        public Visibility Visible
        {
            get => visible; set
            {
                visible = value; OnPropertyChanged();
            }
        }
    }
}
