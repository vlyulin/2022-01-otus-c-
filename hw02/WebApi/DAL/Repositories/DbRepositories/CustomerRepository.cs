using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DAL.Repositories.Interfaces;
using WebApi.Models;

namespace WebApi.DAL.Repositories.DbRepositories
{
    public sealed class CustomerRepository : ICustomerRepository
    {
        private static List<Customer> customers = new()
        {
            new Customer { Id = 1, Firstname = "First 1", Lastname = "Last name 1" },
            new Customer { Id = 2, Firstname = "First 2", Lastname = "Last name 2" },
            new Customer { Id = 3, Firstname = "First 3", Lastname = "Last name 3" },
            new Customer { Id = 4, Firstname = "First 4", Lastname = "Last name 4" },
            new Customer { Id = 5, Firstname = "First 5", Lastname = "Last name 5" },
        };

        private static string _filePath = "LocalDB.json";
        // в многопоточной среде работать не будет
        private static CustomerRepository _instance;
        private CustomerRepository() { }

        // Это статический метод, управляющий доступом к экземпляру одиночки.
        public static CustomerRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CustomerRepository();
                WriteToJsonFile<List<Customer>>(_filePath, customers);
            }
            return _instance;
        }

        // Для тестов
        public static void InitializeDB()
        {
            customers = new()
            {
                new Customer { Id = 1, Firstname = "First 1", Lastname = "Last name 1" },
                new Customer { Id = 2, Firstname = "First 2", Lastname = "Last name 2" },
                new Customer { Id = 3, Firstname = "First 3", Lastname = "Last name 3" },
                new Customer { Id = 4, Firstname = "First 4", Lastname = "Last name 4" },
                new Customer { Id = 5, Firstname = "First 5", Lastname = "Last name 5" },
            };
            WriteToJsonFile<List<Customer>>(_filePath, customers);
        }

        public async Task<long> CreateAsync(Customer entity)
        {
            if (entity == null) throw new ArgumentException("Parameter cannot be null",nameof(entity));
            customers = ReadFromJsonFile<List<Customer>>(_filePath);

            if (entity.Id == 0) {
                entity.Id = customers.Max(x => x.Id) + 1;
            }
            else { 
                var exsistingCustomer = await GetAsync(entity.Id);
                if (exsistingCustomer != null)
                {
                    throw new CustomerAlreadyExistsException("Customer with id = " + entity.Id + " already exists.");
                }
            }
            customers.Add(entity);
            WriteToJsonFile<List<Customer>>(_filePath, customers);
            return entity.Id;
        }

        public void Delete(long id)
        {
            customers = ReadFromJsonFile<List<Customer>>(_filePath);
            var customer = customers.Find(x => x.Id == id);
            if (customer == null) throw new ArgumentException("Customer.id == "+id+" cannot be found.");
            customers.Remove(customer);
            WriteToJsonFile<List<Customer>>(_filePath, customers);
        }

        public IEnumerable<Customer> GetAll()
        {
            return customers;
        }
        
        public async Task<Customer> GetAsync(long id)
        {
            await Task.Delay(1000);
            customers = ReadFromJsonFile<List<Customer>>(_filePath);
            return customers.Find(x => x.Id == id);
        }

        public void SaveChange()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Customer entity)
        {
            throw new System.NotImplementedException();
        }

        public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        public static T ReadFromJsonFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(fileContents);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}
