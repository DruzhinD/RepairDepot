using RepairDepot.ViewModel.Commands;
using System.Windows;
using RepairDepot.ViewModel.DefinitionVM;
using RepairDepot.Model;
using System.IO;
using System.Diagnostics;

namespace RepairDepot.ViewModel;

public class MainVM : BaseVM
{
    #region Свойства для связи с View

    ///<summary>Видимость элементов управления</summary>
    public Visibility VisibilityAdminControl { get => enableControls; set { enableControls = value; OnPropertyChanged(); } }
    Visibility enableControls;

    public Visibility VisibilityAuthorizedControl { get => visibilityAuthorizedControl; set { visibilityAuthorizedControl = value; OnPropertyChanged(); } }
    Visibility visibilityAuthorizedControl;
    #endregion

    #region Команды для View

    RelayCommand openAuthorizationVM;
    public RelayCommand OpenAuthorizationVM
    {
        get
        {
            return openAuthorizationVM ?? (openAuthorizationVM = new RelayCommand(obj =>
            {
                AuthorizationVM vm = new AuthorizationVM();
                Tuple<object, string> tabItemAuth = new Tuple<object, string>(vm, vm.Name);
                Mediator.Notify("CreateTab", tabItemAuth);
            }));
        }
    }

    RelayCommand openAdministrationVM;
    public RelayCommand OpenAdministrationVM
    {
        get
        {
            return openAdministrationVM ?? (openAdministrationVM = new RelayCommand((obj) =>
            {
                var vm = new AdministrationVM();
                Mediator.Notify("CreateTab", new Tuple<object, string>(vm, vm.Name));
            }));
        }
    }

    RelayCommand openMainMenu;
    public RelayCommand OpenMainMenu
    {
        get
        {
            return openMainMenu ?? (openMainMenu = new RelayCommand(obj =>
            {
                var vm = new MainMenuVM();
                Mediator.Notify("CreateTab", new Tuple<object, string>(vm, vm.Name));
            }));
        }
    }

    RelayCommand openProfile;
    public RelayCommand OpenProfile => openProfile ??= new RelayCommand(obj => { var vm = new ProfileVM(); Mediator.Notify("CreateTab", new Tuple<object, string>(vm, vm.Name)); });

    /// <summary>
    /// формирование отчета
    /// </summary>
    AsyncCommand createReport;
    public AsyncCommand CreateReport => createReport ??= new AsyncCommand(async (obj) =>
    {
        string path = await new ReportCreator().CreateReport();
        Process.Start(new ProcessStartInfo(
            Path.Combine(Config.GetInstanse().SavePath, path))
        { UseShellExecute = true }); ;
    });
    #endregion

    public MainVM()
    {
        Mediator.Subscribe(nameof(ShowControlsPerPermission), ShowControlsPerPermission);
        Config.GetInstanse();
    }

    public async override Task Initialize()
    {
        VisibilityAdminControl = Visibility.Collapsed;
        VisibilityAuthorizedControl = Visibility.Collapsed;
        WelcomeVM vm = new WelcomeVM();
        var tabItem = new Tuple<object, string>(vm, vm.Name);
        await Mediator.Notify("CreateTab", tabItem);
        await new ReportCreator().CreateReport();
    }


    async Task ShowControlsPerPermission(object obj)
    {
        if (!CommonData.User.AuthStatus)
            return;

        VisibilityAuthorizedControl = Visibility.Visible;
        if (CommonData.User.Privileges.Admin)
            VisibilityAdminControl = Visibility.Visible;
        else
            VisibilityAdminControl = Visibility.Collapsed;

    }
}
