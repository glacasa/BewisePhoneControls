using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bewise.Phone.Google
{
    /// <summary>
    /// Provides a google news query. AJAX API URL: http://ajax.googleapis.com/ajax/services/search/news.
    /// </summary>    
    public class NewsQuery : BaseQuery
    {
        public NewsQuery(string queryText) : base(queryText) 
        { 
            ServiceName = QueryServicesConsts.NewsQuery;

            //Initializing the news query parameters..
            this.InitQueryParameter<QueryGeoLocationParameter>(ref m_GeoLocation);
            this.InitQueryParameter<QueryQuotePersonIdParameter>(ref m_QuotePersonId);
            this.InitQueryParameter<QueryTopicParameter>(ref m_Topic);
            this.InitQueryParameter<QueryNewsEditionParameter>(ref m_NewsEdition);
            this.InitQueryParameter<QueryScoringParameter>(ref m_Scoring);
        }

        private QueryGeoLocationParameter m_GeoLocation;
        public QueryGeoLocationParameter GeoLocation
        {
            get { return m_GeoLocation; }
            set { m_GeoLocation = value; }
        }

        private QueryQuotePersonIdParameter m_QuotePersonId;
        public QueryQuotePersonIdParameter QuotePersonId
        {
            get { return m_QuotePersonId; }
            set { m_QuotePersonId = value; }
        }


        private QueryTopicParameter m_Topic;
        public QueryTopicParameter Topic
        {
            get { return m_Topic; }
            set { m_Topic = value; }
        }

        private QueryNewsEditionParameter m_NewsEdition;
        public QueryNewsEditionParameter NewsEdition
        {
            get { return m_NewsEdition; }
            set { m_NewsEdition = value; }
        }

        private QueryScoringParameter m_Scoring;
        public QueryScoringParameter Scoring
        {
            get { return m_Scoring; }
            set { m_Scoring = value; }
        }
    }
}
