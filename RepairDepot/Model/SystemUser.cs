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
        public User User { get => user; }
        protected DbContextOptions<RepairDepotContext> dbContextOptions;
        RepairDepotContext dbContext; //удалить
        public Permission Privileges { get => user.Permission; }

        public SystemUser(DbContextOptions<RepairDepotContext> dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }

        #region Авторизация/Регистрация
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <returns>true - авторизация успешна, иначе false</returns>
        public async Task<bool> AuthorizationAsync(string login, string password)
        {

            using (RepairDepotContext dbContext = new RepairDepotContext(dbContextOptions))
            {
                //идентификация (логин)
                User user = await dbContext.Users.FirstOrDefaultAsync(x => x.Login == login);
                if (user == null)
                    return false;
                //аутентификация (пароль)
                bool result = PasswordHasher.Validate(user.Password, password);
                //если пароль введен верно, то сохраняем сведения о пользователе
                if (result)
                    this.user = user;
                return result;
            }
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="notExistUser">объект User с уникальным логином, которого не существует в бд</param>
        /// <returns>true - регистрация успешна</returns>
        public async Task<bool> RegisterAsync(User notExistUser)
        {
            //вернет значение, если пользователь с таким логином уже существует
            User user = await this.dbContext.Users.FirstOrDefaultAsync(x => x.Login == notExistUser.Login);
            if (user != null)
                return false;
            //заменяем явный пароль на его хеш и таким образом отправляем в базу
            user.Password = PasswordHasher.Hash(user.Password);

            await dbContext.Users.AddAsync(notExistUser);
            await dbContext.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
