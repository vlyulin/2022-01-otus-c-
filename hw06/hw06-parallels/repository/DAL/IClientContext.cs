using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.DAL
{
    /// <summary>
    /// Интерфейс "Контекста" для работы с базой данных в соответстии с паттерном "Репозиторий"
    /// </summary>
    public interface IClientContext
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
        /// Получение списка клиентов удовлетворяющих заданным условиям
        /// </summary>
        /// <param name="clientSpecification">условия отбора записи</param>
        /// <returns>список клиентов</returns>
        void Insert(IEnumerable<Client> clients);
        

        /// <summary>
        /// Подсчет количества записей в CSV файле
        /// </summary>
        /// <returns>Число записей Client в репозитории</returns>
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
