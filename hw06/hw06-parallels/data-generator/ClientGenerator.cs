using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using repository.DAL;

namespace data_generator
{
    /// <summary>
    /// Реализация интерфейса IDataGenerator.
    /// Генерация объектов Client
    /// </summary>
    public class ClientGenerator : IDataGenerator<Client>
    {
        private static readonly string[] _consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
        private static readonly string[] _vowels = { "a", "e", "i", "o", "u", "ae", "y" };

        private int _nextId = 0;

        /// <summary>
        /// Создание одного объекта типа Client
        /// </summary>
        /// <returns></returns>
        public Client Next()
        {
            Client client = new();
            client.id = ++_nextId;
            client.fio = GetNewFIO();
            client.email = GetNewEmail(client.fio);
            client.phone = GetNewPhoneNumber();
            return client;
        }

        /// <summary>
        /// Создание нескольких объектов типа Client
        /// </summary>
        /// <param name="count">количество создаваемых объектов</param>
        /// <returns>список сгенерированных объектов типа Client</returns>
        public List<Client> Next(int count)
        {
            List<Client> list = new List<Client>();
            for(int i = 0; i < count; i++)
            {
                list.Add(Next());
            }
            return list;
        }

        /// <summary>
        /// Генерация произвольного ФИО
        /// </summary>
        /// <returns>сгенерированное ФИО</returns>
        private string GetNewFIO() 
        {
            const int MINLEN = 5;
            const int MAXLEN = 10;

            StringBuilder stringBuider = new();
            Random r = new Random();
            stringBuider.Append(GenerateName(r.Next(MINLEN, MAXLEN)));
            stringBuider.Append(' ');
            stringBuider.Append(GenerateName(r.Next(MINLEN,MAXLEN)));
            stringBuider.Append(' ');
            stringBuider.Append(GenerateName(r.Next(MINLEN, MAXLEN)));
            return stringBuider.ToString();
        }

        /// <summary>
        /// Генерация строки похожей на фамилию, имя или отчество
        /// </summary>
        /// <param name="len">длина генерируемой строки</param>
        /// <returns>сгенерированная строка длины len</returns>
        /// <remarks>
        /// https://stackoverflow.com/questions/14687658/random-name-generator-in-c-sharp
        /// </remarks>
        private static string GenerateName(int len)
        {
            Random r = new Random();
            StringBuilder Name = new StringBuilder();
            Name.Append(_consonants[r.Next(_consonants.Length)].ToUpper());
            Name.Append(_vowels[r.Next(_vowels.Length)]);
            int b = 2; // b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
            while (b < len)
            {
                Name.Append(_consonants[r.Next(_consonants.Length)]);
                b++;
                Name.Append(_vowels[r.Next(_vowels.Length)]);
                b++;
            }

            return Name.ToString();
        }

        /// <summary>
        /// Генерация e-mail на основе имени и фамилии
        /// </summary>
        /// <param name="fio">ФИО разделенные пробелом</param>
        /// <returns>сгенерированный e-mail</returns>
        private static string GetNewEmail(string fio)
        {
            string[] emailproviders = { "yandex.ru", "mail.ru", "gmail.com" };
            string[] sptittedFio = fio.Split(' ');
            StringBuilder stringBuilder = new();
            stringBuilder.Append(sptittedFio[0]);
            stringBuilder.Append('.');
            stringBuilder.Append(sptittedFio[1]);
            stringBuilder.Append('@');
            Random rand = new Random();
            stringBuilder.Append(emailproviders[rand.Next(emailproviders.Length)]);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Генерация номера телефона
        /// </summary>
        /// <returns>сгенерированный номер телефона</returns>
        private static string GetNewPhoneNumber()
        {
            Random rand = new Random();
            StringBuilder stringBuilder = new();
            stringBuilder.Append("+7 (");
            stringBuilder.Append(rand.Next(100,999));
            stringBuilder.Append(") ");
            stringBuilder.Append(rand.Next(100, 999));
            stringBuilder.Append('-');
            stringBuilder.Append(rand.Next(10, 99));
            stringBuilder.Append('-');
            stringBuilder.Append(rand.Next(10, 99));
            return stringBuilder.ToString();
        }
    }

}
