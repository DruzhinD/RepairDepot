using RepairDepot.Model;
using RepairDepot.View;
using RepairDepot.ViewModel.Commands;
using System.Windows;
using System.Windows.Controls;

namespace RepairDepot.ViewModel;

public class MainVM : BaseVM
{
    #region Свойства для связи с View

    #region локальные ViewModel
    //авторизация
    RegistrationVM? registrationVM;

    //авторизация
    AuthorizationVM? authorizationVM;

    //приветственная форма
    WelcomeVM welcomeVM;
    MainMenuVM mainMenuVM;
    AdministrationVM administrationVM;
    PermissionEditVM PermissionEditVM;

    //текущая отображаемая форма
    Control currentView;
    public Control CurrentView { get => currentView; set { currentView = value; OnPropertyChanged(); } }
    #endregion

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
                administrationVM = new AdministrationVM();
                administrationVM.Initialize();
                CurrentView = new AdministrationForm(administrationVM);
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
        welcomeVM = new WelcomeVM();
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
    }

    //TODO не await'ится
    async void SelectRegistrationForm(object obj)
    {
        registrationVM = new RegistrationVM();
        await registrationVM.Initialize();
        CurrentView = new RegistrationForm(registrationVM);
    }

    void SelectAuthForm(object obj)
    {
        authorizationVM = new AuthorizationVM();
        authorizationVM.Initialize();
        CurrentView = new AuthorizationForm(authorizationVM);
    }

    void SelectMainMenu(object obj)
    {
        if (mainMenuVM == null)
        {
            mainMenuVM = new MainMenuVM();
            mainMenuVM.Initialize();
        }

        if (authorizationVM.AuthorizationStatus)
            EnableControls = Visibility.Visible;
        CurrentView = new MainMenuForm(mainMenuVM);
    }

    //TODO не await'ится
    async void SelectPermissionEditForm(object obj)
    {
        PermissionEditVM = new PermissionEditVM();
        await PermissionEditVM.Initialize();
        CurrentView = new PermissionEditForm(PermissionEditVM);
    }
}
