using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using RepairDepot.Model;
using RepairDepot.ViewModel.DefinitionVM;
using System.Collections.ObjectModel;

namespace RepairDepot.ViewModel.TableVM
{
    internal class RepairRequestTableVM : BaseTableVM
    {
        public override string Name => "Запросы на ремонт";


        public async override Task Initialize()
        {
            using var db = new RepairDepotContext(Config.GetInstanse().DbContextOptions);

            var repairRequests = await db.RepairRequests
                .Include(x => x.Wagon)
                .ThenInclude(x => x.ServiceDirectorate)
                .Include(x => x.Wagon)
                .ThenInclude(x => x.WagonType)
                .Include(x => x.RepairOrder)
                .Include(x => x.RepairType)
                .ToListAsync();

            Data = new ObservableCollection<IdModel>(repairRequests);
        }
    }
}
