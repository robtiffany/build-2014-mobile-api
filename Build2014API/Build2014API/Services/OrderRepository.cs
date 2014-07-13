using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Added Namespaces
using System.Web.Configuration;
using System.Data.SqlClient;

namespace Build2014API.Services
{
    public class OrderRepository
    {
        public void PostOrder(Models.Order value)
        {
            //Connection String
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "dbo.upOrderInsert";
                    command.Parameters.Add("@CustomerId", System.Data.SqlDbType.Int);
                    command.Parameters.Add("@ProductId", System.Data.SqlDbType.Int);
                    command.Parameters.Add("@Quantity", System.Data.SqlDbType.Int);
                    command.Parameters.Add("@DateCreated", System.Data.SqlDbType.DateTime);

                    command.Parameters["@CustomerId"].Value = value.CustomerId;
                    command.Parameters["@ProductId"].Value = value.ProductId;
                    command.Parameters["@Quantity"].Value = value.Quantity;
                    command.Parameters["@DateCreated"].Value = value.DateCreated;

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}