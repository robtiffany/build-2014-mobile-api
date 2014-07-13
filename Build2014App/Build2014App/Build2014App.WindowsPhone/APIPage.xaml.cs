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

//Added
using Windows.UI.Popups;



// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Build2014App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class APIPage : Page
    {
        private API api = new API();
        public APIPage()
        {
            this.InitializeComponent();
        }

        private void btnDownloadProducts_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                api.DownloadProducts();
            }
            catch(Exception ex)
            {
                var messageDialog = new MessageDialog(ex.Message, "Error");
            }
        }

        
        private void btnDownloadCustomers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                api.DownloadCustomers();
            }
            catch (Exception ex)
            {
                var messageDialog = new MessageDialog(ex.Message, "Error");
            }
        }

        private void btnUploadOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NoSQL.Orders.Count > 0)
                {
                    api.UploadOrder(NoSQL.Orders.First());
                }
            }
            catch (Exception ex)
            {
                var messageDialog = new MessageDialog(ex.Message, "Error");
            }
        }


    }
}
