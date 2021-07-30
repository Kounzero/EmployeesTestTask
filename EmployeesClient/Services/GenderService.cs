using EmployeesClient.Models.Genders;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeesClient.Services
{
    /// <inheritdoc cref="IGenderService"/>
    public class GenderService : IGenderService
    {
        /// <inheritdoc/>
        public async Task<IEnumerable<GenderDto>> GetGenders()
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"{App.AppConfig.GetCurrentConnectionString()}Genders");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Не удалось получить список полов");
            }

            var genders = JsonConvert.DeserializeObject<List<GenderDto>>(response.Content.ReadAsStringAsync().Result);

            return genders;
        }
    }
}
