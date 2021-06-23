using System.Runtime.Serialization;

namespace Bewise.Phone.Google
{
    [DataContract]
    public class GoogleNewsResult : IGoogleResult
    {
        /// <summary>
        /// Supplies the raw URL of the result.
        /// </summary>
        /// <value></value>
        [DataMember(Name="unescapedUrl")]
        public string UnescapedUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies an escaped version of the news URL.
        /// </summary>
        /// <value></value>
        [DataMember(Name="url")]
        public string Url
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies a snippet of content from the news story that includes the quotes.
        /// </summary>
        /// <value></value>
        [DataMember(Name="content")]
        public string Content
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the name of the person that the quote is attributed to.
        /// </summary>
        /// <value></value>
        [DataMember(Name="author")]
        public string Author
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the name of the publisher of the news story.
        /// </summary>
        /// <value></value>
        [DataMember(Name="publisher")]
        public string Publisher
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the title value of the result.
        /// </summary>
        /// <value></value>
        [DataMember(Name="title")]
        public string Title
        {
            get;
            set;
        }
    }
}
