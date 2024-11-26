using DatabaseAdapter.Models;
using RepairDepot.Model.TableManaging;
using RepairDepot.ViewModel.DefinitionVM;
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
    class TableEditVM<T> : BasePageVM where T : BaseModel
    {
        string name;
        public override string Name => name;

        public ObservableCollection<T> Data { get; set; } = new ObservableCollection<T>();

        public TableEditVM()
        {
            name = typeof(T).Name; //TODO: передавать название в качестве агрумента
        }

        public override async Task Initialize()
        {
            Data = await new CommonTableManager<T>().LoadData();
        }
    }
}
