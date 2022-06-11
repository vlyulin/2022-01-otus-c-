using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw08_prototype
{
    internal interface IMyCloneable<T>
    {
        public T clone();
    }
}
