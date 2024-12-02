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

        public TabItemVM SelectedTabItem { get => selectedTabItem; set { selectedTabItem = value; OnPropertyChanged(); } }
        TabItemVM selectedTabItem;
        #endregion

        #region Команды
        /// <summary>
        /// Закрыть конкретную вкладку
        /// </summary>
        public RelayCommand CloseTab => closeTab ??= new RelayCommand(obj => RemoveTabItem(obj as string));
        RelayCommand closeTab;

        public RelayCommand CloseAllTabs => closeAllTabs ??= new RelayCommand(obj => RemoveAllTabs(obj));
        RelayCommand closeAllTabs;
        #endregion

        public CustomTabControlVM()
        {
            var vm = new WelcomeVM();
            AddTabItem(vm, vm.Name);
            Mediator.Subscribe(nameof(CreateTab), CreateTab);
            Mediator.Subscribe(nameof(RemoveTab), RemoveTab);
            Mediator.Subscribe(nameof(RemoveAllTabs), RemoveAllTabs);
        }


        void AddTabItem(object view, string vmName)
        {
            TabItemVM tabItem = new TabItemVM(view, vmName);
            //не позволяет создавать дубликаты
            foreach (var item in tabItems)
                if (tabItem.Header == item.Header)
                {
                    SelectedTabItem = item;
                    return;
                }

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

        public async override Task Initialize() { }

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

        /// <summary>
        /// Удалить все вкладки кроме переданных в obj
        /// </summary>
        /// <param name="obj">должен соответствовать <see cref="IEnumerable{string}"/>T - string</param>
        /// <returns></returns>
        async Task RemoveAllTabs(object obj)
        {
            if (obj == null)
            {
                TabItems.Clear();
                return;
            }
            IEnumerable<string>? tabsKeys = obj as IEnumerable<string>;
            IEnumerable<TabItemVM> tabs = TabItems.Where(x => tabsKeys.Contains(x.Header));
            
            for (int i = 0; i < TabItems.Count; i++)
            {
                if (tabs.Contains(TabItems[i]))
                    continue;
                TabItems.Remove(TabItems[i]);
                i--;
            }
        }

        TabItemVM GetTabItem(string key)
        {
            foreach (var t in TabItems)
            {
                if (t.Header == key)
                    return t;
            }
            return null;
        }
    }
}
