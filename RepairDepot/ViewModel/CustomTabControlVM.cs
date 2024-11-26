using RepairDepot.ViewModel.Commands;
using RepairDepot.ViewModel.DefinitionVM;
using System.Collections.ObjectModel;

namespace RepairDepot.ViewModel
{
    public class CustomTabControlVM : BaseVM
    {
        #region Свойства
        ObservableCollection<TabItemVM> tabItems = new ObservableCollection<TabItemVM>();
        public ObservableCollection<TabItemVM> TabItems { get => tabItems; set { tabItems = value; OnPropertyChanged(); } }

        public TabItemVM SelectedTabItem { get => selectedTabItem; set {  selectedTabItem = value; OnPropertyChanged(); } }
        TabItemVM selectedTabItem;
        #endregion

        #region Команды
        RelayCommand closeTab;
        public RelayCommand CloseTab => closeTab ??= new RelayCommand(obj => RemoveTabItem(obj as string));
        #endregion

        public CustomTabControlVM()
        {
            var vm = new WelcomeVM();
            AddTabItem(vm, vm.Name);
            Mediator.Subscribe(nameof(CreateTab), CreateTab);
            Mediator.Subscribe(nameof(RemoveTab), RemoveTab);
        }


        void AddTabItem(object view, string vmName)
        {
            TabItemVM tabItem = new TabItemVM(view, vmName);
            TabItems.Add(tabItem);
            SelectedTabItem = tabItem;
        }

        void RemoveTabItem(string key)
        {
            foreach (var item in TabItems)
                if (item.Header == key)
                {
                    TabItems.Remove(item);
                    break;
                }
        }

        public async override Task Initialize()
        {
            var vm = new MainMenuVM();
            TabItemVM tabItem = new TabItemVM(vm, vm.Name);
            TabItems.Add(tabItem);

        }

        /// <summary>
        /// Создание вкладки с произвольным содержимым
        /// </summary>
        /// <param name="obj">должен распаковываться как тип <see cref="Tuple{object, string}"/></param>
        async Task CreateTab(object obj)
        {
            var s = obj as Tuple<object, string>;
            AddTabItem(s.Item1, s.Item2);
        }


        /// <summary>
        /// Удаление вкладки по указанному ключу
        /// </summary>
        /// <param name="obj">должен распаковываться как тип <see cref="string"/></param>
        /// <returns></returns>
        async Task RemoveTab(object obj)
        {
            var s = obj as string;
            RemoveTabItem(s);
        }
    }
}
