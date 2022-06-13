using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.DAL
{ 
    /// <summary>
    /// Реализация условия для отбора клиентов из CSV репозитория
    /// </summary>
    /// /// <remarks>
    /// шаблон проектирования "Спецификация"
    /// </remarks>
    public class ClientFileSpecification : IClientSpecification
    {
        private long _from;
        private long _to;

        /// <summary>
        /// Конструктор для создания условия отбора клиентов из CSV репозитория
        /// </summary>
        /// <param name="from">начальный идентификатор диапазона отбора клиентов (включительно)</param>
        /// <param name="to">конечный идентификатор диапазона отбора клиентов (включительно)</param>
        public ClientFileSpecification(long from, long to)
        {
            _from = from;
            _to = to;
        }

        /// <summary>
        /// Проверка соответствия экземпляра Клиент заданным условиям
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <returns>true, если клиент удовлетворяет заданным условиям</returns>
        public bool IsSatisfiedBy(Client client)
        {
            bool res = (client.id >= _from && client.id <= _to);
            return res;
        }
    }
}
