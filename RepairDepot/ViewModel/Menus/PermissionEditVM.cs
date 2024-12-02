using DatabaseAdapter.Models;
using RepairDepot.Model.TableManaging;
using RepairDepot.ViewModel.Commands;
using RepairDepot.ViewModel.DefinitionVM;
using System.Collections.ObjectModel;

namespace RepairDepot.ViewModel
{
    public class PermissionEditVM : BasePageVM
    {
        public override string Name => "Права доступа";

        #region Свойства для View
        public ObservableCollection<Permission> Permissions { get => permissions; set { permissions = value; OnPropertyChanged(); } }
        ObservableCollection<Permission> permissions = new ObservableCollection<Permission>();
        #endregion

        #region Команды
        AsyncCommand saveChanges;
        public AsyncCommand SaveChanges { get => saveChanges ??= new AsyncCommand(async (obj) => { await SyncWithDb(); }); }
        #endregion

        public PermissionEditVM() { }

        public override async Task Initialize()
        {
            Permissions = await new CommonTableManager<Permission>().LoadData();
        }

        AsyncCommand load;
        public AsyncCommand Load
        {
            get => load ??= new AsyncCommand(async (obj) =>
            {
                await Initialize();
            });
        }

        async Task SyncWithDb()
        {
            await new CommonTableManager<Permission>().SaveData(Permissions);
        }
    }
}
