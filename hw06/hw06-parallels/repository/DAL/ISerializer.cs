using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.DAL
{
    /// <summary>
    /// Интерфейс сериализации/десериализации
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISerializer<T>
    {
        string Serialize(T obj);
        T Deserialize(StreamReader stream);
    }
}
