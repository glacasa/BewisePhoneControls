using System.Runtime.Serialization;

namespace Bewise.Phone
{
    [DataContract]
    public class FacebookHomeTown
    {
        private string name;
        [DataMember(Name = "name")]
        public string Name
        {
            get { return name ?? ""; }
            set { name = value; }
        }
    }
}
