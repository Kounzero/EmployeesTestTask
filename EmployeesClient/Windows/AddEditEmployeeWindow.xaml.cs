using EmployeesClient.Models.Employees;
using EmployeesClient.Models.Genders;
using EmployeesClient.Models.Positions;
using EmployeesClient.Models.Subdivisions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace EmployeesClient.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEditEmployeeWindow.xaml
    /// </summary>
    public partial class AddEditEmployeeWindow : Window
    {
        public AddEditEmployeeWindow()
        {
            InitializeComponent();

            Title = "Добавление нового сотрудника";

            DataContext = new AddEmployeeDto();
            (DataContext as AddEmployeeDto).BirthDate = DateTime.Now.AddYears(-18);

            LoadComboBoxes();
        }

        public AddEditEmployeeWindow(EditEmployeeDto editEmployeeDto)
        {
            InitializeComponent();

            Title = "Изменение данных сотрудника №" + editEmployeeDto.ID;

            DataContext = editEmployeeDto;

            LoadComboBoxes();
        }

        private void LoadComboBoxes()
        {
            var client = new HttpClient();
            var response = client.GetAsync($"{App.MainUri}Genders").Result;

            if (response.IsSuccessStatusCode)
            {
                var genders = JsonConvert.DeserializeObject<List<GenderDto>>(response.Content.ReadAsStringAsync().Result);
                GendersCombobox.ItemsSource = genders;
            }

            response = client.GetAsync($"{App.MainUri}Positions").Result;

            if (response.IsSuccessStatusCode)
            {
                var positions = JsonConvert.DeserializeObject<List<PositionDto>>(response.Content.ReadAsStringAsync().Result);
                PositionsCombobox.ItemsSource = positions;
            }

            response = client.GetAsync($"{App.MainUri}Subdivisions").Result;

            if (response.IsSuccessStatusCode)
            {
                var subdivisions = JsonConvert.DeserializeObject<List<SubdivisionDto>>(response.Content.ReadAsStringAsync().Result);
                SubdivisionsCombobox.ItemsSource = subdivisions;
            }
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FullNameTextBox.Text) ||
                BirthDatePicker.SelectedDate == null ||
                GendersCombobox.SelectedIndex == -1 ||
                PositionsCombobox.SelectedIndex == -1 ||
                SubdivisionsCombobox.SelectedIndex == -1)
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var client = new HttpClient();

            var response = new HttpResponseMessage();

            if (DataContext is EditEmployeeDto editEmployeeDto)
            {
                response = client.SendAsync(new HttpRequestMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(editEmployeeDto), Encoding.UTF8, "application/json"),
                    Method = HttpMethod.Put,
                    RequestUri = new Uri($"{App.MainUri}Employees")
                }).Result;
            }
            else
            {
                response = client.SendAsync(new HttpRequestMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(DataContext as AddEmployeeDto), Encoding.UTF8, "application/json"),
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{App.MainUri}Employees")
                }).Result;
            }

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Данные успешно обновлены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show($"При обновлении данных произошла следующая ошибка:\n{response.StatusCode} - {response.Content.ReadAsStringAsync().Result}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
