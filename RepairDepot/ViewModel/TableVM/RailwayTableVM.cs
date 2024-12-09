using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using RepairDepot.Model;
using RepairDepot.ViewModel.Commands;
using RepairDepot.ViewModel.DefinitionVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairDepot.ViewModel.TableVM
{
    public class RailwayTableVM : BaseTableVM
    {
        public override string Name => "Железные дороги";

        #region Свойства

        #endregion

        #region Команды

        #endregion


        public async override Task Initialize()
        {
            using var db = new RepairDepotContext(Config.GetInstanse().DbContextOptions);
            var railway = await db.Railways
                .Include(x => x.Wagons)
                .ThenInclude(x => x.ServiceDirectorate)
                .Include(x => x.Wagons)
                .ThenInclude(x => x.WagonType)
                .ToListAsync();
            Data = new ObservableCollection<IdModel>(railway);
        }

        //protected async override Task SaveChangesMethod()
        //{
        //    using var db = new RepairDepotContext(Config.GetInstanse().DbContextOptions);
        //    db.UpdateRange(Data);
        //    await db.SaveChangesAsync();
        //}
    }
}
