using Piranest.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piranest.UI
{
    public class QuestionInfoView : View
    {
        [SerializeField] private TMP_Text whichAnswerTxt, descriptionTxt;


        [SerializeField] private List<GameObject> answers = new();

        [SerializeField] private Button submitBtn;


        public void UpdateInfo(GameChapterQuestion question)
        {
            var questions = question.Options.Split(',');

            descriptionTxt.text = question.Description;
            whichAnswerTxt.text = question.Question;
        }

    }
}
