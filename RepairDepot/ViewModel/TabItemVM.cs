using RepairDepot.ViewModel.DefinitionVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairDepot.ViewModel
{
    /// <summary>
    /// Является экземпляром конкретной вкладки в CustomTabControl
    /// </summary>
    public class TabItemVM : BaseVM
    {
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

        public TabItemVM(object content, string header)
        {
            Content = content;
            Header = header;
        }
    }
}
