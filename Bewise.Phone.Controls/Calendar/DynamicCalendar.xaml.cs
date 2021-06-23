using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Bewise.Phone
{
    public partial class DynamicCalendar : UserControl
    {
        Calendar itemDate;
        TranslateTransform itemDateTranslate;
        PlaneProjection textTransform;
        const double animationSpeed = 0.2;

        public event EventHandler OnCalendarOpened;
        public event EventHandler OnCalendarClosed;

        public static readonly DependencyProperty SelectedDateProperty = DependencyProperty.Register("SelectedDate", typeof(DateTime), typeof(DynamicCalendar),
                                                                                                        new PropertyMetadata(OnvalueChanged));

        static void OnvalueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DynamicCalendar picker = (DynamicCalendar)d;

            DateTime newDate = (DateTime)e.NewValue;

            picker.itemDisplayDate.Text = newDate.ToLongDateString().ToLower();
        }

        [TypeConverter(typeof(DateTimeTypeConverter))]
        public DateTime SelectedDate
        {
            set
            {
                SetValue(SelectedDateProperty, value);
            }
            get
            {
                return (DateTime)GetValue(SelectedDateProperty);
            }
        }

        public DynamicCalendar()
        {
            InitializeComponent();

            SelectedDate = DateTime.Now;
        }

        private void editDate_Click(object sender, RoutedEventArgs e)
        {
            EditDate();
        }

        void EditDate()
        {
            if (OnCalendarOpened != null)
                OnCalendarOpened(this, EventArgs.Empty);

            if (itemDate == null)
            {
                itemDateTranslate = new TranslateTransform();
                itemDate = new Calendar();
                itemDate.RenderTransform = itemDateTranslate;
                itemDate.Opacity = 0;

                itemDate.OnSelectedDateChanged += itemDate_OnSelectedDateChanged;

                Grid.SetRow(itemDate, 1);
                if (itemDate.Parent != null)
                    (itemDate.Parent as Panel).Children.Remove(itemDate);

                ContentPanel.Children.Add(itemDate);

                textTransform = new PlaneProjection();
                dateGrid.Projection = textTransform;
            }

            itemDate.SelectedDate = SelectedDate;
            itemDate.Visibility = Visibility.Visible;

            Storyboard sb = new Storyboard();
            sb.Completed += sb_CompletedOut;

            Tools.AddNewAnimation(sb, itemDate, "Opacity", 0, 1, animationSpeed);
            Tools.AddNewAnimation(sb, itemDateTranslate, "Y", 30, 10, animationSpeed);
            Tools.AddNewAnimation(sb, dateGrid, "Opacity", 1, 0, animationSpeed);
            Tools.AddNewAnimation(sb, textTransform, "RotationX", 0, -90, animationSpeed);

            Tools.PreLaunchStoryboard(itemDate, sb, 0.2, "Opacity", 0, 0.01);
        }

        void itemDate_OnSelectedDateChanged(object sender, EventArgs e)
        {
            dateGrid.Visibility = Visibility.Visible;
            itemDisplayDate.Text = itemDate.SelectedDate.ToLongDateString().ToLower();
            SelectedDate = itemDate.SelectedDate;
            Storyboard sb = new Storyboard();

            Tools.AddNewAnimation(sb, dateGrid, "Opacity", 0, 1, animationSpeed);
            Tools.AddNewAnimation(sb, itemDateTranslate, "Y", 10, 30, animationSpeed);
            Tools.AddNewAnimation(sb, itemDate, "Opacity", 1, 0, animationSpeed);
            Tools.AddNewAnimation(sb, textTransform, "RotationX", -90, 0, animationSpeed);

            sb.Completed += sb_Completed;
            sb.Begin();
        }

        void sb_CompletedOut(object sender, EventArgs e)
        {
            dateGrid.Visibility = Visibility.Collapsed;
       }

        void sb_Completed(object sender, EventArgs e)
        {
            itemDate.Visibility = Visibility.Collapsed;

            if (OnCalendarClosed != null)
                OnCalendarClosed(this, EventArgs.Empty);
        }

        private void itemDisplayDate_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            EditDate();
        }
    }
}
