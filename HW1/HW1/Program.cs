using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BST t = new BST();

            while (true)
            {
                Console.WriteLine("Enter integers with spaces. ");

                string input = Console.ReadLine();

                if (input == "")
                    break;

                foreach (var num in input.Split(' '))
                    t.insert((Convert.ToInt32(num)));

                t.print();
                Console.WriteLine("Total: " + t.Total);
                Console.WriteLine("Level: " + t.calcLevel());
            }
        }
    }
}
