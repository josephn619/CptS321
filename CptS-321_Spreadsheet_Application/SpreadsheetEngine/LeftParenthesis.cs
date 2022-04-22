// <copyright file="LeftParenthesis.cs" company="Adam Nassar 11588762">
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
    /// Class for Add.
    /// </summary>
    public class LeftParenthesis : BinaryOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeftParenthesis"/> class.
        /// </summary>
        public LeftParenthesis()
            : base("(", 5, 'n')
        {
        }

        /// <summary>
        /// Evaluates expression through addition.
        /// </summary>
        /// <param name="left">left.</param>
        /// <param name="right">right.</param>
        /// <returns>Evaluated expression.</returns>
        public override double Evaluate(double left, double right)
        {
            return left + right;
        }
    }
}
