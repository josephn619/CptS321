// <copyright file="Constant.cs" company="Adam Nassar 11588762">
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
    /// Class for constants.
    /// </summary>
    public class Constant : Node
    {
        private double value;

        /// <summary>
        /// Gets or sets value.
        /// </summary>
        public double Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
            }
        }
    }
}
