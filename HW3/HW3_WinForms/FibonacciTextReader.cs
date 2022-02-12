// <copyright file="FibonacciTextReader.cs" company="Adam Nassar 11588762">
// Copyright (c) Adam Nassar 11588762. All rights reserved.
// </copyright>

namespace HW3_WinForms
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Numerics;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for getting numbers in fibonacci sequence.
    /// </summary>
    public class FibonacciTextReader : TextReader
    {
        private int numLines;
        private int index;
        private BigInteger first;
        private BigInteger second;

        /// <summary>
        /// Initializes a new instance of the <see cref="FibonacciTextReader"/> class.
        /// </summary>
        /// <param name="maxNumLines">maxNumLines.</param>
        public FibonacciTextReader(int maxNumLines)
        {
            this.numLines = maxNumLines;
            this.index = 0;
            this.first = 0;
            this.second = 0;
        }

        /// <summary>
        /// Generates next num in fibonacci sequence.
        /// </summary>
        /// <returns>next num as a string.</returns>
        public override string ReadLine()
        {
            BigInteger cur;
            if (this.index == 0)
            {
                this.index++;
                return "0";
            }
            else if (this.index == 1)
            {
                this.index++;
                return "1";
            }
            else
            {
                // no need to increment index anymore
                cur = this.first + this.second;
                this.first = this.second;
                this.second = cur;
                return cur.ToString();
            }
        }

        /// <summary>
        /// Generates fibonacci sequence to maxNumLines.
        /// </summary>
        /// <returns>entire fibonacci sequence.</returns>
        public override string ReadToEnd()
        {
            return " ";
        }

    }
}
