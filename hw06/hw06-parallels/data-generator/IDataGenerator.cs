using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_generator
{
    /// <summary>
    /// Интерфейс генератора объектов типа T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataGenerator<T>
    {
        /// <summary>
        /// Создание одного объекта типа T
        /// </summary>
        /// <returns>экземпляр объекта с типом T</returns>
        T Next();
        /// <summary>
        /// Создание нескольких объектов типа T
        /// </summary>
        /// <param name="count">количество создаваемых объектов</param>
        /// <returns>список сгенерированных объектов типа T</returns>
        List<T> Next(int count);
    }
}
