// <copyright file="ExpressionTreeFactory.cs" company="Adam Nassar 11588762">
// Copyright (c) Adam Nassar 11588762. All rights reserved.
// </copyright>

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Factory class.
    /// </summary>
    public class ExpressionTreeFactory
    {
        public static Dictionary<string, Type> operators = new Dictionary<string, Type>();

        /// <summary>
        /// Creates and returns operater.
        /// </summary>
        /// <param name="op">Key.</param>
        /// <returns>Returns operation.</returns>
        public static BinaryOperator Create(string op)
        {
            switch (op)
            {
                case "+":
                    return new Add();
                case "-":
                    return new Subtract();
                case "*":
                    return new Multiply();
                case "/":
                    return new Divide();
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Checks if index of expression is valid operator.
        /// </summary>
        /// <param name="index">index.</param>
        /// <returns>boolean.</returns>
        public static bool IsOperator(string index)
        {
            switch (index)
            {
                case "+":
                    return true;
                case "-":
                    return true;
                case "*":
                    return true;
                case "/":
                    return true;
                case "(":
                    return true;
                case ")":
                    return true;
                case "%":
                    throw new NotSupportedException();
                case "^":
                    throw new NotSupportedException();
            }

            return false;
        }

        /// <summary>
        /// Gets precedence for op.
        /// </summary>
        /// <param name="op">op.</param>
        /// <returns>precedence.</returns>
        public static int GetPrecedence(string op)
        {
            switch (op)
            {
                case "+":
                    return 7;
                case "-":
                    return 7;
                case "*":
                    return 6;
                case "/":
                    return 6;
                case "(":
                    return 5;
                case ")":
                    return 5;
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets associativity for op.
        /// </summary>
        /// <param name="op">op.</param>
        /// <returns>type of associativity.</returns>
        public static char GetAssociativity(string op)
        {
            switch (op)
            {
                case "+":
                    return 'l';
                case "-":
                    return 'l';
                case "*":
                    return 'r';
                case "/":
                    return 'r';
                case "(":
                    return 'n';
                case ")":
                    return 'n';
            }

            throw new NotSupportedException();
        }
    }
}
