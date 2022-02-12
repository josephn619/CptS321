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
            this.second = 1;
        }

        /// <summary>
        /// Generates next num in fibonacci sequence.
        /// </summary>
        /// <returns>next num as a string.</returns>
        public override string ReadLine()
        {
            BigInteger cur;

            // Base Case index = 0
            if (this.index == 0)
            {
                this.index++;
                return "0";
            }

            // Base Case index = 1
            else if (this.index == 1)
            {
                this.index++;
                return "1";
            }

            // Computes cur through prior 2 numbers
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
            StringBuilder fibSequence = new StringBuilder();

            // Out of range error checking
            if (this.numLines < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            // Input size is 0
            else if (this.numLines == 0)
            {
                fibSequence.Append(" ");
            }
            else
            {
                for (int i = 0; i < this.numLines; i++)
                {
                    fibSequence.Append(i + 1).Append(": ").AppendLine(this.ReadLine());
                }

                // String Builder to String
                return fibSequence.ToString();
            }

            return null;
        }
    }
}
