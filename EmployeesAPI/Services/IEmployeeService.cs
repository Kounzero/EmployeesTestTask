using EmployeesAPI.Models.Dtos.Employees;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesAPI.Services
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
        public Task<List<EmployeeDto>> GetEmployees(int subdivisionId);

        /// <summary>
        /// Изменение информации о сотруднике
        /// </summary>
        /// <param name="editEmployeeDto">Модель изменяемого сотрудника с новыми значениями</param>
        public Task<ServiceResult> EditEmployee(EditEmployeeDto editEmployeeDto);

        /// <summary>
        /// Добавление нового сотрудника
        /// </summary>
        /// <param name="addEmployeeDto">Модель нового сотрудника</param>
        public Task<ServiceResult> AddEmployee(AddEmployeeDto addEmployeeDto);

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="id">Идентификатор сотрудника.</param>
        public Task<ServiceResult> DeleteEmployee(int id);
    }
}
