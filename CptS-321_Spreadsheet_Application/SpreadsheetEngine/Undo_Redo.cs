// <copyright file="Undo_Redo.cs" company="Adam Nassar 11588762">
// Copyright (c) Adam Nassar 11588762. All rights reserved.
// </copyright>

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class to undo or redo operations.
    /// </summary>
    public class Undo_Redo
    {
        private Cell prevCell;
        private string propertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="Undo_Redo"/> class.
        /// </summary>
        /// <param name="newCell">newCell.</param>
        /// <param name="message">message.</param>
        public Undo_Redo(Cell newCell, string message)
        {
            this.prevCell = newCell;
            this.propertyChanged = message;
        }

        /// <summary>
        /// Gets or sets prevcell.
        /// </summary>
        public Cell PrevCell
        {
            get
            {
                return this.prevCell;
            }

            set
            {
                this.prevCell = value;
            }
        }

        /// <summary>
        /// Gets or sets property changed.
        /// </summary>
        public string PropertyChanged
        {
            get
            {
                return this.propertyChanged;
            }

            set
            {
                this.propertyChanged = value;
            }
        }

        /// <summary>
        /// Updates sender to previous cell.
        /// </summary>
        /// <param name="senderAsCell">senderAsCell.</param>
        public void Update(ref Cell senderAsCell)
        {
            senderAsCell.Text = this.prevCell.Text;
            senderAsCell.BGColor = this.prevCell.BGColor;
        }

        /// <summary>
        /// Gets row for prev cell.
        /// </summary>
        /// <returns>Previous cell.</returns>
        public int GetRow()
        {
            return this.prevCell.RowIndex;
        }

        /// <summary>
        /// Gets col for prev cell.
        /// </summary>
        /// <returns>Previous cel coll.</returns>
        public int GetCol ()
        {
            return this.prevCell.ColIndex;
        }
    }
}
