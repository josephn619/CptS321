using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    // Class definition for BST
    public class BST
    {
        // Member variables
        private Node root;
        private int total;
        private int calcTotal;
        private int levels;

        // BST constructor
        public BST()
        {
            this.root = null;
            this.total = 0;
            this.calcTotal = 0;
            this.levels = 0;
        }

        // total getter and setter
        public int Total { get { return this.total; } set { this.total = value; } }

        // calcTotal getter and setter
        public int CalcTotal { get { return this.calcTotal; } set { this.calcTotal = value; } }

        // levels getter and setter
        public int Levels { get { return this.levels; } set { this.levels = value; } }

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

        // Finds if val in BST
        public bool Find(int val)
        {
            Node cur = this.root;
            while (cur != null)
                if (cur.Data == val)
                    return true;
                else if (cur.Data < val)
                    cur = cur.Right;
                else
                    cur = cur.Left;
            return false;
        }

        // calls private method insert()
        public void insert(int val)
        {
            if (!this.Find(val))
            {
                this.Total += 1;

                this.root = this.insert(this.root, val);
            }
        }

        // inserts val into BST
        private Node insert(Node tree, int val)
        {
            if (tree != null)
                if (val >= tree.Data)
                    if (tree.Right == null)
                        tree.Right = new Node(val);
                    else
                        this.insert(tree.Right, val);
                else
                    if (tree.Left == null)
                        tree.Left = new Node(val);
                    else
                        this.insert(tree.Left, val);
            else
                tree = new Node(val);

            return tree;
        }

        // Calculate how many levels in BST ***Needs Revising*** 1 2 3 should be 3 levels not 2
        public int getMinLevel()
        {
            // Takes log_2 of total nodes rounds down
            if (this.total != 0)
                return (int)Math.Floor(Math.Log(this.total, 2) + 1);
            return 0;
        }

        // Calls private method calcLevel()
        public int calcLevel()
        {
            return this.calcLevel(this.root);
        }

        // Calculates number of levels
        public int calcLevel(Node tree)
        {
            int leftHeight = 0, rightHeight = 0;
            if (this.root == null)
                return 0;
            if (tree.Left != null)
                leftHeight = this.calcLevel(tree.Left);
            if (tree.Right != null)
                rightHeight = this.calcLevel(tree.Right);

            return Math.Max(leftHeight, rightHeight) + 1;
        }

        // Public print function that calls private method
        public void print() => this.print(this.root);

        // Print BST
        private void print(Node tree)
        {
            if (tree != null)
            {
                if (tree.Left != null)
                    this.print(tree.Left);
                Console.WriteLine("Data: " + tree.Data);
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

        // Public count function that calls private method
        public string count() 
        {
            this.CalcTotal = 0;
            return this.count(this.root); 
        }

        // Counts number of nodes in BST
        private string count(Node tree)
        {
            if (tree != null)
            {
                if (tree.Left != null)
                    this.count(tree.Left);
                this.CalcTotal += 1;
                if (tree.Right != null)
                    this.count(tree.Right);
            }

            // Check if total is correct with actual number
            if (this.CalcTotal == this.Total)
                return this.CalcTotal.ToString();
            return "";
        }
    }
}
