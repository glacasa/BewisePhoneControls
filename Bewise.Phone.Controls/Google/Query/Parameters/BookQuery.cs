namespace Bewise.Phone.Google
{
    /// <summary>
    /// Provides a google book query. AJAX API URL: http://ajax.googleapis.com/ajax/services/search/books.
    /// </summary>    
    public class BookQuery : BaseQuery
    {
        public BookQuery(string queryText) : base(queryText) 
        { 
            ServiceName = QueryServicesConsts.BookQuery;

            //Initializing the book query parameters..
            InitQueryParameter(ref m_BookType);
            InitQueryParameter(ref m_Library);
        }

        private QueryBookTypeParameter m_BookType;
        public QueryBookTypeParameter BookType
        {
            get { return m_BookType; }
            set { m_BookType = value; }
        }

        private QueryLibraryParameter m_Library;
        public QueryLibraryParameter Library
        {
            get { return m_Library; }
            set { m_Library = value; }
        }
        

    }
}
