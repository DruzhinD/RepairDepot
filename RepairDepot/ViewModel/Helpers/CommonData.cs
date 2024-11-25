using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using RepairDepot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairDepot.ViewModel
{
    /// <summary>
    /// Сведения общие для всех ViewModel
    /// </summary>
    public static class CommonData
    {
        static SystemUser user;

        /// <summary>
        /// Авторизованный пользователь системы
        /// </summary>
        public static SystemUser User { get => user; set => user = value; }
    }
}
