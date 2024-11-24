using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using RepairDepot.Model;
using RepairDepot.ViewModel.Commands;
using System.Collections.ObjectModel;
#nullable disable
namespace RepairDepot.ViewModel;

/// <summary>
/// ViewModel для страницы регистрации
/// </summary>
public class RegistrationVM : BasePageVM
{
    #region Свойства для взаимодействия с View
    //логин
    string _login = string.Empty;
    public string Login { get => _login; set { _login = value; OnPropertyChanged(); } }

    //пароль
    string _password = string.Empty;
    public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }
    //подтверждение введенного пароля. Должно совпадать с Password [не используется]
    //string _passwordConfirmation;
    //public string PasswordConfirmation { get => _passwordConfirmation; set { _passwordConfirmation = value; OnPropertyChanged(); } }

    //фамилия
    string _lastName = string.Empty;
    public string LastName { get => _lastName; set { _lastName = value; OnPropertyChanged(); } }
    //имя
    string _firstName = string.Empty;
    public string FirstName { get => _firstName; set { _firstName = value; OnPropertyChanged(); } }

    //отчество
    string _middleName = string.Empty;
    public string MiddleName { get => _middleName; set { _middleName = value; OnPropertyChanged(); } }

    //уровни доступа
    public ObservableCollection<Permission> Permissions { get; set; } = new ObservableCollection<Permission>();
    Permission selectedPermission;
    public Permission SelectedPermission { get => selectedPermission; set { selectedPermission = value; OnPropertyChanged(); } }

    //результат регистрации
    string _operationMsg;
    public string OperationMsg { get => _operationMsg; set { _operationMsg = value; OnPropertyChanged(); } }

    //активность кнопки регистрации
    bool registrationButtonEnable;
    public bool RegistrationButtonEnable { get => registrationButtonEnable; set { registrationButtonEnable = value; OnPropertyChanged(); } }

    #endregion

    #region Команды

    AsyncCommand registrate;
    public AsyncCommand Registrate
    {
        get
        {
            return registrate ??= new AsyncCommand(async () =>
            {
                if (!IsCorrectInput())
                {
                    OperationMsg = "Не все поля заполнены!";
                    return;
                }
                SystemUser user = new SystemUser();
                User dbUser = new()
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    MiddleName = MiddleName,
                    Login = Login,
                    Password = Password,
                    Permission = SelectedPermission,
                };
                bool regResult = await user.RegisterAsync(dbUser);
                if (regResult)
                {
                    OperationMsg = "Регистрация прошла успешно!";
                    Mediator.Notify("registrate");
                }
                else
                {
                    OperationMsg = "Некорректный ввод данных";
                }
            });
        }
    }
    #endregion


    public RegistrationVM() { }
    protected RegistrationVM(DbContextOptions<RepairDepotContext> options)
        : base(options)
    {
    }

    public override async Task Initialize()
    {
        await InitializePermissionsFromDb();
        if (CommonData.User.Privileges.Name != "Администратор")
        {
            OperationMsg = "Недостаточно прав для выполнения операции";
            RegistrationButtonEnable = false;
        }
        else
        {
            OperationMsg = "";
            RegistrationButtonEnable = true;
        }
    }

    /// <summary>
    /// Инициализировать список уровнений доступа к ИС
    /// </summary>
    async Task InitializePermissionsFromDb()
    {
        List<Permission> permissions;
        try
        {
            using (RepairDepotContext dbContext = new RepairDepotContext(CommonData.DbContextOptions))
            {
                Task<List<Permission>> task = dbContext.Permissions.ToListAsync();
                permissions = await task;
                if (permissions != null)
                    this.Permissions = new ObservableCollection<Permission>(permissions);
            }
        }
        catch (Exception ex)
        {
            return;
        }
    }

    /// <summary>
    /// Проверка корректности введенных данных
    /// </summary>
    bool IsCorrectInput()
    {
        string[] input =
        {
            this.FirstName,
            LastName,
            MiddleName,
            Login,
            Password,
        };

        //поля не заполнены
        foreach (string s in input)
            if (string.IsNullOrEmpty(s))
                return false;
        //не выбран уровень доступа
        if (SelectedPermission == null)
            return false;

        return true;
    }
}
