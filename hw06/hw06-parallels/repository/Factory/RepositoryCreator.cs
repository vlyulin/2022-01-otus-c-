using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.Factory
{
    public abstract class RepositoryCreator
    {
        public abstract IClientRepository? CreateClientRepository(IConfiguration configuration);
    }
}
