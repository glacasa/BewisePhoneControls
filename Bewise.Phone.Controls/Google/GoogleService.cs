using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Net;

namespace Bewise.Phone.Google
{
    public class GoogleService<ResultType> where ResultType : IGoogleResult
    {
        public delegate void OnSearchCompletedHandler(GoogleResultSet<ResultType> result);
        public delegate void OnSearchErrorHandler(string message);

        public event OnSearchCompletedHandler OnSearchCompleted;
        public event OnSearchErrorHandler OnSearchError;

        private readonly WebClient m_WebClient;
        public GoogleService()
        {
            m_WebClient = new WebClient();
            m_WebClient.Encoding = Encoding.UTF8;
        }

        public void Search(IGoogleQuery query)
        {
            m_WebClient.DownloadStringCompleted += m_WebClient_DownloadStringCompleted;
            m_WebClient.DownloadStringAsync(new Uri(query.ToUrl(), UriKind.Absolute));
        }

        void m_WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            m_WebClient.DownloadStringCompleted -= m_WebClient_DownloadStringCompleted;
            try
            {
                GoogleResultSet<ResultType> result = Deserialize<GoogleResultSet<ResultType>>(e.Result);

                if (OnSearchCompleted != null)
                    OnSearchCompleted(result);
            }
            catch (Exception ex)
            {
                if (OnSearchError != null)
                    OnSearchError(ex.Message);
            }
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
