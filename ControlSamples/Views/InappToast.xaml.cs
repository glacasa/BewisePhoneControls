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
using System.Collections.ObjectModel;

namespace ControlSamples.Views
{
    public partial class InappToast 
    {
        ObservableCollection<string> items;
        public InappToast()
        {
            InitializeComponent();
            AfterInitialize();

            ResetListBox();
        }

        private void ResetListBox()
        {
            items = new ObservableCollection<string>();
            for (int i = 0; i < 10; i++)
                items.Add(String.Format("Item {0}", i + 1));
            lbItems.ItemsSource = items;
        }

        private void InappToast_OnClicked(object sender, EventArgs e)
        {
            toast.Hide();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            ResetListBox();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (lbItems.SelectedIndex != -1)
            {
                toast.Show("Deleting " + lbItems.SelectedItem + ". Click to hide message.");
                items.RemoveAt(lbItems.SelectedIndex);
                lbItems.ItemsSource = items;
            }
        }
    }
}