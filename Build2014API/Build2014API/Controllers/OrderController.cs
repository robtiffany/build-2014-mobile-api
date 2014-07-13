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
    public class OrderController : ApiController
    {
        private OrderRepository orderRepository;

        public OrderController()
        {
            this.orderRepository = new OrderRepository();
        }

        // POST api/orders
        public void Post(Models.Order value)
        {
            try
            {
                orderRepository.PostOrder(value);
            }
            catch (Exception)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Error inserting Order"),
                    ReasonPhrase = "Error inserting Order"
                };
                throw new HttpResponseException(resp);
            }
        }

    }
}
