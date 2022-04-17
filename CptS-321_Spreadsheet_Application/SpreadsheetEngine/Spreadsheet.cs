// <copyright file="Spreadsheet.cs" company="Adam Nassar 11588762">
// Copyright (c) Adam Nassar 11588762. All rights reserved.
// </copyright>

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    /// Container class for spreadsheet.
    /// </summary>
    public class Spreadsheet
    {
        private readonly int nRows;
        private readonly int nCols;
        private Cell[,] cells;

        private Stack<Undo_Redo> undoStack = new Stack<Undo_Redo>();
        private Stack<Undo_Redo> redoStack = new Stack<Undo_Redo>();

        private Stack<int> sizeUndo = new Stack<int>();
        private Stack<int> sizeRedo = new Stack<int>();

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

            this.sizeUndo.Push(0);
            this.SizeRedo.Push(0);

            for (int i = 0; i < this.nRows; i++)
            {
                for (int j = 0; j < this.nCols; j++)
                {
                    this.cells[i, j] = new NewCell(i, j)
                    {
                        Text = string.Empty,
                    };

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
        /// Gets SizeUndo stack.
        /// </summary>
        public Stack<int> SizeUndo
        {
            get
            {
                return this.sizeUndo;
            }
        }

        /// <summary>
        /// Gets  sizeRedo stack.
        /// </summary>
        public Stack<int> SizeRedo
        {
            get
            {
                return this.sizeRedo;
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
        /// Pops and returns previous operation.
        /// </summary>
        /// <param name="relevantStack">relevantStack.</param>
        /// <returns>Last operation as node.</returns>
        public Undo_Redo UndoOrRedoPop(Stack<Undo_Redo> relevantStack)
        {
            if (relevantStack.Count > 0)
            {
                Undo_Redo prevOperation = relevantStack.Pop();

                Undo_Redo undoOrRedo = new Undo_Redo(this.cells[prevOperation.GetRow(), prevOperation.GetCol()].CreateCopy(), prevOperation.PropertyChanged);
                prevOperation.Update(ref this.cells[prevOperation.GetRow(), prevOperation.GetCol()]);

                return undoOrRedo;
            }

            return null;
        }

        /// <summary>
        /// Saves spreadsheet to XML.
        /// </summary>
        /// <param name="fs">filestream.</param>
        public void SaveToXML(Stream fs)
        {
            using (XmlWriter outfile = XmlWriter.Create(fs, new XmlWriterSettings()))
            {
                /* Start of Spreadsheet */
                outfile.WriteStartElement("MySpreadsheet");

                foreach (Cell cell in this.cells)
                {
                    if (cell.IsNotEmpty())
                    {
                        int row = cell.RowIndex;
                        char col = (char)(cell.ColIndex + 'A');
                        string name = col.ToString() + (row + 1).ToString();

                        /* Start of Celll */
                        outfile.WriteStartElement("cell", name);

                        /* Start of Color */
                        outfile.WriteStartElement("bgColor");
                        outfile.WriteString(cell.BGColor.ToString());
                        outfile.WriteEndElement();
                        /* End of Color */

                        /* Start of Text */
                        outfile.WriteStartElement("text");
                        outfile.WriteString(cell.Text);
                        outfile.WriteEndElement();
                        /* End of Text */

                        outfile.WriteEndElement();
                        /* End of Cell */
                    }
                }

                outfile.WriteEndElement();
                /* End of Spreadsheet */

                outfile.Close();
            }
        }

        /// <summary>
        /// Loads spreadsheet from XML.
        /// </summary>
        /// <param name="fileStream">fileStream.</param>
        public void LoadFromXML(Stream fileStream)
        {
            string pathname = Path.Combine(AppContext.BaseDirectory, "Spreadsheet.xml");

            // Clear stacks
            this.undoStack.Clear();
            this.redoStack.Clear();

            using (XmlReader infile = XmlReader.Create(fileStream, new XmlReaderSettings()))
            {
                /* Start of Spreadsheet*/
                infile.ReadStartElement("MySpreadsheet");

                while (infile.Name == "cell")
                {
                    /*Start of Cell */
                    string name = infile.NamespaceURI;
                    infile.ReadStartElement("cell");

                    /* Start of Color */
                    infile.ReadStartElement("bgColor");
                    int color = Convert.ToInt32(infile.ReadContentAsString());
                    infile.ReadEndElement();
                    /* End of Color */

                    /* Start of text */
                    infile.ReadStartElement("text");
                    string text = infile.ReadContentAsString();
                    infile.ReadEndElement();
                    /* Start of text */

                    infile.ReadEndElement();
                    /* End of cell */

                    Cell copyCell = this.CreateCell(name, text, color);
                    this.cells[copyCell.RowIndex, copyCell.ColIndex] = this.CopyCell(copyCell);

                    this.CellPropertyChanged?.Invoke(this.cells[copyCell.RowIndex, copyCell.ColIndex], new PropertyChangedEventArgs("Cell"));
                }

                infile.ReadEndElement();
                /* End of Spreadsheet */
            }
        }

        private Cell CreateCell(string name, string text, int bgColor)
        {
            if (int.TryParse(name.Substring(1), out int row))
            {
                int col = (int)name[0] - 'A';

                return new NewCell(row - 1, col)
                {
                    Text = text,
                    BGColor = bgColor,
                };
            }

            return null;
        }

        private Cell CopyCell(Cell source)
        {
            return new NewCell(source.RowIndex, source.ColIndex)
            {
                Text = source.Text,
                BGColor = source.BGColor,
            };
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
