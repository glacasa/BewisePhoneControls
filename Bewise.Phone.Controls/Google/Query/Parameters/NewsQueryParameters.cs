using System;

namespace Bewise.Phone.Google
{
    /// <summary>
    /// This optional argument tells the news search system to scope search results to a particular location.
    /// With this argument present, the query argument (q) becomes optional. Note, this is a very new feature and
    /// locally scoped query coverage is somewhat sparse. You must supply either a city, state, country, or zip
    /// code as in geo=Santa%20Barbara or geo=British%20Columbia or geo=Peru or geo=93108. Note that this optional
    /// argument cannot be used with the topic argument.
    /// </summary>
    public class QueryGeoLocationParameter : BaseQueryParameter<string>
    {
        public QueryGeoLocationParameter() { Key = "geo"; }
    }
    /// <summary>
    /// This optional argument tells the news search system to scope search results to include only quote typed results
    /// (rather than classic news article style results). With this argument present, the query argument (q) becomes optional.
    /// The value of this argument designates a prominent individual whose quotes have been recognized and classified by
    /// the Google News search service. For instance, Barack Obama has a qsid value of tPjE5CDNzMicmM and John McCain
    /// has a value of lE61RnznhxvadM. Note, this is a very new feature and we currently do not have a good search or
    /// descovery mechanism for these qsid values..
    /// </summary>
    public class QueryQuotePersonIdParameter : BaseQueryParameter<string>
    {
        public QueryQuotePersonIdParameter() { Key = "qsid"; }
    }
    /// <summary>
    /// This optional argument specifies which type of listing the user is interested in. 
    /// If this argument is not supplied, the default value of localonly is used.
    /// A topic selection can only be used without a query. If both a query and topic are specified, the 
    /// query will be ignored. It is also not possible to scope a topic using the geo argument.
    /// Topics vary slightly from edition to edition. E.g., in African editions like Namibia or 
    /// Zimbabwe(&ned=en_na, &ned=en_zw) the topic af is available and represents the African topic. 
    /// In general, if you are viewing an edition of Google News and see a topic of interest, click on 
    /// the topic header and view the &topic argument in the browser's address bar.
    /// </summary>
    public class QueryTopicParameter : BaseQueryParameter<NewsTopic?>
    {
        public QueryTopicParameter() { Key = "topic"; }

        public override string GetValueAsString()
        {
            if (Value == null)
                return string.Empty;

            return Enum.GetName(Value.GetType(), Value);
        }
    }
    /// <summary>
    /// This optional argument tells the news search system which edition of news to pull results from
    /// Use the resource NewsEdition to choose your desired news edition.
    /// </summary>
    public class QueryNewsEditionParameter : BaseQueryParameter<string>
    {
        public QueryNewsEditionParameter() { Key = "ned"; }
    }
}
