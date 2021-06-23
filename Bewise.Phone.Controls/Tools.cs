using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Shell;

namespace Bewise.Phone
{
    public class Tools
    {
        public static void AddNewAnimation(Storyboard sb, DependencyObject source, string property, double from, double to, double transitionSpeed, IEasingFunction easingFunction = null)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = from;
            animation.To = to;
            animation.Duration = TimeSpan.FromSeconds(transitionSpeed);
            if (easingFunction != null)
                animation.EasingFunction = easingFunction;

            Storyboard.SetTarget(animation, source);
            Storyboard.SetTargetProperty(animation, new PropertyPath(property));
            sb.Children.Add(animation);
        }

        public static SplineDoubleKeyFrame CreateSplineDoubleKeyFrame(double keyTime, double value)
        {
            SplineDoubleKeyFrame keyFrame = new SplineDoubleKeyFrame();
            keyFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(keyTime));
            keyFrame.Value = value;
            return keyFrame;
        }

        public static void PreLaunchStoryboard(FrameworkElement root, Storyboard sb, double delay, string property, double from, double to)
        {
            Storyboard preSB = new Storyboard();
            AddNewAnimation(preSB, root, property, from, to, delay);

            preSB.Completed += (s, e) => sb.Begin();
            preSB.Begin();
        }

        public static void SetEnable(IApplicationBar bar, int buttonIndex, bool value)
        {
            (bar.Buttons[buttonIndex] as ApplicationBarIconButton).IsEnabled = value;
        }

        public static void SetText(IApplicationBar bar, int buttonIndex, string text)
        {
            (bar.Buttons[buttonIndex] as ApplicationBarIconButton).Text = text;
        }

        public static void SetMenuText(IApplicationBar bar, int buttonIndex, string text)
        {
            (bar.MenuItems[buttonIndex] as ApplicationBarMenuItem).Text = text;
        }

        public static void AddEaseNewAnimation(Storyboard sb, DependencyObject source, string property, double from, double to, double transitionSpeed)
        {
            DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame key1 = new EasingDoubleKeyFrame();
            EasingDoubleKeyFrame key2 = new EasingDoubleKeyFrame();
            key1.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0));
            key1.Value = from;
            key2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(transitionSpeed));
            CubicEase ease = new CubicEase();
            ease.EasingMode = EasingMode.EaseOut;
            key2.EasingFunction = ease;
            key2.Value = to;

            animation.KeyFrames.Add(key1);
            animation.KeyFrames.Add(key2);

            Storyboard.SetTarget(animation, source);
            Storyboard.SetTargetProperty(animation, new PropertyPath(property));
            sb.Children.Add(animation);
        }

        public static void AddBackEaseNewAnimation(Storyboard sb, DependencyObject source, string property, double from, double to, double transitionSpeed, double amplitude)
        {
            DoubleAnimation animation = new DoubleAnimation();
            BackEase ease = new BackEase();
            ease.Amplitude = amplitude;
            ease.EasingMode = EasingMode.EaseInOut;

            animation.EasingFunction = ease;
            animation.From = from;
            animation.To = to;
            animation.Duration = TimeSpan.FromSeconds(transitionSpeed);

            Storyboard.SetTarget(animation, source);
            Storyboard.SetTargetProperty(animation, new PropertyPath(property));
            sb.Children.Add(animation);
        }

        public static string CurrentThemeBackground()
        {
            var currentColor = (Color)Application.Current.Resources["PhoneBackgroundColor"];
            if (currentColor == Colors.Black)
                return "Dark";
            return "Light";
        }

        public static ImageSource GetResourceImage(string assembly, string name)
        {
            BitmapImage img = new BitmapImage();
            img.UriSource = new Uri(string.Format("/{0};component/Images/{1}/{2}", assembly, CurrentThemeBackground(), name), UriKind.Relative);
            return img;
        }

        public static string GetContentImage(string root, string name)
        {
            return string.Format("/{0}/{1}/{2}", root, CurrentThemeBackground(), name);
        }

        public ImageSource Right
        {
            get
            {
                return GetResourceImage("Bewise.Phone", "appbar.transport.play.rest.png");
            }
        }

        public ImageSource Left
        {
            get
            {
                return GetResourceImage("Bewise.Phone", "appbar.transport.back.rest.png");
            }
        }


        public static ImageSource Circle
        {
            get
            {
                return GetResourceImage("Bewise.Phone", "appbar.basecircle.rest.png");
            }
        }

        public ImageSource CircleImage
        {
            get
            {
                return GetResourceImage("Bewise.Phone", "appbar.basecircle.rest.png");
            }
        }

        public ImageSource AddImage
        {
            get
            {
                return GetResourceImage("Bewise.Phone", "appbar.add.rest.png");
            }
        }

        public ImageSource EditImage
        {
            get
            {
                return GetResourceImage("Bewise.Phone", "appbar.edit.rest.png");
            }
        }
    }
}
