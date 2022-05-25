using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.DAL
{
    public class ClientCSVFileContext : IClientContext
    {
        private ISerializer<Client> _serializer;
        private string _fileName;

        public ClientCSVFileContext(string fileName, ISerializer<Client> serializer)
        {
            if(!File.Exists(fileName))
            {
                if(!CanCreateFile(fileName))
                {
                    throw new Exception("Bad repository file: [" + fileName + "]");
                }
                using (File.Create(fileName)) { };
            }
            this._fileName = fileName;
            this._serializer = serializer;
        }
        public void Close()
        {
        }

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

        void IClientContext.Insert(Client client)
        {
            using(StreamWriter streamWriter = File.AppendText(_fileName))
            {
                string serializedClient = _serializer.Serialize(client);
                streamWriter.WriteLine(serializedClient);
            }
        }
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
