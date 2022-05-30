using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.Data;
using SharedProject;

namespace repository.DAL
{
    public class ClientSQLiteContext : IClientContext
    {
        private string _fileName;
        private SQLiteConnection _connection;
        private SQLiteTransaction _transaction;

        /// <summary>
        /// Получение соединения с SQLite базой данных
        /// </summary>
        /// <returns></returns>
        private SQLiteConnection SetConnection()
        {
            return new SQLiteConnection
                (@"Data Source=" + _fileName + ";Pooling=True;New=New;Version=3");
        }

        /// <summary>
        /// Создание контекста для работы с CSV репозиторием
        /// </summary>
        /// <param name="fileName">путь к SQLite репозиторию</param>
        /// <exception cref="Exception">Ошибка создания/открытия SQLite файла репозитория</exception>
        /// <remarks>
        /// В случае с пустой базой данных создаёт таблицу clients
        /// </remarks>
        public ClientSQLiteContext(string fileName)
        {
            this._fileName = fileName;

            if (!File.Exists(fileName))
            {
                if (!Utils.CanCreateFile(fileName))
                {
                    throw new Exception("Bad repository file: [" + fileName + "]");
                }
                SQLiteConnection.CreateFile(fileName);
            }
        }

        /// <summary>
        /// Открытие репозитория
        /// </summary>
        public void Open()
        {
            _connection = SetConnection();
            _connection.Open();

            string commandText = "CREATE TABLE IF NOT EXISTS clients ( " +
                "[id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                "[reference_id] INTEGER NOT NULL, " +
                "[fio] VARCHAR(255), " +
                "[email] VARCHAR2(50), " +
                "[phone] VARCHAR2(15) " +
                ")";
            SQLiteCommand Command = new SQLiteCommand(commandText, _connection);
            Command.ExecuteNonQuery();
        }

        /// <summary>
        /// Закрытие базы данных
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Close()
        {
            if( _transaction != null)
            {
                _transaction.Rollback();
            }
            if (_connection != null) 
            {
                _connection.Close();
            }
        }

        /// <summary>
        /// Подсчет количества записей в CSV файле
        /// </summary>
        /// <returns>Число записей Client в репозитории</returns>
        /// <exception cref="NotImplementedException"></exception>
        public long Count()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получение списка клиентов удовлетворяющих заданным условиям
        /// </summary>
        /// <param name="clientSpecification">условия отбора записи</param>
        /// <returns>список клиентов</returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Client> Get(IClientSpecification clientSpecification)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Добавление нового клиента
        /// </summary>
        /// <param name="client">добавляемый клиент</param>
        public void Insert(Client client)
        {
            if (_connection == null) throw new Exception("Not connected to the repository.");

            SQLiteCommand insertSQL = new SQLiteCommand(
                "INSERT INTO clients (reference_id, fio, email, phone) VALUES (?,?,?,?,?)", _connection);
            insertSQL.Parameters.Add(client.id);
            insertSQL.Parameters.Add(client.fio);
            insertSQL.Parameters.Add(client.email);
            insertSQL.Parameters.Add(client.phone);
            insertSQL.ExecuteNonQuery();
        }

        /// <summary>
        /// Получение списка клиентов удовлетворяющих заданным условиям
        /// </summary>
        /// <param name="clientSpecification">условия отбора записи</param>
        /// <returns>список клиентов</returns>
        public void Insert(IEnumerable<Client> clients)
        {
            if (_connection == null) throw new Exception("Not connected to the repository.");
            if (clients == null || clients.Count() == 0) return;

            SQLiteCommand insertSQL = new SQLiteCommand(
                "INSERT INTO clients (reference_id, fio, email, phone) " +
                "VALUES (@reference_id, @fio, @email, @phone)", _connection);
            foreach (Client client in clients)
            {
                insertSQL.Parameters.AddWithValue("@reference_id", client.id);
                insertSQL.Parameters.AddWithValue("@fio", client.fio);
                insertSQL.Parameters.AddWithValue("@email", client.email);
                insertSQL.Parameters.AddWithValue("@phone", client.phone);
                insertSQL.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Начало транзакции
        /// </summary>
        public void BeginTransaction()
        {
           this._transaction = _connection.BeginTransaction();
        }

        /// <summary>
        /// Подтверждение транзакции
        /// </summary>
        public void CommitTransaction()
        {
            if (_transaction == null) return;
            _transaction.Commit();
        }

        /// <summary>
        /// Откат транзакции
        /// </summary>
        public void RollbackTransaction()
        {
            if (_transaction == null) return;
            _transaction.Rollback();
        }
    }
}
