using System;
using System.Net;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Bewise.Phone
{
    [DataContract]
    public class FacebookRawObject
    {
        private string id;
        [DataMember(Name = "id")]
        public string Id
        {
            get { return id ?? ""; }
            set { id = value; }
        }

        [DataMember(Name = "type")]
        public String Type { get; set; }

        [DataMember(Name = "from")]
        public FacebookUser From { get; set; }

        [DataMember(Name = "message")]
        public String Message { get; set; }

        [DataMember(Name="picture")]
        public String Picture { get; set; }

        [DataMember(Name = "link")]
        public String Link { get; set; }

        [DataMember(Name = "name")]
        public String Name { get; set; }

        [DataMember(Name = "caption")]
        public String Caption { get; set; }

        [DataMember(Name = "description")]
        public String Description { get; set; }
    }
}
