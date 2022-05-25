using repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.Factory
{
    public class SQLiteRepositoryCreator : RepositoryCreator
    {
        public override IClientRepository? CreateClientRepository(IConfiguration configuration)
        {
            if (configuration == null) return null;
            if (configuration is SQLiteConfiguration)
            {
                SQLiteConfiguration config = (SQLiteConfiguration) configuration;
                IClientContext SQLiteContext = new ClientSQLiteContext(config.repositoryPath);
                IClientRepository repository = new ClientRepository(SQLiteContext);
                return repository;
            }
            return null;
        }
    }
}
