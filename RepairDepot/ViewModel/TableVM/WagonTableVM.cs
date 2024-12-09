using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using RepairDepot.Model;
using RepairDepot.ViewModel.Commands;
using RepairDepot.ViewModel.DefinitionVM;
using System.Collections.ObjectModel;

namespace RepairDepot.ViewModel.TableVM
{
    public class WagonTableVM : BaseTableVM
    {
        public override string Name => "Вагоны";

        #region Свойства

        #endregion

        #region Команды

        #endregion


        public async override Task Initialize()
        {
            using var db = new RepairDepotContext(Config.GetInstanse().DbContextOptions);
            var wagons = await db.Wagons
                .Include(x => x.Railway)
                .Include(x => x.ServiceDirectorate)
                .Include(x => x.WagonType)
                .Include(x => x.RepairRequests)
                .ThenInclude(x => x.RepairType)
                .ToListAsync();

            Data = new ObservableCollection<IdModel>(wagons);
        }

        protected async override Task OpenNestedObjectMethod()
        {
            var vm = new RailwayTableVM();
            await CreateAndNotify(vm);
        }
    }
}
