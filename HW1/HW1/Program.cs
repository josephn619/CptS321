using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    public class Program
    {
        static void Main(string[] args)
        {
            // The problem for this cohesive approach seems to be that root is being passed in through var "tree"
            // but is not being updated (in Cpp pointers would have been involved to ensure it was updated).
            // ->"tree" is not acting as a reference for the root<-

            BST t = new BST();

            while (true)
            {
                Console.WriteLine("Enter integers with spaces. ");

                string input = Console.ReadLine();

                // Exit condition
                if (input == "")
                    break;

                // Parse string into integers and insert into BST
                foreach (var num in input.Split(' '))
                    t.insert((Convert.ToInt32(num)));

                t.print();
                Console.WriteLine("Total: " + t.Total);
                Console.WriteLine("Level: " + t.calcLevel());
            }
        }
    }
}
