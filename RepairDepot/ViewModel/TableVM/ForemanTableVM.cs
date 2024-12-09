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
    public class ForemanTableVM : BaseTableVM
    {
        public override string Name => "Бригадиры";

        public async override Task Initialize()
        {
            using var db = new RepairDepotContext(Config.GetInstanse().DbContextOptions);
            var foremen = await db.Foremen
                .Include(x => x.Employee)
                .Include(x => x.Workers)
                .ThenInclude(x => x.Employee)
                .ToListAsync();

            Data = new System.Collections.ObjectModel.ObservableCollection<IdModel>(foremen);
        }

        protected async override Task OpenNestedObjectMethod()
        {
            var vm = new WorkerTableVM();
            await CreateAndNotify(vm);
        }
    }
}
