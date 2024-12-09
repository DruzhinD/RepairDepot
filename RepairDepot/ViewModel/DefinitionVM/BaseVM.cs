using RepairDepot.ViewModel.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        #endregion

        /// <summary>
        /// Ининициализация ViewModel
        /// </summary>
        public virtual async Task Initialize() { }


        /// <summary>
        /// Команда для инициализации юзерконтрола на этапе загрузки. <br/>
        /// Используется совместно с расширением <see cref="Microsoft.Xaml.Behaviors"/> во View
        /// </summary>
        public AsyncCommand InitializeCommand => initializeCommand ??= new AsyncCommand(async (obj) => await this.Initialize());
        AsyncCommand initializeCommand;
    }
}
