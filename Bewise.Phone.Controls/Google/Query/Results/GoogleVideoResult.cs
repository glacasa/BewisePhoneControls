using System;
using System.Runtime.Serialization;

namespace Bewise.Phone.Google
{
    [DataContract]
    public class GoogleVideoResult : IGoogleResult
    {
        /// <summary>
        /// Supplies the title of the video result.
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
        /// Supplies the url of a playable version of the video result.
        /// </summary>
        /// <value></value>
        [DataMember(Name="url")]
        public string Url
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies a snippet style description of the video clip.
        /// </summary>
        /// <value></value>
        [DataMember(Name="content")]
        public string Content
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the published date of the video (rfc-822 format).
        /// </summary>
        /// <value></value>
        [DataMember(Name="published")]
        public DateTime Published
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the name of the video's publisher, typically displayed
        /// in green below the video thumbnail, similar to the treatment
        /// used for visibleUrl in the other search result objects.
        /// </summary>
        /// <value></value>
        [DataMember(Name="publisher")]
        public string Publisher
        {
            get;
            set;
        }

        /// <summary>
        /// The approximate duration, in seconds, of the video.
        /// </summary>
        /// <value></value>
        [DataMember(Name="duration")]
        public int Duration
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the width, in pixels, of the video thumbnail.
        /// </summary>
        /// <value></value>
        [DataMember(Name="tbWidth")]
        public int TbWidth
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the height, in pixels, of the video thumbnail.
        /// </summary>
        /// <value></value>
        [DataMember(Name="tbHeight")]
        public int TbHeight
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the url of a thumbnail image which visually represents the video.
        /// </summary>
        /// <value></value>
        [DataMember(Name="tbUrl")]
        public string TbUrl
        {
            get;
            set;
        }

        /// <summary>
        /// If present, supplies the url of the flash version of the video that can be
        /// played inline on your page. To play this video simply create an embed
        /// element on your page using this value as the src attribute and using
        /// application/x-shockwave-flash as the type attribute. If you want the
        /// video to play right away, make sure to append and autoPlay=true to the url.
        /// </summary>
        /// <value></value>
        [DataMember(Name="playUrl")]
        public string PlayUrl
        {
            get;
            set;
        }

        /// <summary>
        /// If present, this property supplies the YouTube user name of the author of the video.
        /// </summary>
        /// <value></value>
        [DataMember(Name="author")]
        public string Author
        {
            get;
            set;
        }

        /// <summary>
        /// If present, this property supplies a count of the number of plays for this video.
        /// </summary>
        /// <value></value>
        [DataMember(Name="viewCount")]
        public int ViewCount
        {
            get;
            set;
        }

        /// <summary>
        /// If present, this property supplies the rating of the video on a scale of 1 to 5.
        /// </summary>
        /// <value></value>
        [DataMember(Name="rating")]
        public int Rating
        {
            get;
            set;
        }
    }
}
