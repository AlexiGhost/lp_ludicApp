using UnityEngine;

namespace jumpAndLearn.calculation
{
    public class CalculationManager : MonoBehaviour {
        public static CalculationManager Instance { get; private set; }

        public Calculation Calculation { get; private set; }
        [SerializeField]
        private int fakeResultStandardDeviationPercentage = 35; //Ecart type des résultats erronés (en %)

        private void Start()
        {
            Instance = this;
        }

        public void GenerateCalculation()
        {
            Operator ope = (Operator)Mathf.RoundToInt(Random.Range(0, 4));
            Calculation = new Calculation(ope);
            NeedDisplay();
        }

        public bool CheckAnswer(int answer)
        {
            return (answer == Calculation.Answer);
        }

        public int FakeResult()
        {
            bool isBigger = Mathf.RoundToInt(Random.Range(0, 1)) == 1 ? true : false;
            float gap = Mathf.Round(Random.Range(1, fakeResultStandardDeviationPercentage)) / 100;
            int approximateResult;
            if (isBigger) approximateResult = Mathf.RoundToInt(Calculation.Answer * (1f + gap));
            else approximateResult = Mathf.RoundToInt(Calculation.Answer * (1f - gap));

            if (approximateResult != Calculation.Answer)
                return approximateResult;
            else
                return isBigger ? approximateResult + 1 : approximateResult - 1;
        }

        private void NeedDisplay()
        {
            GameManager.Instance.DisplayText(Calculation.ToString());
        }
    }
}
