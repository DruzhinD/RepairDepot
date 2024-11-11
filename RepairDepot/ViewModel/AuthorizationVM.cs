using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using RepairDepot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading.Tasks;

namespace RepairDepot.ViewModel
{
    /// <summary>
    /// Страница авторизации
    /// </summary>
    public class AuthorizationVM : BasePageVM
    {
        #region Свойства для взаимодействия с View
        string _inputLogin;
        /// <summary>
        /// вводимый логин
        /// </summary>
        public string InputLogin
        {
            get => _inputLogin;
            set
            {
                this._inputLogin = value;
                OnPropertyChanged();
            }
        }

        string _inputPassword;
        /// <summary>
        /// вводимый пароль
        /// </summary>
        public string InputPassword
        {
            get => _inputPassword;
            set
            {
                this._inputPassword = value;
                OnPropertyChanged();
            }
        }

        //вывод по результатам авторизации
        string _operationResult;
        public string OperationMsg
        {
            get => _operationResult; set
            {
                this._operationResult = value;
                OnPropertyChanged();
            }
        }


        bool _authorizationStatus = false;
        /// <summary>
        /// статус авторизации. True - авторизация прошла успешно; False - авторизация не выполнена
        /// </summary>
        public bool AuthorizationStatus
        {
            get => _authorizationStatus; set
            {
                this._authorizationStatus = value;
                OnPropertyChanged();
            }
        }

        #endregion

        /// <summary>
        /// пользователь
        /// </summary>
        SystemUser user;
        /// <summary>
        /// параметры бд
        /// </summary>
        DbContextOptions<RepairDepotContext> dbOptions;

        public AuthorizationVM() { }
        public AuthorizationVM(DbContextOptions<RepairDepotContext> dbOptions)
        {
            this.dbOptions = dbOptions;
        }

        RelayCommand authorize;
        public RelayCommand Authorize
        {
            get
            {
                return authorize ??= new RelayCommand(async obj =>
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
                    user = new SystemUser(this.dbOptions);
                    Task<bool> authTask = user.AuthorizationAsync(this.InputLogin, InputPassword);
                    bool authResult = await authTask;
                    if (authResult)
                    {
                        this.AuthorizationStatus = true;
                        msg = "Авторизация выполнена успешно! \n" +
                        $"Добро пожаловать, {user.User.FirstName}!";
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
            return (string.IsNullOrEmpty(InputLogin) || string.IsNullOrEmpty(InputPassword));
        }
    }
}
