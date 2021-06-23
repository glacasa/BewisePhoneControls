using System;
using System.Runtime.Serialization;

namespace Bewise.Phone.Google
{
    [DataContract]
    public class GooglePatentResult : IGoogleResult
    {
        /// <summary>
        /// Supplies the title of the patent result.
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
        /// Supplies an escaped version of the patent URL.
        /// </summary>
        /// <value></value>
        [DataMember(Name="url")]
        public string Url
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies a snippet style description of the patent.
        /// </summary>
        /// <value></value>
        [DataMember(Name="content")]
        public string Content
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the application filing date of the patent (rfc-822 format).
        /// </summary>
        /// <value></value>
        [DataMember(Name="applicationDate")]
        public DateTime ApplicationDate
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the patent number for issued patents, and the application number for filed,
        /// but not yet issued patents.
        /// </summary>
        /// <value></value>
        [DataMember(Name="patentNumber")]
        public double PatentNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the status of the patent which can either be "filed" for filed, but not
        /// yet issued patents, or "issued" for issued patents.
        /// </summary>
        /// <value></value>
        [DataMember(Name="patentStatus")]
        public string PatentStatus
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the assignee of the patent.
        /// </summary>
        /// <value></value>
        [DataMember(Name="assignee")]
        public string Assignee
        {
            get;
            set;
        }

        /// <summary>
        /// Supplies the url of a thumbnail image which visually represents the patent.
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
