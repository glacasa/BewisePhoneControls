using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Bewise.Phone.Google
{
    [DataContract]
    public class GoogleResultData<ResultType> where ResultType : IGoogleResult
    {
        /// <summary>
        /// The list of the Google Search results.
        /// </summary>
        /// <value></value>
        [DataMember(Name = "results")]
        public List<ResultType> Results
        {
            get;
            set;
        }

        /// <summary>
        /// Provides details about the amount of results, pages and which start indexes are valid to query.
        /// </summary>
        /// <value></value>
        [DataMember(Name = "cursor")]
        public Cursor Cursor
        {
            get;
            set;
        }
    }
}
