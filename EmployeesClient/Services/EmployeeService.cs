using EmployeesClient.Models.Employees;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesClient.Services
{
    /// <inheritdoc cref="IEmployeeService"/>
    public class EmployeeService : IEmployeeService
    {
        public async Task<HttpResponseMessage> AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var response = await App.Client.SendAsync(new HttpRequestMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(addEmployeeDto), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{App.AppConfig.GetConnectionString()}Employees")
            });

            return response;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> DeleteEmployee(int id)
        {
            var response = await App.Client.DeleteAsync($"{App.AppConfig.GetConnectionString()}Employees/{id}");

            return response;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> EditEmployee(EditEmployeeDto editEmployeeDto)
        {
            var response = await App.Client.SendAsync(new HttpRequestMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(editEmployeeDto), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{App.AppConfig.GetConnectionString()}Employees")
            });

            return response;
        }

        /// <inheritdoc/>
        public async Task<List<EmployeeDto>> GetEmployees(int subdivisionId)
        {
            var response = await App.Client.GetAsync($"{App.AppConfig.GetConnectionString()}Employees?subdivisionId={subdivisionId}");
            var responseString = await response.Content.ReadAsStringAsync();
            var employees = JsonConvert.DeserializeObject<List<EmployeeDto>>(responseString);

            return employees;
        }
    }
}
