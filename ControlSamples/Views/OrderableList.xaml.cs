using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Data;
using System.Text;

namespace ControlSamples.Views
{
    public partial class OrderableList 
    {
        public OrderableList()
        {
            InitializeComponent();
            AfterInitialize();

            ObservableCollection<string> l = new ObservableCollection<string>();
            for (int i = 0; i < 10; i++) 
                l.Add(i.ToString());

            this.DataContext = l;

        }

    

    }


}