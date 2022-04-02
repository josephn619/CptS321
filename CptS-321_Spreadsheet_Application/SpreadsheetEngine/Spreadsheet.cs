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
        /// Gets value given variableName.
        /// </summary>
        /// <param name="variableName">variableName.</param>
        /// <returns>value.</returns>
        public string GetValue(string variableName)
        {
            int col = Convert.ToInt32(variableName[1]) - 'A';
            int row = Convert.ToInt32(variableName.Substring(2)) - 1;

            return this.cells[row, col].Val;
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
                    string formula = cell.Text.Substring(0);

                    cell.ExpTree.Expression = formula;

                    List<string> names = cell.ExpTree.GetVariableNames();

                    foreach (string variable in names)
                    {
                        string value_as_string = this.GetValue(variable);

                        if (double.TryParse(value_as_string, out double value))
                        {
                            cell.ExpTree.SetVariable(variable, value);
                            this.cells[row, col].Val = cell.ExpTree.Evaluate().ToString();
                        }
                        else
                        {
                            this.cells[row, col].Val = value_as_string;
                        }

                        cell.ExpTree.SetVariable(variable, value);
                    }
                }
                else
                {
                    ((Cell)sender).Val = ((Cell)sender).Text;
                }
            }

            this.CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Refresh"));
        }
    }
}
