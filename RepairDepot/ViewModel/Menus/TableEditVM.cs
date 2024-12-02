using DatabaseAdapter.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using RepairDepot.Model;
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
        //коллекция элементов
        public ObservableCollection<T> Data { get => data; set { data = value; OnPropertyChanged(); } }
        ObservableCollection<T> data = new ObservableCollection<T>();

        //выделенный элемент
        public T SelectedItem { get => selectedItem; set { selectedItem = value; OnPropertyChanged(); } }
        T selectedItem;

        string operationMsg;
        public string OperationMsg { get => operationMsg; set { operationMsg = value; OnPropertyChanged(); } }
        #endregion

        #region Команды
        public AsyncCommand SaveChanges => saveChanges ??= new AsyncCommand(async (obj) => { await SaveChangesAsync(); });
        AsyncCommand saveChanges;

        public RelayCommand RemoveRow => removeRow ??= new RelayCommand(obj =>
        {
            if (obj != null)
            {
                removeData.Add(obj as T);
                Data.Remove(obj as T);
            }
        });
        RelayCommand removeRow;
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
            //сохранение данных в бд
            if (Data.Count > 0)
            {
                try
                {
                    await tableManager.SaveData(Data);
                    this.OperationMsg = "Операция выполнена успешно";
                }
                catch (Exception ex)
                {
                    this.OperationMsg = "Не все поля заполнены";
                }
            }

            //удаление записей из бд
            if (removeData.Count > 0)
            {
                try
                {
                    await tableManager.DeleteData(removeData);
                    removeData.Clear();
                    this.OperationMsg = "Операция выполнена успешно";
                }
                catch (Exception ex)
                {
                    this.OperationMsg = "Не удалось удалить записи";
                }
            }

            if (Data.Count == 0 && removeData.Count == 0)
            {
                this.OperationMsg = "Нет данных для изменения";
                return;
            }
            string msg = $"{CommonData.User.User.Login} внес изменения в {this.Name}";
            Logger.Log(msg);
        }
        /// <summary>
        /// Данные на удаление
        /// </summary>
        HashSet<T> removeData = new HashSet<T>();
    }
}
