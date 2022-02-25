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
    using SpreadsheetEngine;

    /// <summary>
    /// Basic Form Class.
    /// </summary>
    public partial class SpreadsheetForm : Form
    {
        private Spreadsheet mySpreadsheet = new Spreadsheet(50, 26);

        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadsheetForm"/> class.
        /// </summary>
        public SpreadsheetForm()
        {
            this.InitializeComponent();
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
    }
}
