using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using RepairDepot.ViewModel.Commands;
using RepairDepot.ViewModel.DefinitionVM;

namespace RepairDepot.ViewModel
{
    public class WelcomeVM : BasePageVM
    {
        public override string Name => "Приветствие";

        #region Свойства для связи с View
        #endregion

        #region Команды
        RelayCommand auth;
        public RelayCommand Auth
        {
            get => auth ??= new RelayCommand(obj =>
            {
                Mediator.Notify(nameof(AuthorizationVM));
            });
        }
        #endregion

        public WelcomeVM()
        {
            
        }
    }
}
