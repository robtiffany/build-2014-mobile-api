using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Build2014API.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
    }
}