using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Runtime.Serialization;

namespace Bewise.Phone
{
    [DataContract]
    public class FacebookNewsfeed
    {
        private FacebookRawObject[] newsfeed;
        [DataMember(Name = "data")]
        public FacebookRawObject[] Newsfeed
        {
            get { return newsfeed; }
            set { newsfeed = value; }
        }
    }

    

 
   


}
