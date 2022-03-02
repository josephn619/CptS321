// <copyright file="Variable.cs" company="Adam Nassar 11588762">
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
    /// Class for variables.
    /// </summary>
    public class Variable : Node
    {
        private string var;

        /// <summary>
        /// Gets or sets var.
        /// </summary>
        public string Var
        {
            get
            {
                return this.var;
            }

            set
            {
                this.var = value;
            }
        }
    }
}
