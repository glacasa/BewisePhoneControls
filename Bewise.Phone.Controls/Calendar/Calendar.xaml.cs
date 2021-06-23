using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Bewise.Phone
{
    public partial class Calendar : INotifyPropertyChanged
    {
        public event EventHandler OnSelectedDateChanged;

        public static readonly DependencyProperty SelectedDateProperty = DependencyProperty.Register("SelectedDate", typeof(DateTime), typeof(Calendar),
                                                                                                     new PropertyMetadata(OnvalueChanged));
        DateTime date;
        Grid previousGrid;
        Grid currentGrid;
        Button currentBlock;
        readonly Button[, ,] blocks;
        readonly Image[] images;
        bool blockAnimations;
        bool firstAnimation = true;
        bool disableCurrentIndicator;

        static void OnvalueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Calendar picker = (Calendar)d;
            picker.SetDate((DateTime)e.NewValue);
        }

        public bool ShowHeader
        {
            get
            {
                return dateText.Visibility == Visibility.Visible;
            }
            set
            {
                if (value)
                    dateText.Visibility = Visibility.Visible;
                else
                    dateText.Visibility = Visibility.Collapsed;
            }
        }

        public Calendar()
        {
            InitializeComponent();

            blocks = new Button[2, 7, 7];
            images = new Image[2];

            images[0] = new Image();
            images[0].Source = Tools.Circle;
            images[0].Height = 42;
            images[0].Width = 42;


            calendarGrid1.Children.Add(images[0]);

            images[1] = new Image();
            images[1].Source = Tools.Circle;
            images[1].Height = 42;
            images[1].Width = 42;

            calendarGrid2.Children.Add(images[1]);

            DateTime monday = DateTime.Now;
            while (monday.DayOfWeek != DayOfWeek.Monday)
            {
                monday = monday.AddDays(1);
            }

            for (int col = 0; col < 7; col++)
            {
                GenerateHeader(col, calendarGrid1, monday);
                GenerateHeader(col, calendarGrid2, monday);

                monday = monday.AddDays(1);

                for (int row = 1; row < 7; row++)
                {
                    blocks[0, row, col] = GenerateBlock(row, col, DateTime.Now, calendarGrid1);
                    blocks[1, row, col] = GenerateBlock(row, col, DateTime.Now, calendarGrid2);
                }
            }

            previousGrid = calendarGrid2;
            currentGrid = calendarGrid1;

            calendarGrid1.Tag = 0;
            calendarGrid2.Tag = 1;

            date = DateTime.MinValue;
            SelectedDate = DateTime.Now;
        }

        static void GenerateHeader(int col, Grid grid, DateTime dateOfWeek)
        {
            TextBlock headerText = new TextBlock();

            headerText.Text = dateOfWeek.ToString("ddd").Substring(0, 2).ToLower();

            grid.Children.Add(headerText);

            Grid.SetRow(headerText, 0);
            Grid.SetColumn(headerText, col);

            headerText.HorizontalAlignment = HorizontalAlignment.Center;
            headerText.VerticalAlignment = VerticalAlignment.Center;

            headerText.Foreground = (Brush)Application.Current.Resources["PhoneSubtleBrush"];
        }

        void SetDate(DateTime value)
        {
            dateText.Text = value.ToString("MMMM yyyy").ToLower();
            if (value.Month == date.Month && value.Year == date.Year)
            {
                SelectDate(value);
                return;
            }
            date = value;

            int currentGridID = (int)currentGrid.Tag;
            System.Globalization.Calendar calendar = Thread.CurrentThread.CurrentCulture.Calendar;
            int daysCount = calendar.GetDaysInMonth(value.Year, value.Month);

            int row = 1;
            images[currentGridID].Visibility = Visibility.Collapsed;
            // Before
            DateTime firstDateOfWeek = new DateTime(value.Year, value.Month, 1);
            int firstCol = GetIndexOfDayOfWeek(calendar, firstDateOfWeek);
            for (int index = 0; index < firstCol; index++)
            {
                DateTime localDateOfWeek = firstDateOfWeek.AddDays(-(firstCol - index));
                Button block = blocks[currentGridID, 1, index];
                block.Tag = localDateOfWeek;
                block.Content = localDateOfWeek.Day.ToString();

                block.Foreground = (Brush)Application.Current.Resources["PhoneInactiveBrush"];
            }

            // Current month
            for (int index = 1; index <= daysCount; index++)
            {
                DateTime dateOfWeek = new DateTime(value.Year, value.Month, index);
                int col = GetIndexOfDayOfWeek(calendar, dateOfWeek);

                Button block = blocks[currentGridID, row, col];
                block.Foreground = (Brush)Application.Current.Resources["PhoneForegroundBrush"];
                block.Tag = dateOfWeek;
                block.Content = dateOfWeek.Day.ToString();

                if (dateOfWeek.DayOfYear == date.DayOfYear && !disableCurrentIndicator)
                {
                    currentBlock = block;
                    block.FontWeight = FontWeights.Bold;
                    block.Foreground = (Brush)Application.Current.Resources["PhoneAccentBrush"];

                    images[currentGridID].Visibility = Visibility.Visible;

                    Grid.SetRow(images[currentGridID], row);
                    Grid.SetColumn(images[currentGridID], col);
                }

                if (col == 6)
                    row++;
            }

            // After
            DateTime lastDateOfWeek = new DateTime(value.Year, value.Month, daysCount);
            int lastCol = GetIndexOfDayOfWeek(calendar, lastDateOfWeek.AddDays(1));
            int max = (7 - (row + 1)) * 7 + (7 - lastCol);
            for (int index = 1; index <= max; index++)
            {
                DateTime localDateOfWeek = lastDateOfWeek.AddDays(index);
                int col = GetIndexOfDayOfWeek(calendar, localDateOfWeek);
                Button block = blocks[currentGridID, row, col];
                block.Foreground = (Brush)Application.Current.Resources["PhoneInactiveBrush"];
                block.Tag = localDateOfWeek;
                block.Content = localDateOfWeek.Day.ToString();
                if (col == 6)
                    row++;
            }
        }

        void SelectDate(DateTime value)
        {
            currentBlock.FontWeight = FontWeights.Normal;
            currentBlock.Foreground = (Brush)Application.Current.Resources["PhoneForegroundBrush"];

            int currentGridID = (int)currentGrid.Tag;
            images[currentGridID].Visibility = Visibility.Collapsed;
            foreach (FrameworkElement element in currentGrid.Children)
            {
                if (element.Tag != null && ((DateTime)element.Tag).Day == value.Day && ((DateTime)element.Tag).Month == value.Month && !disableCurrentIndicator)
                {
                    int row = Grid.GetRow(element);
                    int col = Grid.GetColumn(element);

                    images[currentGridID].Visibility = Visibility.Visible;

                    Grid.SetRow(images[currentGridID], row);
                    Grid.SetColumn(images[currentGridID], col);
                    currentBlock = (Button)element;
                    currentBlock.FontWeight = FontWeights.Bold;
                    currentBlock.Foreground = (Brush)Application.Current.Resources["PhoneAccentBrush"];

                    date = value;

                    return;
                }
            }
        }

        Button GenerateBlock(int row, int col, DateTime dateTime, Grid rootGrid)
        {
            Button block = new Button();
            block.Content = dateTime.Day.ToString();
            block.Click += block_Click;
            block.Tag = dateTime;
            block.Style = (Style)LayoutRoot.Resources["calendarEntryButtonStyle"];
            rootGrid.Children.Add(block);

            Grid.SetRow(block, row);
            Grid.SetColumn(block, col);

            block.HorizontalAlignment = HorizontalAlignment.Stretch;
            block.VerticalAlignment = VerticalAlignment.Stretch;
            return block;
        }

        static int GetIndexOfDayOfWeek(System.Globalization.Calendar calendar, DateTime dateOfWeek)
        {
            switch (calendar.GetDayOfWeek(dateOfWeek))
            {
                case DayOfWeek.Monday:
                    return 0;
                case DayOfWeek.Tuesday:
                    return 1;
                case DayOfWeek.Wednesday:
                    return 2;
                case DayOfWeek.Thursday:
                    return 3;
                case DayOfWeek.Friday:
                    return 4;
                case DayOfWeek.Saturday:
                    return 5;
                case DayOfWeek.Sunday:
                    return 6;
            }

            return 0;
        }

        void block_Click(object sender, RoutedEventArgs e)
        {
            if (blockAnimations)
                return;
            DateTime newDate = (DateTime)((sender as Button).Tag);
            bool needAnimations = false;

            if (newDate.Month != date.Month)
            {
                SwapGrids();
                needAnimations = true;
            }

            SelectedDate = newDate;

            if (needAnimations)
            {
                blockAnimations = true;
                Storyboard sb = new Storyboard();

                (currentGrid.RenderTransform as TranslateTransform).X = 0;
                Tools.AddNewAnimation(sb, currentGrid, "Opacity", 0, 1.0, 0.5);
                Tools.AddNewAnimation(sb, previousGrid, "Opacity", 1.0, 0, 0.5);

                sb.Completed += sb_Completed;

                StartStoryboard(sb);
            }

            if (OnSelectedDateChanged != null)
                OnSelectedDateChanged(this, EventArgs.Empty);
        }

        void sb_Completed(object sender, EventArgs e)
        {
            previousGrid.Visibility = Visibility.Collapsed;
            blockAnimations = false;
        }

        [TypeConverter(typeof(DateTimeTypeConverter))]
        public DateTime SelectedDate
        {
            set
            {
                if (value != date)
                {
                    SetValue(SelectedDateProperty, value);
                    RaisePropertyChanged("SelectedDate");
                }
            }
            get
            {
                return (DateTime)GetValue(SelectedDateProperty);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private void monthLeftButtonButton_Click(object sender, RoutedEventArgs e)
        {
            MoveLeft();
        }

        void MoveLeft()
        {
            if (blockAnimations)
            {
                return;
            }
            blockAnimations = true;

            SwapGrids();

            disableCurrentIndicator = true;
            SelectedDate = date.AddMonths(-1);
            disableCurrentIndicator = false;

            Storyboard sb = new Storyboard();

            Tools.AddEaseNewAnimation(sb, currentGrid.RenderTransform, "X", -250, 0, 0.5);
            Tools.AddNewAnimation(sb, previousGrid.RenderTransform, "X", 0, 250, 0.5);
            Tools.AddEaseNewAnimation(sb, currentGrid, "Opacity", 0, 1.0, 0.5);
            Tools.AddNewAnimation(sb, previousGrid, "Opacity", 1.0, 0, 0.6);
            sb.Completed += sb_Completed;

            StartStoryboard(sb);
        }

        void StartStoryboard(Storyboard sb)
        {
            if (firstAnimation)
            {
                firstAnimation = false;
                Tools.PreLaunchStoryboard(currentGrid, sb, 0.1, "Opacity", 0, 0.01);
            }
            else
            {
                sb.Begin();
            }
        }

        void SwapGrids()
        {
            Grid temp = currentGrid;
            currentGrid = previousGrid;
            previousGrid = temp;

            currentGrid.Visibility = Visibility.Visible;
            previousGrid.Visibility = Visibility.Visible;
        }

        private void monthRightButton_Click(object sender, RoutedEventArgs e)
        {
            MoveRight();
        }

        void MoveRight()
        {
            if (blockAnimations)
            {
                return;
            }
            blockAnimations = true;

            SwapGrids();

            disableCurrentIndicator = true;
            SelectedDate = date.AddMonths(1);
            disableCurrentIndicator = false;

            Storyboard sb = new Storyboard();

            Tools.AddEaseNewAnimation(sb, currentGrid.RenderTransform, "X", 250, 0, 0.5);
            Tools.AddNewAnimation(sb, previousGrid.RenderTransform, "X", 0, -250, 0.5);
            Tools.AddEaseNewAnimation(sb, currentGrid, "Opacity", 0, 1.0, 0.5);
            Tools.AddNewAnimation(sb, previousGrid, "Opacity", 1.0, 0, 0.6);
            sb.Completed += sb_Completed;

            StartStoryboard(sb);
        }

        private void calendarGrid_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            if (e.TotalManipulation.Translation.X > 0)
                MoveLeft();
            else if (e.TotalManipulation.Translation.X < 0)
                MoveRight();
        }
    }
}
