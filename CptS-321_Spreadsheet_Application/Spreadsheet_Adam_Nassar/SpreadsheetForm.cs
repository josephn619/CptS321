// <copyright file="SpreadsheetForm.cs" company="Adam Nassar 11588762">
// Copyright (c) Adam Nassar 11588762. All rights reserved.
// </copyright>

namespace Spreadsheet_Adam_Nassar
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Cpts321;

    /// <summary>
    /// Basic Form Class.
    /// </summary>
    public partial class SpreadsheetForm : Form
    {
        private Spreadsheet mySpreadsheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadsheetForm"/> class.
        /// </summary>
        public SpreadsheetForm()
        {
            this.mySpreadsheet = new Spreadsheet(50, 26);

            this.InitializeComponent();

            this.mySpreadsheet.CellPropertyChanged += this.Refresh_CellPropertyChanged;

            this.DataGridView.CellBeginEdit += this.DGV_CellBeginEdit;

            this.DataGridView.CellEndEdit += this.DGV_CellEndEdit;
        }

        private void SpreadsheetForm_Load(object sender, EventArgs e)
        {
            this.undoToolStripMenuItem.Enabled = false;
            this.undoToolStripMenuItem.Text = "Undo operation";

            this.redoToolStripMenuItem.Enabled = false;
            this.redoToolStripMenuItem.Text = "Redo operation";

            string[] s = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            // Column creation
            for (int i = 0; i < s.Length; i++)
            {
                this.DataGridView.Columns.Add(s[i], s[i]);
            }

            // Row initialization
            this.DataGridView.Rows.Add(50);

            // Row creation
            for (int i = 0; i < 50; i++)
            {
                this.DataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }

        private void Refresh_CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell senderAsCell = (Cell)sender;

            int row = senderAsCell.RowIndex;
            int col = senderAsCell.ColIndex;

            if (e.PropertyName == "RefreshVal")
            {
                if (senderAsCell != null)
                {
                    this.DataGridView.Rows[row].Cells[col].Value = senderAsCell.Val;
                }
            }
            else if (e.PropertyName == "RefreshBgColor")
            {
                if (senderAsCell != null)
                {
                    this.DataGridView.Rows[row].Cells[col].Style.BackColor = Color.FromArgb(senderAsCell.BGColor);
                }
            }
            else if (e.PropertyName == "Cell")
            {
                if (senderAsCell != null)
                {
                    this.DataGridView.Rows[row].Cells[col].Value = senderAsCell.Val;
                    this.DataGridView.Rows[row].Cells[col].Style.BackColor = Color.FromArgb(senderAsCell.BGColor);


                }
            }
        }

        private void DGV_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Cell initCell = this.mySpreadsheet.GetCell(e.RowIndex, e.ColumnIndex);

            if (initCell != null)
            {
                this.DataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = initCell.Text;
            }
        }

        private void DGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Cell initCell = this.mySpreadsheet.GetCell(e.RowIndex, e.ColumnIndex);

            if (initCell != null)
            {
                try
                {
                    initCell.Text = this.DataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                }
                catch
                {
                    initCell.Text = string.Empty;
                }

                this.DataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = initCell.Val;
            }
        }

        private void Demo_Button_Click(object sender, EventArgs e)
        {
            Random randomCell = new Random();

            for (int i = 0; i < 50; i++)
            {
                int col = randomCell.Next(1, 26);
                int row = randomCell.Next(1, 50);

                this.NamePlacement(row, col, "Random Placement");
            }

            for (int i = 0; i < 50; i++)
            {
                this.NamePlacement(i, 1, "This is cell B");
            }

            for (int i = 0; i < 50; i++)
            {
                this.NamePlacement(i, 0, "=B");
            }
        }

        private void NamePlacement(int row, int col, string message)
        {
            Cell initCell = this.mySpreadsheet.GetCell(row, col);
            initCell.Text = message + (row + 1);
        }

        private void ChangeSelectedCellsColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.DataGridView.SelectedCells.Count > 0)
            {
                // Creates new ColorDialog instance
                ColorDialog myDialog = new ColorDialog
                {
                    AllowFullOpen = false,
                    ShowHelp = true,
                    Color = this.DataGridView.SelectedCells[0].Style.BackColor,
                };

                if (myDialog.ShowDialog() == DialogResult.OK)
                {
                    this.mySpreadsheet.SizeUndo.Push(this.DataGridView.SelectedCells.Count);

                    for (int i = 0; i < this.DataGridView.SelectedCells.Count; i++)
                    {
                        int row = this.DataGridView.SelectedCells[i].RowIndex;
                        int col = this.DataGridView.SelectedCells[i].ColumnIndex;

                        // Creates copy of the cell, then pushes new undo operation
                        Cell newCell = this.mySpreadsheet.GetCell(row, col).CreateCopy();
                        this.mySpreadsheet.UndoStack.Push(new Undo_Redo(newCell, "Color Change"));

                        // Changes button text, and then UnGreys the button
                        this.undoToolStripMenuItem.Text = "Undo " + this.mySpreadsheet.UndoStack.Peek().PropertyChanged;
                        this.GreyOrUnGreyButton(this.mySpreadsheet.UndoStack, this.undoToolStripMenuItem);

                        // Changes the color
                        this.mySpreadsheet.GetCell(row, col).BGColor = myDialog.Color.ToArgb();
                    }
                }
            }
        }

        // Greys out revelantStacks button if empty
        private void GreyOrUnGreyButton(Stack<Undo_Redo> relevantStack, ToolStripMenuItem relevantToolStrip)
        {
            if (relevantStack.Count == 0)
            {
                relevantToolStrip.Enabled = false;
            }
            else
            {
                relevantToolStrip.Enabled = true;
            }
        }

        // Updates a single cell of the undo or redo stacks
        private void SingleUndoOrRedo(Stack<Undo_Redo> relevantStack, Stack<Undo_Redo> otherStack, ToolStripMenuItem relevantToolStrip, ToolStripMenuItem otherToolStrip, Undo_Redo pop, string method)
        {
            Undo_Redo curCell = pop;

            if (curCell != null)
            {
                // Create new Undo_Redo instance and push onto undoStack (future undo operation)
                otherStack.Push(new Undo_Redo(curCell.PrevCell, curCell.PropertyChanged));

                this.GreyOrUnGreyButton(otherStack, otherToolStrip);

                if (otherStack.Count > 0)
                {
                    // Sets button to method + operation ("Redo " + "Color Changed")
                    otherToolStrip.Text = method + otherStack.Peek().PropertyChanged;
                }
            }

            this.GreyOrUnGreyButton(relevantStack, relevantToolStrip);
        }

        // Updates the undo and redo stacks entirely (This function is uncessary as its only called once but I'm keeping it cuz it helps readability)
        private void AllUndoOrRedo(Stack<Undo_Redo> relevantStack, Stack<Undo_Redo> otherStack, ToolStripMenuItem relevantToolStrip, ToolStripMenuItem otherToolStrip, Undo_Redo[] popArr, int relevantSize, ref int otherSize, string checkMessage, string method)
        {
            // Checks if the text expecting the undo or redo for colors (more than one cell)
            if (relevantToolStrip.Text == checkMessage)
            {
                // Updates the reference for SizeUndo or SizeRedo
                otherSize = relevantSize;

                for (int i = 0; i < relevantSize; i++)
                {
                    this.SingleUndoOrRedo(relevantStack, otherStack, relevantToolStrip, otherToolStrip, popArr[i], method);
                }
            }
            else
            {
                this.SingleUndoOrRedo(relevantStack, otherStack, relevantToolStrip, otherToolStrip, popArr[0], method);
            }
        }

        // Gets all the undo or redo operations that need to happen, and then calls AllUndoOrRedo which executes them all, returns the size of otherStack
        private int UndoOrRedo(Stack<Undo_Redo> relevantStack, Stack<Undo_Redo> otherStack, ToolStripMenuItem relevantToolStrip, ToolStripMenuItem otherToolStrip, Func<Stack<Undo_Redo>, Undo_Redo> undoOrRedoPop, int relevantSize, int otherSize, string checkMessage, string method)
        {
            Undo_Redo[] popArr = new Undo_Redo[relevantSize];

            // Gets all the undo or redo pops (previous operations)
            for (int i = 0; i < popArr.Length; i++)
            {
                // Note this is where the actual color change happens
                popArr[i] = undoOrRedoPop(relevantStack);
            }

            // This just updates the stacks as well as the buttons
            this.AllUndoOrRedo(relevantStack, otherStack, relevantToolStrip, otherToolStrip, popArr, popArr.Length, ref otherSize, checkMessage, method);

            return otherSize;
        }

        // Undo button click
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // We need to update SizeRedo because this sets the number of iterations when we redo
            this.mySpreadsheet.SizeRedo.Push(this.UndoOrRedo(
                this.mySpreadsheet.UndoStack,
                this.mySpreadsheet.RedoStack,
                this.undoToolStripMenuItem,
                this.redoToolStripMenuItem,
                this.mySpreadsheet.UndoOrRedoPop,
                this.mySpreadsheet.SizeUndo.Peek(),
                this.mySpreadsheet.SizeRedo.Peek(),
                "Undo Color Change",
                "Redo "));

            this.mySpreadsheet.SizeUndo.Pop();
        }

        // Redo button click
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // We need to update SizeUndo because this sets the number of iterations when we undo
            this.mySpreadsheet.SizeUndo.Push(this.UndoOrRedo(
                this.mySpreadsheet.RedoStack,
                this.mySpreadsheet.UndoStack,
                this.redoToolStripMenuItem,
                this.undoToolStripMenuItem,
                this.mySpreadsheet.UndoOrRedoPop,
                this.mySpreadsheet.SizeRedo.Peek(),
                this.mySpreadsheet.SizeUndo.Peek(),
                "Redo Color Change",
                "Undo "));

            this.mySpreadsheet.SizeRedo.Pop();
        }

        private void SaveToXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileInstance = new SaveFileDialog()
            {
                InitialDirectory = AppContext.BaseDirectory,
            };

            if (saveFileInstance.ShowDialog() == DialogResult.OK)
            {
                Stream fileStream = saveFileInstance.OpenFile();

                if (fileStream != null)
                {
                    this.mySpreadsheet.SaveToXML(fileStream);
                    fileStream.Close();
                }
            }
        }

        private void LoadFromXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadFileInstance = new OpenFileDialog
            {
                InitialDirectory = AppContext.BaseDirectory,
            };

            if (loadFileInstance.ShowDialog() == DialogResult.OK)
            {
                string pathname = loadFileInstance.FileName;

                Stream fileStream = loadFileInstance.OpenFile();

                this.mySpreadsheet.LoadFromXML(fileStream);

                fileStream.Close();
            }
        }
    }
}
