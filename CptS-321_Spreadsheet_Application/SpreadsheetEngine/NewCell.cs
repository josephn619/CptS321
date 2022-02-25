// <copyright file="NewCell.cs" company="Adam Nassar 11588762">
// Copyright (c) Adam Nassar 11588762. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Inherited class of Cell.
    /// </summary>
    public class NewCell : Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewCell"/> class.
        /// </summary>
        /// <param name="row">row.</param>
        /// <param name="col">col.</param>
        public NewCell(int row, int col)
            : base(row, col)
        {
        }
    }
}
