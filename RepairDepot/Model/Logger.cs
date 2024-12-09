using System.IO;

namespace RepairDepot.Model
{
    /// <summary>
    /// Логгирование.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Записать лог с сообщением message
        /// </summary>
        /// <param name="message">Информация для лога</param>
        public static void Log(string message)
        {
            message = FormatLog(message);
            File.AppendAllText(Config.GetInstanse().LogPath, message);
        }


        /// <summary>
        /// Форматировать лог с сообщением message
        /// </summary>
        /// <param name="message">Информация для лога</param>
        public static string FormatLog(string message)
        {
            //добавляем время лога
            string time = DateTime.Now.ToString("dd.MM.y HH:mm:ss");
            if (message[message.Length - 1] != '\n')
                message = time + "\t" + message + '\n';
            else
                message = time + "\t" + message;
            return message;
        }
    }
}
