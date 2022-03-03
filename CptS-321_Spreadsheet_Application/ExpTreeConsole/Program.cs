﻿// <copyright file="Program.cs" company="Adam Nassar 11588762">
// Copyright (c) Adam Nassar 11588762. All rights reserved.
// </copyright>

namespace ExpTreeConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Cpts321;

    /// <summary>
    /// Expression Tree console app.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main driver for console app.
        /// </summary>
        /// <param name="args">args.</param>
        public static void Main(string[] args)
        {
            ExpressionTree mainExpTree = new ExpressionTree(string.Empty);

            while (true)
            {
                int option = 0;
                do
                {
                    Console.WriteLine("Menu: Current Expression = " + mainExpTree.Expression + "\n1. Enter a new expression. \n2. Set a variable value. \n3. Evaluate Tree. \n4. Quit. \nEnter Option: ");
                    try
                    {
                        string response = Console.ReadLine();
                        option = int.Parse(response);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                while (option < 1 || option > 4);

                switch (option)
                {
                    case 1:
                        Console.WriteLine("Enter a new expression.");
                        string expression = Console.ReadLine();
                        mainExpTree.Expression = expression;
                        break;
                    case 2:
                        Console.WriteLine("Enter name of variable.");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter value of variable.");
                        string value = Console.ReadLine();
                        mainExpTree.SetVariable(name, int.Parse(value));
                        break;
                    case 3:
                        // Console.WriteLine("Result: " + mainExpTree.Evaluate());
                        break;
                    case 4:
                        Environment.Exit(1);
                        break;
                }
            }
        }
    }
}
