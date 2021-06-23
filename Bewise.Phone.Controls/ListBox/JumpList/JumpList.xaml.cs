using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;

namespace Bewise.Phone
{
    public partial class JumpList
    {
        public event EventHandler OnPumpCompleted;
        public event EventHandler OnContinuumCompleted;

        public static readonly DependencyProperty HeaderWidthProperty =
            DependencyProperty.Register("HeaderWidth", typeof(double), typeof(JumpList), new PropertyMetadata(70.0));
        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.Register("ItemHeight", typeof(double), typeof(JumpList), new PropertyMetadata(double.NaN));
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(JumpList), new PropertyMetadata(OnItemsSourceChanged));
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(JumpList), new PropertyMetadata(OnSelectedItemChanged));
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(JumpList), new PropertyMetadata(OnItemTemplateChanged));
        public static readonly DependencyProperty ShowHeadersProperty =
            DependencyProperty.Register("ShowHeaders", typeof(bool), typeof(JumpList), new PropertyMetadata(true));
        public static readonly DependencyProperty ShowRowHeaderProperty =
            DependencyProperty.Register("ShowRowHeader", typeof(bool), typeof(JumpList), new PropertyMetadata(true));
        public static readonly DependencyProperty PicturePathProperty =
            DependencyProperty.Register("PicturePath", typeof(string), typeof(JumpList), new PropertyMetadata(null));
        public static readonly DependencyProperty EmptyTextProperty =
            DependencyProperty.Register("EmptyText", typeof(string), typeof(JumpList), new PropertyMetadata(OnEmptyTextChanged));
        public static readonly DependencyProperty IsPumpedProperty =
            DependencyProperty.Register("IsPumped", typeof(bool), typeof(JumpList), new PropertyMetadata(true));
        public static readonly DependencyProperty PumpLimitProperty =
            DependencyProperty.Register("PumpLimit", typeof(int), typeof(JumpList), new PropertyMetadata(10));
        public static readonly DependencyProperty BlockListOnLoadProperty =
            DependencyProperty.Register("BlockListOnLoad", typeof(bool), typeof(JumpList), new PropertyMetadata(true));
        public static readonly DependencyProperty HideRowHeaderBackgroundIfPicturePresentProperty =
            DependencyProperty.Register("HideRowHeaderBackgroundIfPicturePresent", typeof(bool), typeof(JumpList), new PropertyMetadata(true));

        Grid lastGrid;
        Image animatedSelectionImage;
        readonly TranslateTransform translateTransform = new TranslateTransform();
        ItemObservableCollection<JumpListItem> ordered;
        IEnumerable cachedSource;
        ListBoxItem restoreListBoxItem;
        readonly Storyboard sbRestoreContinuum = new Storyboard();
        readonly Storyboard sbContinuum = new Storyboard();

        public double HeaderWidth
        {
            get { return (double)GetValue(HeaderWidthProperty); }
            set { SetValue(HeaderWidthProperty, value); }
        }

        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }

        public int PumpLimit
        {
            get { return (int)GetValue(PumpLimitProperty); }
            set { SetValue(PumpLimitProperty, value); }
        }

        public bool BlockListOnLoad
        {
            get { return (bool)GetValue(BlockListOnLoadProperty); }
            set { SetValue(BlockListOnLoadProperty, value); }
        }

        /// <summary>
        /// If your list need a long time to be loaded, set this property to True in order to have a smooth loading
        /// </summary>
        public bool IsPumped
        {
            get { return (bool)GetValue(IsPumpedProperty); }
            set { SetValue(IsPumpedProperty, value); }
        }

        public bool ShowHeaders
        {
            get { return (bool)GetValue(ShowHeadersProperty); }
            set { SetValue(ShowHeadersProperty, value); }
        }

        public bool ShowRowHeader
        {
            get { return (bool)GetValue(ShowRowHeaderProperty); }
            set { SetValue(ShowRowHeaderProperty, value); }
        }

        public bool HideRowHeaderBackgroundIfPicturePresent
        {
            get { return (bool)GetValue(HideRowHeaderBackgroundIfPicturePresentProperty); }
            set { SetValue(HideRowHeaderBackgroundIfPicturePresentProperty, value); }
        }

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
                RaiseSelectedItemChanged();
            }
        }

        /// <summary>
        /// Let you define the path to a picture in the bound item 
        /// </summary>
        public string PicturePath
        {
            get { return (string)GetValue(PicturePathProperty); }
            set
            {
                SetValue(PicturePathProperty, value);
            }
        }

        public string EmptyText
        {
            get { return (string)GetValue(EmptyTextProperty); }
            set
            {
                SetValue(EmptyTextProperty, value);
            }
        }

        public bool LettersGridIsVisible
        {
            get
            {
                return gridLetter.IsVisible;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return emptyText.Visibility == Visibility.Visible;
            }
        }

        public void HideLettersGrid()
        {
            gridLetter.Hide();
        }

        protected static void OnEmptyTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            JumpList jumpList = sender as JumpList;

            jumpList.emptyText.Text = (string)e.NewValue;
        }

        protected static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            JumpList jumpList = sender as JumpList;

            if (e.NewValue == null)
                jumpList.mainListBox.SelectedItem = null;

        }

        protected static void OnItemTemplateChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            JumpList jumpList = sender as JumpList;
            DataTemplate template = e.NewValue as DataTemplate;

            jumpList.mainListBox.ItemTemplate = template;
        }

        public event EventHandler SelectedItemChanged;

        protected virtual void RaiseSelectedItemChanged()
        {
            if (SelectedItemChanged != null)
            {
                SelectedItemChanged(this, EventArgs.Empty);
            }
        }

        readonly GridLetter gridLetter = new GridLetter();

        public JumpList()
        {
            InitializeComponent();

            gridLetter.AssociatedListbox = mainListBox;

            TiltEffect.SetIsTiltEnabled(this, true);           
        }

        protected static void OnItemsSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            JumpList jumpList = sender as JumpList;

            jumpList.restoreListBoxItem = null;

            if (e.OldValue != null)
            {
                jumpList.UnbindOnChanges(e.OldValue as IEnumerable);
            }

            if (e.NewValue == null)
            {
                jumpList.mainListBox.Visibility = Visibility.Collapsed;
                jumpList.emptyText.Visibility = Visibility.Visible;
                if (jumpList.IsPumped)
                    jumpList.RaisePumpCompleted();
                return;
            }

            IEnumerable list = e.NewValue as IEnumerable;

            jumpList.BindOnChanges(list);
            jumpList.DoBinding(list);
        }

        private void UnbindOnChanges(IEnumerable list)
        {
            INotifyCollectionChanged notifyCollectionChanged = list as INotifyCollectionChanged;

            if (notifyCollectionChanged != null)
                notifyCollectionChanged.CollectionChanged -= notifyCollectionChanged_CollectionChanged;
        }

        private void BindOnChanges(IEnumerable list)
        {
            INotifyCollectionChanged notifyCollectionChanged = list as INotifyCollectionChanged;

            if (notifyCollectionChanged != null)
                notifyCollectionChanged.CollectionChanged += notifyCollectionChanged_CollectionChanged;
        }

        void notifyCollectionChanged_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Reset)
                DoBinding(sender as IEnumerable);
        }

        static char RemoveAccent(char c)
        {
            switch (c)
            {
                case 'à':
                case 'á':
                case 'â':
                case 'ã':
                case 'ä':
                case 'å':
                    return 'a';
                case 'ç':
                    return 'c';
                case 'è':
                case 'é':
                case 'ê':
                case 'ë':
                    return 'e';
                case 'ì':
                case 'í':
                case 'î':
                case 'ï':
                    return 'i';
                case 'ñ':
                    return 'n';
                case 'ò':
                case 'ó':
                case 'ô':
                case 'ö':
                case 'õ':
                    return 'o';
                case 'ù':
                case 'ú':
                case 'û':
                case 'ü':
                    return 'u';
                case 'ý':
                case 'ÿ':
                    return 'y';
            }

            return c;
        }

        void DoBinding(IEnumerable list)
        {
            restoreListBoxItem = null;
            mainListBox.DataContext = null;
            List<object> items = new List<object>();

            if (ordered != null)
                ordered.ItemPropertyChanged -= ordered_ItemPropertyChanged;

            ordered = new ItemObservableCollection<JumpListItem>();
            cachedSource = list;

            IEnumerable tempItems;

            if (BlockListOnLoad)
                waitGrid.Visibility = Visibility.Visible;

            if (ShowHeaders)
            {
                foreach (object obj in list)
                {
                    if (!string.IsNullOrEmpty(obj.ToString()))
                        items.Add(obj);
                }

                items.Sort(new StringWithIndexComparer());
                tempItems = items;
            }
            else
            {
                tempItems = list;
            }

            char letter = (char)0;
            int count = 0;

            gridLetter.Clear();
            foreach (object tempItem in tempItems)
            {
                if (ShowHeaders)
                {
                    char firstLetter = RemoveAccent(tempItem.ToString().ToLower()[0]);

                    if (firstLetter != letter)
                    {
                        letter = firstLetter;

                        int index = letter - 'a' + 1;
                        bool dontAdd = false;

                        if (index < 0)
                        {
                            index = 0;
                            letter = '#';
                            if (gridLetter.Letters[index].HasItems)
                                dontAdd = true;
                        }
                        if (index > 27)
                        {
                            index = 27;
                            letter = '?';
                            if (gridLetter.Letters[index].HasItems)
                                dontAdd = true;
                        }


                        if (!dontAdd)
                        {
                            gridLetter.Letters[index].HasItems = true;
                            gridLetter.Letters[index].Count = count;

                            JumpListItem letterItem = new JumpListItem { DataContext = letter.ToString(), IsGroup = true };
                            ordered.Add(letterItem);
                            letterItem.HeaderWidth = HeaderWidth;

                            if (double.IsNaN(ItemHeight))
                                letterItem.Height = 70;
                            else
                            {
                                letterItem.Height = ItemHeight;
                            }

                            count++;
                        }
                    }
                }

                JumpListItem jumpListItem = new JumpListItem
                                                {
                                                    ImagePropertyName = PicturePath,
                                                    DataContext = tempItem,
                                                    ShowRowHeader = ShowRowHeader,
                                                    HideRowHeaderBackgroundIfPicturePresent =
                                                        HideRowHeaderBackgroundIfPicturePresent,
                                                    Height = ItemHeight,
                                                    HeaderWidth = HeaderWidth
                                                };

                ordered.Add(jumpListItem);
                count++;
            }

            if (count == 0)
            {
                mainListBox.Visibility = Visibility.Collapsed;
                emptyText.Visibility = Visibility.Visible;

                if (IsPumped)
                    RaisePumpCompleted();
                return;
            }

            mainListBox.Visibility = Visibility.Visible;
            emptyText.Visibility = Visibility.Collapsed;

            if (IsPumped)
            {
                PumpList<JumpListItem> pump = new PumpList<JumpListItem>(ordered) {MaxPumpItems = PumpLimit};
                pump.OnPumpCompleted += pump_OnPumpCompleted;
                mainListBox.ItemsSource = pump.Items;
                pump.StartPump();
            }
            else
            {
                mainListBox.ItemsSource = ordered;
                if (BlockListOnLoad)
                    waitGrid.Visibility = Visibility.Collapsed;
            }


            ordered.ItemPropertyChanged += ordered_ItemPropertyChanged;
        }

        void ordered_ItemPropertyChanged(object sender, ItemPropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                DoBinding(cachedSource);
            }
        }

        void pump_OnPumpCompleted(object sender, EventArgs e)
        {
            if (BlockListOnLoad)
                waitGrid.Visibility = Visibility.Collapsed;
            RaisePumpCompleted();
        }

        void RaisePumpCompleted()
        {
            if (OnPumpCompleted != null)
                OnPumpCompleted(this, EventArgs.Empty);
        }

        public void ScrollIntoView(object item)
        {
            mainListBox.ScrollIntoView(item);
        }

        private void ButtonLetterClick(object sender, RoutedEventArgs e)
        {
            gridLetter.Show();
        }

        private void mainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mainListBox.SelectedItem == null)
                return;

            JumpListItem jump = mainListBox.SelectedItem as JumpListItem;

            if (jump.IsGroup)
                return;

            SelectedItem = jump.DataContext;
        }

        public void DoContinuumAnimationOnSelectedItem()
        {
            if (mainListBox.SelectedItem == null)
                return;

            if (restoreListBoxItem != null)
                return;

            restoreListBoxItem = mainListBox.ItemContainerGenerator.ContainerFromItem(mainListBox.SelectedItem) as ListBoxItem;
            GeneralTransform generalTransform = restoreListBoxItem.TransformToVisual(lastGrid);

            Point location = generalTransform.Transform(new Point(0, 0));

            WriteableBitmap bitmap = new WriteableBitmap(restoreListBoxItem, null);

            animatedSelectionImage.Source = bitmap;

            translateTransform.X = location.X;
            translateTransform.Y = location.Y;

            animatedSelectionImage.Visibility = Visibility.Visible;
            
            restoreListBoxItem.Opacity = 0;

            (sbContinuum.Children[0] as DoubleAnimation).To = location.X + 225;
            (sbContinuum.Children[1] as DoubleAnimation).To = location.Y + 73;
            
            sbContinuum.Begin();
        }

        void sbContinuum_Completed(object sender, EventArgs e)
        {
            animatedSelectionImage.Visibility = Visibility.Collapsed;

            if (OnContinuumCompleted != null)
                OnContinuumCompleted(this, EventArgs.Empty);
        }

        public void RestoreContinuum(bool animate = true)
        {
            if (restoreListBoxItem == null)
                return;

            if (!animate)
            {
                animatedSelectionImage.Visibility = Visibility.Collapsed;
                restoreListBoxItem.Opacity = 1.0;
                restoreListBoxItem = null;
                return;
            }

            GeneralTransform generalTransform = restoreListBoxItem.TransformToVisual(lastGrid);

            Point location = generalTransform.Transform(new Point(0, 0));

            WriteableBitmap bitmap = new WriteableBitmap(restoreListBoxItem, null);

            animatedSelectionImage.Source = bitmap;

            translateTransform.X = location.X - 225;
            translateTransform.Y = location.Y - 73;

            animatedSelectionImage.Visibility = Visibility.Visible;

            (sbRestoreContinuum.Children[0] as DoubleAnimation).To = location.X;
            (sbRestoreContinuum.Children[1] as DoubleAnimation).To = location.Y;

            sbRestoreContinuum.Begin();
        }

        void sbRestoreContinuum_Completed(object sender, EventArgs e)
        {
            animatedSelectionImage.Visibility = Visibility.Collapsed;
            restoreListBoxItem.Opacity = 1.0;
            restoreListBoxItem = null;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (gridLetter.Parent != null)
                return;

            FrameworkElement root = Parent as FrameworkElement;
            lastGrid = null;

            while (root.Parent != null)
            {
                Grid tempGrid = root as Grid;

                if (tempGrid != null)
                    lastGrid = tempGrid;

                if (root.Parent is PhoneApplicationPage)
                {
                    break;
                }
                root = root.Parent as FrameworkElement;
            }

            animatedSelectionImage = new Image
                                         {
                                             HorizontalAlignment = HorizontalAlignment.Left,
                                             VerticalAlignment = VerticalAlignment.Top,
                                             Stretch = Stretch.None
                                         };


            if (lastGrid.RowDefinitions.Count > 0)
            {
                Grid.SetRowSpan(gridLetter, lastGrid.RowDefinitions.Count);
                Grid.SetRowSpan(animatedSelectionImage, lastGrid.RowDefinitions.Count);
            }

            if (lastGrid.ColumnDefinitions.Count > 0)
            {
                Grid.SetColumnSpan(gridLetter, lastGrid.ColumnDefinitions.Count);
                Grid.SetColumnSpan(animatedSelectionImage, lastGrid.ColumnDefinitions.Count);
            }

            lastGrid.Children.Add(animatedSelectionImage);
            lastGrid.Children.Add(gridLetter);

            animatedSelectionImage.RenderTransform = translateTransform;

            // Continuum
            DoubleAnimation animation = new DoubleAnimation {Duration = TimeSpan.FromMilliseconds(350)};

            Storyboard.SetTarget(animation, translateTransform);
            Storyboard.SetTargetProperty(animation, new PropertyPath("X"));
            ExponentialEase ease = new ExponentialEase {EasingMode = EasingMode.EaseIn, Exponent = 6};
            animation.EasingFunction = ease;

            sbContinuum.Children.Add(animation);

            animation = new DoubleAnimation {Duration = TimeSpan.FromMilliseconds(350)};

            Storyboard.SetTarget(animation, translateTransform);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Y"));
            ease = new ExponentialEase {EasingMode = EasingMode.EaseIn, Exponent = 6};
            animation.EasingFunction = ease;

            sbContinuum.Children.Add(animation);

            animation = new DoubleAnimation {From = 1, To = 0, Duration = TimeSpan.FromMilliseconds(350)};

            Storyboard.SetTarget(animation, animatedSelectionImage);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
            ease = new ExponentialEase {EasingMode = EasingMode.EaseIn, Exponent = 6};
            animation.EasingFunction = ease;

            sbContinuum.Children.Add(animation);

            sbContinuum.Completed += sbContinuum_Completed;


            // Restore Continuum
            animation = new DoubleAnimation {Duration = TimeSpan.FromMilliseconds(350)};

            Storyboard.SetTarget(animation, translateTransform);
            Storyboard.SetTargetProperty(animation, new PropertyPath("X"));
            ease = new ExponentialEase {EasingMode = EasingMode.EaseOut, Exponent = 6};
            animation.EasingFunction = ease;

            sbRestoreContinuum.Children.Add(animation);

            animation = new DoubleAnimation {Duration = TimeSpan.FromMilliseconds(350)};

            Storyboard.SetTarget(animation, translateTransform);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Y"));
            ease = new ExponentialEase {EasingMode = EasingMode.EaseOut, Exponent = 6};
            animation.EasingFunction = ease;

            sbRestoreContinuum.Children.Add(animation);

            animation = new DoubleAnimation {From = 0, To = 1, Duration = TimeSpan.FromMilliseconds(350)};

            Storyboard.SetTarget(animation, animatedSelectionImage);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
            ease = new ExponentialEase {EasingMode = EasingMode.EaseOut, Exponent = 6};
            animation.EasingFunction = ease;

            sbRestoreContinuum.Children.Add(animation);

            sbRestoreContinuum.Completed += sbRestoreContinuum_Completed;
        }
    }
}
