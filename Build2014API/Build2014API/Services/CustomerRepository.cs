using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Add Reference
//System.Runtime.Caching

//Added Namespaces
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Runtime.Caching;

namespace Build2014API.Services
{
    public class CustomerRepository
    {
        public IEnumerable<Models.Customer> GetAllCustomers()
        {
            List<Models.Customer> customerList;

            ObjectCache cache = MemoryCache.Default;
            customerList = (List<Models.Customer>)cache.Get("CustomerList");

            if (customerList == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();

                //Connection String
                string connectionString = WebConfigurationManager.ConnectionStrings["CustomerNodeConnection"].ConnectionString;

                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "dbo.upGetAllCustomers";

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader != null && reader.HasRows)
                            {
                                //Initialize a Customer 
                                Models.Customer customer = null;
                                //Create a List to hold multiple Customers
                                customerList = new List<Models.Customer>();

                                while (reader.Read())
                                {
                                    //Create and hydrate a new Object
                                    customer = new Models.Customer();
                                    customer.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                                    customer.FirstName = Convert.ToString(reader["FirstName"]).Trim();
                                    customer.LastName = Convert.ToString(reader["LastName"]).Trim();

                                    //Add to List
                                    customerList.Add(customer);
                                }

                                policy.SlidingExpiration = TimeSpan.FromSeconds(30);
                                cache.Add("CustomerList", customerList, policy);
                            }
                        }
                    }
                }
            }
            //Return List
            return customerList;
        }

    }
}