using EmployeesClient.Models.Positions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesClient.Services
{
    /// <summary>
    /// Сервис для работы с данными о должностях
    /// </summary>
    public interface IPositionService
    {
        /// <summary>
        /// Получение списка всех должностей.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<PositionDto>> GetPositions();
    }
}
