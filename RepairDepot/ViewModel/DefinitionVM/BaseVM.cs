using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace RepairDepot.ViewModel.DefinitionVM
{
    public abstract class BaseVM : INotifyPropertyChanged
    {
        #region Реализация интерфейса
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        /// <summary>
        /// Ининициализация ViewModel
        /// </summary>
        public virtual async Task Initialize() { }

        #endregion
    }
}
