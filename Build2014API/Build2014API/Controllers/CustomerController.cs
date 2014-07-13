using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

//Added Namespace
using Build2014API.Services;

namespace Build2014API.Controllers
{
    public class CustomerController : ApiController
    {
        private CustomerRepository customerRepository;

        public CustomerController()
        {
            this.customerRepository = new CustomerRepository();
        }

        // GET api/customers
        public IEnumerable<Models.Customer> Get()
        {
            return customerRepository.GetAllCustomers();
        }

    }
}
