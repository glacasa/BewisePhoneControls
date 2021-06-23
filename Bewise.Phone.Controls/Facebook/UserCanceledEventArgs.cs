using System;

namespace Bewise.Phone
{
    public class UserCanceledEventArgs : EventArgs
    {
        public bool UserCanceled { get; set; }
    }
}
