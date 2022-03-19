using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DAL.Repositories.DbRepositories;
using WebApi.DAL.Repositories.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetCustomerAsync([FromRoute] long id)
        {
            var customer = await customerRepository.GetAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] Customer customer)
        {
            try
            {
                long id = await customerRepository.CreateAsync(customer);
                return Ok(id); // 200
            }
            catch (CustomerAlreadyExistsException ex)
            {
                return Conflict(new { message = ex.Message }); // 409
            }
        }
    }
}