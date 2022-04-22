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
            if (this.variableDict.TryGetValue(variableName, out double number))
            {
                this.variableDict[variableName] = variableValue;
            }
            else
            {
                this.variableDict.Add(variableName, variableValue);
            }
        }

        /// <summary>
        /// Called from ConvertToPostFix & GetVariables in Spreadsheet.cs - Converts string to list of strings (10 instead of 1 0).
        /// </summary>
        /// <param name="expression">expression.</param>
        /// <returns>List of available variables names.</returns>
        public List<string> GetExprList(string expression)
        {
            List<string> names = new List<string>();

            int start = 0, expressionIndex = 0;

            for (; expressionIndex < expression.Length; expressionIndex++)
            {
                if (ExpressionTreeFactory.IsOperator(expression[expressionIndex].ToString()))
                {
                    // Adds substring before op
                    names.Add(expression.Substring(start, expressionIndex - start));

                    // Adds op
                    names.Add(expression[expressionIndex].ToString());

                    // Sets new start to index after op
                    start = expressionIndex + 1;
                }
            }

            // Case that expression doesn't end with operator
            if (!ExpressionTreeFactory.IsOperator(expression[expressionIndex - 1].ToString()))
            {
                names.Add(expression.Substring(start, expressionIndex - start));
            }

            // Removes additional whitespace
            names = names.Where(s => !string.IsNullOrEmpty(s)).ToList();

            return names;
        }

        /// <summary>
        /// Called from program.cs in ExpTreeConsole & from spreadsheet.cs in Spreadsheet_Adam_Nassar - calls private Evaluate.
        /// </summary>
        /// <returns>Evaluted Root.</returns>
        public double Evaluate()
        {
            return this.Evaluate(this.root);
        }

        // Called by public evaluate method - Evaluates given expression tree.
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

                        throw new ArgumentNullException();
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

        // Called by constructor and setter - Converts to PostFix and then builds expression tree.
        private Node Compile(string expression)
        {
            if (!string.IsNullOrEmpty(expression))
            {
                // postfix has no practical use
                string postfix = this.ConvertToPostFix(expression);

                return this.BuildUsingExprStack();
            }

            return null;
        }

        // Called from compile - Converts expression to postfix and fills static member exprStack.
        private string ConvertToPostFix(string expression)
        {
            string postfix = string.Empty, pop = string.Empty;
            Stack<string> opStack = new Stack<string>();

            List<string> elements = this.GetExprList(expression);

            /* SHUNTING BEGIN */
            foreach (string elem in elements)
            {
                if (elem == "(")
                {
                    // Rule 2
                    opStack.Push(elem);
                }
                else if (elem == ")")
                {
                    // Rule 3
                    pop = opStack.Pop();

                    while (pop != "(")
                    {
                        // Adding to string and exprStack.
                        postfix += pop;
                        exprStack.Push(this.GetNode(pop));

                        pop = opStack.Pop();
                    }
                }
                else if (ExpressionTreeFactory.IsOperator(elem))
                {
                    if (opStack.Count == 0 || opStack.Peek() == "(")
                    {
                        // Rule 4
                        opStack.Push(elem);
                    }
                    else if (ExpressionTreeFactory.GetPrecedence(elem) <= ExpressionTreeFactory.GetPrecedence(opStack.Peek()) && ExpressionTreeFactory.GetAssociativity(elem) == 'r')
                    {
                        // Rule 5
                        opStack.Push(elem);
                    }
                    else
                    {
                        // Rule 6
                        try
                        {
                            while (ExpressionTreeFactory.GetPrecedence(elem) >= ExpressionTreeFactory.GetPrecedence(opStack.Peek()) && ExpressionTreeFactory.GetAssociativity(elem) == 'l')
                            {
                                pop = opStack.Pop();
                            }
                        }
                        catch (Exception)
                        {
                            opStack.Push(elem);

                            postfix += pop;
                            exprStack.Push(this.GetNode(pop));
                        }
                    }
                }
                else
                {
                    // Rule 1
                    postfix += elem;
                    exprStack.Push(this.GetNode(elem));
                }
            }

            // Rule 7 (all the remaining code)
            try
            {
                pop = opStack.Pop();
            }
            catch
            {
                // no operators ever used
            }

            // Unecessary if only one element in exprStack
            if (exprStack.Count > 1)
            {
                while (true)
                {
                    try
                    {
                        postfix += pop;
                        exprStack.Push(this.GetNode(pop));

                        pop = opStack.Pop();
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
            }

            /* SHUNTING END */

            return postfix;
        }

        // Called by ConvertToPostfix - Converts string to one of the 3 types of nodes
        private Node GetNode(string expression)
        {
            if (ExpressionTreeFactory.IsOperator(expression))
            {
                return ExpressionTreeFactory.Create(expression);
            }
            else if (double.TryParse(expression, out double number))
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
        }

        // Called by compile - Builds expression tree
        private Node BuildUsingExprStack()
        {
            Stack<Node> reverse = new Stack<Node>();

            // Reverses stack (stack LIFO, so operators would be compiled first, which we don't want)
            while (exprStack.Count > 0)
            {
                reverse.Push(exprStack.Pop());
            }

            Node newNode;
            Stack<Node> result = new Stack<Node>();

            // Goes through reverse exprStack and returns current root
            while (reverse.Count > 0)
            {
                newNode = reverse.Pop();

                if (newNode is Constant newConst)
                {
                    result.Push(newConst);
                }

                if (newNode is Variable newVar)
                {
                    result.Push(newVar);
                }

                if (newNode is BinaryOperator newOp)
                {
                    if (newOp != null)
                    {
                        newOp.Right = result.Pop();
                        newOp.Left = result.Pop();

                        result.Push(newOp);
                    }
                }
            }

            return result.Pop();
        }
    }
}