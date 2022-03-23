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
        private static Stack<string> opStack = new Stack<string>();
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
        /// Calls private Evaluate.
        /// </summary>
        /// <returns>Evaluted Root.</returns>
        public double Evaluate()
        {
            // Called from program.cs in ExpTreeConsole
            return this.Evaluate(this.root);
        }

        private double Evaluate(Node newNode)
        {
            // Called by public evaluate method
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
            // Called by constructor and setter

            // Checks if empty.
            if (string.IsNullOrEmpty(expression))
            {
                return null;
            }

            // postfix has no practical use
            string postfix = this.ConvertToPostFix(expression);

            // Converts to postfix with spaces, then fills exprStack and removes spaces
            Node newNode = this.CompileUsingExprStack();

            return newNode;
        }

        private string ConvertToPostFix(string expression)
        {
            // Called from compile - Converts expression to postfix and creats exprStack.
            List<string> elements = new List<string>(expression.Length);
            int start = 0, expressionIndex = 0;

            // Converts string to list of strings to ensure 10 is not read as 1 0
            for (; expressionIndex < expression.Length; expressionIndex++)
            {
                if (ExpressionTreeFactory.IsOperator(expression[expressionIndex].ToString()))
                {
                    // Adds substring before op
                    elements.Add(expression.Substring(start, expressionIndex - start));

                    // Adds op
                    elements.Add(expression[expressionIndex].ToString());

                    // Sets new start to index after op
                    start = expressionIndex + 1;
                }
            }

            // Case that expression doesn't end with operator
            if (!ExpressionTreeFactory.IsOperator(expression[expressionIndex - 1].ToString()))
            {
                elements.Add(expression.Substring(start, expressionIndex - start));
            }

            // Removes additional whitespace
            elements = elements.Where(s => !string.IsNullOrEmpty(s)).ToList();

            string postfix = string.Empty;
            string pop = string.Empty;

            // Goes through each string in list (shunting alg)
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

            // Rule 7
            pop = opStack.Pop();

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

            // returns string for testcase purposes
            return postfix;
        }

        private Node GetNode(string expression)
        {
            // Called by ConvertToPostfix

            // Checks if op.
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

            throw new NotSupportedException();
        }

        private Node CompileUsingExprStack()
        {
            // Called by compile
            Stack<Node> reverse = new Stack<Node>();

            // Reverses stack
            while (exprStack.Count > 0)
            {
                reverse.Push(exprStack.Pop());
            }

            Stack<Node> result = new Stack<Node>();
            Node newNode;

            // goes through reverse exprStack and returns cur root
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
                        // Current problem
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