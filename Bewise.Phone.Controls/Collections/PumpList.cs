using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace Bewise.Phone
{
    public class PumpList<T>
    {
        public event EventHandler OnPumpCompleted;

        readonly ObservableCollection<T> items = new ObservableCollection<T>();
        readonly DispatcherTimer timer = new DispatcherTimer();        
        readonly IList<T> source;

        readonly int totalPumpItems;
        int currentPumpItem;
        int maxPumpItems = 10;

        public ObservableCollection<T> Items
        {
            get { return items; }
        }

        public int MaxPumpItems
        {
            get { return maxPumpItems; }
            set { maxPumpItems = value; }
        }

        public PumpList(IList<T> items)
        {
            source = items;

            totalPumpItems = source.Count;

            // Prefetch the first item
            if (totalPumpItems > 0)
            {
                Items.Add(source[0]);
                currentPumpItem = 1;
            }

            timer.Tick += timer_Tick;
        }
        
        public void StartPump()
        {
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (currentPumpItem == totalPumpItems || currentPumpItem == maxPumpItems)
            {
                timer.Stop();

                for (int index = maxPumpItems; index < totalPumpItems; index++)
                {
                    Items.Add(source[index]);
                }

                if (OnPumpCompleted != null)
                    OnPumpCompleted(this, EventArgs.Empty);

                return;
            }

            Items.Add(source[currentPumpItem++]);
        }
    }
}
