// <copyright file="ExpressionTreeTests.cs" company="Adam Nassar 11588762">
// Copyright (c) Adam Nassar 11588762. All rights reserved.
// </copyright>

namespace Spreadsheet_Adam_Nassar.Tests
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using Cpts321;
    using NUnit.Framework;

    /// <summary>
    /// Class for Exp Tree tests.
    /// </summary>
    [TestFixture]
    public class ExpressionTreeTests
    {
        private ExpressionTree objectUnderTest = new (string.Empty);

        /// <summary>
        /// Test Evaluate method w/ normal cases.
        /// </summary>
        /// <param name="expression">expression.</param>
        /// <returns>Evaluated expression.</returns>
        [TestCase("(5+10)*2", ExpectedResult = 30)]
        [TestCase("2*(5+10)", ExpectedResult = 30)]
        [TestCase("3+5", ExpectedResult = 8)]
        public double TestEvaluateNormalCases(string expression)
        {
            ExpressionTree testTree = new (expression);
            return testTree.Evaluate();
        }

        /// <summary>
        /// Tests if operator is considered unsupported.
        /// </summary>
        /// <param name="expression">expression.</param>
        [TestCase("4%2")]
        public void TestUnsupportedOperatorExpression(string expression)
        {
            Assert.Throws<NotSupportedException>(() => new ExpressionTree(expression));

            // Assert.That(() => new Cpts321.ExpressionTree(expression), Throws.TypeOf<System.NotSupportedException>());
        }

        /// <summary>
        /// Tests Infinity cases.
        /// </summary>
        [Test]
        public void TestInfinity()
        {
            string maxValue = double.MaxValue.ToString("F", CultureInfo.InvariantCulture);
            double result = new ExpressionTree($"{maxValue}+{maxValue}").Evaluate();
            Assert.True(double.IsInfinity(result));
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
            MethodInfo methodInfo = this.GetMethod("ConvertToPostFix", new Type[] { typeof(string) });

            if (methodInfo.Invoke(this.objectUnderTest, new object[] { expression }) == null)
            {
                throw new ArgumentNullException();
            }

            // As far as I have found, there is no way to remove this warning (without supressing)
            return methodInfo.Invoke(this.objectUnderTest, new object[] { expression }).ToString();
        }

        private MethodInfo GetMethod(string methodName, Type[] parameterTypes)
        {
            if (string.IsNullOrWhiteSpace(methodName))
            {
                throw new ArgumentNullException("methodName cannot be null or whitespace");
            }

            var method = this.objectUnderTest.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance, null, parameterTypes, null);
            if (method == null)
            {
                throw new ArgumentNullException(string.Format("{0} method not found", methodName));
            }

            return method;
        }
    }
}
