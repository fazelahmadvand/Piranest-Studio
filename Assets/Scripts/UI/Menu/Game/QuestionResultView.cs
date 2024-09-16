using Piranest.Model;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI
{
    public class QuestionResultView : View
    {
        [SerializeField] private TMP_Text gemTxt;

        [Space]
        [SerializeField] private TMP_Text resultTxt;
        [SerializeField] private Color successColor, failColor;
        [SerializeField] private string successTitle, failTitle;
        [SerializeField] private TMP_Text descriptionTxt;
        [Space]
        [SerializeField] private Button nextQuestionBtn;
        [SerializeField] private Image nextQuestionLocationImg;


        public void UpdateResult(bool isAnswerTrue, int gemPrize, GameChapterQuestion nextQuestion, Action OnClick)
        {
            Show();
            descriptionTxt.text = nextQuestion.Description;
            if (isAnswerTrue)
            {
                resultTxt.text = successTitle;
                resultTxt.color = successColor;
                gemTxt.text = gemPrize.ToString();
            }
            else
            {
                resultTxt.text = failTitle;
                resultTxt.color = failColor;
                gemTxt.text = 0.ToString();
            }
            nextQuestionBtn.SetEvent(OnClick);
        }



    }
}
