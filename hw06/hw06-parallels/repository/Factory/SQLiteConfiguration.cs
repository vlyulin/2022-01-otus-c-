using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.Factory
{
    /// <summary>
    /// Реализация интерфейса Конфигурация (IConfiguration) для работы с SQLite репозиторием
    /// </summary>
    public class SQLiteConfiguration : IConfiguration
    {
        public String repositoryPath { get; }

        /// <summary>
        /// Конструктор конфигурации SQLite репозитория
        /// </summary>
        /// <param name="repositoryPath">путь к SQLite репозиторию</param>
        public SQLiteConfiguration(string repositoryPath)
        {
            this.repositoryPath = repositoryPath;
        }
    }
}
