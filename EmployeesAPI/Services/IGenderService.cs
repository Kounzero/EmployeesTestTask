using EmployeesAPI.Models.Dtos.Genders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesAPI.Services
{
    /// <summary>
    /// Сервис для работы с данными о полах
    /// </summary>
    public interface IGenderService
    {
        /// <summary>
        /// Получение всех полов.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<GenderDto>> GetGenders();
    }
}
