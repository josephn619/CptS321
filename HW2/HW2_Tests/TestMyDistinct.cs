// <copyright file="TestMyDistinct.cs" company="Adam Nassar 11588762">
// Copyright (c) Adam Nassar 11588762. All rights reserved.
// </copyright>

namespace HW2_WinForms.MyDistinct.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    /// <summary>
    /// Runs tests on 3 methods in normal, edge, and exception cases.
    /// </summary>
    [TestFixture]
    public class TestMyDistinct
    {
        /// <summary>
        /// Sets up nothing.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Tests HashSet method through a normal case.
        /// </summary>
        /// <param name="testArray">testArray.</param>
        /// <returns>Assertions.</returns>
        [TestCase(new[] { 1, 1, 2, 3 }, ExpectedResult = 3)]
        public int TestHashSetNormal(int[] testArray)
        {
            List<int> testList = new List<int>(4);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.HashSet(testList);
        }

        /// <summary>
        /// Tests HashSet method through max exception case.
        /// </summary>
        /// <param name="testArray">testArray.</param>
        /// <returns>Assertions.</returns>
        [TestCase(new[] { 20000, 20000, 19999 }, ExpectedResult = 2)]
        public int TestHashSetEdgeMax(int[] testArray)
        {
            List<int> testList = new List<int>(2);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.HashSet(testList);
        }

        /// <summary>
        /// Tests HashSet method through min exception case.
        /// </summary>
        /// <param name="testArray">testArray.</param>
        /// <returns>Assertions.</returns>
        [TestCase(new[] { 0, 0, 1 }, ExpectedResult = 2)]
        public int TestHashSetEdgeMin(int[] testArray)
        {
            List<int> testList = new List<int>(3);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.HashSet(testList);
        }

        /// <summary>
        /// Tests HashSet method through max exception case.
        /// </summary>
        [Test]
        public void TestHashSetExceptionMax()
        {
            Assert.Throws<System.OverflowException>(() => MyDistinct.HashSet(new List<int> { int.MaxValue, int.MaxValue }));
        }

        /// <summary>
        /// Tests HashSet method through min exception case.
        /// </summary>
        [Test]
        public void TestHashSetExceptionMin()
        {
            Assert.Throws<System.OverflowException>(() => MyDistinct.HashSet(new List<int> { int.MinValue, int.MinValue }));
        }

        /// <summary>
        /// Tests LowMemory method through normal case.
        /// </summary>
        /// <param name="testArray">testArray.</param>
        /// <returns>Assertions.</returns>
        [TestCase(new[] { 1, 1, 2, 3 }, ExpectedResult = 3)]
        public int TestLowMemoryNormal(int[] testArray)
        {
            List<int> testList = new List<int>(4);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.LowMemory(testList);
        }

        /// <summary>
        /// Tests LowMemory method through max edge case.
        /// </summary>
        /// <param name="testArray">testArray.</param>
        /// <returns>Assertions.</returns>
        [TestCase(new[] { 20000, 20000, 19999 }, ExpectedResult = 2)]
        public int TestLowMemoryEdgeMax(int[] testArray)
        {
            List<int> testList = new List<int>(2);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.LowMemory(testList);
        }

        /// <summary>
        /// Tests LowMemory method through min edge case.
        /// </summary>
        /// <param name="testArray">testArray.</param>
        /// <returns>Assertions.</returns>
        [TestCase(new[] { 0, 0, 1 }, ExpectedResult = 2)]
        public int TestLowMemoryEdgeMin(int[] testArray)
        {
            List<int> testList = new List<int>(3);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.LowMemory(testList);
        }

        /// <summary>
        /// Tests LowMemory method through max exception case.
        /// </summary>
        [Test]
        public void TestLowMemoryExceptionMax()
        {
            Assert.Throws<System.OverflowException>(() => MyDistinct.LowMemory(new List<int> { int.MaxValue, int.MaxValue }));
        }

        /// <summary>
        /// Tests LowMemory method through min exception case.
        /// </summary>
        [Test]
        public void TestLowMemoryExceptionMin()
        {
            Assert.Throws<System.OverflowException>(() => MyDistinct.LowMemory(new List<int> { int.MinValue, int.MinValue }));
        }

        /// <summary>
        /// Tests BuiltInsort method through normal case.
        /// </summary>
        /// <param name="testArray">testArray.</param>
        /// <returns>Assertions.</returns>
        [TestCase(new[] { 1, 1, 2, 3 }, ExpectedResult = 3)]
        public int TestBuiltInSortNormal(int[] testArray)
        {
            List<int> testList = new List<int>(4);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.BuiltInSort(testList);
        }

        /// <summary>
        /// Tests BuiltInsort method through max edge case.
        /// </summary>
        /// <param name="testArray">testArray.</param>
        /// <returns>Assertions.</returns>
        [TestCase(new[] { 20000, 20000, 19999 }, ExpectedResult = 2)]
        public int TestBuiltInSortEdgeMax(int[] testArray)
        {
            List<int> testList = new List<int>(2);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.BuiltInSort(testList);
        }

        /// <summary>
        /// Tests BuiltInSort method through min edge case.
        /// </summary>
        /// <param name="testArray">testArray.</param>
        /// <returns>Assertions.</returns>
        [TestCase(new[] { 0, 0, 1 }, ExpectedResult = 2)]
        public int TestBuiltInSortEdgeMin(int[] testArray)
        {
            List<int> testList = new List<int>(3);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.BuiltInSort(testList);
        }

        /// <summary>
        /// Tests BuiltInSort method through max exception case.
        /// </summary>
        [Test]
        public void TestBuiltInSortExceptionMax()
        {
            Assert.Throws<System.OverflowException>(() => MyDistinct.BuiltInSort(new List<int> { int.MaxValue, int.MaxValue }));
        }

        /// <summary>
        /// Tests BuiltInSort method through min exception case.
        /// </summary>
        [Test]
        public void TestBuiltInSortExceptionMin()
        {
            Assert.Throws<System.OverflowException>(() => MyDistinct.BuiltInSort(new List<int> { int.MinValue, int.MinValue }));
        }
    }
}