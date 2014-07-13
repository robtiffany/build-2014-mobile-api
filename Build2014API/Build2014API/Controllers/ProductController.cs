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
    public class ProductController : ApiController
    {
        private ProductRepository productRepository;

        public ProductController()
        {
            this.productRepository = new ProductRepository();
        }

        // GET api/products
        public IEnumerable<Models.Product> Get()
        {
            return productRepository.GetAllProducts();
        }

    }
}
