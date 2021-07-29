using EmployeesClient.Models.Subdivisions;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeesClient.Services
{
    /// <summary>
    /// Сервис для работы с данными о подразделениях
    /// </summary>
    public interface ISubdivisionService
    {
        /// <summary>
        /// Получение всех подразделений
        /// </summary>
        /// <returns></returns>
        public Task<List<SubdivisionDto>> GetAllSubdivisions();

        /// <summary>
        /// Получение всех дочерних подразделений первого уровня вложенности по идентификатору родительского
        /// </summary>
        /// <param name="parentSubdivisionId">Идентификатор родительского подразделения</param>
        /// <returns></returns>
        public Task<List<SubdivisionDto>> GetSubdivisions(int? parentSubdivisionId);

        /// <summary>
        /// Изменение данных о подразделении
        /// </summary>
        /// <param name="editSubdivisionDto">Модель изменяемого подразделения</param>
        public Task<HttpResponseMessage> EditSubdivision(EditSubdivisionDto editSubdivisionDto);

        /// <summary>
        /// Создание нового подразделения
        /// </summary>
        /// <param name="addSubdivisionDto">Модель создаваемого подразделения</param>
        public Task<HttpResponseMessage> AddSubdivision(AddSubdivisionDto addSubdivisionDto);

        /// <summary>
        /// Удаление подразделения
        /// </summary>
        /// <param name="id"></param>
        public Task<HttpResponseMessage> DeleteSubdivision(int id);
    }
}
