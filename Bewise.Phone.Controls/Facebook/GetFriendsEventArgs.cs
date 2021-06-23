using System;

namespace Bewise.Phone
{
    public class GetFriendsEventArgs : EventArgs
    {
        public FacebookUser[] Friends
        {
            get; set;
        }
    }
}
