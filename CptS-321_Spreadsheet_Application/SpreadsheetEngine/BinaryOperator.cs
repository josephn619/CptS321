﻿// <copyright file="BinaryOperator.cs" company="Adam Nassar 11588762">
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
    public abstract class BinaryOperator : Node
    {
        private char op;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryOperator"/> class.
        /// </summary>
        /// <param name="newOperator">newOperator.</param>
        /// <param name="precedence">precedence.</param>
        public BinaryOperator(char newOperator, int precedence)
        {
            this.op = newOperator;
            this.Precedence = precedence;
            this.Left = null;
            this.Right = null;
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
        /// Gets or sets precedence.
        /// </summary>
        public int Precedence { get; set; }

        /// <summary>
        /// Gets or sets left.
        /// </summary>
        public Node Left { get; set; }

        /// <summary>
        /// Gets or sets right.
        /// </summary>
        public Node Right { get; set; }

        /// <summary>
        /// Abstract Evaluate method.
        /// </summary>
        /// <param name="left">left.</param>
        /// <param name="right">right.</param>
        /// <returns>Evaluated expression.</returns>
        public abstract double Evaluate(double left, double right);
    }
}
