// <copyright file="Subtract.cs" company="Adam Nassar 11588762">
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
    /// Class for Subtract.
    /// </summary>
    public class Subtract : BinaryOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Subtract"/> class.
        /// </summary>
        public Subtract()
            : base("-", 7, 'l')
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
            return left - right;
        }
    }
}
