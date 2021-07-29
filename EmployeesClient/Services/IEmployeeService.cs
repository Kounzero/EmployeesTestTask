using EmployeesClient.Models.Employees;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeesClient.Services
{
    /// <summary>
    /// Сервис для работы с данными о сотрудниках.
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Получение списка сотрудников из указанного подразделения и всех его вложенных подразделений
        /// </summary>
        /// <param name="subdivisionId">Идентификатор подразделения</param>
        /// <returns></returns>
        public Task<List<EmployeeDto>> GetEmployees(int subdivisionId);

        /// <summary>
        /// Изменение информации о сотруднике
        /// </summary>
        /// <param name="editEmployeeDto">Модель изменяемого сотрудника с новыми значениями</param>
        /// <returns>Статус код, где:
        /// 0 - выполнение успешно;
        /// 1 - сотрудник не найден;
        /// 2 - ошибка сохранения данных.</returns>
        public Task<HttpResponseMessage> EditEmployee(EditEmployeeDto editEmployeeDto);

        /// <summary>
        /// Добавление нового сотрудника
        /// </summary>
        /// <param name="addEmployeeDto">Модель нового сотрудника</param>
        /// <returns>Статус код, где:
        /// 0 - выполнение успешно;
        /// 2 - ошибка сохранения данных.</returns>
        public Task<HttpResponseMessage> AddEmployee(AddEmployeeDto addEmployeeDto);

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="id">Идентификатор сотрудника.</param>
        /// <returns>Статус код, где:
        /// 0 - выполнение успешно;
        /// 1 - сотрудник не найден.</returns>
        public Task<HttpResponseMessage> DeleteEmployee(int id);
    }
}
