using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.DAL.Repositories.DbRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DAL.Repositories.Interfaces;
using WebApi.Models;

namespace WebApi.DAL.Repositories.DbRepositories.Tests
{
    [TestClass()]

    public class CustomersRepositoryTests
    {
        ICustomerRepository customerRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            CustomerRepository.InitializeDB();
            customerRepository = CustomerRepository.GetInstance();
        }

        [TestMethod()]
        public async Task CreateTest()
        {
            Customer customer = new() { Id = -1, Firstname = "-1", Lastname = "-1" };
            var id = await customerRepository.CreateAsync(customer);
            var savedCustomer = await customerRepository.GetAsync(-1);
            Assert.AreEqual(savedCustomer.Id, -1);
        }

        [TestMethod()]
        public async Task DeleteTest()
        {
            Customer customer = new() { Id = -2, Firstname = "-2", Lastname = "-2" };
            await customerRepository.CreateAsync(customer);
            var savedCustomer = await customerRepository.GetAsync(-2);
            Assert.AreEqual(savedCustomer.Id, -2);

            customerRepository.Delete(-2);
            savedCustomer = await customerRepository.GetAsync(-2);
            Assert.IsNull(savedCustomer);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            IEnumerable<Customer> customersAll = customerRepository.GetAll();
            Assert.AreEqual(customersAll.Count(), 5);
        }

        [TestMethod()]
        public void GetAsyncTest()
        {
            Task<Customer> customerTask = customerRepository.GetAsync(1);
            customerTask.Wait();
            Assert.IsNotNull(customerTask.Result);
            Assert.AreEqual(customerTask.Result.Id, 1);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.NotImplementedException),"SaveChangeTest implemented.")]
        public void SaveChangeTest()
        {
            customerRepository.SaveChange();
        }

        [TestMethod()]
        [ExpectedException(typeof(NotImplementedException), "NotImplementedException")]
        public void UpdateTest()
        {
            customerRepository.Update(new Customer { Id = 1, Firstname = "-1", Lastname = "-1" });
        }
    }
}