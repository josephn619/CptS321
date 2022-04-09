// <copyright file="Spreadsheet.cs" company="Adam Nassar 11588762">
// Copyright (c) Adam Nassar 11588762. All rights reserved.
// </copyright>

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// Container class for spreadsheet.
    /// </summary>
    public class Spreadsheet
    {
        private readonly int nRows;
        private readonly int nCols;
        private Cell[,] cells;

        private Stack<Undo_Redo> undoStack;
        private Stack<Undo_Redo> redoStack;

        private int sizeUndo;
        private int sizeRedo;

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

            this.undoStack = new Stack<Undo_Redo>();
            this.redoStack = new Stack<Undo_Redo>();

            this.sizeUndo = 0;
            this.sizeRedo = 0;

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
        /// Gets or sets undo stack.
        /// </summary>
        public Stack<Undo_Redo> UndoStack
        {
            get
            {
                return this.undoStack;
            }

            set
            {
                this.undoStack = value;
            }
        }

        /// <summary>
        /// Gets or sets redo stack.
        /// </summary>
        public Stack<Undo_Redo> RedoStack
        {
            get
            {
                return this.redoStack;
            }

            set
            {
                this.redoStack = value;
            }
        }

        /// <summary>
        /// Gets or sets for total number of undos.
        /// </summary>
        public int SizeUndo
        {
            get
            {
                return this.sizeUndo;
            }

            set
            {
                this.sizeUndo = value;
            }
        }

        /// <summary>
        /// Gets or sets for total number of redos.
        /// </summary>
        public int SizeRedo
        {
            get
            {
                return this.sizeRedo;
            }

            set
            {
                this.sizeRedo = value;
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
                int col = Convert.ToInt32(name[0]) - 'A';
                return this.GetCell(row - 1, col);
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

        /// <summary>
        /// Returns previous operation.
        /// </summary>
        /// <returns>Last node.</returns>
        public Undo_Redo Undo()
        {
            if (this.undoStack.Count > 0)
            {
                Undo_Redo prevOperation = this.undoStack.Pop();

                Undo_Redo undo = new Undo_Redo(this.cells[prevOperation.GetRow(), prevOperation.GetCol()].CreateCopy(), prevOperation.PropertyChanged);
                prevOperation.Update(ref this.cells[prevOperation.GetRow(), prevOperation.GetCol()]);

                return undo;
            }

            return null;
        }

        /// <summary>
        /// Returns previous operation.
        /// </summary>
        /// <returns>Last node.</returns>
        public Undo_Redo Redo()
        {
            if (this.redoStack.Count > 0)
            {
                Undo_Redo prevOperation = this.redoStack.Pop();

                Undo_Redo redo = new Undo_Redo(this.cells[prevOperation.GetRow(), prevOperation.GetCol()].CreateCopy(), prevOperation.PropertyChanged);
                prevOperation.Update(ref this.cells[prevOperation.GetRow(), prevOperation.GetCol()]);

                return redo;
            }

            return null;
        }

        private List<string> GetVariables(Cell senderCell, string expression)
        {
            // Gets list of operators, numbers, and variables
            List<string> names = senderCell.ExpTree.GetExprList(expression);

            // Filters out operators
            names = names.Where(s => !ExpressionTreeFactory.IsOperator(s)).ToList();

            // Filters out variables
            names = names.Where(s => !double.TryParse(s, out double num)).ToList();

            return names;
        }

        private void SubOrUnsubToNames(Cell senderCell, string expression, bool subscribe)
        {
            List<string> names = this.GetVariables(senderCell, expression);

            foreach (string variable in names)
            {
                if (subscribe)
                {
                    senderCell.ExpTree.SetVariable(variable, this.GetValue(variable));
                }

                Cell refCell = this.GetCell(variable);

                if (refCell != null)
                {
                    // Sub or unsub
                    if (subscribe)
                    {
                        refCell.PropertyChanged += senderCell.CellPropertyChanged;
                    }
                    else
                    {
                        refCell.PropertyChanged -= senderCell.CellPropertyChanged;
                    }
                }
            }
        }

        private void Spreadsheet_CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell senderCell = (Cell)sender;

            int row = senderCell.RowIndex;
            int col = senderCell.ColIndex;

            if (e.PropertyName == "Text")
            {
                if (senderCell.Text.StartsWith("="))
                {
                    // Cell already has expression (unsubscribe)
                    if (senderCell.ExpTree.Expression != string.Empty)
                    {
                        this.SubOrUnsubToNames(senderCell, senderCell.ExpTree.Expression, false);
                    }

                    this.SubOrUnsubToNames(senderCell, senderCell.ExpTree.Expression = senderCell.Text.Substring(1), true);

                    senderCell.Val = this.cells[row, col].Val = senderCell.ExpTree.Evaluate().ToString();
                }
                else
                {
                    senderCell.Val = senderCell.Text;
                }

                this.CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("RefreshVal"));
            }
            else if (e.PropertyName == "BgColor")
            {
                this.CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("RefreshBgColor"));
            }
        }
    }
}
