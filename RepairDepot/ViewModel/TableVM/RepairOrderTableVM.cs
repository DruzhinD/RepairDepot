using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using RepairDepot.Model;
using RepairDepot.ViewModel.DefinitionVM;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairDepot.ViewModel.TableVM
{
    internal class RepairOrderTableVM : BaseTableVM
    {
        public override string Name => "Наряды на ремонт";


        public async override Task Initialize()
        {
            using var db = new RepairDepotContext(Config.GetInstanse().DbContextOptions);
            var repairOrders = await db.RepairOrders
                .Include(x => x.RepairRequest)
                .ThenInclude(x => x.RepairType)
                .Include(x => x.RepairTask)
                .ThenInclude(x => x.Foreman)
                .ThenInclude(x => x.Employee)
                .ToListAsync();
            Data = new ObservableCollection<IdModel>(repairOrders);
        }
    }
}
