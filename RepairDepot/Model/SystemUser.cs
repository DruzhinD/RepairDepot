using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using RepairDepot.ViewModel;
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
        public Permission Privileges { get => user.Permission; }

        public SystemUser()
        {
        }

        #region Авторизация/Регистрация
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <returns>true - авторизация успешна, иначе false</returns>
        public async Task<bool> AuthorizationAsync(string login, string password)
        {

            using (RepairDepotContext dbContext = new RepairDepotContext(CommonData.DbContextOptions))
            {
                //идентификация (логин)
                User user = await dbContext.Users
                    .Include(p => p.Permission)
                    .FirstOrDefaultAsync(x => x.Login == login);
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
            using (RepairDepotContext dbContext = new(CommonData.DbContextOptions))
            {
                //вернет значение, если пользователь с таким логином уже существует
                User user = await dbContext.Users.FirstOrDefaultAsync(x => x.Login == notExistUser.Login);
                if (user != null)
                    return false;
                //заменяем явный пароль на его хеш и таким образом отправляем в базу
                notExistUser.Password = PasswordHasher.Hash(notExistUser.Password);

                dbContext.Attach(notExistUser);
                await dbContext.Users.AddAsync(notExistUser);
                await dbContext.SaveChangesAsync();
                return true;
            }
        }
        #endregion
    }
}
