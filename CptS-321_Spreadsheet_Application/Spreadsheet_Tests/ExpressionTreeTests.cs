// <copyright file="ExpressionTreeTests.cs" company="Adam Nassar 11588762">
// Copyright (c) Adam Nassar 11588762. All rights reserved.
// </copyright>

namespace Spreadsheet_Adam_Nassar.Tests
{
    using System.Globalization;
    using NUnit.Framework;

    /// <summary>
    /// Class for Exp Tree tests.
    /// </summary>
    [TestFixture]
    public class ExpressionTreeTests
    {
        /// <summary>
        /// Test Evaluate method w/ normal cases.
        /// </summary>
        /// <param name="expression">expression.</param>
        /// <returns>Evaluated expression.</returns>
        [TestCase("3+5", ExpectedResult = 8)]
        public double TestEvaluateNormalCases(string expression)
        {
            Cpts321.ExpressionTree testTree = new Cpts321.ExpressionTree(expression);
            return 0.0;
        }

        /// <summary>
        /// Tests if operator is considered unsupported.
        /// </summary>
        /// <param name="expression">expression.</param>
        public void TestUnsupportedOperatorExpression(string expression)
        {
            Assert.That(() => new Cpts321.ExpressionTree(expression), Throws.TypeOf<System.Exception>());
        }

        /// <summary>
        /// Tests Infinity cases.
        /// </summary>
        [Test]
        public void TestInfinity()
        {
            // string maxValue = double.MaxValue.ToString("F",CultureInfo.InvariantCulture);
            // double result = new Cpts321.ExpressionTree($"{maxValue}+{maxValue}").Evaluate();
            // Assert.True(double.IsInfinity(result));
        }
    }
}
