using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using RepairDepot.Model;
using RepairDepot.ViewModel.Commands;
using RepairDepot.ViewModel.DefinitionVM;

namespace RepairDepot.ViewModel
{
    public class WelcomeVM : BasePageVM
    {
        public override string Name => "Приветствие";

        string logoPath;
        public string LogoPath { get => Config.GetInstanse().LogoPath; set { OnPropertyChanged(); } }

        #region Свойства для связи с View
        #endregion

        #region Команды
        RelayCommand auth;
        public RelayCommand Auth
        {
            get => auth ??= new RelayCommand(obj =>
            {
                var vm = new AuthorizationVM();
                Mediator.Notify("CreateTab", new Tuple<object, string>(vm, vm.Name));
            });
        }
        #endregion

        public WelcomeVM()
        {
            
        }
    }
}
