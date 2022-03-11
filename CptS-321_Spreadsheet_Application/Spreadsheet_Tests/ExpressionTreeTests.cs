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
        [TestCase("(5+10)*2", ExpectedResult = 17)]
        [TestCase("3+5", ExpectedResult = 8)]
        public double TestEvaluateNormalCases(string expression)
        {
            Cpts321.ExpressionTree testTree = new Cpts321.ExpressionTree(expression);
            return testTree.Evaluate();
        }

        /// <summary>
        /// Tests if operator is considered unsupported.
        /// </summary>
        /// <param name="expression">expression.</param>
        [TestCase("4%2")]
        public void TestUnsupportedOperatorExpression(string expression)
        {
            Assert.Throws<System.NotSupportedException>(() => new Cpts321.ExpressionTree(expression));

            // Assert.That(() => new Cpts321.ExpressionTree(expression), Throws.TypeOf<System.NotSupportedException>());
        }

        /// <summary>
        /// Tests Infinity cases.
        /// </summary>
        [Test]
        public void TestInfinity()
        {
            string maxValue = double.MaxValue.ToString("F", CultureInfo.InvariantCulture);
            double result = new Cpts321.ExpressionTree($"{maxValue}+{maxValue}").Evaluate();
            Assert.True(double.IsInfinity(result));
        }

        /// <summary>
        /// Tests Negative Infinity cases.
        /// </summary>
        [Test]
        public void TestNegativeInfinity()
        {
            string minValue = double.MinValue.ToString("F", CultureInfo.InvariantCulture);
            double result = new Cpts321.ExpressionTree($"{minValue}-{minValue}").Evaluate();
            Assert.True(double.IsNegativeInfinity(result));
        }

        /// <summary>
        /// Converts infix string to postfix.
        /// </summary>
        /// <param name="expression">expression.</param>
        /// <returns>postfix expression.</returns>
        [TestCase("a*b/(c-d)", ExpectedResult = "abcd-/*")]
        [TestCase("10+12", ExpectedResult = "1012+")]
        [TestCase("a+b*c", ExpectedResult = "abc*+")]
        [TestCase("a*(b+c*d)+e", ExpectedResult = "abcd*+*e+")]
        public string TestInfixToPostfix(string expression)
        {
            Cpts321.ExpressionTree testTree = new Cpts321.ExpressionTree(expression);
            return testTree.ConvertToPostFix(expression);
        }
    }
}
