
using System;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Bewise.Phone
{
    public partial class InappToast
    {

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message",
                                                                                                typeof (string),
                                                                                                typeof (InappToast),
                                                                                                new PropertyMetadata(
                                                                                                    "Message",
                                                                                                    OnMessageChanged));

        private readonly Storyboard showStoryboard;
        private readonly Storyboard hideStoryboard;


        private static void OnMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((InappToast) d).messageText.Text = (string) e.NewValue;
        }


        public string Message
        {
            set { SetValue(MessageProperty, value); }
            get { return (string) GetValue(MessageProperty); }
        }

        public event EventHandler OnClicked;

        public void InvokeOnClicked(EventArgs e)
        {
            EventHandler handler = OnClicked;
            if (handler != null) handler(this, e);
        }

        private bool _isVisible = false;
        private Timer _timer;

        public InappToast()
        {
            InitializeComponent();

            showStoryboard = (Storyboard) rootGrid.Resources["ShowStoryboard"];
            hideStoryboard = (Storyboard) rootGrid.Resources["HideStoryboard"];
            hideStoryboard.Completed += hideStoryboard_Completed;
            
        }

        private void hideStoryboard_Completed(object sender, EventArgs e)
        {
            (rootGrid.Projection as PlaneProjection).RotationX = 90;
        }

        public void Show(string pMessage)
        {
            Message = pMessage;
            Show();
        }

        public void Show()
        {
            showStoryboard.Begin();
            _isVisible = true;

            if (_timer != null)
            {
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
                _timer.Dispose();
                _timer = null;
            }
            _timer = new Timer(this.Hide, null, new TimeSpan(0,0,3), new TimeSpan(1,0,0));
        }

        /// <summary>
        /// Stub method for Timer use
        /// </summary>
        /// <param name="o"></param>
        private void Hide(object o)
        {
            Deployment.Current.Dispatcher.BeginInvoke(this.Hide);
        }

        public void Hide()
        {
            if (_isVisible)
            {
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
                _timer.Dispose();
                _timer = null;
                _isVisible = false;
                hideStoryboard.Begin();
            }
        }

        private void messageText_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            InvokeOnClicked(new EventArgs());
            this.Hide();
        }
    }
}
