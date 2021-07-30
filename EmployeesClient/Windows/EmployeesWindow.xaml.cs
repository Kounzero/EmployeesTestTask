using EmployeesClient.Models.Employees;
using EmployeesClient.Models.Subdivisions;
using EmployeesClient.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmployeesClient.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class EmployeesWindow : Window
    {
        private List<SubdivisionDto> Subdivisions = new List<SubdivisionDto>();
        private IEmployeeService EmployeeService { get; set; }
        private ISubdivisionService SubdivisionService { get; set; }

        public EmployeesWindow()
        {
            InitializeComponent();

            EmployeeService = new EmployeeService();
            SubdivisionService = new SubdivisionService();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshSubdivisions();
        }

        private async Task RefreshSubdivisions()
        {
            try
            {
                Subdivisions = await SubdivisionService.GetSubdivisions(null);

                SubdivisionsListView.ItemsSource = Subdivisions;
                SubdivisionsListView.SelectedIndex = 0;
            }
            catch (Exception error)
            {
                MessageBox.Show($"Неизвестная ошибка. Сообщение:\n{error.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
        }

        private async void LoadEmployees(int subdivisionId)
        {
            try
            {
                EmployeesDataGrid.ItemsSource = await EmployeeService.GetEmployees(subdivisionId);
            }
            catch (Exception error)
            {
                MessageBox.Show($"Неизвестная ошибка. Сообщение:\n{error.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
        }

        private void EditEmployee()
        {
            try
            {
                if (!(EmployeesDataGrid.SelectedItem is EmployeeDto employeeDto))
                {
                    return;
                }

                var editableEmployee = new EditEmployeeDto()
                {
                    BirthDate = employeeDto.BirthDate,
                    FullName = employeeDto.FullName,
                    GenderId = employeeDto.GenderId,
                    HasDrivingLicense = employeeDto.HasDrivingLicense,
                    Id = employeeDto.Id,
                    PositionId = employeeDto.PositionId,
                    SubdivisionId = employeeDto.SubdivisionId
                };

                if (new AddEditEmployeeWindow(editableEmployee).ShowDialog().Value)
                {
                    LoadEmployees((SubdivisionsListView.SelectedItem as SubdivisionDto).Id);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show($"Неизвестная ошибка. Сообщение:\n{error.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
        }

        private async Task OpenSubdivision(SubdivisionDto parentSubdivision, int startIndex)
        {
            var children = await SubdivisionService.GetSubdivisions(parentSubdivision.Id);

            foreach (var child in children)
            {
                child.LeftMargin = parentSubdivision.LeftMargin + 10;
            }

            Subdivisions.InsertRange(startIndex, children);
        }

        private void CloseSubdivision(SubdivisionDto parentSubdivision, int startIndex)
        {
            var endIndex = Subdivisions.Count;

            for (int i = startIndex; i < Subdivisions.Count; i++)
            {
                if (Subdivisions[i].LeftMargin <= parentSubdivision.LeftMargin)
                {
                    endIndex = i;

                    break;
                }
            }

            Subdivisions.RemoveRange(startIndex, endIndex - startIndex);
        }

        private void SubdivisionsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (SubdivisionsListView.SelectedItem is SubdivisionDto selectedSubdivision)
                {
                    LoadEmployees(selectedSubdivision.Id);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show($"Неизвестная ошибка. Сообщение:\n{error.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
        }

        private async void BtnOpenChildren_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var parentSubdivision = (sender as Button).DataContext as SubdivisionDto;
                parentSubdivision.Opening = !parentSubdivision.Opening;
                var startIndex = Subdivisions.IndexOf(parentSubdivision) + 1;

                if (parentSubdivision.Opening)
                {
                    await OpenSubdivision(parentSubdivision, startIndex);
                }
                else
                {
                    CloseSubdivision(parentSubdivision, startIndex);
                }

                SubdivisionsListView.ItemsSource = null;
                SubdivisionsListView.ItemsSource = Subdivisions;
            }
            catch (Exception error)
            {
                MessageBox.Show($"Неизвестная ошибка. Сообщение:\n{error.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
        }

        private void EmployeesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditEmployee();
        }

        private async void BtnAddSubdivision_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (new AddEditSubdivisionWindow().ShowDialog().Value)
                {
                    await RefreshSubdivisions();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show($"Неизвестная ошибка. Сообщение:\n{error.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
        }

        private async void BtnEditSubdivision_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(SubdivisionsListView.SelectedItem is SubdivisionDto subdivisionDto))
                {
                    return;
                }

                var editableSubdivision = new EditSubdivisionDto()
                {
                    Id = subdivisionDto.Id,
                    Description = subdivisionDto.Description,
                    ParentId = subdivisionDto.ParentId,
                    Title = subdivisionDto.Title
                };

                if (new AddEditSubdivisionWindow(editableSubdivision).ShowDialog().Value)
                {
                    await RefreshSubdivisions();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show($"Неизвестная ошибка. Сообщение:\n{error.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
        }

        private async void BtnDeleteSubdivision_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(SubdivisionsListView.SelectedItem is SubdivisionDto subdivision))
                {
                    MessageBox.Show("Выберите Подразделение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                    return;
                }

                if (subdivision.HasChildren && MessageBox.Show("При удалении подразделения также удалятся все его дочерние подразделения, сотрудники и сотрудники дочерних подразделений",
                    "Продолжить?", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                {
                    return;
                }

                var response = await SubdivisionService.DeleteSubdivision(subdivision.Id);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Произошла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                    return;
                }

                MessageBox.Show("Удаление успешно", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                await RefreshSubdivisions();
            }
            catch (Exception error)
            {
                MessageBox.Show($"Неизвестная ошибка. Сообщение:\n{error.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
        }

        private void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!new AddEditEmployeeWindow().ShowDialog().Value)
                {
                    return;
                }

                if (SubdivisionsListView.SelectedIndex == -1)
                {
                    EmployeesDataGrid.ItemsSource = null;

                    return;
                }

                LoadEmployees((SubdivisionsListView.SelectedItem as SubdivisionDto).Id);
            }
            catch (Exception error)
            {
                MessageBox.Show($"Неизвестная ошибка. Сообщение:\n{error.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
        }

        private void BtnEditEmployee_Click(object sender, RoutedEventArgs e)
        {
            EditEmployee();
        }

        private async void BtnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(EmployeesDataGrid.SelectedItem is EmployeeDto employee))
                {
                    MessageBox.Show("Выберите сотрудника", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                    return;
                }

                if (MessageBox.Show("Удалить выббранного сотрудника?", "Продолжить?", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                {
                    return;
                }

                var response = await EmployeeService.DeleteEmployee(employee.Id);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Произошла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                    return;
                }

                MessageBox.Show("Удаление успешно", "Успех", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                LoadEmployees((SubdivisionsListView.SelectedItem as SubdivisionDto).Id);
            }
            catch (Exception error)
            {
                MessageBox.Show($"Неизвестная ошибка. Сообщение:\n{error.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
        }
    }
}
