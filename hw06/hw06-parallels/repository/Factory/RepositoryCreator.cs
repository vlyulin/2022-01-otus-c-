using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.Factory
{
    /// <summary>
    /// Интерфейс фабрики создания репозитория
    /// </summary>
    /// <remarks>
    /// Паттерн "Фабричный метод" (Factory Method)
    /// </remarks>
    public abstract class RepositoryCreator
    {
        /// <summary>
        /// Создание репозитория
        /// </summary>
        /// <param name="configuration">Конфигурация репозитория</param>
        /// <returns></returns>
        public abstract IClientRepository? CreateClientRepository(IConfiguration configuration);
    }
}
