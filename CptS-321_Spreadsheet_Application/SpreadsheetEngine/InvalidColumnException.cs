// <copyright file="InvalidColumnException.cs" company="Adam Nassar 11588762">
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
    public class InvalidColumnException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidColumnException"/> class.
        /// </summary>
        public InvalidColumnException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidColumnException"/> class.
        /// </summary>
        /// <param name="message">message.</param>
        public InvalidColumnException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidColumnException"/> class.
        /// </summary>
        /// <param name="message">message.</param>
        /// <param name="inner">inner.</param>
        public InvalidColumnException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
