using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Bewise.Phone
{
    internal static class FacebookHelper
    {
        static string accessToken;

        public static string AccessToken
        {
            get { return accessToken; }
            set { accessToken = value; }
        }

        private const string loginURL = "https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri=http://www.facebook.com/connect/login_success.html&skipcookie=1&display=touch&scope=publish_stream,user_hometown";
        private const string accessTokenURL = "https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri=http://www.facebook.com/connect/login_success.html&client_secret={1}&code={2}";
        private const string postMessageURL = "https://graph.facebook.com/{0}/feed";
        private const string loadFriendsURL = "https://graph.facebook.com/me/friends?access_token={0}";
        private const string newsFeedURL = "https://graph.facebook.com/me/home?access_token={0}";

        internal static Uri GetPostMessageUri(String friendName)
        {
            String myWallUri = String.Format(postMessageURL, friendName);
            return (new Uri(myWallUri, UriKind.Absolute));
        }

        internal static Uri GetLoginUri(string appID)
        {
            return (new Uri(string.Format(loginURL, appID), UriKind.Absolute));
        }

        internal static Uri GetTokenLoadUri(string strCode, string appID, string appSecret)
        {
            return (new Uri(string.Format(accessTokenURL, appID, appSecret, strCode), UriKind.Absolute));
        }

        internal static Uri GetLoadFriendsUri()
        {
            return (new Uri(string.Format(loadFriendsURL, accessToken), UriKind.Absolute));
        }

        internal static Uri GetNewsfeedUri()
        {
            return (new Uri(string.Format(newsFeedURL, accessToken), UriKind.Absolute));
        }

        internal static T Deserialize<T>(string strData) where T : class
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
            byte[] byteArray = Encoding.UTF8.GetBytes(strData);
            MemoryStream mS = new MemoryStream(byteArray);
            T tRet = jsonSerializer.ReadObject(mS) as T;
            mS.Dispose();
            return (tRet);
        }
    }
}
