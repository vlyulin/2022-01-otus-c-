using System.Collections;

namespace hw05_delegates_and_events
{
    public static class Hw05
    {
        // 1. Написать обобщённую функцию расширения, находящую и возвращающую максимальный элемент коллекции.
        //    Функция должна принимать на вход делегат, преобразующий входной тип в число для возможности
        //    поиска максимального значения.
        //    public static T GetMax(this IEnumerable e, Func<T, float> getParameter) where T : class;
        public static T? GetMax<T>(this IEnumerable e, Func<T, float> getParameter) where T : class
        {
            if (e == null)
            {
                return default(T);
            }

            T? max = default(T);
            foreach (T item in e)
            {
                if (max == null || getParameter(item) > getParameter(max)) max = item;
            }

            return max;
        }
    }
}
