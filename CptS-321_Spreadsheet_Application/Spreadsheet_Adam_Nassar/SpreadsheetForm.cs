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
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// Basic Form Class.
    /// </summary>
    public partial class SpreadsheetForm : Form
    {
        private Cpts321.Spreadsheet mySpreadsheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadsheetForm"/> class.
        /// </summary>
        public SpreadsheetForm()
        {
            this.mySpreadsheet = new Cpts321.Spreadsheet(50, 26);

            this.InitializeComponent();

            this.mySpreadsheet.CellPropertyChanged += this.Refresh_CellPropertyChanged;

            this.DataGridView.CellBeginEdit += this.DGV_CellBeginEdit;

            this.DataGridView.CellEndEdit += this.DGV_CellEndEdit;
        }

        private void SpreadsheetForm_Load(object sender, EventArgs e)
        {
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
            Cpts321.Cell senderAsCell = (Cpts321.Cell)sender;

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
        }

        private void DGV_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Cpts321.Cell initCell = this.mySpreadsheet.GetCell(e.RowIndex, e.ColumnIndex);

            if (initCell != null)
            {
                this.DataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = initCell.Text;
            }
        }

        private void DGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Cpts321.Cell initCell = this.mySpreadsheet.GetCell(e.RowIndex, e.ColumnIndex);

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
            Cpts321.Cell initCell = this.mySpreadsheet.GetCell(row, col);
            initCell.Text = message + (row + 1);
        }

        private void ChangeSelectedCellsColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.DataGridView.SelectedCells.Count > 0)
            {
                ColorDialog myDialog = new ColorDialog
                {
                    AllowFullOpen = false,
                    ShowHelp = true,
                    Color = this.DataGridView.SelectedCells[0].Style.BackColor,
                };

                if (myDialog.ShowDialog() == DialogResult.OK)
                {
                    this.mySpreadsheet.SizeUndo = this.DataGridView.SelectedCells.Count;

                    for (int i = 0; i < this.DataGridView.SelectedCells.Count; i++)
                    {
                        int row = this.DataGridView.SelectedCells[i].RowIndex;
                        int col = this.DataGridView.SelectedCells[i].ColumnIndex;

                        Cpts321.Cell newCell = this.mySpreadsheet.GetCell(row, col).CreateCopy();
                        this.mySpreadsheet.UndoStack.Push(new Cpts321.Undo_Redo(newCell, "Color change"));

                        this.undoToolStripMenuItem.Enabled = true;
                        this.undoToolStripMenuItem.Text = "Undo " + this.mySpreadsheet.UndoStack.Peek().PropertyChanged;

                        this.mySpreadsheet.GetCell(row, col).BGColor = myDialog.Color.ToArgb();

                        // this.mySpreadsheet.GetCell(row, col).BGColor =
                        //   (myDialog.Color.A << 24)
                        //    | (myDialog.Color.R << 16)
                        //    | (myDialog.Color.G << 8)
                        //    | (myDialog.Color.B << 0);
                    }
                }
            }
        }

        // Greys out revelantStacks button if empty
        private void GreyOutButton(Stack<Cpts321.Undo_Redo> relevantStack, ToolStripMenuItem relevantToolStrip)
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

        // Undoes or redoes a single previous operation
        private void SingleUndoOrRedo(Stack<Cpts321.Undo_Redo> relevantStack, Stack<Cpts321.Undo_Redo> otherStack, ToolStripMenuItem relevantToolStrip, ToolStripMenuItem otherToolStrip, Cpts321.Undo_Redo pop, string method)
        {
            Cpts321.Undo_Redo curCell = pop;

            if (curCell != null)
            {
                // Create new Undo_Redo instance and push onto undoStack (future undo operation)
                otherStack.Push(new Cpts321.Undo_Redo(curCell.PrevCell, curCell.PropertyChanged));

                otherToolStrip.Enabled = true;

                if (otherStack.Count > 0)
                {
                    // Sets button to method + operation ("Redo " + "Color Changed")
                    otherToolStrip.Text = method + otherStack.Peek().PropertyChanged;
                }
            }

            this.GreyOutButton(relevantStack, relevantToolStrip);
        }

        // Undoes or redoes every operation previously done
        private void AllUndoOrRedo(Stack<Cpts321.Undo_Redo> relevantStack, Stack<Cpts321.Undo_Redo> otherStack, ToolStripMenuItem relevantToolStrip, ToolStripMenuItem otherToolStrip, Cpts321.Undo_Redo[] popArr, int relevantSize, ref int otherSize, string checkMessage, string method)
        {
            // Checks if the text expecting the undo or redo
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

        // Gets all the undo or redo operations that need to happen, and then calls AllUndoOrRedo which executes them all
        private void UndoOrRedo(Stack<Cpts321.Undo_Redo> relevantStack, Stack<Cpts321.Undo_Redo> otherStack, ToolStripMenuItem relevantToolStrip, ToolStripMenuItem otherToolStrip, Func<Stack<Cpts321.Undo_Redo>, Cpts321.Undo_Redo> methodName, int relevantSize, ref int otherSize, string checkMessage, string method)
        {
            Cpts321.Undo_Redo[] popArr = new Cpts321.Undo_Redo[relevantSize];

            // Gets all the undo or redo pops (previous operations)
            for (int i = 0; i < popArr.Length; i++)
            {
                popArr[i] = methodName(relevantStack);
            }

            this.AllUndoOrRedo(relevantStack, otherStack, relevantToolStrip, otherToolStrip, popArr, popArr.Length, ref otherSize, checkMessage, method);
        }

        // Undo button click
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Have to use temp because you can't reference a member of a class (ref this.mySpreadsheet.SizeRedo)
            int temp = 0;
            this.UndoOrRedo(this.mySpreadsheet.UndoStack, this.mySpreadsheet.RedoStack, this.undoToolStripMenuItem, this.redoToolStripMenuItem, this.mySpreadsheet.UndoOrRedoPop, this.mySpreadsheet.SizeUndo, ref temp, "Undo Color change", "Redo ");
            this.mySpreadsheet.SizeRedo = temp;
        }

        // Redo button click
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Have to use temp because you can't reference a member of a class (ref this.mySpreadsheet.SizeUndo)
            int temp = 0;
            this.UndoOrRedo(this.mySpreadsheet.RedoStack, this.mySpreadsheet.UndoStack, this.redoToolStripMenuItem, this.undoToolStripMenuItem, this.mySpreadsheet.UndoOrRedoPop, this.mySpreadsheet.SizeRedo, ref temp, "Redo Color change", "Undo ");
            this.mySpreadsheet.SizeUndo = temp;
        }
    }
}
