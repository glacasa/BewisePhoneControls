using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;
using System.Windows.Shapes;

namespace Bewise.Phone
{
    public delegate void BeforeFadeHandler(Storyboard sb);

    public class TransitioningPage : PhoneApplicationPage
    {
        public event EventHandler ReadyForAnimations;
        public event EventHandler BeforeGoBack;
        public event BeforeFadeHandler OnBeforeFadeOut;
        const double fadeInSpeed = 0.2;
        const double fadeOutSpeed = 0.2;
        readonly PlaneProjection transform;
        bool blockBackKeyBehavior;
        bool blockStartAnimation;
        Grid layoutRoot;
        bool forceGoBack;
        static bool animate = true;
        static double fadeOutIn;
        static double fadeOutOut = 90;
        static double fadeInIn = -90;
        static double fadeInOut;
        static TransitioningPage currentPage;

        Image beforeRotationImage;
        Rectangle rectangle;
        private PageOrientation previousOrientation;



        public bool AnimateRotation
        {
            get { return (bool)GetValue(AnimateRotationProperty); }
            set { SetValue(AnimateRotationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimateRotation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimateRotationProperty =
            DependencyProperty.Register("AnimateRotation", typeof(bool), typeof(TransitioningPage), new PropertyMetadata(true));



        public static int CacheIndex { get; set; }

        int localCacheIndex;

        public bool BlockGoBack
        {
            get;
            set;
        }

        public TransitioningPage()
        {
            BackKeyPress += TransitioningPage_BackKeyPress;

            Loaded += TransitioningPage_Loaded;
            transform = new PlaneProjection { CenterOfRotationX = 0, CenterOfRotationY = 0 };

            TiltEffect.SetIsTiltEnabled(this, true);
        }

        protected override void OnOrientationChanged(OrientationChangedEventArgs e)
        {
            if (!AnimateRotation)
            {
                base.OnOrientationChanged(e);
                return;
            }

            WriteableBitmap beforeBitmap = new WriteableBitmap(layoutRoot, null);
            beforeRotationImage.Source = beforeBitmap;

            rectangle.Visibility = Visibility.Visible;
            beforeRotationImage.Visibility = Visibility.Visible;

            base.OnOrientationChanged(e);

            double rotationFrom;
            if ((previousOrientation == PageOrientation.LandscapeLeft && e.Orientation == PageOrientation.PortraitUp) ||
               (previousOrientation == PageOrientation.PortraitUp && e.Orientation == PageOrientation.LandscapeRight) ||
               (previousOrientation == PageOrientation.LandscapeLeft && e.Orientation == PageOrientation.LandscapeRight))
            {
                rotationFrom = -90;
            }
            else
            {
                rotationFrom = 90;
            }
            previousOrientation = Orientation;


            Storyboard sb = new Storyboard();
            sb.Completed += new EventHandler(sbRorate_Completed);
            transform.CenterOfRotationX = 0.5;
            transform.CenterOfRotationY = 0.5;

            ExponentialEase easing = new ExponentialEase();
            easing.EasingMode = EasingMode.EaseInOut;
            easing.Exponent = 6;

            double rotationSpeed = 0.5;

            Tools.AddNewAnimation(sb, transform, "RotationZ", rotationFrom, 0, rotationSpeed, easing);

            DoubleAnimationUsingKeyFrames anim = new DoubleAnimationUsingKeyFrames();
            anim.KeyFrames.Add(Tools.CreateSplineDoubleKeyFrame(0, 1));
            anim.KeyFrames.Add(Tools.CreateSplineDoubleKeyFrame(rotationSpeed / 3, 1));
            anim.KeyFrames.Add(Tools.CreateSplineDoubleKeyFrame(2 * rotationSpeed / 3, 0));

            Storyboard.SetTarget(anim, beforeRotationImage);
            Storyboard.SetTargetProperty(anim, new PropertyPath("Opacity"));
            sb.Children.Add(anim);

            DoubleAnimationUsingKeyFrames anim2 = new DoubleAnimationUsingKeyFrames();
            anim2.KeyFrames.Add(Tools.CreateSplineDoubleKeyFrame(0, 1));
            anim2.KeyFrames.Add(Tools.CreateSplineDoubleKeyFrame(rotationSpeed / 3, 1));
            anim2.KeyFrames.Add(Tools.CreateSplineDoubleKeyFrame(2 * rotationSpeed / 3, 0));

            Storyboard.SetTarget(anim2, rectangle);
            Storyboard.SetTargetProperty(anim2, new PropertyPath("Opacity"));
            sb.Children.Add(anim2);

            transform.RotationZ = rotationFrom;

            sb.Begin();

        }

        void sbRorate_Completed(object sender, EventArgs e)
        {
            rectangle.Visibility = Visibility.Collapsed;
            beforeRotationImage.Visibility = Visibility.Collapsed;
            beforeRotationImage.Opacity = 0;
        }

        protected void AfterInitialize()
        {
            layoutRoot = Content as Grid;

            layoutRoot.Projection = transform;

            if (!blockStartAnimation)
                layoutRoot.Opacity = 0;

            // Placement des images de rotation (qui doit prendre tout l'écran)
            previousOrientation = this.Orientation;

            rectangle = new Rectangle();
            rectangle.Fill = Resources["PhoneBackgroundBrush"] as Brush;
            rectangle.Visibility = Visibility.Collapsed;
            if (layoutRoot.ColumnDefinitions.Count > 0)
                Grid.SetColumnSpan(rectangle, layoutRoot.ColumnDefinitions.Count);
            if (layoutRoot.RowDefinitions.Count > 0)
                Grid.SetRowSpan(rectangle, layoutRoot.RowDefinitions.Count);
            layoutRoot.Children.Add(rectangle);

            beforeRotationImage = new Image();
            beforeRotationImage.Opacity = 0;
            if (layoutRoot.ColumnDefinitions.Count > 0)
                Grid.SetColumnSpan(beforeRotationImage, layoutRoot.ColumnDefinitions.Count);
            if (layoutRoot.RowDefinitions.Count > 0)
                Grid.SetRowSpan(beforeRotationImage, layoutRoot.RowDefinitions.Count);
            layoutRoot.Children.Add(beforeRotationImage);

            beforeRotationImage.Stretch = Stretch.None;
            beforeRotationImage.VerticalAlignment = VerticalAlignment.Center;
            beforeRotationImage.HorizontalAlignment = HorizontalAlignment.Center;
            beforeRotationImage.Visibility = Visibility.Collapsed;

        }

        void TransitioningPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            currentPage = this;
            if (!animate || forceGoBack || blockStartAnimation)
                return;
            FadeIn();
        }

        public bool BlockBackKey
        {
            get { return blockBackKeyBehavior; }
            set { blockBackKeyBehavior = value; }
        }

        public static TransitioningPage CurrentPage
        {
            get { return currentPage; }
        }

        public bool BlockStartAnimation
        {
            get { return blockStartAnimation; }
            set { blockStartAnimation = value; }
        }

        void TransitioningPage_BackKeyPress(object sender, CancelEventArgs e)
        {
            if (blockBackKeyBehavior)
                return;
            GoBack();
            e.Cancel = true;
        }

        protected void FadeIn()
        {
            if (DesignerProperties.IsInDesignTool)
                return;

            transform.CenterOfRotationX = 0;
            transform.CenterOfRotationY = 0;

            Dispatcher.BeginInvoke(() =>
                                       {
                                           Storyboard sb = new Storyboard();
                                           Tools.AddNewAnimation(sb, layoutRoot, "Opacity", 0.1, 1.0, fadeInSpeed);
                                           Tools.AddNewAnimation(sb, transform, "RotationY", fadeInIn, fadeInOut, fadeInSpeed);
                                           transform.RotationY = fadeInIn;

                                           sb.Completed += sb_Completed;

                                           Tools.PreLaunchStoryboard(layoutRoot, sb, 0.05, "Opacity", 0, 0.1);
                                           animate = false;
                                       });
        }

        void sb_Completed(object sender, EventArgs e)
        {
            OnLoad();
            if (ReadyForAnimations != null)
                ReadyForAnimations(this, EventArgs.Empty);
        }

        protected void FadeOut(EventHandler handler)
        {
            transform.CenterOfRotationX = 0;
            transform.CenterOfRotationY = 0;

            Dispatcher.BeginInvoke(() =>
                                       {
                                           Storyboard sb = new Storyboard();
                                           Tools.AddNewAnimation(sb, layoutRoot, "Opacity", 1.0, 0, fadeOutSpeed);
                                           Tools.AddNewAnimation(sb, transform, "RotationY", fadeOutIn, fadeOutOut, fadeOutSpeed);


                                           if (OnBeforeFadeOut != null)
                                               OnBeforeFadeOut(sb);

                                           sb.Completed += handler;
                                           sb.Begin();
                                       });
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            OnBeforeLoad();

            if (localCacheIndex != CacheIndex)
            {
                localCacheIndex = CacheIndex;
                RefreshCache();
            }
        }

        public void ForceGoBack()
        {
            animate = true;
            forceGoBack = true;
            NavigationService.GoBack();
        }

        public void GoBack()
        {
            if (BlockGoBack)
                return;

            Dispatcher.BeginInvoke(() =>
                                       {
                                           animate = true;

                                           SetBackAnimationMode();

                                           FadeOut((s, e) =>
                                                       {
                                                           if (BeforeGoBack != null)
                                                               BeforeGoBack(this, EventArgs.Empty);
                                                           NavigationService.GoBack();
                                                       });
                                       });
        }

        protected void SetBackAnimationMode()
        {
            fadeOutIn = 0;
            fadeOutOut = -90;
            fadeInIn = 90;
            fadeInOut = 0;
        }

        public void Navigate(Uri uri)
        {
            string uri0 = NavigationService.CurrentSource.ToString();
            string uri1 = uri.ToString();
            if (uri0 == uri1)
                return;

            animate = true;

            SetInAnimationMode();

            FadeOut((s, e) => NavigationService.Navigate(uri));
        }

        protected void SetInAnimationMode()
        {
            fadeOutIn = 0;
            fadeOutOut = 90;
            fadeInIn = -90;
            fadeInOut = 0;
        }

        public virtual void OnLoad()
        {

        }

        public virtual void OnBeforeLoad()
        {

        }

        public virtual void RefreshCache()
        {

        }
    }
}
