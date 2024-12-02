using DatabaseAdapter.Models;
using RepairDepot.Model.TableManaging;
using RepairDepot.ViewModel.Commands;
using RepairDepot.ViewModel.DefinitionVM;
using System.Collections.ObjectModel;

namespace RepairDepot.ViewModel
{
    public class TableEditVM<T> : BasePageVM where T : BaseModel
    {
        public override string Name => name;
        string name = string.Empty;
        ITableManager<T> tableManager = new CommonTableManager<T>();

        #region Свойства
        public ObservableCollection<T> Data { get => data; set { data = value; OnPropertyChanged(); } }
        ObservableCollection<T> data = new ObservableCollection<T>();

        string operationMsg;
        public string OperationMsg { get => operationMsg; set {  operationMsg = value; OnPropertyChanged(); } }
        #endregion

        #region Команды
        public AsyncCommand SaveChanges => saveChanges ??= new AsyncCommand(async (obj) => { await SaveChangesAsync(); });
        AsyncCommand saveChanges;
        #endregion


        public TableEditVM()
        {
            name = typeof(T).Name; //TODO: передавать название в качестве агрумента
        }

        /// <summary>
        /// Конструктор с передачей имени VM
        /// </summary>
        /// <param name="name">Имя для VM</param>
        public TableEditVM(string name)
        {
            this.name = "Таблица " + name;
        }


        public override async Task Initialize()
        {
            Data = await tableManager.LoadData();
        }

        public async Task SaveChangesAsync()
        {
            for (int i = Data.Count - 1; i >= 0; i--)
            {
                if (Data[i] == null)
                    Data.Remove(Data[i]);
            }
            if (Data.Count == 0)
                return;
            try
            {
                await tableManager.SaveData(Data);
            }
            catch (Exception ex)
            {
                this.OperationMsg = "Не все поля заполнены";
            }
        }
    }
}
