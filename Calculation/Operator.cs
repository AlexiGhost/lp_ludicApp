using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jumpAndLearn.calculation
{
    public enum Operator { ADD, SUB, MULTIPLY, DIVIDE }
    public static class OperatorExtension
    {
        public static string Description(this Operator ope)
        {
            switch (ope)
            {
                case Operator.ADD:
                    return " + ";
                case Operator.SUB:
                    return " - ";
                case Operator.MULTIPLY:
                    return " x ";
                case Operator.DIVIDE:
                    return " / ";
                default:
                    throw new System.NotImplementedException();
            }
        }

        public static int Calculate(this Operator ope, int left, int right)
        {
            switch (ope)
            {
                case Operator.ADD:
                    return Mathf.CeilToInt(left + right);
                case Operator.SUB:
                    return Mathf.CeilToInt(left - right);
                case Operator.MULTIPLY:
                    return Mathf.CeilToInt(left * right);
                case Operator.DIVIDE:
                    return Mathf.CeilToInt(left / right);
                default:
                    throw new System.NotImplementedException();
            }
        }

        public static void GenerateOperands(this Operator ope, out int leftOperand, out int rightOperand)
        {
            switch (ope)
            {
                case Operator.ADD:
                    leftOperand = Mathf.RoundToInt(Random.Range(1, 999));
                    rightOperand = Mathf.RoundToInt(Random.Range(1, 999 - leftOperand));
                    return;
                case Operator.SUB:
                    leftOperand = Mathf.RoundToInt(Random.Range(1, 999));
                    rightOperand = Mathf.RoundToInt(Random.Range(1, leftOperand)); //No negative results
                    return;
                case Operator.MULTIPLY:
                    leftOperand = Mathf.RoundToInt(Random.Range(2, 10));
                    rightOperand = Mathf.RoundToInt(Random.Range(2, 10));
                    return;
                case Operator.DIVIDE:
                    int a = Mathf.RoundToInt(Random.Range(1, 10));
                    rightOperand = Mathf.RoundToInt(Random.Range(2, 10));
                    leftOperand = a * rightOperand;
                    return;
                default:
                    throw new System.NotImplementedException();
            }
        }
    }
}
