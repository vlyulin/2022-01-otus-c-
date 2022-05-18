using repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository
{
    public interface IClientRepository
    {
        IEnumerable<Client> Get(IClientSpecification clientSpecification);
        void Insert(Client client);
        void Insert(IEnumerable<Client> clients);
        long Count();
    }
}
