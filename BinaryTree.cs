using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    public class BinaryTreeNode<T>
    {
        public T Value { get; set; }
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }
    }

    // Обобщенный класс-коллекция для бинарного дерева
    public class BinaryTree<T> : IEnumerable<T>
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
        }
    }
}
