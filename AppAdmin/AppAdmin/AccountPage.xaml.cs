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
    public sealed partial class AccountPage : Page
    {
        public AccountPage()
        {
            this.InitializeComponent();
        }

        private void changePasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ChangePasswordPage));
        }

        private async void logOutBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog confirmDialog = new ContentDialog()
            {
                Title = "Log out",
                Content = "Are you sure?",
                PrimaryButtonText = "Sign out",
                SecondaryButtonText = "Cancel"
            };

            var result = await confirmDialog.ShowAsync();
            if(result == ContentDialogResult.Primary)
            {

                Frame rootFrame = Window.Current.Content as Frame;

                rootFrame.GoBack();
            }

        }
    }
}
