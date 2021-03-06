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

                    this.cells[i, j].PropertyChanged += this.CellPropertyChanged;
                }
            }
        }

        /// <summary>
        /// Event for cell value changing.
        /// </summary>
        public event PropertyChangedEventHandler SpreadsheetChanged;

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
        /// Gets sizeUndo stack.
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
        /// Gets name given a cell.
        /// </summary>
        /// <param name="senderCell">senderCell.</param>
        /// <returns>cell name.</returns>
        public string GetName(Cell senderCell)
        {
            char col = (char)(senderCell.ColIndex + 'A');
            int row = senderCell.RowIndex + 1;

            return col.ToString() + row.ToString();
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
                        /* Start of Celll */
                        outfile.WriteStartElement("Cell", this.GetName(cell));

                        /* Start of Val */
                        outfile.WriteStartElement("Val");
                        outfile.WriteString(cell.Val);
                        outfile.WriteEndElement();
                        /* End of Val*/

                        /* Start of Text */
                        outfile.WriteStartElement("Text");
                        outfile.WriteString(cell.Text);
                        outfile.WriteEndElement();
                        /* End of Text */

                        /* Start of Color */
                        outfile.WriteStartElement("bgColor");
                        outfile.WriteString(cell.BGColor.ToString());
                        outfile.WriteEndElement();
                        /* End of Color */

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

                while (infile.Name == "Cell")
                {
                    /*Start of Cell */
                    string name = infile.NamespaceURI;
                    infile.ReadStartElement("Cell");

                    /* Start of Val */
                    infile.ReadStartElement("Val");
                    string val = infile.ReadContentAsString();
                    infile.ReadEndElement();
                    /* End of Val */

                    /* Start of Text */
                    infile.ReadStartElement("Text");
                    string text = infile.ReadContentAsString();
                    infile.ReadEndElement();
                    /* Start of Text */

                    /* Start of Color */
                    infile.ReadStartElement("bgColor");
                    int color = Convert.ToInt32(infile.ReadContentAsString());
                    infile.ReadEndElement();
                    /* End of Color */

                    infile.ReadEndElement();
                    /* End of Cell */

                    Cell copyCell = this.CreateCell(name, val, text, color);

                    // Assigns spreadsheet cell to newly created cell and then subscribes to spreadsheet cell
                    this.cells[copyCell.RowIndex, copyCell.ColIndex] = this.CopyCell(copyCell);
                    this.cells[copyCell.RowIndex, copyCell.ColIndex].PropertyChanged += this.CellPropertyChanged;

                    this.SpreadsheetChanged?.Invoke(this.cells[copyCell.RowIndex, copyCell.ColIndex], new PropertyChangedEventArgs("Cell"));
                }

                infile.ReadEndElement();
                /* End of Spreadsheet */

                infile.Close();
            }
        }

        // Creates cell given all the components
        private Cell CreateCell(string name,  string val, string text, int bgColor)
        {
            if (int.TryParse(name.Substring(1), out int row))
            {
                int col = Convert.ToInt32(name[0]) - 'A';

                return new NewCell(row - 1, col)
                {
                    Val = val,
                    Text = text,
                    BGColor = bgColor,
                };
            }

            return null;
        }

        // Copies cell given the source cell
        private Cell CopyCell(Cell source)
        {
            return new NewCell(source.RowIndex, source.ColIndex)
            {
                Val = source.Val,
                Text = source.Text,
                BGColor = source.BGColor,
            };
        }

        // Gets variable names in the expression
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

        // Detects if any cell in the senderCell expression references the senderCell down the line (by looking at their expressions)
        private bool DetectCircularReference(Cell senderCell, string senderName)
        {
            // Check if the current cell has no expression
            if (senderCell.ExpTree.Expression == string.Empty)
            {
                return true;
            }
            else
            {
                List<string> names = this.GetVariables(senderCell, senderCell.ExpTree.Expression);

                // Go through each variable in the current expression, and then do the same for that variables expression
                foreach (string name in names)
                {
                    if (name == senderName)
                    {
                        throw new CircleReferenceException("Cell references a cell that references it.");
                    }
                    else
                    {
                        return this.DetectCircularReference(this.GetCell(name), senderName);
                    }
                }

                return false;
            }
        }

        // Verifies if variables are a valid entry
        private void VerifyVariables(Cell senderCell, List<string> varList)
        {
            foreach (string name in varList)
            {
                if (!int.TryParse(name[1].ToString(), out int first))
                {
                    // Expected row slot is not an integer
                    throw new InvalidColumnException("Column is not a letter.");
                }
                else if (name.Substring(1).Length > 2 || first > 5)
                {
                    // More than 2 numbers or greater than 50
                    throw new VariableOutOfRangeException("Row is greater than 50.");
                }
                else if ((int)name[0] - 'A' == senderCell.ColIndex && Convert.ToInt32(name[1]) - '1' == senderCell.RowIndex)
                {
                    // Cell references itself
                    throw new SelfReferenceException("Equation references sender cell.");
                }
            }

            // Easier to make a new function rather than define entire thing here because in the recusrive calls it would have to do the entire loop above as well
            this.DetectCircularReference(senderCell, this.GetName(senderCell));
        }

        // Subscribes or Unsubscribes to a single variable (subscribe is either true or false)
        private void SubOrUnsubToSingleVariable(Cell senderCell, string variable, bool subscribe)
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
                    refCell.PropertyChanged += senderCell.RHSPropertyChanged;
                }
                else
                {
                    refCell.PropertyChanged -= senderCell.RHSPropertyChanged;
                }
            }
        }

        // Subscribes or Unsubscribes to given names in the expression, if there is an error, it updates the message
        private void SubOrUnsubToAllVariables(Cell senderCell, string expression, bool subscribe, ref string message)
        {
            List<string> names = this.GetVariables(senderCell, expression);

            try
            {
                // Checks if names violate any rules
                this.VerifyVariables(senderCell, names);

                // Names are valid if we reach here
                foreach (string variable in names)
                {
                    this.SubOrUnsubToSingleVariable(senderCell, variable, subscribe);
                }

                message = string.Empty;
            }
            catch (SelfReferenceException)
            {
                message = "self reference!";
            }
            catch (CircleReferenceException)
            {
                message = "circle reference";
            }
            catch
            {
                // No specified exception because InvalidColumn and VariableOutOfRange are both the same message
                message = "bad reference!";
            }
        }

        // Called when an event is raised from Cell.cs
        private void CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell senderCell = (Cell)sender;

            int row = senderCell.RowIndex;
            int col = senderCell.ColIndex;

            if (e.PropertyName == "Text")
            {
                if (senderCell.Text.StartsWith("="))
                {
                    string message = string.Empty;

                    // Cell already has expression (unsubscribe)
                    if (senderCell.ExpTree.Expression != string.Empty)
                    {
                        // No need to check here if error message occured because these are previously approved names
                        this.SubOrUnsubToAllVariables(senderCell, senderCell.ExpTree.Expression, false, ref message);
                    }

                    this.SubOrUnsubToAllVariables(senderCell, senderCell.ExpTree.Expression = senderCell.Text.Substring(1), true, ref message);

                    // Checks if an error message occured
                    if (message == string.Empty)
                    {
                        senderCell.Val = this.cells[row, col].Val = senderCell.ExpTree.Evaluate().ToString();
                    }
                    else
                    {
                        senderCell.Val = this.cells[row, col].Val = senderCell.Text = message;
                    }
                }
                else
                {
                    senderCell.Val = senderCell.Text;
                }

                this.SpreadsheetChanged?.Invoke(sender, new PropertyChangedEventArgs("RefreshVal"));
            }
            else if (e.PropertyName == "BgColor")
            {
                this.SpreadsheetChanged?.Invoke(sender, new PropertyChangedEventArgs("RefreshBgColor"));
            }
        }
    }
}
