using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Bewise.Phone
{
    public class VolumeGrid : Grid
    {
        public event EventHandler SelectedIndexChanged;

        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(VolumeGrid), new PropertyMetadata(0, OnSelectedIndexChanged));

        public static readonly DependencyProperty AnimationSpeedProperty =
            DependencyProperty.Register("AnimationSpeed", typeof(double), typeof(VolumeGrid), new PropertyMetadata(300.0));

        public static readonly DependencyProperty BouncesProperty =
            DependencyProperty.Register("Bounces", typeof(int), typeof(VolumeGrid), new PropertyMetadata(10));

        public static readonly DependencyProperty BouncinessProperty =
            DependencyProperty.Register("Bounciness", typeof(double), typeof(VolumeGrid), new PropertyMetadata(4.0));

        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.Register("IsEnabled", typeof(bool), typeof(VolumeGrid), new PropertyMetadata(true));


        double angleOffset;
        int currentWheelIndex;
        bool canMove;
        double accumulation;
        double translation;
        int indexOffset;


        public bool IsEnabled
        {
            get
            {
                return (bool)GetValue(IsEnabledProperty);
            }
            set
            {
                SetValue(IsEnabledProperty, value);
            }
        }


        public int Bounces
        {
            get { return (int)GetValue(BouncesProperty); }
            set
            {
                SetValue(BouncesProperty, value);
            }
        }

        public double Bounciness
        {
            get { return (double)GetValue(BouncinessProperty); }
            set
            {
                SetValue(BouncinessProperty, value);
            }
        }

        public double AnimationSpeed
        {
            get { return (double)GetValue(AnimationSpeedProperty); }
            set
            {
                SetValue(AnimationSpeedProperty, value);
            }
        }

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set
            {
                indexOffset = value - SelectedIndex;
                currentWheelIndex += indexOffset;
                if (value < 0)
                    value = Children.Count - 1;

                if (value >= Children.Count)
                    value = 0;

                SetValue(SelectedIndexProperty, value);
            }
        }

        protected static void OnSelectedIndexChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            VolumeGrid volumePanel = sender as VolumeGrid;

            volumePanel.ChangeSelectedIndex();

            if (volumePanel.SelectedIndexChanged != null)
                volumePanel.SelectedIndexChanged(volumePanel, EventArgs.Empty);
        }

        private void ChangeSelectedIndex()
        {
            if (Children.Count < 2)
                return;

            double newAngle = currentWheelIndex * angleOffset;

            for (int childIndex = 0; childIndex < Children.Count; childIndex++)
            {
                UIElement child = Children[childIndex];
                Storyboard sb = new Storyboard();

                DoubleAnimation animation = new DoubleAnimation();
                animation.To = newAngle - childIndex * angleOffset;
                animation.Duration = TimeSpan.FromMilliseconds(AnimationSpeed * Math.Abs(indexOffset));
                BounceEase ease = new BounceEase();
                ease.Bounces = Bounces;
                ease.EasingMode = EasingMode.EaseOut;
                ease.Bounciness = Bounciness;
                animation.EasingFunction = ease;
                Storyboard.SetTarget(animation, child.Projection);
                Storyboard.SetTargetProperty(animation, new PropertyPath("RotationY"));

                sb.Children.Add(animation);
                sb.Begin();
            }
        }

        public VolumeGrid()
        {
            Loaded += VolumeGrid_Loaded;

            ManipulationStarted += VolumeGrid_ManipulationStarted;
            ManipulationDelta += VolumeGrid_ManipulationDelta;
            ManipulationCompleted += VolumeGrid_ManipulationCompleted;
        }

        void VolumeGrid_Loaded(object sender, RoutedEventArgs e)
        {
            this.Initialize();
        }
        public void Initialize()
        {
            PrepareLayout();
            SelectedIndex = 0;
            currentWheelIndex = 0;
        }
        void VolumeGrid_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            if (!canMove || !this.IsEnabled)
                return;

            if (translation > 75)
                SelectedIndex--;
            else if (translation < -75)
                SelectedIndex++;
            else
            {
                ChangeSelectedIndex();
            }
        }

        void VolumeGrid_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            accumulation += Math.Abs(e.DeltaManipulation.Translation.X);
            translation += e.DeltaManipulation.Translation.X;

            if (accumulation > 25)
            {
                canMove = true;
            }

            if (!canMove || !this.IsEnabled)
                return;

            foreach (Panel panel in Children)
            {
                (panel.Projection as PlaneProjection).RotationY -= e.DeltaManipulation.Translation.X / 10.0;
            }
        }

        void VolumeGrid_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            canMove = false;
            accumulation = 0;
            translation = 0;
        }

        private void PrepareLayout()
        {
            angleOffset = 360.0 / Children.Count;
            double radiansAngle = Math.PI / Children.Count;
            double angle = 0;

            double radius = ((ActualWidth / 2) / Math.Tan(radiansAngle));

            if (Children.Count == 2)
                radius += 1.0;

            foreach (Panel panel in Children)
            {
                PlaneProjection projection = new PlaneProjection();
                projection.CenterOfRotationZ = -radius;
                projection.RotationY = angle;
                panel.Projection = projection;

                angle -= angleOffset;
            }
        }
    }
}
