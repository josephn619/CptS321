using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    internal class BST
    {
        private Node root;
        private int total;

        public BST()
        {
            this.root = null;
            this.total = 0;
        }

        public int Total { get { return total; } set { this.total = value; } }

        public void insert(int val)
        {
            if (this.root != null)
            {
                this.root.insert(val);
            }
            else
            {
                this.root = new Node(val);
            }

            this.Total += 1;
        }

        public int calcLevel()
        {
            return (int)Math.Floor(Math.Log(this.total, 2) + 1);
        }

        public void print()
        {
            if (this.root != null)
                this.root.print();
        }
    }
}
