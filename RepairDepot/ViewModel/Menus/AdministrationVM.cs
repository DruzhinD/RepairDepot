using DatabaseAdapter.Models;
using RepairDepot.Model.TableManaging;
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

        AsyncCommand openPermissionTable;
        public AsyncCommand OpenPermissionTable
        {
            get => openPermissionTable ??= new AsyncCommand(
                async (obj) =>
                {
                    var vm = new TableEditVM<Permission>("Уровни доступа");
                    Mediator.Notify("CreateTab", new Tuple<object, string>(new TableEditForm(vm), vm.Name));
                });
        }
        AsyncCommand openUsersTable;
        public AsyncCommand OpenUsersTable
        {
            get => openUsersTable ??= new AsyncCommand(
                async (obj) =>
                {
                    var vm = new TableEditVM<User>("Пользователи ИС");
                    Mediator.Notify("CreateTab", new Tuple<object, string>(new TableEditForm(vm), vm.Name));
                });
        }
        #endregion

        public AdministrationVM() : base() { }
    }
}
