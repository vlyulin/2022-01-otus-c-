﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using repository.DAL;

namespace data_generator
{
    public class ClientGenerator : IDataGenerator<Client>
    {
        private int _nextId = 0;
        public Client Next()
        {
            Client client = new();
            client.id = ++_nextId;
            client.fio = GetNewFIO();
            client.email = GetNewEmail(client.fio);
            client.phone = GetNewPhoneNumber();
            return client;
        }

        public List<Client> Next(int count)
        {
            List<Client> list = new List<Client>();
            for(int i = 0; i < count; i++)
            {
                list.Add(Next());
            }
            return list;
        }

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

        // https://stackoverflow.com/questions/14687658/random-name-generator-in-c-sharp
        private static string GenerateName(int len)
        {
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2; // b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;
        }

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