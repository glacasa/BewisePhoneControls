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
using Microsoft.Phone.Controls;

namespace ControlSamples
{
    public partial class MainPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            AfterInitialize();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Navigate(new Uri(((Button)sender).Tag.ToString(),UriKind.RelativeOrAbsolute));
        }
    }
}