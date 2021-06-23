using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Bewise.Phone.Google
{
    [DataContract]
    public class GoogleBookResult : IGoogleResult
    {
        /// <summary>
        /// Supplies the title of the book.
        /// </summary>
        /// <value></value>
        [DataMember(Name="title")]
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the title, but unlike .title, this property is
        /// stripped of html markup (e.g., b, i, etc.).
        /// </summary>
        /// <value></value>
        [DataMember(Name="titleNoFormatting")]
        public string TitleNoFormatting
        {
            get;
            set;
        }

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
        /// Supplies an escaped version of the book's URL.
        /// </summary>
        /// <value></value>
        [DataMember(Name="url")]
        public string Url
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the list of authors of the book.
        /// </summary>
        /// <value></value>
        [DataMember(Name="authors")]
        public string Authors
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the identifier associated with the book. This is typically an ISBN.
        /// </summary>
        /// <value></value>
        [DataMember(Name="bookId")]
        public string BookId
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the year that the book was published.
        /// </summary>
        /// <value></value>
        [DataMember(Name="publishedYear")]
        public int PublishedYear
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the number of pages contained within the book.
        /// </summary>
        /// <value></value>
        [DataMember(Name="pageCount")]
        public int PageCount
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies an html dom node that represents a thumbnail image of the book's cover.
        /// </summary>
        /// <value></value>
        [DataMember(Name="thumbnailHtml")]
        public string ThumbnailHtml
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }
    }
}
