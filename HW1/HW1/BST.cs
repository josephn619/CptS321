using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    public class BST
    {
        private Node root;
        private int total;

        // BST constructor
        public BST()
        {
            this.root = null;
            this.total = 0;
        }

        // Total getter and setter
        public int Total { get { return total; } set { this.total = value; } }

        // Insert value into BST
        //public void insert(int val)
        //{
        //    if (this.root != null)
        //    {
        //        this.root.insert(val);
        //    }
        //    else
        //    {
        //        this.root = new Node(val);
        //    }

        //    this.Total += 1;
        //}

        // calls private method insert
        public void insert(int val)
        {
            this.Total += 1;

            this.root = this.insert(this.root, val);
        }

        // inserts val into BST
        private Node insert(Node tree, int val)
        {
            if (tree != null)
            {
                if (val >= tree.Data)
                {
                    if (tree.Right == null)
                        tree.Right = new Node(val);
                    else
                        this.insert(tree.Right, val);
                }
                else
                {
                    if (tree.Left == null)
                        tree.Left = new Node(val);
                    else
                        this.insert(tree, val);
                }
            }
            else
            {
                tree = new Node(val);
            }

            return tree;
        }

        // Calculate how many levels in BST ***Needs Revising*** 1 2 3 should be 3 levels not 2
        public int calcLevel()
        {
            // Takes log_2 of total nodes rounds down
            if (this.total != 0)
                return (int)Math.Floor(Math.Log(this.total, 2) + 1);
            return 0;
        }

        public void print() => this.print(this.root);

        // Print BST
        private void print(Node tree)
        {
            if (tree != null)
            {
                if (tree.Left != null)
                    this.print(tree.Left);
                Console.WriteLine(tree.Data + " ");
                if (tree.Right != null)
                    this.print(tree.Right);
            }
        }

        //private void print(Node tree)
        //{
        //    if (tree != null)
        //    {
        //        if (this.left != null)
        //            this.left.print();
        //        Console.WriteLine(data + " ");
        //        if (this.right != null)
        //            this.right.print();
        //    }
        //}
    }
}
