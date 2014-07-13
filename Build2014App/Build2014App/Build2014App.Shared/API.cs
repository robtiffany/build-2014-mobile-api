using System;
using System.Collections.Generic;
using System.Text;

//Added
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Net.Http.Headers;
using System.IO;

namespace Build2014App
{
    class API
    {
        string httpBaseAddress = "http://localhost:2487/";

        public async void DownloadProducts()
        {
            //Create an HttpClientHandler object 
            HttpClientHandler handler = new HttpClientHandler();

            //Test for Decompression Support
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate;
            }

            //Create an HttpClient object
            HttpClient client = new HttpClient(handler);

            try
            {
                //Download Data
                HttpResponseMessage response = await client.GetAsync(httpBaseAddress + "api/product");
                //Test for Successful Download
                if (response.IsSuccessStatusCode)
                {
                    //Deserialize Data
                    Stream stream = await response.Content.ReadAsStreamAsync();
                    var serializer = new DataContractJsonSerializer(typeof(List<Models.Product>));
                    NoSQL.Products = serializer.ReadObject(stream) as List<Models.Product>;
                    await TableService.SaveChanges<List<Models.Product>>(NoSQL.Products, "Product");
                }
            }
            catch(HttpRequestException httpEx)
            {
                //throw new Exception(httpEx.Message);
            }
            catch(Exception ex)
            {
               // throw;
            }
            finally
            {
                client.Dispose();
                handler.Dispose();
            }

        }


       

        public async void DownloadCustomers()
        {
            //Create an HttpClientHandler object 
            HttpClientHandler handler = new HttpClientHandler();

            //Test for Decompression Support
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate;
            }

            //Create an HttpClient object
            HttpClient client = new HttpClient(handler);

            try
            {
                //Download Data
                HttpResponseMessage response = await client.GetAsync(httpBaseAddress + "api/customer");
                
                //Test for success status code
                if (response.IsSuccessStatusCode)
                {
                    //Deserialize Data
                    Stream stream = await response.Content.ReadAsStreamAsync();
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Models.Customer>));
                    NoSQL.Customers = serializer.ReadObject(stream) as List<Models.Customer>;
                    await TableService.SaveChanges<List<Models.Customer>>(NoSQL.Customers, "Customer");
                }
            }
            catch(HttpRequestException httpEx)
            {
                //throw new Exception(httpEx.Message);
            }
            catch(Exception ex)
            {
               // throw;
            }
            finally
            {
                client.Dispose();
                handler.Dispose();
            }
        }

        public async void UploadOrder(Models.Order order)
        {
            HttpClient client = new HttpClient();

            try
            {
                //Serialize Orders List as JSON
                string data = JSONSerializer<Models.Order>(order);

                //Post Order
                HttpContent content = new StringContent(data);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PostAsync(httpBaseAddress + "api/order", content);

                //Test for Successful Upload
                if (!response.IsSuccessStatusCode)
                {
                    // var messageDialog = new MessageDialog("Order submission failed", "Error");
                }
            }
            catch (HttpRequestException httpEx)
            {
                //throw new Exception(httpEx.Message);
            }
            catch (Exception ex)
            {
                // throw;
            }
            finally
            {
                client.Dispose();
            }
        }



        private string JSONSerializer<T>(T collection)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, collection);
            return Encoding.UTF8.GetString(stream.ToArray(), 0, (int)stream.Length);
        }

    }
}
