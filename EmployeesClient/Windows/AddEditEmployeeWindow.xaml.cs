using EmployeesClient.Models.Employees;
using EmployeesClient.Models.Genders;
using EmployeesClient.Models.Positions;
using EmployeesClient.Models.Subdivisions;
using EmployeesClient.Services;
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
        private IEmployeeService EmployeeService { get; set; }
        private IGenderService GenderService { get; set; }
        private IPositionService PositionService { get; set; }
        private ISubdivisionService SubdivisionService { get; set; }

        public AddEditEmployeeWindow()
        {
            InitializeComponent();

            Title = "Добавление нового сотрудника";

            DataContext = new AddEmployeeDto();
            (DataContext as AddEmployeeDto).BirthDate = DateTime.Now.AddYears(-18);
        }

        public AddEditEmployeeWindow(EditEmployeeDto editEmployeeDto)
        {
            InitializeComponent();

            Title = "Изменение данных сотрудника №" + editEmployeeDto.Id;

            DataContext = editEmployeeDto;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EmployeeService = new EmployeeService();
            GenderService = new GenderService();
            PositionService = new PositionService();
            SubdivisionService = new SubdivisionService();
            LoadComboBoxes();
        }

        private async void LoadComboBoxes()
        {
            try
            {
                GendersCombobox.ItemsSource = await GenderService.GetGenders();
                PositionsCombobox.ItemsSource = await PositionService.GetPositions();
                SubdivisionsCombobox.ItemsSource = await SubdivisionService.GetAllSubdivisions();
            }
            catch (HttpRequestException error)
            {
                MessageBox.Show($"Ошибка соединения с сервером: {error.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
            catch (Exception error)
            {
                MessageBox.Show($"Неизвестная ошибка. Сообщение:\n{error.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
        }

        private async void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var isDataIsValid =
                    string.IsNullOrEmpty(FullNameTextBox.Text) ||
                    BirthDatePicker.SelectedDate == null ||
                    GendersCombobox.SelectedIndex == -1 ||
                    PositionsCombobox.SelectedIndex == -1 ||
                    SubdivisionsCombobox.SelectedIndex == -1;

                if (!isDataIsValid)
                {
                    MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                    return;
                }

                HttpResponseMessage response;

                if (DataContext is EditEmployeeDto editEmployeeDto)
                {
                    response = await EmployeeService.EditEmployee(editEmployeeDto);
                }
                else
                {
                    response = await EmployeeService.AddEmployee(DataContext as AddEmployeeDto);
                }

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"При обновлении данных произошла следующая ошибка:\n{response.StatusCode} - {response.Content.ReadAsStringAsync().Result}", 
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                    return;
                }

                MessageBox.Show("Данные успешно обновлены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Ошибка соединения с сервером", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
            catch (Exception error)
            {
                MessageBox.Show($"Неизвестная ошибка. Сообщение:\n{error.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
