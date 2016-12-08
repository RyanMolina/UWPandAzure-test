using AppAdmin.DataModel;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class LogInPage : Page
    {
        public LogInPage()
        {
            this.InitializeComponent();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            progressRing.IsActive = true;
            MobileServiceCollection<Admin, Admin> items = await App.MobileService.GetTable<Admin>()
                .Where(a => a.Username == username.Text && a.Password == password.Password).ToCollectionAsync();
            Admin admin = items.FirstOrDefault(a => a.Username == username.Text && a.Password == password.Password);

            if (admin != null)
            {
                App.Admin = admin;
                (Window.Current.Content as Frame).Navigate(typeof(ManagePage), admin);
            }
            else
            {
                var dialog = new MessageDialog("Invalid username and password");
                username.Text = "";
                password.Password = "";
                await dialog.ShowAsync();
            }
            progressRing.IsActive = false;

        }

        private void forgotPassword_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ChangePasswordPage));
        }
    }
}
