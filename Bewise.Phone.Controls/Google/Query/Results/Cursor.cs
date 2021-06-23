using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Bewise.Phone.Google
{
    [DataContract]
    public class Cursor
    {
        [DataMember(Name="pages")]
        public List<Page> Pages
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the estimated result count.
        /// </summary>
        /// <value>The estimated result count.</value>
        [DataMember(Name="estimatedResultCount")]
        public int EstimatedResultCount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the index of the current page.
        /// </summary>
        /// <value>The index of the current page.</value>
        [DataMember(Name="currentPageIndex")]
        public int CurrentPageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the more results URL.
        /// </summary>
        /// <value>The more results URL.</value>
        [DataMember(Name="moreResultsUrl")]
        public string MoreResultsURL
        {
            get;
            set;
        }
    }
}
