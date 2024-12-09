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
    public class QualityControlTableVM : BaseTableVM
    {
        public override string Name => "Акты контроля качества";

        public async override Task Initialize()
        {
            using var db = new RepairDepotContext(Config.GetInstanse().DbContextOptions);

            var qualityControls = await db.QualityControls
                .Include(x => x.CompleteReport)
                .Include(x => x.AwardOrder)
                .ToListAsync();

            Data = new System.Collections.ObjectModel.ObservableCollection<IdModel>(qualityControls);
        }
    }
}
