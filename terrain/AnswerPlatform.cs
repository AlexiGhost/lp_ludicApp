using jumpAndLearn.calculation;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace jumpAndLearn.terrain
{
    public class AnswerPlatform : Platform {

        private int answer;
        private TextMeshPro answerDisplay;
        public int Answer
        {
            get { return answer; }
            set
            {
                answer = value;
                answerDisplay.text = answer.ToString();
            }
        }

        [SerializeField]
        private bool isFirstPlatform;

        protected override void Start()
        {
            base.Start();
            answerDisplay = GetComponentInChildren<TextMeshPro>();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !isUsed)
            {
                base.OnTriggerEnter(other);
                if (isFirstPlatform || CalculationManager.Instance.CheckAnswer(answer))
                {
                    //Good answer (or initialisation)
                    CalculationManager.Instance.GenerateCalculation();
                    GenerateNextAnswers();
                }
                else
                {
                    //Wrong answer
                    GameManager.Instance.LoseLevel();
                }
            }
        }

        private void GenerateNextAnswers()
        {
            List<Platform> nextPlatforms = GetPlatformsByGroup(group + 1);
            foreach (Platform platform in nextPlatforms)
            {
                if(platform is AnswerPlatform)
                {
                    AnswerPlatform answerPlatform = platform as AnswerPlatform;
                    answerPlatform.Answer = CalculationManager.Instance.FakeResult();
                    GameManager.Instance.RestartTimer();
                }
                else
                {
                    //Win
                    GameManager.Instance.WinLevel();
                    return;
                }
            }
            int index = Random.Range(0, nextPlatforms.Count - 1);
            AnswerPlatform truePlatform = nextPlatforms[index] as AnswerPlatform;
            truePlatform.Answer = CalculationManager.Instance.Calculation.Answer;
        }
    }
}
