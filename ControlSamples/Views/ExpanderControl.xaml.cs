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
    public partial class ExpanderControl 
    {
        public ExpanderControl()
        {
            InitializeComponent();
            AfterInitialize();
            this.DataContext = new ExpanderControlViewModel();
        }
    }

    public class ExpanderControlViewModel : INotifyPropertyChanged
    {
        public ExpanderControlViewModel()
        {
            _isExpanded = true;
        }

        public string HeaderText
        {
            get
            {
                if (_isExpanded )
                    return "Hide list";
                else
                    return "Display list";
            }
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }
            set
            {
                if(_isExpanded != value)
                {
                    _isExpanded = value;

                    PropertyChanged(this, new PropertyChangedEventArgs("HeaderText"));
                    PropertyChanged(this, new PropertyChangedEventArgs("IsExpanded"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}