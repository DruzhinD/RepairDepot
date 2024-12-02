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
    public class RepairRequestTableManager<T> : ITableManager<T> where T : RepairRequest
    {
        public Task DeleteData(IEnumerable<T> data)
        {
            throw new NotImplementedException();
        }

        public async Task<ObservableCollection<T>> LoadData()
        {
            using (RepairDepotContext db = new RepairDepotContext(Config.GetInstanse().DbContextOptions))
            {
                List<RepairRequest> s = await db.RepairRequests.ToListAsync();
                Console.WriteLine(s[0].Wagon.RegNumber);
            }
            return null;
        }

        public Task SaveData(IEnumerable<T> data)
        {
            throw new NotImplementedException();
        }
    }
}
