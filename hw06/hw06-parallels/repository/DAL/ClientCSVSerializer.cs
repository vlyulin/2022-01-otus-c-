using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.DAL
{
    public class ClientCSVSerializer : ISerializer<Client>
    {
        const char FIELD_SEPARATOR = ',';

        public string Serialize(Client obj)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(obj.id);
            stringBuilder.Append(FIELD_SEPARATOR);
            stringBuilder.Append(obj.fio);
            stringBuilder.Append(FIELD_SEPARATOR);
            stringBuilder.Append(obj.email);
            stringBuilder.Append(FIELD_SEPARATOR);
            stringBuilder.Append(obj.phone);
            return stringBuilder.ToString();
        }
        public Client Deserialize(StreamReader stream)
        {
            string str = stream.ReadLine();
            string[] fields = str.Split(FIELD_SEPARATOR);
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
