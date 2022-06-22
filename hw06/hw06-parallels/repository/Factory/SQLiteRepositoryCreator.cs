using repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.Factory
{
    /// <summary>
    /// Создание SQLite репозитория
    /// </summary>
    /// <remarks>
    /// Реализация раттерна "Фабричный метод" (Factory Method)
    /// </remarks>
    public class SQLiteRepositoryCreator : RepositoryCreator
    {

        /// <summary>
        /// Создание SQLite репозитория
        /// </summary>
        /// <param name="configuration">Конфигурация SQLite репозитория</param>
        /// <returns></returns>
        public override IClientRepository? CreateClientRepository(IConfiguration configuration)
        {
<<<<<<< HEAD
            if (configuration == null) return null;
=======
            // if (configuration == null) return null;
>>>>>>> main
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
