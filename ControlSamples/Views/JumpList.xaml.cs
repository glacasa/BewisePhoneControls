using System;
using System.Collections.Generic;
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
using Bewise.Phone;
using Microsoft.Phone.Controls;

namespace ControlSamples.Views
{
    public partial class JumpList 
    {
        ItemObservableCollection<User> collection;

        public JumpList()
        {
            InitializeComponent();
            AfterInitialize();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            collection = new ItemObservableCollection<User>();
            Random rnd = new Random();

            for (int index = 0; index < 500; index++)
            {
                collection.Add(new User { Name = ((char)('a' + rnd.Next(26))) + "", Description = "User " + index });
            }
            lstUsers.ItemsSource = collection;
            lstUsers.OnContinuumCompleted += lstUsers_OnContinuumCompleted;
            base.OnNavigatedTo(e);
        }

        void lstUsers_OnContinuumCompleted(object sender, EventArgs e)
        {
            lstUsers.RestoreContinuum();
        }


        private void lstUsers_SelectedItemChanged(object sender, EventArgs e)
        {
            lstUsers.DoContinuumAnimationOnSelectedItem();
        }



    }



    public class User : INotifyPropertyChanged
    {
        string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        string description;

        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public string Image
        {
            get
            {
                return "http://www.collecto4phone.com/logo.png";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}