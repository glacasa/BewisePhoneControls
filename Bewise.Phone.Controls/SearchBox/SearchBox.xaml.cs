using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Bewise.Phone
{
    public partial class SearchBox
    {
        public static readonly DependencyProperty FilterProperty = DependencyProperty.Register("Filter", typeof(string), typeof(SearchBox),
                                                                                                     new PropertyMetadata("", OnFilterChanged));

        string filter = "";

        static void OnFilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string val = (string) e.NewValue;
            SearchBox searchBox = (SearchBox) d;

            searchBox.filter = val;
            if (searchBox.filterText.Text != val)
                searchBox.filterText.Text = val;
        }

        public event EventHandler Search;

        public string Filter
        {
            get
            {
                return (string)GetValue(FilterProperty);
            }
            set
            {
                SetValue(FilterProperty, value);
                filter = value;
            }
        }

        public bool HasFilter
        {
            get
            {
                return !string.IsNullOrEmpty(filter);
            }
        }

        public bool DoFilter(string value)
        {
            return value.ToLower().Contains(filter.ToLower());
        }

        public SearchBox()
        {
            InitializeComponent();
            ((Storyboard)filterBox.Resources["moveUp"]).Completed += SearchBox_Completed;
            ((Storyboard)filterBox.Resources["moveDown"]).Completed += sb_Completed;
        }

        void SearchBox_Completed(object sender, EventArgs e)
        {
            filterText.Focus();
        }

        void filterText_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter = filterText.Text;
        }

        void filterButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseSearch();
        }

        void RaiseSearch()
        {
            if (Search != null)
                Search(this, EventArgs.Empty);
        }

        public bool IsVisible
        {
            get
            {
                return filterBox.Opacity == 1;
            }
        }

        public void Show()
        {
            filterBox.Visibility = Visibility.Visible;
            filterText.IsEnabled = true;
            ((Storyboard)filterBox.Resources["moveUp"]).Begin();
        }

        public void Hide()
        {
            if (filterBox.Visibility == Visibility.Collapsed)
                return;

            if (Filter != "")
            {
                Filter = "";
                RaiseSearch();
            }
            Storyboard sb = ((Storyboard) filterBox.Resources["moveDown"]);

            sb.Begin();
        }

        void sb_Completed(object sender, EventArgs e)
        {
            filter = "";
            filterText.IsEnabled = false;
            filterBox.Visibility = Visibility.Collapsed;
        }

        private void filterText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                filterButton.Focus();
                RaiseSearch();
            }
        }
    }
}
