using System;
using System.Windows;

namespace EmployeesClient.Models.Subdivisions
{
    /// <summary>
    /// Модель для работы с данными о подразделениях
    /// </summary>
    public class SubdivisionDto
    {
        public SubdivisionDto()
        {
            Opening = false;
            LeftMargin = 0;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Дата формирования
        /// </summary>
        public DateTime FormDate { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Идентификатор ролительского подразделения
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Наличие дочерних подразделений
        /// </summary>
        public bool HasChildren { get; set; }

        /// <summary>
        /// Вспомогательное свойство для интерфейса: регулирует видимость кнопки "развернуть/свернуть дочерние подразделения"
        /// </summary>
        public Visibility ShowedVisibility { get { return HasChildren ? Visibility.Visible : Visibility.Hidden; } }

        /// <summary>
        /// Вспомогательное свойство для интерфейса: регулирует внешний вид кнопки "развернуть/свернуть дочерние подразделения"
        /// </summary>
        public string BtnDisplayContent { get { return Opening ? "v" : ">"; } }

        /// <summary>
        /// Вспомогательное свойство для интерфейса: регулирует отступ подразделений для создания визуальной иерархии подразделений
        /// </summary>
        public string MarginForDisplay { get { return $"{LeftMargin} 0 0 0"; } }

        /// <summary>
        /// Левый отступ подразделения при отображении
        /// </summary>
        public int LeftMargin { get; set; }

        /// <summary>
        /// Свойство для отслеживания, развёрнуто ли подразделение в списке
        /// </summary>
        public bool Opening { get; set; }
    }
}
