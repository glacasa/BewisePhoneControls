using System;

namespace Bewise.Phone.Google
{
    /// <summary>
    /// This optional argument supplies the unique id for the Custom Search Engine that should be used for this request 
    /// (e.g., cx=000455696194071821846:reviews).
    /// </summary>
    public class QueryCSEUniqueIdParameter : BaseQueryParameter<string>
    {
        public QueryCSEUniqueIdParameter() { Key = "cx"; }
    }
    /// <summary>
    /// This optional argument supplies the url of a linked Custom Search Engine specification that should be used to satisfy this request 
    /// (e.g., cref=http://www.google.com/cse/samples/vegetarian.xml).
    /// </summary>
    public class QueryCRefParameter : BaseQueryParameter<string>
    {
        public QueryCRefParameter() { Key = "cref"; }
    }
    /// <summary>
    /// This optional argument allows the caller to restrict the search to documents written in a particular language (e.g., lr=lang_ja). 
    /// Note: Use the resource Languages to get a valid language code. e.g. Languages.Hebrew
    /// </summary>
    public class QueryLanguageRestrictParameter : BaseQueryParameter<string>
    {
        public QueryLanguageRestrictParameter() { Key = "lr"; }
    }
    /// <summary>
    /// This optional argument controls turning on or off the duplicate content filter:
    /// 0 - Turns off the duplicate content filter
    /// 1 - Turns on the duplicate content filter (default)
    /// </summary>
    public class QueryFilterParameter : BaseQueryParameter<DuplicateContentFilterLookup?>
    {
        public QueryFilterParameter() { Key = "filter"; }
        public override string GetValueAsString()
        {
            if (Value == null)
                return string.Empty;

            return Enum.GetName(Value.GetType(), Value);
        }
    }
    /// <summary>
    /// This optional argument allows the caller to tailor the results to a specific country. 
    /// The value should be a valid country code (e.g. uk, de, etc.). If this argument is not present, 
    /// then the system will use a value based on the domain used to load the API (e.g. http://www.google.com/jsapi). 
    /// If the API loader was not used, a value of "us" is assumed.
    /// Note: Use the Resource CountryCode to get a valid country code. e.g. CountryCode.Israel
    /// </summary>
    public class QueryCountryCodeParameter : BaseQueryParameter<string>
    {
        public QueryCountryCodeParameter() { Key = "gl"; }
    }
}
