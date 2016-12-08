using App.DataModel;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FilterPage : Page
    {

        public List<CuisineType> Cuisines
        {
            get { return Enum.GetValues(typeof(CuisineType)).Cast<CuisineType>().ToList(); }
        }

        public FilterPage()
        {
            this.InitializeComponent();
            filterList.ItemsSource = Cuisines;
            Sortby.SelectedIndex = 0;
            NavigationCacheMode = NavigationCacheMode.Required;

        }
        private void Cancel_Filter(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
                return;

            // If we can go back and the event has not already been handled, do so.
            if (rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }
        }
        private void Apply_Filter(object sender, RoutedEventArgs e)
        {

        }
        private async void Sortby_Changed(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = (sender as ListView).SelectedIndex;
            var frame = (Frame)Window.Current.Content;
            MainPage page = frame.Content as MainPage;

            if (page == null) return;

            try
            {

                switch (selectedIndex)
                {
                    case 0:
                        {
                            page.items = await App.MobileService.GetTable<Restaurant>().OrderByDescending(c => c.Cost).ToCollectionAsync();
                          
                            break;
                        }
                    case 1:
                        {

                            page.items = await App.MobileService.GetTable<Restaurant>().OrderBy(c => c.Cost).ToCollectionAsync();
                            break;
                        }
                }
                page.Refresh();
            }
            catch
            {
                return;
            }


        }

        private async void filterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var frame = (Frame)Window.Current.Content;
            MainPage page = frame.Content as MainPage;


            if (filterList.SelectedItem.ToString() == "All")
            {
                var match = await App.MobileService.GetTable<Restaurant>().ToCollectionAsync();
                page.Refresh(match);
            }
            else
            {
                var match = page.items.Where(c => c.Cuisine.All(d => filterList.SelectedItem.ToString() == d.CuisineType.ToString()));
                page.Refresh(match);
            }
        }
    }
}
