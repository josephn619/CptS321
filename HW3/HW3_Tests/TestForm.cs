// <copyright file="TestForm.cs" company="Adam Nassar 1158872">
// Copyright (c) Adam Nassar 1158872. All rights reserved.
// </copyright>

namespace HW3_WinForms.Tests
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
    using NUnit.Framework;

    /// <summary>
    /// Class to test form functionality.
    /// </summary>
    public class TestForm
    {
        /// <summary>
        /// Sets up nothing.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Tests Load Ability of FibonacciTextReader.
        /// </summary>
        [Test]
        public void TestLoadFibonacciNormal()
        {
            FibonacciTextReader fib = new FibonacciTextReader(3);
            string testResult = fib.ReadToEnd();

            string expectedResult = "1: 0\r\n2: 1\r\n3: 1\r\n";

            Assert.AreEqual(expectedResult, testResult);
        }

        /// <summary>
        /// Tests edge case where no elements are added.
        /// </summary>
        [Test]
        public void TestLoadFibonacciEdgeMin()
        {
            FibonacciTextReader fib = new FibonacciTextReader(0);
            string testResult = fib.ReadToEnd();

            Assert.AreEqual(null, testResult);
        }

        /// <summary>
        /// Tests -1 exception case for Fibbonaci Text Reader.
        /// </summary>
        [Test]
        public void TestLoadFibonacciException()
        {
            FibonacciTextReader fib = new FibonacciTextReader(-1);
            Assert.Throws<System.ArgumentOutOfRangeException>(() => fib.ReadToEnd());
        }
    }
}