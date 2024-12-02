using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairDepot.ViewModel.DefinitionVM
{
    public class BasePageVM : BaseVM, IEquatable<BasePageVM>
    {
        /// <summary>
        /// Название окна (UserControl), содержащего текущую viewmodel. <br/>
        /// Также может быть использовано для отображения на форме
        /// </summary>
        public virtual string Name { get; }

        public bool Equals(BasePageVM? other)
        {
            return (this.Name == other.Name);
        }

        //TODO надо ли?
        public static bool operator ==(BasePageVM pageVM, TabItemVM tabItem)
        {
            return (pageVM.Name == tabItem.Header) == true;
        }

        public static bool operator !=(BasePageVM pageVM, TabItemVM tabItem)
        {
            return (pageVM.Name == tabItem.Header) == false;
        }
    }
}
