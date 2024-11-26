using RepairDepot.ViewModel.Commands;
using RepairDepot.ViewModel.DefinitionVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairDepot.ViewModel
{
    public class CustomTabControlVM : BaseVM
    {
        #region Свойства
        public ObservableCollection<TabItemVM> TabItems { get; set; } = new ObservableCollection<TabItemVM>();

        public TabItemVM SelectedTabItem { get => selectedTabItem; set {  selectedTabItem = value; OnPropertyChanged(); } }
        TabItemVM selectedTabItem;
        #endregion

        #region Команды
        #endregion

        public void AddTabItem(object view, string vmName)
        {
            TabItemVM tabItem = new TabItemVM(view, vmName);
            TabItems.Add(tabItem);
        }
    }
}
