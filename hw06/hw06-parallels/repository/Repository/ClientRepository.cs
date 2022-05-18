using repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository
{
    public class ClientRepository : IClientRepository
    {
        private IClientContext _clientContext;
        public ClientRepository(IClientContext clientContext)
        {
            _clientContext = clientContext;
        }

        public long Count()
        {
            return _clientContext.Count();
        }

        public IEnumerable<Client> Get(IClientSpecification clientSpecification)
        {
            return _clientContext.Get(clientSpecification);
        }

        public void Insert(Client client)
        {
            _clientContext.Insert(client);
        }

        public void Insert(IEnumerable<Client> clients)
        {
            _clientContext.Insert(clients);
        }
    }
}
