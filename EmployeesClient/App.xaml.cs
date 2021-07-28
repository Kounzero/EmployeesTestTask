using EmployeesClient.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeesClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly string MainUri = "http://localhost:5002/api/";

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow = new EmployeesWindow();
            MainWindow.Show();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show("Произошла непредвиденная ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
