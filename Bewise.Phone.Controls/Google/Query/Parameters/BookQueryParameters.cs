namespace Bewise.Phone.Google
{
    /// <summary>
    /// This optional argument tells the book search system to restrict the search to "full view" books,
    /// or all books. A value of as_brr=1 restricts the search to only those books that are viewable in full.
    /// The default case is all books and that is indicated by not specifying this argument.
    /// </summary>
    public class QueryBookTypeParameter : BaseQueryParameter<BookQueryType?>
    {
        public QueryBookTypeParameter() { Key = "as_brr"; }

        public override string GetValueAsString()
        {
            if (Value == null || Value == BookQueryType.all_books)
                return string.Empty;

            return "1";
        }
    }
    /// <summary>
    /// This optional argument tells the book search system to restrict the search to the specified user-defined library.
    /// </summary>
    public class QueryLibraryParameter : BaseQueryParameter<string>
    {
        public QueryLibraryParameter() { Key = "as_list"; }
    }
}
