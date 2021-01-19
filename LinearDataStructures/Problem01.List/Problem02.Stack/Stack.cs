namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private Node<T> top;


        public Stack()
        {
            this.Count = 0;
            this.top = null;
        }

        public Stack(Node<T> topElement)
           
        {
            this.top = topElement;
            this.Count = 1;
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            Node<T> current = this.top;

            while (current!=null)
            {
                if (current.Element.Equals(item))
                {
                    return true;
                }
                current = current.Next;
            }

            return false;
        }

        public T Peek()
        {
            CheckIfEmpty();
            return this.top.Element;
        }

        public T Pop()
        {
            CheckIfEmpty();
            
            T removed = this.top.Element;
            var newTop= this.top.Next;
            this.top.Next = null;
            this.top = newTop;
            
            this.Count--;

            return removed;
        }

        
        public void Push(T item)
        {
            Node<T> newNode = new Node<T>(item);
            newNode.Next = this.top;
            this.top = newNode;
            this.Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = this.top;

            while (current!=null)
            {
                yield return current.Element;

                current = current.Next;


            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void CheckIfEmpty()
        {
            if (this.Count==0)
            {
                throw new InvalidOperationException("The Stack is empty! ");
            }
        }

    }
}