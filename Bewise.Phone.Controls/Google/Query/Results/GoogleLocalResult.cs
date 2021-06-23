using System.Runtime.Serialization;

namespace Bewise.Phone.Google
{
    [DataContract]
    public class GoogleLocalResult : IGoogleResult
    {
        /// <summary>
        /// Supplies the title for the result. In some cases, the title and the streetAddress are the same.
        /// This typically occurs when the search term is a street address such as 1231 Lisa Lane, Los Altos, CA.
        /// </summary>
        /// <value></value>
        [DataMember(Name="title")]
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the title, but unlike .title, this property is stripped of html markup (e.g., b, i, etc.).
        /// </summary>
        /// <value></value>
        [DataMember(Name="titleNoFormatting")]
        public string TitleNoFormatting
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the latitude value of the result.
        /// </summary>
        /// <value></value>
        [DataMember(Name="latitude")]
        public string Latitude
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the longitude value of the result.
        /// </summary>
        /// <value></value>
        [DataMember(Name="longitude")]
        public string Longitude
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the street address and number for the given result. Note: In some cases, this
        /// property may be set to "" if the result has no known street address.
        /// </summary>
        /// <value></value>
        [DataMember(Name="streetAddress")]
        public string StreetAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the city name for the result. Note: In some cases, this property may be set to "".
        /// </summary>
        /// <value></value>
        [DataMember(Name="city")]
        public string City
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies a region name for the result (e.g., in the U.S., this is typically a state
        /// abbreviation, in other regions it might be a province, etc.) Note: In some cases,
        /// this property may be set to "".
        /// </summary>
        /// <value></value>
        [DataMember(Name="region")]
        public string Region
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies a country name for the result. Note: In some cases, this property may be set to "".
        /// </summary>
        /// <value></value>
        [DataMember(Name="country")]
        public string Country
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies an array of phone number objects where each object contains a .type property and a
        /// .number property. The value of the .type property can be one of "main", "fax", "mobile",
        /// "data", or "".
        /// </summary>
        /// <value></value>
        [DataMember(Name="phoneNumbers")]
        public string PhoneNumbers
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies an array consisting of the mailing address lines for this result, for
        /// instance: ["1600 Amphitheatre Pky", "Mountain View, CA 94043"] or ["Via del Corso, 330",
        /// "00186 Roma (RM), Italy"]. To correctly render an address associated with a result,
        /// either use the .html property of the result directly or iterate through this
        /// array and display each addressLine in turn.
        /// </summary>
        /// <value></value>
        [DataMember(Name="addressLines")]
        public string AddressLines
        {
            get;
            set;
        }

        [DataMember(Name="ddrl")]
        public string DDrl
        {
            get;
            set;
        }

        [DataMember(Name="ddUrlToHere")]
        public string DDUrlToHere
        {
            get;
            set;
        }

        [DataMember(Name="ddUrlFromHere")]
        public string DDUrlFromHere
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies a url to a static map image representation of the current result. The image is
        /// 150px wide by 100px tall with a single marker representing the current location. Expected
        /// usage is to hyperlink this image using the url property. The image may be resized using
        /// google.search.LocalSearch.resizeStaticMapUrl(). The Static Map Tile sample demonstrates
        /// one way to use this property.
        /// </summary>
        /// <value></value>
        [DataMember(Name="staticMapUrl")]
        public string StaticMapUrl
        {
            get;
            set;
        }

        /// <summary>
        /// This property indicates the type of this result which can either be "local" in the case of
        /// a local business listing or geocode result, or "kml" in the case of a KML listing.
        /// </summary>
        /// <value></value>
        [DataMember(Name="listingType")]
        public string ListingType
        {
            get;
            set;
        }

        /// <summary>
        /// For "kml" results, this property contains a content snippet associated with the KML result.
        /// For "local" results, this property is the empty string.
        /// </summary>
        /// <value></value>
        [DataMember(Name="content")]
        public string Content
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies an escaped version of the above URL.
        /// </summary>
        /// <value></value>
        [DataMember(Name="url")]
        public string Url
        {
            get;
            set;
        }
    }
}
