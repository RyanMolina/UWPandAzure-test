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
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Popups;
using App.DataModel;
using System.Diagnostics;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using System.Collections;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MobileServiceCollection<Restaurant, Restaurant> items;
        public IMobileServiceTable<Restaurant> restaurantTable = App.MobileService.GetTable<Restaurant>(); // offline sync
        public Customer User { get; set; }


        public MainPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
            RefreshTodoItems();

        }



        public async void RefreshTodoItems()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                items = await restaurantTable.ToCollectionAsync();

            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                restaurantList.ItemsSource = items;
            }
        }


        public void Refresh()
        {
            restaurantList.ItemsSource = items;
        }



        public void Refresh(IEnumerable ie)
        {
            restaurantList.ItemsSource = ie;
        }







        private void Restaurant_Selected(object sender, ItemClickEventArgs e)
        {
            var selected = e.ClickedItem as Restaurant;
            selected.StoreTime = selected.StoreTime.OrderBy(a => a.Day).ToList();
            this.Frame.Navigate(typeof(RestaurantInfo), e.ClickedItem);
        }
        private void Filter_Navigated(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FilterPage));
        }

        private void TitleTextBlock_Click(object sender, RoutedEventArgs e)
        {


        }




        private async void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                if (sender.Text.Length > 1)
                {
                    var match = items.Where(a => a.Name.StartsWith(sender.Text, StringComparison.CurrentCultureIgnoreCase) ||
                                            a.Address.Contains(sender.Text) ||
                                            a.Description.Contains(sender.Text) ||
                                            a.Cuisine.Any(c => c.CuisineType.ToString().StartsWith(sender.Text, StringComparison.CurrentCultureIgnoreCase)));



                    sender.ItemsSource = match.ToList();
                }
                else if (sender.Text.Length == 0)
                {
                    items = await restaurantTable.ToCollectionAsync();
                    restaurantList.ItemsSource = items;
                    sender.IsSuggestionListOpen = false;
                }
            }
        }



        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.QueryText.Length > 0)
            {
                var match = items.Where(a => a.Name.StartsWith(sender.Text, StringComparison.CurrentCultureIgnoreCase) ||
                                        a.Description.Contains(sender.Text) ||
                                        a.Cuisine.Any(c => c.CuisineType.ToString().StartsWith(sender.Text, StringComparison.CurrentCultureIgnoreCase)));

                restaurantList.ItemsSource = match;
            }
            else
            {
                //await restaurantTable.LookupAsync("d71314c7-2462-419f-b69e-a40f81a2a410");
                //Do something if nothing matches
            }
        }

        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            //sender.Text = "";

            //this.Frame.Navigate(typeof(RestaurantInfo), args.SelectedItem);
        }

    }
}
