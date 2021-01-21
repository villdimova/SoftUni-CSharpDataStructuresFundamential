namespace Problem03.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IAbstractList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] items;

        public ReversedList()
            : this(DefaultCapacity) { }

        public ReversedList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            this.items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                this.CheckIndex(index);
                return this.items[this.Count - 1 - index];
            }
            set
            {
                this.CheckIndex(index);
                this.items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            CheckTheLength();
            this.items[this.Count++] = item;
           
        }

        

        public bool Contains(T item)
        {
            return this.IndexOf(item) != -1;
        }

        public int IndexOf(T item)
        {

            for (int i = this.Count - 1; i >= 0; i--)
            {
                if (this.items[i].Equals(item))
                {
                    return this.Count - 1 - i;
                }
            }

            return -1;
            
        }

        public void Insert(int index, T item)
        {
            CheckTheLength();
            this.CheckIndex(index);
            int indexToInsert = this.Count -index;

            for (int i = this.Count ; i > indexToInsert; i--)
            {
                this.items[i] = this.items[i - 1];

            }
            this.items[indexToInsert] = item;
            this.Count++;
        }

        public bool Remove(T item)
        {
            int indexOfElement = this.IndexOf(item);
            if (indexOfElement==-1)
            {
                return false;
            }
              this.RemoveAt(indexOfElement);
               
                return true;
           
            
        }

        public void RemoveAt(int index)
        {
            this.CheckIndex(index);
            int indexOfRemovedElement = this.Count - 1 - index;

            for (int i = indexOfRemovedElement; i < this.Count-1; i++)
            {
                this.items[i] = this.items[i + 1];
            }

            
            this.items[this.Count-1] = default;
            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                yield return this.items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
       => this.GetEnumerator();

        private void CheckTheLength()
        {
            if (this.Count == this.items.Length)
            {
                this.GrowIfNecessary();
            }
        }
        private void GrowIfNecessary()
        {
            T[] newArray = new T[2 * this.items.Length];
            for (int i = 0; i < this.items.Length; i++)
            {
                newArray[i] = this.items[i];

            }
            this.items = newArray;
        }

        private void CheckIndex(int index)
        {
            if (index<0|| index>=this.Count)
            {
                throw new IndexOutOfRangeException("Index is out of range!");
            }
        }
    }
}