namespace Bewise.Phone.Google
{
    /// <summary>
    /// Provides a google blog query. AJAX API URL: http://ajax.googleapis.com/ajax/services/search/blogs.
    /// </summary>    
    public class BlogQuery : BaseQuery
    {
        public BlogQuery(string queryText) : base(queryText) 
        { 
            ServiceName = QueryServicesConsts.BlogQuery;

            InitQueryParameter(ref m_QueryScoringParameter);
        }

        private QueryScoringParameter m_QueryScoringParameter;
        public QueryScoringParameter Scoring
        {
            get { return m_QueryScoringParameter; }
            set { m_QueryScoringParameter = value; }
        }
    }
}
