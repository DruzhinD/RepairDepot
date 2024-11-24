using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RepairDepot.ViewModel
{
    public abstract class BasePageVM : BaseVM
    {
        protected Visibility visible;
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

        public BasePageVM() { }
        protected BasePageVM(DbContextOptions<RepairDepotContext> options)
        {
            this.dbContextOptions = options;
        }
        protected DbContextOptions<RepairDepotContext> dbContextOptions;
    }
}
