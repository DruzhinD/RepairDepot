using RepairDepot.ViewModel.Commands;

namespace RepairDepot.ViewModel
{
    public class AdministrationVM : BaseVM
    {
        #region Команды
        RelayCommand openRegistrationForm;
        public RelayCommand OpenRegistrationForm
        {
            get => openRegistrationForm ??= new RelayCommand(obj =>
            {
                Mediator.Notify(nameof(RegistrationVM));
            });
        }


        AsyncCommand openPermissionEditForm;
        public AsyncCommand OpenPermissionEditForm
        {
            get => openPermissionEditForm ??= new AsyncCommand(async (obj) => { Mediator.Notify(nameof(PermissionEditVM)); });
        }
        #endregion

        public AdministrationVM() { }


    }
}
