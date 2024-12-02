using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using RepairDepot.Model;
using RepairDepot.ViewModel.Commands;
using RepairDepot.ViewModel.DefinitionVM;

namespace RepairDepot.ViewModel;

/// <summary>
/// Страница авторизации
/// </summary>
public class AuthorizationVM : BasePageVM
{
    public override string Name => "Авторизация";

    #region Свойства для взаимодействия с View
    string _inputLogin = string.Empty;
    /// <summary>
    /// вводимый логин
    /// </summary>
    public string InputLogin { get => _inputLogin; set { this._inputLogin = value; OnPropertyChanged(); } }

    string _inputPassword = string.Empty;
    /// <summary>
    /// вводимый пароль
    /// </summary>
    public string InputPassword { get => _inputPassword; set { this._inputPassword = value; OnPropertyChanged(); } }

    //вывод по результатам авторизации
    string _operationResult = string.Empty;
    public string OperationMsg { get => _operationResult; set { this._operationResult = value; OnPropertyChanged(); } }


    bool _authorizationStatus = false;
    /// <summary>
    /// статус авторизации. True - авторизация прошла успешно; False - авторизация не выполнена
    /// </summary>
    public bool AuthorizationStatus { get => _authorizationStatus; set { this._authorizationStatus = value; OnPropertyChanged(); } }

    #endregion


    public AuthorizationVM() { }

    AsyncCommand? authorize;
    public AsyncCommand Authorize
    {
        get
        {
            return authorize ??= new AsyncCommand(async (obj) =>
            {
                string msg;
                //проверка ввода
                bool result = IsCorrectInput();
                if (!result)
                {
                    msg = "Пароль или логин введенны некорректно.";
                    this.OperationMsg = msg;
                    return;
                }

                //попытка авторизации
                SystemUser User = new SystemUser();
                Task<bool> authTask = User.AuthorizationAsync(this.InputLogin, InputPassword);
                bool authResult = await authTask;
                if (authResult)
                {
                    this.AuthorizationStatus = true;
                    msg = "Авторизация выполнена успешно! \n" +
                    $"Добро пожаловать, {User.User.FirstName}!";
                    CommonData.User = User;
                    await Mediator.Notify("RemoveAllTabs", new List<string>() { this.Name });
                    await Mediator.Notify("ShowControlsPerPermission");
                }
                else
                {
                    msg = "Неверный логин/пароль";
                }
                this.OperationMsg = msg;
            });
        }
    }

    /// <summary>
    /// Проверка на корректность ввода данных
    /// </summary>
    /// <returns>true - данные введены верно</returns>
    bool IsCorrectInput()
    {
        return (!string.IsNullOrEmpty(InputLogin) && !string.IsNullOrEmpty(InputPassword));
    }
}
