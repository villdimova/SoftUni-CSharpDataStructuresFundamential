namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> head;

        public SinglyLinkedList()
        {
            this.head = null;
            this.Count = 0;
        }

        public SinglyLinkedList(Node<T> headElement)
        {
            this.head = headElement;
            this.Count = 1;
        }
        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            Node<T> newNode = new Node<T>(item, this.head);
            this.head = newNode;
            this.Count++;
        }

        public void AddLast(T item)
        {
            Node<T> newNode = new Node<T>(item);
            Node<T> current = this.head;

            if (current == null)
            {
                this.head = newNode;
            }
            else
            {
                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = newNode;
            }
            this.Count++;
        }

        public T GetFirst()
        {
            CheckIfListIsNotEmpty();

            return this.head.Element;
        }



        public T GetLast()
        {
            CheckIfListIsNotEmpty();
            Node<T> current = this.head;
            while(current.Next!=null)
            {
                current = current.Next;
            }

            return current.Element;
        }

        public T RemoveFirst()
        {
            CheckIfListIsNotEmpty();
            Node<T> firstNode = this.head;
            this.head = this.head.Next;
            this.Count--;

            return firstNode.Element;
        }

        public T RemoveLast()
        {
            
            CheckIfListIsNotEmpty();
            if (this.Count==1)
            {
                Node<T> deletedElement = this.head;
                this.head = null;
                this.Count--;
                return deletedElement.Element;
            }
            else
            {
                
                Node<T> currentElement = this.head;
                Node<T> previous = this.head;

                while (currentElement.Next!=null)
                {
                    previous = currentElement;
                    currentElement = currentElement.Next;
                }

                Node<T> deletedElement =previous.Next;
                previous.Next = null;

                this.Count--;
                return deletedElement.Element;

            }

            
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = this.head;
            while (current!=null)
            {
                yield return current.Element;

                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void CheckIfListIsNotEmpty()
        {
            if (this.Count==0)
            {
                throw new InvalidOperationException("LinkedList is empty!");
            }
        }

    }
}