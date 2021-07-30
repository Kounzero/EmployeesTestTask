using System;

namespace EmployeesClient.Models.Employees
{
    /// <summary>
    /// Модель для работы с данными о сотруднике
    /// </summary>
    public class EmployeeDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Идентификатор пола
        /// </summary>
        public int GenderId { get; set; }

        /// <summary>
        /// Наименование пола
        /// </summary>
        public string GenderTitle { get; set; }

        /// <summary>
        /// Идентификатор должности
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// Наименование должности
        /// </summary>
        public string PositionTitle { get; set; }

        /// <summary>
        /// Наличие водительского удостоверения
        /// </summary>
        public bool HasDrivingLicense { get; set; }

        /// <summary>
        /// Идентификатор подразделения
        /// </summary>
        public int SubdivisionId { get; set; }

        /// <summary>
        /// Наименование подразделения
        /// </summary>
        public string SubdivisionTitle { get; set; }

        /// <summary>
        /// Свойство для удобного отображения значения поля HasDrivingLicense
        /// </summary>
        public string ShowedHasDrivingLicense { get { return HasDrivingLicense ? "Да" : "Нет"; } }

        /// <summary>
        /// Свойство для удобного отображения поля BirthDate
        /// </summary>
        public string ShowedDate { get { return BirthDate.ToShortDateString(); } }

    }
}
