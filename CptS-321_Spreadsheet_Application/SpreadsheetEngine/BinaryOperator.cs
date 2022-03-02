// <copyright file="BinaryOperator.cs" company="Adam Nassar 11588762">
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
    /// Class for binary operators.
    /// </summary>
    public class BinaryOperator : Node
    {
        private char op;
        private Node left;
        private Node right;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryOperator"/> class.
        /// </summary>
        /// <param name="newOperator">newOperator.</param>
        public BinaryOperator(char newOperator)
        {
            this.op = newOperator;
            this.left = null;
            this.right = null;
        }

        /// <summary>
        /// Gets or sets op.
        /// </summary>
        public char Op
        {
            get
            {
                return this.op;
            }

            set
            {
                this.op = value;
            }
        }

        /// <summary>
        /// Gets or sets left.
        /// </summary>
        public Node Left { get; set; }

        /// <summary>
        /// Gets or sets right.
        /// </summary>
        public Node Right { get; set; }
    }
}
