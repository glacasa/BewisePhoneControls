using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Phone.Controls;

namespace Bewise.Phone
{
    public partial class GridLetter
    {
        ListBox associatedListbox;
        int targetCount;
        readonly ObservableCollection<JumpListLetter> letters = new ObservableCollection<JumpListLetter>();
  
        bool warmUp = true;

        public GridLetter()
        {
            InitializeComponent();
            hideLetters.Completed += hideLetters_Completed;
            
            TiltEffect.SetIsTiltEnabled(this, true);

            for (int index = 0; index < 28; index++)
            {
                if (index == 0)
                    Letters.Add(new JumpListLetter { Text = "#" });
                else if (index == 27)
                    Letters.Add(new JumpListLetter { Text = "..." });
                else
                    Letters.Add(new JumpListLetter { Text = ((char)('a' + index - 1)).ToString()});
            }

            DataContext = Letters;
        }

        void hideLetters_Completed(object sender, System.EventArgs e)
        {
            lettersGrid.Visibility = Visibility.Collapsed;
        }

        internal ListBox AssociatedListbox
        {
            set { associatedListbox = value; }
        }

        public bool IsVisible
        {
            get { return lettersGrid.Visibility == Visibility.Visible; }
        }

        public ObservableCollection<JumpListLetter> Letters
        {
            get { return letters; }
        }

        public void Show()
        {
            lettersGrid.Visibility = Visibility.Visible;

            if (warmUp)
            {
                warmUp = false;
                Tools.PreLaunchStoryboard(lettersGrid, showLetters, 0.1, "Opacity", 0, 0.01);
            }
            else
            {
                showLetters.Begin();
            }
        }

        public void Hide()
        {
            hideLetters.Begin();
        }

        public void Clear()
        {
            foreach (JumpListLetter letter in letters)
            {
                letter.HasItems = false;
                letter.Count = 0;
            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Button Button = sender as Button;
            string letter = Button.Content.ToString();

            foreach (JumpListLetter letterItem in letters)
            {
                if (letterItem.Text == letter)
                {
                    targetCount = letterItem.Count;

                    Dispatcher.BeginInvoke(() =>
                                               {
                                                   ScrollViewer scrollViewer = VisualTreeHelper.GetChild(associatedListbox, 0) as ScrollViewer;
                                                   scrollViewer.ScrollToVerticalOffset(targetCount);
                                               });
                    hideLetters.Begin();

                    return;
                }
            }
        }
    }
}
