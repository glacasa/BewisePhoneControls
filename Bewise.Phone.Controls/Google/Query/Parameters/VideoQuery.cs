using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bewise.Phone.Google
{
    /// <summary>
    /// Provides a google video query. AJAX API URL: http://ajax.googleapis.com/ajax/services/search/video.
    /// </summary>    
    public class VideoQuery : BaseQuery
    {
        public VideoQuery(string queryText) : base(queryText) 
        { 
            ServiceName = QueryServicesConsts.VideoQuery;

            this.InitQueryParameter<QueryScoringParameter>(ref m_QueryScoringParameter);
        }

        private QueryScoringParameter m_QueryScoringParameter;
        public QueryScoringParameter Scoring
        {
            get { return m_QueryScoringParameter; }
            set { m_QueryScoringParameter = value; }
        }
        
    }
}
