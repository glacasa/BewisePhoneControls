using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Resources;
using System.Windows.Shapes;
using Bewise.Phone;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace Bewise.Phone
{
    public partial class HtmlDisplayer : Grid
    {
        private readonly string _template;

        public event EventHandler<NavigatingEventArgs> Navigating;

        public bool ScrollDisabled
        {
            get { return (bool)GetValue(ScrollDisabledProperty); }
            set { SetValue(ScrollDisabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScrollDisabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScrollDisabledProperty =
            DependencyProperty.Register("ScrollDisabled", typeof(bool), typeof(HtmlDisplayer), new PropertyMetadata(false));

        public HtmlDisplayer()
        {
            InitializeComponent();

            StreamResourceInfo sri = Application.GetResourceStream(new Uri("Bewise.Phone;component/HtmlDisplayer/HtmlTemplate.html", UriKind.Relative));
            using (StreamReader templateReader = new StreamReader(sri.Stream))
            {
                _template = templateReader.ReadToEnd();
            }

        }

        private bool _isBrowserLoaded = false;
        private string _htmlToLoad;

        public void DisplayHtml(String html)
        {
            if (_isBrowserLoaded)
            {
                string fontColor = FetchFontColor();
                string backgroundColor = FetchBackgroundColor();
                string accentColor = FetchAccentColor();

                String navigateString = _template;
                navigateString = navigateString.Replace("$FONTCOLOR$", fontColor);
                navigateString = navigateString.Replace("$BGCOLOR$", backgroundColor);
                navigateString = navigateString.Replace("$ACCENTCOLOR$", accentColor);
                navigateString = navigateString.Replace("$HTMLCONTENT$", html);

                try
                {
                    this.browser.NavigateToString(navigateString);
                }
                catch (Exception)
                {
                    //TODO : faire une vérification plus propre

                }
            }
            else
            {
                _htmlToLoad = html;
            }
        }

        private void browser_Loaded(object sender, RoutedEventArgs e)
        {
            _isBrowserLoaded = true;

            // VISUAL TREE /////////////////////////////////////////
            // WebBrowser
            //      StateContainer (Border)
            //          PresentationContainer (Border)
            //              PanZoomContainer
            //                  Grid
            //                      border (Border)
            /////////////////////////////////////////////////////////

            DependencyObject borderPresentationContainer = VisualTreeHelper.GetChild(VisualTreeHelper.GetChild((Microsoft.Phone.Controls.WebBrowser)sender, 0), 0);
            DependencyObject gridPanZoomContainer = VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(borderPresentationContainer, 0), 0);
            Border border = (Border)VisualTreeHelper.GetChild(gridPanZoomContainer, 0);

            border.ManipulationDelta += Border_ManipulationDelta;
            border.ManipulationCompleted += Border_ManipulationCompleted;

            if (!String.IsNullOrEmpty(_htmlToLoad))
                DisplayHtml(_htmlToLoad);
        }

        private void Border_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            // suppress zoom
            if (e.FinalVelocities.ExpansionVelocity.X != 0.0 ||
                e.FinalVelocities.ExpansionVelocity.Y != 0.0)
                e.Handled = true;
        }

        private void Border_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            // suppress zoom
            if (e.DeltaManipulation.Scale.X != 0.0 ||
                e.DeltaManipulation.Scale.Y != 0.0)
                e.Handled = true;

            // optionally suppress scrolling
            if (ScrollDisabled)
            {
                if (e.DeltaManipulation.Translation.X != 0.0 ||
                    e.DeltaManipulation.Translation.Y != 0.0)
                    e.Handled = true;
            }
        }

        private string FetchAccentColor()
        {
            Color c = (Color)Application.Current.Resources["PhoneAccentColor"];
            return String.Concat("#", c.R.ToString("X2"), c.G.ToString("X2"), c.B.ToString("X2"));
        }

        private string FetchBackgroundColor()
        {
            return IsBackgroundBlack() ? "#000" : "#fff";
        }

        private string FetchFontColor()
        {
            return IsBackgroundBlack() ? "#fff" : "#000";
        }

        private bool IsBackgroundBlack()
        {
            Color mc = (Color)Resources["PhoneBackgroundColor"];
            return mc == Colors.Black;
        }

        private void browser_Navigating(object sender, Microsoft.Phone.Controls.NavigatingEventArgs e)
        {
            if (Navigating != null)
            {
                Navigating(this, e);
            }
            else
            {
                e.Cancel = true;

                WebBrowserTask ie = new WebBrowserTask();
                ie.URL = e.Uri.ToString();
                ie.Show();
            }

        }
    }
}
