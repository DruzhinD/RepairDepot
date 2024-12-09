using ClosedXML;
using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairDepot.Model.TableManaging
{
    /// <summary>
    /// Универсальный класс доступа к любой таблице базы данных. <br/>
    /// Примечание: часть операций выполняется синхронно
    /// </summary>
    public class CommonTableManager<T> : ITableManager<T> where T : IdModel
    {
        /// <summary>
        /// <inheritdoc/> <br/>
        /// Выполняется синхронно
        /// </summary>
        public async Task<ObservableCollection<T>> LoadData()
        {
            using (RepairDepotContext db = new RepairDepotContext(Config.GetInstanse().DbContextOptions))
            {
                DbSet<T> result = db.Set<T>();
                return new ObservableCollection<T>(result);
            }
        }

        public async Task SaveData(IEnumerable<T> data)
        {
            using (RepairDepotContext db = new RepairDepotContext(Config.GetInstanse().DbContextOptions))
            {
                db.UpdateRange(data);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteData(IEnumerable<T> data)
        {
            using (RepairDepotContext db = new RepairDepotContext(Config.GetInstanse().DbContextOptions))
            {
                db.RemoveRange(data);
                await db.SaveChangesAsync();
            }
        }
    }
}
