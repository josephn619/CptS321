// <copyright file="Cell.cs" company="Adam Nassar 11588762">
// Copyright (c) Adam Nassar 11588762. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Cell class for spreadsheet application.
    /// </summary>
    public abstract class Cell
    {
        private readonly int rowIndex;
        private readonly int colIndex;
        private string text;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="row">row.</param>
        /// <param name="col">col.</param>
        public Cell(int row, int col)
        {
            this.rowIndex = row;
            this.colIndex = col;
        }

        /// <summary>
        /// Gets rowIndex.
        /// </summary>
        public int RowIndex
        {
            get { return this.rowIndex; }
        }

        /// <summary>
        /// Gets colIndex.
        /// </summary>
        public int ColIndex
        {
            get { return this.colIndex; }
        }

        /// <summary>
        /// Gets text.
        /// </summary>
        public string Text
        {
            get { return this.text; }
        }
    }
}
