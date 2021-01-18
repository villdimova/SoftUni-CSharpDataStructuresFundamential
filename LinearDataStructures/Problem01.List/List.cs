namespace Problem01.List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class List<T> : IAbstractList<T>
    {
        private const int DEFAULT_CAPACITY = 4;
        private T[] array;
        private int count;
       

        public List(int capacity = DEFAULT_CAPACITY)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(capacity)} is not a valid capacity.");
            }
            this.array = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                this.ValidateIndex(index);
                return this.array[index];
            }
            set
            {
                this.ValidateIndex(index);
                this.array[index] = value;
            }
        }

        
        public int Count { get; private set; }
        

        public void Add(T item)
        {

            EnsureNotEmpty();
            this.array[this.Count++] = item;
        }


        public bool Contains(T item)
        {
            bool result = false;
            for (int i = 0; i < this.Count; i++)
            {
                
                if (this.array[i].Equals(item))
                {
                    result= true;
                }
                

            }

            return result;
        }


        public int IndexOf(T item)
        {
            
            int result = -1;
            for (int i = 0; i < this.Count; i++)
            {
                if (this.array[i].Equals(item))
                {
                    result = i;
                }
            }
            return result;
        }

        public void Insert(int index, T item)
        {
            this.ValidateIndex(index);
            this.EnsureNotEmpty();

            for (int i = this.Count; i > index; i--)
            {
                this.array[i] = this.array[i - 1];


            }
            this.array[index] = item;
            this.Count++;
        }


        public bool Remove(T item)
        {
            bool result = false;
            for (int i = 0; i < this.array.Length; i++)
            {
                if (this.array[i].Equals(item))
                {
                    result= true;
                    this.RemoveAt(i);
                }
            }

            return result;
        }

        public void RemoveAt(int index)
        {
            ValidateIndex(index);
            for (int i = index; i < this.Count-1; i++)
            {
                this.array[i] = this.array[i + 1];

            }
            this.array[this.Count - 1] = default;
            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this.array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void EnsureNotEmpty()
        {
            if (this.Count == this.array.Length)
            {
                Resize();
            }
        }

        private void Resize()
        {
            var newArray = new T[this.array.Length * 2];
            for (int i = 0; i < this.array.Length; i++)
            {
                newArray[i] = this.array[i];
            }

            this.array = newArray;
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }
        }



    }

}