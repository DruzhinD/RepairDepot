using RepairDepot.ViewModel.Commands;
using System.Windows;
using RepairDepot.ViewModel.DefinitionVM;
using RepairDepot.Model;

namespace RepairDepot.ViewModel;

public class MainVM : BaseVM
{
    #region Свойства для связи с View

    //Видимость элементов управления
    Visibility enableControls;
    public Visibility EnableControls { get => enableControls; set { enableControls = value; OnPropertyChanged(); } }

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
    #endregion

    public MainVM()
    {
        Mediator.Subscribe(nameof(ShowControlsPerPermission), ShowControlsPerPermission);
        Config.GetInstanse();
    }

    public async override Task Initialize()
    {
        EnableControls = Visibility.Collapsed;
        WelcomeVM vm = new WelcomeVM();
        var tabItem = new Tuple<object, string>(vm, vm.Name);
        await Mediator.Notify("CreateTab", tabItem);
    }


    async Task ShowControlsPerPermission(object obj)
    {
        if (CommonData.User.Privileges.Name == "Администратор")
            EnableControls = Visibility.Visible;
    }
}
