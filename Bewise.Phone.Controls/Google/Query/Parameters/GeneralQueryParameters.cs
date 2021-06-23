using System;

namespace Bewise.Phone.Google
{
    public class QueryTextParameter : BaseQueryParameter<string>
    {
        public string this[string Value] { get { return Value; } set { Value = value; } }

        public QueryTextParameter() { Key = "q"; }
    }
    public class QueryVersionParameter : BaseQueryParameter<string>
    {
        public QueryVersionParameter() { Key = "v"; }
    }
    /// <summary>
    /// TODO: Maybe use here the class IPAddress?
    /// </summary>
    public class QueryUserIPParameter : BaseQueryParameter<string>
    {
        public QueryUserIPParameter() { Key = "userip"; }
    }
    public class QueryResultSetSizeParameter : BaseQueryParameter<ResultSetSize?>
    {
        public QueryResultSetSizeParameter() { Key = "rsz"; }
        public override string GetValueAsString()
        {
            if (Value == null)
                return string.Empty;

            return Enum.GetName(Value.GetType(), Value);
        }
    }
    /// <summary>
    /// Use the resource Languages to look up a valid language parameter.
    /// </summary>
    public class QueryHostLanguageParameter : BaseQueryParameter<string>
    {
        public QueryHostLanguageParameter() { Key = "hl"; }
    }
    public class QueryAPIKeyParameter : BaseQueryParameter<string>
    {
        public QueryAPIKeyParameter() { Key = "key"; }
    }
    public class QueryStartIndexParameter : BaseQueryParameter<int?>
    {
        public QueryStartIndexParameter() { Key = "start"; }
    }
    /// <summary>
    /// This optional argument supplies the search safety level which may be one of:
    ///     active - enables the highest level of safe search filtering
    ///     moderate - enables moderate safe search filtering (default)
    ///     off - disables safe search filtering
    /// </summary>
    public class QuerySafeParameter : BaseQueryParameter<SearchSafetyLookup?>
    {
        public QuerySafeParameter() { Key = "safe"; }
        public override string GetValueAsString()
        {
            if (Value == null)
                return string.Empty;

            return Enum.GetName(Value.GetType(), Value);
        }
    }

    /// <summary>
    /// This optional argument tells the video search system how to order results. Results may be ordered by relevance 
    /// (which is the default) or by date. To select ordering by relevance, do not supply this argument. 
    /// To select ordering by date, set scoring as scoring=d.
    /// </summary>
    public class QueryScoringParameter : BaseQueryParameter<ResultsOrder?>
    {
        public QueryScoringParameter() { Key = "scoring"; }

        public override string GetValueAsString()
        {
            if (Value == null || Value == ResultsOrder.relevance)
                return string.Empty;

            return "d";
        }
    }
}
