using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace HW2_WinForms.MyDistinct.Tests
{
    [TestFixture]
    public class TestMyDistinct
    {
        [SetUp]
        public void Setup()
        {

        }

        [TestCase(new[] { 1, 1, 2, 3 }, ExpectedResult = 3)]
        public int TestHashSetNormal(int[] testArray)
        {
            List<int> testList = new List<int>(4);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.hashSet(testList);
        }

        [TestCase(new[] { 20000, 20000, 19999 }, ExpectedResult = 2)]
        public int TestHashSetEdgeMax(int[] testArray)
        {
            List<int> testList = new List<int>(2);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.hashSet(testList);
        }

        [TestCase(new[] { 0, 0, 1 }, ExpectedResult = 2)]
        public int TestHashSetEdgeMin(int[] testArray)
        {
            List<int> testList = new List<int>(3);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.hashSet(testList);
        }

        [Test]
        public void TestHashSetExceptionMax()
        {
            Assert.Throws<System.OverflowException>(() => MyDistinct.hashSet(new List<int> { int.MaxValue, int.MaxValue }));
        }

        [Test]
        public void TestHashSetExceptionMin()
        {
            Assert.Throws<System.OverflowException>(() => MyDistinct.hashSet(new List<int> { int.MinValue, int.MinValue }));
        }

        [TestCase(new[] { 1, 1, 2, 3 }, ExpectedResult = 3)]
        public int TestLowMemoryNormal(int[] testArray)
        {
            List<int> testList = new List<int>(4);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.lowMemory(testList);
        }

        [TestCase(new[] { 20000, 20000, 19999 }, ExpectedResult = 2)]
        public int TestLowMemoryEdgeMax(int[] testArray)
        {
            List<int> testList = new List<int>(2);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.lowMemory(testList);
        }

        [TestCase(new[] { 0, 0, 1 }, ExpectedResult = 2)]
        public int TestLowMemoryEdgeMin(int[] testArray)
        {
            List<int> testList = new List<int>(3);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.lowMemory(testList);
        }

        [Test]
        public void TestLowMemoryExceptionMax()
        {
            Assert.Throws<System.OverflowException>(() => MyDistinct.lowMemory(new List<int> { int.MaxValue, int.MaxValue }));
        }

        [Test]
        public void TestLowMemoryExceptionMin()
        {
            Assert.Throws<System.OverflowException>(() => MyDistinct.lowMemory(new List<int> { int.MinValue, int.MinValue }));
        }

        [TestCase(new[] { 1, 1, 2, 3 }, ExpectedResult = 3)]
        public int TestBuiltInSortNormal(int[] testArray)
        {
            List<int> testList = new List<int>(4);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.builtInSort(testList);
        }

        [TestCase(new[] { 20000, 20000, 19999 }, ExpectedResult = 2)]
        public int TestBuiltInSortEdgeMax(int[] testArray)
        {
            List<int> testList = new List<int>(2);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.builtInSort(testList);
        }

        [TestCase(new[] { 0, 0, 1 }, ExpectedResult = 2)]
        public int TestBuiltInSortEdgeMin(int[] testArray)
        {
            List<int> testList = new List<int>(3);
            foreach (var num in testArray)
            {
                testList.Add(num);
            }

            return MyDistinct.builtInSort(testList);
        }

        [Test]
        public void TestBuiltInSortExceptionMax()
        {
            Assert.Throws<System.OverflowException>(() => MyDistinct.builtInSort(new List<int> { int.MaxValue, int.MaxValue }));
        }

        [Test]
        public void TestBuiltInSortExceptionMin()
        {
            Assert.Throws<System.OverflowException>(() => MyDistinct.builtInSort(new List<int> { int.MinValue, int.MinValue }));
        }
    }
}