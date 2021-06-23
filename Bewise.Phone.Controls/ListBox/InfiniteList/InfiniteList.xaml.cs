using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Bewise.Phone
{
    public partial class InfiniteList : UserControl
    {
        public event EventHandler OnContinuumCompleted;
        public event EventHandler OnRestoreContinuumCompleted;
        public event EventHandler ScrolledToEnd;
        public event EventHandler SelectedItemChanged;

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(InfiniteList), new PropertyMetadata(OnItemsSourceChanged));
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(InfiniteList), new PropertyMetadata(OnSelectedItemChanged));
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(InfiniteList), new PropertyMetadata(OnItemTemplateChanged));

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set
            {
                SetValue(SelectedItemProperty, value);
                if (SelectedItemChanged != null)
                    SelectedItemChanged(this, EventArgs.Empty);
            }
        }


        public InfiniteList()
        {
            InitializeComponent();
            mainListBox.OnContinuumCompleted += new EventHandler(mainListBox_OnContinuumCompleted);
            mainListBox.OnRestoreContinuumCompleted += new EventHandler(mainListBox_OnRestoreContinuumCompleted);
        }

        protected static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            InfiniteList infiniteList = sender as InfiniteList;

            if (e.NewValue == null)
                infiniteList.mainListBox.SelectedItem = null;
        }


        protected static void OnItemTemplateChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            InfiniteList infiniteList = sender as InfiniteList;
            DataTemplate template = e.NewValue as DataTemplate;

            infiniteList.mainListBox.ItemTemplate = template;
        }

        protected static void OnItemsSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            InfiniteList infiniteList = sender as InfiniteList;
            IList list = e.NewValue as IList;

            infiniteList.DoBinding(list);
        }


        private void mainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mainListBox.SelectedItem == null)
                return;
            
            SelectedItem = mainListBox.SelectedItem;
        }

        private void DoBinding(IList list)
        {
                mainListBox.ItemsSource = list;
        }



        public static readonly DependencyProperty ListVerticalOffsetProperty = DependencyProperty.Register(
         "ListVerticalOffset", typeof(double), typeof(InfiniteList), new PropertyMetadata(new PropertyChangedCallback(OnListVerticalOffsetChanged)));


        public double ListVerticalOffset
        {
            get { return (double)this.GetValue(ListVerticalOffsetProperty); }
            set { this.SetValue(ListVerticalOffsetProperty, value); }
        }

        private double _lastFetch;
        private ScrollViewer _listScrollViewer;

        private void ScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            _listScrollViewer = sender as ScrollViewer;

            Binding binding = new Binding();
            binding.Source = _listScrollViewer;
            binding.Path = new PropertyPath("VerticalOffset");
            binding.Mode = BindingMode.OneWay;
            this.SetBinding(ListVerticalOffsetProperty, binding);
        }

        private static void OnListVerticalOffsetChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            InfiniteList list = obj as InfiniteList;
            ScrollViewer viewer = list._listScrollViewer;

            if (viewer != null)
            {
                if (list._lastFetch < viewer.ScrollableHeight)
                {
                    // Trigger within 1/4 the viewport.
                    if (viewer.VerticalOffset >= (viewer.ScrollableHeight - viewer.ViewportHeight / 4))
                    {
                        list.FireScrolledToEnd();
                    }
                }
            }
        }

        private void FireScrolledToEnd()
        {
            if (ScrolledToEnd != null)
                ScrolledToEnd(this, EventArgs.Empty);
        }



        public void DoContinuumAnimationOnSelectedItem()
        {
            mainListBox.DoContinuumAnimationOnSelectedItem();
        }

        public void RestoreContinuum(bool animate = true)
        {
            mainListBox.RestoreContinuum(animate);
        }

        void mainListBox_OnRestoreContinuumCompleted(object sender, EventArgs e)
        {
            if (OnRestoreContinuumCompleted != null)
                OnRestoreContinuumCompleted(this, e);
        }

        void mainListBox_OnContinuumCompleted(object sender, EventArgs e)
        {
            if (OnContinuumCompleted != null)
                OnContinuumCompleted(this, e);
        }
    }
}
