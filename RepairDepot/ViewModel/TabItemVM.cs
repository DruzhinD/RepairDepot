using RepairDepot.ViewModel.Commands;
using RepairDepot.ViewModel.DefinitionVM;

namespace RepairDepot.ViewModel
{
    /// <summary>
    /// Является экземпляром конкретной вкладки в CustomTabControl
    /// </summary>
    public class TabItemVM : BaseVM
    {

        #region Свойства
        /// <summary>
        /// Содержимое вкладки
        /// </summary>
        public object Content { get => content; set { content = value; OnPropertyChanged(); } }
        object content;

        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Header { get => header; set {  header = value; OnPropertyChanged(); } }
        string header;
        #endregion

        #region Команды
        RelayCommand closeSelf;
        public RelayCommand CloseSelf => closeSelf ??= new RelayCommand(obj => Mediator.Notify("RemoveTab", this.Header));
        #endregion


        public TabItemVM(object content, string header)
        {
            Content = content;
            Header = header;
        }
    }
}
