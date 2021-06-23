using System.Windows;
using System.Windows.Media;

namespace Bewise.Phone
{
    public partial class RatingControl
    {
        public static readonly DependencyProperty StarSizeProperty =
            DependencyProperty.Register("StarSize", typeof(double), typeof(RatingControl), null);

        public double StarSize
        {
            get { return (double)GetValue(StarSizeProperty); }
            set
            {
                SetValue(StarSizeProperty, value);
                UpdateStarSizes();
            }
        }

        public static readonly DependencyProperty StarMarginProperty =
            DependencyProperty.Register("StarMargin", typeof(Thickness), typeof(RatingControl), new PropertyMetadata(new Thickness(0)));

        public Thickness StarMargin
        {
            get { return (Thickness)GetValue(StarMarginProperty); }
            set
            {
                SetValue(StarMarginProperty, value);
                UpdateStarMargins();
            }
        }

        public static readonly DependencyProperty ScoreProperty = DependencyProperty.Register("Score", typeof(int), typeof(RatingControl), new PropertyMetadata(OnScoreChanged));

        static void OnScoreChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RatingControl rating = (RatingControl)d;

            rating.Synchronize();
        }

        public int Score
        {
            get { return (int)GetValue(ScoreProperty); }
            set
            {
                SetValue(ScoreProperty, value);
            }
        }

        private bool _isInitialized = false;

        public RatingControl()
        {
            InitializeComponent();

            this._isInitialized = true;

            for (int i = 0; i < 5; i++)
            {
                RatingButton button = rootElement.Children[i] as RatingButton;

                button.Index = i;

                button.UpdateChecked += button_UpdateChecked;
                button.UpdateState += button_UpdateState;
            }
        }

        private void UpdateStarSizes()
        {
            if (this._isInitialized)
            {
                this.rootElement.Height = this.StarSize;

                this.star1.Width = this.StarSize;
                this.star2.Width = this.StarSize;
                this.star3.Width = this.StarSize;
                this.star4.Width = this.StarSize;
                this.star5.Width = this.StarSize;
            }
        }

        private void UpdateStarMargins()
        {
            if (this._isInitialized)
            {
                this.star1.Margin = this.StarMargin;
                this.star2.Margin = this.StarMargin;
                this.star3.Margin = this.StarMargin;
                this.star4.Margin = this.StarMargin;
                this.star5.Margin = this.StarMargin;
            }
        }

        private void Synchronize()
        {
            for (int i = 0; i < 5; i++)
            {
                RatingButton button = rootElement.Children[i] as RatingButton;

                button.IsChecked = (Score > i);
            }
        }

        void button_UpdateState(RatingButton b, string state)
        {
            int index = 0;
            while (index < rootElement.Children.Count)
            {
                RatingButton target = rootElement.Children[index] as RatingButton;
                if ((target == b))
                {
                    VisualStateManager.GoToState(target, state, true);
                    return;
                }
                VisualStateManager.GoToState(target, state, true);
                index++;
            }
        }
        void button_UpdateChecked(RatingButton b)
        {
            int buttonIndex = b.Index + 1;

            if (buttonIndex == Score)
                Score = buttonIndex - 1;
            else if (buttonIndex > Score)
                Score = buttonIndex;
            else
            {
                Score = buttonIndex;
            }

            int index = 0;
            while (index < rootElement.Children.Count)
            {
                RatingButton target = rootElement.Children[index] as RatingButton;
                target.IsChecked = Score > index;
                index++;
            }
        }
    }
}
