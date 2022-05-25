using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.Factory
{
    public class SQLiteConfiguration : IConfiguration
    {
        public String repositoryPath { get; }

        public SQLiteConfiguration(string repositoryPath)
        {
            this.repositoryPath = repositoryPath;
        }
    }
}
