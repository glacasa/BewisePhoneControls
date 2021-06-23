using System.Runtime.Serialization;

namespace Bewise.Phone
{
    [DataContract]
    public class FacebookFriendsList
    {
        #region Friends
        private FacebookUser[] friends;
        [DataMember(Name = "data")]
        public FacebookUser[] Friends
        {
            get { return friends; }
            set { friends = value; }
        }
        #endregion
    }
}