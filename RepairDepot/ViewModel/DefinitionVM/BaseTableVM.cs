using DatabaseAdapter.Models;
using RepairDepot.Model;
using RepairDepot.View;
using RepairDepot.ViewModel.Commands;
using System.Collections.ObjectModel;

namespace RepairDepot.ViewModel.DefinitionVM;


/// <summary>
/// Базовая ViewModel для VM работающих с таблицами
/// </summary>
public abstract class BaseTableVM : BasePageVM
{
    #region Свойства
    /// <summary>
    /// Коллекция данных для контекста <see cref="RepairDepotContext"/>
    /// </summary>
    public ObservableCollection<IdModel> Data { get => data; set { data = value; OnPropertyChanged(); } }
    ObservableCollection<IdModel> data = new ObservableCollection<IdModel>();

    /// <summary>
    /// Выбранный основной объект
    /// </summary>
    public IdModel SelectedItem { get => selectedItem; set { selectedItem = value; OnPropertyChanged(); } }
    IdModel selectedItem;

    /// <summary>
    /// Вложенный выбранный объект
    /// </summary>
    public IdModel NestedSelectedItem { get => nestedSelectedItem; set { nestedSelectedItem = value; OnPropertyChanged(); } }
    IdModel nestedSelectedItem;

    /// <summary>
    /// Сообщение, выводимое на экран по результату операций
    /// </summary>
    public string OperationMsg { get => operationMsg; set { operationMsg = value; OnPropertyChanged(); } }
    string operationMsg;
    #endregion

    #region Команды
    /// <summary>
    /// Команда сохранения изменений
    /// </summary>
    public AsyncCommand SaveChanges => saveChanges ??= new AsyncCommand(async (obj) => await SaveChangesMethod());
    AsyncCommand saveChanges;

    /// <summary>
    /// Команда удаления строки данных
    /// </summary>
    public RelayCommand DeleteRow => deleteRow ??= new RelayCommand(obj => DeleteRowMethod(obj));
    RelayCommand deleteRow;

    /// <summary>
    /// Команда добавления строк данных
    /// </summary>
    public RelayCommand AddRow => addRow ??= new RelayCommand(obj => AddRowMethod(obj));
    RelayCommand addRow;

    /// <summary>
    /// Команда для открытия вложенной таблицы в отдельной вкладке
    /// </summary>
    public AsyncCommand OpenNestedObject => openNestedObject ??= new AsyncCommand(async (obj) => await OpenNestedObjectMethod());
        AsyncCommand openNestedObject;
    #endregion

    /// <summary>
    /// данные для удаления
    /// </summary>
    protected List<IdModel> dataToDelete = new List<IdModel>();

    protected virtual void DeleteRowMethod(object obj)
    {
        if (Data.Contains(obj))
        {
            Data.Remove((IdModel)obj);
            dataToDelete.Add((IdModel)obj);
        }
        
    }

    protected virtual void AddRowMethod(object obj)
    {
        var newItem = (IdModel)Data[0].Clone();
        newItem.Id = 0;
        Data.Add(newItem);
    }


    protected async virtual Task SaveChangesMethod()
    {
        try
        { 
            using var db = new RepairDepotContext(Config.GetInstanse().DbContextOptions);
            db.UpdateRange(Data);
            //await db.SaveChangesAsync();

            string logMsg = Logger.FormatLog($"Сохранение {this.Name}");
            var log = new UserLog() { User = CommonData.User.User, Message = logMsg};
            db.Update(log);
            await db.SaveChangesAsync();
            OperationMsg = "Сохранение прошло успешно!";

            //удаление
            if (dataToDelete.Count > 0)
            {
                db.RemoveRange(dataToDelete);
                await db.SaveChangesAsync();
                OperationMsg += "\n" + "Удаление выбранных данных прошло успешно!";
            }


        }
        catch (Exception ex) { }

    }

    protected abstract Task OpenNestedObjectMethod();


    /// <summary>
    /// Создать tab, согласно переданному VM
    /// </summary>
    /// <param name="vm"></param>
    /// <returns></returns>
    protected async Task CreateAndNotify(BasePageVM vm)
    {
        var tuple = new Tuple<object, string>(vm, vm.Name);
        await Mediator.Notify("CreateTab", tuple);
    }
}
