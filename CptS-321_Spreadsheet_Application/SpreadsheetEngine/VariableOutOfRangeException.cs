// <copyright file="VariableOutOfRangeException.cs" company="Adam Nassar 11588762">
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
    public class VariableOutOfRangeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VariableOutOfRangeException"/> class.
        /// </summary>
        public VariableOutOfRangeException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableOutOfRangeException"/> class.
        /// </summary>
        /// <param name="message">message.</param>
        public VariableOutOfRangeException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableOutOfRangeException"/> class.
        /// </summary>
        /// <param name="message">message.</param>
        /// <param name="inner">inner.</param>
        public VariableOutOfRangeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
