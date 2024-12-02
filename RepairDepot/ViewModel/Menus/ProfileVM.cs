using RepairDepot.Model;
using RepairDepot.ViewModel.Commands;
using RepairDepot.ViewModel.DefinitionVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairDepot.ViewModel
{
    public class ProfileVM : BasePageVM
    {
        public override string Name => "Профиль";

        #region Свойства
        public SystemUser UserInfo { get => userInfo; set { userInfo = value; OnPropertyChanged(); } }
        SystemUser userInfo;

        /// <summary>
        /// статус авторизации
        /// </summary>
        public string Status { get => status; set { status = value; OnPropertyChanged(); } }
        string status = string.Empty;

        /// <summary>
        /// департамент, которому принадлежит пользователь/сотрудник
        /// </summary>
        public string Department { get => department; set { department = value; OnPropertyChanged(); } }
        string department = string.Empty;
        #endregion

        #region Команды
        public AsyncCommand Refresh => refresh ??= new AsyncCommand(async (obj) => await Initialize());
        AsyncCommand refresh;

        #endregion

        public ProfileVM() { }

        public async override Task Initialize()
        {
            UserInfo = CommonData.User;
            if (UserInfo == null || !UserInfo.AuthStatus)
            {
                Status = "Требуется вход";
                return;
            }
            else
            {
                Status = "Вход выполнен";
            }

            if (UserInfo.Privileges.Admin)
                Department = "Администратор";
            else if (UserInfo.Privileges.TechnicalDepartment)
                Department = "Технический отдел";
            else if (UserInfo.Privileges.PlaningDepartment)
                Department = "Отдел планирования";
            else if (UserInfo.Privileges.StaffDepartment)
                Department = "Отдел кадров";
            else if (UserInfo.Privileges.RepairDepartment)
                Department = "Ремонтный отдел";
        }
    }
}
