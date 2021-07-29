using EmployeesClient.Models.Positions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesClient.Services
{
    public class PositionService : IPositionService
    {
        public async Task<IEnumerable<PositionDto>> GetPositions()
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"{App.AppConfig.GetCurrentConnectionString()}Positions");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Не удалось загрузить данные о должностях");
            }

            var positions = JsonConvert.DeserializeObject<List<PositionDto>>(response.Content.ReadAsStringAsync().Result);

            return positions;
        }
    }
}
