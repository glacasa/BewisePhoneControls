using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Bewise.Phone.Google
{
    [DataContract]
    public class GoogleResultSet<ResultType> where ResultType : IGoogleResult
    {

        [DataMember(Name = "responseData")]
        public GoogleResultData<ResultType> Data
        {
            get;
            set;
        }

        [DataMember(Name = "responseStatus")]
        public int Status
        {
            get;
            set;
        }
    }
}
