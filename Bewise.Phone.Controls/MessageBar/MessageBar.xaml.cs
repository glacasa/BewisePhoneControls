using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Bewise.Phone
{
    public partial class MessageBar
    {
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(MessageBar),
                                                                                                     new PropertyMetadata("", OnMessageChanged));

        static void OnMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string val = (string) e.NewValue;
            MessageBar messageBox = (MessageBar) d;

            messageBox.messageText.Text = val;
            messageBox.messageTextError.Text = val;
        }

        public string Message
        {
            get
            {
                return (string)GetValue(MessageProperty);
            }
            set
            {
                SetValue(MessageProperty, value);
            }
        }

        public MessageBar()
        {
            InitializeComponent();
        }

        public bool IsVisible
        {
            get
            {
                return messageBox.Opacity == 1;
            }
        }


        public bool IsError
        {
            set
            {
                if (value)
                {
                    messageTextError.Visibility = Visibility.Visible;
                    messageText.Visibility = Visibility.Collapsed;
                }
                else
                {
                    messageTextError.Visibility = Visibility.Collapsed;
                    messageText.Visibility = Visibility.Visible;
                }
            }
        }

        public void Show()
        {
            messageBox.Visibility = Visibility.Visible;
            ((Storyboard)messageBox.Resources["moveUp"]).Begin();
        }

        public void Hide(bool direct)
        {
            if (direct)
            {
                messageBox.Opacity = 0;
                boxTranslate.Y = 60;
                return;
            }
            Storyboard sb = ((Storyboard)messageBox.Resources["moveDown"]);

            sb.Completed += sb_Completed;
            sb.Begin();
        }

        void sb_Completed(object sender, EventArgs e)
        {
            messageBox.Visibility = Visibility.Collapsed;
        }

        private void messageBox_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Hide(false);
        }
    }
}
