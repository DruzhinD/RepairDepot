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
    /// Команда для открытия вложенной таблицы в отдельной вкладке
    /// </summary>
    public AsyncCommand OpenNestedObject => openNestedObject ??= new AsyncCommand(async (obj) => await OpenNestedObjectMethod());
        AsyncCommand openNestedObject;
    #endregion

    protected virtual void DeleteRowMethod(object obj)
    {
        if (Data.Contains(obj))
            Data.Remove((IdModel)obj);
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
        }
        catch { }

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
