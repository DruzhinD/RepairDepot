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
using System.Windows;
#nullable disable

namespace RepairDepot.ViewModel
{
    public class MainMenuVM : BasePageVM
    {
        #region Свойства
        public Visibility VisibleForeman { get => visibleForeman; set { visibleForeman = value; OnPropertyChanged(); } }
        Visibility visibleForeman = Visibility.Collapsed;
        public Visibility VisibleWorker { get => visibleWorker; set { visibleWorker = value; OnPropertyChanged(); } }
        Visibility visibleWorker = Visibility.Collapsed;
        public Visibility VisibleEmployee { get => visibleEmployee; set { visibleEmployee = value; OnPropertyChanged(); } }
        Visibility visibleEmployee = Visibility.Collapsed;
        public Visibility VisibleEmployeeRepairTask { get => visibleEmployeeRepairTask; set { visibleEmployeeRepairTask = value; OnPropertyChanged(); } }
        Visibility visibleEmployeeRepairTask = Visibility.Collapsed;
        public Visibility VisibleInternalRailway { get => visibleInternalRailway; set { visibleInternalRailway = value; OnPropertyChanged(); } }
        Visibility visibleInternalRailway = Visibility.Collapsed;
        public Visibility VisibleExternalRailway { get => visibleExternalRailway; set { visibleExternalRailway = value; OnPropertyChanged(); } }
        Visibility visibleExternalRailway = Visibility.Collapsed;
        public Visibility VisibleRailway { get => visibleRailway; set { visibleRailway = value; OnPropertyChanged(); } }
        Visibility visibleRailway = Visibility.Collapsed;
        public Visibility VisibleWagon { get => visibleWagon; set { visibleWagon = value; OnPropertyChanged(); } }
        Visibility visibleWagon = Visibility.Collapsed;
        public Visibility VisibleServiceDirectorate { get => visibleServiceDirectorate; set { visibleServiceDirectorate = value; OnPropertyChanged(); } }
        Visibility visibleServiceDirectorate = Visibility.Collapsed;
        public Visibility VisibleRepairRequest { get => visibleRepairRequest; set { visibleRepairRequest = value; OnPropertyChanged(); } }
        Visibility visibleRepairRequest = Visibility.Collapsed;
        public Visibility VisibleRepairType { get => visibleRepairType; set { visibleRepairType = value; OnPropertyChanged(); } }
        Visibility visibleRepairType = Visibility.Collapsed;
        public Visibility VisibleRepairOrder { get => visibleRepairOrder; set { visibleRepairOrder = value; OnPropertyChanged(); } }
        Visibility visibleRepairOrder = Visibility.Collapsed;
        public Visibility VisibleRepairTask { get => visibleRepairTask; set { visibleRepairTask = value; OnPropertyChanged(); } }
        Visibility visibleRepairTask = Visibility.Collapsed;
        public Visibility VisibleCompleteReport { get => visibleCompleteReport; set { visibleCompleteReport = value; OnPropertyChanged(); } }
        Visibility visibleCompleteReport = Visibility.Collapsed;
        public Visibility VisibleQualityControl { get => visibleQualityControl; set { visibleQualityControl = value; OnPropertyChanged(); } }
        Visibility visibleQualityControl = Visibility.Collapsed;
        public Visibility VisibleAwardOrder { get => visibleAwardOrder; set { visibleAwardOrder = value; OnPropertyChanged(); } }
        Visibility visibleAwardOrder = Visibility.Collapsed;

        #endregion

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
        AsyncCommand openEmployeeRepairTask;
        public AsyncCommand OpenEmployeeRepairTask => openEmployeeRepairTask ??= new AsyncCommand(async (obj) =>
        {
            var vm = new TableEditVM<EmployeeRepairTask>("Задание на ремонт - сотрудники");
            await CreateViewAndNotify(vm);
        });
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

        public override string Name => "Главное меню";
        public MainMenuVM() : base() { }

        public override async Task Initialize()
        {
            if (CommonData.User.Privileges.TechnicalDepartment)
            {
                VisibleWagon = Visibility.Visible;
                VisibleRailway = Visibility.Visible;
                VisibleInternalRailway = Visibility.Visible;
                VisibleExternalRailway = Visibility.Visible;
                VisibleRepairRequest = Visibility.Visible;

                VisibleServiceDirectorate = Visibility.Visible;
                VisibleRepairType = Visibility.Visible;
            }
            if (CommonData.User.Privileges.PlaningDepartment)
            {
                VisibleRepairRequest = Visibility.Visible;
                VisibleRepairOrder = Visibility.Visible;
                VisibleRepairTask = Visibility.Visible;
                VisibleCompleteReport = Visibility.Visible;
                VisibleQualityControl = Visibility.Visible;

                VisibleWorker = Visibility.Visible;
                VisibleForeman = Visibility.Visible;
                VisibleEmployee = Visibility.Visible;
                //VisibleEmployeeRepairTask = Visibility.Visible;
            }
            if (CommonData.User.Privileges.StaffDepartment)
            {
                VisibleWorker = Visibility.Visible;
                VisibleForeman = Visibility.Visible;
                VisibleEmployee = Visibility.Visible;
                VisibleAwardOrder = Visibility.Visible;

                VisibleQualityControl = Visibility.Visible;
            }
            if (CommonData.User.Privileges.RepairDepartment)
            {
                VisibleWorker = Visibility.Visible;
                VisibleForeman = Visibility.Visible;
                VisibleEmployee = Visibility.Visible;
                VisibleAwardOrder = Visibility.Visible;

                VisibleRepairTask = Visibility.Visible;
                VisibleCompleteReport = Visibility.Visible;

            }

        }


        async Task CreateViewAndNotify(BasePageVM vm)
        {
            var tuple = new Tuple<object, string>(new TableEditForm(vm), vm.Name);
            await Mediator.Notify("CreateTab", tuple);
        }
    }
}
