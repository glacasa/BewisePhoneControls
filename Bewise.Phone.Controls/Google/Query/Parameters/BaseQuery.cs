using System.Collections.Generic;
using System.Net;

namespace Bewise.Phone.Google
{
    public abstract class BaseQuery : IGoogleQuery
    {
        #region Data Members
        protected string ServiceUrl { get; set; }
        protected string ServiceName { get; set; }
        protected List<IQueryParameter> m_QueryParameters = new List<IQueryParameter>();  
        #endregion
        #region Properties
        private QueryTextParameter m_Text;
        public QueryTextParameter Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }

        private QueryVersionParameter m_Version;
        public QueryVersionParameter Version
        {
            get { return m_Version; }
            set { m_Version = value; }
        }

        private QueryUserIPParameter m_UserIP;
        public QueryUserIPParameter UserIP
        {
            get { return m_UserIP; }
            set { m_UserIP = value; }
        }

        private QueryResultSetSizeParameter m_ResultSetSize;
        public QueryResultSetSizeParameter ResultSetSize
        {
            get { return m_ResultSetSize; }
            set { m_ResultSetSize = value; }
        }

        private QueryHostLanguageParameter m_HostLangauge;
        public QueryHostLanguageParameter HostLangauge
        {
            get { return m_HostLangauge; }
            set { m_HostLangauge = value; }
        }

        private QueryAPIKeyParameter m_APIKey;
        public QueryAPIKeyParameter APIKey
        {
            get { return m_APIKey; }
            set { m_APIKey = value; }
        }

        private QueryStartIndexParameter m_StartIndex;
        public QueryStartIndexParameter StartIndex
        {
            get { return m_StartIndex; }
            set { m_StartIndex = value; }
        }

        #endregion
       
        #region Ctor
        protected BaseQuery(string queryText)
        {
            ServiceUrl = "http://ajax.googleapis.com/ajax/services/search/";
            //Initializing the base query parameters..
            InitQueryParameter(ref m_Text);
            InitQueryParameter(ref m_Version);
            InitQueryParameter(ref m_ResultSetSize);
            InitQueryParameter(ref m_HostLangauge);
            InitQueryParameter(ref m_APIKey);
            InitQueryParameter(ref m_StartIndex);

            Text.Value = HttpUtility.UrlEncode(queryText);
            //The query default version value is 1.0. Currently it's the only valid value in this parameter.
            Version.Value = "1.0";
        }        
        #endregion

        #region IGoogleQuery Members

        public string ToUrl()
        {
            string result = string.Empty;
            result = ServiceUrl + ServiceName + "?";

            foreach (IQueryParameter queryParam in m_QueryParameters)
            {
                string queryParamStr = queryParam.GetQueryParameter();
                if (!string.IsNullOrEmpty(queryParamStr))
                    result += queryParamStr;
            }

            result = result.TrimEnd('&');

            return result;
        }

        #endregion

        #region BaseQuery Methods
        protected void InitQueryParameter<QP>(ref QP queryParameter) where QP : IQueryParameter, new()
        {
            queryParameter = new QP();
            m_QueryParameters.Add(queryParameter);
        }        
        #endregion       
    }
}
