using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Bewise.Phone;
using Microsoft.Phone.Controls;

namespace ControlSamples.Views
{
    public partial class Facebook 
    {
        public Facebook()
        {
            InitializeComponent();
            AfterInitialize();
        }

        // Connection
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (facebook.IsConnected)
                MessageBox.Show("You are already connected");
            else
            {
                facebook.ConnectionCompleted += new EventHandler(facebook_ConnectionCompleted);
                facebook.Connect();
            }
        }

        void facebook_ConnectionCompleted(object sender, EventArgs e)
        {
            // Connection complete
            //facebook.GetNewsfeedCompleted += new EventHandler<GetNewsfeedEventArgs>(facebook_GetNewsfeedCompleted);
            //facebook.GetNewsfeedAsync();
        }

        //void facebook_GetNewsfeedCompleted(object sender, GetNewsfeedEventArgs e)
        //{
        //    listFeed.ItemsSource = e.Newsfeed;
        //}


        // Post to the user wall
        private void PostWall_Click(object sender, RoutedEventArgs e)
        {
            if (!facebook.IsConnected)
                MessageBox.Show("You must be connected to facebook before to update your status");
            else
                facebook.PostToMyWall(null, null, null, tbPublishOnMyWall.Text, null, null);
        }


        // Download friend list
        private void loadFriends_Click(object sender, RoutedEventArgs e)
        {
            if (!facebook.IsConnected)
                MessageBox.Show("You must be connected to facebook before to download your friend list");
            else
            {
                loadFriends.Visibility = Visibility.Collapsed;
                facebook.GetFriendsCompleted += new EventHandler<Bewise.Phone.GetFriendsEventArgs>(facebook_GetFriendsCompleted);
                facebook.GetFriendsListAsync();
            }
        }

        void facebook_GetFriendsCompleted(object sender, Bewise.Phone.GetFriendsEventArgs e)
        {
            friendList.ItemsSource = e.Friends;
        }


        // Post to a friend's wall
        private void friendList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FacebookUser user = friendList.SelectedItem as FacebookUser;
            if (user != null)
            {
                facebook.PostToFriendWall(user.ID, null, null, null, tbPublishOnFriendWall.Text, null, null);
            }
        }


        // Post with automatic connection :
        // You can use this code sample to post without requiring to connect in first place.
        // If the facebook control is already connected, the message will be posted.
        // If it's not, it will connect and then post the message
        private void ConnectAndPost_Click(object sender, RoutedEventArgs e)
        {
            if (!facebook.IsConnected)
            {
                facebook.ConnectionCompleted += new EventHandler(facebook_AutoConnectionCompleted);
                facebook.Connect();
            }
            else
            {
                Post();
            }
        }

        void facebook_AutoConnectionCompleted(object sender, EventArgs e)
        {
            Post();
        }

        private void Post()
        {
            facebook.PostToMyWall(null, null, null, tbPublishOnMyWallAutoConnect.Text, null, null);
        }

    }
}