namespace EmployeesAPI.Caching
{
    /// <summary>
    /// Уникальные суффиксы ключей кэша
    /// </summary>
    public static class CacheKeys
    {
        /// <summary>
        /// Все подразделения
        /// </summary>
        public static string AllSubdivisions { get { return "_AllSubdivisions"; } }

        /// <summary>
        /// Дочерние подразделения
        /// </summary>
        public static string SubdivisionsByParent { get { return "_SubdivisionsByParent_"; } }

        /// <summary>
        /// Полы
        /// </summary>
        public static string Genders { get { return "_Genders"; } }

        /// <summary>
        /// Должности
        /// </summary>
        public static string Positions { get { return "_Positions"; } }

        /// <summary>
        /// Сотрудники подразделения
        /// </summary>
        public static string EmployeesBySubdivision { get { return "_EmployeesBySubdivision_"; } }

    }
}
