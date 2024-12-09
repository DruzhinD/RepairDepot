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
    public class CompleteReportTableVM : BaseTableVM
    {
        public override string Name => "Отчеты о выполнении";

        public async override Task Initialize()
        {
            using var db = new RepairDepotContext(Config.GetInstanse().DbContextOptions);
            var completeReports = await db.CompleteReports
                .Include(x => x.RepairTask)
                .ThenInclude(x => x.Foreman)
                .ThenInclude(x => x.Employee)
                .Include(x => x.QualityControl)
                .ToListAsync();
            Data = new System.Collections.ObjectModel.ObservableCollection<IdModel>(completeReports);
        }

        protected async override Task OpenNestedObjectMethod()
        {
            var vm = new QualityControlTableVM();
            await CreateAndNotify(vm);
        }
    }
}
