
using System.ComponentModel;
namespace Bewise.Phone
{
    public class JumpListLetter : INotifyPropertyChanged
    {
        bool hasItems;

        public string Text
        {
            get;
            set;
        }

        public bool HasItems
        {
            get { return hasItems; }
            set
            {
                if (hasItems != value)
                {
                    hasItems = value;
                    RaisePropertyChanged("HasItems");
                }
            }
        }

        public int Count
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
