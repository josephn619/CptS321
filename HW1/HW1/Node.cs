using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    internal class Node
    {
        private int data;
        private Node left;
        private Node right;

        public Node(int val)
        {
            this.data = val;
        }

        public int Data { get { return this.data; } }

        // public Node Left { get { return this.left; } set { this.left = value; } }

        // public Node Right { get { return this.right; } set { this.right = value; } }

        public void insert(int val)
        {
            if (val >= this.data)
            {
                if (this.right == null)
                    this.right = new Node(val);
                else
                    this.right.insert(val);
            }
            else
            {
                if (this.left == null)
                    this.left = new Node(val);
                else
                    this.left.insert(val);
            }
        }

        public void print()
        {
            if (this.left != null)
                this.left.print();
            Console.WriteLine(data + " ");
            if (this.right != null)
                this.right.print();
        }
    }
}
