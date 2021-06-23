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
using System.Collections.ObjectModel;

namespace ControlSamples.Views
{
    public partial class HtmlDisplayer 
    {
        public HtmlDisplayer()
        {
            InitializeComponent();
            AfterInitialize();
        }

        private void HtmlDisplayer_Loaded(object sender, RoutedEventArgs e)
        {
            this.html.DisplayHtml("<h1>Hello World !</h1><p>This is HTML code !</p><p>External link : <a href=\"http://bewisephonecontrols.codeplex.com\">Back to Bewise Phone Controls page !</a></p><p>");
        }
    }
}