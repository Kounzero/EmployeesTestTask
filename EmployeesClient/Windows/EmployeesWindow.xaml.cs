using EmployeesClient.Models.Employees;
using EmployeesClient.Models.Subdivisions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EmployeesClient.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class EmployeesWindow : Window
    {
        List<SubdivisionDto> Subdivisions = new List<SubdivisionDto>();
        List<EmployeeDto> Employees = new List<EmployeeDto>();


        public EmployeesWindow()
        {
            InitializeComponent();

            RefreshSubdivisions();
        }

        List<SubdivisionDto> LoadSubdivisions(int? parentId)
        {
            var client = new HttpClient();
            var response = client.GetAsync($"{App.MainUri}Subdivisions/GetSubdivisions{(parentId == null ? "" : "?parentSubdivisionId=" + parentId)}").Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            var data = JsonConvert.DeserializeObject<List<SubdivisionDto>>(responseString);

            return data;
        }

        void RefreshSubdivisions()
        {
            Subdivisions = LoadSubdivisions(null);

            foreach (var subdivision in Subdivisions)
            {
                subdivision.LeftMargin = 0;
            }

            SubdivisionsListView.ItemsSource = Subdivisions;
            SubdivisionsListView.SelectedIndex = 0;
        }

        void LoadEmployees(int subdivisionId)
        {
            var client = new HttpClient();
            var response = client.GetAsync($"{App.MainUri}Employees?subdivisionId={subdivisionId}").Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            var data = JsonConvert.DeserializeObject<List<EmployeeDto>>(responseString);
            Employees = data;

            EmployeesDataGrid.ItemsSource = Employees;
        }

        private void EditEmployee()
        {
            if (EmployeesDataGrid.SelectedItem is EmployeeDto employeeDto)
            {
                var editableEmployee = new EditEmployeeDto()
                {
                    BirthDate = employeeDto.BirthDate,
                    FullName = employeeDto.FullName,
                    GenderID = employeeDto.GenderID,
                    HasDrivingLicense = employeeDto.HasDrivingLicense,
                    ID = employeeDto.ID,
                    PositionID = employeeDto.PositionID,
                    SubdivisionID = employeeDto.SubdivisionID
                };

                if (new AddEditEmployeeWindow(editableEmployee).ShowDialog() == true)
                {
                    LoadEmployees((SubdivisionsListView.SelectedItem as SubdivisionDto).id);
                }

            }
        }

        private void SubdivisionsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SubdivisionsListView.SelectedItem is SubdivisionDto selectedSubdivision)
            {
                LoadEmployees(selectedSubdivision.id);
            }
        }

        private void BtnOpenChildren_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var parentSubdivision = button.DataContext as SubdivisionDto;

            parentSubdivision.Opened = !parentSubdivision.Opened;
            var startIndex = Subdivisions.IndexOf(parentSubdivision) + 1;

            if (parentSubdivision.Opened)
            {
                var children = LoadSubdivisions(parentSubdivision.id);

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
            if (new AddEditSubdivisionWindow().ShowDialog() == true)
            {
                RefreshSubdivisions();
            }
        }

        private void BtnEditSubdivision_Click(object sender, RoutedEventArgs e)
        {
            if (SubdivisionsListView.SelectedItem is SubdivisionDto subdivisionDto)
            {
                var editableSubdivision = new EditSubdivisionDto()
                {
                    ID = subdivisionDto.id,
                    Description = subdivisionDto.description,
                    ParentSubdivisionID = subdivisionDto.parentSubdivisionID,
                    Title = subdivisionDto.title
                };

                if (new AddEditSubdivisionWindow(editableSubdivision).ShowDialog() == true)
                {
                    RefreshSubdivisions();
                }
            }
        }

        private void BtnDeleteSubdivision_Click(object sender, RoutedEventArgs e)
        {
            if (SubdivisionsListView.SelectedItem is SubdivisionDto subdivision)
            {
                if (!subdivision.hasChildren || MessageBox.Show("При удалении подразделения также удалятся все его дочерние подразделения, сотрудники и сотрудники дочерних подразделений",
                    "Продолжить?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var client = new HttpClient();
                    var response = client.DeleteAsync($"{App.MainUri}Subdivisions/{subdivision.id}").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Удаление успешно", "Успех", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                        RefreshSubdivisions();
                    }
                    else
                    {
                        MessageBox.Show("Произошла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите Подразделение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (new AddEditEmployeeWindow().ShowDialog() == true)
            {
                if (SubdivisionsListView.SelectedIndex == -1)
                {
                    EmployeesDataGrid.ItemsSource = null;
                    return;
                }
                LoadEmployees((SubdivisionsListView.SelectedItem as SubdivisionDto).id);
            }
        }

        private void BtnEditEmployee_Click(object sender, RoutedEventArgs e)
        {
            EditEmployee();
        }

        private void BtnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesDataGrid.SelectedItem is EmployeeDto employee)
            {
                if (MessageBox.Show("Удалить выббранного сотрудника?", "Продолжить?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var client = new HttpClient();
                    var response = client.DeleteAsync($"{App.MainUri}Employees/{employee.ID}").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Удаление успешно", "Успех", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                        LoadEmployees((SubdivisionsListView.SelectedItem as SubdivisionDto).id);
                    }
                    else
                    {
                        MessageBox.Show("Произошла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
