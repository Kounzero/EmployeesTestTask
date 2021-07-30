using EmployeesClient.Windows;
using Newtonsoft.Json;
using System.IO;
using System.Windows;

namespace EmployeesClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static AppConfig.AppConfig AppConfig;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ConfigureApp();

            MainWindow = new EmployeesWindow();
            MainWindow.Show();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show("Произошла непредвиденная ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ConfigureApp()
        {
            try
            {
                var configurationString = File.ReadAllText("./AppConfig/appconfig.json");
                var configuration = JsonConvert.DeserializeObject<AppConfig.AppConfig>(configurationString);

                AppConfig = configuration;
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("Файл конфигурации не обнаружен");
            }
        }
    }
}
