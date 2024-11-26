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
        public ObservableCollection<Permission> Permissions { get; set; }
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

        async Task SyncWithDb()
        {
            await new CommonTableManager<Permission>().SaveData(Permissions);
        }
    }
}
