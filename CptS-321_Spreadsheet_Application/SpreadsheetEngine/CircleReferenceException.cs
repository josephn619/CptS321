// <copyright file="CircleReferenceException.cs" company="Adam Nassar 11588762">
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
    public class CircleReferenceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CircleReferenceException"/> class.
        /// </summary>
        public CircleReferenceException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircleReferenceException"/> class.
        /// </summary>
        /// <param name="message">message.</param>
        public CircleReferenceException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircleReferenceException"/> class.
        /// </summary>
        /// <param name="message">message.</param>
        /// <param name="inner">inner.</param>
        public CircleReferenceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
