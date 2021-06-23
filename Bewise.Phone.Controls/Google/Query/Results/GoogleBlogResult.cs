using System;
using System.Runtime.Serialization;

namespace Bewise.Phone.Google
{
    [DataContract]
    public class GoogleBlogResult : IGoogleResult
    {
        /// <summary>
        /// Supplies the title of the blog post returned as a search result.
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
        /// Supplies a snippet of content from the blog post associated with this search result.
        /// </summary>
        /// <value></value>
        [DataMember(Name="content")]
        public string Content
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the URL to the blog post referenced in this search result.
        /// </summary>
        /// <value></value>
        [DataMember(Name="postUrl")]
        public string PostUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the name of the author that wrote the blog post.
        /// </summary>
        /// <value></value>
        [DataMember(Name="author")]
        public string Author
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the URL of the blog which contains the post.
        /// Typically, this URL is displayed in green, beneath the blog search
        /// result and is linked to the blog.
        /// </summary>
        /// <value></value>
        [DataMember(Name="blogUrl")]
        public string BlogUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the published date (rfc-822 format) of the blog post referenced
        /// by this search result.
        /// </summary>
        /// <value></value>
        [DataMember(Name="publishedDate")]
        public DateTime PublishedDate
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
