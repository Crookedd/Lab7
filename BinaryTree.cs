using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    public class BinaryTreeNode<T> where T : IComparable<T>
    {
        public T Value { get; set; }
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }
        public BinaryTreeNode(T value)
        {
            Value = value;
        }

        public void Add(T value)
        {
            if (value.CompareTo(Value) < 0)
            {
                if (Left == null)
                {
                    Left = new BinaryTreeNode<T>(value);
                }
                else
                {
                    Left.Add(value);
                }
            }
            else
            {
                if (Right == null)
                {
                    Right = new BinaryTreeNode<T>(value);
                }
                else
                {
                    Right.Add(value);
                }
            }
        }
    }


    // Обобщенный класс-коллекция для бинарного дерева
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        public BinaryTreeNode<T> root;

        public void Add(T value)
        {
            if (root == null)
            {
                root = new BinaryTreeNode<T>(value);
            }
            else
            {
                root.Add(value);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new BinaryTreeEnumerator<T>(root);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<T> InOrderTravesal()
        {
            return InOrderTravesal(root);
        }

        public IEnumerable<T> InOrderTravesal(BinaryTreeNode<T> node)
        {
            if (node.Left != null)
            {
                foreach (T value in InOrderTravesal(node.Left))
                {
                    yield return value;
                }
            }
            yield return node.Value;
            if (node.Right != null)
            {
                foreach (T value in InOrderTravesal(node.Right))
                {
                    yield return value;
                }
            }
        }

        private class BinaryTreeEnumerator<T> : IEnumerator<T> where T : IComparable<T>
        {
            private BinaryTreeNode<T> root;
            private Stack<BinaryTreeNode<T>> stack;

            public BinaryTreeEnumerator(BinaryTreeNode<T> root)
            {
                this.root = root;
                stack = new Stack<BinaryTreeNode<T>>();
            }

            public bool MoveNext()
            {
                if (stack.Count == 0 && root != null)
                {
                    BinaryTreeNode<T> current = root;
                    while (current != null)
                    {
                        stack.Push(current);
                        current = current.Left;
                    }
                }
                else if (stack.Count > 0 && stack.Peek().Right != null)
                {
                    BinaryTreeNode<T> current = stack.Peek().Right;
                    while (current != null)
                    {
                        stack.Push(current);
                        current = current.Left;
                    }
                }
                else
                {
                    stack.Pop();
                }
                return stack.Count > 0;
            }

            public void Reset()
            {
                stack.Clear();
            }

            public T Current
            {
                get { return stack.Peek().Value; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Dispose()
            {
            }
        }

        // Метод для центрального обхода дерева
       /* public IEnumerable<T> InOrderTraversal()
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
                    yield return node.Value;
                    node = node.Right;
                }
                else
                {
                    stack.Push(node);
                    node = node.Left;
                }
            }
        }*/

        //Метод для прямого обхода дерева
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

        //Метод для обратного обхода дерева
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

        //Метод для обхода дерева в ширину
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
