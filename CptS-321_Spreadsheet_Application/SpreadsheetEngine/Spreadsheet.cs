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

        private void Spreadsheet_CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell cell = (Cell)sender;
            int row = cell.RowIndex;
            int col = cell.ColIndex;

            if (e.PropertyName == "Text")
            {
                if (cell.Text[0] == '=')
                {
                    // Case where cell already has expression
                    if (cell.ExpTree.Expression != string.Empty)
                    {
                        List<string> oldNames = cell.ExpTree.GetExprList(cell.ExpTree.Expression);

                        // Filter operators
                        oldNames = oldNames.Where(s => !ExpressionTreeFactory.IsOperator(s)).ToList();

                        // Filters numbers
                        oldNames = oldNames.Where(s => !double.TryParse(s, out double num)).ToList();

                        foreach (string name in oldNames)
                        {
                            Cell refCell = this.GetCell(name);

                            // Unsubscribe
                            if (refCell != null)
                            {
                                refCell.PropertyChanged -= cell.CellPropertyChanged;
                            }
                        }
                    }

                    string formula = cell.Text.Substring(1);

                    cell.ExpTree.Expression = formula;

                    List<string> names = cell.ExpTree.GetExprList(formula);

                    // Filter operators
                    names = names.Where(s => !ExpressionTreeFactory.IsOperator(s)).ToList();

                    // Filters numbers
                    names = names.Where(s => !double.TryParse(s, out double num)).ToList();

                    foreach (string variable in names)
                    {
                        cell.ExpTree.SetVariable(variable, this.GetValue(variable));

                        Cell refCell = this.GetCell(variable);

                        // Subscribe
                        if (refCell != null)
                        {
                            refCell.PropertyChanged += cell.CellPropertyChanged;
                        }
                    }

                    this.cells[row, col].Val = cell.ExpTree.Evaluate().ToString();
                    cell.Val = this.cells[row, col].Val;
                }
                else
                {
                    cell.Val = cell.Text;
                }
            }

            this.CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Refresh"));
        }
    }
}
