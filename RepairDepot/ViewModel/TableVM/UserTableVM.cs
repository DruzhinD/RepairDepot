using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using RepairDepot.Model;
using RepairDepot.ViewModel.DefinitionVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairDepot.ViewModel.TableVM
{
    public class UserTableVM : BaseTableVM
    {
        public override string Name => "Пользователи ИС";

        public async override Task Initialize()
        {
            using var db = new RepairDepotContext(Config.GetInstanse().DbContextOptions);

            var users = await db.Users
                .Include(x => x.Permission)
                .Include(x => x.UserLogs)
                .ToListAsync();
            Data = new System.Collections.ObjectModel.ObservableCollection<IdModel>(users);
        }

        protected async override Task OpenNestedObjectMethod()
        {
            
        }
    }
}
