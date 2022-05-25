using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.Factory
{
    public class CSVFileConfiguration : IConfiguration
    {
        public String repositoryPath { get; }
        public CSVFileConfiguration(string repositoryPath)
        {
            this.repositoryPath = repositoryPath;
        }
    }
}
