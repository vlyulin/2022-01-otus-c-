using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.DAL
{
    public class ClientFileSpecification : IClientSpecification
    {
        private long _from;
        private long _to;
        public ClientFileSpecification(long from, long to)
        {
            this._from = from;
            this._to = to;
        }

        public bool IsSatisfiedBy(Client client)
        {
            bool res = (client.id >= _from && client.id <= _to);
            return res; // (client.id >= _from && client.id <= _to);
        }
    }
}
