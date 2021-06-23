
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System;
namespace Bewise.Phone
{
    public class JumpListItem : INotifyPropertyChanged
    {
        object dataContext;
        string text;
        bool hideRowHeaderBackgroundIfPicturePresent = true;

        PropertyInfo propertyInfo;

        public double Height
        {
            get;
            set;
        }

        public double HeaderWidth
        {
            get;
            set;
        }

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (text != value)
                {
                    text = value;

                    RaisePropertyChanged("Text");
                }
            }
        }

        public bool ShowRowHeader
        {
            get;
            set;
        }

        public bool IsGroup
        {
            get;
            set;
        }

        public Visibility IsGroupVisibility
        {
            get
            {
                return IsGroup ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility IsNotGroupVisibility
        {
            get
            {
                return IsGroup ? Visibility.Collapsed : Visibility.Visible;
            }
        }


        public Visibility IsNotGroupAndShowGroupVisibility
        {
            get
            {
                return (!IsGroup && ShowRowHeader) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        internal string ImagePropertyName
        {
            get;
            set;
        }

        public object DataContext
        {
            get
            {
                return dataContext;
            }
            set
            {
                dataContext = value;
                Text = dataContext.ToString();

                SynchroniseImage();

                INotifyPropertyChanged notifyPropertyChanged = value as INotifyPropertyChanged;

                if (notifyPropertyChanged != null)
                    notifyPropertyChanged.PropertyChanged += notifyPropertyChanged_PropertyChanged;
            }
        }

        void SynchroniseImage()
        {
            if (propertyInfo == null && !string.IsNullOrEmpty(ImagePropertyName))
            {
                propertyInfo = dataContext.GetType().GetProperty(ImagePropertyName, BindingFlags.Instance | BindingFlags.Public);
            }
            if (propertyInfo != null)
            {
                object value = propertyInfo.GetValue(dataContext, null);

                if (value == null)
                    Image = null;
                else
                    Image = value.ToString();
            }
        }

        void notifyPropertyChanged_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ImagePropertyName)
            {
                SynchroniseImage();
                RaisePropertyChanged("Image");
                RaisePropertyChanged("HasImageVisibility");
            }
            else
            {
                string newText = dataContext.ToString();
                if (newText != Text)
                {
                    Text = newText;
                }
            }
        }

        public string Image
        {
            get;
            set;
        }

        public Visibility HasImageVisibility
        {
            get
            {
                return (!HideRowHeaderBackgroundIfPicturePresent || string.IsNullOrEmpty(Image)) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public bool HideRowHeaderBackgroundIfPicturePresent
        {
            get { return hideRowHeaderBackgroundIfPicturePresent; }
            set { hideRowHeaderBackgroundIfPicturePresent = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
