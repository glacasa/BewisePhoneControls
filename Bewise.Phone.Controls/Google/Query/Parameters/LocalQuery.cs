using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bewise.Phone.Google
{
    /// <summary>
    /// Provides a google local query. AJAX API URL: http://ajax.googleapis.com/ajax/services/search/local.
    /// </summary>    
    public class LocalQuery : BaseQuery
    {
        public LocalQuery(string queryText) : base(queryText) 
        { 
            ServiceName = QueryServicesConsts.LocalQuery;

            //Initializing the web query parameters..
            this.InitQueryParameter<QueryCenterPointParameter>(ref m_CenterPoint);
            this.InitQueryParameter<QueryBBoxParameter>(ref m_BBox);
            this.InitQueryParameter<QueryLocalListingTypeParameter>(ref m_LocalListingType);
        }

        private QueryCenterPointParameter m_CenterPoint;
        public QueryCenterPointParameter CenterPoint
        {
            get { return m_CenterPoint; }
            set { m_CenterPoint = value; }
        }

        private QueryBBoxParameter m_BBox;
        public QueryBBoxParameter BBox
        {
            get { return m_BBox; }
            set { m_BBox = value; }
        }

        private QueryLocalListingTypeParameter m_LocalListingType;
        public QueryLocalListingTypeParameter LocalListingType
        {
            get { return m_LocalListingType; }
            set { m_LocalListingType = value; }
        }
    }
}
