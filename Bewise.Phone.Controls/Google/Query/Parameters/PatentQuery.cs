using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bewise.Phone.Google
{
    /// <summary>
    /// Provides a google patent query. AJAX API URL: http://ajax.googleapis.com/ajax/services/search/patent.
    /// </summary>    
    public class PatentQuery : BaseQuery
    {
        public PatentQuery(string queryText) : base(queryText) 
        { 
            ServiceName = QueryServicesConsts.PatentQuery;

            //Initializing the patent query parameters..
            this.InitQueryParameter<QueryPatentIssuedParameter>(ref m_PatentIssued);
            this.InitQueryParameter<QueryPatentFiledParameter>(ref m_PatentFiled);
            this.InitQueryParameter<QueryScoringParameter>(ref m_Scoring);
        }

        private QueryPatentIssuedParameter m_PatentIssued;
        public QueryPatentIssuedParameter PatentIssued
        {
            get { return m_PatentIssued; }
            set { m_PatentIssued = value; }
        }

        private QueryPatentFiledParameter m_PatentFiled;
        public QueryPatentFiledParameter PatentFiled
        {
            get { return m_PatentFiled; }
            set { m_PatentFiled = value; }
        }

        private QueryScoringParameter m_Scoring;
        public QueryScoringParameter Scoring
        {
            get { return m_Scoring; }
            set { m_Scoring = value; }
        }
        
        
        
    }
}
