using EmployeesAPI.Models.Dtos.Positions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesAPI.Services
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
