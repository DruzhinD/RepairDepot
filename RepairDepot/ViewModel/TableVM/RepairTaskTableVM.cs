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
    public class RepairTaskTableVM : BaseTableVM
    {
        public override string Name => "Задания на ремонт";

        public async override Task Initialize()
        {
            using var db = new RepairDepotContext(Config.GetInstanse().DbContextOptions);
            var repairTasks = await db.RepairTasks
                .Include(x => x.CompleteReport)
                .Include(x => x.RepairOrder)
                .Include(x => x.Foreman)
                .ThenInclude(x => x.Employee)
                .Include(x => x.Employees)
                .ToListAsync();
            Data = new System.Collections.ObjectModel.ObservableCollection<IdModel>(repairTasks);
        }
    }
}
