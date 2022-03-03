// <copyright file="ExpressionTree.cs" company="Adam Nassar 11588762">
// Copyright (c) Adam Nassar 11588762. All rights reserved.
// </copyright>

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for ExpressionTree.
    /// </summary>
    public class ExpressionTree
    {
        private string expression;
        private Node root;

        private Dictionary<string, double> variableDict = new Dictionary<string, double>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression">expression.</param>
        public ExpressionTree(string expression)
        {
            this.root = this.Compile(expression);
            this.expression = expression;
        }

        /// <summary>
        /// Gets or sets expression.
        /// </summary>
        public string Expression
        {
            get
            {
                return this.expression;
            }

            set
            {
                this.expression = value;
                this.root = this.Compile(value);
            }
        }

        /// <summary>
        /// Sets variable in dictionary given name and value.
        /// </summary>
        /// <param name="variableName">variableName.</param>
        /// <param name="variableValue">variableValue.</param>
        public void SetVariable(string variableName, double variableValue)
        {
            this.variableDict.Add(variableName, variableValue);
        }

        /// <summary>
        /// Calls private Evaluate.
        /// </summary>
        /// <returns>Evaluted Root.</returns>
        public double Evaluate()
        {
            return this.Evaluate(this.root);
        }

        private double Evaluate(Node newNode)
        {
            // Checks if Constant type.
            Constant testConst = newNode as Constant;
            if (testConst != null)
            {
                return testConst.Value;
            }

            // Checks if Var type.
            Variable testVar = newNode as Variable;
            if (testVar != null)
            {
                double number;
                if (this.variableDict.TryGetValue(testVar.Var, out number))
                {
                    return number;
                }
                else
                {
                    return 0;
                }
            }

            // Checks if BinaryOperator type.
            BinaryOperator testOp = newNode as BinaryOperator;
            if (testOp != null)
            {
                return testOp.Evaluate(this.Evaluate(testOp.Left), this.Evaluate(testOp.Right));
            }

            throw new NotSupportedException();
        }

        private Node Compile(string expression)
        {
            // Checks if empty.
            if (string.IsNullOrEmpty(expression))
            {
                return null;
            }

            // Recursive implementation that compiles expression string.
            Node newNode = this.GetNode(expression);
            if (newNode != null)
            {
                return newNode;
            }

            return null;
        }

        private Node GetNode(string expression)
        {
            char[] operators = { '+', '-', '*', '/' };

            // Looks for operators in given expression string and then compiles left and right subtrings.
            foreach (char op in operators)
            {
                for (int expressionIndex = expression.Length - 1; expressionIndex >= 0; expressionIndex--)
                {
                    if (expression[expressionIndex] == op)
                    {
                        BinaryOperator o = ExpressionTreeFactory.Create(expression[expressionIndex]);
                        o.Left = this.Compile(expression.Substring(0, expressionIndex));
                        o.Right = this.Compile(expression.Substring(expressionIndex + 1));
                        return o;
                    }
                }
            }

            // If expression is constant and returns as such, otherwise expression is var.
            double number = 0;
            if (double.TryParse(expression, out number))
            {
                return new Constant()
                {
                    Value = number,
                };
            }
            else
            {
                return new Variable()
                {
                    Var = expression,
                };
            }

            throw new NotSupportedException();
        }
    }
}
