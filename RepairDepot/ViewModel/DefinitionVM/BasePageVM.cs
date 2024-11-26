using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairDepot.ViewModel.DefinitionVM
{
    public abstract class BasePageVM : BaseVM, IEquatable<BasePageVM>
    {
        /// <summary>
        /// Название окна (UserControl), содержащего текущую viewmodel. <br/>
        /// Также может быть использовано для отображения на форме
        /// </summary>
        public abstract string Name { get; }

        public bool Equals(BasePageVM? other)
        {
            return (this.Name == other.Name);
        }
    }
}
