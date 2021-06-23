using System;
using System.Net;
using System.Windows;
using System.Windows.Navigation;

namespace Bewise.Phone
{
    public partial class FacebookLoginControl
    {
        public event EventHandler ConnectionCompleted;
        public event EventHandler PostToMyWallCompleted;
        public event EventHandler PostToFriendWallCompleted;
        public event EventHandler<UserCanceledEventArgs> OnError;
        public event EventHandler<GetFriendsEventArgs> GetFriendsCompleted;
        public event EventHandler<GetNewsfeedEventArgs> GetNewsfeedCompleted;

        public string AppID
        {
            get;
            set;
        }

        public string AppSecret
        {
            get;
            set;
        }

        public FacebookLoginControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        public void Connect()
        {
            try
            {
                Visibility = Visibility.Visible;
                wbLogin.Navigate(FacebookHelper.GetLoginUri(AppID));
            }
            catch
            {
                RaiseOnError();
            }
        }

        void WbLogin_OnLoadCompleted(object sender, NavigationEventArgs e)
        {
            try
            {
                if (wbLogin.Source != e.Uri)
                {
                    RaiseOnError();
                    return;
                }
                string strLoweredAddress = e.Uri.OriginalString.ToLower();
                if (strLoweredAddress.StartsWith("http://www.facebook.com/connect/login_success.html?code="))
                {
                    wbLogin.Visibility = Visibility.Collapsed;
                    wbLogin.Navigate(FacebookHelper.GetTokenLoadUri(e.Uri.OriginalString.Substring(56), AppID, AppSecret));
                    return;
                }
                string strTest = wbLogin.SaveToString();
                if (strTest.Contains("access_token"))
                {
                    wbLogin.Visibility = Visibility.Collapsed;
                    int nPos = strTest.IndexOf("access_token");
                    string strPart = strTest.Substring(nPos + 13);
                    nPos = strPart.IndexOf("</PRE>");
                    strPart = strPart.Substring(0, nPos);
                    nPos = strPart.IndexOf("&expires=");
                    if (nPos != -1)
                    {
                        strPart = strPart.Substring(0, nPos);
                    }
                    FacebookHelper.AccessToken = strPart;

                    Visibility = Visibility.Collapsed;
                    if (ConnectionCompleted != null)
                        ConnectionCompleted(this, EventArgs.Empty);
                }
                else if (strTest.Contains("Success"))
                {
                    RaiseOnError(true);
                    return;
                }
                else
                {
                    wbLogin.Visibility = Visibility.Visible;
                }
            }
            catch
            {
                RaiseOnError();
            }
        }

        void RaiseOnError(bool userCanceled = false)
        {
            wbLogin.Visibility = Visibility.Collapsed;

            Dispatcher.BeginInvoke(() =>
                                       {
                                           if (OnError != null)
                                               OnError(this, new UserCanceledEventArgs { UserCanceled = userCanceled });
                                       });
        }

        public bool IsConnected
        {
            get
            {
                return !string.IsNullOrEmpty(FacebookHelper.AccessToken);
            }
        }

        public void PostToMyWall(string message)
        {
            PostToMyWall(null, null, null, message, null, null);
        }

        public void PostToMyWall(string caption, string description, string link, string message, string name, string pictureLink)
        {
            Visibility = Visibility.Visible;
            WebClient webClient = new WebClient();

            string strRet = CreatePostString(caption, description, link, message, name, pictureLink);

            webClient.UploadStringCompleted += webClientPostToMyWall_UploadStringCompleted;
            webClient.UploadStringAsync(FacebookHelper.GetPostMessageUri("me"), "POST", strRet);
        }

        void webClientPostToMyWall_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                RaiseOnError();
                return;
            }
            Visibility = Visibility.Collapsed;
            if (PostToMyWallCompleted != null)
                PostToMyWallCompleted(this, EventArgs.Empty);
        }

        public void PostToFriendWall(String userId, String message)
        {
            PostToFriendWall(userId, null, null, null, message, null, null);
        }

        public void PostToFriendWall(String userId, string caption, string description, string link, string message, string name, string pictureLink)
        {
            Visibility = Visibility.Visible;
            WebClient webClient = new WebClient();

            string strRet = CreatePostString(caption, description, link, message, name, pictureLink);

            webClient.UploadStringCompleted += webClientPostToFriendWall_UploadStringCompleted;
            webClient.UploadStringAsync(FacebookHelper.GetPostMessageUri(userId), "POST", strRet);
        }

        void webClientPostToFriendWall_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                RaiseOnError();
                return;
            }
            Visibility = Visibility.Collapsed;
            if (PostToFriendWallCompleted != null)
                PostToFriendWallCompleted(this, EventArgs.Empty);
        }

        private string CreatePostString(string caption, string description, string link, string message, string name, string pictureLink)
        {
            string strRet = "access_token=" + FacebookHelper.AccessToken;
            if (!string.IsNullOrEmpty(caption))
            {
                strRet += "&caption=" + HttpUtility.UrlEncode(caption);
            }
            if (!string.IsNullOrEmpty(description))
            {
                strRet += "&description=" + HttpUtility.UrlEncode(description);
            }
            if (!string.IsNullOrEmpty(link))
            {
                strRet += "&link=" + HttpUtility.UrlEncode(link);
            }
            if (!string.IsNullOrEmpty(message))
            {
                strRet += "&message=" + HttpUtility.UrlEncode(message);
            }
            if (!string.IsNullOrEmpty(name))
            {
                strRet += "&name=" + HttpUtility.UrlEncode(name);
            }
            if (!string.IsNullOrEmpty(pictureLink))
            {
                strRet += "&picture=" + HttpUtility.UrlEncode(pictureLink);
            }
            return strRet;
        }

        public void GetFriendsListAsync()
        {
            Visibility = Visibility.Visible;
            WebClient webClient = new WebClient();

            webClient.DownloadStringCompleted += FriendListReceived;
            webClient.DownloadStringAsync(FacebookHelper.GetLoadFriendsUri());
        }

        void FriendListReceived(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                RaiseOnError();
                return;
            }

            FacebookFriendsList result = FacebookHelper.Deserialize<FacebookFriendsList>(e.Result);

            Visibility = Visibility.Collapsed;
            if (GetFriendsCompleted != null)
                GetFriendsCompleted(this, new GetFriendsEventArgs { Friends = result.Friends });
        }

        public void GetNewsfeedAsync()
        {
            Visibility = Visibility.Visible;
            WebClient webClient = new WebClient();

            webClient.DownloadStringCompleted += NewsfeedReceived;
            webClient.DownloadStringAsync(FacebookHelper.GetNewsfeedUri());
        }

        void NewsfeedReceived(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                RaiseOnError();
                return;
            }

            FacebookNewsfeed result = FacebookHelper.Deserialize<FacebookNewsfeed>(e.Result);

            Visibility = Visibility.Collapsed;
            if (GetNewsfeedCompleted != null)
                GetNewsfeedCompleted(this, new GetNewsfeedEventArgs{Newsfeed = result.Newsfeed});
        }
    }
}
