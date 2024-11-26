using DatabaseAdapter.Models;
using RepairDepot.Model.TableManaging;
using RepairDepot.ViewModel.DefinitionVM;
using System.Collections.ObjectModel;

namespace RepairDepot.ViewModel
{
    public class TableEditVM<T> : BasePageVM where T : BaseModel
    {
        public override string Name => name;
        string name = string.Empty;

        ObservableCollection<T> data = new ObservableCollection<T>();
        public ObservableCollection<T> Data { get => data; set { data = value; OnPropertyChanged(); } }


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
