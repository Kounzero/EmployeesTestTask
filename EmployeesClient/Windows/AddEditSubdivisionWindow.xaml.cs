using EmployeesClient.Models.Subdivisions;
using EmployeesClient.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeesClient.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEditSubdivisionWindow.xaml
    /// </summary>
    public partial class AddEditSubdivisionWindow : Window
    {
        private List<SubdivisionDto> Subdivisions = new List<SubdivisionDto>();
        private ISubdivisionService SubdivisionService { get; set; }

        public AddEditSubdivisionWindow()
        {
            InitializeComponent();

            Title = "Добавление подразделения";

            DataContext = new AddSubdivisionDto();
        }

        public AddEditSubdivisionWindow(EditSubdivisionDto editSubdivisionDto)
        {
            InitializeComponent();

            Title = "Изменение данных о подразделении №" + editSubdivisionDto.Id;

            DataContext = editSubdivisionDto;
        }

        private async void Window_LoadedAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                SubdivisionService = new SubdivisionService();
                ParentComboBox.ItemsSource = await SubdivisionService.GetAllSubdivisions();

                if (DataContext is EditSubdivisionDto editSubdivisionDto)
                {
                    ParentComboBox.Items.Remove(editSubdivisionDto);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show($"Неизвестная ошибка. Сообщение:\n{error.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
        }

        private async void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TitleTextBox.Text))
            {
                MessageBox.Show("Введите название подразделения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            var response = new HttpResponseMessage();

            if (DataContext is EditSubdivisionDto editSubdivisionDto)
            {
                response = await SubdivisionService.EditSubdivision(editSubdivisionDto);
            }
            else
            {
                response = await SubdivisionService.AddSubdivision(DataContext as AddSubdivisionDto);
            }

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show($"При обновлении данных произошла следующая ошибка:\n{response.StatusCode} - {response.Content.ReadAsStringAsync().Result}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Данные успешно обновлены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void BtnMakeRoot_Click(object sender, RoutedEventArgs e)
        {
            ParentComboBox.SelectedIndex = -1;
        }
    }
}
