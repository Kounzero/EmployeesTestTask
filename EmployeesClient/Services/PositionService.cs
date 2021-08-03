using EmployeesClient.Models.Positions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeesClient.Services
{
    /// <inheritdoc cref="IPositionService"/>
    public class PositionService : IPositionService
    {
        /// <inheritdoc/>
        public async Task<IEnumerable<PositionDto>> GetPositions()
        {
            var response = await App.Client.GetAsync($"{App.AppConfig.GetConnectionString()}Positions");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Не удалось загрузить данные о должностях");
            }

            var positions = JsonConvert.DeserializeObject<List<PositionDto>>(response.Content.ReadAsStringAsync().Result);

            return positions;
        }
    }
}
