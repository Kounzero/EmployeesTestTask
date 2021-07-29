using EmployeesClient.Models.Subdivisions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesClient.Services
{
    public class SubdivisionService : ISubdivisionService
    {
        public async Task<HttpResponseMessage> AddSubdivision(AddSubdivisionDto addSubdivisionDto)
        {
            var client = new HttpClient();

            var response = await client.SendAsync(new HttpRequestMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(addSubdivisionDto), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{App.AppConfig.GetCurrentConnectionString()}Subdivisions")
            });

            return response;
        }

        public async Task<HttpResponseMessage> DeleteSubdivision(int id)
        {
            var client = new HttpClient();
            var response = await client.DeleteAsync($"{App.AppConfig.GetCurrentConnectionString()}Subdivisions/{id}");

            return response;
        }

        public async Task<HttpResponseMessage> EditSubdivision(EditSubdivisionDto editSubdivisionDto)
        {
            var client = new HttpClient();

            var response = await client.SendAsync(new HttpRequestMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(editSubdivisionDto), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{App.AppConfig.GetCurrentConnectionString()}Subdivisions")
            });

            return response;
        }

        public async Task<List<SubdivisionDto>> GetAllSubdivisions()
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"{App.AppConfig.GetCurrentConnectionString()}Subdivisions");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Не удалось загрузить подразделения");
            }

            var subdivisions = JsonConvert.DeserializeObject<List<SubdivisionDto>>(response.Content.ReadAsStringAsync().Result);
            
            return subdivisions;
        }

        public async Task<List<SubdivisionDto>> GetSubdivisions(int? parentSubdivisionId)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"{App.AppConfig.GetCurrentConnectionString()}Subdivisions/GetSubdivisions{(parentSubdivisionId == null ? "" : "?parentSubdivisionId=" + parentSubdivisionId)}");
            var responseString = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<List<SubdivisionDto>>(responseString);

            return data;
        }
    }
}
