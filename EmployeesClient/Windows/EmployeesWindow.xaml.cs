using EmployeesClient.Models.Employees;
using EmployeesClient.Models.Subdivisions;
using EmployeesClient.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
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

            RefreshSubdivisions();
        }

        private async void RefreshSubdivisions()
        {
            Subdivisions = await SubdivisionService.GetSubdivisions(null);

            SubdivisionsListView.ItemsSource = Subdivisions;
            SubdivisionsListView.SelectedIndex = 0;
        }

        private async void LoadEmployees(int subdivisionId)
        {
            EmployeesDataGrid.ItemsSource = await EmployeeService.GetEmployees(subdivisionId);
        }

        private void EditEmployee()
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

        private void SubdivisionsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SubdivisionsListView.SelectedItem is SubdivisionDto selectedSubdivision)
            {
                LoadEmployees(selectedSubdivision.Id);
            }
        }

        private async void BtnOpenChildren_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var parentSubdivision = button.DataContext as SubdivisionDto;
            parentSubdivision.Opened = !parentSubdivision.Opened;
            var startIndex = Subdivisions.IndexOf(parentSubdivision) + 1;

            if (parentSubdivision.Opened)
            {
                var children = await SubdivisionService.GetSubdivisions(parentSubdivision.Id);

                foreach (var child in children)
                {
                    child.LeftMargin = parentSubdivision.LeftMargin + 10;
                }

                Subdivisions.InsertRange(startIndex, children);
            }
            else
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

            SubdivisionsListView.ItemsSource = null;
            SubdivisionsListView.ItemsSource = Subdivisions;
        }

        private void EmployeesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditEmployee();
        }

        private void BtnAddSubdivision_Click(object sender, RoutedEventArgs e)
        {
            if (new AddEditSubdivisionWindow().ShowDialog().Value)
            {
                RefreshSubdivisions();
            }
        }

        private void BtnEditSubdivision_Click(object sender, RoutedEventArgs e)
        {
            if (!(SubdivisionsListView.SelectedItem is SubdivisionDto subdivisionDto))
            {
                return;
            }

            var editableSubdivision = new EditSubdivisionDto()
            {
                Id = subdivisionDto.Id,
                Description = subdivisionDto.Description,
                ParentSubdivisionId = subdivisionDto.ParentSubdivisionID,
                Title = subdivisionDto.Title
            };

            if (new AddEditSubdivisionWindow(editableSubdivision).ShowDialog().Value)
            {
                RefreshSubdivisions();
            }
        }

        private async void BtnDeleteSubdivision_Click(object sender, RoutedEventArgs e)
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

            RefreshSubdivisions();
        }

        private void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
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

        private void BtnEditEmployee_Click(object sender, RoutedEventArgs e)
        {
            EditEmployee();
        }

        private async void BtnDeleteEmployee_Click(object sender, RoutedEventArgs e)
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
    }
}
