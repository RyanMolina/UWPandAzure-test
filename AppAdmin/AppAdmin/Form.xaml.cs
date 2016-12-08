using AppAdmin.DataModel;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AppAdmin
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Form : Page
    {
        List<StoreTime> storeTimes = new List<StoreTime>();
        ObservableCollection<MenuImage> menu = new ObservableCollection<MenuImage>();
        List<StorageFile> sf = new List<StorageFile>();
        List<Cuisine> cuisine = new List<Cuisine>();
        Restaurant Restaurant;
        List<TextBox> tbs = new List<TextBox>();
        List<CheckBox> cbs = new List<CheckBox>();


        public Form()
        {
            this.InitializeComponent();
            Tags.ItemsSource = Cuisines;
            menuImages.ItemsSource = menu;

            FindChildren(tbs, form);
            foreach(TextBox tb in tbs)
            {
                tb.TextChanged += TextChanged;
            }
            FindChildren(cbs, form);
            foreach (CheckBox cb in cbs)
            {
                cb.Click += Checked;
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Restaurant = e.Parameter as Restaurant;

            if (Restaurant != null)
            {
                MobileServiceCollection<Restaurant, Restaurant> items = await App.MobileService.GetTable<Restaurant>().Where(r => r.Id == Restaurant.Id).ToCollectionAsync();
                add.Content = "Save";
                clear.Content = "Delete";
                Restaurant = items.First();
                
                name.Text = Restaurant.Name;
                address.Text = Restaurant.Address;
                contact.Text = Restaurant.ContactNo;
                cost.Text = Restaurant.Cost.ToString();
                description.Document.SetText(TextSetOptions.None, Restaurant.Description);
                creditCard.IsChecked = Restaurant.CreditCard;
                wifi.IsChecked = Restaurant.Wifi;
                petFriendly.IsChecked = Restaurant.PetFriendly;
                outdoorSeating.IsChecked = Restaurant.OutdoorSeating;
                coverPhoto.Source = ByteArrayBitmapExtensions.AsBitmapImage(Restaurant.Image);
                
                foreach (StoreTime st in Restaurant.StoreTime)
                {
                    switch (st.Day)
                    {
                        case DayOfWeek.Sunday:
                            sun.IsChecked = true;
                            sunOpenTime.Time = st.OpenTime;
                            sunOpenTime.IsEnabled = true;
                            sunCloseTime.IsEnabled = true;
                            sunCloseTime.Time = st.CloseTime;
                            break;
                        case DayOfWeek.Monday:
                            mon.IsChecked = true;
                            monOpenTime.Time = st.OpenTime;
                            monOpenTime.IsEnabled = true;
                            monCloseTime.IsEnabled = true;

                            monCloseTime.Time = st.CloseTime;
                            break;
                        case DayOfWeek.Tuesday:
                            tue.IsChecked = true;
                            tueOpenTime.Time = st.OpenTime;
                            tueOpenTime.IsEnabled = true;
                            tueCloseTime.IsEnabled = true;
                            tueCloseTime.Time = st.CloseTime;
                            break;
                        case DayOfWeek.Wednesday:
                            wed.IsChecked = true;
                            wedOpenTime.Time = st.OpenTime;
                            wedCloseTime.Time = st.CloseTime;
                            wedOpenTime.IsEnabled = true;
                            wedCloseTime.IsEnabled = true;

                            break;
                        case DayOfWeek.Thursday:
                            thu.IsChecked = true;
                            thuOpenTime.Time = st.OpenTime;
                            thuOpenTime.IsEnabled = true;
                            thuCloseTime.IsEnabled = true;
                            thuCloseTime.Time = st.CloseTime;
                            break;
                        case DayOfWeek.Friday:
                            fri.IsChecked = true;
                            friOpenTime.Time = st.OpenTime;
                            friOpenTime.IsEnabled = true;
                            friCloseTime.IsEnabled = true;
                            friCloseTime.Time = st.CloseTime;
                            break;
                        case DayOfWeek.Saturday:
                            sat.IsChecked = true;
                            satCloseTime.Time = st.CloseTime;
                            satOpenTime.IsEnabled = true;
                            satCloseTime.IsEnabled = true;
                            satOpenTime.Time = st.OpenTime;
                            break;
                    }
                }

                foreach (Cuisine c in Restaurant.Cuisine)
                {
                    Tags.SelectedItem = c.CuisineType;
                }

                foreach (Menu m in Restaurant.Menu)
                {
                    menu.Add(new MenuImage { Image = ByteArrayBitmapExtensions.AsBitmapImage(m.MenuImage), Id = m.Id});
                }
                menuImages.ItemsSource = menu;


            }
            else
            {
                Restaurant = new Restaurant();
            }
        }


        private async void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Restaurant.Name = name.Text;
                Restaurant.Address = address.Text;
                Restaurant.Cost = Convert.ToInt32(cost.Text);
                Restaurant.ContactNo = contact.Text;
                string value = string.Empty;
                description.Document.GetText(TextGetOptions.AdjustCrlf, out value);

                Restaurant.Description = value;
                Restaurant.CreditCard = (bool)creditCard.IsChecked;
                Restaurant.Wifi = (bool)wifi.IsChecked;
                Restaurant.PetFriendly = (bool)petFriendly.IsChecked;
                Restaurant.OutdoorSeating = (bool)outdoorSeating.IsChecked;
                foreach (CheckBox cb in cbs)
                {
                    if ((bool)cb.IsChecked)
                    {
                        DayOfWeek day = DayOfWeek.Sunday;
                        TimeSpan openTime;
                        TimeSpan closeTime;
                        switch (cb.Content as string)
                        {
                            case "Sun":
                                day = DayOfWeek.Sunday;
                                openTime = sunOpenTime.Time;
                                closeTime = sunCloseTime.Time;
                                storeTimes.Add(new StoreTime { Day = day, OpenTime = openTime, CloseTime = closeTime, Id = Guid.NewGuid().ToString() });
                                break;
                            case "Mon":
                                day = DayOfWeek.Monday;
                                openTime = monOpenTime.Time;
                                closeTime = monCloseTime.Time;
                                storeTimes.Add(new StoreTime { Day = day, OpenTime = openTime, CloseTime = closeTime, Id = Guid.NewGuid().ToString() });
                                break;
                            case "Tue":
                                day = DayOfWeek.Tuesday;
                                openTime = tueOpenTime.Time;
                                closeTime = tueCloseTime.Time;
                                storeTimes.Add(new StoreTime { Day = day, OpenTime = openTime, CloseTime = closeTime, Id = Guid.NewGuid().ToString() });
                                break;
                            case "Wed":
                                day = DayOfWeek.Wednesday;
                                openTime = wedOpenTime.Time;
                                closeTime = wedCloseTime.Time;
                                storeTimes.Add(new StoreTime { Day = day, OpenTime = openTime, CloseTime = closeTime, Id = Guid.NewGuid().ToString() });
                                break;
                            case "Thu":
                                day = DayOfWeek.Thursday;
                                openTime = thuOpenTime.Time;
                                closeTime = thuCloseTime.Time;
                                storeTimes.Add(new StoreTime { Day = day, OpenTime = openTime, CloseTime = closeTime, Id = Guid.NewGuid().ToString() });
                                break;
                            case "Fri":
                                day = DayOfWeek.Friday;
                                openTime = friOpenTime.Time;
                                closeTime = friCloseTime.Time;
                                storeTimes.Add(new StoreTime { Day = day, OpenTime = openTime, CloseTime = closeTime, Id = Guid.NewGuid().ToString() });
                                break;
                            case "Sat":
                                day = DayOfWeek.Saturday;
                                openTime = satOpenTime.Time;
                                closeTime = satCloseTime.Time;
                                storeTimes.Add(new StoreTime { Day = day, OpenTime = openTime, CloseTime = closeTime, Id = Guid.NewGuid().ToString() });
                                break;
                        }
                        
                    }

                }

                var selectedItems = Tags.SelectedItems;
                foreach (CuisineType selectedItem in selectedItems)
                {

                    cuisine.Add(new Cuisine { CuisineType = selectedItem, Id = Guid.NewGuid().ToString() });
                }

                Restaurant.Cuisine = cuisine;
                Restaurant.StoreTime = storeTimes;

                if (add.Content as string == "Add")
                {


                    Restaurant.AdminId = App.Admin.Id;
                    Restaurant.Menu = new List<Menu>();
                    foreach (MenuImage img in menu)
                    {
                        Restaurant.Menu.Add(new Menu { Id = Guid.NewGuid().ToString(), MenuImage = await ByteArrayBitmapExtensions.AsByteArray(img.File) });
                    }
                    App.Admin.Restaurant.Add(Restaurant);
                    Restaurant.Id = Guid.NewGuid().ToString();
                    await App.MobileService.GetTable<Admin>().UpdateAsync(App.Admin);
                    var dialog = new MessageDialog("Record has been added");
                    await dialog.ShowAsync();
                    Clear();
                    this.Frame.GoBack();


                }
                else
                {


                    Restaurant.AdminId = App.Admin.Id;
                    App.Admin.Restaurant.Add(Restaurant);

                    await App.MobileService.GetTable<Restaurant>().UpdateAsync(Restaurant);
                    var dialog = new MessageDialog("Record has been updated");
                    await dialog.ShowAsync();
                    //App.Admin.Restaurant.RemoveAll(r => r.Id == Restaurant.Id);



                }

            }
            catch
            {
                ContentDialog confirmDialog = new ContentDialog()
                {
                    Title = "Failed",
                    Content = "Invalid input  \"" + cost.Text + "\" in Cost field",
                    PrimaryButtonText = "Ok"
                };
                await confirmDialog.ShowAsync();
            }
            

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


        public List<CuisineType> Cuisines
        {
            get { return Enum.GetValues(typeof(CuisineType)).Cast<CuisineType>().ToList(); }
        }



        private async void upload_Click(object sender, RoutedEventArgs e)
        {
            
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation =
                PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {

                Restaurant.Image = await ByteArrayBitmapExtensions.AsByteArray(file);
                coverPhoto.Source = ByteArrayBitmapExtensions.AsBitmapImage(Restaurant.Image);
                removeCoverphoto.Visibility = Visibility.Visible;
                
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            Clear();

        }

        private void Clear()
        {
            List<UIElement> uies = new List<UIElement>();

            FindChildren(uies, form);

            foreach (UIElement ui in uies)
            {
                if (ui is TimePicker)
                {
                    (ui as TimePicker).Time = new TimeSpan(0);
                    (ui as TimePicker).IsEnabled = false;
                }
                else if (ui is CheckBox)
                {
                    (ui as CheckBox).IsChecked = false;
                }
                else if (ui is TextBox)
                {
                    (ui as TextBox).Text = string.Empty;
                }
            }
            Tags.SelectedIndex = -1;
            menu.Clear();
            coverPhoto.Source = new BitmapImage(new Uri("ms-appx:///Assets/coverphoto.png"));
            description.Document.SetText(Windows.UI.Text.TextSetOptions.None, "");
        }

        private async void menuImage_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation =
            PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            IReadOnlyList <StorageFile> file = await picker.PickMultipleFilesAsync();
            if (file != null)
            {
                foreach (StorageFile f in file)
                {
                    menu.Add(new MenuImage { Image = await ByteArrayBitmapExtensions.AsBitmapImage(f), File = f });
                }   
                Validate();
            }
        }

        private void deleteMenuImage_Click(object sender, RoutedEventArgs e)
        {
            if(add.Content as string == "Save")
            {
                Restaurant.Menu.RemoveAll(m => m.Id == (menuImages.SelectedItem as MenuImage).Id);
            }

            menu.Remove(menuImages.SelectedItem as MenuImage);

            Validate();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            Validate();
        }

        private void Validate()
        {
            bool isTbFilled, isStoreTimeFilled, isMenuFilled, isTagSelected;
            isTagSelected = isStoreTimeFilled = isMenuFilled = false;
            isTbFilled = true;

            foreach (TextBox tb in tbs) 
            {
                isTbFilled &= tb.Text != string.Empty;
            }
            foreach(CheckBox cb in cbs)
            {
                switch (cb.Content as string)
                {
                    case "Sun":
                    case "Mon":
                    case "Tue":
                    case "Wed":
                    case "Thu":
                    case "Fri":
                    case "Sat":
                        isStoreTimeFilled |= (bool)cb.IsChecked;
                        break;
                } 
            }
            isMenuFilled = menu.Count > 0;
            deleteMenuImage.Visibility = isMenuFilled ? Visibility.Visible : Visibility.Collapsed;
            isTagSelected = Tags.SelectedItems.Count > 0;
            add.IsEnabled = isTbFilled && isStoreTimeFilled && isMenuFilled && isTagSelected;
        }

        private void Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            switch(cb.Content as string)
            {
                case "Sun": sunOpenTime.IsEnabled = sunCloseTime.IsEnabled = (bool)cb.IsChecked;
                            break;
                case "Mon": monOpenTime.IsEnabled = monCloseTime.IsEnabled = (bool)cb.IsChecked;
                            break;
                case "Tue": tueOpenTime.IsEnabled = tueCloseTime.IsEnabled = (bool)cb.IsChecked;
                            break;
                case "Wed": wedOpenTime.IsEnabled = wedCloseTime.IsEnabled = (bool)cb.IsChecked;
                            break;
                case "Thu": thuOpenTime.IsEnabled = thuCloseTime.IsEnabled = (bool)cb.IsChecked;
                            break;
                case "Fri": friOpenTime.IsEnabled = friCloseTime.IsEnabled = (bool)cb.IsChecked;
                            break;
                case "Sat": satOpenTime.IsEnabled = satCloseTime.IsEnabled = (bool)cb.IsChecked;
                            break;
            }
            Validate();
        }

        private void removeCoverphoto_Click(object sender, RoutedEventArgs e)
        {
            coverPhoto.Source = new BitmapImage(new Uri("ms-appx:///Assets/coverphoto.png"));
            removeCoverphoto.Visibility = Visibility.Collapsed;
        }

        private void Tags_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Validate();
        }
    }
}
