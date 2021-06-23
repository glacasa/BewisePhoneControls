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
    public partial class InfiniteList 
    {

        ObservableCollection<String> collection = new ObservableCollection<string>();
        private int reloadCount = 0;

        public InfiniteList()
        {
            InitializeComponent();
            AfterInitialize();

            for (int i = 0; i < 50; i++)
            {
                collection.Add("Item n° : " + i);
            }

            list.ItemsSource = collection;

        }

        private void InfiniteList_ScrolledToEnd(object sender, EventArgs e)
        {
            reloadCount++;
            for (int i = 0; i < 10; i++)
            {
                collection.Add("Reload n° " + reloadCount + " - Item n° : " + i);
            }
        }

        private void list_SelectedItemChanged(object sender, EventArgs e)
        {
            list.DoContinuumAnimationOnSelectedItem();
        }

        private void list_OnContinuumCompleted(object sender, EventArgs e)
        {
            list.SelectedItem = null;
            list.RestoreContinuum();
        }

     
    }
}