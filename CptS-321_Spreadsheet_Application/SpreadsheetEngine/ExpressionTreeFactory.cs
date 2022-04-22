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
            }

            return false;
        }

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
            }

            throw new NotSupportedException();
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
                    return new Add().Precedence;
                case "-":
                    return new Subtract().Precedence;
                case "*":
                    return new Multiply().Precedence;
                case "/":
                    return new Divide().Precedence;
                case "(":
                    return new LeftParenthesis().Precedence;
                case ")":
                    return new RightParenthesis().Precedence;
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
                    return new Add().Associativity;
                case "-":
                    return new Subtract().Associativity;
                case "*":
                    return new Multiply().Associativity;
                case "/":
                    return new Divide().Associativity;
                case "(":
                    return new LeftParenthesis().Associativity;
                case ")":
                    return new RightParenthesis().Associativity;
            }

            throw new NotSupportedException();
        }
    }
}
