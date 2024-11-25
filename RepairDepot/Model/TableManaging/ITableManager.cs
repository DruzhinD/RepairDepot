using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairDepot.Model.TableManaging
{
    /// <summary>
    /// Работа с конкретной таблицей базы данных в рамках контекста предметной области
    /// </summary>
    public interface ITableManager<T> where T : BaseModel
    {
        /// <summary>
        /// Выгрузка данных из таблицы, соответствующей модели <see cref="T"/> 
        /// </summary>
        Task<ObservableCollection<T>> LoadData();


        /// <summary>
        /// Обновить данные
        /// </summary>
        /// <param name="data"></param>
        Task SaveData(IEnumerable<T> data);
    }
}
