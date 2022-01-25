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
