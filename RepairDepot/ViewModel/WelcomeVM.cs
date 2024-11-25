using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using RepairDepot.ViewModel.Commands;

namespace RepairDepot.ViewModel
{
    public class WelcomeVM : BaseVM
    {
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
