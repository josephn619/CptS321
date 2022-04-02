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

        private ExpressionTree expTree;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="row">row.</param>
        /// <param name="col">col.</param>
        public Cell(int row, int col)
        {
            this.expTree = new ExpressionTree(string.Empty);

            this.rowIndex = row;
            this.colIndex = col;
            this.text = string.Empty;
            this.val = string.Empty;
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
        /// Raises PropertyChanged event
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">e.</param>
        public void CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
        }
    }
}
