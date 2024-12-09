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
    public class AwardOrderTableVM : BaseTableVM
    {
        public override string Name => "Приказы на премию";

        public async override Task Initialize()
        {
            using var db = new RepairDepotContext(Config.GetInstanse().DbContextOptions);

            var awardOrders = await db.AwardOrders
                .Include(x => x.QualityControl)
                .ThenInclude(x => x.CompleteReport)
                .ThenInclude(x => x.RepairTask)
                .ThenInclude(x => x.Employees)
                .ToListAsync();

            Data = new System.Collections.ObjectModel.ObservableCollection<IdModel>(awardOrders);
        }

        protected override async Task OpenNestedObjectMethod()
        {
            var vm = new QualityControlTableVM();
            await this.CreateAndNotify(vm);
        }
    }
}
