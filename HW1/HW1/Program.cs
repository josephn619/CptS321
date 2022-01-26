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
            // ->"tree" is not acting as a reference for the root<- **RESOLVED**

            // Initializes t as new BST
            BST t = new BST();

            while (true)
            {
                // Clears console
                Console.Clear();

                Console.WriteLine("Enter integers with spaces. ");

                // #1
                string input = Console.ReadLine();

                // string input = "1 2 3 4 5 6 7 8 9 10";

                // Exit condition
                if (input == "")
                    break;

                // #2
                // Parse string into integers and insert into BST
                foreach (var num in input.Split(' '))
                    if (Convert.ToInt32(num) < 100)
                        t.insert((Convert.ToInt32(num)));

                // #3
                t.print();
                // #4.1
                Console.WriteLine("Total: " + t.count());
                // #4.2
                Console.WriteLine("Level: " + t.calcLevel());
                // #4.3
                Console.WriteLine("Min Levels: " + t.getMinLevel());

                // Waits for user input to continue
                Console.ReadKey();
            }
        }
    }
}
