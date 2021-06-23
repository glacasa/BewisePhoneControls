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
using System.ComponentModel;

namespace ControlSamples.Views
{
    public partial class Rating 
    {
        public Rating()
        {
            InitializeComponent();
            AfterInitialize();
            this.DataContext = new RatingViewModel();
        }
    }

    public class RatingViewModel : INotifyPropertyChanged
    {
        private int _score;
        public int Score
        {
            get { return _score; }
            set
            {
                if (_score != value)
                {
                    _score = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Score"));
                }
            }
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}