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
        public string ConnectionString { get; set; }

        /// <summary>
        /// Метод для получения актуальной строки подключения
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            return ConnectionString;
        }
    }
}
