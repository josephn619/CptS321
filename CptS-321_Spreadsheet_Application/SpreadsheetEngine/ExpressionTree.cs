﻿// <copyright file="ExpressionTree.cs" company="Adam Nassar 11588762">
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
        private static Stack<char> opStack = new Stack<char>();
        private static Stack<Node> exprStack = new Stack<Node>();

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
                this.root = this.Compile(value);
                this.expression = value;
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
        /// Converts expression to postfix.
        /// </summary>
        /// <param name="expression">expression.</param>
        /// <returns>postfix expression.</returns>
        public string ConvertToPostFix(string expression)
        {
            string postfix = string.Empty;

            char pop = '\0';

            foreach (char character in expression)
            {
                if (character == '(')
                {
                    // 2
                    opStack.Push(character);
                }
                else if (character == ')')
                {
                    // 3
                    pop = opStack.Pop();

                    while (pop != '(')
                    {
                        postfix += pop;
                        postfix += " ";
                        pop = opStack.Pop();
                    }
                }
                else if (ExpressionTreeFactory.IsOperator(character))
                {
                    if (opStack.Count == 0 || opStack.Peek() == '(')
                    {
                        // 4
                        opStack.Push(character);
                    }
                    else if (ExpressionTreeFactory.GetPrecedence(character) <= ExpressionTreeFactory.GetPrecedence(opStack.Peek()) && ExpressionTreeFactory.GetAssociativity(character) == 'r')
                    {
                        // 5
                        opStack.Push(character);
                    }
                    else
                    {
                        // 6
                        try
                        {
                            while (ExpressionTreeFactory.GetPrecedence(character) >= ExpressionTreeFactory.GetPrecedence(opStack.Peek()) && ExpressionTreeFactory.GetAssociativity(character) == 'l')
                            {
                                pop = opStack.Pop();
                            }
                        }
                        catch (Exception)
                        {
                            opStack.Push(character);
                            postfix += pop;
                            postfix += " ";
                        }
                    }
                }
                else
                {
                    // 1
                    postfix += character;
                    postfix += " ";
                }
            }

            // 7
            pop = opStack.Pop();

            while (true)
            {
                try
                {
                    postfix += pop;
                    postfix += " ";
                    pop = opStack.Pop();
                }
                catch (Exception)
                {
                    break;
                }
            }

            return postfix;
        }

        /// <summary>
        /// Converts postfix expression into stack.
        /// </summary>
        public void StringToStack()
        {
            this.ConvertToPostFix(this.Expression);
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
            try
            {
                // Checks if Constant type.
                if (newNode is Constant testConst)
                {
                    return testConst.Value;
                }

                // Checks if Var type.
                if (newNode is Variable testVar)
                {
                    if (this.variableDict.TryGetValue(testVar.Var, out double number))
                    {
                        return number;
                    }
                    else
                    {
                        Console.WriteLine("Variable " + testVar.Var + " doesn't exist.");

                        return 0;

                        // throw new NotImplementedException();
                    }
                }

                // Checks if BinaryOperator type.
                if (newNode is BinaryOperator testOp)
                {
                    return testOp.Evaluate(this.Evaluate(testOp.Left), this.Evaluate(testOp.Right));
                }

                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        private Node Compile(string expression)
        {
            // Checks if empty.
            if (string.IsNullOrEmpty(expression))
            {
                return null;
            }

            // Recursive implementation that compiles expression string.
            // Node newNode = this.GetNode(this.ConvertToPostFix(expression));
            Node newNode = this.GetNode(expression);

            return newNode;
        }

        private Node GetNode(string expression)
        {
            // Could also start at end and decrement as an alternative.
            for (int expressionIndex = 0; expressionIndex < expression.Length - 1; expressionIndex++)
            {
                // Checks if element is valid operator.
                if (ExpressionTreeFactory.IsOperator(expression[expressionIndex]))
                {
                    BinaryOperator newOp = ExpressionTreeFactory.Create(expression[expressionIndex]);
                    newOp.Left = this.Compile(expression.Substring(0, expressionIndex));
                    newOp.Right = this.Compile(expression.Substring(expressionIndex + 1));
                    return newOp;
                }
            }

            // If expression is constant and returns as such, otherwise expression is var.
            if (double.TryParse(expression, out double number))
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