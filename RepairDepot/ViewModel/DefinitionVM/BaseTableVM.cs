using DatabaseAdapter.Models;
using RepairDepot.Model;
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
    #endregion

    protected virtual void DeleteRowMethod(object obj)
    {
        if (Data.Contains(obj))
            Data.Remove((IdModel)obj);
    }

    protected async virtual Task SaveChangesMethod()
    {
        using var db = new RepairDepotContext(Config.GetInstanse().DbContextOptions);
        db.UpdateRange(Data);
        await db.SaveChangesAsync();
    }
}
