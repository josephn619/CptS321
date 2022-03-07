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
        /// <summary>
        /// Creates and returns operater.
        /// </summary>
        /// <param name="op">Key.</param>
        /// <returns>Returns operation.</returns>
        public static BinaryOperator Create(char op)
        {
            switch (op)
            {
                case '+':
                    return new Add();
                case '-':
                    return new Subtract();
                case '*':
                    return new Multiply();
                case '/':
                    return new Divide();
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Checks if op is valid.
        /// </summary>
        /// <param name="op">op.</param>
        /// <returns>boolean.</returns>
        public static bool IsValid(char op)
        {
            switch (op)
            {
                case '+':
                    return true;
                case '-':
                    return true;
                case '*':
                    return true;
                case '/':
                    return true;
                case '%':
                    throw new NotSupportedException();
                case '^':
                    throw new NotSupportedException();
            }

            return false;
        }
    }
}
