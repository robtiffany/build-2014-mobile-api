using System;
using System.Collections.Generic;
using System.Text;

//Add Using Statements
using Windows.Storage;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Build2014App
{
    static class TableService
    {
        //Delete Existing Table
        public static async Task DeleteTableIfExist(string tableName)
        {
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(tableName);
                await file.DeleteAsync();
            }
            catch (Exception ex)
            {
                if (ex.HResult != -2147024894)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        

        //Serialize Object Collection into JSON File in Local Folder
        public static async Task SaveChanges<T>(T collection, String tableName)
        {
            if (collection != null)
            {
                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(tableName, CreationCollisionOption.ReplaceExisting);
                var outStream = await file.OpenStreamForWriteAsync();
                var serializer = new DataContractJsonSerializer(typeof(T));
                serializer.WriteObject(outStream, collection);
                await outStream.FlushAsync();
                outStream.Dispose();
            }
            else
            {
                throw new Exception("Collection is empty");
            }
        }


        //Deserialize JSON File into Object Collection
        public static async Task<T> LoadTable<T>(string tableName)
        {
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(tableName);
                Stream stream = await file.OpenStreamForReadAsync();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                T table = (T)serializer.ReadObject(stream);
                stream.Dispose();
                return table;
            }
            catch(IOException io)
            {
                throw;
                //throw new Exception("Table doesn't exist");
            }
            catch(Exception ex)
            {
                throw;
            }
        }




    }
}
