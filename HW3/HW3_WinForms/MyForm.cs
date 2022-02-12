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
        /// Loads from selected file.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">e.</param>
        private void LoadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadFileInstance = new OpenFileDialog();
            loadFileInstance.Filter = "Text Files (*.txt)|*.txt| All Files (*.*)|*.*";
            loadFileInstance.FilterIndex = 2;
            loadFileInstance.RestoreDirectory = true;
            if (loadFileInstance.ShowDialog() == DialogResult.OK)
            {
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
            FibonacciTextReader newFibSequence = new FibonacciTextReader(100);
            this.LoadText(newFibSequence);
        }

        /// <summary>
        /// Saves file to output file.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">e.</param>
        private void SaveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileInstance = new SaveFileDialog();
            saveFileInstance.Filter = "Text Files (*.txt)|*.txt| All Files (*.*)|*.*";
            saveFileInstance.FilterIndex = 2;
            saveFileInstance.RestoreDirectory = true;
            if (saveFileInstance.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileInstance.FileName;
                StreamWriter writeStreamInstance = new StreamWriter(File.Create(fileName));
                writeStreamInstance.Write(this.TextBox.Text);
                writeStreamInstance.Dispose();
            }
        }
    }
}
