using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<int> integerTree = new BinaryTree<int>();

            Random rand = new Random();

            for (int Index = 0; Index < 20; ++Index)
            {
                int value = rand.Next(100);
                Console.WriteLine("Adding {0}", value);
                integerTree.Add(value);
            }
            Console.WriteLine("Preorder traversal:");
            Console.WriteLine(string.Join(" ", integerTree.Preorder()));
            Console.WriteLine("Postorder traversal:");
            Console.WriteLine(string.Join(" ", integerTree.Postorder()));
            Console.WriteLine("Levelorder traversal:");
            Console.WriteLine(string.Join(" ", integerTree.Levelorder()));
            Console.WriteLine("Inorder traversal:");
            foreach (int value in integerTree)
                Console.Write("{0} ", value);
            Console.WriteLine();
            Console.ReadKey(true);
        }
    }
}
