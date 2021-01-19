namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private Node<T> head;

        public Queue()
        {
            this.head = null;
            this.Count = 0;
        }

        public Queue(Node<T>headElement)
        {
            this.head = headElement;
            this.Count = 1;
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            Node<T> current = this.head;

            while (current != null)
            {
                if (current.Element.Equals(item))
                {
                    return true;
                }
                current = current.Next;
            }

            return false;
        }

        public T Dequeue()
        {
            CheckIfNotEmpty();

            Node<T> current = this.head;
            this.head = this.head.Next;
            this.Count--;
            return current.Element;
            
        }

        

        public void Enqueue(T item)
        {
           
            Node<T> newNode = new Node<T>(item);
            if (this.head==null)
            {
                this.head = newNode;
            }
            else
            {
                Node<T> current = this.head;
                while (current.Next!=null)
                {
                    current = current.Next;
                }

                current.Next = newNode;
                
            }
            this.Count++;
        }

        public T Peek()
        {
            CheckIfNotEmpty();
            return this.head.Element;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = this.head;
            while (current == null) 
            {
                yield return current.Element;
                current = current.Next;

            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void CheckIfNotEmpty()
        {
            if (this.Count==0)
            {
                throw new InvalidOperationException("The Stack is empty!");
            }
        }
    }
}