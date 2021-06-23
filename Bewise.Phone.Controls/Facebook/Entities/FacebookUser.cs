using System.Runtime.Serialization;

namespace Bewise.Phone
{
    [DataContract]
    public class FacebookUser
    {
        private string id;
        [DataMember(Name = "id")]
        public string ID
        {
            get { return id ?? ""; }
            set { id = value; }
        }
        private string name;
        [DataMember(Name = "name")]
        public string Name
        {
            get { return name ?? ""; }
            set { name = value; }
        }

        private string pictureLink;
        [DataMember(Name = "picture")]
        public string PictureLink
        {
            get { return pictureLink ?? ""; }
            set { pictureLink = value; }
        }

        private string gender;
        [DataMember(Name = "gender")]
        public string Gender
        {
            get { return gender ?? ""; }
            set { gender = value; }
        }

        private string link;
        [DataMember(Name = "link")]
        public string Link
        {
            get { return link ?? ""; }
            set { link = value; }
        }

        private FacebookHomeTown homeTown;
        [DataMember(Name = "hometown")]
        public FacebookHomeTown HomeTown
        {
            get { return homeTown ?? new FacebookHomeTown { Name = "" }; }
            set { homeTown = value; }
        }

        public string DefaultPictureLink
        {
            get { return GetPictureUri(id); }
        }

        public override string ToString()
        {
            return name;
        }

        public static string GetPictureUri(string id)
        {
            return string.Format("http://graph.facebook.com/{0}/picture", id);
        }
    }
}
