using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.Factory
{
    /// <summary>
    /// Реализация интерфейса Конфигурация (IConfiguration) для работы с CSV репозиторием
    /// </summary>
    public class CSVFileConfiguration : IConfiguration
    {
        public String repositoryPath { get; }

        /// <summary>
        /// Конструктор конфигурации CSV репозитория
        /// </summary>
        /// <param name="repositoryPath">путь к CSV репозиторию</param>
        public CSVFileConfiguration(string repositoryPath)
        {
            this.repositoryPath = repositoryPath;
        }
    }
}
