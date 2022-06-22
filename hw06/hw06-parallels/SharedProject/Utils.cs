using System;
using System.Collections.Generic;
using System.Text;

namespace SharedProject
{
    /// <summary>
    /// Сервисные функции
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Проверка возможности создания файла
        /// </summary>
        /// <param name="file">путь и имя файла</param>
        /// <returns>true - файл можут быть создан</returns>
        public static bool CanCreateFile(string file)
        {
            try
            {
                using (File.Create(file)) { }
                File.Delete(file);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
