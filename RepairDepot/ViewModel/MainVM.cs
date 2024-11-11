using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using RepairDepot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RepairDepot.ViewModel
{
    public class MainVM : BaseVM
    {
        //авторизация
        AuthorizationVM authorizationVM;

        Config config;
        DbContextOptions<RepairDepotContext> dbContextOptions;

        public AuthorizationVM AuthorizationVM
        {
            get => authorizationVM; set
            {
                authorizationVM = value;
                OnPropertyChanged();
            }
        }

        public MainVM()
        {
            //параметры бд
            config = Config.GetInstanse();
            this.dbContextOptions = new DbContextOptionsBuilder<RepairDepotContext>().UseNpgsql(config["connectionString"]).Options;

            this.AuthorizationVM = new AuthorizationVM(this.dbContextOptions);
        }

        RelayCommand myButton;
        public RelayCommand MyButton
        {
            get
            {
                return myButton ?? (myButton = new RelayCommand(obj =>
                {
                    if (authorizationVM.Visible == Visibility.Visible)
                        authorizationVM.Visible = Visibility.Hidden;
                    else
                        authorizationVM.Visible = Visibility.Visible;
                    Label = "qwerty";
                }));
            }
        }

        string label;
        public string Label
        {
            get => label;
            set
            {
                label = value; OnPropertyChanged();

            }
        }
    }
}
