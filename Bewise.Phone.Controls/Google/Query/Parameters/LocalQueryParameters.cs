using System;
using Bewise.Phone.Google.Parameters;

namespace Bewise.Phone.Google
{
    /// <summary>
    /// This optional argument supplies the search center point for a local search. It's value is a comma separated latitude/longitude pair, e.g., sll=48.8565,2.3509.
    /// /// Use the LocalCoordinate class to provide a value to the parameter.
    /// </summary>
    public class QueryCenterPointParameter : BaseQueryParameter<LocalCoordinate>
    {
        public QueryCenterPointParameter() { Key = "sll"; }

        public override string GetValueAsString()
        {
            if (Value == null)
                return string.Empty;

            return Value.Latitude + "," + Value.Longitude;
        }
    }
    /// <summary>
    /// This optional argument supplies a bounding box that the local search should be relative to. When using a Google Map, 
    /// the sspn value can be computed using: myMap.getBounds().toSpan().toUrlValue(); (e.g., sspn=0.065169,0.194149).
    /// Use the LocalCoordinate class to provide a value to the parameter.
    /// </summary>
    public class QueryBBoxParameter : BaseQueryParameter<LocalCoordinate>
    {
        public QueryBBoxParameter() { Key = "sspn"; }

        public override string GetValueAsString()
        {
            if (Value == null)
                return string.Empty;

            return Value.Latitude + "," + Value.Longitude;
        }
    }
    /// <summary>
    /// This optional argument specifies which type of listing the user is interested in. 
    /// If this argument is not supplied, the default value of localonly is used.
    /// </summary>
    public class QueryLocalListingTypeParameter : BaseQueryParameter<LocalListingType?>
    {
        public QueryLocalListingTypeParameter() { Key = "mrt"; }

        public override string GetValueAsString()
        {
            if (Value == null)
                return string.Empty;

            return Enum.GetName(Value.GetType(), Value);
        }
    }
}
