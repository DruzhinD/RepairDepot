using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace RepairDepot.Model
{
    /// <summary>
    /// отвечает за подключение к бд
    /// </summary>
    public class DatabaseConnection
    {
        private NpgsqlConnection connection;


        /// <summary>
        /// инициализация бд при помощи конфигурации
        /// </summary>
        /// <param name="config"></param>
        public DatabaseConnection(Config config)
        {
            string conStr = $"Server={config["server"]};Port={config["port"]};Database={config["dbName"]}" +
                $";Username={config["username"]};Password={config["password"]}";
            connection = new NpgsqlConnection(conStr);
        }
    }
}
