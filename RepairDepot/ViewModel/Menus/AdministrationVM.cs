using DatabaseAdapter.Models;
using RepairDepot.View;
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
                var vm = new RegistrationVM();
                Mediator.Notify("CreateTab", new Tuple<object, string>(vm, vm.Name));
            });
        }


        AsyncCommand openPermissionEditForm;
        public AsyncCommand OpenPermissionEditForm
        {
            get => openPermissionEditForm ??= new AsyncCommand(
                async (obj) =>
                {
                    var vm = new PermissionEditVM();
                    Mediator.Notify("CreateTab", new Tuple<object, string>(vm, vm.Name));
                });
        }
        #endregion

        public AdministrationVM() { }


    }
}
