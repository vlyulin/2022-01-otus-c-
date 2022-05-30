using repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository
{
    /// <summary>
    /// Реализация интерфейса IClientRepository
    /// </summary>
    public class ClientRepository : IClientRepository
    {
        private IClientContext _clientContext;
        public ClientRepository(IClientContext clientContext)
        {
            _clientContext = clientContext;
        }

        /// <summary>
        /// Открытие репозитория
        /// </summary>
        public void Open()
        {
            _clientContext.Open();
        }

        /// <summary>
        /// Закрытие соединения с репозиторием
        /// </summary>
        public void Close()
        {
            _clientContext.Close();
        }

        /// <summary>
        /// Начало транзакции
        /// </summary>
        public void BeginTransaction()
        {
            _clientContext.BeginTransaction();
        }

        /// <summary>
        /// Подтверждение транзакции
        /// </summary>
        public void CommitTransaction()
        {
            _clientContext.CommitTransaction();
        }

        /// <summary>
        /// Откат транзакции
        /// </summary>
        public void RollbackTransaction()
        {
            _clientContext.RollbackTransaction();
        }

        /// <summary>
        /// Подсчет количества записей в CSV файле
        /// </summary>
        /// <returns>Число записей</returns>
        public long Count()
        {
            return _clientContext.Count();
        }

        /// <summary>
        /// Получение списка клиентов удовлетворяющих заданным условиям
        /// </summary>
        /// <param name="clientSpecification">условия отбора записи</param>
        /// <returns>список клиентов</returns>
        public IEnumerable<Client> Get(IClientSpecification clientSpecification)
        {
            return _clientContext.Get(clientSpecification);
        }

        /// <summary>
        /// Добавление нового клиента
        /// </summary>
        /// <param name="client">добавляемый клиент</param>
        public void Insert(Client client)
        {
            _clientContext.Insert(client);
        }

        /// <summary>
        /// Добавление списка клиентов
        /// </summary>
        /// <param name="clients">список клиентов</param>
        public void Insert(IEnumerable<Client> clients)
        {
            _clientContext.Insert(clients);
        }

    }
}
