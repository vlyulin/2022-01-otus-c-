using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebClient
{
    static class Program
    {
        const string baseUrl = "https://localhost:5001";
        private const string CustomersRequestUri = "customers";
        static Random rnd = new Random();
        static readonly HttpClient httpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            httpClient.BaseAddress = new Uri(baseUrl);

            // Добавление пользователя
            Customer customer = new Customer()
            {
                Firstname = GetRandomString(10),
                Lastname = GetRandomString(10)
            };
            Console.WriteLine("Добавление пользователя:\nid = '"+customer.Id + 
                "'\nFistname = '" + customer.Firstname + 
                "'\nLastname = '" + customer.Lastname +"'");
            long customerId = PostCustomerRequest(customer).Result;
            Console.WriteLine("Возвращенный id = " + customerId);

            // Запрос добавленного пользователя
            Console.WriteLine("Запрос добавленного пользователя c id = " + customerId);
            customer = GetCustomerRequest(customerId).Result;
            Console.WriteLine("Созданный пользователь:");
            Console.WriteLine("id: '" + customer.Id+"'\nFirstname: '" + 
                customer.Firstname + "'\nLastname: '" + 
                customer.Lastname + "'");

            // Проверка возврата кода 409 при добавлении пользователя с таким же id
            try
            {
                Console.WriteLine("Проверка возврата кода 409 при добавлении пользователя с таким же id.");
                customerId = PostCustomerRequest(customer).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static string GetRandomString(int Length)
        {
            string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder sb = new StringBuilder(Length - 1);
            int Position = 0;

            for (int i = 0; i < Length; i++)
            {
                Position = rnd.Next(0, Alphabet.Length - 1);
                sb.Append(Alphabet[Position]);
            }
            return sb.ToString();
        }

        private static CustomerCreateRequest RandomCustomer()
        {
            CustomerCreateRequest request = new CustomerCreateRequest(
                GetRandomString(10),
                GetRandomString(10));

            return request;
        }

        private static async Task<Customer> GetCustomerRequest(long customerId)
        {
            HttpResponseMessage response = await httpClient.GetAsync(CustomersRequestUri + "/" + customerId);
            Console.WriteLine("Response StatusCode: " + response.StatusCode);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("URL: " + httpClient.BaseAddress + CustomersRequestUri + "/" + customerId + "Response status " + response.StatusCode);
            }
            String jsonString = await response.Content.ReadAsStringAsync();
            Customer customer = JsonConvert.DeserializeObject<Customer>(jsonString);

            return customer;
        }

        private static async Task<long> PostCustomerRequest(Customer customer)
        {
            string returnValue = null;
            using (
                var content = new StringContent(
                        JsonConvert.SerializeObject(customer),
                        System.Text.Encoding.UTF8,
                        "application/json")
                )
            {
                HttpResponseMessage result = await httpClient.PostAsync(CustomersRequestUri, content);
                Console.WriteLine("Response StatusCode: " + result.StatusCode);
                returnValue = result.Content.ReadAsStringAsync().Result;
                if (result.StatusCode != System.Net.HttpStatusCode.OK) // .Created = 201
                {
                    throw new Exception($"Failed to POST data: ({result.StatusCode}): {returnValue}");
                }
            }

            return (long)Convert.ToDouble(returnValue);
        }
    }
}