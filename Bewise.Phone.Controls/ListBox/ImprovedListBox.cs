using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Bewise.Phone
{
    public class ImprovedListBox : ListBox
    {
 

        public ImprovedListBox()
        {
            this.Loaded += new RoutedEventHandler(ContinuumListBox_Loaded);
        }

        #region Continuum

        public event EventHandler OnContinuumCompleted;
        public event EventHandler OnRestoreContinuumCompleted;

        ListBoxItem restoreListBoxItem;
        Grid lastGrid;
        Image animatedSelectionImage;
        readonly TranslateTransform translateTransform = new TranslateTransform();
        readonly Storyboard sbRestoreContinuum = new Storyboard();
        readonly Storyboard sbContinuum = new Storyboard();

        void ContinuumListBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (animatedSelectionImage != null && animatedSelectionImage.Parent != null)
                return;

            FrameworkElement root = Parent as FrameworkElement;
            lastGrid = null;

            while (VisualTreeHelper.GetParent(root) != null)
            {
                Grid tempGrid = root as Grid;

                if (tempGrid != null)
                    lastGrid = tempGrid;

                if (VisualTreeHelper.GetParent(root) is PhoneApplicationPage)
                {
                    break;
                }

                root = VisualTreeHelper.GetParent(root) as FrameworkElement;
            }


            animatedSelectionImage = new Image
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Stretch = Stretch.None
            };
            if (lastGrid.RowDefinitions.Count > 0)
                Grid.SetRowSpan(animatedSelectionImage, lastGrid.RowDefinitions.Count);

            if (lastGrid.ColumnDefinitions.Count > 0)
                Grid.SetColumnSpan(animatedSelectionImage, lastGrid.ColumnDefinitions.Count);

            lastGrid.Children.Add(animatedSelectionImage);
            animatedSelectionImage.RenderTransform = translateTransform;

            // Continuum
            DoubleAnimation animation = new DoubleAnimation { Duration = TimeSpan.FromMilliseconds(350) };

            Storyboard.SetTarget(animation, translateTransform);
            Storyboard.SetTargetProperty(animation, new PropertyPath("X"));
            ExponentialEase ease = new ExponentialEase { EasingMode = EasingMode.EaseIn, Exponent = 6 };
            animation.EasingFunction = ease;

            sbContinuum.Children.Add(animation);

            animation = new DoubleAnimation { Duration = TimeSpan.FromMilliseconds(350) };

            Storyboard.SetTarget(animation, translateTransform);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Y"));
            ease = new ExponentialEase { EasingMode = EasingMode.EaseIn, Exponent = 6 };
            animation.EasingFunction = ease;

            sbContinuum.Children.Add(animation);

            animation = new DoubleAnimation { From = 1, To = 0, Duration = TimeSpan.FromMilliseconds(350) };

            Storyboard.SetTarget(animation, animatedSelectionImage);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
            ease = new ExponentialEase { EasingMode = EasingMode.EaseIn, Exponent = 6 };
            animation.EasingFunction = ease;

            sbContinuum.Children.Add(animation);

            sbContinuum.Completed += sbContinuum_Completed;


            // Restore Continuum
            animation = new DoubleAnimation { Duration = TimeSpan.FromMilliseconds(350) };

            Storyboard.SetTarget(animation, translateTransform);
            Storyboard.SetTargetProperty(animation, new PropertyPath("X"));
            ease = new ExponentialEase { EasingMode = EasingMode.EaseOut, Exponent = 6 };
            animation.EasingFunction = ease;

            sbRestoreContinuum.Children.Add(animation);

            animation = new DoubleAnimation { Duration = TimeSpan.FromMilliseconds(350) };

            Storyboard.SetTarget(animation, translateTransform);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Y"));
            ease = new ExponentialEase { EasingMode = EasingMode.EaseOut, Exponent = 6 };
            animation.EasingFunction = ease;

            sbRestoreContinuum.Children.Add(animation);

            animation = new DoubleAnimation { From = 0, To = 1, Duration = TimeSpan.FromMilliseconds(350) };

            Storyboard.SetTarget(animation, animatedSelectionImage);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
            ease = new ExponentialEase { EasingMode = EasingMode.EaseOut, Exponent = 6 };
            animation.EasingFunction = ease;

            sbRestoreContinuum.Children.Add(animation);

            sbRestoreContinuum.Completed += sbRestoreContinuum_Completed;
        }


        public void DoContinuumAnimationOnSelectedItem()
        {
            if (this.SelectedItem == null)
                return;

            if (restoreListBoxItem != null)
                return;

            restoreListBoxItem = this.ItemContainerGenerator.ContainerFromItem(this.SelectedItem) as ListBoxItem;
            GeneralTransform generalTransform = restoreListBoxItem.TransformToVisual(lastGrid);

            Point location = generalTransform.Transform(new Point(0, 0));

            WriteableBitmap bitmap = new WriteableBitmap(restoreListBoxItem, null);

            animatedSelectionImage.Source = bitmap;

            translateTransform.X = location.X;
            translateTransform.Y = location.Y;

            animatedSelectionImage.Visibility = Visibility.Visible;

            restoreListBoxItem.Opacity = 0;

            (sbContinuum.Children[0] as DoubleAnimation).To = location.X + 225;
            (sbContinuum.Children[1] as DoubleAnimation).To = location.Y + 73;

            sbContinuum.Begin();
        }

        public void RestoreContinuum(bool animate = true)
        {
            if (restoreListBoxItem == null)
                return;

            if (!animate)
            {
                animatedSelectionImage.Visibility = Visibility.Collapsed;
                restoreListBoxItem.Opacity = 1.0;
                restoreListBoxItem = null;
                return;
            }

            GeneralTransform generalTransform = restoreListBoxItem.TransformToVisual(lastGrid);

            Point location = generalTransform.Transform(new Point(0, 0));

            WriteableBitmap bitmap = new WriteableBitmap(restoreListBoxItem, null);

            animatedSelectionImage.Source = bitmap;

            translateTransform.X = location.X - 225;
            translateTransform.Y = location.Y - 73;

            animatedSelectionImage.Visibility = Visibility.Visible;

            (sbRestoreContinuum.Children[0] as DoubleAnimation).To = location.X;
            (sbRestoreContinuum.Children[1] as DoubleAnimation).To = location.Y;

            sbRestoreContinuum.Begin();
        }

        void sbContinuum_Completed(object sender, EventArgs e)
        {
            animatedSelectionImage.Visibility = Visibility.Collapsed;

            if (OnContinuumCompleted != null)
                OnContinuumCompleted(this, EventArgs.Empty);
        }

        void sbRestoreContinuum_Completed(object sender, EventArgs e)
        {
            animatedSelectionImage.Visibility = Visibility.Collapsed;
            restoreListBoxItem.Opacity = 1.0;
            restoreListBoxItem = null;

            if (OnRestoreContinuumCompleted != null)
                OnRestoreContinuumCompleted(this, EventArgs.Empty);
        }

        #endregion




    }
}
