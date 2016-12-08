using App.DataModel;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RestaurantInfo : Page
    {
        public Restaurant Restaurant { get; set; }
        List<BitmapImage> menuImage;
        
        //private IMobileServiceTable<Customer> customerTable = App.MobileService.GetTable<Customer>();

        public RestaurantInfo()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Restaurant = e.Parameter as Restaurant;
            if (Restaurant.Image != null) coverPhoto.Source = ByteArrayBitmapExtensions.AsBitmapImage(Restaurant.Image);
            menuImage = new List<BitmapImage>();
            foreach (Menu m in Restaurant.Menu)
            {
                menuImage.Add(ByteArrayBitmapExtensions.AsBitmapImage(m.MenuImage));
            }
            creditCard.Visibility = Restaurant.CreditCard ? Visibility.Visible : Visibility.Collapsed;
            wifi.Visibility = Restaurant.Wifi ? Visibility.Visible : Visibility.Collapsed;
            petFriendly.Visibility = Restaurant.PetFriendly ? Visibility.Visible : Visibility.Collapsed;
            outdoorSeating.Visibility = Restaurant.OutdoorSeating ? Visibility.Visible : Visibility.Collapsed;
            menuImages.ItemsSource = menuImage;
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string value = string.Empty;
            reviewEditBox.Document.GetText(Windows.UI.Text.TextGetOptions.AdjustCrlf, out value);
            reviewEditBox.Document.SetText(Windows.UI.Text.TextSetOptions.None, "");
            Feedback feedback = new Feedback
            {
                Review = value,
                Rating = 3,
                Id = Guid.NewGuid().ToString()
            };
           
            Restaurant.Feedback.Add(feedback);
            await App.MobileService.GetTable<Restaurant>().UpdateAsync(Restaurant);
            
        }

        private  void menuImages_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(MenuView), menuImage);

        }
    }
}
