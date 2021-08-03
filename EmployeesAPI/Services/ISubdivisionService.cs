using EmployeesAPI.Models.Dtos.Subdivisions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesAPI.Services
{
    /// <summary>
    /// Сервис для работы с данными о подразделениях
    /// </summary>
    public interface ISubdivisionService
    {
        /// <summary>
        /// Получение всех подразделений
        /// </summary>
        public Task<List<SubdivisionDto>> GetAllSubdivisions();

        /// <summary>
        /// Получение всех дочерних подразделений первого уровня вложенности по идентификатору родительского
        /// </summary>
        /// <param name="parentSubdivisionId">Идентификатор родительского подразделения</param>
        public Task<List<SubdivisionDto>> GetSubdivisions(int? parentSubdivisionId);

        /// <summary>
        /// Изменение данных о подразделении
        /// </summary>
        /// <param name="editSubdivisionDto">Модель изменяемого подразделения</param>
        public Task<ServiceResult> EditSubdivision(EditSubdivisionDto editSubdivisionDto);

        /// <summary>
        /// Создание нового подразделения
        /// </summary>
        /// <param name="addSubdivisionDto">Модель создаваемого подразделения</param>
        public Task<ServiceResult> AddSubdivision(AddSubdivisionDto addSubdivisionDto);

        /// <summary>
        /// Удаление подразделения
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Модель создаваемого подразделения</param>
        public Task<ServiceResult> DeleteSubdivision(int id);

        /// <summary>
        /// Метод для определения, можно ли сделать одно подразделение дочерним для другого. Необходимо для избежания циклических зависимостей подразделений
        /// </summary>
        /// <param name="subdivisionId">Идентификатор проверяемого подразделения</param>
        /// <param name="targetSubdivisionId">Идентификатор целевого подразделения</param>
        /// <returns>Возвращает true, если возможно, иначе false</returns>
        public Task<bool> CheckSubdivisionParentingPossibility(int subdivisionId, int targetSubdivisionId);

        /// <summary>
        /// Метод для получения всех вложенных подразделений указанного подразделения
        /// </summary>
        /// <param name="subdivisionId">Идентификатор родительского подразделения</param>
        public Task<List<SubdivisionDto>> GetAllSubdivisionChildren(int subdivisionId);
    }
}
