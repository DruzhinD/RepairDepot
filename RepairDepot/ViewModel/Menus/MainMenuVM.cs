using DatabaseAdapter.Models;
using RepairDepot.Model.TableManaging;
using RepairDepot.View;
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
        AsyncCommand openPermission;
        public AsyncCommand OpenPermission => openPermission ??= new AsyncCommand(async (obj) =>
        {
            var vm = new TableEditVM<Permission>("Уровни прав доступа");
            await CreateViewAndNotify(vm);
        });

        AsyncCommand openUser;
        public AsyncCommand OpenUser => openUser ??= new AsyncCommand(async (obj) =>
        {
            var vm = new TableEditVM<User>("Пользователи ИС");
            await CreateViewAndNotify(vm);
        });

        AsyncCommand openAwardOrder;
        public AsyncCommand OpenAwardOrder => openAwardOrder ??= new AsyncCommand(async (obj) =>
        {
            var vm = new TableEditVM<AwardOrder>("Приказы о начислении премий");
            await CreateViewAndNotify(vm);
        });
        AsyncCommand openCompleteReport;
        public AsyncCommand OpenCompleteReport => openCompleteReport ??= new AsyncCommand(async (obj) =>
        {
            var vm = new TableEditVM<CompleteReport>("Отчеты о выполнении работ");
            await CreateViewAndNotify(vm);
        });
        AsyncCommand openEmployee;
        public AsyncCommand OpenEmployee => openEmployee ??= new AsyncCommand(async (obj) =>
        {
            var vm = new TableEditVM<Employee>("Сотрудники (общий список)");
            await CreateViewAndNotify(vm);
        });
        //AsyncCommand openEmployeeRepairTask;
        //public AsyncCommand OpenEmployeeRepairTask => openEmployeeRepairTask ??= new AsyncCommand(async (obj) =>
        //{
        //    var vm = new TableEditVM<EmployeeRepairTask>("Задание на ремонт - сотрудники");
        //    await CreateViewAndNotify(vm);
        //});
        AsyncCommand openExternalRailway;
        public AsyncCommand OpenExternalRailway => openExternalRailway ??= new AsyncCommand(async (obj) =>
        {
            var vm = new TableEditVM<ExternalRailway>("Внешние железные дороги");
            await CreateViewAndNotify(vm);
        });
        AsyncCommand openForeman;
        public AsyncCommand OpenForeman => openForeman ??= new AsyncCommand(async (obj) =>
        {
            var vm = new TableEditVM<Foreman>("Бригадиры");
            await CreateViewAndNotify(vm);
        });
        AsyncCommand openInternalRailway;
        public AsyncCommand OpenInternalRailway => openInternalRailway ??= new AsyncCommand(async (obj) =>
        {
            var vm = new TableEditVM<InternalRailway>("Внутренние железные дороги");
            await CreateViewAndNotify(vm);
        });
        AsyncCommand openQualityControl;
        public AsyncCommand OpenQualityControl => openQualityControl ??= new AsyncCommand(async (obj) =>
        {
            var vm = new TableEditVM<QualityControl>("Акты контроля качества");
            await CreateViewAndNotify(vm);
        });
        AsyncCommand openRailway;
        public AsyncCommand OpenRailway => openRailway ??= new AsyncCommand(async (obj) =>
        {
            var vm = new TableEditVM<Railway>("Железные дороги (общий список)");
            await CreateViewAndNotify(vm);
        });
        AsyncCommand openRepairOrder;
        public AsyncCommand OpenRepairOrder => openRepairOrder ??= new AsyncCommand(async (obj) =>
        {
            var vm = new TableEditVM<RepairOrder>("Наряды на ремонт");
            await CreateViewAndNotify(vm);
        });
        AsyncCommand openRepairRequest;
        public AsyncCommand OpenRepairRequest => openRepairRequest ??= new AsyncCommand(async (obj) =>
        {
            var vm = new TableEditVM<RepairRequest>("Запросы на ремонт");
            await CreateViewAndNotify(vm);
        });
        AsyncCommand openRepairTask;
        public AsyncCommand OpenRepairTask => openRepairTask ??= new AsyncCommand(async (obj) =>
        {
            var vm = new TableEditVM<RepairTask>("Задания на ремонт");
            await CreateViewAndNotify(vm);
        });
        AsyncCommand openRepairType;
        public AsyncCommand OpenRepairType => openRepairType ??= new AsyncCommand(async (obj) =>
        {
            var vm = new TableEditVM<RepairType>("Типы ремонта");
            await CreateViewAndNotify(vm);
        });
        AsyncCommand openServiceDirectorate;
        public AsyncCommand OpenServiceDirectorate => openServiceDirectorate ??= new AsyncCommand(async (obj) =>
        {
            var vm = new TableEditVM<ServiceDirectorate>("Дирекции по обслуживанию пассажиров");
            await CreateViewAndNotify(vm);
        });
        AsyncCommand openWagon;
        public AsyncCommand OpenWagon => openWagon ??= new AsyncCommand(async (obj) =>
        {
            var vm = new TableEditVM<Wagon>("Вагоны");
            await CreateViewAndNotify(vm);
        });
        AsyncCommand openWorker;
        public AsyncCommand OpenWorker => openWorker ??= new AsyncCommand(async (obj) =>
        {
            var vm = new TableEditVM<Worker>("Рабочие");
            await CreateViewAndNotify(vm);
        });

        #endregion

        public MainMenuVM() : base() { }

        async Task CreateViewAndNotify(BasePageVM vm)
        {
            var tuple = new Tuple<object, string>(new TableEditForm(vm), vm.Name);
            await Mediator.Notify("CreateTab", tuple);
        }
    }
}
