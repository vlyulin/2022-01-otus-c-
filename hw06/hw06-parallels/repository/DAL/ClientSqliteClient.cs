using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace repository.DAL
{
    public class ClientSqliteClient : IClientContext
    {
        private string _fileName;
        public ClientSqliteClient(string fileName)
        {
            if (!File.Exists(fileName))
            {
                if (!CanCreateFile(fileName))
                {
                    throw new Exception("Bad repository file: [" + fileName + "]");
                }
                SQLiteConnection.CreateFile(fileName);

                using (SQLiteConnection Connect = new SQLiteConnection(@"Data Source="+_fileName+"; Version=3;"))
                {
                    string commandText = "CREATE TABLE IF NOT EXISTS clients ( " +
                        "[id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                        "[origin_id] INTEGER NOT NULL, " +
                        "[fio] VARCHAR(255), " +
                        "[email] VARCHAR2(50), " +
                        "[phone] VARCHAR2(15) " +
                        ")";
                    SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                    Connect.Open();
                    Command.ExecuteNonQuery();
                    Connect.Close();
                }
            this._fileName = fileName;
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

            throw new NotImplementedException();
        }

        public void Insert(IEnumerable<Client> clients)
        {
            throw new NotImplementedException();
        }

        /* Проверка возможности создания файла */
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
