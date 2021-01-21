﻿namespace Problem01.FasterQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class FastQueue<T> : IAbstractQueue<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public FastQueue()
        {
            this.head = null;
            this.tail = null;
            this.Count = 0;
        }

        public FastQueue(Node<T> headElement)
        {
            this.head = headElement;
            this.tail = this.head;
            this.Count = 1;
        }
        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var current = this.head;

            while (current != null)
            {
                if (current.Item.Equals(item))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public T Dequeue()
        {
            EnsureNotEmpty();
            Node<T> current = this.head;
            if (this.Count==1)
            {
                this.head = null;
                this.tail = null;
                
            }
            else
            {
                this.head = this.head.Next;
            }
            this.Count--;
            return current.Item;
           
            
        }

        public void Enqueue(T item)
        {
            Node<T> newNode = new Node<T>();
            newNode.Item = item;

            if (this.Count==0)
            {
                this.head = this.tail = newNode;
            }
            else
            {
                this.tail.Next = newNode;
                this.tail = newNode;
            }
            this.Count++;
            
        }

        public T Peek()
        {
            EnsureNotEmpty();
            return this.head.Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this.head;
            while(current!=null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();

        private void EnsureNotEmpty()
        {
            if (this.Count == 0)
                throw new InvalidOperationException();
        }
    }
}