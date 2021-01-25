namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> children;

        public Tree(T value)
        {
            this.Value = value;
            this.Parent = null;
            this.children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {
                child.Parent = this;
                this.children.Add(child);
            }
        }

        public T Value { get; private set; }
        public Tree<T> Parent { get; private set; }
        public IReadOnlyCollection<Tree<T>> Children => this.children.AsReadOnly();

        public bool IsRootDeleted { get; private set; }
        public ICollection<T> OrderBfs()
        {
            var result = new List<T>();
            if (this.IsRootDeleted)
            {
                return result;
            }
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                var currentElement = queue.Dequeue();
                result.Add(currentElement.Value);

                foreach (var child in currentElement.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public ICollection<T> OrderDfs()
        {
            var result = new List<T>();

            if (this.IsRootDeleted)
            {
                return result;
            }
            this.Dfs(this, result);

            return result;
        }

        

        public void AddChild(T parentKey, Tree<T> child)
        {
            var parentSubtree = this.FindBfs(parentKey);
            this.CheckEmptyNode(parentSubtree);
            parentSubtree.children.Add(child);
        }

        

        public void RemoveNode(T nodeKey)
        {
            var searchedNode = this.FindBfs(nodeKey);
            CheckEmptyNode(searchedNode);

            foreach (var child in searchedNode.children)
            {
                child.Parent = null;
            }

            searchedNode.children.Clear();

            var parentNode = searchedNode.Parent;
            if (parentNode == null)
            {
                this.IsRootDeleted = true;
            }
            else
            {
                parentNode.children.Remove(searchedNode);
            }
           
            searchedNode.Value = default;

           
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstNode = this.FindBfs(firstKey);
            var secondNode = this.FindBfs(secondKey);
            this.CheckEmptyNode(firstNode);
            this.CheckEmptyNode(secondNode);

            var firstParent = firstNode.Parent;
            var secondParent = secondNode.Parent;

            if (firstParent == null)
            {
                SwapRoots(secondNode);
                return;
            }

            if (secondParent == null)
            {
                SwapRoots(firstNode);
                return;
            }

            firstNode.Parent = secondParent;
            secondNode.Parent = firstParent;

            var indexOfFirstNode = firstParent.children.IndexOf(firstNode);
            var indexOfSecondNode = secondParent.children.IndexOf(secondNode);

            firstParent.children[indexOfFirstNode] = secondNode;
            secondParent.children[indexOfSecondNode] = firstNode;



        }

        private void Dfs(Tree<T> subTree, List<T> result)
        {
            foreach (var child in subTree.Children)
            {
                child.Dfs(child,result);
            }

            result.Add(subTree.Value);
        }

        private Tree<T> FindBfs(T value)
        {
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count>0)
            {
                var subtree = queue.Dequeue();

                if (subtree.Value.Equals(value))
                {
                    return subtree;
                }

                foreach (var child in subtree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        private void CheckEmptyNode(Tree<T> parentSubtree)
        {
            if (parentSubtree==null)
            {
                throw new ArgumentNullException();
            }
        }

        private void SwapRoots(Tree<T> rootNode)
        {
            
                this.Value = rootNode.Value;
                this.children.Clear();
                foreach (var child in rootNode.Children)
                {
                    this.children.Add(child);
                }
           
        }
        
    }
}
