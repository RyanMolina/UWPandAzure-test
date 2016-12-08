using AppAdmin.DataModel;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    public sealed partial class SignUpPage : Page
    {

        List<UIElement> uies = new List<UIElement>();
        public SignUpPage()
        {
            this.InitializeComponent();
            FindChildren(uies, this.Content);
            foreach(UIElement uie in uies)
            {
                if (uie is TextBox) (uie as TextBox).TextChanged += TextChanged;
                else if (uie is PasswordBox) (uie as PasswordBox).PasswordChanged += password_PasswordChanged;
            }
        }

        private async void signUpBtn_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();

            admin.Username = email.Text;
            admin.Password = password.Password;

            MobileServiceCollection<Admin, Admin> items = await App.MobileService.GetTable<Admin>().Where(a => a.Username == admin.Username).ToCollectionAsync();
            admin = items.FirstOrDefault(a => a.Username == admin.Username);
            if (admin != null)
            {
                ContentDialog dialog = new ContentDialog()
                {
                    Title = "Failed",
                    Content = "Username already taken",
                    PrimaryButtonText = "OK"
                };

                await dialog.ShowAsync();
            }
            else
            {
                admin = new Admin();

                admin.Username = email.Text;
                admin.Password = password.Password;
                await App.MobileService.GetTable<Admin>().InsertAsync(admin);
                ContentDialog confirmDialog = new ContentDialog()
                {
                    Title = "Success",
                    Content = "Registration Success",
                    PrimaryButtonText = "OK"
                };

                var result = await confirmDialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    this.Frame.GoBack();
                }
            }


        }


        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            Validate();
        }

        private void Validate()
        {
            bool isTbFilled = true;
            bool isPasswordMatched = false;

            foreach (UIElement uie in uies)
            {
                if (uie is TextBox) isTbFilled &= (uie as TextBox).Text != string.Empty;
            }

            if(password.Password != string.Empty) isPasswordMatched = password.Password == retypepass.Password;

            if (!isPasswordMatched)
            {
                message.Text = "Password not match";
            }
            else message.Text = "";

            signUpBtn.IsEnabled = isTbFilled && isPasswordMatched;
        }

        internal static void FindChildren<T>(List<T> results, DependencyObject startNode)
               where T : DependencyObject
        {
            int count = VisualTreeHelper.GetChildrenCount(startNode);
            for (int i = 0; i < count; i++)
            {
                DependencyObject current = VisualTreeHelper.GetChild(startNode, i);
                if ((current.GetType()).Equals(typeof(T)) || (current.GetType().GetTypeInfo().IsSubclassOf(typeof(T))))
                {
                    T asType = (T)current;
                    results.Add(asType);
                }
                FindChildren<T>(results, current);
            }
        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Validate();
        }
    }
}
