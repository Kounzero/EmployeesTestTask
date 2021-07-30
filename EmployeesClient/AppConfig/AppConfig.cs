using System.Collections.Generic;

namespace EmployeesClient.AppConfig
{
    /// <summary>
    /// Модель конфигурации приложения
    /// </summary>
    public class AppConfig
    {
        /// <summary>
        /// Список строк подключения
        /// </summary>
        public List<string> ConnectionStrings { get; set; }

        /// <summary>
        /// Метод для получения актуальной строки подключения
        /// </summary>
        /// <returns></returns>
        public string GetCurrentConnectionString()
        {
            return ConnectionStrings[0];
        }
    }
}
