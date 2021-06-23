using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Bewise.Phone
{
    public class ItemPropertyChangedEventArgs : PropertyChangedEventArgs
    {
        public object Item
        {
            get;
            private set;
        }

        public ItemPropertyChangedEventArgs(object item, String propertyName)
            : base(propertyName)
        {
            Item = item;
        }
    }

    public delegate void ItemPropertyChangedEventHandler(object sender, ItemPropertyChangedEventArgs e);

    public interface INotifyItemPropertyChanged
    {
         event ItemPropertyChangedEventHandler ItemPropertyChanged;
    }

    /// <summary>
    /// Observable Collection qui écoute aussi le PropertyChanged des enfants
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ItemObservableCollection<T> : ObservableCollection<T>, INotifyItemPropertyChanged, IDisposable where T : INotifyPropertyChanged
    {
        public event ItemPropertyChangedEventHandler ItemPropertyChanged;
        bool _suspendCollectionChangeNotification;

        public ItemObservableCollection()
        {
            _suspendCollectionChangeNotification = false;
        }

        public void SuspendCollectionChangeNotification()
        {
            _suspendCollectionChangeNotification = true;
        }

        public void ResumeCollectionChangeNotification()
        {
            _suspendCollectionChangeNotification = false;
        }

        // This method was developed by Dan Haligas. Thanks guy:)
        public void AddRange(IList<T> items)
        {
            SuspendCollectionChangeNotification();
            try
            {
                foreach (var i in items)
                {
                    base.InsertItem(Count, i);
                }
            }
            finally
            {
                ResumeCollectionChangeNotification();

                foreach (INotifyPropertyChanged item in items)
                {
                    item.PropertyChanged += item_PropertyChanged;
                }

                var arg = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
                OnCollectionChanged(arg);
            }
        }
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (_suspendCollectionChangeNotification) 
                return;
            
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (INotifyPropertyChanged item in e.NewItems)
                    {
                        item.PropertyChanged += item_PropertyChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (INotifyPropertyChanged item in e.OldItems)
                    {
                        item.PropertyChanged -= item_PropertyChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    foreach (INotifyPropertyChanged item in e.OldItems)
                    {
                        item.PropertyChanged -= item_PropertyChanged;
                    }

                    foreach (INotifyPropertyChanged item in e.NewItems)
                    {
                        item.PropertyChanged += item_PropertyChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:

                    if (e.OldItems != null)
                    {
                        foreach (INotifyPropertyChanged item in e.OldItems)
                        {
                            item.PropertyChanged -= item_PropertyChanged;
                        }
                    }

                    if (e.NewItems != null)
                    {
                        foreach (INotifyPropertyChanged item in e.NewItems)
                        {
                            item.PropertyChanged += item_PropertyChanged;
                        }
                    }

                    break;
                default:
                    break;
            }
            base.OnCollectionChanged(e);
        }

        void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnItemPropertyChanged(new ItemPropertyChangedEventArgs(sender, e.PropertyName));
        }

        protected virtual void OnItemPropertyChanged(ItemPropertyChangedEventArgs e)
        {
            if (ItemPropertyChanged != null)
                ItemPropertyChanged(this, e);
        }

        #region IDisposable Members

        protected override void ClearItems()
        {
            foreach (INotifyPropertyChanged item in this)
            {
                item.PropertyChanged -= item_PropertyChanged;
            }
            base.ClearItems();
        }

        public void Dispose()
        {
            Clear();
        }

        #endregion
    }


}
