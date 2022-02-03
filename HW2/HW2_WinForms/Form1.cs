// <copyright file="Form1.cs" company="Adam Nassar 11588762">
// Copyright (c) Adam Nassar 11588762. All rights reserved.
// </copyright>

namespace HW2_WinForms
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
    /// Generates Form.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 1)Fix text in TextBox
            // 2)Fix test cases
            // 3)Maybe add more tests
            List<int> myList = new List<int>(10000);
            Random r = new Random();

            for (int i = 0; i < myList.Capacity; i++)
            {
                myList.Add(r.Next(0, 20000));
            }

            int uniqueViaHashSet = MyDistinct.MyDistinct.HashSet(myList);

            int uniqueViaLowMemory = MyDistinct.MyDistinct.LowMemory(myList);

            int uniqueViaListSort = MyDistinct.MyDistinct.ListSort(myList);

            string textHashSet = "1. HashSet method returned " + uniqueViaHashSet.ToString() + " distinct integers at O(n) time complexity with O(n) storage complexity. The reason for this is when the HashSet is initialized, it goes through the entire input list one time (size n). Due to the creation of our HashSet, we now have a dynamically allocated container that stores distinct contents from our input list. ";

            string textLowMemory = "2. LowMemory method returned " + uniqueViaLowMemory.ToString() + " distinct integers at O(n^2) time complexity with O(1) storage complexity. This is because there are no dynamically allocated containers used to store distinct integers. To account for that, the function goes through the list once forward, and then n times backwards (in order to find the last occurence of an integer. ";

            string textListSort = "3. ListSort method returned " + uniqueViaListSort.ToString() + " distinct integers at O(nlogn) + O(n) (best case) or O(n^2) + O(n) (worst case) time complexity with O(1) storage complexity. This is because we have to sort to list initially, then we go through the resulting list to find distinct integers.";

            string myText = textHashSet + textLowMemory + textListSort;

            this.TextBox.Text = myText;
        }

        /// <summary>
        /// Updates TextBox with corresponding code.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">e.</param>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
        }
    }
}