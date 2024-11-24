using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using RepairDepot.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairDepot.ViewModel
{
    public class PermissionEditVM : BasePageVM
    {
        #region Свойства для View
        public ObservableCollection<Permission> Permissions { get; set; }
        #endregion

        #region Команды
        AsyncCommand saveChanges;
        public AsyncCommand SaveChanges { get => saveChanges ??= new AsyncCommand(async () => { await SyncWithDb(); }); }
        #endregion
        public PermissionEditVM()
        {
            
        }

        public override async Task Initialize()
        {
            using (RepairDepotContext dbContext = new RepairDepotContext(CommonData.DbContextOptions))
            {
                List<Permission> permissions = await dbContext.Permissions.ToListAsync();
                Permissions = new ObservableCollection<Permission>(permissions);
            }
        }

        async Task SyncWithDb()
        {
            using (RepairDepotContext dbContext = new RepairDepotContext(CommonData.DbContextOptions))
            {
                dbContext.Permissions.UpdateRange(Permissions); //TODO
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
