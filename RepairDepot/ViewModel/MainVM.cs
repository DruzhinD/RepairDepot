using DatabaseAdapter.Models;
using RepairDepot.Model;
using RepairDepot.View;
using RepairDepot.ViewModel.Commands;
using System.Windows;
using System.Windows.Controls;

namespace RepairDepot.ViewModel;

public class MainVM : BaseVM
{
    #region Свойства для связи с View
    //текущая отображаемая форма
    Control currentView;
    public Control CurrentView { get => currentView; set { currentView = value; OnPropertyChanged(); } }

    //Видимость элементов управления
    Visibility enableControls;
    public Visibility EnableControls { get => enableControls; set {  enableControls = value; OnPropertyChanged(); } }

    string label;
    public string Label { get => label; 
        set { label = value; OnPropertyChanged(); } }
    #endregion

    #region Команды для View

    RelayCommand openAuthorizationVM;
    public RelayCommand OpenAuthorizationVM
    {
        get
        {
            return openAuthorizationVM ?? (openAuthorizationVM = new RelayCommand(obj =>
            {
                SelectAuthForm("");
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
                AdministrationVM administrationVM = new AdministrationVM();
                administrationVM.Initialize();
                CurrentView = new AdministrationForm(administrationVM);
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
                SelectMainMenu("");
            }));
        }
    }

    #endregion

    Config config;

    public MainVM()
    {
        //параметры бд
        config = Config.GetInstanse();

        Initialize();
        WelcomeVM welcomeVM = new WelcomeVM();
        CurrentView = new WelcomeForm(welcomeVM);
    }


    public async override Task Initialize()
    {
        EnableControls = Visibility.Hidden;
        Mediator.Subscribe("authorize", SelectMainMenu);
        Mediator.Subscribe("registrate", SelectMainMenu);
        Mediator.Subscribe(nameof(AuthorizationVM), SelectAuthForm);
        Mediator.Subscribe(nameof(RegistrationVM), SelectRegistrationForm);
        Mediator.Subscribe(nameof(PermissionEditVM), SelectPermissionEditForm);
        Mediator.Subscribe("TableEditVM", SelectTableEditForm);
        Mediator.Subscribe("TableEditVM2", SelectTableEditForm2);

    }

    async Task SelectRegistrationForm(object obj)
    {
        RegistrationVM registrationVM = new RegistrationVM();
        await registrationVM.Initialize();
        CurrentView = new RegistrationForm(registrationVM);
    }

    async Task SelectAuthForm(object obj)
    {
        AuthorizationVM authorizationVM = new AuthorizationVM();
        await authorizationVM.Initialize();
        CurrentView = new AuthorizationForm(authorizationVM);
        //CurrentView = authorizationVM;
    }

    async Task SelectMainMenu(object obj)
    {
        if (CommonData.User.Privileges.Name == "Администратор")
            this.EnableControls = Visibility.Visible;
        MainMenuVM mainMenuVM = new MainMenuVM();
        await mainMenuVM.Initialize();
        CurrentView = new MainMenuForm(mainMenuVM);
    }

    async Task SelectPermissionEditForm(object obj)
    {
        PermissionEditVM PermissionEditVM = new PermissionEditVM();
        await PermissionEditVM.Initialize();
        CurrentView = new PermissionEditForm(PermissionEditVM);
    }

    async Task SelectTableEditForm(object obj)
    {

        var form = new TableEditVM<Permission>();
        await form.Initialize();
        CurrentView = new TableEditForm(form);
    }

    async Task SelectTableEditForm2(object obj)
    {

        var form = new TableEditVM<User>();
        await form.Initialize();
        CurrentView = new TableEditForm(form);
    }
}
