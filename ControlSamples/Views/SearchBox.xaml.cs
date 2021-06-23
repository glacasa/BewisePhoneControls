using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace ControlSamples.Views
{
    public partial class SearchBox
    {
        IEnumerable<Person> allPersons;

        private void Filter()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(o =>
                                             {
                                                 lock (allPersons)
                                                 {
                                                     IEnumerable<Person> result;

                                                     if (searchBox1.HasFilter)
                                                         result = allPersons.Where(p => searchBox1.DoFilter(p.Name));
                                                     else
                                                         result = new List<Person>(allPersons);

                                                     Dispatcher.BeginInvoke(() =>
                                                     {
                                                         lbPersons.ItemsSource = result;
                                                     });
                                                 }
                                             });
        }

        public override void OnBeforeLoad()
        {
            allPersons = new List<Person>()
                                        {
                                            new Person() { Name = "John Doe" },
                                            new Person() { Name = "Mickael Stinson"},
                                            new Person() { Name = "Vince Carter"},
                                            new Person() { Name = "Bob Holly"}
                                        };
        }

        public override void OnLoad()
        {
            if (lbPersons.ItemsSource == null)
                Filter();
        }

        public SearchBox()
        {
            InitializeComponent();
            AfterInitialize();
            searchBox1.Search += new EventHandler(searchBox1_Search);
        }

        void searchBox1_Search(object sender, EventArgs e)
        {
            Filter();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            searchBox1.Show();
        }
    }

    public class Person
    {
        public string Name { get; set; }
    }
}