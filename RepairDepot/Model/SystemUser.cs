using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
#nullable disable
namespace RepairDepot.Model
{
    /// <summary>
    /// объект пользователя в ИС депо
    /// </summary>
    public class SystemUser
    {
        protected User user;
        protected RepairDepotContext dbContext;
        public Permission Privileges { get => user.Permission; }

        public SystemUser(RepairDepotContext dbContext)
        {
            this.dbContext = dbContext; 
        }

        #region Авторизация/Регистрация
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <returns>true - авторизация успешна, иначе false</returns>
        public async Task<bool> Authorization(string login, string password)
        {
            //идентификация (логин)
            bool result = await this.IdentifyAsync(login);
            if (!result)
                return false;
            //аутентификация (пароль)
            result = this.Authentificate(password);
            return result;
        }

        /// <summary>
        /// Идентификация, т.е. сравнение логина
        /// </summary>
        private async Task<bool> IdentifyAsync(string login)
        {
            User result = await dbContext.Users.FirstAsync(x => x.Login == login);
            if (result == null)
                return false;
            user = result;
            return true;
        }

        /// <summary>
        /// Аутентификация, т.е. сравнение пароля при гарантированном совпадении логина
        /// </summary>
        public bool Authentificate(string password)
        {
            return user.Password == password;
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="notExistUser">объект User с уникальным логином, которого не существует в бд</param>
        /// <returns>true - регистрация успешна</returns>
        public async Task<bool> RegisterAsync(User notExistUser)
        {
            //вернет значение, если пользователь с таким логином уже существует
            User user = await this.dbContext.Users.FirstAsync(x => x.Login == notExistUser.Login);
            if (user != null)
                return false;
            await dbContext.Users.AddAsync(notExistUser);
            return true;
        }
        #endregion
    }
}
