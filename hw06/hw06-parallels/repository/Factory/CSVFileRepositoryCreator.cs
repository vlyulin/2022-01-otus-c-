using repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.Factory
{
    public class CSVFileRepositoryCreator : RepositoryCreator
    {
        public override IClientRepository? CreateClientRepository(IConfiguration configuration)
        {
            if (configuration == null) return null;
            if(configuration is CSVFileConfiguration) { 
                CSVFileConfiguration conf = (CSVFileConfiguration)configuration;
                ISerializer<Client> serializer = new ClientCSVSerializer();
                IClientContext context = new ClientCSVFileContext(conf.repositoryPath, serializer);
                IClientRepository repository = new ClientRepository(context);
                return repository;
            }
            return null;
        }
    }
}
