using System.Runtime.Serialization;

namespace Bewise.Phone.Google
{
    [DataContract]
    public class GoogleImageResult : IGoogleResult
    {
        /// <summary>
        /// Supplies the title of the image, which is usually the base filename.
        /// </summary>
        /// <value></value>
        [DataMember(Name="title")]
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the title, but unlike .title, this property
        /// is stripped of html markup (e.g., b, i, etc.).
        /// </summary>
        /// <value></value>
        [DataMember(Name="titleNoFormatting")]
        public string TitleNoFormatting
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the raw URL of the image.
        /// </summary>
        /// <value></value>
        [DataMember(Name="unescapedUrl")]
        public string UnescapedUrl
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

        /// <summary>
        /// Supplies a brief snippet of information from the page associated with the search result.
        /// </summary>
        /// <value></value>
        [DataMember(Name="content")]
        public string Content
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the same information as .content, only stripped of HTML formatting.
        /// </summary>
        /// <value></value>
        [DataMember(Name="contentNoFormatting")]
        public string ContentNoFormatting
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the URL of the page containing the image.
        /// </summary>
        /// <value></value>
        [DataMember(Name="originalContextUrl")]
        public string OriginalContextUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the width of the image in pixels.
        /// </summary>
        /// <value></value>
        [DataMember(Name="width")]
        public int Width
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the height of the image in pixels.
        /// </summary>
        /// <value></value>
        [DataMember(Name="height")]
        public int Height
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the width, in pixels, of the image thumbnail.
        /// </summary>
        /// <value></value>
        [DataMember(Name="tbWidth")]
        public int TbWidth
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the height, in pixels, of the image thumbnail.
        /// </summary>
        /// <value></value>
        [DataMember(Name="tbHeight")]
        public int TbHeight
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the url of a thumbnail image.
        /// </summary>
        /// <value></value>
        [DataMember(Name="tbUrl")]
        public string TbUrl
        {
            get;
            set;
        }
    }
}
