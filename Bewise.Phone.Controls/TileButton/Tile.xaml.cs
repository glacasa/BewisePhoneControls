using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Bewise.Phone
{
    public partial class Tile : UserControl
    {
        private static Random r = new Random();
        private DispatcherTimer dt;

        public String Text
        {
            get { return (String)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(String), typeof(Tile), new PropertyMetadata(OnPropertyChanged));

        public String Description
        {
            get { return (String)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(String), typeof(Tile), new PropertyMetadata(OnPropertyChanged));

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(Tile), new PropertyMetadata(OnPropertyChanged));

        public Int32 Delay
        {
            get { return (Int32)GetValue(DelayProperty); }
            set { SetValue(DelayProperty, value); }
        }

        public static readonly DependencyProperty DelayProperty =
            DependencyProperty.Register("Delay", typeof(Int32), typeof(Tile), new PropertyMetadata(2000));




        private bool animateFront, showBack;
        protected static void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ComputeAllowedAnimations(sender as Tile);
        }

        private static void ComputeAllowedAnimations(Tile t)
        {
            bool wereAnimated = t.animateFront || t.showBack;

            t.animateFront = !String.IsNullOrEmpty(t.Text) && t.ImageSource != null;
            t.showBack = !String.IsNullOrEmpty(t.Description);

            if (t.ImageSource == null && !String.IsNullOrEmpty(t.Text))
                (t.AnimatedFrontGrid.RenderTransform as TranslateTransform).Y = 0;
            else
                (t.AnimatedFrontGrid.RenderTransform as TranslateTransform).Y = -173;

            if (!wereAnimated && (t.animateFront || t.showBack))
            {
                t.dt.Interval = TimeSpan.FromMilliseconds(r.Next(30) * 100) + TimeSpan.FromMilliseconds(t.Delay);
                t.dt.Start();
            }
        }


        public Tile()
        {
            InitializeComponent();
            this.DataContext = this;
            TileAnim.Completed += new EventHandler(TileAnim_Completed);
            FlipAnim.Completed += new EventHandler(FlipAnim_Completed);
            dt = new DispatcherTimer();
            dt.Tick += new EventHandler(dt_Tick);
        }



        void dt_Tick(object sender, EventArgs e)
        {
            dt.Stop();
            if (animateFront)
                TileAnim.Begin();
            else
                FlipAnim.Begin();
        }

        void TileAnim_Completed(object sender, EventArgs e)
        {
            if (showBack)
            {
                FlipAnim.Begin();
            }
            else
            {
                dt.Interval = TimeSpan.FromMilliseconds(r.Next(30) * 100) + TimeSpan.FromMilliseconds(Delay);
                dt.Start();
            }
        }

        void FlipAnim_Completed(object sender, EventArgs e)
        {
            dt.Interval = TimeSpan.FromMilliseconds(r.Next(30) * 100) + TimeSpan.FromMilliseconds(Delay);
            if (!animateFront)
                dt.Interval += TimeSpan.FromSeconds(5);
            dt.Start();
        }




    }
}
