using EmployeesClient.Models.Subdivisions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesClient.Services
{
    /// <inheritdoc cref="ISubdivisionService"/>
    public class SubdivisionService : ISubdivisionService
    {
        /// <inheritdoc/>
        public async Task<HttpResponseMessage> AddSubdivision(AddSubdivisionDto addSubdivisionDto)
        {
            var response = await App.Client.SendAsync(new HttpRequestMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(addSubdivisionDto), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{App.AppConfig.GetConnectionString()}Subdivisions")
            });

            return response;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> DeleteSubdivision(int id)
        {
            var response = await App.Client.DeleteAsync($"{App.AppConfig.GetConnectionString()}Subdivisions/{id}");

            return response;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> EditSubdivision(EditSubdivisionDto editSubdivisionDto)
        {
            var response = await App.Client.SendAsync(new HttpRequestMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(editSubdivisionDto), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{App.AppConfig.GetConnectionString()}Subdivisions")
            });

            return response;
        }

        /// <inheritdoc/>
        public async Task<List<SubdivisionDto>> GetAllSubdivisions()
        {
            var response = await App.Client.GetAsync($"{App.AppConfig.GetConnectionString()}Subdivisions");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Не удалось загрузить подразделения");
            }

            var subdivisions = JsonConvert.DeserializeObject<List<SubdivisionDto>>(response.Content.ReadAsStringAsync().Result);

            return subdivisions;
        }

        /// <inheritdoc/>
        public async Task<List<SubdivisionDto>> GetSubdivisions(int? parentSubdivisionId)
        {
            var parameters = parentSubdivisionId == null ? "" : "?parentSubdivisionId=" + parentSubdivisionId;
            var response = await App.Client.GetAsync($"{App.AppConfig.GetConnectionString()}Subdivisions/GetSubdivisions{parameters}");
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<SubdivisionDto>>(responseString);

            return data;
        }
    }
}
