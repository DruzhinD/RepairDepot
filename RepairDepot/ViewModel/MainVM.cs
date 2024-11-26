using DatabaseAdapter.Models;
using RepairDepot.Model;
using RepairDepot.View;
using RepairDepot.ViewModel.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RepairDepot.ViewModel.DefinitionVM;

namespace RepairDepot.ViewModel;

public class MainVM : BaseVM
{
    #region Свойства для связи с View
    //текущая отображаемая форма
    Control currentView; //TODO
    public Control CurrentView { get => currentView; set { currentView = value; OnPropertyChanged(); } }

    //Видимость элементов управления
    Visibility enableControls;
    public Visibility EnableControls { get => enableControls; set {  enableControls = value; OnPropertyChanged(); } }
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

    //TODO
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



    #region tab
    static int tabs = 1;
    public ObservableCollection<Item> Titles
    {
        get { return _titles; }
        set
        {
            _titles = value;
            OnPropertyChanged("Titles");
        }
    }

    public class Item
    {
        public string Header { get; set; }
        public Control Content { get; set; }
        RelayCommand remove;
        //public RelayCommand Remove { get => remove ??= ; }
    }

    private ICommand _addTab;
    private ICommand _removeTab;
    private ObservableCollection<Item> _titles = new ObservableCollection<Item>();

    public ICommand AddTab
    {
        get
        {
            return _addTab ?? (_addTab = new RelayCommand(
               x =>
               {
                   AddTabItem();
               }));
        }
    }

    public ICommand RemoveTab
    {
        get
        {
            return _removeTab ?? (_removeTab = new RelayCommand(
               x =>
               {
                   RemoveTabItem();
               }));
        }
    }

    private void RemoveTabItem()
    {
        Titles.Remove(Titles.Last());
        tabs--;
    }

    private void AddTabItem()
    {
        var vm = new MainMenuVM();
        var header = vm.Name;
        vm.Initialize();
        var content = new MainMenuForm(vm); //TODO
        var item = new Item { Header = header, Content = content };

        Titles.Add(item);
        tabs++;
        OnPropertyChanged("Titles");
    }
    #endregion

}
