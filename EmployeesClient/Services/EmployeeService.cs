using EmployeesClient.Models.Employees;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesClient.Services
{
    public class EmployeeService : IEmployeeService
    {
        public async Task<HttpResponseMessage> AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var client = new HttpClient();

            var response = await client.SendAsync(new HttpRequestMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(addEmployeeDto), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{App.AppConfig.GetCurrentConnectionString()}Employees")
            });

            return response;
        }

        public async Task<HttpResponseMessage> DeleteEmployee(int id)
        {
            var client = new HttpClient();
            var response = await client.DeleteAsync($"{App.AppConfig.GetCurrentConnectionString()}Employees/{id}");

            return response;
        }

        public async Task<HttpResponseMessage> EditEmployee(EditEmployeeDto editEmployeeDto)
        {
            var client = new HttpClient();

            var response = await client.SendAsync(new HttpRequestMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(editEmployeeDto), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{App.AppConfig.GetCurrentConnectionString()}Employees")
            });

            return response;
        }

        public async Task<List<EmployeeDto>> GetEmployees(int subdivisionId)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"{App.AppConfig.GetCurrentConnectionString()}Employees?subdivisionId={subdivisionId}");
            var responseString = await response.Content.ReadAsStringAsync();
            var employees = JsonConvert.DeserializeObject<List<EmployeeDto>>(responseString);

            return employees;
        }
    }
}
