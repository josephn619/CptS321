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
            // Creates list, then populates with numbers from testArray, then runs algorithm on list
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
            // Creates list, then populates with numbers from testArray, then runs algorithm on list
            List<int> testList = new List<int>(3);
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
            // Creates list, then populates with numbers from testArray, then runs algorithm on list
            List<int> testList = new List<int>(3);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.HashSet(testList);
        }

        /// <summary>
        /// Tests LowMemory method through normal case.
        /// </summary>
        /// <param name="testArray">testArray.</param>
        /// <returns>Assertions.</returns>
        [TestCase(new[] { 1, 1, 2, 3 }, ExpectedResult = 3)]
        public int TestLowMemoryNormal(int[] testArray)
        {
            // Creates list, then populates with numbers from testArray, then runs algorithm on list
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
            // Creates list, then populates with numbers from testArray, then runs algorithm on list
            List<int> testList = new List<int>(3);
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
            // Creates list, then populates with numbers from testArray, then runs algorithm on list
            List<int> testList = new List<int>(3);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.LowMemory(testList);
        }

        /// <summary>
        /// Tests ListSort method through normal case with duplicate numbers at start.
        /// </summary>
        /// <param name="testArray">testArray.</param>
        /// <returns>Assertions.</returns>
        [TestCase(new[] { 1, 1, 2, 3 }, ExpectedResult = 3)]
        public int TestListSortNormalDoubleStart(int[] testArray)
        {
            // Creates list, then populates with numbers from testArray, then runs algorithm on list
            List<int> testList = new List<int>(4);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.ListSort(testList);
        }

        /// <summary>
        /// Tests ListSort method through normal case with duplicate numbers at end.
        /// </summary>
        /// <param name="testArray">testArray.</param>
        /// <returns>Assertions.</returns>
        [TestCase(new[] { 1, 2, 3, 3 }, ExpectedResult = 3)]
        public int TestListSortNormalDoubleEnd(int[] testArray)
        {
            // Creates list, then populates with numbers from testArray, then runs algorithm on list
            List<int> testList = new List<int>(4);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.ListSort(testList);
        }

        /// <summary>
        /// Tests ListSort method through normal case with duplicate numbers at start and end.
        /// </summary>
        /// <param name="testArray">testArray.</param>
        /// <returns>Assertions.</returns>
        [TestCase(new[] { 1, 1, 2, 2 }, ExpectedResult = 2)]
        public int TestListSortNormalDoubleBoth(int[] testArray)
        {
            // Creates list, then populates with numbers from testArray, then runs algorithm on list
            List<int> testList = new List<int>(4);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.ListSort(testList);
        }

        /// <summary>
        /// Tests ListSort method through normal case with no duplicate numbers.
        /// </summary>
        /// <param name="testArray">testArray.</param>
        /// <returns>Assertions.</returns>
        [TestCase(new[] { 1, 2, 3, 4 }, ExpectedResult = 4)]
        public int TestListSortNormalNoDouble(int[] testArray)
        {
            // Creates list, then populates with numbers from testArray, then runs algorithm on list
            List<int> testList = new List<int>(4);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.ListSort(testList);
        }

        /// <summary>
        /// Tests ListInsert method through max edge case.
        /// </summary>
        /// <param name="testArray">testArray.</param>
        /// <returns>Assertions.</returns>
        [TestCase(new[] { 20000, 20000, 19999 }, ExpectedResult = 2)]
        public int TestListSortEdgeMax(int[] testArray)
        {
            // Creates list, then populates with numbers from testArray, then runs algorithm on list
            List<int> testList = new List<int>(3);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.ListSort(testList);
        }

        /// <summary>
        /// Tests ListSort method through min edge case.
        /// </summary>
        /// <param name="testArray">testArray.</param>
        /// <returns>Assertions.</returns>
        [TestCase(new[] { 0, 0, 1 }, ExpectedResult = 2)]
        public int TestListSortEdgeMin(int[] testArray)
        {
            // Creates list, then populates with numbers from testArray, then runs algorithm on list
            List<int> testList = new List<int>(3);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.ListSort(testList);
        }

        // /// <summary>
        // /// Tests HashSet method through max exception case.
        // /// </summary>
        // [Test]
        // public void TestHashSetExceptionMax()
        // {
        //     Assert.Throws<System.OverflowException>(() => MyDistinct.HashSet(new List<int> { int.MaxValue, int.MaxValue }));
        // }

        // /// <summary>
        // /// Tests HashSet method through min exception case.
        // /// </summary>
        // [Test]
        // public void TestHashSetExceptionMin()
        // {
        //     Assert.Throws<System.OverflowException>(() => MyDistinct.HashSet(new List<int> { int.MinValue, int.MinValue }));
        // }

        // /// <summary>
        // /// Tests LowMemory method through max exception case.
        // /// </summary>
        // [Test]
        // public void TestLowMemoryExceptionMax()
        // {
        //     Assert.Throws<System.OverflowException>(() => MyDistinct.LowMemory(new List<int> { int.MaxValue, int.MaxValue }));
        // }

        // /// <summary>
        // /// Tests LowMemory method through min exception case.
        // /// </summary>
        // [Test]
        // public void TestLowMemoryExceptionMin()
        // {
        //     Assert.Throws<System.OverflowException>(() => MyDistinct.LowMemory(new List<int> { int.MinValue, int.MinValue }));
        // }

        // /// <summary>
        // /// Tests ListSort method through max exception case.
        // /// </summary>
        // [Test]
        // public void TestListSortExceptionMax()
        // {
        //     Assert.Throws<System.OverflowException>(() => MyDistinct.ListSort(new List<int> { int.MaxValue, int.MaxValue }));
        // }

        // /// <summary>
        // /// Tests ListSort method through min exception case.
        // /// </summary>
        // [Test]
        // public void TestListSortExceptionMin()
        // {
        //     Assert.Throws<System.OverflowException>(() => MyDistinct.ListSort(new List<int> { int.MinValue, int.MinValue }));
        // }
    }
}