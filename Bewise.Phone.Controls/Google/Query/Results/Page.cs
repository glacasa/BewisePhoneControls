using System.Runtime.Serialization;

namespace Bewise.Phone.Google
{
    [DataContract]
    public class Page
    {
        [DataMember(Name="start")]
        public int Start
        {
            get;
            set;
        }

        [DataMember(Name="label")]
        public string Label
        {
            get;
            set;
        }
    }
}
