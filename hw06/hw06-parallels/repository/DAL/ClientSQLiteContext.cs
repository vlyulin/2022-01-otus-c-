using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.Data;

namespace repository.DAL
{
    public class ClientSQLiteContext : IClientContext
    {
        private string _fileName;

        private SQLiteConnection SetConnection()
        {
            /* if (!File.Exists(_fileName))
            {
                throw new Exception("Repository file [" + _fileName + "] doesn't exists.");
            } */

            return new SQLiteConnection
                (@"Data Source=" + _fileName + ";Pooling=True;New=New;Version=3");
        }

        public ClientSQLiteContext(string fileName)
        {
            this._fileName = fileName;

            if (!File.Exists(fileName))
            {
                if (!CanCreateFile(fileName))
                {
                    throw new Exception("Bad repository file: [" + fileName + "]");
                }
                SQLiteConnection.CreateFile(fileName);
            }

            SQLiteConnection Connect = SetConnection();
            using (Connect)
            {
                string commandText = "CREATE TABLE IF NOT EXISTS clients ( " +
                    "[id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                    "[reference_id] INTEGER NOT NULL, " +
                    "[fio] VARCHAR(255), " +
                    "[email] VARCHAR2(50), " +
                    "[phone] VARCHAR2(15) " +
                    ")";
                Connect.Open();
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Command.ExecuteNonQuery();
                Connect.Close();
            }
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public long Count()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Client> Get(IClientSpecification clientSpecification)
        {
            throw new NotImplementedException();
        }

        public void Insert(Client client)
        {
            SQLiteConnection Connection = SetConnection();
            using (Connection)
            {
                SQLiteCommand insertSQL = new SQLiteCommand(
                    "INSERT INTO clients (reference_id, fio, email, phone) VALUES (?,?,?,?,?)", Connection);
                insertSQL.Parameters.Add(client.id);
                insertSQL.Parameters.Add(client.fio);
                insertSQL.Parameters.Add(client.email);
                insertSQL.Parameters.Add(client.phone);
                insertSQL.ExecuteNonQuery();
            }
        }

        public void Insert(IEnumerable<Client> clients)
        {
            if (clients == null) return;
            if(clients.Count() == 0) return;

            SQLiteConnection Connection = SetConnection();
            using (Connection)
            {
                Connection.Open();
                SQLiteCommand insertSQL = new SQLiteCommand(
                    "INSERT INTO clients (reference_id, fio, email, phone) " +
                    "VALUES (@reference_id, @fio, @email, @phone)", Connection);
                foreach (Client client in clients)
                {
                    insertSQL.Parameters.AddWithValue("@reference_id", client.id);
                    insertSQL.Parameters.AddWithValue("@fio", client.fio);
                    insertSQL.Parameters.AddWithValue("@email", client.email);
                    insertSQL.Parameters.AddWithValue("@phone", client.phone);
                    insertSQL.ExecuteNonQuery();
                }
            }
        }

        /* Проверка возможности создания файла */
        // TODO: перенести в общий проект
        private bool CanCreateFile(string file)
        {
            try
            {
                using (File.Create(file)) { }
                File.Delete(file);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
