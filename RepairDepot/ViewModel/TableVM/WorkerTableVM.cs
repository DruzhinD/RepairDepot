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
    public class WorkerTableVM : BaseTableVM
    {
        public override string Name => "Рабочие";

        public async override Task Initialize()
        {
            using var db = new RepairDepotContext(Config.GetInstanse().DbContextOptions);
            var workers = await db.Workers
                .Include(x => x.Employee)
                .ThenInclude(x => x.RepairTasks)
                .ThenInclude(x => x.RepairOrder)
                //.Include(x => x.Chief) //почему-то не хватает инфу о бригадирах
                //.ThenInclude(x => x.Employee)
                .ToListAsync();

            Data = new System.Collections.ObjectModel.ObservableCollection<IdModel>(workers);
        }
    }
}
