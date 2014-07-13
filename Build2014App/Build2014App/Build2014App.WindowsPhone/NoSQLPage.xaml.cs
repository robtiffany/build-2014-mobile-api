using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Build2014App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NoSQLPage : Page
    {
        public NoSQLPage()
        {
            this.InitializeComponent();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NoSQL.InsertCustomers();
                NoSQL.InsertProducts();
                NoSQL.InsertOrders();
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.CustomerList.ItemsSource = NoSQL.SelectAllCustomers();
                this.ProductList.ItemsSource = NoSQL.SelectAllProducts();
                this.BadOrderList.ItemsSource = NoSQL.SelectAllOrders();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteOne_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Delete One Customer
                NoSQL.DeleteOne();

                //Refresh Customer List
                this.CustomerList.ItemsSource = NoSQL.SelectAllCustomers();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteMany_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Delete a Range of Customers
                NoSQL.DeleteMany();

                //Refresh Customer List
                this.CustomerList.ItemsSource = NoSQL.SelectAllCustomers();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Delete All Customers
                NoSQL.DeleteAll();

                //Refresh Customer List
                this.CustomerList.ItemsSource = NoSQL.SelectAllCustomers();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Join Tables to Display Order
                NoSQL.JoinOrders();

                //Refresh Good Order List
                this.GoodOrderList.ItemsSource = NoSQL.JoinOrders();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void btnLoadTables_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Load Tables from Storage
                NoSQL.LoadTables();
            }
            catch(IOException io)
            {
                
                //MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateOne_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NoSQL.UpdateOne();

                //Refresh Customer List
                this.CustomerList.ItemsSource = NoSQL.SelectAllCustomers();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateMany_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NoSQL.UpdateMany();

                //Refresh Customer List
                this.CustomerList.ItemsSource = NoSQL.SelectAllCustomers();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NoSQL.UpdateAll();

                //Refresh Customer List
                this.CustomerList.ItemsSource = NoSQL.SelectAllCustomers();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }




    }
}
