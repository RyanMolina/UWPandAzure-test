using AppAdmin.DataModel;
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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AppAdmin
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ManagePage : Page
    {
        public ManagePage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Content.Navigate(typeof(ListPage));
            mainListBox.SelectedIndex = 0;
            TitleTextBlock.Text = "Restaurants";
        }


        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            SplitView.IsPaneOpen = !SplitView.IsPaneOpen;
        }



        private void secondaryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (account.IsSelected)
            {
                Content.Navigate(typeof(AccountPage));
                TitleTextBlock.Text = "Account";

            }
            else if (about.IsSelected)
            {
                Content.Navigate(typeof(AboutPage));
                TitleTextBlock.Text = "About";
            } else return;


            mainListBox.SelectedIndex = -1;
        }

        private void mainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (manage.IsSelected)
            {
                Content.Navigate(typeof(ListPage));
                TitleTextBlock.Text = "Restaurants";
            }
            else if (add.IsSelected)
            {
                Content.Navigate(typeof(Form));
                TitleTextBlock.Text = "Add Restaurant";
            }
            else return;

            secondaryListBox.SelectedIndex = -1;
        }

        private void Content_Navigated(object sender, NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    Content.CanGoBack ?
                    AppViewBackButtonVisibility.Visible :
                    AppViewBackButtonVisibility.Collapsed;
        }
        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Content.CanGoBack)
            {
                e.Handled = true;
                Content.GoBack();

                Type t = Content.Content.GetType();

                mainListBox.SelectionChanged -= mainListBox_SelectionChanged;
                secondaryListBox.SelectionChanged -= secondaryListBox_SelectionChanged;
                mainListBox.SelectedIndex = -1;
                secondaryListBox.SelectedIndex = -1;
                if (t == typeof(ListPage))
                {
                    manage.IsSelected = true;
                    TitleTextBlock.Text = "Restaurants";
                } 
                else if(t == typeof(Form))
                {
                    add.IsSelected = true;
                    TitleTextBlock.Text = "Add Restaurant";
                }
                else if(t == typeof(AccountPage))
                {
                    account.IsSelected = true;
                    TitleTextBlock.Text = "Account";
                }
                else if(t == typeof(AboutPage))
                {
                    about.IsSelected = true;
                    TitleTextBlock.Text = "About";
                }
                mainListBox.SelectionChanged += mainListBox_SelectionChanged;
                secondaryListBox.SelectionChanged += secondaryListBox_SelectionChanged;
            }
        }
    }
}
