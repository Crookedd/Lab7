using System.Collections;
using System.Collections.Generic;

namespace Lab7 
{

    class BinaryTreeNode<T>
    {
        public T Value { get; set; }
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }
    }
    class BinaryTree<T> : IEnumerable<T>
    {
        private BinaryTreeNode<T> root;

        public void Add(T value)
        {
            var newNode = new BinaryTreeNode<T> { Value = value };

            if (root == null)
            {
                root = newNode;
            }
            else
            {
                AddTo(root, newNode);
            }
        }

        private void AddTo(BinaryTreeNode<T> node, BinaryTreeNode<T> newNode)
        {
            if (Comparer<T>.Default.Compare(newNode.Value, node.Value) < 0)
            {
                if (node.Left == null)
                {
                    node.Left = newNode;
                }
                else
                {
                    AddTo(node.Left, newNode);
                }
            }
            else
            {
                if (node.Right == null)
                {
                    node.Right = newNode;
                }
                else
                {
                    AddTo(node.Right, newNode);
                }
            }
        }

        // Реализация интерфейса IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Метод для центрального обхода дерева
        private IEnumerable<T> InOrderTraversal()
        {
            if (root != null)
            {
                var stack = new Stack<BinaryTreeNode<T>>();
                var currentNode = root;

                while (stack.Count > 0 || currentNode != null)
                {
                    if (currentNode != null)
                    {
                        stack.Push(currentNode);
                        currentNode = currentNode.Left;
                    }
                    else
                    {
                        currentNode = stack.Pop();
                        yield return currentNode.Value;
                        currentNode = currentNode.Right;
                    }
                }
            }
        }
        //Метод для прямого типа обхода
        public IEnumerable<T> Preorder()
        {
            if (root == null)
                yield break;

            var stack = new Stack<BinaryTreeNode<T>>();
            stack.Push(root);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                yield return node.Value;
                if (node.Right != null)
                    stack.Push(node.Right);
                if (node.Left != null)
                    stack.Push(node.Left);
            }
        }
        // Метод для обратного обхода дерева
        public IEnumerable<T> Postorder()
        {
            if (root == null)
                yield break;

            var stack = new Stack<BinaryTreeNode<T>>();
            var node = root;

            while (stack.Count > 0 || node != null)
            {
                if (node == null)
                {
                    node = stack.Pop();
                    if (stack.Count > 0 && node.Right == stack.Peek())
                    {
                        stack.Pop();
                        stack.Push(node);
                        node = node.Right;
                    }
                    else
                    {
                        yield return node.Value;
                        node = null;
                    }
                }
                else
                {
                    if (node.Right != null)
                        stack.Push(node.Right);
                    stack.Push(node);
                    node = node.Left;
                }
            }
        }
        //Метод для обхода в порядке обхода в ширину 
        public IEnumerable<T> Levelorder()
        {
            if (root == null)
                yield break;

            var queue = new Queue<BinaryTreeNode<T>>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                yield return node.Value;
                if (node.Left != null)
                    queue.Enqueue(node.Left);
                if (node.Right != null)
                    queue.Enqueue(node.Right);
            }
        }
    }
}