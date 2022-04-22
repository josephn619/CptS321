// <copyright file="Cell.cs" company="Adam Nassar 11588762">
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
    /// Cell class for spreadsheet application.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        private readonly int rowIndex;
        private readonly int colIndex;
        private string text;
        private string val;
        private int bgColor;

        private ExpressionTree expTree;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="row">row.</param>
        /// <param name="col">col.</param>
        public Cell(int row, int col)
        {
            this.rowIndex = row;
            this.colIndex = col;
            this.text = string.Empty;
            this.val = string.Empty;
            this.bgColor = -1;

            this.expTree = new ExpressionTree(string.Empty);
        }

        /// <summary>
        /// Event for property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Gets rowIndex.
        /// </summary>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }

        /// <summary>
        /// Gets colIndex.
        /// </summary>
        public int ColIndex
        {
            get
            {
                return this.colIndex;
            }
        }

        /// <summary>
        /// Gets or sets text.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (this.text != value)
                {
                    this.text = value;

                    this.PropertyChanged(this, new PropertyChangedEventArgs("Text"));
                }
            }
        }

        /// <summary>
        /// Gets or sets val.
        /// </summary>
        public string Val
        {
            get
            {
                return this.val;
            }

            protected internal set
            {
                if (this.val != value)
                {
                    this.val = value;

                    this.PropertyChanged(this, new PropertyChangedEventArgs("Val"));
                }
            }
        }

        /// <summary>
        /// Gets or sets bgColor (note, had a big issue where using uint caused a system overflow exception. Someone explained
        /// that uint is to big for Color since max(uint) is greater than max(int32).
        /// </summary>
        public int BGColor
        {
            get
            {
                return this.bgColor;
            }

            set
            {
                if (this.bgColor != value)
                {
                    this.bgColor = value;

                    this.PropertyChanged(this, new PropertyChangedEventArgs("BgColor"));
                }
            }
        }

        /// <summary>
        /// Gets expTree.
        /// </summary>
        public ExpressionTree ExpTree
        {
            get
            {
                return this.expTree;
            }
        }

        /// <summary>
        /// Creates copy of the given cell.
        /// </summary>
        /// <returns>Cell copy.</returns>
        public Cell CreateCopy()
        {
            return new NewCell(this.rowIndex, this.colIndex)
            {
                Text = this.Text,
                BGColor = this.BGColor,
            };
        }

        /// <summary>
        /// Finds if cell is not empty.
        /// </summary>
        /// <returns>True if not empty.</returns>
        public bool IsNotEmpty()
        {
            return this.text.Length > 0 || this.bgColor != -1;
        }

        /// <summary>
        /// Raises PropertyChanged event.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">e.</param>
        public void RHSPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
        }
    }
}
