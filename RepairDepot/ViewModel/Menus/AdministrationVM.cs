using RepairDepot.ViewModel.Commands;
using RepairDepot.ViewModel.DefinitionVM;

namespace RepairDepot.ViewModel
{
    public class AdministrationVM : BasePageVM
    {
        public override string Name => "Администрирование";

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
