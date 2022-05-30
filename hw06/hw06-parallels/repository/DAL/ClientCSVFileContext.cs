using SharedProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.DAL
{
    /// <summary>
    /// Реализация интерфейса IClientContext для работы с CSV репозиторием
    /// </summary>
    public class ClientCSVFileContext : IClientContext
    {
        private ISerializer<Client> _serializer;
        private string _fileName;
        
        /// <summary>
        /// Создание контекста для работы с CSV репозиторием
        /// </summary>
        /// <param name="fileName">путь к CSV репозиторию</param>
        /// <param name="serializer">CSV сериалайзер</param>
        /// <exception cref="Exception">Ошибка создания/открытия CSV файла репозитория</exception>
        public ClientCSVFileContext(string fileName, ISerializer<Client> serializer)
        {
            if(!File.Exists(fileName))
            {
                if(!Utils.CanCreateFile(fileName))
                {
                    throw new Exception("Bad repository file: [" + fileName + "]");
                }
                using (File.Create(fileName)) { };
            }
            this._fileName = fileName;
            this._serializer = serializer;
        }

        /// <summary>
        /// Открытие репозитория
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Open()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Закрытие базы данных
        /// </summary>
        public void Close()
        {
        }

        /// <summary>
        /// Начало транзакции
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Начало транзакции
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Откат транзакции
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void RollbackTransaction()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Подсчет количества записей в CSV файле
        /// </summary>
        /// <returns>Число записей</returns>
        public long Count()
        {
            int count = 0;
            string line;
            using (StreamReader reader = new StreamReader(this._fileName))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Получение списка клиентов удовлетворяющих заданным условиям
        /// </summary>
        /// <param name="clientSpecification">условия отбора записи</param>
        /// <returns>список клиентов</returns>
        IEnumerable<Client> IClientContext.Get(IClientSpecification clientSpecification)
        {
            List<Client> clients = new();
            using(StreamReader streamReader = new StreamReader(this._fileName))
            {
                while (!streamReader.EndOfStream)
                {
                    Client client = _serializer.Deserialize(streamReader);
                    if (clientSpecification != null && clientSpecification.IsSatisfiedBy(client))
                    {
                        clients.Add(client);
                    }
                    else if (clientSpecification == null)
                    {
                        clients.Add(client);
                    }
                }
            }
            return clients;
        }

        /// <summary>
        /// Добавление нового клиента
        /// </summary>
        /// <param name="client">добавляемый клиент</param>
        void IClientContext.Insert(Client client)
        {
            using(StreamWriter streamWriter = File.AppendText(_fileName))
            {
                string serializedClient = _serializer.Serialize(client);
                streamWriter.WriteLine(serializedClient);
            }
        }

        /// <summary>
        /// Добавление списка клиентов
        /// </summary>
        /// <param name="clients">список клиентов</param>
        void IClientContext.Insert(IEnumerable<Client> clients)
        {
            using (StreamWriter streamWriter = File.AppendText(_fileName))
            {
                foreach (Client client in clients)
                {
                    string serializedClient = _serializer.Serialize(client);
                    streamWriter.WriteLine(serializedClient);
                }
            }
        }
    }
}
