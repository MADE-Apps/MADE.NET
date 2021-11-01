namespace MADE.Collections.Generic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a <see cref="List{T}"/> that is a limited to a set number of items.
    /// </summary>
    /// <typeparam name="T">
    /// The type of object contained within the collection.
    /// </typeparam>
    public class LimitedList<T> : IList<T>
    {
        private readonly List<T> list;

        /// <summary>
        /// Initializes a new instance of the <see cref="LimitedList{T}"/> class.
        /// </summary>
        /// <param name="limit">
        /// The limit of the list.
        /// </param>
        public LimitedList(int limit)
        {
            this.list = new List<T>(limit);

            this.Limit = limit;
        }

        /// <summary>
        /// Gets the limit of the list.
        /// </summary>
        public int Limit { get; }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">
        /// The zero-based index of the element to get or set.
        /// </param>
        /// <returns>
        /// Returns the element at the specified index.
        /// </returns>
        public T this[int index]
        {
            get => this.list[index];
            set => this.list[index] = value;
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="LimitedList{T}"/>.
        /// </summary>
        public int Count => this.list.Count;

        /// <summary>
        /// Gets a value indicating whether the <see cref="LimitedList{T}"/> is read-only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Adds an item to the <see cref="LimitedList{T}"/>.
        /// </summary>
        /// <param name="item">
        /// The object to add.
        /// </param>
        public void Add(T item)
        {
            if (this.list.Count >= this.Limit)
            {
                throw new InvalidOperationException(
                    "This list is limited to " + this.Limit + " items. Additional items cannot be added.");
            }

            this.list.Add(item);
        }

        /// <summary>
        /// Removes all items from the <see cref="LimitedList{T}"/>.
        /// </summary>
        public void Clear()
        {
            this.list.Clear();
        }

        /// <summary>
        /// Determines whether the <see cref="LimitedList{T}"/> contains a specific value.
        /// </summary>
        /// <param name="item">
        /// The object to locate in the <see cref="LimitedList{T}"/>.
        /// </param>
        /// <returns>
        /// Returns true if the item is found; else false.
        /// </returns>
        public bool Contains(T item)
        {
            return this.list.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the <see cref="LimitedList{T}"/> to an array, starting at a particular index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional array that is the destination of the elements copied from <see cref="LimitedList{T}"/>. The array must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">
        /// The zero-based index at which copying begins.
        /// </param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// Returns an <see cref="IEnumerator{T}"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="LimitedList{T}"/>.
        /// </summary>
        /// <param name="item">
        /// The object to locate in the <see cref="LimitedList{T}"/>.
        /// </param>
        /// <returns>
        /// The index of item if found in the list; else -1.
        /// </returns>
        public int IndexOf(T item)
        {
            return this.list.IndexOf(item);
        }

        /// <summary>
        /// Inserts an item to the <see cref="LimitedList{T}"/> at the specified index.
        /// </summary>
        /// <param name="index">
        /// The zero-based index at which the item should be inserted.
        /// </param>
        /// <param name="item">
        /// The object to insert into the <see cref="LimitedList{T}"/>.
        /// </param>
        public void Insert(int index, T item)
        {
            if (this.list.Count >= this.Limit)
            {
                throw new InvalidOperationException(
                    "This list is limited to " + this.Limit + " items. Additional items cannot be inserted.");
            }

            this.list.Insert(index, item);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="LimitedList{T}"/>.
        /// </summary>
        /// <param name="item">
        /// The object to remove from the <see cref="LimitedList{T}"/>.
        /// </param>
        /// <returns>
        /// Returns true if the item was successfully removed; else false.
        /// </returns>
        public bool Remove(T item)
        {
            return this.list.Remove(item);
        }

        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        /// <param name="index">
        /// The zero-based index of the item to remove.
        /// </param>
        public void RemoveAt(int index)
        {
            this.list.RemoveAt(index);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerator{T}"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}