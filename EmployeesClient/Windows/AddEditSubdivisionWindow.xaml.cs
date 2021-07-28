using EmployeesClient.Models.Subdivisions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
    /// Логика взаимодействия для AddEditSubdivisionWindow.xaml
    /// </summary>
    public partial class AddEditSubdivisionWindow : Window
    {
        List<SubdivisionDto> Subdivisions = new List<SubdivisionDto>();

        public AddEditSubdivisionWindow()
        {
            InitializeComponent();

            Title = "Добавление подразделения";

            LoadData();

            ParentComboBox.ItemsSource = Subdivisions;

            DataContext = new AddSubdivisionDto();
        }

        public AddEditSubdivisionWindow(EditSubdivisionDto editSubdivisionDto)
        {
            InitializeComponent();

            Title = "Изменение данных о подразделении №" + editSubdivisionDto.ID;

            LoadData();

            Subdivisions.Remove(Subdivisions.FirstOrDefault(x=>x.id == editSubdivisionDto.ID));
            ParentComboBox.ItemsSource = Subdivisions;

            DataContext = editSubdivisionDto;
        }

        void LoadData()
        {
            var client = new HttpClient();
            var response = client.GetAsync($"{App.MainUri}Subdivisions").Result;

            if (response.IsSuccessStatusCode)
            {
                Subdivisions = JsonConvert.DeserializeObject<List<SubdivisionDto>>(response.Content.ReadAsStringAsync().Result);
            }
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TitleTextBox.Text))
            {
                MessageBox.Show("Введите название подразделения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var client = new HttpClient();

            var response = new HttpResponseMessage();

            if (DataContext is EditSubdivisionDto editSubdivisionDto)
            {
                response = client.SendAsync(new HttpRequestMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(editSubdivisionDto), Encoding.UTF8, "application/json"),
                    Method = HttpMethod.Put,
                    RequestUri = new Uri($"{App.MainUri}Subdivisions")
                }).Result;
            }
            else
            {
                response = client.SendAsync(new HttpRequestMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(DataContext as AddSubdivisionDto), Encoding.UTF8, "application/json"),
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{App.MainUri}Subdivisions")
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

        private void BtnMakeRoot_Click(object sender, RoutedEventArgs e)
        {
            ParentComboBox.SelectedIndex = -1;
        }
    }
}
