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
    }
}
