// <copyright file="Spreadsheet.cs" company="Adam Nassar 11588762">
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
    /// Container class for spreadsheet.
    /// </summary>
    public class Spreadsheet
    {
        private int nRows;
        private int nCols;
        private Cell[,] cells;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="rows">rows.</param>
        /// <param name="cols">cols.</param>
        public Spreadsheet(int rows, int cols)
        {
            this.nRows = rows;
            this.nCols = cols;
            this.cells = new Cell[this.nRows, this.nCols];

            for (int i = 0; i < this.nRows; i++)
            {
                for (int j = 0; j < this.nCols; j++)
                {
                    this.cells[i, j] = new NewCell(i, j);
                    this.cells[i, j].PropertyChanged += this.Spreadsheet_CellPropertyChanged;
                }
            }
        }

        /// <summary>
        /// Event for cell value changing.
        /// </summary>
        public event PropertyChangedEventHandler CellPropertyChanged;

        /// <summary>
        /// Gets number rows.
        /// </summary>
        public int RowCount
        {
            get
            {
                return this.nRows;
            }
        }

        /// <summary>
        /// Gets number cols.
        /// </summary>
        public int ColCount
        {
            get
            {
                return this.nCols;
            }
        }

        /// <summary>
        /// Gets cell at specified location.
        /// </summary>
        /// <param name="row">row.</param>
        /// <param name="col">col.</param>
        /// <returns>Cell.</returns>
        public Cell GetCell(int row, int col)
        {
            if ((Cell)this.cells[row, col] != null)
            {
                return (Cell)this.cells[row, col];
            }

            return null;
        }

        private void Spreadsheet_CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                if (((Cell)sender).Text[0] == '=')
                {
                    int col = ((Cell)sender).Text[1] - 65;
                    int row = Convert.ToInt32(((Cell)sender).Text.Substring(2)) - 1;
                    ((Cell)sender).Val = this.cells[row, col].Val;
                }
                else
                {
                    ((Cell)sender).Val = ((Cell)sender).Text;
                }
            }

            this.CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(e.PropertyName));
        }
    }
}
