using AppAdmin.DataModel;
using Microsoft.WindowsAzure.MobileServices;
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

namespace AppAdmin
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChangePasswordPage : Page
    {
        public ChangePasswordPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (App.Admin == null) currentPass.Visibility = Visibility.Collapsed;
        }

        private async void changePassBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentPass.Password == App.Admin.Password && username.Text == App.Admin.Username && newPass.Password != string.Empty)
                {
                    App.Admin.Password = newPass.Password;
                    await App.MobileService.GetTable<Admin>().UpdateAsync(App.Admin);
                    ContentDialog confirmDialog = new ContentDialog()
                    {
                        Title = "Success",
                        Content = "Password has been changed",
                        PrimaryButtonText = "Sign out"
                    };

                    await confirmDialog.ShowAsync();
                    (Window.Current.Content as Frame).Navigate(typeof(MainPage));
                }
                else
                {
                    ContentDialog confirmDialog = new ContentDialog()
                    {
                        Title = "Failed",
                        Content = "Incorrect username and password",
                        PrimaryButtonText = "Ok"
                    };

                    await confirmDialog.ShowAsync();
                }
            }
            catch
            {
                MobileServiceCollection<Admin, Admin> items = await App.MobileService.GetTable<Admin>().Where(a => a.Username == username.Text).ToCollectionAsync();
                App.Admin = items.FirstOrDefault();
                if ((App.Admin != null) && newPass.Password != string.Empty)
                {
                    App.Admin.Password = newPass.Password;
                    await App.MobileService.GetTable<Admin>().UpdateAsync(App.Admin);
                    ContentDialog confirmDialog = new ContentDialog()
                    {
                        Title = "Success",
                        Content = "Password has been changed",
                        PrimaryButtonText = "Ok"
                    };

                    await confirmDialog.ShowAsync();
                    (Window.Current.Content as Frame).Navigate(typeof(MainPage));
                }
                else
                {
                    ContentDialog confirmDialog = new ContentDialog()
                    {
                        Title = "Failed",
                        Content = "Incorrect username",
                        PrimaryButtonText = "Ok"
                    };

                    await confirmDialog.ShowAsync();
                }
            }
        }
    }
}
