using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.DAL
{
    public interface IClientContext
    {
        IEnumerable<Client> Get(IClientSpecification clientSpecification);
        void Insert(Client client);
        void Insert(IEnumerable<Client> clients);
        void Close();
        long Count();
    }
}
