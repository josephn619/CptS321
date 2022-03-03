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
            // this.root = this.Compile(expression);
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

                // this.root = this.Compile(value);
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
            Constant testConst = newNode as Constant;
            if (testConst != null)
            {
                return testConst.Value;
            }

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

            BinaryOperator testOp = newNode as BinaryOperator;
            if (testOp != null)
            {
                return testOp.Evaluate(this.Evaluate(testOp.Left), this.Evaluate(testOp.Right));
            }

            throw new NotSupportedException();
        }
    }
}
