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

            this.mySpreadsheet.CellPropertyChanged += this.Refresh_PropertyChanged;

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

        private void Refresh_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Refresh")
            {
                Cpts321.Cell refreshCell = (Cpts321.Cell)sender;

                if (refreshCell != null)
                {
                    int row = refreshCell.RowIndex;
                    int col = refreshCell.ColIndex;

                    this.DataGridView.Rows[row].Cells[col].Value = refreshCell.Val;
                }
            }
        }

        private void Demo_Button_Click(object sender, EventArgs e)
        {
            Random randomCell = new Random();

            for (int i = 0; i < 50; i++)
            {
                int col = randomCell.Next(1, 26);
                int row = randomCell.Next(1, 50);

                Cpts321.Cell initCell = this.mySpreadsheet.GetCell(row, col);
                initCell.Text = "Random Placement";
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
    }
}
