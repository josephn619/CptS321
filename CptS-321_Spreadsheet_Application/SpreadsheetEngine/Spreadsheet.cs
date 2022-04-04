// <copyright file="Spreadsheet.cs" company="Adam Nassar 11588762">
// Copyright (c) Adam Nassar 11588762. All rights reserved.
// </copyright>

namespace Cpts321
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
        private readonly int nRows;
        private readonly int nCols;
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
                    this.cells[i, j].Text = string.Empty;

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

        /// <summary>
        /// Gets cell from specified name.
        /// </summary>
        /// <param name="name">row.</param>
        /// <returns>Cell.</returns>
        public Cell GetCell(string name)
        {
            if (int.TryParse(name.Substring(1), out int row))
            {
                int col = Convert.ToInt32(name[0]) -'A';
                return this.GetCell(row- 1, col);
            }

            return null;
        }


        /// <summary>
        /// Gets value given variableName.
        /// </summary>
        /// <param name="variableName">variableName.</param>
        /// <returns>value.</returns>
        public double GetValue(string variableName)
        {
            int col = Convert.ToInt32(variableName[0]) - 'A';
            int row = Convert.ToInt32(variableName.Substring(1)) - 1;

            double.TryParse(this.cells[row, col].Val, out double num);

            return num;
        }

        private void SubOrUnsubToNames(Cell cell, string expression, bool subscribe)
        {
            List<string> names = cell.ExpTree.GetExprList(expression);

            // Filter operators
            names = names.Where(s => !ExpressionTreeFactory.IsOperator(s)).ToList();

            // Filters numbers
            names = names.Where(s => !double.TryParse(s, out double num)).ToList();

            foreach (string variable in names)
            {
                if (subscribe)
                {
                    cell.ExpTree.SetVariable(variable, this.GetValue(variable));
                }

                Cell refCell = this.GetCell(variable);

                if (refCell != null)
                {
                    // Sub or unsub
                    if (subscribe)
                    {
                        refCell.PropertyChanged += cell.CellPropertyChanged;
                    }
                    else
                    {
                        refCell.PropertyChanged -= cell.CellPropertyChanged;
                    }
                }
            }
        }

        private void Spreadsheet_CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                Cell senderCell = (Cell)sender;

                if (senderCell.Text.StartsWith("="))
                {
                    int row = senderCell.RowIndex;
                    int col = senderCell.ColIndex;

                    // Cell already has expression (unsubscribe)
                    if (senderCell.ExpTree.Expression != string.Empty)
                    {
                        this.SubOrUnsubToNames(senderCell, senderCell.ExpTree.Expression, false);
                    }

                    senderCell.ExpTree.Expression = senderCell.Text.Substring(1);
                    this.SubOrUnsubToNames(senderCell, senderCell.ExpTree.Expression, true);

                    this.cells[row, col].Val = senderCell.ExpTree.Evaluate().ToString();
                    senderCell.Val = this.cells[row, col].Val = senderCell.ExpTree.Evaluate().ToString();
                }
                else
                {
                    senderCell.Val = senderCell.Text;
                }
            }

            this.CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Refresh"));
        }
    }
}
