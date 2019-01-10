using jumpAndLearn.calculation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jumpAndLearn.calculation
{
    public class Calculation : MonoBehaviour {
        private Operator ope;

        private int left;
        private int right;

        public int Answer { get; private set; }

        public Calculation(Operator ope)
        {
            this.ope = ope;
            ope.GenerateOperands(out left, out right);
            Calculate();
        }
    
        public Calculation(Operator ope, int left, int right)
        {
            this.ope = ope;
            this.left = left;
            this.right = right;
            Calculate();
        }

        private void Calculate()
        {
            Answer = ope.Calculate(left, right);
        }

        public override string ToString()
        {
            return left + ope.Description() + right;
        }
    }
}
