using System;
using System.Windows;
using Bewise.Phone;
using Microsoft.Phone.Controls;

namespace WindowsPhoneApplication3
{
    public partial class MainPage : PhoneApplicationPage
    {
        ItemObservableCollection<User> collection;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            collection = new ItemObservableCollection<User>();
            Random rnd = new Random();

            for (int index = 0; index < 500; index++)
            {
                collection.Add(new User { Name = ((char)('a' + rnd.Next(26))) + "", Description = "User " + index });
            }
            lstUsers.ItemsSource = collection;
            lstUsers.OnContinuumCompleted += lstUsers_OnContinuumCompleted;
        }

        void lstUsers_OnContinuumCompleted(object sender, EventArgs e)
        {
            lstUsers.RestoreContinuum();
        }

        private void picture_Checked(object sender, RoutedEventArgs e)
        {
            if (lstUsers == null)
                return;

            lstUsers.PicturePath = "Image";
            lstUsers.ItemsSource = null;
            lstUsers.ItemsSource = collection;
        }

        private void picture_Unchecked(object sender, RoutedEventArgs e)
        {
            if (lstUsers == null)
                return;

            lstUsers.PicturePath = "";
            lstUsers.ItemsSource = null;
            lstUsers.ItemsSource = collection;
        }

        private void rowHeader_Checked(object sender, RoutedEventArgs e)
        {
            if (lstUsers == null)
                return;

            lstUsers.ShowRowHeader = true;
            lstUsers.ItemsSource = null;
            lstUsers.ItemsSource = collection;
        }

        private void rowHeader_Unchecked(object sender, RoutedEventArgs e)
        {
            if (lstUsers == null)
                return;

            lstUsers.ShowRowHeader = false;
            lstUsers.ItemsSource = null;
            lstUsers.ItemsSource = collection;
        }

        private void pump_Checked(object sender, RoutedEventArgs e)
        {
            if (lstUsers == null)
                return;

            lstUsers.IsPumped = true;
            lstUsers.ItemsSource = null;
            lstUsers.ItemsSource = collection;
        }

        private void pump_Unchecked(object sender, RoutedEventArgs e)
        {
            if (lstUsers == null)
                return;

            lstUsers.IsPumped = false;
            lstUsers.ItemsSource = null;

            Dispatcher.BeginInvoke(() =>
                                       {
                                           lstUsers.ItemsSource = collection;
                                       });
        }

        private void lstUsers_SelectedItemChanged(object sender, EventArgs e)
        {
            lstUsers.DoContinuumAnimationOnSelectedItem();
        }
    }
}