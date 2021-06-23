using System.Threading;
using System.Windows;

namespace Bewise.Phone
{
    public enum FlagPositions
    {
        Top,
        Bottom
    }

    public partial class Flag
    {
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(Flag),
                                                                                             new PropertyMetadata("", OnMessageChanged));
        public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register("Offset", typeof(double), typeof(Flag),
                                                                                             new PropertyMetadata(0.0, OnOffsetChanged));
        public static readonly DependencyProperty FlagPositionProperty = DependencyProperty.Register("FlagPosition", typeof(FlagPositions), typeof(Flag),
                                                                                             new PropertyMetadata(FlagPositions.Bottom, OnFlagPositionChanged));

        static void OnMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string val = (string)e.NewValue;
            Flag flag = (Flag)d;

            flag.messageText.Text = val;
        }

        static void OnFlagPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FlagPositions val = (FlagPositions)e.NewValue;
            Flag flag = (Flag)d;

            switch (val)
            {
                case FlagPositions.Top:
                    flag.offsetDownGrid.Visibility = Visibility.Collapsed;
                    flag.offsetUpGrid.Visibility = Visibility.Visible;
                    break;
                case FlagPositions.Bottom:
                    flag.offsetDownGrid.Visibility = Visibility.Visible;
                    flag.offsetUpGrid.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        static void OnOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            double val = (double)e.NewValue;
            Flag flag = (Flag)d;

            flag.offsetDownTransform.X = val;
            flag.offsetUpTransform.X = val;
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

        public FlagPositions FlagPosition
        {
            get
            {
                return (FlagPositions)GetValue(FlagPositionProperty);
            }
            set
            {
                SetValue(FlagPositionProperty, value);
            }
        }


        public double Offset
        {
            get
            {
                return (double)GetValue(OffsetProperty);
            }
            set
            {
                SetValue(OffsetProperty, value);
            }
        }

        public Flag()
        {
            InitializeComponent();

            hide.Completed += hide_Completed;
        }

        private void LayoutRoot_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Hide();
        }

        public void Hide()
        {
            hide.Begin();
        }

        void hide_Completed(object sender, System.EventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        public void StartHideTimer(int milliseconds)
        {
            new Timer(o => Dispatcher.BeginInvoke(Hide), 0, milliseconds, Timeout.Infinite);
        }
    }
}
