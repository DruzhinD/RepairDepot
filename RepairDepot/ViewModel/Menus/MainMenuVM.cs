using DatabaseAdapter.Models;
using RepairDepot.ViewModel.Commands;
using RepairDepot.ViewModel.DefinitionVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairDepot.ViewModel
{
    public class MainMenuVM : BasePageVM
    {
        public override string Name => "Главное меню";

        #region Команды
        AsyncCommand openTableEdit;
        public AsyncCommand OpenTableEdit
        {
            get
            {
                return openTableEdit ??= new AsyncCommand(async (obj) =>
                {
                    await Mediator.Notify("TableEditVM");
                });
            }
        }

        AsyncCommand openTableEdit2;
        public AsyncCommand OpenTableEdit2
        {
            get
            {
                return openTableEdit2 ??= new AsyncCommand(async (obj) =>
                {
                    await Mediator.Notify("TableEditVM2");
                });
            }
        }

        #endregion

        public MainMenuVM() : base() { }
    }
}
