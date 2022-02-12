// <copyright file="MyForm.cs" company="Adam Nassar 11588762">
// Copyright (c) Adam Nassar 11588762. All rights reserved.
// </copyright>

namespace HW3_WinForms
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

    /// <summary>
    /// Class for WinForms Interface.
    /// </summary>
    public partial class MyForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyForm"/> class.
        /// </summary>
        public MyForm()
        {
            this.InitializeComponent();
        }

        private void MyForm_Load(object sender, EventArgs e)
        {
        }

        private void LoadText(TextReader r)
        {
            this.TextBox.Text = r.ReadToEnd();
            r.Dispose();
        }

        /// <summary>
        /// Loads from selected file in file system.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">e.</param>
        private void LoadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Opens file from computer file system
            OpenFileDialog loadFileInstance = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt| All Files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true,
            };
            if (loadFileInstance.ShowDialog() == DialogResult.OK)
            {
                // Gets text and loads to textbox
                StreamReader readStreamInstance = new StreamReader(loadFileInstance.FileName);
                this.LoadText(readStreamInstance);
                readStreamInstance.Close();
            }
        }

        /// <summary>
        /// Calculates first 50 fibonacci numbers.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">e.</param>
        private void LoadFirst50FibonacciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // New fib instance
            FibonacciTextReader newFibSequence = new FibonacciTextReader(50);
            this.LoadText(newFibSequence);
        }

        /// <summary>
        /// Calculates first 100 fibonacci numbers.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">e.</param>
        private void LoadFirst100FibonacciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // New fib instance
            FibonacciTextReader newFibSequence = new FibonacciTextReader(100);
            this.LoadText(newFibSequence);
        }

        /// <summary>
        /// Saves file to output file in file system.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">e.</param>
        private void SaveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Opens file to save to from computer file system
            SaveFileDialog saveFileInstance = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt| All Files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true,
            };
            if (saveFileInstance.ShowDialog() == DialogResult.OK)
            {
                // Gets text and writes to file.
                string fileName = saveFileInstance.FileName;
                StreamWriter writeStreamInstance = new StreamWriter(File.Create(fileName));
                writeStreamInstance.Write(this.TextBox.Text);
                writeStreamInstance.Dispose();
            }
        }
    }
}
