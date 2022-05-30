using repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository
{
    /// <summary>
    /// Интерфейс репозитория для работы с сущностями Клиент
    /// </summary>
    /// <remarks>
    /// Шаблон проектирования "Репозиторий"
    /// </remarks>
    public interface IClientRepository
    {
        /// <summary>
        /// Открытие репозитория
        /// </summary>
        void Open();

        /// <summary>
        /// Закрытие репозитория
        /// </summary>
        void Close();

        /// <summary>
        /// Получение списка клиентов удовлетворяющих заданным условиям
        /// </summary>
        /// <param name="clientSpecification">условия отбора записи</param>
        /// <returns>список клиентов</returns>
        IEnumerable<Client> Get(IClientSpecification clientSpecification);

        /// <summary>
        /// Добавление нового клиента
        /// </summary>
        /// <param name="client">добавляемый клиент</param>
        void Insert(Client client);

        /// <summary>
        /// Добавление списка клиентов
        /// </summary>
        /// <param name="clients">список клиентов</param>
        void Insert(IEnumerable<Client> clients);
        
        /// <summary>
        /// Подсчет количества записей в CSV файле
        /// </summary>
        /// <returns>Число записей</returns>
        long Count();

        /// <summary>
        /// Создание транзакции
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Подтверждение транзакции
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Откат транзакции
        /// </summary>
        void RollbackTransaction();
    }
}
