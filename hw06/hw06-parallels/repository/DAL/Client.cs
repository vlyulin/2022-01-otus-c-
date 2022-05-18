using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.DAL
{
    public class Client
    {
        //id(целое число)
        public int id { get; set; }
        //ФИО(строка),
        public string fio { get; set; }
        //Email(строка)
        public string email { get; set; }
        //Телефон(строка).
        public string phone { get; set; }

        override public string ToString()
        {
            return id.ToString() + "; fio: " + fio + "; email: " + email + "; phone:" + phone;
        }
    }
}
