// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Collections.ObjectModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// Defines an <see cref="ObservableCollection{T}"/> that manages the property changed events of the contained <see cref="INotifyPropertyChanged"/> items.
    /// </summary>
    /// <typeparam name="T">
    /// The type of <see cref="INotifyPropertyChanged"/> items.
    /// </typeparam>
    public class ObservableItemCollection<T> : ObservableCollection<T>, IDisposable
        where T : INotifyPropertyChanged
    {
        private bool enableCollectionChanged = true;

        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableItemCollection{T}"/> class that is empty and has a default initial capacity.
        /// </summary>
        public ObservableItemCollection()
        {
            base.CollectionChanged += (s, e) =>
            {
                if (this.enableCollectionChanged)
                {
                    this.CollectionChanged?.Invoke(this, e);
                }
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableItemCollection{T}"/> class that contains elements copied from the specified collection
        /// and has sufficient capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="collection">
        /// The collection whose elements are copied to the new list.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="collection">collection</paramref> parameter cannot be null.</exception>
        public ObservableItemCollection(IEnumerable<T> collection)
            : base(collection)
        {
            base.CollectionChanged += (s, e) =>
            {
                if (this.enableCollectionChanged)
                {
                    this.CollectionChanged?.Invoke(this, e);
                }
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableItemCollection{T}"/> class that contains elements copied from the specified list.
        /// </summary>
        /// <param name="list">
        /// The list whose elements are copied to the new list.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="list">list</paramref> parameter cannot be null.</exception>
        public ObservableItemCollection(List<T> list)
            : base(list)
        {
            base.CollectionChanged += (s, e) =>
            {
                if (this.enableCollectionChanged)
                {
                    this.CollectionChanged?.Invoke(this, e);
                }
            };
        }

        /// <summary>
        /// Occurs when an item is added, removed, changed, moved, or the entire list is refreshed.
        /// </summary>
        public override event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Occurs when an item's <see cref="INotifyPropertyChanged.PropertyChanged"/> event is invoked.
        /// </summary>
        public event ObservableItemCollectionPropertyChangedEventHandler ItemPropertyChanged;

        /// <summary>
        /// Adds a range of objects to the end of the collection.
        /// </summary>
        /// <param name="items">
        /// The objects to add to the end of the collection.
        /// </param>
        public void AddRange(IEnumerable<T> items)
        {
            this.CheckDisposed();
            this.enableCollectionChanged = false;

            foreach (T item in items)
            {
                this.Add(item);
            }

            this.enableCollectionChanged = true;
            this.CollectionChanged?.Invoke(
                this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items));
        }

        /// <summary>
        /// Removes a range of objects from the collection.
        /// </summary>
        /// <param name="items">
        /// The objects to remove from the collection.
        /// </param>
        public void RemoveRange(IEnumerable<T> items)
        {
            this.CheckDisposed();
            this.enableCollectionChanged = false;

            foreach (T item in items)
            {
                this.Remove(item);
            }

            this.enableCollectionChanged = true;
            this.CollectionChanged?.Invoke(
                this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, items));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.disposed)
            {
                return;
            }

            this.ClearItems();
            this.disposed = true;
        }

        /// <summary>
        /// Checks whether the collection is disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// Thrown if the object is disposed.
        /// </exception>
        public void CheckDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
        }

        /// <summary>
        /// Raises the <see cref="CollectionChanged"/> event with the provided arguments.
        /// </summary>
        /// <param name="e">The arguments of the event being raised.</param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            this.CheckDisposed();
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.RegisterPropertyChangedEvents(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Replace:
                    this.UnregisterPropertyChangedEvents(e.OldItems);
                    if (e.NewItems != null)
                    {
                        this.RegisterPropertyChangedEvents(e.NewItems);
                    }

                    break;
                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Reset: break;
            }

            base.OnCollectionChanged(e);
        }

        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
        protected override void ClearItems()
        {
            this.UnregisterPropertyChangedEvents(this);
            base.ClearItems();
        }

        private void RegisterPropertyChangedEvents(IEnumerable items)
        {
            this.CheckDisposed();
            foreach (INotifyPropertyChanged item in items.Cast<INotifyPropertyChanged>().Where(item => item != null))
            {
                item.PropertyChanged += this.OnItemPropertyChanged;
            }
        }

        private void UnregisterPropertyChangedEvents(IEnumerable items)
        {
            this.CheckDisposed();
            foreach (INotifyPropertyChanged item in items.Cast<INotifyPropertyChanged>().Where(item => item != null))
            {
                item.PropertyChanged -= this.OnItemPropertyChanged;
            }
        }

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.CheckDisposed();
            this.ItemPropertyChanged?.Invoke(
                this,
                new ObservableItemCollectionPropertyChangedEventArgs(sender, this.IndexOf((T)sender), e));
        }
    }
}