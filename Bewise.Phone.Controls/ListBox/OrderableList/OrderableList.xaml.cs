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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bewise.Phone
{
    public partial class OrderableList : UserControl
    {
        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public string DragImageUrl
        {
            get { return (string)GetValue(DragImageUrlProperty); }
            set
            {
                SetValue(DragImageUrlProperty, value);
            }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IList), typeof(OrderableList), new PropertyMetadata(OnItemsSourceChanged));
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(OrderableList), new PropertyMetadata(null));
        public static readonly DependencyProperty DragImageUrlProperty =
            DependencyProperty.Register("DragImageUrl", typeof(string), typeof(OrderableList), new PropertyMetadata("/Bewise.Phone;component/ListBox/OrderableList/grip.png", null));

        private List<OrderableListItem> boundList;
        private OrderableListItem currentlyMovingItem;
        private int newPosition;


        protected static void OnItemsSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            OrderableList list = sender as OrderableList;
            list.RebindList(e.NewValue as IList);

        }

        private void RebindList(IList newList)
        {
            boundList = new List<OrderableListItem>();
            int i = 0;
            foreach (var item in newList)
            {
                boundList.Add(new OrderableListItem(item, i++, this.DragImageUrl));
            }

            mainList.ItemsSource = boundList;
        }

        public OrderableList()
        {
            InitializeComponent();
            mainList.DataContext = this;
        }


        private double initialImagePosition;
        void rect_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            e.Handled = true;

            Grid g = ((Image)sender).Parent as Grid;
            currentlyMovingItem = g.DataContext as OrderableListItem;
            newPosition = currentlyMovingItem.Index;

            WriteableBitmap bitmap = new WriteableBitmap(g, null);
            movingImage.Source = bitmap;
            movingImage.Visibility = Visibility.Visible;

            GeneralTransform generalTransform = g.TransformToVisual(mainGrid);
            Point location = generalTransform.Transform(new Point(0, 0));

            initialImagePosition = location.Y;
            SetImagePositionAnimation.To = initialImagePosition;
            SetImagePosition.Begin();

            g.Opacity = 0;

            FadeOut.Begin();
        }

        void rect_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            e.Handled = true;
            if (currentlyMovingItem != null)
            {
                //movingImageTransform.Y += e.DeltaManipulation.Translation.Y;
                SetImagePositionAnimation.To = initialImagePosition + e.CumulativeManipulation.Translation.Y;
                SetImagePosition.Begin();

                // Find current position
                newPosition = 0;
                foreach (OrderableListItem item in boundList)
                {
                    if (currentlyMovingItem.Top + e.CumulativeManipulation.Translation.Y < item.Top)
                        break;
                    newPosition = item.Index;
                }

                foreach (OrderableListItem item in boundList)
                {
                    if ((item.Index < newPosition && item.Index < currentlyMovingItem.Index)
                    || (item.Index > newPosition && item.Index > currentlyMovingItem.Index))
                        item.Reset();

                    if (item.Index <= newPosition && item.Index > currentlyMovingItem.Index)
                        item.GoUp(currentlyMovingItem.Grid.ActualHeight);

                    if (item.Index >= newPosition && item.Index < currentlyMovingItem.Index)
                        item.GoDown(currentlyMovingItem.Grid.ActualHeight);
                }
            }

        }

        void rect_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            e.Handled = true;
            RestoreImagePositionAnimation.To = boundList.First(li => li.Index == newPosition).Top;
            System.Diagnostics.Debug.WriteLine(RestoreImagePositionAnimation.To);

            RestoreImagePosition.Begin();
            FadeIn.Begin();
        }

        void FadeIn_Completed(object sender, EventArgs e)
        {
            object objectToMove = currentlyMovingItem.DataContext;
            ItemsSource.RemoveAt(currentlyMovingItem.Index);
            ItemsSource.Insert(newPosition, objectToMove);
            RebindList(ItemsSource);

            movingImage.Visibility = Visibility.Collapsed;
            currentlyMovingItem = null;
        }


        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Grid g = sender as Grid;
            OrderableListItem listItem = g.DataContext as OrderableListItem;
            listItem.SetGrid(g);

            GeneralTransform gt = g.TransformToVisual(mainGrid);
            Point offset = gt.Transform(new Point(0, 0));
            listItem.Top = offset.Y;
            System.Diagnostics.Debug.WriteLine("Load : " + listItem.Top);
        }



    }


    public class OrderableListItem
    {
        private enum AnimState
        {
            Up,
            GoingUp,
            GoingToDefault,
            Default,
            GoingDown,
            Down
        }

        public string ImageUrl { get; set; }

        public Grid Grid { get; set; }
        public double Top { get; set; }

        public int Index { get; private set; }
        public object DataContext { get; private set; }

        public OrderableListItem(object context, int index, string imgUrl)
        {
            state = AnimState.Default;
            this.DataContext = context;
            this.Index = index;
            this.ImageUrl = imgUrl;
        }

        public void SetGrid(Grid g)
        {
            Grid = g;
            InitAnimation();
        }


        // Animation

        private AnimState state;
        private Storyboard sbMove;
        private Storyboard sbMoveReverse;
        private DoubleAnimation animation;
        private DoubleAnimation animationReverse;


        private void InitAnimation()
        {
            sbMove = new Storyboard();
            sbMove.Completed += new EventHandler(sbMove_Completed);

            animation = new DoubleAnimation { Duration = TimeSpan.FromMilliseconds(250) };

            Storyboard.SetTarget(animation, Grid.RenderTransform);
            Storyboard.SetTargetProperty(animation, new PropertyPath("TranslateY"));

            sbMove.Children.Add(animation);



            sbMoveReverse = new Storyboard();
            sbMoveReverse.Completed += new EventHandler(sbMove_Completed);

            animationReverse = new DoubleAnimation { Duration = TimeSpan.FromMilliseconds(250) };

            Storyboard.SetTarget(animationReverse, Grid.RenderTransform);
            Storyboard.SetTargetProperty(animationReverse, new PropertyPath("TranslateY"));
            animationReverse.To = 0;
            sbMoveReverse.Children.Add(animationReverse);
        }

        public void Reset()
        {
            if (state != AnimState.Default && state != AnimState.GoingToDefault)
            {
                state = AnimState.GoingToDefault;
                sbMoveReverse.Begin();
            }
        }

        public void GoDown(double offset)
        {
            if (state == AnimState.Default || state == AnimState.GoingToDefault)
            {
                state = AnimState.GoingDown;
                animation.To = offset;
                sbMove.Begin();
            }
        }

        public void GoUp(double offset)
        {
            if (state == AnimState.Default || state == AnimState.GoingToDefault)
            {
                state = AnimState.GoingUp;
                animation.To = -offset;
                sbMove.Begin();
            }
        }

        void sbMove_Completed(object sender, EventArgs e)
        {
            if (state == AnimState.GoingDown)
                state = AnimState.Down;
            if (state == AnimState.GoingUp)
                state = AnimState.Up;
            if (state == AnimState.GoingToDefault)
                state = AnimState.Default;
        }

    }
}
