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
    public class ProductRepository
    {
        public IEnumerable<Models.Product> GetAllProducts()
        {
            List<Models.Product> productList;

            ObjectCache cache = MemoryCache.Default;
            productList = (List<Models.Product>)cache.Get("ProductList");

            if (productList == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();

                //Connection String
                string connectionString = WebConfigurationManager.ConnectionStrings["ProductNodeConnection"].ConnectionString;

                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "dbo.upGetAllProducts";
                        
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader != null && reader.HasRows)
                            {
                                //Initialize a Product 
                                Models.Product product = null;
                                //Create a List to hold multiple Products
                                productList = new List<Models.Product>();

                                while (reader.Read())
                                {
                                    //Create and hydrate a new Object
                                    product = new Models.Product();
                                    product.ProductId = Convert.ToInt32(reader["ProductId"]);
                                    product.Name = Convert.ToString(reader["Name"]).Trim();

                                    //Add to List
                                    productList.Add(product);
                                }

                                policy.SlidingExpiration = TimeSpan.FromSeconds(30);
                                cache.Add("ProductList", productList, policy);
                            }
                        }
                    }
                }
            }
            //Return List
            return productList;
        }

    }
}