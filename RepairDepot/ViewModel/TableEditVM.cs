using DatabaseAdapter.Models;
using RepairDepot.Model.TableManaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RepairDepot.ViewModel
{
    class TableEditVM<T> : BaseVM where T : BaseModel
    {
        public ObservableCollection<T> Data { get; set; } = new ObservableCollection<T>();

        public TableEditVM()
        {
            
        }

        public override async Task Initialize()
        {
            Data = await new CommonTableManager<T>().LoadData();
        }
    }
}
