using System;
using System.Collections.Generic;
using System.Text;

//Added Namespace
using System.Linq;

namespace Build2014App
{
    static class NoSQL
    {
        //Define Tables
        public static List<Models.Customer> Customers { get; set; }
        public static List<Models.Order> Orders { get; set; }
        public static List<Models.Product> Products { get; set; }

        //Update a Single Customer
        public static async void UpdateOne()
        {
            //Update Customer Value Where CustomerId is 2
            foreach (var item in Customers.Where((c) => c.CustomerId == 2))
            {
                item.FirstName = "Mike";
            }

            //Save Changes to Storage
            await TableService.SaveChanges<List<Models.Customer>>(Customers, "Customer");
        }

        //Update More than one Customer
        public static async void UpdateMany()
        {
            //Update Customer Values Where CustomerId is Greater than one
            foreach (var item in Customers.Where((c) => c.CustomerId > 1))
            {
                item.LastName = "Wyatt";
            }

            //Save Changes to Storage
            await TableService.SaveChanges<List<Models.Customer>>(Customers, "Customer");
        }

        //Update All Customers
        public static async void UpdateAll()
        {
            //Update the Values of all Customers
            foreach (var item in Customers)
            {
                item.LastName = "Satter";
            }

            //Save Changes to Storage
            await TableService.SaveChanges<List<Models.Customer>>(Customers, "Customer");
        }

        //Delete a Single Customer
        public static async void DeleteOne()
        {
            //Delete Customer Where CustomerId is 2
            Customers.RemoveAll((c) => c.CustomerId == 2);

            //Save Changes to Storage
            await TableService.SaveChanges<List<Models.Customer>>(Customers, "Customer");
        }

        //Delete More than one Customer
        public static async void DeleteMany()
        {
            //Delete all Customers Where CustomerId is Greater than 1
            Customers.RemoveAll((c) => c.CustomerId > 1);

            //Save Changes to Storage
            await TableService.SaveChanges<List<Models.Customer>>(Customers, "Customer");
        }

        //Delete All Customers
        public static async void DeleteAll()
        {
            //Delete In-Memory Table
            Customers.Clear();

            //Delete Table from Storage
            await TableService.DeleteTableIfExist("Customer");
        }


        public static async void InsertCustomers()
        {
            Customers.Clear();

            Customers.Add(new Models.Customer
            {
                CustomerId = 1,
                FirstName = "Andy",
                LastName = "Wigley"
            });

            Customers.Add(new Models.Customer
            {
                CustomerId = 2,
                FirstName = "Rob",
                LastName = "Tiffany"
            });

            Customers.Add(new Models.Customer
            {
                CustomerId = 3,
                FirstName = "Nick",
                LastName = "Randolph"
            });

            Customers.Add(new Models.Customer
            {
                CustomerId = 4,
                FirstName = "Maarten",
                LastName = "Struys"
            });

            await TableService.SaveChanges<List<Models.Customer>>(Customers, "Customer");
        }


        public static async void InsertProducts()
        {
            Products.Clear();

            Products.Add(new Models.Product
            {
                ProductId = 1,
                Name = "Motherboard"
            });

            Products.Add(new Models.Product
            {
                ProductId = 2,
                Name = "RAM"
            });

            Products.Add(new Models.Product
            {
                ProductId = 3,
                Name = "Hard Drive"
            });

            Products.Add(new Models.Product
            {
                ProductId = 4,
                Name = "Graphics Card"
            });

            await TableService.SaveChanges<List<Models.Product>>(Products, "Product");
        }



        public static async void InsertOrders()
        {
            Orders.Clear();

            Orders.Add(new Models.Order
            {
                OrderId = 1,
                CustomerId = 1,
                ProductId = 1,
                Quantity = 1,
                DateCreated = System.DateTime.Now
            });

            Orders.Add(new Models.Order
            {
                OrderId = 2,
                CustomerId = 1,
                ProductId = 4,
                Quantity = 1,
                DateCreated = System.DateTime.Now
            });

            Orders.Add(new Models.Order
            {
                OrderId = 3,
                CustomerId = 2,
                ProductId = 2,
                Quantity = 4,
                DateCreated = System.DateTime.Now
            });

            await TableService.SaveChanges<List<Models.Order>>(Orders, "Order");
        }


        //Return all Customers from Collection
        public static List<Models.Customer> SelectAllCustomers()
        {
            //Get Rows
            var query = from c in Customers
                        select c;

            return query.ToList();
        }


        //Return all Products from Collection
        public static List<Models.Product> SelectAllProducts()
        {
            //Get Rows
            var query = from p in Products
                        select p;

            return query.ToList();
        }


        //Return all Orders from Collection
        public static List<Models.Order> SelectAllOrders()
        {
            //Get Rows
            var query = from p in Orders
                        select p;
            
            return query.ToList();
        }





        public static async void LoadTables()
        {
            try
            {
                //Load Offline Data
                Customers = await TableService.LoadTable<List<Models.Customer>>("Customer");
                Products = await TableService.LoadTable<List<Models.Product>>("Product");
                Orders = await TableService.LoadTable<List<Models.Order>>("Order");
            }
            catch(Exception ex)
            {
                throw;
            }
        }


        public static System.Collections.IEnumerable JoinOrders()
        {
            //3-Way Join to Return Meaningful Data
            var innerJoinQuery =
                from order in Orders
                join customer in Customers on order.CustomerId equals customer.CustomerId
                join product in Products on order.ProductId equals product.ProductId
                select new { OrderId = order.OrderId, FirstName = customer.FirstName, LastName = customer.LastName, ProductName = product.Name, Quantity = order.Quantity };

            return innerJoinQuery.ToList();
        }




    }
}
