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
            //добавляем время лога
            string time = DateTime.Now.ToString("dd.MM.y HH:mm:ss");
            if (message[message.Length - 1] != '\n')
                message = time + "\t" + message + '\n';
            else
                message = time + "\t" + message;
            File.AppendAllText(Config.GetInstanse().LogPath, message);
        }
    }
}
