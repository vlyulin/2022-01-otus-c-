using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.DAL
{
    /// <summary>
    /// Сериализация/десериализация сущности "Клиент" в/из CSV формата
    /// </summary>
    public class ClientCSVSerializer : ISerializer<Client>
    {
        const char FieldSeparator = ',';

        /// <summary>
        /// Сериализация Client в CSV формат
        /// </summary>
        /// <param name="obj">экземпляр типа Client</param>
        /// <returns></returns>
        public string Serialize(Client obj)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(obj.id);
            stringBuilder.Append(FieldSeparator);
            stringBuilder.Append(obj.fio);
            stringBuilder.Append(FieldSeparator);
            stringBuilder.Append(obj.email);
            stringBuilder.Append(FieldSeparator);
            stringBuilder.Append(obj.phone);
            return stringBuilder.ToString();
        }
        
        /// <summary>
        /// Десериализация клиента из CSV формата в экземпляр Client
        /// </summary>
        /// <param name="stream">поток bytes</param>
        /// <returns>экземпляр типа Client</returns>
        /// <exception cref="IOException"></exception>
        public Client Deserialize(StreamReader stream)
        {
            string str = stream.ReadLine();
            string[] fields = str.Split(FieldSeparator);
            if(fields.Length != 4)
            {
                throw new IOException("bad reccord: " + str);
            }
            Client client = new Client();
            client.id = int.Parse(fields[0]);
            client.fio = fields[1];
            client.email = fields[2];
            client.phone = fields[3];
            return client;
        }
    }
}
