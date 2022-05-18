using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_generator
{
    public interface IDataGenerator<T>
    {
        T Next();
        List<T> Next(int count);
    }
}
