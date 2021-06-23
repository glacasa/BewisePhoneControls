using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bewise.Phone.Google
{
    /// <summary>
    /// Provides a google web query. AJAX API URL: http://ajax.googleapis.com/ajax/services/search/web.
    /// </summary>    
    public class WebQuery : BaseQuery
    {
        public WebQuery(string queryText)
            : base(queryText)
        {
            ServiceName = QueryServicesConsts.WebQuery;

            //Initializing the web query parameters..
            this.InitQueryParameter<QueryCSEUniqueIdParameter>(ref m_QueryCSEUniqueIdParameter);
            this.InitQueryParameter<QueryCRefParameter>(ref m_QueryCRefParameter);
            this.InitQueryParameter<QuerySafeParameter>(ref m_QuerySafeParameter);
            this.InitQueryParameter<QueryLanguageRestrictParameter>(ref m_QueryLanguageRestrictParameter);
            this.InitQueryParameter<QueryFilterParameter>(ref m_QueryFilterParameter);
            this.InitQueryParameter<QueryCountryCodeParameter>(ref m_QueryCountryCodeParameter);
        }

        private QueryCSEUniqueIdParameter m_QueryCSEUniqueIdParameter;
        public QueryCSEUniqueIdParameter CSEUniqueId
        {
            get { return m_QueryCSEUniqueIdParameter; }
            set { m_QueryCSEUniqueIdParameter = value; }
        }

        private QueryCRefParameter m_QueryCRefParameter;
        public QueryCRefParameter CRef
        {
            get { return m_QueryCRefParameter; }
            set { m_QueryCRefParameter = value; }
        }

        private QuerySafeParameter m_QuerySafeParameter;
        public QuerySafeParameter Safe
        {
            get { return m_QuerySafeParameter; }
            set { m_QuerySafeParameter = value; }
        }

        private QueryLanguageRestrictParameter m_QueryLanguageRestrictParameter;
        public QueryLanguageRestrictParameter LanguageRestrict
        {
            get { return m_QueryLanguageRestrictParameter; }
            set { m_QueryLanguageRestrictParameter = value; }
        }

        private QueryFilterParameter m_QueryFilterParameter;
        public QueryFilterParameter Filter
        {
            get { return m_QueryFilterParameter; }
            set { m_QueryFilterParameter = value; }
        }

        private QueryCountryCodeParameter m_QueryCountryCodeParameter;
        public QueryCountryCodeParameter CountryCode
        {
            get { return m_QueryCountryCodeParameter; }
            set { m_QueryCountryCodeParameter = value; }
        }
        
    }
}
