using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Bewise.Phone
{
    public partial class ExpanderControl : UserControl
    {

        private bool goTodirectly = true;

        public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register("HeaderText", typeof(object), typeof(ExpanderControl), null);

        public static readonly DependencyProperty ContentAreaProperty = DependencyProperty.Register("ContentArea", typeof(object), typeof(ExpanderControl), null);

        public object ContentArea
        {

            get { return (object)GetValue(ContentAreaProperty); }
            set { SetValue(ContentAreaProperty, value); }
        }

        public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register("IsExpanded", typeof(bool), typeof(ExpanderControl), new PropertyMetadata(true, OnExpandedChangeCallback));

        public bool IsExpanded
        {

            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }


        private static void OnExpandedChangeCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ExpanderControl expanderControl = (ExpanderControl)d;

            bool oldValue = (bool)e.OldValue;
            bool newValue = (bool)e.NewValue;

            expanderControl.DoExpanding(newValue);


        }

        public object HeaderText
        {


            get { return (string)GetValue(HeaderTextProperty); }

            set { SetValue(HeaderTextProperty, value); }
        }


        public ExpanderControl()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(ExpanderControl_Loaded);
        }

        void ExpanderControl_Loaded(object sender, RoutedEventArgs e)
        {
            goTodirectly = false;
        }



        private void DoExpanding(bool visible)
        {
            if (visible)
            {
                Storyboard db = this.Resources["ToExpanded"] as Storyboard;
                db.Begin();
            }
            else
            {
                Storyboard db = this.Resources["ToUnexpanded"] as Storyboard;
                if (goTodirectly)
                {
                    (ContentP.RenderTransform as CompositeTransform).ScaleY = 0;
                    ContentP.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                    db.Begin();

            }
        }

        //private void ToggleB_Checked(object sender, RoutedEventArgs e)
        //{
        //    this.IsExpanded = true;
        //}

        //private void ToggleB_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    this.IsExpanded = false;
        //}
    }
}
