// <copyright file="Tests.cs" company="Adam Nassar 11588762">
// Copyright (c) Adam Nassar 11588762. All rights reserved.
// </copyright>

namespace Spreadsheet_Adam_Nassar.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// Basic Test Class for Tests.
    /// </summary>
    [TestFixture]
    public class Tests
    {
        private Cpts321.Spreadsheet testSpreadsheet = new Cpts321.Spreadsheet(2, 2);

        /// <summary>
        /// Tests cell val.
        /// </summary>
        [Test]
        public void TestCellVal()
        {
            this.testSpreadsheet = new Cpts321.Spreadsheet(2, 2);
            this.testSpreadsheet.GetCell(1, 1).Text = "This is a test";

            Assert.AreEqual(this.testSpreadsheet.GetCell(1, 1).Text, "This is a test");
        }


        /// <summary>
        /// Test row number.
        /// </summary>
        [Test]
        public void TestRowCount()
        {
            Assert.AreEqual(this.testSpreadsheet.RowCount, 2);
        }

        /// <summary>
        /// test col number.
        /// </summary>
        [Test]

        public void TestColumnCount()
        {
            Assert.AreEqual(this.testSpreadsheet.ColCount, 2);
        }

    }
}