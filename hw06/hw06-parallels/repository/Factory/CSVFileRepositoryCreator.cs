using repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.Factory
{
    /// <summary>
    /// Создание CSV репозитория
    /// </summary>
    /// <remarks>
    /// Реализация раттерна "Фабричный метод" (Factory Method)
    /// </remarks>
    public class CSVFileRepositoryCreator : RepositoryCreator
    {
        /// <summary>
        /// Создание CSV репозитория
        /// </summary>
        /// <param name="configuration">Конфигурация CSV репозитория</param>
        /// <returns></returns>
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
